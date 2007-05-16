/////////////////////////////////////////////////////////////////////////////////
// Mono Paint                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using Microsoft.Win32;
using PaintDotNet;
using PaintDotNet.SystemLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace PaintDotNet.Setup
{
    public class SetupWizard 
        : System.Windows.Forms.Form
    {
        private const string mutexName = "Mono Paint.SetupWizard";
        private const string regSubKey = @"SOFTWARE\Mono Paint";

        private Button nextButton;
        private Button backButton;
        private Button cancelButton;
        private Control headingSpacer;
        private PdnBanner pdnBanner;
        private System.ComponentModel.Container components = null;
        private Hashtable msiProperties = new Hashtable();
        private Label separator1;
        private PictureBox languageIcon;
        private ComboBox languageBox;
        private Label separator2;
        private Control whiteBackground;
        private Stack<Type> pages = new Stack<Type>();
        private WizardPage wizardPage;
        private bool finished = false;
        private bool skipConfig = false;
        private bool autoMode = false;
        private bool languageInitDone = false;
        private bool rebootRequired = false;
        private float xScale;
        private float yScale;

        public int ScaleX(int x)
        {
            return (int)ScaleX((float)x);
        }

        public float ScaleX(float x)
        {
            return x * xScale;
        }

        public int ScaleY(int y)
        {
            return (int)ScaleY((float)y);
        }

        public float ScaleY(float y)
        {
            return y * yScale;
        }

        public bool RebootRequired
        {
            get
            {
                return this.rebootRequired;
            }

            set
            {
                this.rebootRequired = value;
            }
        }

        public bool SkipConfig
        {
            get
            {
                return this.skipConfig;
            }

            set
            {
                this.skipConfig = value;
            }
        }

        public bool AutoMode
        {
            get
            {
                return this.autoMode;

            }

            set
            {
                this.autoMode = value;
            }
        }

        public string HeaderText
        {
            get
            {
                return this.pdnBanner.BannerText;
            }

            set
            {
                this.pdnBanner.BannerText = value;
            }
        }

        public void SetNextEnabled(bool enabled)
        {
            this.nextButton.Enabled = enabled;
        }

        public void SetBackEnabled(bool enabled)
        {
            this.backButton.Enabled = enabled;
        }

        public void SetCancelEnabled(bool enabled)
        {
            this.cancelButton.Enabled = enabled;
        }

        public void SetFinished(bool finished)
        {
            this.finished = finished;

            if (finished)
            {
                SetNextEnabled(true);
                SetBackEnabled(false);
                SetCancelEnabled(false);
                this.AcceptButton = this.nextButton;
                this.nextButton.Text = PdnResources.GetString("SetupWizard.NextButton.Text.Finished");

                if (this.autoMode)
                {
                    Close();
                }
            }
            else
            {
                this.AcceptButton = null;
                this.nextButton.Text = PdnResources.GetString("SetupWizard.NextButton.Text");
            }
        }

        public void SetMsiProperty(string property, string value)
        {
            msiProperties[property] = value;
        }

        public void SaveMsiProperties()
        {
            using (RegistryKey key = Registry.LocalMachine.CreateSubKey(regSubKey))
            {
                if (key != null)
                {
                    foreach (string name in msiProperties.Keys)
                    {
                        string value = (string)msiProperties[name];
                        key.SetValue(name, value);
                    }
                }
            }
        }

        public string GetMsiCommandLine()
        {
            string commandLine = string.Empty;

            foreach (string property in this.msiProperties.Keys)
            {
                string value = (string)this.msiProperties[property];
                commandLine += property + "=" + "\"" + value + "\" ";
            }

            // Remove trailing space
            if (commandLine.Length > 1)
            {
                commandLine = commandLine.Substring(0, commandLine.Length - 1);
            }

            return commandLine;
        }

        public void AddPropertyFromArg(string arg)
        {
            int indexOfEq = arg.IndexOf('=');

            if (indexOfEq != -1)
            {
                string property = arg.Substring(0, indexOfEq);
                string value = arg.Substring(indexOfEq + 1, arg.Length - indexOfEq - 1);
                SetMsiProperty(property, value);
            }
        }

        public Hashtable MsiProperties
        {
            get
            {
                return (Hashtable)this.msiProperties.Clone();
            }
        }

        public string GetMsiProperty(string property, string defaultValue)
        {
            return GetMsiProperty(property, defaultValue, false);
        }

        public string GetMsiProperty(string property, string defaultValue, bool forceRegRead)
        {
            string returnVal;

            if (msiProperties.Contains(property) && !forceRegRead)
            {
                returnVal = (string)msiProperties[property];
            }
            else
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regSubKey, false))
                {
                    if (key == null)
                    {
                        returnVal = defaultValue;
                    }
                    else
                    {
                        returnVal = (string)key.GetValue(property, defaultValue);
                    }
                }

            }

            if (defaultValue != null)
            {
                msiProperties[property] = returnVal;
            }

            return returnVal;
        }

        public Font HeadingTextFont
        {
            get
            {
                return Utility.CreateFont("Tahoma", 10.0f, FontStyle.Regular);
            }
        }

        public Font NormalTextFont
        {
            get
            {
                return Utility.CreateFont("Verdana", 8.0f, FontStyle.Regular);
            }
        }

        public Font FixedWidthFont
        {
            get
            {
                return Utility.CreateFont("Courier New", 9.0f, FontStyle.Regular);
            }
        }

        public Font FootNoteFont
        {
            get
            {
                return Utility.CreateFont("Verdana", 6.5f, FontStyle.Regular);
            }
        }

        public Color TextColor
        {
            get
            {
                return Color.Black;
            }
        }

        private void SetPage(Type pageType)
        {
            ConstructorInfo ci = pageType.GetConstructor(Type.EmptyTypes);
            object obj = ci.Invoke(new object[0]);
            WizardPage page = (WizardPage)obj;
            page.WizardHost = this;
            this.Controls.Remove(this.wizardPage);
            page.Location = new Point(0, ScaleY(77));
            page.Size = new Size(ClientSize.Width, ScaleY(259));
            SetNextEnabled(true);
            SetBackEnabled(true);
            SetCancelEnabled(true);
            this.wizardPage = page;
            this.Controls.Add(this.wizardPage);
            this.Controls.SetChildIndex(this.wizardPage, 0);

            this.languageBox.Visible = (pageType == typeof(IntroPage));
            this.languageIcon.Visible = this.languageBox.Visible;
        }

        public void ClearPageStack()
        {
            this.pages.Clear();
        }

        public void GoToPage(Type type)
        {
            if (this.wizardPage != null)
            {
                this.pages.Push(this.wizardPage.GetType());
            }

            SetPage(type);
        }

        public SetupWizard()
        {
            this.SuspendLayout();

            using (Graphics g = this.CreateGraphics())
            {
                this.xScale = g.DpiX / 96.0f;
                this.yScale = g.DpiY / 96.0f;
            }

            UI.InitScaling(this);

            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            this.ResumeLayout(false);

            this.languageIcon.Image = PdnResources.GetImage("Icons.MenuHelpLanguageIcon.png");

            LoadResources();
        }

        private string GetProgramFilesDir()
        {
            // Environment.GetFolderPath() has a bug in that it will freak out when %PROGRAMFILES% is set
            // to something like "D:" (note the lack of a backslash).
            string path = NativeMethods.SHGetFolderPath((int)(NativeConstants.CSIDL_PROGRAM_FILES | NativeConstants.CSIDL_FLAG_CREATE));

            if (path.Length == 2 && path[1] == ':')
            {
                path = path + Path.DirectorySeparatorChar;
            }

            return path;
        }

        private string GetCultureInfoName(CultureInfo ci)
        {
            CultureInfo en_US = new CultureInfo("en-US");

            // For "English (United States)" we'd rather just display "English"
            if (ci.Equals(en_US))
            {
                return GetCultureInfoName(ci.Parent);
            }
            else
            {
                return ci.NativeName;
            }
        }

        private class CultureNameAndInfo
        {
            private CultureInfo cultureInfo;
            private string displayName;

            public CultureInfo CultureInfo
            {
                get
                {
                    return this.cultureInfo;
                }
            }

            public string DisplayName
            {
                get
                {
                    return this.displayName;
                }
            }

            public CultureNameAndInfo(CultureInfo cultureInfo, string displayName)
            {
                this.cultureInfo = cultureInfo;
                this.displayName = displayName;
            }
        }

        private void LoadResources()
        {
            this.Icon = PdnInfo.AppIcon;
            this.Text = PdnInfo.GetProductName();
            this.cancelButton.Text = PdnResources.GetString("SetupWizard.CancelButton.Text");
            this.backButton.Text = PdnResources.GetString("SetupWizard.BackButton.Text");
            this.nextButton.Text = PdnResources.GetString("SetupWizard.NextButton.Text");
        }

        protected override void OnLoad(EventArgs e)
        {
            // Setup default install dir
            string targetSubDir = PdnResources.GetString("SetupWizard.InstallDirPage.DefaultTargetSubDir");
            string programFilesDir = GetProgramFilesDir();
            string defaultTargetDir = Path.Combine(programFilesDir, targetSubDir);
            string targetDir = GetMsiProperty(PropertyNames.TargetDir, defaultTargetDir);

            // Set other default properties
            string jpgPngBmpEditor = GetMsiProperty(PropertyNames.JpgPngBmpEditor, "1");
            string tgaEditor = GetMsiProperty(PropertyNames.TgaEditor, "1");
            string checkForUpdates = GetMsiProperty(PropertyNames.CheckForUpdates, "1");
            string checkForBetas = GetMsiProperty(PropertyNames.CheckForBetas, PropertyNames.CheckForBetasDefault);

            if (!PdnInfo.IsFinalBuild)
            {
                checkForBetas = "1";
            }

            this.pdnBanner.BannerFont = this.HeadingTextFont;

            if (this.wizardPage == null || this.wizardPage.GetType() == typeof(IntroPage))
            {
                // Populate the language combo box
                string[] locales = PdnResources.GetInstalledLocales();
                CultureNameAndInfo[] cnais = new CultureNameAndInfo[locales.Length];

                for (int i = 0; i < locales.Length; ++i)
                {
                    string locale = locales[i];
                    CultureInfo ci = new CultureInfo(locale);
                    CultureNameAndInfo cnai = new CultureNameAndInfo(ci, GetCultureInfoName(ci));
                    cnais[i] = cnai;
                }

                Array.Sort(
                    cnais,
                    delegate(CultureNameAndInfo lhs, CultureNameAndInfo rhs)
                    {
                        return string.Compare(lhs.DisplayName, rhs.DisplayName,
                            StringComparison.InvariantCultureIgnoreCase);
                    });

                this.languageBox.DataSource = cnais;
                this.languageBox.DisplayMember = "DisplayName";

                // Choose the current locale

                // First find English
                CultureNameAndInfo englishCnai;

                englishCnai = Array.Find(
                    cnais,
                    delegate(CultureNameAndInfo cnai)
                    {
                        if (cnai.CultureInfo.Name.Length == 0 || cnai.CultureInfo.Name == "en-US")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    });

                // Next, figure out what culture we're currently set to
                CultureNameAndInfo currentCnai;

                currentCnai = Array.Find(
                    cnais,
                    delegate(CultureNameAndInfo cnai)
                    {
                        return (cnai.CultureInfo == PdnResources.Culture);
                    });

                if (currentCnai == null)
                {
                    this.languageBox.SelectedItem = englishCnai;
                }
                else
                {
                    this.languageBox.SelectedItem = currentCnai;
                }

                this.languageInitDone = true;

                // Go to the appropriate page
                if (this.skipConfig)
                {
                    GoToPage(typeof(InstallingPage));
                }
                else
                {
                    GoToPage(typeof(IntroPage));
                }
            }

            base.OnLoad(e);
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

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!this.finished)
            {
                string title = PdnInfo.GetBareProductName();
                string message = PdnResources.GetString("SetupWizard.CancelDialog.Message");
                DialogResult result = MessageBox.Show(this, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

            base.OnClosing(e);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nextButton = new Button();
            this.backButton = new Button();
            this.cancelButton = new Button();
            this.separator1 = new Label();
            this.headingSpacer = new Control();
            this.separator2 = new Label();
            this.languageIcon = new PictureBox();
            this.languageBox = new ComboBox();
            this.whiteBackground = new Control();
            this.pdnBanner = new PdnBanner();
            this.SuspendLayout();
            // 
            // nextButton
            // 
            this.nextButton.Location = new Point(412, 350);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new Size(75, 23);
            this.nextButton.TabIndex = 0;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(332, 350);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 1;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(244, 350);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            //
            // pdnBanner
            //
            this.pdnBanner.Location = new Point(0, 0);
            // 
            // headingSpacer
            // 
            this.headingSpacer.BackColor = System.Drawing.Color.White;
            this.headingSpacer.Location = new System.Drawing.Point(0, 48);
            this.headingSpacer.Name = "headingSpacer";
            this.headingSpacer.Size = new System.Drawing.Size(55, 23);
            this.headingSpacer.TabIndex = 5;
            this.headingSpacer.Text = "headingSpacer";
            // 
            // separator1
            // 
            this.separator1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separator1.Location = new System.Drawing.Point(0, 339);
            this.separator1.Name = "separator1";
            this.separator1.Size = new System.Drawing.Size(503, 2);
            this.separator1.TabIndex = 3;
            // 
            // separator2
            // 
            this.separator2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separator2.Location = new System.Drawing.Point(0, 71);
            this.separator2.Name = "separator2";
            this.separator2.Size = new System.Drawing.Size(503, 2);
            this.separator2.TabIndex = 6;
            //
            // whiteBackground
            //
            this.whiteBackground.Location = new System.Drawing.Point(0, 73);
            this.whiteBackground.Size = new System.Drawing.Size(503, 266);
            this.whiteBackground.BackColor = Color.FromArgb(255, 255, 255);
            //
            // languageIcon
            //
            this.languageIcon.Location = new System.Drawing.Point(10, 352);
            this.languageIcon.Name = "languageIcon";
            this.languageIcon.Size = new Size(16, 16);
            this.languageIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            // 
            // languageBox
            // 
            this.languageBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageBox.Location = new System.Drawing.Point(32, 350);
            this.languageBox.Name = "languageBox";
            this.languageBox.Size = new System.Drawing.Size(140, 21);
            this.languageBox.TabIndex = 7;
            this.languageBox.SelectedIndexChanged += new System.EventHandler(this.LanguageBox_SelectedIndexChanged);
            // 
            // SetupWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(495, 382);
            this.Controls.Add(this.languageBox);
            this.Controls.Add(this.languageIcon);
            this.Controls.Add(this.separator2);
            this.Controls.Add(this.separator1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.pdnBanner);
            this.Controls.Add(this.headingSpacer);
            this.Controls.Add(this.whiteBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SetupWizard";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }
        #endregion

        private void LanguageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CultureNameAndInfo cnai = this.languageBox.SelectedItem as CultureNameAndInfo;

            if (this.languageInitDone && cnai != null && 
                cnai.CultureInfo != PdnResources.Culture)
            {
                PdnResources.Culture = cnai.CultureInfo;
                LoadResources();
                this.pages.Clear();
                this.GoToPage(typeof(IntroPage));
            }
        }

        private static bool CheckRequirements()
        {
            bool pass = true;

            // Check for Win2K or later
            bool osRequirement = OS.CheckOSRequirement();
            pass &= osRequirement;

            // Check for admin
            bool isAdmin = Security.IsAdministrator;
            pass &= isAdmin;

            // Show error if necessary
            if (!pass)
            {
                string title = PdnInfo.GetBareProductName();
                string message = "internal error";

                if (!osRequirement)
                {
                    message = PdnResources.GetString("Error.OSRequirement");
                }
                else if (!isAdmin)
                {
                    message = PdnResources.GetString("SetupWizard.Error.AdminRequired");
                }

                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return pass;
        }

        private static void ShowHelp()
        {
            string title = PdnInfo.GetBareProductName();
            string helpText = PdnResources.GetString("SetupWizard.HelpText");
            MessageBox.Show(helpText, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) 
        {
            try
            {
                MainImpl(args);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private static void KillParentPdnExe()
        {
            try
            {
                Process[] pdnList = Process.GetProcessesByName("PaintDotNet");

                foreach (Process process in pdnList)
                {
                    try
                    {
                        process.Kill();
                    }

                    catch (Exception)
                    {
                    }
                }
            }

            catch (Exception)
            {
            }
        }

        static void MainImpl(string[] args)
        {
            if (!PdnInfo.HandleExpiration(null))
            {
                return;
            }

            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
            UI.EnableDpiAware();

            // Uncomment to test German
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de");
                    
            bool doInstall = true;
            bool doMsiDump = false;
            bool restartPdnOnExit = false;
            string[] propertyDefaults = PropertyNames.Defaults;

            SetupWizard setupWizard = new SetupWizard();

            // Parse through command-line options
            for (int i = 0; i < args.Length; ++i)
            {
                string arg = args[i];
                string argLower = arg.ToLower();

                switch (argLower)
                {
                    case "-?":
                    case "/?":
                    case "-help":
                    case "/help":
                        ShowHelp();
                        doInstall = false;
                        break;

                    case "-restartpdnonexit":
                    case "/restartpdnonexit":
                        restartPdnOnExit = true;
                        break;

                    case "-skipconfig":
                    case "/skipconfig":
                        KillParentPdnExe();
                        setupWizard.SkipConfig = true;
                        setupWizard.SetMsiProperty(PropertyNames.PdnUpdating, "1");
                        break;

                    case "-auto":
                    case "/auto":
                        setupWizard.AutoMode = true;
                        setupWizard.SkipConfig = true;
                        break;

                    case "-createmsi":
                    case "/createmsi":
                        doMsiDump = true;
                        propertyDefaults = PropertyNames.AdGpoDefaults;
                        break;

                    default:
                        setupWizard.AddPropertyFromArg(arg);
                        break;
                }
            }

            // Load all the propreties that we always need to have. Defaults will be loaded
            // for properties that are not already set.
            for (int i = 0; i < propertyDefaults.Length; i += 2)
            {
                setupWizard.GetMsiProperty(propertyDefaults[i], propertyDefaults[i + 1]);
            }

            setupWizard.SetMsiProperty(PropertyNames.PdnUpdating, "0");

            // Only allow 1 instance of setup wizard running at a time...
            bool createdNew;
            Mutex mutex = new Mutex(false, mutexName, out createdNew);

            doInstall &= createdNew;

            if (doInstall)
            {
                if (CheckRequirements())
                {
                    if (doMsiDump)
                    {
                        setupWizard.ClearPageStack();
                        setupWizard.GoToPage(typeof(CreateMsiPage));
                    }

                    setupWizard.ShowDialog();
                }

                // When we do an update, Mono Paint launches our installer with the /restartPdnOnExit
                // flag. This tells us to run Mono Paint when the update is finished. This accomplishes
                // two things:
                //
                // 1. Adds a slight amount of continuity for the user. When they're installing an update
                //    they'll probably want to start-up Mono Paint right away. So we do it for them.
                // 2. Cleans up (deletes) the downloaded setup file. Otherwise, the user has to make sure
                //    that they immediately re-run Mono Paint under the same user account in order to
                //    clean this up.
                // 
                // #2 does introduce a slight race condition, so PaintDotNet.exe will actually retry
                // up to 3 times to delete the file with a 1 second pause between retries. This should
                // give enough time for the setup processes to unwind without causing too horrible of
                // a delay in the worst case.
                if (restartPdnOnExit && !setupWizard.RebootRequired)
                {
                    string targetDir = setupWizard.GetMsiProperty(PropertyNames.TargetDir, null);

                    if (targetDir != null)
                    {
                        string pdnPathName = Path.Combine(targetDir, "PaintDotNet.exe");

                        if (File.Exists(pdnPathName))
                        {
                            Process.Start(pdnPathName);
                        }
                    }
                }

                setupWizard.Dispose();
            }

            mutex.Close();
        }

        private void nextButton_Click(object sender, System.EventArgs e)
        {
            this.wizardPage.OnNextClicked();
        }

        private void backButton_Click(object sender, System.EventArgs e)
        {
            if (this.pages.Count > 0)
            {
                Type pageType = (Type)this.pages.Pop();
                SetPage(pageType);
            }
        }

        private void cancelButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}

