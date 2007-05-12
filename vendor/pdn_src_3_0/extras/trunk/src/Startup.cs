/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet.SystemLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PaintDotNet
{
    public sealed class Startup
    {
        private static Startup instance;
        private static DateTime startupTime;
        private string[] args;
        private MainForm mainForm;

        private Startup(string[] args)
        {
            this.args = args;
        }

        /// <summary>
        /// Starts a new instance of Paint.NET with the give arguments.
        /// </summary>
        /// <param name="fileName">The name of the filename to open, or null to start with a blank canvas.</param>
        public static void StartNewInstance(IWin32Window parent, bool requireAdmin, string[] args)
        {
            StringBuilder allArgsSB = new StringBuilder();

            foreach (string arg in args)
            {
                allArgsSB.Append(' ');

                if (arg.IndexOf(' ') != -1)
                {
                    allArgsSB.Append('"');
                }

                allArgsSB.Append(arg);

                if (arg.IndexOf(' ') != -1)
                {
                    allArgsSB.Append('"');
                }
            }

            string allArgs;

            if (allArgsSB.Length > 0)
            {
                allArgs = allArgsSB.ToString(1, allArgsSB.Length - 1);
            }
            else
            {
                allArgs = null;
            }

            Shell.Execute(parent, Application.ExecutablePath, allArgs, requireAdmin, Shell.ExecuteWaitType.ReturnImmediately);
        }

        public static void StartNewInstance(IWin32Window parent, string fileName)
        {
            string arg;

            if (fileName != null && fileName.Length != 0)
            {
                arg = "\"" + fileName + "\"";
            }
            else
            {
                arg = "";
            }

            StartNewInstance(parent, false, new string[1] { arg });
        }

        private static bool CloseForm(Form form)
        {
            ArrayList openForms = new ArrayList(Application.OpenForms);

            if (openForms.IndexOf(form) == -1)
            {
                return false;
            }

            form.Close();

            ArrayList openForms2 = new ArrayList(Application.OpenForms);

            if (openForms2.IndexOf(form) == -1)
            {
                return true;
            }

            return false;
        }

        public static bool CloseApplication()
        {
            bool returnVal = true;

            List<Form> allFormsButMainForm = new List<Form>();

            foreach (Form form in Application.OpenForms)
            {
                if (form.Modal && !object.ReferenceEquals(form, instance.mainForm))
                {
                    allFormsButMainForm.Add(form);
                }
            }

            if (allFormsButMainForm.Count > 0)
            {
                // Cannot close application if there are modal dialogs
                return false;
            }

            returnVal = CloseForm(instance.mainForm);
            return returnVal;
        }
        
        /// <summary>
        /// Checks to make sure certain files are present, and tries to repair the problem.
        /// </summary>
        /// <returns>
        /// true if any repairs had to be made, at which point PDN must be restarted.
        /// false otherwise, if everything's okay.
        /// </returns>
        private bool CheckForImportantFiles()
        {
            string[] requiredFiles =
                new string[]
                {
#if WINDOWS
                    "ICSharpCode.SharpZipLib.dll",
                    "Interop.WIA.dll",
                    "SetupNgen.exe",
                    "ShellExtension_x64.dll",
                    "ShellExtension_x86.dll",
                    "UpdateMonitor.exe",
                    "WiaProxy32.exe",
#else
				    "PaintDotNet.Data.dll",
                    "PaintDotNet.Effects.dll",
                    "PaintDotNet.Resources.dll",
                    "PaintDotNet.Strings.3.DE.resources",
                    "PaintDotNet.Strings.3.ES.resources",
                    "PaintDotNet.Strings.3.FR.resources",
                    "PaintDotNet.Strings.3.JA.resources",
                    "PaintDotNet.Strings.3.KO.resources",
                    "PaintDotNet.Strings.3.PT-BR.resources",
                    "PaintDotNet.Strings.3.resources",
                    "PaintDotNet.Strings.3.ZH-CN.resources",
                    "PaintDotNet.StylusReader.dll",
                    "PaintDotNet.SystemLayer.dll",
                    "PdnLib.dll"
#endif
                };

            string dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            List<string> missingFiles = null;

            foreach (string requiredFile in requiredFiles)
            {
                bool missing;

                try
                {
                    string pathName = Path.Combine(dirName, requiredFile);
                    FileInfo fileInfo = new FileInfo(pathName);
                    missing = !fileInfo.Exists;
                }

                catch (Exception)
                {
                    missing = true;
                }

                if (missing)
                {
                    if (missingFiles == null)
                    {
                        missingFiles = new List<string>();
                    }

                    missingFiles.Add(requiredFile);
                }
            }

            if (missingFiles == null)
            {
                return false;
            }
            else
            {
                if (Shell.ReplaceMissingFiles(missingFiles.ToArray()))
                {
                    // Everything is repaired and happy.
                    return true;
                }
                else
                {
                    // Things didn't get fixed. Bail.
                    Process.GetCurrentProcess().Kill();
                    return false;
                }
            }
        }

        public void Start()
        {
            // Set up unhandled exception handlers
#if DEBUG
            // In debug builds we'd prefer to have it dump us into the debugger
#else
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
#endif

            // Initialize some misc. Windows Forms settings
            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
            UI.EnableDpiAware();

            // If any files are missing, try to repair.
            // However, support /skipRepairAttempt for when developing in the IDE 
            // so that we don't needlessly try to repair in that case.
            if (this.args.Length > 0 && 
                string.Compare(this.args[0], "/skipRepairAttempt", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                // do nothing: we need this so that we can run from IDE/debugger
                // without it trying to repair itself all the time
            }
            else
            {
                if (CheckForImportantFiles())
                {
                    Startup.StartNewInstance(null, false, args);
                    return;
                }
            }

            // The rest of the code is put in a separate method so that certain DLL's
            // won't get delay loaded until after we try to do repairs.
            StartPart2();
        }

        private void StartPart2()
        {
            // Set up locale / resource details
            string locale = Settings.CurrentUser.GetString(PdnSettings.LanguageName, null);

            if (locale == null)
            {
                locale = Settings.SystemWide.GetString(PdnSettings.LanguageName, null);
            }

            if (locale != null)
            {
                CultureInfo ci = new CultureInfo(locale, true);
                Thread.CurrentThread.CurrentUICulture = ci;
            }

            // Check system requirements
            if (!OS.CheckOSRequirement())
            {
                string message = PdnResources.GetString("Error.OSRequirement");
                Utility.ErrorBox(null, message);
                return;
            }

            // Parse command-line arguments
            if (this.args.Length == 1 && 
                this.args[0] == Updates.UpdatesOptionsDialog.CommandLineParameter)
            {
                Updates.UpdatesOptionsDialog.ShowUpdateOptionsDialog(null, false);
            }
            else
            {
                SingleInstanceManager singleInstanceManager = new SingleInstanceManager("PaintDotNet");

                // If this is not the first instance of PDN.exe, then forward the command-line
                // parameters over to the first instance.
                if (!singleInstanceManager.IsFirstInstance)
                {
                    singleInstanceManager.FocusFirstInstance();

                    foreach (string arg in this.args)
                    {
                        singleInstanceManager.SendInstanceMessage(arg, 30);
                    }

                    singleInstanceManager.Dispose();
                    singleInstanceManager = null;

                    return;
                }

                // Create main window
                this.mainForm = new MainForm(this.args);

                // if the display is set to a portrait mode (tall), then orient the PDN window the same way
                if (this.mainForm.ScreenAspect < 1.0)
                {
                    int width = mainForm.Width;
                    int height = mainForm.Height;

                    this.mainForm.Width = height;
                    this.mainForm.Height = width;
                }

                // if the window opens and part of it is off screen, correct this
                Screen screen = Screen.FromControl(this.mainForm);

                Rectangle intersect = Rectangle.Intersect(screen.Bounds, mainForm.Bounds);
                if (intersect.Width == 0 || intersect.Height == 0)
                {
                    mainForm.Location = new Point(screen.Bounds.Left + 16, screen.Bounds.Top + 16);
                }

                // if the window is not big enough, correct this
                if (this.mainForm.Width < 200)
                {
                    this.mainForm.Width = 200; // this value was chosen arbitrarily
                }

                if (this.mainForm.Height < 200)
                {
                    this.mainForm.Height = 200; // this value was chosen arbitrarily
                }

                this.mainForm.SingleInstanceManager = singleInstanceManager;
                singleInstanceManager = null; // mainForm owns it now

                // 3 2 1 go
                Application.Run(this.mainForm);

                this.mainForm.Dispose();
                this.mainForm = null;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static int Main(string[] args) 
        {
            startupTime = DateTime.Now;

#if !DEBUG
            try
            {
#endif
                instance = new Startup(args);
                instance.Start();
#if !DEBUG
            }

            catch (Exception ex)
            {
                try
                {
                    UnhandledException(ex);
                    Process.GetCurrentProcess().Kill();
                }

                catch (Exception)
                {
                    MessageBox.Show(ex.ToString());
                    Process.GetCurrentProcess().Kill();
                }
            }
#endif

            return 0;
        }

        private static void UnhandledException(Exception ex)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            const string fileName = "pdncrash.log";
            string fullName = Path.Combine(dir, fileName);

            using (StreamWriter stream = new System.IO.StreamWriter(fullName, true))
            {
                stream.AutoFlush = true;

                string headerFormat;

                try
                {
                    headerFormat = PdnResources.GetString("CrashLog.HeaderText.Format");
                }

                catch (Exception ex13)
                {
                    headerFormat = 
                        InvariantStrings.CrashLogHeaderTextFormatFallback + 
                        ", --- Exception while calling PdnResources.GetString(\"CrashLog.HeaderText.Format\"): " + 
                        ex13.ToString() + 
                        Environment.NewLine;
                }

                string header;

                try
                {
                    header = string.Format(headerFormat, InvariantStrings.FeedbackEmail);
                }

                catch
                {
                    header = string.Empty;
                }

                stream.WriteLine(header);

                const string noInfoString = "err";

                string fullAppName = noInfoString;
                string timeOfCrash = noInfoString;
                string appUptime = noInfoString;
                string osVersion = noInfoString;
                string osRevision = noInfoString;
                string osType = noInfoString;
                string processorNativeArchitecture = noInfoString;
                string fxVersion = noInfoString;
                string processorArchitecture = noInfoString;
                string cpuName = noInfoString;
                string cpuCount = noInfoString;
                string totalPhysicalBytes = noInfoString;
                string localeName = noInfoString;
                string inkInfo = noInfoString;

                try
                {
                    try
                    {
                        fullAppName = PdnInfo.GetFullAppName();
                    }

                    catch (Exception ex1)
                    {
                        fullAppName = Application.ProductVersion + ", --- Exception while calling PdnInfo.GetFullAppName(): " + ex1.ToString() + Environment.NewLine;
                    }

                    try
                    {
                        timeOfCrash = DateTime.Now.ToString();
                    }

                    catch (Exception ex2)
                    {
                        timeOfCrash = "--- Exception while populating timeOfCrash: " + ex2.ToString() + Environment.NewLine;
                    }

                    try
                    {
                        appUptime = (DateTime.Now - startupTime).ToString();
                    }

                    catch (Exception ex13)
                    {
                        appUptime = "--- Exception while populating appUptime: " + ex13.ToString() + Environment.NewLine;
                    }

                    try
                    {
                        osVersion = System.Environment.OSVersion.Version.ToString();
                    }

                    catch (Exception ex3)
                    {
                        osVersion = "--- Exception while populating osVersion: " + ex3.ToString() + Environment.NewLine;
                    }

                    try
                    {
                        osRevision = OS.Revision;
                    }

                    catch (Exception ex4)
                    {
                        osRevision = "--- Exception while populating osRevision: " + ex4.ToString() + Environment.NewLine;
                    }

                    try
                    {
                        osType = OS.Type.ToString();
                    }

                    catch (Exception ex5)
                    {
                        osType = "--- Exception while populating osType: " + ex5.ToString() + Environment.NewLine;
                    }

                    try
                    {
                        processorNativeArchitecture = Processor.NativeArchitecture.ToString().ToLower();
                    }

                    catch (Exception ex6)
                    {
                        processorNativeArchitecture = "--- Exception while populating processorNativeArchitecture: " + ex6.ToString() + Environment.NewLine;
                    }

                    try
                    {
                        fxVersion = System.Environment.Version.ToString();
                    }

                    catch (Exception ex7)
                    {
                        fxVersion = "--- Exception while populating fxVersion: " + ex7.ToString() + Environment.NewLine;
                    }

                    try
                    {
                        processorArchitecture = Processor.Architecture.ToString().ToLower();
                    }

                    catch (Exception ex8)
                    {
                        processorArchitecture = "--- Exception while populating processorArchitecture: " + ex8.ToString() + Environment.NewLine;
                    }

                    try
                    {
                        cpuName = SystemLayer.Processor.CpuName;
                    }

                    catch (Exception ex9)
                    {
                        cpuName = "--- Exception while populating cpuName: " + ex9.ToString() + Environment.NewLine;
                    }

                    try
                    {
                        cpuCount = SystemLayer.Processor.LogicalCpuCount.ToString() + "x";
                    }

                    catch (Exception ex10)
                    {
                        cpuCount = "--- Exception while populating cpuCount: " + ex10.ToString() + Environment.NewLine;
                    }

                    try
                    {
                        totalPhysicalBytes = ((SystemLayer.Memory.TotalPhysicalBytes / 1024) / 1024) + " MB";
                    }

                    catch (Exception ex11)
                    {
                        totalPhysicalBytes = "--- Exception while populating totalPhysicalBytes: " + ex11.ToString() + Environment.NewLine;
                    }

                    try
                    {
                        localeName = 
                            "pdnr.c: " + PdnResources.Culture.Name +
                            ", hklm: " + Settings.SystemWide.GetString(PdnSettings.LanguageName, "n/a") +
                            ", hkcu: " + Settings.CurrentUser.GetString(PdnSettings.LanguageName, "n/a") + 
                            ", cc: " + CultureInfo.CurrentCulture.Name + 
                            ", cuic: " + CultureInfo.CurrentUICulture.Name;
                    }

                    catch (Exception ex14)
                    {
                        localeName = "--- Exception while populating localeName: " + ex14.ToString() + Environment.NewLine;
                    }

                    try
                    {
                        inkInfo = Ink.IsAvailable() ? "yes" : "no";
                    }

                    catch (Exception ex15)
                    {
                        inkInfo = "--- Exception while populating inkInfo: " + ex15.ToString() + Environment.NewLine;
                    }
                }

                catch (Exception ex12)
                {
                    stream.WriteLine("Exception while gathering app and system info: " + ex12.ToString());
                }

                stream.WriteLine("Application version: " + fullAppName);
                stream.WriteLine("Time of crash: " + timeOfCrash);
                stream.WriteLine("Application uptime: " + appUptime);

                stream.WriteLine("OS Version: " + osVersion + " " + osRevision + " " + osType + " " + processorNativeArchitecture);
                stream.WriteLine(".NET Framework version: " + fxVersion + " " + processorArchitecture);
                stream.WriteLine("Processor: " + cpuCount + " " + cpuName);
                stream.WriteLine("Physical memory: " + totalPhysicalBytes);
                stream.WriteLine("Tablet PC: " + inkInfo);
                stream.WriteLine("Locale: " + localeName);
                stream.WriteLine();

                stream.WriteLine("Exception details:");
                stream.WriteLine(ex.ToString());

                // Determine if there is any 'secondary' exception to report
                Exception[] otherEx = null;

                if (ex is System.Reflection.ReflectionTypeLoadException)
                {
                    otherEx = ((System.Reflection.ReflectionTypeLoadException)ex).LoaderExceptions;
                }

                if (otherEx != null)
                {
                    for (int i = 0; i < otherEx.Length; ++i)
                    {
                        stream.WriteLine();
                        stream.WriteLine("Secondary exception details:");

                        if (otherEx[i] == null)
                        {
                            stream.WriteLine("(null)");
                        }
                        else
                        {
                            stream.WriteLine(otherEx[i].ToString());
                        }
                    }
                }

                stream.WriteLine("------------------------------------------------------------------------------");
            }

            string errorFormat;
            string errorText;

            try
            {
                errorFormat = PdnResources.GetString("Startup.UnhandledError.Format");
            }

            catch (Exception)
            {
                errorFormat = InvariantStrings.StartupUnhandledErrorFormatFallback;
            }

            errorText = string.Format(errorFormat, fileName);
            Utility.ErrorBox(null, errorText);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            UnhandledException((Exception)e.ExceptionObject);
            Process.GetCurrentProcess().Kill();
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            UnhandledException(e.Exception);
            Process.GetCurrentProcess().Kill();
        }
    }
}
