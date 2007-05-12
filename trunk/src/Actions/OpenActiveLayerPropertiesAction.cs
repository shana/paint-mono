/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;

namespace PaintDotNet.Actions
{
    public sealed class OpenActiveLayerPropertiesAction
        : DocumentWorkspaceAction
    {
        public override HistoryMemento PerformAction(DocumentWorkspace documentWorkspace)
        {
            bool oldDirtyValue = documentWorkspace.Document.Dirty;

            using (Form lpd = documentWorkspace.ActiveLayer.CreateConfigDialog())
            {
                DialogResult result = Utility.ShowDialog(lpd, documentWorkspace.FindForm());

                if (result == DialogResult.Cancel)
                {
                    documentWorkspace.Document.Dirty = oldDirtyValue;
                }
            }

            return null;
        }

        public OpenActiveLayerPropertiesAction()
            : base(ActionFlags.KeepToolActive)
        {
            // This action does not require that the current tool be deactivated.
        }
    }
}
