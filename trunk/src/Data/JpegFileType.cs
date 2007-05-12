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
using System.Windows.Forms;

namespace PaintDotNet
{
    public sealed class JpegFileType
        : GdiPlusFileType
    {
        public JpegFileType()
            : base("JPEG", ImageFormat.Jpeg, false, new string[] { ".jpg", ".jpeg", ".jpe", ".jfif" })
        {
        }

        protected override SaveConfigToken OnCreateDefaultSaveConfigToken()
        {
            return new JpegSaveConfigToken(95);
        }

        public override SaveConfigWidget CreateSaveConfigWidget()
        {
            return new JpegSaveConfigWidget();
        }

        protected override void OnSave(Document input, Stream output, SaveConfigToken token, Surface scratchSurface, ProgressEventHandler callback)
        {
            JpegSaveConfigToken jsct = (JpegSaveConfigToken)token;

            ImageCodecInfo icf = GdiPlusFileType.GetImageCodecInfo(ImageFormat.Jpeg);
            EncoderParameters parms = new EncoderParameters(1);
            EncoderParameter parm = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, jsct.Quality); // force '95% quality'
            parms.Param[0] = parm;

            scratchSurface.Clear(ColorBgra.White);

            using (RenderArgs ra = new RenderArgs(scratchSurface))
            {
                input.Render(ra, true);
            }
            
            using (Bitmap bitmap = scratchSurface.CreateAliasedBitmap())
            {
                GdiPlusFileType.LoadProperties(bitmap, input);
                bitmap.Save(output, icf, parms);
            }
        }
    }
}