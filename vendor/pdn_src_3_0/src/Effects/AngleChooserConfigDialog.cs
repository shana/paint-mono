/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet;
using PaintDotNet.Effects;
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PaintDotNet.Effects
{
    public sealed class AngleChooserConfigDialog
        : AngleChooserConfigDialogBase
    {
        public AngleChooserConfigDialog()
        {
        }
    }

    public abstract class AngleChooserConfigDialogBase
        : EffectConfigDialog
    {
        private AngleChooserControl angleChooserControl;
        private System.Windows.Forms.NumericUpDown angleUpDown;
        private System.Windows.Forms.Label degreeLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private PaintDotNet.HeaderLabel angleHeader;

        private System.ComponentModel.Container components = null;

        protected internal AngleChooserConfigDialogBase()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
            this.cancelButton.Text = PdnResources.GetString("Form.CancelButton.Text");
            this.okButton.Text = PdnResources.GetString("Form.OkButton.Text");
            this.angleHeader.Text = PdnResources.GetString("AngleChooserConfigDialog.AngleHeader.Text");
            this.degreeLabel.Text = PdnResources.GetString("AngleChooserConfigDialog.DegreeLabel.Text");
        }

        protected override void OnLoad(EventArgs e)
        {
            this.angleUpDown.Select();
            this.angleUpDown.Select(0, this.angleUpDown.Text.Length);
            base.OnLoad (e);
        }

        // create default config token with angle 45 degress
        protected override void InitialInitToken()
        {
            theEffectToken = new AngleChooserConfigToken(45);
        }

        protected override void InitTokenFromDialog()
        {
            ((AngleChooserConfigToken)EffectToken).Angle = angleChooserControl.ValueDouble;
        }

        protected override void InitDialogFromToken(EffectConfigToken effectToken)
        {
            AngleChooserConfigToken token = (AngleChooserConfigToken)effectToken;
            angleChooserControl.ValueDouble = token.Angle;
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

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.okButton = new System.Windows.Forms.Button();
            this.angleUpDown = new System.Windows.Forms.NumericUpDown();
            this.degreeLabel = new System.Windows.Forms.Label();
            this.angleChooserControl = new PaintDotNet.AngleChooserControl();
            this.cancelButton = new System.Windows.Forms.Button();
            this.angleHeader = new PaintDotNet.HeaderLabel();
            ((System.ComponentModel.ISupportInitialize)(this.angleUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(24, 94);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 24);
            this.okButton.TabIndex = 0;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // angleUpDown
            // 
            this.angleUpDown.DecimalPlaces = 2;
            this.angleUpDown.Location = new System.Drawing.Point(88, 37);
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
            // degreeLabel
            // 
            this.degreeLabel.Location = new System.Drawing.Point(161, 37);
            this.degreeLabel.Name = "degreeLabel";
            this.degreeLabel.Size = new System.Drawing.Size(16, 23);
            this.degreeLabel.TabIndex = 15;
            // 
            // angleChooserControl
            // 
            this.angleChooserControl.Location = new System.Drawing.Point(16, 29);
            this.angleChooserControl.Name = "angleChooserControl";
            this.angleChooserControl.Size = new System.Drawing.Size(56, 56);
            this.angleChooserControl.TabIndex = 0;
            this.angleChooserControl.TabStop = false;
            this.angleChooserControl.Value = 16;
            this.angleChooserControl.ValueDouble = 16;
            this.angleChooserControl.ValueChanged += new System.EventHandler(this.angleChooserControl_ValueChanged);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(105, 94);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 24);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // angleHeader
            // 
            this.angleHeader.Location = new System.Drawing.Point(8, 8);
            this.angleHeader.Name = "angleHeader";
            this.angleHeader.Size = new System.Drawing.Size(176, 14);
            this.angleHeader.TabIndex = 14;
            this.angleHeader.TabStop = false;
            this.angleHeader.Text = "Header";
            // 
            // AngleChooserConfigDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(186, 124);
            this.Controls.Add(this.angleHeader);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.degreeLabel);
            this.Controls.Add(this.angleChooserControl);
            this.Controls.Add(this.angleUpDown);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "AngleChooserConfigDialog";
            this.Controls.SetChildIndex(this.angleUpDown, 0);
            this.Controls.SetChildIndex(this.angleChooserControl, 0);
            this.Controls.SetChildIndex(this.degreeLabel, 0);
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.Controls.SetChildIndex(this.angleHeader, 0);
            ((System.ComponentModel.ISupportInitialize)(this.angleUpDown)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void angleChooserControl_ValueChanged(object sender, System.EventArgs e)
        {
            if (angleUpDown.Value != (decimal)angleChooserControl.Value)
            {
                angleUpDown.Value = (decimal)angleChooserControl.ValueDouble;
                FinishTokenUpdate();
                Update();
            }
        }

        private void angleUpDown_ValueChanged(object sender, System.EventArgs e)
        {
            if (angleChooserControl.ValueDouble != (double)angleUpDown.Value)
            {
                angleChooserControl.ValueDouble = (double)angleUpDown.Value;
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
    }
}
