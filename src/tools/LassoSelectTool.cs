/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PaintDotNet.Tools
{
    public class LassoSelectTool
        : SelectionTool
    {
        private Cursor lassoToolCursor;

        protected override void OnActivate()
        {
            this.lassoToolCursor = new Cursor(PdnResources.GetResourceStream("Cursors.LassoSelectToolCursor.cur"));
            this.Cursor = this.lassoToolCursor;
            base.OnActivate();
        }

        protected override void OnDeactivate()
        {
            if (this.lassoToolCursor != null)
            {
                this.lassoToolCursor.Dispose();
                this.lassoToolCursor = null;
            }

            base.OnDeactivate();
        }

        public LassoSelectTool(DocumentWorkspace documentWorkspace)
            : base(documentWorkspace,
                   ImageResource.Get("Icons.LassoSelectToolIcon.png"),
                   PdnResources.GetString("LassoSelectTool.Name"),
                   PdnResources.GetString("LassoSelectTool.HelpText"),
                   's',
                   ToolBarConfigItems.None)
        {
        }
    }
}
