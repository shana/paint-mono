/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;

namespace PaintDotNet.Actions
{
    public sealed class ZoomToWindowAction
        : DocumentWorkspaceAction
    {
        public override HistoryMemento PerformAction(DocumentWorkspace documentWorkspace)
        {
            if (documentWorkspace.ZoomBasis == ZoomBasis.FitToWindow)
            {
                documentWorkspace.ZoomBasis = ZoomBasis.ScaleFactor;
            }
            else
            {
                documentWorkspace.ZoomBasis = ZoomBasis.FitToWindow;
            } 
            
            return null;
        }

        public ZoomToWindowAction()
            : base(ActionFlags.KeepToolActive)
        {
        }
    }
}
