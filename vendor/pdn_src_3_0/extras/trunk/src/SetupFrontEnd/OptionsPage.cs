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
    public class OptionsPage 
        : WizardPage
    {
        private System.Windows.Forms.Label launchHandlingText;
        private System.Windows.Forms.CheckBox jpgPngBmpCheckBox;
        private System.Windows.Forms.CheckBox tgaCheckBox;
        private System.Windows.Forms.Label updatesText;
        private System.Windows.Forms.CheckBox checkForUpdatesCheckBox;
        private System.Windows.Forms.CheckBox checkForBetasCheckBox;
        private System.Windows.Forms.CheckBox desktopShortcutCheckBox;
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public OptionsPage()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            this.launchHandlingText.Text = PdnResources.GetString("SetupWizard.OptionsPage.LaunchHandlingText.Text");
            this.jpgPngBmpCheckBox.Text = PdnResources.GetString("SetupWizard.OptionsPage.JpgPngBmpCheckBox.Text");
            this.tgaCheckBox.Text = PdnResources.GetString("SetupWizard.OptionsPage.TgaCheckBox.Text");
            this.updatesText.Text = PdnResources.GetString("SetupWizard.OptionsPage.UpdatesText.Text");
            this.desktopShortcutCheckBox.Text = PdnResources.GetString("SetupWizard.OptionsPage.DesktopShortcutCheckBox.Text");
            this.checkForUpdatesCheckBox.Text = PdnResources.GetString("SetupWizard.OptionsPage.CheckForUpdatesCheckBox.Text");
            this.checkForBetasCheckBox.Text = PdnResources.GetString("SetupWizard.OptionsPage.CheckForBetasCheckBox.Text");
        }

        protected override void OnLoad(EventArgs e)
        {
            if (WizardHost != null)
            {
                WizardHost.HeaderText = PdnResources.GetString("SetupWizard.OptionsPage.HeaderText");
                this.launchHandlingText.Font = WizardHost.NormalTextFont;

                this.jpgPngBmpCheckBox.Font = WizardHost.NormalTextFont;
                this.jpgPngBmpCheckBox.ForeColor = WizardHost.TextColor;
                this.jpgPngBmpCheckBox.Checked = ("1" == WizardHost.GetMsiProperty(PropertyNames.JpgPngBmpEditor, "1"));

                this.tgaCheckBox.Font = WizardHost.NormalTextFont;
                this.tgaCheckBox.ForeColor = WizardHost.TextColor;
                this.tgaCheckBox.Checked = ("1" == WizardHost.GetMsiProperty(PropertyNames.TgaEditor, "1"));

                this.desktopShortcutCheckBox.Font = WizardHost.NormalTextFont;
                this.desktopShortcutCheckBox.ForeColor = WizardHost.TextColor;
                this.desktopShortcutCheckBox.Checked = ("1" == WizardHost.GetMsiProperty(PropertyNames.DesktopShortcut, "1"));

                this.updatesText.Font = WizardHost.NormalTextFont;
                this.updatesText.ForeColor = WizardHost.TextColor;

                this.checkForUpdatesCheckBox.Font = WizardHost.NormalTextFont;
                this.checkForUpdatesCheckBox.ForeColor = WizardHost.TextColor;
                this.checkForUpdatesCheckBox.Checked = ("1" == WizardHost.GetMsiProperty(PropertyNames.CheckForUpdates, "1"));

                this.checkForBetasCheckBox.Font = WizardHost.NormalTextFont;
                this.checkForBetasCheckBox.ForeColor = WizardHost.TextColor;
                this.checkForBetasCheckBox.Checked = ("1" == WizardHost.GetMsiProperty(PropertyNames.CheckForBetas, PropertyNames.CheckForBetasDefault));

                if (!PdnInfo.IsFinalBuild)
                {
                    this.checkForBetasCheckBox.Checked = true;
                }

                if (!checkForUpdatesCheckBox.Checked)
                {
                    this.checkForBetasCheckBox.Checked = false;
                    this.checkForBetasCheckBox.Enabled = false;
                }
            }
            base.OnLoad (e);
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
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
            this.launchHandlingText = new System.Windows.Forms.Label();
            this.jpgPngBmpCheckBox = new System.Windows.Forms.CheckBox();
            this.tgaCheckBox = new System.Windows.Forms.CheckBox();
            this.updatesText = new System.Windows.Forms.Label();
            this.checkForUpdatesCheckBox = new System.Windows.Forms.CheckBox();
            this.checkForBetasCheckBox = new System.Windows.Forms.CheckBox();
            this.desktopShortcutCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // launchHandlingText
            // 
            this.launchHandlingText.Location = new System.Drawing.Point(12, 6);
            this.launchHandlingText.Name = "launchHandlingText";
            this.launchHandlingText.Size = new System.Drawing.Size(476, 34);
            this.launchHandlingText.TabIndex = 9;
            // 
            // jpgPngBmpCheckBox
            // 
            this.jpgPngBmpCheckBox.Location = new System.Drawing.Point(28, 40);
            this.jpgPngBmpCheckBox.Name = "jpgPngBmpCheckBox";
            this.jpgPngBmpCheckBox.Size = new System.Drawing.Size(468, 24);
            this.jpgPngBmpCheckBox.TabIndex = 1;
            this.jpgPngBmpCheckBox.Text = "checkBox1";
            this.jpgPngBmpCheckBox.CheckedChanged += new System.EventHandler(this.jpgPngBmpCheckBox_CheckedChanged);
            // 
            // tgaCheckBox
            // 
            this.tgaCheckBox.Location = new System.Drawing.Point(28, 64);
            this.tgaCheckBox.Name = "tgaCheckBox";
            this.tgaCheckBox.Size = new System.Drawing.Size(468, 24);
            this.tgaCheckBox.TabIndex = 2;
            this.tgaCheckBox.Text = "checkBox1";
            this.tgaCheckBox.CheckedChanged += new System.EventHandler(this.tgaCheckBox_CheckedChanged);
            // 
            // updatesText
            // 
            this.updatesText.Location = new System.Drawing.Point(12, 128);
            this.updatesText.Name = "updatesText";
            this.updatesText.Size = new System.Drawing.Size(476, 59);
            this.updatesText.TabIndex = 3;
            this.updatesText.Text = "label1";
            // 
            // checkForUpdatesCheckBox
            // 
            this.checkForUpdatesCheckBox.Location = new System.Drawing.Point(28, 189);
            this.checkForUpdatesCheckBox.Name = "checkForUpdatesCheckBox";
            this.checkForUpdatesCheckBox.Size = new System.Drawing.Size(468, 24);
            this.checkForUpdatesCheckBox.TabIndex = 4;
            this.checkForUpdatesCheckBox.Text = "checkBox1";
            this.checkForUpdatesCheckBox.CheckedChanged += new System.EventHandler(this.checkForUpdatesCheckBox_CheckedChanged);
            // 
            // checkForBetasCheckBox
            // 
            this.checkForBetasCheckBox.Location = new System.Drawing.Point(46, 213);
            this.checkForBetasCheckBox.Name = "checkForBetasCheckBox";
            this.checkForBetasCheckBox.Size = new System.Drawing.Size(444, 24);
            this.checkForBetasCheckBox.TabIndex = 5;
            this.checkForBetasCheckBox.Text = "checkBox2";
            this.checkForBetasCheckBox.CheckedChanged += new System.EventHandler(this.checkForBetasCheckBox_CheckedChanged);
            // 
            // desktopShortcutCheckBox
            // 
            this.desktopShortcutCheckBox.Location = new System.Drawing.Point(28, 88);
            this.desktopShortcutCheckBox.Name = "desktopShortcutCheckBox";
            this.desktopShortcutCheckBox.Size = new System.Drawing.Size(468, 24);
            this.desktopShortcutCheckBox.TabIndex = 7;
            this.desktopShortcutCheckBox.Text = "checkBox1";
            this.desktopShortcutCheckBox.CheckedChanged += new System.EventHandler(this.desktopShortcutCheckBox_CheckedChanged);
            // 
            // OptionsPage
            // 
            this.Controls.Add(this.desktopShortcutCheckBox);
            this.Controls.Add(this.checkForBetasCheckBox);
            this.Controls.Add(this.checkForUpdatesCheckBox);
            this.Controls.Add(this.updatesText);
            this.Controls.Add(this.tgaCheckBox);
            this.Controls.Add(this.jpgPngBmpCheckBox);
            this.Controls.Add(this.launchHandlingText);
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.Name = "OptionsPage";
            this.ResumeLayout(false);

        }
        #endregion

        private void jpgPngBmpCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            WizardHost.SetMsiProperty(PropertyNames.JpgPngBmpEditor, jpgPngBmpCheckBox.Checked ? "1" : "0");
        }

        private void tgaCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            WizardHost.SetMsiProperty(PropertyNames.TgaEditor, tgaCheckBox.Checked ? "1" : "0");
        }

        private void checkForUpdatesCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            WizardHost.SetMsiProperty(PropertyNames.CheckForUpdates, checkForUpdatesCheckBox.Checked ? "1" : "0");
            checkForBetasCheckBox.Enabled = checkForUpdatesCheckBox.Checked;
        }

        private void checkForBetasCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            WizardHost.SetMsiProperty(PropertyNames.CheckForBetas, checkForBetasCheckBox.Checked ? "1" : "0");
        }

        private void desktopShortcutCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            WizardHost.SetMsiProperty(PropertyNames.DesktopShortcut, desktopShortcutCheckBox.Checked ? "1" : "0");
        }

        public override void OnNextClicked()
        {
            WizardHost.GoToPage(typeof(InstallDirPage));
            base.OnNextClicked();
        }
    }
}
