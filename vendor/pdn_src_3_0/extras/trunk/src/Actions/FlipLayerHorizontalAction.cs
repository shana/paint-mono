/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet.HistoryFunctions;
using System;

namespace PaintDotNet.Actions
{
    public class FlipLayerHorizontalFunction
        : FlipLayerFunction
    {
        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("FlipLayerHorizontalAction.Name");
            }
        }

        public FlipLayerHorizontalFunction(int layerIndex)
            : base(StaticName,
                   ImageResource.Get("Icons.MenuLayersFlipHorizontalIcon.png"), 
                   FlipType.Horizontal,
                   layerIndex)
        {
        }
    }
}
