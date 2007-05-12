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
    public sealed class JpegSaveConfigWidget 
        : SaveConfigWidget
    {
        private System.Windows.Forms.TrackBar qualitySlider;
        private System.Windows.Forms.Label qualityLabel;
        private System.Windows.Forms.NumericUpDown qualityUpDown;
        private System.ComponentModel.IContainer components = null;

        public JpegSaveConfigWidget()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            this.qualityLabel.Text = PdnResources.GetString("JpegSaveConfigWidget.QualityLabel.Text");
        }

        protected override void InitFileType()
        {
            this.fileType = new JpegFileType();
        }

        protected override void InitTokenFromWidget()
        {
            ((JpegSaveConfigToken)this.Token).Quality = this.qualitySlider.Value;
        }

        protected override void InitWidgetFromToken(SaveConfigToken token)
        {
            this.qualitySlider.Value = ((JpegSaveConfigToken)token).Quality;
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
            this.qualitySlider = new System.Windows.Forms.TrackBar();
            this.qualityLabel = new System.Windows.Forms.Label();
            this.qualityUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.qualitySlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qualityUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // qualitySlider
            // 
            this.qualitySlider.Location = new System.Drawing.Point(0, 24);
            this.qualitySlider.Maximum = 100;
            this.qualitySlider.Minimum = 1;
            this.qualitySlider.Name = "qualitySlider";
            this.qualitySlider.Size = new System.Drawing.Size(180, 42);
            this.qualitySlider.TabIndex = 1;
            this.qualitySlider.TickFrequency = 10;
            this.qualitySlider.Value = 1;
            this.qualitySlider.ValueChanged += new System.EventHandler(this.qualitySlider_ValueChanged);
            // 
            // qualityLabel
            // 
            this.qualityLabel.Location = new System.Drawing.Point(7, 3);
            this.qualityLabel.Name = "qualityLabel";
            this.qualityLabel.Size = new System.Drawing.Size(97, 18);
            this.qualityLabel.TabIndex = 1;
            // 
            // qualityUpDown
            // 
            this.qualityUpDown.Location = new System.Drawing.Point(115, 0);
            this.qualityUpDown.Minimum = new System.Decimal(new int[] {
                                                                          1,
                                                                          0,
                                                                          0,
                                                                          0});
            this.qualityUpDown.Name = "qualityUpDown";
            this.qualityUpDown.Size = new System.Drawing.Size(56, 20);
            this.qualityUpDown.TabIndex = 0;
            this.qualityUpDown.Value = new System.Decimal(new int[] {
                                                                        1,
                                                                        0,
                                                                        0,
                                                                        0});
            this.qualityUpDown.Enter += new System.EventHandler(this.qualityUpDown_Enter);
            this.qualityUpDown.ValueChanged += new System.EventHandler(this.qualityUpDown_ValueChanged);
            this.qualityUpDown.Leave += new System.EventHandler(this.qualityUpDown_Leave);
            // 
            // JpegSaveConfigWidget
            // 
            this.Controls.Add(this.qualityUpDown);
            this.Controls.Add(this.qualityLabel);
            this.Controls.Add(this.qualitySlider);
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Name = "JpegSaveConfigWidget";
            this.Size = new System.Drawing.Size(180, 72);
            ((System.ComponentModel.ISupportInitialize)(this.qualitySlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qualityUpDown)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void qualitySlider_ValueChanged(object sender, System.EventArgs e)
        {
            if (this.qualityUpDown.Value != (decimal)this.qualitySlider.Value)
            {
                this.qualityUpDown.Value = (decimal)this.qualitySlider.Value;
            }

            UpdateToken();
        }

        private void qualityUpDown_ValueChanged(object sender, System.EventArgs e)
        {
            if (this.qualitySlider.Value != (int)this.qualityUpDown.Value)
            {
                this.qualitySlider.Value = (int)this.qualityUpDown.Value;
            }
        }

        private void qualityUpDown_Leave(object sender, System.EventArgs e)
        {
            qualityUpDown_ValueChanged(sender, e);
        }

        private void qualityUpDown_Enter(object sender, System.EventArgs e)
        {
            qualityUpDown.Select(0, qualityUpDown.Text.Length);
        }
    }
}

