/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;

namespace PaintDotNet.HistoryFunctions
{
    public sealed class FlipDocumentHorizontalFunction
        : FlipDocumentFunction
    {
        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("FlipDocumentHorizontalAction.Name");
            }
        }

        public FlipDocumentHorizontalFunction()
            : base(StaticName,
                   ImageResource.Get("Icons.MenuImageFlipHorizontalIcon.png"), 
                   FlipType.Horizontal)
        {
        }
    }
}
