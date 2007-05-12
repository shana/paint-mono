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
    public sealed class ZoomToSelectionAction
        : DocumentWorkspaceAction
    {
        public override HistoryMemento PerformAction(DocumentWorkspace documentWorkspace)
        {
            documentWorkspace.ZoomToSelection();
            return null;
        }

        public ZoomToSelectionAction()
            : base(ActionFlags.KeepToolActive)
        {
        }
    }
}
