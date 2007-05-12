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
    public sealed class ClearMruListAction
        : AppWorkspaceAction
    {
        public override void PerformAction(AppWorkspace appWorkspace)
        {
            string question = PdnResources.GetString("ClearOpenRecentList.Dialog.Text");
            DialogResult result = Utility.AskYesNo(appWorkspace, question);

            if (result == DialogResult.Yes)
            {
                appWorkspace.MostRecentFiles.Clear();
                appWorkspace.MostRecentFiles.SaveMruList();
            }
        }

        public ClearMruListAction()
        {
        }
    }
}
