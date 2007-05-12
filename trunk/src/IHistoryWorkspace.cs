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
    public interface IHistoryWorkspace
    {
        Document Document
        {
            get;
            set;
        }

        Selection Selection
        {
            get;
        }

        Layer ActiveLayer
        {
            get;
        }

        int ActiveLayerIndex
        {
            get;
        }
    }
}
