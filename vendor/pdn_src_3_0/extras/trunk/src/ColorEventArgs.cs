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

namespace PaintDotNet
{
    [Serializable]
    public class ColorEventArgs
        : System.EventArgs
    {
        private ColorBgra color;
        public ColorBgra Color
        {
            get
            {
                return color;
            }
        }

        public ColorEventArgs(ColorBgra color)
        {
            this.color = color;
        }
    }
}
