/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;

namespace PaintDotNet
{
    // TODO: use EventArgs<Layer>
    public class LayerEventArgs 
        : EventArgs
    {
        Layer layer;

        public Layer Layer
        {
            get
            {
                return layer;
            }
        }

        public LayerEventArgs(Layer layer)
        {
            this.layer = layer;
        }
    }
}
