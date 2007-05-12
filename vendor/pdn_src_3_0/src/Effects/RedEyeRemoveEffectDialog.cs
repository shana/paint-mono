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

namespace PaintDotNet.Effects
{
    public sealed class RedEyeRemoveEffectDialog 
        : TwoAmountsConfigDialogBase
    {
        private System.Windows.Forms.Label usageHintLabel;
        private System.ComponentModel.IContainer components = null;

        public RedEyeRemoveEffectDialog()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            this.usageHintLabel.Text = PdnResources.GetString("RedEyeRemoveEffectDialog.UsageHintLabel.Text");
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

        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.usageHintLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(101, 211);
            this.okButton.Size = new System.Drawing.Size(81, 23);
            this.okButton.Name = "okButton";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(188, 211);
            this.cancelButton.Size = new System.Drawing.Size(81, 23);
            this.cancelButton.Name = "cancelButton";
            // 
            // usageHintLabel
            // 
            this.usageHintLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.usageHintLabel.Location = new System.Drawing.Point(8, 147);
            this.usageHintLabel.Name = "usageHintLabel";
            this.usageHintLabel.Size = new System.Drawing.Size(240, 64);
            this.usageHintLabel.TabIndex = 9;
            // 
            // RedEyeRemoveEffectDialog
            // 
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(275, 240);
            this.Controls.Add(this.usageHintLabel);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "RedEyeRemoveEffectDialog";
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.Controls.SetChildIndex(this.usageHintLabel, 0);
            this.ResumeLayout(false);

        }
        #endregion
    }
}

