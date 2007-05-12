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

namespace PaintDotNet.Setup
{
    public class ReadyToInstallPage 
        : WizardPage
    {
        private System.Windows.Forms.Label introText;
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public ReadyToInstallPage()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            string introFormat = PdnResources.GetString("SetupWizard.ReadyToInstallPage.IntroText.Text.Format");
            string appName = PdnInfo.GetBareProductName();
            this.introText.Text = string.Format(introFormat, appName);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (WizardHost != null)
            {
                WizardHost.HeaderText = PdnResources.GetString("SetupWizard.ReadyToInstallPage.HeaderText");
                this.introText.Font = WizardHost.NormalTextFont;
                this.introText.ForeColor = WizardHost.TextColor;
            }

            base.OnLoad(e);
        }

        public override void OnNextClicked()
        {
            WizardHost.GoToPage(typeof(InstallingPage));
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
            this.SuspendLayout();
            // 
            // introText
            // 
            this.introText.Location = new System.Drawing.Point(12, 6);
            this.introText.Name = "introText";
            this.introText.Size = new System.Drawing.Size(468, 54);
            this.introText.TabIndex = 0;
            this.introText.Text = "introText";
            // 
            // ReadyToInstallPage
            // 
            this.Controls.Add(this.introText);
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.Name = "ReadyToInstallPage";
            this.ResumeLayout(false);

        }
        #endregion
    }
}
