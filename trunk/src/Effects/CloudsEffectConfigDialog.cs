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
    public sealed class CloudsEffectConfigDialog 
        : TwoAmountsConfigDialogBase
    {
        private HeaderLabel seedHeader;
        private HeaderLabel headerBlendMode;
        private Label usageLabel;
        private ComboBox comboBlendModes;
        private Button reseedButton;
        private byte seed = 0;
    
        public CloudsEffectConfigDialog()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
            
            this.Text = CloudsEffect.StaticName;

            this.Amount1Label = PdnResources.GetString("CloudsEffect.ConfigDialog.ScaleLabel");
            this.Amount1Minimum = 2;
            this.Amount1Maximum = 1000;
            this.Amount1Default = 250;

            this.Amount2Label = PdnResources.GetString("CloudsEffect.ConfigDialog.RoughnessLabel");
            this.Amount2Minimum = 0;
            this.Amount2Maximum = 100;
            this.Amount2Default = 50;
            
            this.Icon = Utility.ImageToIcon(CloudsEffect.StaticImage);

            this.seedHeader.Text = PdnResources.GetString("CloudsEffect.ConfigDialog.SeedHeader.Text");
            this.headerBlendMode.Text = PdnResources.GetString("CloudsEffect.ConfigDialog.BlendModeHeader.Text");
            this.reseedButton.Text = PdnResources.GetString("CloudsEffect.ConfigDialog.ReseedButton.Text");
            this.usageLabel.Text = PdnResources.GetString("CloudsEffect.ConfigDialog.UsageLabel");

            // populate the blendOpComboBox with all the blend modes they're allowed to use
            foreach (Type type in UserBlendOps.GetBlendOps())
            {
                this.comboBlendModes.Items.Add(UserBlendOps.CreateBlendOp(type));
            }
        }

        // create default config token with angle 45 degress
        protected override void InitialInitToken()
        {
            this.theEffectToken = new CloudsEffectConfigToken(250, 50, seed, new UserBlendOps.NormalBlendOp());
        }        

        protected override void InitDialogFromToken(EffectConfigToken effectToken)
        {
            base.InitDialogFromToken(effectToken);

            UserBlendOp setOp = ((CloudsEffectConfigToken)effectToken).BlendOp;

            if (setOp == null)
            {
                setOp = new UserBlendOps.NormalBlendOp();
            }

            foreach (object op in this.comboBlendModes.Items)
            {
                if (0 == string.Compare(op.ToString(), setOp.ToString(), StringComparison.CurrentCultureIgnoreCase))
                {
                    this.comboBlendModes.SelectedItem = op;
                    break;
                }
            }
        }

        protected override void InitTokenFromDialog()
        {
            base.InitTokenFromDialog();
            ((CloudsEffectConfigToken)theEffectToken).BlendOp = this.comboBlendModes.SelectedItem as UserBlendOp;
            ((CloudsEffectConfigToken)theEffectToken).Seed = this.seed;
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.seedHeader = new PaintDotNet.HeaderLabel();
            this.reseedButton = new System.Windows.Forms.Button();
            this.headerBlendMode = new PaintDotNet.HeaderLabel();
            this.usageLabel = new Label();
            this.comboBlendModes = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(101, 284);
            this.okButton.TabIndex = 8;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(188, 284);
            this.cancelButton.TabIndex = 9;
            // 
            // seedHeader
            // 
            this.seedHeader.Location = new System.Drawing.Point(6, 198);
            this.seedHeader.Name = "seedHeader";
            this.seedHeader.Size = new System.Drawing.Size(170, 14);
            this.seedHeader.TabIndex = 11;
            this.seedHeader.TabStop = false;
            this.seedHeader.Text = "Seed Header";
            // 
            // reseedButton
            // 
            this.reseedButton.Location = new System.Drawing.Point(188, 196);
            this.reseedButton.Name = "reseedButton";
            this.reseedButton.Size = new System.Drawing.Size(81, 20);
            this.reseedButton.TabIndex = 7;
            this.reseedButton.UseVisualStyleBackColor = true;
            this.reseedButton.Click += new EventHandler(ReseedButton_Click);
            // 
            // headerBlendMode
            // 
            this.headerBlendMode.Location = new System.Drawing.Point(6, 148);
            this.headerBlendMode.Name = "headerBlendMode";
            this.headerBlendMode.Size = new System.Drawing.Size(271, 14);
            this.headerBlendMode.TabIndex = 12;
            this.headerBlendMode.TabStop = false;
            this.headerBlendMode.Text = "Blend Header";
            // 
            // comboBlendModes
            // 
            this.comboBlendModes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBlendModes.FormattingEnabled = true;
            this.comboBlendModes.Location = new System.Drawing.Point(9, 165);
            this.comboBlendModes.Name = "comboBlendModes";
            this.comboBlendModes.Size = new System.Drawing.Size(260, 21);
            this.comboBlendModes.TabIndex = 6;
            this.comboBlendModes.SelectedIndexChanged += new EventHandler(ComboBlendModes_SelectedIndexChanged);
            //
            // usageLabel
            //
            this.usageLabel.Name = "usageLabel";
            this.usageLabel.AutoSize = false;
            this.usageLabel.Location = new System.Drawing.Point(5, 225);
            this.usageLabel.Size = new System.Drawing.Size(260, 55);
            // 
            // CloudsEffectConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(275, 313);
            this.Controls.Add(this.usageLabel);
            this.Controls.Add(this.comboBlendModes);
            this.Controls.Add(this.headerBlendMode);
            this.Controls.Add(this.seedHeader);
            this.Controls.Add(this.reseedButton);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "CloudsEffectConfigDialog";
            this.Controls.SetChildIndex(this.reseedButton, 0);
            this.Controls.SetChildIndex(this.seedHeader, 0);
            this.Controls.SetChildIndex(this.okButton, 0);
            this.Controls.SetChildIndex(this.cancelButton, 0);
            this.Controls.SetChildIndex(this.headerBlendMode, 0);
            this.Controls.SetChildIndex(this.comboBlendModes, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void ReseedButton_Click(object sender, EventArgs e)
        {
            this.seed = unchecked((byte)(seed + 1));
            FinishTokenUpdate();
        }

        private void ComboBlendModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            FinishTokenUpdate();
        }

    }
}
