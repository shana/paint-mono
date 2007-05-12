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
    public sealed class MotionBlurEffectConfigDialog 
        : EffectConfigDialog
    {
        private AngleChooserControl angleChooserControl;
        private System.Windows.Forms.NumericUpDown angleUpDown;
        private System.Windows.Forms.TrackBar distanceTrackBar;
        private System.Windows.Forms.NumericUpDown distanceUpDown;
        private System.Windows.Forms.Label pixelsLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.CheckBox centeredCheckBox;
        private PaintDotNet.HeaderLabel distanceHeader;
        private PaintDotNet.HeaderLabel angleHeader;
        private System.Windows.Forms.Label degreeLabel;
        private System.ComponentModel.IContainer components = null;

        public MotionBlurEffectConfigDialog()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();
        }

        protected override void InitialInitToken()
        {
            theEffectToken = new MotionBlurEffectConfigToken(25, 10, true);
        }

        protected override void InitTokenFromDialog()
        {
            ((MotionBlurEffectConfigToken)EffectToken).Angle = angleChooserControl.ValueDouble;
            ((MotionBlurEffectConfigToken)EffectToken).Distance = distanceTrackBar.Value;
            ((MotionBlurEffectConfigToken)EffectToken).Centered = centeredCheckBox.Checked;
        }

        protected override void InitDialogFromToken(EffectConfigToken effectToken)
        {
            MotionBlurEffectConfigToken token = (MotionBlurEffectConfigToken)effectToken;
            angleChooserControl.ValueDouble = token.Angle;
            distanceTrackBar.Value = token.Distance;
            centeredCheckBox.Checked = token.Centered;

            this.Text = PdnResources.GetString("MotionBlurEffectConfigDialog.Text");
            this.degreeLabel.Text = PdnResources.GetString("MotionBlurEffectConfigDialog.DegreeLabel.Text");
            this.pixelsLabel.Text = PdnResources.GetString("MotionBlurEffectConfigDialog.PixelsLabel.Text");
            this.cancelButton.Text = PdnResources.GetString("Form.CancelButton.Text");
            this.okButton.Text = PdnResources.GetString("Form.OkButton.Text");
            this.centeredCheckBox.Text = PdnResources.GetString("MotionBlurEffectConfigDialog.CenteredCheckBox.Text");
            this.angleHeader.Text = PdnResources.GetString("MotionBlurEffectConfigDialog.AngleHeader.Text");
            this.distanceHeader.Text = PdnResources.GetString("MotionBlurEffectConfigDialog.DistanceHeader.Text");
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
            this.angleChooserControl = new PaintDotNet.AngleChooserControl();
            this.angleUpDown = new System.Windows.Forms.NumericUpDown();
            this.distanceTrackBar = new System.Windows.Forms.TrackBar();
            this.distanceUpDown = new System.Windows.Forms.NumericUpDown();
            this.pixelsLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.centeredCheckBox = new System.Windows.Forms.CheckBox();
            this.distanceHeader = new PaintDotNet.HeaderLabel();
            this.angleHeader = new PaintDotNet.HeaderLabel();
            this.degreeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.angleUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.distanceTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.distanceUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // angleChooserControl
            // 
            this.angleChooserControl.Location = new System.Drawing.Point(16, 26);
            this.angleChooserControl.Name = "angleChooserControl";
            this.angleChooserControl.Size = new System.Drawing.Size(56, 56);
            this.angleChooserControl.TabIndex = 0;
            this.angleChooserControl.TabStop = false;
            this.angleChooserControl.Value = 16;
            this.angleChooserControl.ValueDouble = 16;
            this.angleChooserControl.ValueChanged += new System.EventHandler(this.angleChooserControl_ValueChanged);
            // 
            // angleUpDown
            // 
            this.angleUpDown.DecimalPlaces = 2;
            this.angleUpDown.Location = new System.Drawing.Point(80, 32);
            this.angleUpDown.Maximum = new System.Decimal(new int[] {
                                                                        180,
                                                                        0,
                                                                        0,
                                                                        0});
            this.angleUpDown.Minimum = new System.Decimal(new int[] {
                                                                        180,
                                                                        0,
                                                                        0,
                                                                        -2147483648});
            this.angleUpDown.Name = "angleUpDown";
            this.angleUpDown.Size = new System.Drawing.Size(72, 20);
            this.angleUpDown.TabIndex = 0;
            this.angleUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.angleUpDown.Enter += new System.EventHandler(this.angleUpDown_Enter);
            this.angleUpDown.ValueChanged += new System.EventHandler(this.angleUpDown_ValueChanged);
            this.angleUpDown.Leave += new System.EventHandler(this.angleUpDown_Leave);
            // 
            // distanceTrackBar
            // 
            this.distanceTrackBar.AutoSize = false;
            this.distanceTrackBar.Location = new System.Drawing.Point(174, 59);
            this.distanceTrackBar.Maximum = 200;
            this.distanceTrackBar.Minimum = 1;
            this.distanceTrackBar.Name = "distanceTrackBar";
            this.distanceTrackBar.Size = new System.Drawing.Size(161, 24);
            this.distanceTrackBar.TabIndex = 2;
            this.distanceTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.distanceTrackBar.Value = 1;
            this.distanceTrackBar.ValueChanged += new System.EventHandler(this.distanceTrackBar_ValueChanged);
            // 
            // distanceUpDown
            // 
            this.distanceUpDown.Location = new System.Drawing.Point(184, 32);
            this.distanceUpDown.Maximum = new System.Decimal(new int[] {
                                                                           200,
                                                                           0,
                                                                           0,
                                                                           0});
            this.distanceUpDown.Minimum = new System.Decimal(new int[] {
                                                                           1,
                                                                           0,
                                                                           0,
                                                                           0});
            this.distanceUpDown.Name = "distanceUpDown";
            this.distanceUpDown.Size = new System.Drawing.Size(72, 20);
            this.distanceUpDown.TabIndex = 1;
            this.distanceUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.distanceUpDown.Value = new System.Decimal(new int[] {
                                                                         1,
                                                                         0,
                                                                         0,
                                                                         0});
            this.distanceUpDown.Enter += new System.EventHandler(this.angleUpDown_Enter);
            this.distanceUpDown.ValueChanged += new System.EventHandler(this.distanceUpDown_ValueChanged);
            this.distanceUpDown.Leave += new System.EventHandler(this.distanceUpDown_Leave);
            // 
            // pixelsLabel
            // 
            this.pixelsLabel.Location = new System.Drawing.Point(258, 33);
            this.pixelsLabel.Name = "pixelsLabel";
            this.pixelsLabel.Size = new System.Drawing.Size(62, 16);
            this.pixelsLabel.TabIndex = 7;
            this.pixelsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancelButton.Location = new System.Drawing.Point(254, 97);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.okButton.Location = new System.Drawing.Point(173, 97);
            this.okButton.Name = "okButton";
            this.okButton.TabIndex = 4;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // centeredCheckBox
            // 
            this.centeredCheckBox.Checked = true;
            this.centeredCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.centeredCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.centeredCheckBox.Location = new System.Drawing.Point(10, 86);
            this.centeredCheckBox.Name = "centeredCheckBox";
            this.centeredCheckBox.Size = new System.Drawing.Size(150, 24);
            this.centeredCheckBox.TabIndex = 3;
            this.centeredCheckBox.CheckedChanged += new System.EventHandler(this.centeredCheckBox_CheckedChanged);
            // 
            // distanceHeader
            // 
            this.distanceHeader.Location = new System.Drawing.Point(176, 8);
            this.distanceHeader.Name = "distanceHeader";
            this.distanceHeader.Size = new System.Drawing.Size(159, 14);
            this.distanceHeader.TabIndex = 13;
            this.distanceHeader.TabStop = false;
            this.distanceHeader.Text = "headerLabel1  ";
            // 
            // angleHeader
            // 
            this.angleHeader.Location = new System.Drawing.Point(6, 8);
            this.angleHeader.Name = "angleHeader";
            this.angleHeader.RightMargin = 0;
            this.angleHeader.Size = new System.Drawing.Size(161, 14);
            this.angleHeader.TabIndex = 14;
            this.angleHeader.TabStop = false;
            this.angleHeader.Text = "headerLabel1  ";
            // 
            // degreeLabel
            // 
            this.degreeLabel.Location = new System.Drawing.Point(152, 32);
            this.degreeLabel.Name = "degreeLabel";
            this.degreeLabel.Size = new System.Drawing.Size(16, 23);
            this.degreeLabel.TabIndex = 15;
            // 
            // MotionBlurEffectConfigDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(335, 126);
            this.Controls.Add(this.degreeLabel);
            this.Controls.Add(this.angleHeader);
            this.Controls.Add(this.distanceHeader);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.centeredCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.distanceTrackBar);
            this.Controls.Add(this.distanceUpDown);
            this.Controls.Add(this.pixelsLabel);
            this.Controls.Add(this.angleChooserControl);
            this.Controls.Add(this.angleUpDown);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "MotionBlurEffectConfigDialog";
            this.Enter += new System.EventHandler(this.angleUpDown_Enter);
            this.Controls.SetChildIndex(this.angleUpDown, 0);
            this.Controls.SetChildIndex(this.angleChooserControl, 0);
            this.Controls.SetChildIndex(this.pixelsLabel, 0);
            this.Controls.SetChildIndex(this.distanceUpDown, 0);
            this.Controls.SetChildIndex(this.distanceTrackBar, 0);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.Controls.SetChildIndex(this.centeredCheckBox, 0);
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.distanceHeader, 0);
            this.Controls.SetChildIndex(this.angleHeader, 0);
            this.Controls.SetChildIndex(this.degreeLabel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.angleUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.distanceTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.distanceUpDown)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void angleChooserControl_ValueChanged(object sender, System.EventArgs e)
        {
            if (angleUpDown.Value != (decimal)angleChooserControl.ValueDouble)
            {
                angleUpDown.Value = (decimal)angleChooserControl.ValueDouble;
                Update();
                FinishTokenUpdate();
            }
        }

        private void angleUpDown_ValueChanged(object sender, System.EventArgs e)
        {
            if (angleChooserControl.ValueDouble != (double)angleUpDown.Value)
            {
                angleChooserControl.ValueDouble = (double)angleUpDown.Value;
                Update();
                FinishTokenUpdate();
            }
        }

        private void distanceUpDown_ValueChanged(object sender, System.EventArgs e)
        {
            if (distanceTrackBar.Value != (int)distanceUpDown.Value)
            {
                distanceTrackBar.Value = (int)distanceUpDown.Value;
                Update();
                FinishTokenUpdate();
            }
        }

        private void distanceTrackBar_ValueChanged(object sender, System.EventArgs e)
        {
            if (distanceUpDown.Value != (decimal)distanceTrackBar.Value)
            {
                distanceUpDown.Value = (decimal)distanceTrackBar.Value;
                Update();
                FinishTokenUpdate();
            }
        }

        private void okButton_Click(object sender, System.EventArgs e)
        {
            // if the user types, then presses Enter or clicks OK, this will make sure we take what they typed and not the value of the trackbar            
            angleUpDown_Leave(sender, e);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void centeredCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            FinishTokenUpdate();
        }

        private void angleUpDown_Leave(object sender, System.EventArgs e)
        {
            Utility.ClipNumericUpDown(angleUpDown);

            if (Utility.CheckNumericUpDown(angleUpDown))
            {
                angleUpDown.Value = decimal.Parse(angleUpDown.Text);
            }
        }

        private void angleUpDown_Enter(object sender, System.EventArgs e)
        {
            NumericUpDown nud = (NumericUpDown)sender;
            nud.Select(0, nud.Text.Length);
        }
        
        private void distanceUpDown_Leave(object sender, System.EventArgs e)
        {
            Utility.ClipNumericUpDown(distanceUpDown);

            if (Utility.CheckNumericUpDown(distanceUpDown))
            {
                distanceUpDown.Value = decimal.Parse(distanceUpDown.Text);
            }
        }

    }
}

