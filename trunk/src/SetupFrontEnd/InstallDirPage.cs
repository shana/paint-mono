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
using System.IO;
using System.Windows.Forms;

namespace PaintDotNet.Setup
{
    public class InstallDirPage 
        : WizardPage
    {
        private System.Windows.Forms.Label introText;
        private System.Windows.Forms.Label folderLabel;
        private System.Windows.Forms.TextBox targetDirTextBox;
        private System.Windows.Forms.Button browseButton;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public InstallDirPage()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            string appName = PdnInfo.GetBareProductName();
            string introFormat = PdnResources.GetString("SetupWizard.InstallDirPage.IntroText.Text.Format");
            this.introText.Text = string.Format(introFormat, appName);
            this.folderLabel.Text = PdnResources.GetString("SetupWizard.InstallDirPage.FolderLabel.Text");
            this.browseButton.Text = PdnResources.GetString("SetupWizard.InstallDirPage.BrowseButton.Text");
        }

        protected override void OnLoad(EventArgs e)
        {
            if (WizardHost != null)
            {
                WizardHost.HeaderText = PdnResources.GetString("SetupWizard.InstallDirPage.HeaderText");

                string targetDir = WizardHost.GetMsiProperty(PropertyNames.TargetDir, null);
                this.targetDirTextBox.Text = targetDir;
                this.targetDirTextBox.ForeColor = WizardHost.TextColor;

                this.introText.Font = WizardHost.NormalTextFont;
                this.introText.ForeColor = WizardHost.TextColor;

                this.folderLabel.Font = WizardHost.NormalTextFont;
                this.folderLabel.ForeColor = WizardHost.TextColor;

                this.browseButton.Font = WizardHost.NormalTextFont;
            }

            base.OnLoad(e);
        }

        public override void OnNextClicked()
        {
            bool ok = true;

            DirectoryInfo info = new DirectoryInfo(this.targetDirTextBox.Text);

            if (!info.Exists)
            {
                try
                {
                    info.Create();
                    info.Delete();
                }

                catch
                {
                    ok = false;
                    string title = PdnInfo.GetBareProductName();
                    string message = PdnResources.GetString("SetupWizard.InstallDirPage.BadDirError.Message");
                    MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (ok)
            {
                WizardHost.SetMsiProperty(PropertyNames.TargetDir, this.targetDirTextBox.Text);
                WizardHost.GoToPage(typeof(ReadyToInstallPage));
            }

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
                    this.components.Dispose();
                    this.components = null;
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
            this.folderLabel = new System.Windows.Forms.Label();
            this.targetDirTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // introText
            // 
            this.introText.Location = new System.Drawing.Point(12, 6);
            this.introText.Name = "introText";
            this.introText.Size = new System.Drawing.Size(468, 82);
            this.introText.TabIndex = 0;
            this.introText.Text = "label1";
            // 
            // folderLabel
            // 
            this.folderLabel.Location = new System.Drawing.Point(12, 84);
            this.folderLabel.Name = "folderLabel";
            this.folderLabel.Size = new System.Drawing.Size(256, 16);
            this.folderLabel.TabIndex = 1;
            this.folderLabel.Text = "label1";
            // 
            // targetDirTextBox
            // 
            this.targetDirTextBox.Location = new System.Drawing.Point(15, 102);
            this.targetDirTextBox.Name = "targetDirTextBox";
            this.targetDirTextBox.Size = new System.Drawing.Size(344, 20);
            this.targetDirTextBox.TabIndex = 2;
            this.targetDirTextBox.Text = "textBox1";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(372, 100);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(108, 23);
            this.browseButton.TabIndex = 3;
            this.browseButton.Text = "button1";
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            this.browseButton.FlatStyle = FlatStyle.System;
            // 
            // InstallDirPage
            // 
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.targetDirTextBox);
            this.Controls.Add(this.folderLabel);
            this.Controls.Add(this.introText);
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.Name = "InstallDirPage";
            this.ResumeLayout(false);

        }
        #endregion

        private void browseButton_Click(object sender, System.EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                string path = this.targetDirTextBox.Text;

                while (path != null)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(path);

                    if (!dirInfo.Exists)
                    {
                        path = Path.GetDirectoryName(path);
                    }
                    else
                    {
                        break;
                    }
                }

                if (path == null)
                {
                    path = @"C:\";
                }

                dialog.SelectedPath = path;
                dialog.Description = PdnResources.GetString("SetupWizard.InstallDirPage.BrowseForFolder.Description");
                dialog.ShowNewFolderButton = true;
                DialogResult result = dialog.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    this.targetDirTextBox.Text = dialog.SelectedPath;
                }
            }
        }
    }
}
