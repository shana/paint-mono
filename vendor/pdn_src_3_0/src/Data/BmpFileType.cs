/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PaintDotNet
{
    public sealed class BmpFileType
        : GdiPlusFileType
    {
        public BmpFileType()
            : base(PdnResources.GetString("BmpFileType.Name"),
                   ImageFormat.Bmp, 
                   false, 
                   new string[] { ".bmp" })
        {
        }

        public override bool IsReflexive(SaveConfigToken token)
        {
            return false;
        }

        protected override Document OnLoad(Stream input)
        {
            // This allows us to open images that were created in Explorer using New -> Bitmap Image
            // which actually just creates a 0-byte file
            if (input.Length == 0)
            {
                Document newDoc = new Document(800, 600);
                Layer layer = Layer.CreateBackgroundLayer(newDoc.Width, newDoc.Height);
                newDoc.Layers.Add(layer);
                return newDoc;
            }
            else
            {
                return base.OnLoad(input);
            }
        }

        private unsafe void SquishSurfaceTo24Bpp(Surface surface)
        {
            byte *dst = (byte *)surface.GetRowAddress(0);
            int byteWidth = surface.Width * 3;
            int stride24bpp = ((byteWidth + 3) / 4) * 4; // round up to multiple of 4
            int delta = stride24bpp - byteWidth;

            for (int y = 0; y < surface.Height; ++y)
            {
                ColorBgra *src = surface.GetRowAddress(y);
                ColorBgra *srcEnd = src + surface.Width;

                while (src < srcEnd)
                {
                    dst[0] = src->B;
                    dst[1] = src->G;
                    dst[2] = src->R;
                    ++src;
                    dst += 3;
                }

                dst += delta;
            }

            return;
        }

        private unsafe Bitmap CreateAliased24BppBitmap(Surface surface)
        {
            int stride = surface.Width * 3;
            int realStride = ((stride + 3) / 4) * 4; // round up to multiple of 4
            return new Bitmap(surface.Width, surface.Height, realStride, PixelFormat.Format24bppRgb, new IntPtr(surface.Scan0.VoidStar));
        }

        protected override void OnSave(Document input, Stream output, SaveConfigToken token, Surface scratchSurface, ProgressEventHandler callback)
        {
            ImageCodecInfo icf = GdiPlusFileType.GetImageCodecInfo(ImageFormat.Bmp);
            EncoderParameters parms = new EncoderParameters(1);
            EncoderParameter parm = new EncoderParameter(System.Drawing.Imaging.Encoder.ColorDepth, 24); // BMP's should always save as 24-bit
            parms.Param[0] = parm;

            scratchSurface.Clear(ColorBgra.White);

            using (RenderArgs ra = new RenderArgs(scratchSurface))
            {
                input.Render(ra, true);
            }

            // In order to save memory, we 'squish' the 32-bit bitmap down to 24-bit in-place
            // instead of allocating a new bitmap and copying it over.
            SquishSurfaceTo24Bpp(scratchSurface);

            using (Bitmap bitmap = CreateAliased24BppBitmap(scratchSurface))
            {
                GdiPlusFileType.LoadProperties(bitmap, input);
                bitmap.Save(output, icf, parms);
            }
        }
    }
}
