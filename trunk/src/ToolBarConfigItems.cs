/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace PaintDotNet
{
    [Flags]
    public enum ToolBarConfigItems
        : uint
    {
        None = 0,
        All = ~None,

        // IMPORTANT: Keep these in alphabetical order.
        AlphaBlending = 1,
        Antialiasing = 2,
        Brush = 4,
        ColorPickerBehavior = 8,
        Gradient = 16,
        Pen = 32,
        ShapeType = 64,
        Resampling = 128,
        Text = 256,     
        Tolerance = 512,
    }
}
