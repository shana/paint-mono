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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PaintDotNet
{
    public sealed class HeaderLabel 
        : Control
    {
        private Control leftMask;
        private Control rightMask;
        private int leftMargin = 1;
        private int rightMargin = 8;
        private System.Windows.Forms.GroupBox groupBox;

        public override string Text
        {
            get
            {
                if (this.groupBox == null)
                {
                    return string.Empty;
                }
                else
                {
                    string text = this.groupBox.Text;

                    if (text.Length > 2)
                    {
                        text = text.Substring(0, text.Length - 2);
                    }

                    return text;
                }
            }

            set
            {
                if (this.groupBox != null)
                {
                    if (value == null)
                    {
                        this.groupBox.Text = string.Empty;
                    }
                    else if (value.Length >= 1)
                    {
                        this.groupBox.Text = value + "  ";
                    }
                    else
                    {
                        this.groupBox.Text = string.Empty;
                    }
                }
            }
        }

        [DefaultValue(8)]
        public int RightMargin
        {
            get
            {
                return this.rightMargin;
            }

            set
            {
                this.rightMargin = value;
                PerformLayout();
            }
        }

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public HeaderLabel()
        {
            UI.InitScaling(null);

            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            PerformLayout();
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.leftMask = new System.Windows.Forms.Control();
            this.rightMask = new System.Windows.Forms.Control();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Location = new System.Drawing.Point(128, 144);
            this.groupBox.Name = "groupBox";
            this.groupBox.TabStop = false;
            this.groupBox.FlatStyle = FlatStyle.System;
            // 
            // leftMask
            // 
            this.leftMask.Location = new System.Drawing.Point(0, 0);
            this.leftMask.Name = "leftMask";
            this.leftMask.TabStop = false;
            // 
            // rightMask
            // 
            this.rightMask.Location = new System.Drawing.Point(0, 0);
            this.rightMask.Name = "rightMask";
            this.rightMask.TabStop = false;
            // 
            // HeaderLabel
            // 
            this.Controls.Add(this.leftMask);
            this.Controls.Add(this.rightMask);
            this.Controls.Add(this.groupBox);
            this.TabStop = false;
            this.Name = "HeaderLabel";
            this.Size = new System.Drawing.Size(144, 14);
            this.ResumeLayout(false);

        }
        #endregion

        protected override void OnLayout(LayoutEventArgs levent)
        {
            this.groupBox.Location = new Point(-8 + UI.ScaleWidth(leftMargin), 0);
            this.groupBox.Size = new Size(this.ClientRectangle.Width + 16, this.ClientRectangle.Height + 16);
            this.leftMask.Location = new Point(-1, 0);
            this.leftMask.Size = new Size(1 + leftMargin, this.ClientRectangle.Height);
            this.rightMask.Location = new Point(this.ClientRectangle.Width - rightMargin, 0);
            this.rightMask.Size = new Size(1 + rightMargin, this.ClientRectangle.Height);

            this.leftMask.Visible = false;

            base.OnLayout(levent);
        }
    }
}
