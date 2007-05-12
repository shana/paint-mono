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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PaintDotNet
{
    public class FlowPanel 
        : Control
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private int forcedHeight;

        [DefaultValue(27)]
        public int ForcedHeight
        {
            get
            {
                return forcedHeight;
            }

            set
            {
                forcedHeight = value;
                Invalidate();
            }
        }

        public FlowPanel()
        {
            forcedHeight = 27;

            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
        }

        private Point[] GetLayoutPoints(Control.ControlCollection controls)
        {
            Point[] points = new Point[controls.Count];
            Point cursor = new Point(0, 0);

            for (int i = controls.Count - 1; i >= 0; --i)
            {
                Control c = controls[i];
                
                if (cursor.X != 0 && (cursor.X + c.Width >= this.Width))
                {
                    cursor = new Point(0, cursor.Y + this.forcedHeight);
                }

                points[i] = cursor;
                cursor = new Point(cursor.X + c.Width, cursor.Y);
            }

            return points;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize (e);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            if (Controls.Count > 0)
            {
                Point[] points = GetLayoutPoints(Controls);

                for (int i = 0; i < Controls.Count; ++i)
                {
                    Controls[i].Location = points[i];
                    Controls[i].Height = forcedHeight;
                }

                this.Height = points[0].Y + forcedHeight; // ensure exact height
            }

            base.OnLayout(levent);
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                    components = null;
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}
