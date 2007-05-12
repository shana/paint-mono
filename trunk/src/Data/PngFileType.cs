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

namespace PaintDotNet
{
    public sealed class PngFileType
        : GdiPlusFileType
    {
        public PngFileType()
            : base("PNG", ImageFormat.Png, false, new string[] { ".png" })
        {
        }

        public override bool IsReflexive(SaveConfigToken token)
        {
            return true;
        }
    }
}
