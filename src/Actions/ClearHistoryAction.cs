/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet.HistoryMementos;
using System;
using System.Windows.Forms;

namespace PaintDotNet.Actions
{
    public sealed class ClearHistoryAction
        : DocumentWorkspaceAction
    {
        public override HistoryMemento PerformAction(DocumentWorkspace documentWorkspace)
        {
            if (DialogResult.Yes == Utility.AskYesNo(documentWorkspace, 
                PdnResources.GetString("ClearHistory.Confirmation")))
            {
                documentWorkspace.History.ClearAll();

                documentWorkspace.History.PushNewMemento(new NullHistoryMemento(
                    PdnResources.GetString("ClearHistory.HistoryMementoName"),
                    ImageResource.Get("Icons.MenuLayersDeleteLayerIcon.png")));
            }

            return null;
        }

        public ClearHistoryAction()
            : base(ActionFlags.None)
        {
        }
    }
}
