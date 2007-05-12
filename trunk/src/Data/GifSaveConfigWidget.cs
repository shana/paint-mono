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
    public sealed class GifSaveConfigWidget 
        : SaveConfigWidget
    {
        private System.Windows.Forms.TrackBar thresholdSlider;
        private System.Windows.Forms.Label thresholdLabel;
        private System.Windows.Forms.NumericUpDown thresholdUpDown;
        private System.Windows.Forms.CheckBox preMultiplyAlphaCheckBox;
        private System.Windows.Forms.NumericUpDown ditherUpDown;
        private System.Windows.Forms.Label ditherLabel;
        private System.Windows.Forms.TrackBar ditherSlider;
        private Label thresholdInfoLabel;
        private System.ComponentModel.IContainer components = null;

        public GifSaveConfigWidget()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            this.thresholdLabel.Text = PdnResources.GetString("GifSaveConfigWidget.ThresholdLabel.Text");
            this.ditherLabel.Text = PdnResources.GetString("GifSaveConfigWidget.DitherLabel.Text");
            this.preMultiplyAlphaCheckBox.Text = PdnResources.GetString("GifSaveConfigWidget.PreMultiplyAlphaCheckBox.Text");
            this.thresholdInfoLabel.Text = PdnResources.GetString("GifSaveConfigWidget.ThresholdInfoLabel.Text");
        }

        protected override void InitFileType()
        {
            this.fileType = new GifFileType();
        }

        protected override void InitTokenFromWidget()
        {
            ((GifSaveConfigToken)this.Token).Threshold = this.thresholdSlider.Value;
            ((GifSaveConfigToken)this.Token).DitherLevel = this.ditherSlider.Value;
            ((GifSaveConfigToken)this.Token).PreMultiplyAlpha = this.preMultiplyAlphaCheckBox.Checked;
        }

        protected override void InitWidgetFromToken(SaveConfigToken token)
        {
            this.thresholdSlider.Value = ((GifSaveConfigToken)token).Threshold;
            this.ditherSlider.Value = ((GifSaveConfigToken)token).DitherLevel;
            this.preMultiplyAlphaCheckBox.Checked = ((GifSaveConfigToken)token).PreMultiplyAlpha;
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
            this.thresholdSlider = new System.Windows.Forms.TrackBar();
            this.thresholdLabel = new System.Windows.Forms.Label();
            this.thresholdUpDown = new System.Windows.Forms.NumericUpDown();
            this.preMultiplyAlphaCheckBox = new System.Windows.Forms.CheckBox();
            this.ditherUpDown = new System.Windows.Forms.NumericUpDown();
            this.ditherLabel = new System.Windows.Forms.Label();
            this.ditherSlider = new System.Windows.Forms.TrackBar();
            this.thresholdInfoLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ditherUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ditherSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // thresholdSlider
            // 
            this.thresholdSlider.Location = new System.Drawing.Point(0, 38);
            this.thresholdSlider.Maximum = 255;
            this.thresholdSlider.Name = "thresholdSlider";
            this.thresholdSlider.Size = new System.Drawing.Size(180, 42);
            this.thresholdSlider.TabIndex = 2;
            this.thresholdSlider.TickFrequency = 32;
            this.thresholdSlider.Value = 1;
            this.thresholdSlider.ValueChanged += new System.EventHandler(this.ThresholdSlider_ValueChanged);
            // 
            // thresholdLabel
            // 
            this.thresholdLabel.Location = new System.Drawing.Point(6, 3);
            this.thresholdLabel.Name = "thresholdLabel";
            this.thresholdLabel.Size = new System.Drawing.Size(106, 33);
            this.thresholdLabel.TabIndex = 0;
            // 
            // thresholdUpDown
            // 
            this.thresholdUpDown.Location = new System.Drawing.Point(115, 14);
            this.thresholdUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.thresholdUpDown.Name = "thresholdUpDown";
            this.thresholdUpDown.Size = new System.Drawing.Size(56, 20);
            this.thresholdUpDown.TabIndex = 1;
            this.thresholdUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.thresholdUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.thresholdUpDown.Enter += new System.EventHandler(this.ThresholdUpDown_Enter);
            this.thresholdUpDown.ValueChanged += new System.EventHandler(this.ThresholdUpDown_ValueChanged);
            this.thresholdUpDown.Leave += new System.EventHandler(this.ThresholdUpDown_Leave);
            // 
            // preMultiplyAlphaCheckBox
            // 
            this.preMultiplyAlphaCheckBox.Location = new System.Drawing.Point(8, 203);
            this.preMultiplyAlphaCheckBox.Name = "preMultiplyAlphaCheckBox";
            this.preMultiplyAlphaCheckBox.Size = new System.Drawing.Size(168, 24);
            this.preMultiplyAlphaCheckBox.TabIndex = 6;
            this.preMultiplyAlphaCheckBox.CheckedChanged += new System.EventHandler(this.PreMultiplyAlphaCheckBox_CheckedChanged);
            // 
            // ditherUpDown
            // 
            this.ditherUpDown.Location = new System.Drawing.Point(115, 128);
            this.ditherUpDown.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.ditherUpDown.Name = "ditherUpDown";
            this.ditherUpDown.Size = new System.Drawing.Size(56, 20);
            this.ditherUpDown.TabIndex = 4;
            this.ditherUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ditherUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ditherUpDown.Enter += new System.EventHandler(this.DitherUpDown_Enter);
            this.ditherUpDown.ValueChanged += new System.EventHandler(this.DitherUpDown_ValueChanged);
            this.ditherUpDown.Leave += new System.EventHandler(this.DitherUpDown_Leave);
            // 
            // ditherLabel
            // 
            this.ditherLabel.Location = new System.Drawing.Point(6, 130);
            this.ditherLabel.Name = "ditherLabel";
            this.ditherLabel.Size = new System.Drawing.Size(106, 20);
            this.ditherLabel.TabIndex = 3;
            // 
            // ditherSlider
            // 
            this.ditherSlider.LargeChange = 2;
            this.ditherSlider.Location = new System.Drawing.Point(0, 152);
            this.ditherSlider.Maximum = 8;
            this.ditherSlider.Name = "ditherSlider";
            this.ditherSlider.Size = new System.Drawing.Size(180, 42);
            this.ditherSlider.TabIndex = 5;
            this.ditherSlider.Value = 1;
            this.ditherSlider.ValueChanged += new System.EventHandler(this.DitherSlider_ValueChanged);
            // 
            // thresholdInfoLabel
            // 
            this.thresholdInfoLabel.Location = new System.Drawing.Point(6, 79);
            this.thresholdInfoLabel.Name = "thresholdInfoLabel";
            this.thresholdInfoLabel.Size = new System.Drawing.Size(168, 42);
            this.thresholdInfoLabel.TabIndex = 7;
            this.thresholdInfoLabel.Text = "label1";
            // 
            // GifSaveConfigWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.thresholdInfoLabel);
            this.Controls.Add(this.preMultiplyAlphaCheckBox);
            this.Controls.Add(this.thresholdUpDown);
            this.Controls.Add(this.thresholdLabel);
            this.Controls.Add(this.thresholdSlider);
            this.Controls.Add(this.ditherUpDown);
            this.Controls.Add(this.ditherLabel);
            this.Controls.Add(this.ditherSlider);
            this.Name = "GifSaveConfigWidget";
            this.Size = new System.Drawing.Size(180, 236);
            ((System.ComponentModel.ISupportInitialize)(this.thresholdSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ditherUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ditherSlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void ThresholdSlider_ValueChanged(object sender, System.EventArgs e)
        {
            if (this.thresholdUpDown.Value != (decimal)this.thresholdSlider.Value)
            {
                this.thresholdUpDown.Value = (decimal)this.thresholdSlider.Value;
            }

            UpdateToken();
        }

        private void ThresholdUpDown_ValueChanged(object sender, System.EventArgs e)
        {
            if (this.thresholdSlider.Value != (int)this.thresholdUpDown.Value)
            {
                this.thresholdSlider.Value = (int)this.thresholdUpDown.Value;
            }
        }

        private void ThresholdUpDown_Leave(object sender, System.EventArgs e)
        {
            ThresholdUpDown_ValueChanged(sender, e);
        }

        private void ThresholdUpDown_Enter(object sender, System.EventArgs e)
        {
            thresholdUpDown.Select(0, thresholdUpDown.Text.Length);
        }

        private void PreMultiplyAlphaCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            UpdateToken();
        }

        private void DitherSlider_ValueChanged(object sender, EventArgs e)
        {
            if (this.ditherUpDown.Value != (decimal)this.ditherSlider.Value)
            {
                this.ditherUpDown.Value = (decimal)this.ditherSlider.Value;
            }

            UpdateToken();
        }

        private void DitherUpDown_Enter(object sender, EventArgs e)
        {
            ditherUpDown.Select(0, thresholdUpDown.Text.Length);
        }

        private void DitherUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.ditherSlider.Value != (int)this.ditherUpDown.Value)
            {
                this.ditherSlider.Value = (int)this.ditherUpDown.Value;
            }
        }

        private void DitherUpDown_Leave(object sender, EventArgs e)
        {
            DitherUpDown_ValueChanged(sender, e);
        }
    }
}

