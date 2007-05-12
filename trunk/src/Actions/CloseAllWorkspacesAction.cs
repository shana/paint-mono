/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet.SystemLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PaintDotNet.Actions
{
    public sealed class CloseAllWorkspacesAction
        : AppWorkspaceAction
    {
        private bool cancelled;

        public bool Cancelled
        {
            get
            {
                return this.cancelled;
            }
        }

        public override void PerformAction(AppWorkspace appWorkspace)
        {
            int oldLatency = appWorkspace.Widgets.DocumentStrip.ThumbnailUpdateLatency;
            appWorkspace.Widgets.DocumentStrip.ThumbnailUpdateLatency = 0;

            List<DocumentWorkspace> unsavedDocs = new List<DocumentWorkspace>();
            foreach (DocumentWorkspace dw in appWorkspace.DocumentWorkspaces)
            {
                if (dw.Document != null && dw.Document.Dirty)
                {
                    unsavedDocs.Add(dw);
                }
            }

            if (unsavedDocs.Count == 1)
            {
                CloseWorkspaceAction cwa = new CloseWorkspaceAction(unsavedDocs[0]);
                cwa.PerformAction(appWorkspace);
                this.cancelled = cwa.Cancelled;
            }
            else if (unsavedDocs.Count > 1)
            {
                using (UnsavedChangesDialog dialog = new UnsavedChangesDialog())
                {
                    dialog.DocumentClicked +=
                        delegate(object sender, EventArgs<DocumentWorkspace> e2)
                        {
                            appWorkspace.ActiveDocumentWorkspace = e2.Data;
                        };

                    dialog.Documents = unsavedDocs.ToArray();

                    if (appWorkspace.ActiveDocumentWorkspace.Document.Dirty)
                    {
                        dialog.SelectedDocument = appWorkspace.ActiveDocumentWorkspace;
                    }

                    Form mainForm = appWorkspace.FindForm();
                    if (mainForm != null)
                    {
                        PdnBaseForm asPDF = mainForm as PdnBaseForm;

                        if (asPDF != null)
                        {
                            asPDF.RestoreWindow();
                        }
                    }

                    DialogResult dr = Utility.ShowDialog(dialog, appWorkspace);

                    switch (dr)
                    {
                        case DialogResult.Yes:
                            {
                                foreach (DocumentWorkspace dw in unsavedDocs)
                                {
                                    appWorkspace.ActiveDocumentWorkspace = dw;
                                    bool result = dw.DoSave();

                                    if (result)
                                    {
                                        appWorkspace.RemoveDocumentWorkspace(dw);
                                    }
                                    else
                                    {
                                        this.cancelled = true;
                                        break;
                                    }
                                }
                            }
                            break;

                        case DialogResult.No:
                            this.cancelled = false;
                            break;

                        case DialogResult.Cancel:
                            this.cancelled = true;
                            break;

                        default:
                            throw new InvalidEnumArgumentException();
                    }
                }

                appWorkspace.Widgets.DocumentStrip.ThumbnailUpdateLatency = oldLatency;
            }

            if (!this.cancelled)
            {
                UI.SuspendControlPainting(appWorkspace);
                
                foreach (DocumentWorkspace dw in appWorkspace.DocumentWorkspaces)
                {
                    appWorkspace.RemoveDocumentWorkspace(dw);
                }

                UI.ResumeControlPainting(appWorkspace);
                appWorkspace.Invalidate(true);
            }
        }

        public CloseAllWorkspacesAction()
        {
            this.cancelled = false;
        }
    }
}
