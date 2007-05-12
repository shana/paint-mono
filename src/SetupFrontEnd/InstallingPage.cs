/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using Microsoft.Win32;
using PaintDotNet.SystemLayer;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Media;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PaintDotNet.Setup
{
    public class InstallingPage 
        : WizardPage
    {
        private const string msiName = "PaintDotNet.msi";
        private const string stagingDirName = "Staging";
        private string appName;
        private System.Windows.Forms.Label infoText;
        private ProgressBar progressBar;

        private string installingText;
        private string uninstallingText;
        private string optimizingText;
        private Label errorLabel;

        private Size bannerSize = new Size(465, 70);
        private Bitmap pdnDonateBannerImage;

        private PictureBox topBanner;
        private PictureBox bottomBanner;

        private ToolTip toolTip;

        private void RenderPdnDonateBannerImage(Graphics g, Rectangle rect)
        {
            g.Clear(Color.White);

            // Draw black outline
            g.DrawRectangle(Pens.Black, new Rectangle(rect.Left, rect.Top, rect.Width - 1, rect.Height - 1));

            // Draw the PayPal icon
            Image payPalDonate = PdnResources.GetImage("Images.PayPalDonate.gif");

            Rectangle payPalRect = new Rectangle(
                rect.Right - payPalDonate.Width - (rect.Height - payPalDonate.Width),
                rect.Top + (rect.Height - payPalDonate.Height) / 2,
                payPalDonate.Width,
                payPalDonate.Height);

            g.DrawImage(payPalDonate, payPalRect, new Rectangle(0, 0, payPalDonate.Width, payPalDonate.Height), GraphicsUnit.Pixel);

            // Draw the PDN icon
            Image pdnIcon = PdnResources.GetImage("Images.Icon50x50.png");

            Rectangle pdnIconRect = new Rectangle(
                rect.Left + (rect.Height - pdnIcon.Width) / 2,
                rect.Top + (rect.Height - pdnIcon.Height) / 2,
                pdnIcon.Width,
                pdnIcon.Height);

            g.DrawImage(pdnIcon, pdnIconRect, new Rectangle(0, 0, pdnIcon.Width, pdnIcon.Height), GraphicsUnit.Pixel);

            // Draw inset text
            using (StringFormat sf = (StringFormat)StringFormat.GenericTypographic.Clone())
            {
                using (Font donateFont = new Font(Font.FontFamily, 12, FontStyle.Underline, GraphicsUnit.Pixel))
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    string insetText = PdnResources.GetString("SetupWizard.InstallingPage.PdnDonateBannerImage.Text");

                    int insetMargin = 2;

                    Rectangle textRect = new Rectangle(
                        pdnIconRect.Right,
                        rect.Top + insetMargin,
                        rect.Width - insetMargin - (rect.Right - payPalRect.Left) - (pdnIcon.Width + insetMargin) - ((rect.Right - payPalRect.Left) - payPalDonate.Width),
                        rect.Height - insetMargin * 2);

                    g.DrawString(insetText, donateFont, Brushes.Blue, textRect, sf);
                }
            }

            return;
        }

        private void LoadMirrorInfo(out Image mirrorImage, out string mirrorClickUrl)
        {
            try
            {
                FileInfo mirrorCfgInfo = new FileInfo("MirrorBanner.cfg");

                if (!mirrorCfgInfo.Exists ||
                    (DateTime.Now - mirrorCfgInfo.CreationTime) > new TimeSpan(0, 0, 30, 0, 0))
                {
                    throw new Exception();
                }

                StreamReader mciReader = mirrorCfgInfo.OpenText();
                string clickUrl = mciReader.ReadLine();

                const string clickUrlText = "ClickUrl=";
                int clickUrlIndex = clickUrl.IndexOf(clickUrlText);

                if (clickUrlIndex != 0)
                {
                    throw new Exception();
                }

                string url = clickUrl.Substring(clickUrlText.Length);
                mirrorClickUrl = url;

                using (Stream mirrorBannerStream = new FileStream("MirrorBanner.png", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    mirrorImage = Image.FromStream(mirrorBannerStream);
                }
            }

            catch
            {
                mirrorImage = null;
                mirrorClickUrl = null;
            }
        }

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public InstallingPage()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            string introFormat = PdnResources.GetString("SetupWizard.InstallingPage.InfoText.Text.Installing.Format");
            this.appName = PdnInfo.GetBareProductName();
            this.installingText = string.Format(introFormat, appName);

            this.uninstallingText = PdnResources.GetString("SetupWizard.InstallingPage.InfoText.Text.Uninstalling");
            this.optimizingText = PdnResources.GetString("SetupWizard.InstallingPage.InfoText.Text.Optimizing");

            this.pdnDonateBannerImage = new Bitmap(bannerSize.Width, bannerSize.Height, PixelFormat.Format24bppRgb);

            using (Graphics g = Graphics.FromImage(this.pdnDonateBannerImage))
            {
                RenderPdnDonateBannerImage(g, new Rectangle(Point.Empty, this.pdnDonateBannerImage.Size));
            }

            this.topBanner.Image = this.pdnDonateBannerImage;
            this.topBanner.Tag = InvariantStrings.DonateUrlSetup;
            this.toolTip.SetToolTip(this.topBanner, InvariantStrings.DonateUrlSetup);

            Image mirrorImage;
            string mirrorClickUrl;
            LoadMirrorInfo(out mirrorImage, out mirrorClickUrl);

            if (mirrorImage != null && mirrorClickUrl != null)
            {
                this.bottomBanner.Image = mirrorImage;
                this.bottomBanner.Tag = mirrorClickUrl;
                this.toolTip.SetToolTip(this.bottomBanner, mirrorClickUrl);
            }
        }

        private string GetOriginalMsiName(string msiPath)
        {
            Random random = new Random();
            
            string dirName = Path.GetDirectoryName(msiPath);
            string fileName = Path.GetFileNameWithoutExtension(msiPath);
            string ext = Path.GetExtension(msiPath);

            while (true)
            {
                int salt = random.Next();
                string newFileName = Path.Combine(dirName, Path.ChangeExtension(fileName + "_" + salt, ext));

                FileInfo info = new FileInfo(newFileName);

                if (!info.Exists)
                {
                    return newFileName;
                }
            }
        }

        private void ShowBanners()
        {
            if (this.topBanner.Image != null && this.bottomBanner.Image == null)
            {
                this.topBanner.Location = this.bottomBanner.Location;
            }

            if (this.topBanner.Image != null)
            {
                this.topBanner.Enabled = true;
                this.topBanner.Visible = true;
            }

            if (this.bottomBanner.Image != null)
            {
                this.bottomBanner.Enabled = true;
                this.bottomBanner.Visible = true;
            }
        }

        private void DoInstallation()
        {
            WizardHost.SetNextEnabled(false);
            WizardHost.SetBackEnabled(false);
            WizardHost.SetCancelEnabled(false);

            IntPtr hWnd = this.Handle;

            uint result = NativeMethods.MsiSetInternalUI(
                NativeConstants.INSTALLUILEVEL_BASIC | NativeConstants.INSTALLUILEVEL_HIDECANCEL, 
                ref hWnd);

            // value of result is discarded

            string ourDir = PdnInfo.GetApplicationDir();
            string originalPackagePath = Path.Combine(ourDir, msiName);
            string targetDir = WizardHost.GetMsiProperty(PropertyNames.TargetDir, null);
            string stagingDir = Path.Combine(targetDir, stagingDirName);

            // The 'old' target dir is read from the registry.
            // This way if they are reinstalling to a new directory, we will propertly uninstall and cleanup
            // from the old directory.
            // The 'old' target dir defaults to the 'new' target dir (in case of new installation)
            string oldTargetDir = WizardHost.GetMsiProperty(PropertyNames.TargetDir, targetDir, true);
            string oldStagingDir = Path.Combine(oldTargetDir, stagingDirName);

            // Uninstallers should skip certain parts of cleanup when we're going to turn around
            // and install a newer version right away
            WizardHost.SetMsiProperty(PropertyNames.SkipCleanup, "0");

            // Uninstall anything already in the staging directory (should only be the previous version)
            if (Directory.Exists(oldStagingDir))
            {
                this.infoText.Text = this.uninstallingText;
                WizardHost.SetMsiProperty(PropertyNames.SkipCleanup, "1");

                foreach (string filePath in Directory.GetFiles(oldStagingDir, "*.msi"))
                {
                    uint result2 = NativeMethods.MsiInstallProduct(
                        filePath, 
                        "REMOVE=ALL " + 
                        PropertyNames.SkipCleanup + "=1 " + 
                        PropertyNames.DesktopShortcut + "=" + WizardHost.GetMsiProperty(PropertyNames.DesktopShortcut, "1"));
                }
            }

            // Proceed with installation
            this.infoText.Text = this.installingText;

            Directory.CreateDirectory(stagingDir);
            string msiPath = Path.Combine(stagingDir, msiName);
            string dstPackagePath = GetOriginalMsiName(msiPath);

            // Copy the MSI to the Staging directory before installing. This way it will always
            // be available when Windows Installer needs to refer to it.
            FileInfo info = new FileInfo(originalPackagePath);
            info.CopyTo(dstPackagePath, true);

            // Keep an open file handle so that setupngen.exe cannot delete the file.
            // This happens if the current installation of Paint.NET 

            // We need to set the Target Platform property of the MSI before we install it.
            // This way if the user types "C:\Program Files\Whatever" on an x64 system, it will
            // not get redirected over to "C:\Program Files (x86)\Whatever"
            Msi.SetMsiTargetPlatform(dstPackagePath, PaintDotNet.SystemLayer.Processor.NativeArchitecture);

            string commandLine1 = WizardHost.GetMsiCommandLine();
            string commandLine = commandLine1;
            
            if (commandLine.Length > 0)
            {
                commandLine += " ";
            }

            commandLine += PropertyNames.QueueNgen + "=1";

            // Install newest package
            result = NativeMethods.MsiInstallProduct(dstPackagePath, commandLine);

            if (result == NativeConstants.ERROR_SUCCESS ||
                result == NativeConstants.ERROR_SUCCESS_REBOOT_INITIATED ||
                result == NativeConstants.ERROR_SUCCESS_REBOOT_REQUIRED)
            {
                ShowBanners();
    
                WizardHost.SaveMsiProperties();

                // clean up staging dir
                string msiFileName = Path.GetFileName(dstPackagePath);
                foreach (string filePath in Directory.GetFiles(stagingDir, "*.msi"))
                {
                    string fileName = Path.GetFileName(filePath);

                    if (0 != string.Compare(msiFileName, fileName, true, CultureInfo.InvariantCulture))
                    {
                        File.Delete(filePath);
                    }
                }

                // Run "ngen.exe executeQueuedItems", aka "Optimizing performance for your system..."
                if (Application.VisualStyleState == VisualStyleState.ClientAreaEnabled ||
                    Application.VisualStyleState == VisualStyleState.ClientAndNonClientAreasEnabled)
                {
                    this.progressBar.Style = ProgressBarStyle.Marquee;
                    this.progressBar.Visible = true;
                }

                string ngenExe = PdnInfo.GetNgenPath();
                const string ngenArg = "executeQueuedItems";

                try
                {
                    this.infoText.Text = this.optimizingText;
                    ProcessStartInfo psi = new ProcessStartInfo(ngenExe, ngenArg);
                    psi.UseShellExecute = false;
                    psi.CreateNoWindow = true;
                    Process process = Process.Start(psi);

                    while (!process.HasExited)
                    {
                        System.Threading.Thread.Sleep(10);
                        Application.DoEvents();
                    }
                }

                catch
                {
                    // If this fails, do not fail the installation
                }

                // Try to write locale setting to registry
                try
                {
                    Settings.SystemWide.SetString("LanguageName", CultureInfo.CurrentUICulture.Name);
                }

                catch (Exception)
                {
                    // Ignore errors, however
                }

                SystemSounds.Beep.Play();

                WizardHost.SetFinished(true);
                this.progressBar.Visible = false;

                // set text to indicate success
                WizardHost.HeaderText = PdnResources.GetString("SetupWizard.InstallingPage.HeaderText.Success");
                string infoFormat;
                    
                if (result == NativeConstants.ERROR_SUCCESS)
                {
                    WizardHost.RebootRequired = false;
                    infoFormat = PdnResources.GetString("SetupWizard.InstallingPage.InfoText.Text.Success.Format");
                }
                else
                {
                    WizardHost.RebootRequired = true;
                    infoFormat = PdnResources.GetString("SetupWizard.InstallingPage.InfoText.Text.Success.RebootRequired.Format");
                }

                this.infoText.Text = string.Format(infoFormat, this.appName);
                WizardHost.SetBackEnabled(false);
            }
            else
            {
                WizardHost.SetFinished(true);
                this.progressBar.Visible = false;

                // set text to indicate failure
                WizardHost.HeaderText = PdnResources.GetString("SetupWizard.InstallingPage.HeaderText.Failure");
                string infoFormat = PdnResources.GetString("SetupWizard.InstallingPage.InfoText.Text.Failure.Format");
                string errorString = NativeMethods.FormatMessageW(result);
                this.errorLabel.Font = WizardHost.NormalTextFont;
                this.errorLabel.Visible = true;
                this.errorLabel.Text = errorString;
                this.infoText.Text = string.Format(infoFormat, this.appName) + " (" + result.ToString() + ")";
                WizardHost.SetBackEnabled(false);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (WizardHost != null)
            {
                WizardHost.HeaderText = PdnResources.GetString("SetupWizard.InstallingPage.HeaderText.Installing");
                this.infoText.Font = WizardHost.NormalTextFont;
                this.infoText.ForeColor = WizardHost.TextColor;
                this.errorLabel.Font = WizardHost.NormalTextFont;
                this.errorLabel.ForeColor = WizardHost.TextColor;
                this.BeginInvoke(new Procedure(this.DoInstallation), null);
            }

            base.OnLoad(e);
        }

        public override void OnNextClicked()
        {
            WizardHost.Close();
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
            this.toolTip = new ToolTip();
            this.infoText = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.errorLabel = new System.Windows.Forms.Label();
            this.topBanner = new PictureBox();
            this.bottomBanner = new PictureBox();
            this.SuspendLayout();
            // 
            // infoText
            // 
            this.infoText.Location = new System.Drawing.Point(12, 6);
            this.infoText.Name = "infoText";
            this.infoText.Size = new System.Drawing.Size(468, 44);
            this.infoText.TabIndex = 0;
            this.infoText.Text = "label1";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(42, 59);
            this.progressBar.MarqueeAnimationSpeed = 50;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(408, 19);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 1;
            this.progressBar.Visible = false;
            // 
            // errorLabel
            // 
            this.errorLabel.Location = new System.Drawing.Point(12, 50);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(468, 200);
            this.errorLabel.TabIndex = 2;
            this.errorLabel.Text = "label1";
            this.errorLabel.Visible = false;
            //
            // topBanner
            //
            this.topBanner.Name = "topBanner";
            this.topBanner.SizeMode = PictureBoxSizeMode.StretchImage;
            this.topBanner.Click += new EventHandler(Banner_Click);
            this.topBanner.Cursor = Cursors.Hand;
            this.topBanner.Visible = false;
            this.topBanner.Enabled = false;
            //
            // bottomBanner
            //
            this.bottomBanner.Name = "bottomBanner";
            this.bottomBanner.SizeMode = PictureBoxSizeMode.StretchImage;
            this.bottomBanner.Click += new EventHandler(Banner_Click);
            this.bottomBanner.Cursor = Cursors.Hand;
            this.bottomBanner.Visible = false;
            this.bottomBanner.Enabled = false;
            // 
            // InstallingPage
            // 
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.infoText);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.topBanner);
            this.Controls.Add(this.bottomBanner);
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.Name = "InstallingPage";
            this.ResumeLayout(false);

            // Size and Location for these is done after ResumeLayout() because otherwise
            // they would be setting their Size/Location in 96dpi, but ClientSize has already
            // been scaled by DPI and so these properties would effectively be double-scaled.
            Size scaledBannerSize = UI.ScaleSize(bannerSize);

            this.topBanner.Location = new Point(
                (ClientSize.Width - scaledBannerSize.Width) / 2,
                ClientSize.Height - (scaledBannerSize.Height * 2) - UI.ScaleHeight(20));

            this.topBanner.Size = scaledBannerSize;

            this.bottomBanner.Location = new Point(
                (ClientSize.Width - scaledBannerSize.Width) / 2,
                ClientSize.Height - scaledBannerSize.Height - UI.ScaleHeight(10));

            this.bottomBanner.Size = scaledBannerSize;
        }
        #endregion

        private void Banner_Click(object sender, EventArgs e)
        {
            PictureBox asPB = sender as PictureBox;

            if (asPB != null)
            {
                string url = asPB.Tag as string;

                if (url != null)
                {
                    PdnInfo.OpenUrl(this, url);
                }
            }
        }
    }
}
