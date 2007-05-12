/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet.Actions;
using PaintDotNet.HistoryMementos;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace PaintDotNet.Tools
{
    public class MagicWandTool
        : FloodToolBase
    {
        private Cursor cursorMouseUp;
        private CombineMode combineMode;

        // nothing = replace
        // Ctrl = union
        // RMB = exclude
        // Ctrl+RMB = xor

        protected override void OnActivate()
        {
            DocumentWorkspace.EnableSelectionTinting = true;
            this.cursorMouseUp = new Cursor(PdnResources.GetResourceStream("Cursors.MagicWandToolCursor.cur"));
            this.Cursor = cursorMouseUp;
            base.OnActivate();
        }

        protected override void OnDeactivate()
        {
            if (cursorMouseUp != null)
            {
                cursorMouseUp.Dispose();
                cursorMouseUp = null;
            }

            DocumentWorkspace.EnableSelectionTinting = false;
            base.OnDeactivate();
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            Cursor = cursorMouseUp;
            base.OnMouseUp(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            if ((ModifierKeys & Keys.Control) != 0 && e.Button == MouseButtons.Left)
            {
                this.combineMode = CombineMode.Union;
            }
            else if ((ModifierKeys & Keys.Control) != 0 && e.Button == MouseButtons.Right)
            {
                this.combineMode = CombineMode.Xor;
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.combineMode = CombineMode.Exclude;
            }
            else
            {
                this.combineMode = CombineMode.Replace;
            }

            base.OnMouseDown(e);
        }

        protected override void OnFillRegionComputed(Point[][] polygonSet)
        {
            SelectionHistoryMemento undoAction = new SelectionHistoryMemento(this.Name, this.Image, this.DocumentWorkspace);

            Selection.PerformChanging();
            Selection.SetContinuation(polygonSet, this.combineMode);
            Selection.CommitContinuation();
            Selection.PerformChanged();

            HistoryStack.PushNewMemento(undoAction);           
        }

        public MagicWandTool(DocumentWorkspace documentWorkspace)
            : base(documentWorkspace,
                   ImageResource.Get("Icons.MagicWandToolIcon.png"),
                   PdnResources.GetString("MagicWandTool.Name"),
                   PdnResources.GetString("MagicWandTool.HelpText"), 
                   's',
                   false,
                   ToolBarConfigItems.None)
        {
            LimitToSelection = false;
        }
    }
}