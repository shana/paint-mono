/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet.Data.Quantize;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PaintDotNet
{
    public sealed class GifFileType
        : GdiPlusFileType
    {
        public GifFileType()
            : base("GIF", ImageFormat.Gif, false, new string[] { ".gif" }, true)
        {
        }

        protected override SaveConfigToken OnCreateDefaultSaveConfigToken()
        {
            return new GifSaveConfigToken(128, true, 7);
        }

        public override SaveConfigWidget CreateSaveConfigWidget()
        {
            return new GifSaveConfigWidget();
        }

        protected override void OnSave(Document input, Stream output, SaveConfigToken token, Surface scratchSurface, ProgressEventHandler progressCallback)
        {
            GifSaveConfigToken gsct = (GifSaveConfigToken)token;

            // Flatten and pre-process the image
            scratchSurface.Clear(ColorBgra.FromBgra(255, 255, 255, 0));

            using (RenderArgs ra = new RenderArgs(scratchSurface))
            {
                input.Render(ra, true);
            }

            for (int y = 0; y < scratchSurface.Height; ++y)
            {
                unsafe
                {
                    ColorBgra* ptr = scratchSurface.GetRowAddressUnchecked(y);

                    for (int x = 0; x < scratchSurface.Width; ++x)
                    {
                        if (ptr->A < gsct.Threshold)
                        {
                            ptr->Bgra = 0;
                        }
                        else
                        {
                            if (gsct.PreMultiplyAlpha)
                            {
                                int r = ((ptr->R * ptr->A) + (255 * (255 - ptr->A))) / 255;
                                int g = ((ptr->G * ptr->A) + (255 * (255 - ptr->A))) / 255;
                                int b = ((ptr->B * ptr->A) + (255 * (255 - ptr->A))) / 255;
                                int a = 255;

                                *ptr = ColorBgra.FromBgra((byte)b, (byte)g, (byte)r, (byte)a);
                            }
                            else
                            {
                                ptr->Bgra |= 0xff000000;
                            }
                        }

                        ++ptr;
                    }
                }
            }

            using (Bitmap quantized = Quantize(scratchSurface, gsct.DitherLevel, 255, progressCallback))
            {
                quantized.Save(output, ImageFormat.Gif);
            }
        }
    }
}
