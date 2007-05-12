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
using System.Globalization;
using System.Windows.Forms;

namespace PaintDotNet.Setup
{
    public class IntroPage 
        : WizardPage
    {
        public static bool UserChoseQuickSetup = true;

        private System.Windows.Forms.Label introText;
        private System.Windows.Forms.Label copyrightLabel;
        private System.Windows.Forms.RadioButton quickRB;
        private System.Windows.Forms.RadioButton customRB;
        private System.Windows.Forms.Label quickDescription;
        private System.Windows.Forms.Label customDescription;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public IntroPage()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            string expireTextFormat = PdnResources.GetString("SetupWizard.IntroPage.ExpirationWarning.Format");
            string expireText;

            if (PdnInfo.IsFinalBuild)
            {
                expireText = string.Empty;
            }
            else
            {
                expireText = string.Format(expireTextFormat, PdnInfo.ExpirationDate.ToLongDateString());
            }

            string introFormat = PdnResources.GetString("SetupWizard.IntroPage.IntroText.Text.Format");
            string appNameWithTag = PdnInfo.GetBareProductName();
            string intro = string.Format(introFormat, appNameWithTag);
            this.introText.Text = intro;

            this.quickRB.Text = PdnResources.GetString("SetupWizard.IntroPage.QuickRB.Text");
            this.quickDescription.Text = PdnResources.GetString("SetupWizard.IntroPage.QuickDescription.Text");
            this.customRB.Text = PdnResources.GetString("SetupWizard.IntroPage.CustomRB.Text");
            this.customDescription.Text = PdnResources.GetString("SetupWizard.IntroPage.CustomDescription.Text");

            if (IntroPage.UserChoseQuickSetup)
            {
                this.quickRB.Checked = true;
            }
            else
            {
                this.customRB.Checked = true;
            }

            string format = InvariantStrings.CopyrightFormat;
            string allRightsReserved = PdnResources.GetString("Application.Copyright.AllRightsReserved");
            string copyright = string.Format(CultureInfo.CurrentCulture, format, allRightsReserved);
            this.copyrightLabel.Text = copyright;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (WizardHost != null)
            {
                WizardHost.HeaderText = PdnResources.GetString("SetupWizard.IntroPage.HeaderText");
                this.introText.Font = WizardHost.NormalTextFont;
                this.introText.ForeColor = WizardHost.TextColor;
                Font normalFont = WizardHost.NormalTextFont;
                Font rbFont = Utility.CreateFont(normalFont.Name, normalFont.Size, normalFont.Style | FontStyle.Bold);
                this.quickRB.Font = rbFont;
                this.quickRB.ForeColor = WizardHost.TextColor;
                this.quickDescription.Font = WizardHost.NormalTextFont;
                this.quickDescription.ForeColor = WizardHost.TextColor;
                this.customRB.Font = rbFont;
                this.customRB.ForeColor = WizardHost.TextColor;
                this.customDescription.Font = WizardHost.NormalTextFont;
                this.customDescription.ForeColor = WizardHost.TextColor;
                this.copyrightLabel.Font = WizardHost.FootNoteFont;
                this.copyrightLabel.ForeColor = WizardHost.TextColor;
                WizardHost.SetBackEnabled(false);
            }

            base.OnLoad (e);
        }

        public override void OnNextClicked()
        {
            IntroPage.UserChoseQuickSetup = this.quickRB.Checked; // license page uses this to determine where to go for 'Next' button press

            WizardHost.GoToPage(typeof(LicensePage));
            base.OnNextClicked();
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
            this.introText = new System.Windows.Forms.Label();
            this.copyrightLabel = new System.Windows.Forms.Label();
            this.quickRB = new System.Windows.Forms.RadioButton();
            this.customRB = new System.Windows.Forms.RadioButton();
            this.quickDescription = new System.Windows.Forms.Label();
            this.customDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // introText
            // 
            this.introText.Location = new System.Drawing.Point(12, 6);
            this.introText.Name = "introText";
            this.introText.Size = new System.Drawing.Size(464, 50);
            this.introText.TabIndex = 0;
            this.introText.Text = "introText";
            // 
            // copyrightLabel
            // 
            this.copyrightLabel.Location = new System.Drawing.Point(12, 224);
            this.copyrightLabel.Name = "copyrightLabel";
            this.copyrightLabel.Size = new System.Drawing.Size(472, 48);
            this.copyrightLabel.TabIndex = 5;
            this.copyrightLabel.Text = "label1";
            // 
            // quickRB
            // 
            this.quickRB.Location = new System.Drawing.Point(32, 56);
            this.quickRB.Name = "quickRB";
            this.quickRB.Size = new System.Drawing.Size(448, 24);
            this.quickRB.TabIndex = 1;
            // 
            // customRB
            // 
            this.customRB.Location = new System.Drawing.Point(32, 125);
            this.customRB.Name = "customRB";
            this.customRB.Size = new System.Drawing.Size(448, 24);
            this.customRB.TabIndex = 3;
            // 
            // quickDescription
            // 
            this.quickDescription.Location = new System.Drawing.Point(48, 80);
            this.quickDescription.Name = "quickDescription";
            this.quickDescription.Size = new System.Drawing.Size(432, 48);
            this.quickDescription.TabIndex = 2;
            // 
            // customDescription
            // 
            this.customDescription.Location = new System.Drawing.Point(48, 149);
            this.customDescription.Name = "customDescription";
            this.customDescription.Size = new System.Drawing.Size(432, 46);
            this.customDescription.TabIndex = 4;
            // 
            // IntroPage
            // 
            this.Controls.Add(this.customRB);
            this.Controls.Add(this.quickRB);
            this.Controls.Add(this.customDescription);
            this.Controls.Add(this.quickDescription);
            this.Controls.Add(this.copyrightLabel);
            this.Controls.Add(this.introText);
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.Name = "IntroPage";
            this.ResumeLayout(false);

        }
        #endregion
    }
}
