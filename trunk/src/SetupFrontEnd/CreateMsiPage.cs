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
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PaintDotNet.Setup
{
    public class CreateMsiPage
        : WizardPage
    {
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label statusText;

        private System.Windows.Forms.Label introText;

        private void PrepMsi(string msiPath, string dstDir, PaintDotNet.SystemLayer.ProcessorArchitecture platform, Hashtable msiProperties)
        {
            string dstMsiExt = "." + platform.ToString().ToLower() + ".msi";
            string dstMsiName = Path.ChangeExtension(msiPath, dstMsiExt);

            if (!Directory.Exists(dstDir))
            {
                Directory.CreateDirectory(dstDir);
            }

            string dstMsiPath = Path.Combine(dstDir, dstMsiName);

            this.statusText.Text = dstMsiName;
            WizardHost.Update();

            File.Copy(msiPath, dstMsiPath, true);
            ++this.progressBar.Value;

            Msi.SetMsiTargetPlatform(dstMsiPath, platform);
            ++this.progressBar.Value;

            foreach (string key in msiProperties.Keys)
            {
                string value = (string)msiProperties[key];
                Msi.SetMsiProperties(dstMsiPath, Msi.PropertyTable, new string[1] { key }, new string[1] { value });
                ++this.progressBar.Value;
            }

            ++this.progressBar.Value;
        }

        public CreateMsiPage()
        {
            InitializeComponent();
        }

        public override void OnNextClicked()
        {
            WizardHost.Close();
            base.OnNextClicked();
        }

        private void CreateMSIs()
        {
            Hashtable msiProperties = WizardHost.MsiProperties;
            this.progressBar.Maximum = 2 * (3 + msiProperties.Count);

            const string msiPath = "PaintDotNet.msi";
            string desktopDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string dstDir = Path.Combine(desktopDir, "PaintDotNetMsi");

            PrepMsi(msiPath, dstDir, PaintDotNet.SystemLayer.ProcessorArchitecture.X86, msiProperties);
            PrepMsi(msiPath, dstDir, PaintDotNet.SystemLayer.ProcessorArchitecture.X64, msiProperties);
            
            string finishedTextFormat = PdnResources.GetString("SetupWizard.CreateMsiPage.IntroText.Text.Finished");
            string finishedText = string.Format(finishedTextFormat, dstDir);
            this.introText.Text = finishedText;
            this.statusText.Text = string.Empty;

            WizardHost.SetFinished(true);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.progressBar.Minimum = 0;

            if (WizardHost != null)
            {
                WizardHost.HeaderText = PdnResources.GetString("SetupWizard.CreateMsiPage.HeaderText");
                this.introText.Text = PdnResources.GetString("SetupWizard.CreateMsiPage.IntroText.Text.Creating");
                this.introText.ForeColor = WizardHost.TextColor;
                this.introText.Font = WizardHost.NormalTextFont;
                this.introText.ForeColor = WizardHost.TextColor;
                this.statusText.Font = WizardHost.NormalTextFont;
                this.statusText.ForeColor = WizardHost.TextColor;

                WizardHost.SetNextEnabled(false);
                WizardHost.SetBackEnabled(false);
                WizardHost.SetCancelEnabled(false);

                this.BeginInvoke(new Procedure(CreateMSIs));
            }

            base.OnLoad(e);
        }

        private void InitializeComponent()
        {
            this.introText = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.statusText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // introText
            // 
            this.introText.Location = new System.Drawing.Point(12, 6);
            this.introText.Name = "introText";
            this.introText.Size = new System.Drawing.Size(468, 50);
            this.introText.TabIndex = 1;
            this.introText.Text = "introText";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(42, 59);
            this.progressBar.MarqueeAnimationSpeed = 50;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(408, 19);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 1;
            this.progressBar.Visible = true;
            // 
            // statusText
            // 
            this.statusText.AutoSize = true;
            this.statusText.Location = new System.Drawing.Point(55, 85);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(35, 13);
            this.statusText.TabIndex = 2;
            this.statusText.Text = "label1";
            // 
            // CreateMsiPage
            // 
            this.Controls.Add(this.statusText);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.introText);
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.Name = "CreateMsiPage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
