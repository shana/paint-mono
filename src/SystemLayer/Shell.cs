/////////////////////////////////////////////////////////////////////////////////
// Mono Paint                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

// Unix port Copyright 2007 Novell, Inc.

using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace PaintDotNet.SystemLayer
{
    public static class Shell
    {
        /// <summary>
        /// Repairs the installation of Mono Paint by replacing any files that have gone missing.
        /// This method should only be called after it has been determined that the files are missing,
        /// and not as a way to determine which files are missing.
        /// This is used, for instance, if the resource files, such as PaintDotNet.Strings.3.resources,
        /// cannot be found. This is actually a top support issue, and by automatically repairing
        /// this problem we save a lot of people a lot of trouble.
        /// </summary>
        /// <param name="missingFiles">
        /// Friendly names for the files that are missing. These will not be used as part of the
        /// repair process but rather as part of any UI presented to the user, or in an exception that 
        /// will be thrown in the case of an error.
        /// </param>
        /// <returns>
        /// true if everything was successful, false if the user cancelled or does not have administrator
        /// privilege (and cannot elevate). An exception is thrown for errors.
        /// </returns>
        /// <remarks>
        /// Note to implementors: This may be implemented as a no-op. Just return true in this case.
        /// </remarks>
        public static bool ReplaceMissingFiles(string[] missingFiles)
        {
            // Generate a friendly, comma separated list of the missing file names
            StringBuilder missingFilesSB = new StringBuilder();

            for (int i = 0; i < missingFiles.Length; ++i)
            {
                missingFilesSB.Append(missingFiles[i]);

                if (i != missingFiles.Length - 1)
                {
                    missingFilesSB.Append(", ");
                }
            }

            try
            {
                // If they are not an admin and have no possibility of elevating, such as for a standard User
                // in XP, then give them an error. Unfortunately we do not know if we can even load text
                // resources at this point, and so must provide an English-only error message.
                if (!Security.IsAdministrator && !Security.CanElevateToAdministrator)
                {
                    MessageBox.Show(
                        null,
                        "Mono Paint has detected that some important installation files are missing. Repairing " +
                        "this requires administrator privilege. Please run the 'PdnRepair.exe' program in the installation " +
                        "directory after logging in with a user that has administrator privilege." + Environment.NewLine + 
                        Environment.NewLine + 
                        "The missing files are: " + missingFilesSB.ToString(),
                        "Mono Paint",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return false;
                }

                const int hMargin = 8;
                const int vMargin = 8;
                Form form = new Form();
                form.Text = "Mono Paint";
                form.ClientSize = new Size(400, 10);
                form.StartPosition = FormStartPosition.CenterScreen;

                Label infoLabel = new Label();
                form.Controls.Add(infoLabel);
                infoLabel.Text = 
                    "Mono Paint has detected that some important installation files are missing. If you click " +
                    "the Repair button it will attempt to repair this and then continue loading." + Environment.NewLine + 
                    Environment.NewLine +
                    "The missing files are: " + missingFilesSB.ToString();

#if DEBUG
                infoLabel.Text += Environment.NewLine +
                    "Since this is a DEBUG build, you should probably add /skipRepairAttempt to the command-line.";
#endif

                infoLabel.Location = new Point(hMargin, vMargin);
                infoLabel.Width = form.ClientSize.Width - hMargin * 2;
                infoLabel.Height = infoLabel.GetPreferredSize(new Size(infoLabel.Width, 1)).Height;

                Button repairButton = new Button();
                form.Controls.Add(repairButton);
                repairButton.Text = "&Repair";

                Exception exception = null;

                repairButton.Click +=
                    delegate(object sender, EventArgs e)
                    {
                        form.DialogResult = DialogResult.Yes;
                        repairButton.Enabled = false;

                        try
                        {
                            Shell.Execute(form, "PdnRepair.exe", "/noPause", false, ExecuteWaitType.WaitForExit);
                        }

                        catch (Exception ex)
                        {
                            exception = ex;
                        }
                    };

                repairButton.AutoSize = true;
                repairButton.PerformLayout();
                repairButton.Width += 20;
                repairButton.Location = new Point((form.ClientSize.Width - repairButton.Width) / 2, infoLabel.Bottom + vMargin * 2);
                repairButton.FlatStyle = FlatStyle.System;
                UI.EnableShield(repairButton, true);
                
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.ShowInTaskbar = true;
                form.Icon = null;
                form.ClientSize = new Size(form.ClientRectangle.Width, repairButton.Bottom + vMargin);

                DialogResult result = form.ShowDialog(null);
                form.Dispose();
                form = null;

                if (result == DialogResult.Yes)
                {
                    return true;
                }
                else if (exception == null)
                {
                    return false;
                }
                else
                {
                    throw new Exception("Error while attempting to repair", exception);
                }                
            }

            catch (Exception ex)
            {
                throw new Exception("Could not repair installation after it was determined that the following files are missing: " +
                    missingFilesSB.ToString(), ex);
            }
        }

        /// <summary>
        /// Opens the requested directory in the shell's file/folder browser.
        /// </summary>
        /// <param name="parent">The window that is currently in the foreground.</param>
        /// <param name="folderPath">The folder to open.</param>
        /// <remarks>
        /// This UI is presented modelessly, in another process, and in the foreground.
        /// Error handling and messaging (error dialogs) will be handled by the shell,
        /// and these errors will not be communicated to the caller of this method.
        /// </remarks>
        public static void BrowseFolder(IWin32Window parent, string folderPath)
        {
			Process.Start ("xdg-open", folderPath);
        }

        [Obsolete("Do not use this method.", true)]
        public static void Execute(
            IWin32Window parent,
            string exePath,
            string args,
            bool requireAdmin)
        {
            Execute(parent, exePath, args, requireAdmin, ExecuteWaitType.ReturnImmediately);
        }

        public enum ExecuteWaitType
        {
            /// <summary>
            /// Returns immediately after executing without waiting for the task to finish.
            /// </summary>
            ReturnImmediately,

            /// <summary>
            /// Waits until the task exits before returning control to the calling method.
            /// </summary>
            WaitForExit,

            /// <summary>
            /// Returns immediately after executing without waiting for the task to finish.
            /// However, another task will be spawned that will wait for the requested task
            /// to finish, and it will then relaunch Mono Paint if the task was successful.
            /// This is only intended to be used by the Mono Paint updater so that it can
            /// relaunch Mono Paint with the same user and privilege-level that initiated
            /// the update.
            /// </summary>
            RelaunchPdnOnExit
        }

        /// <summary>
        /// Uses the shell to execute the command. This method must only be used by Mono Paint
        /// and not by plugins.
        /// </summary>
        /// <param name="parent">
        /// The window that is currently in the foreground. This may be null if requireAdmin 
        /// is false and the executable that exePath refers to is not marked (e.g. via a 
        /// manifest) as requiring administrator privilege.
        /// </param>
        /// <param name="exePath">
        /// The path to the executable to launch.
        /// </param>
        /// <param name="args">
        /// The command-line arguments for the executable.
        /// </param>
        /// <param name="requireAdmin">
        /// Whether or not administrator privilege is required to launch this. However,
        /// if the executable is already marked as requiring administrator privilege
        /// (e.g. via a "requiresAdministrator" UAC manifest), this parameter should be 
        /// set to false.
        /// </param>
        /// <remarks>
        /// If administrator privilege is required, a consent UI may be displayed asking the
        /// user to approve the action. A parent window must be provided in this case so that
        /// the consent UI will know where to position itself. Administrator privilege is
        /// required if requireAdmin is set to true, or if the executable being launched
        /// has a manifest declaring that it requires this privilege and if the operating
        /// system recognizes the manifest.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// requireAdmin was true, but parent was null.
        /// </exception>
        /// <exception cref="SecurityException">
        /// requireAdmin was true, but the user does not have this privilege, nor do they 
        /// have the ability to acquire or elevate to obtain this privilege.
        /// </exception>
        /// <exception cref="Win32Exception">
        /// There was an error launching the program.
        /// </exception>
        public static void Execute(
            IWin32Window parent, 
            string exePath, 
            string args, 
            bool requireAdmin, 
            ExecuteWaitType executeWaitType)
        {
            const string runAs = "runas";

            if (exePath == null)
            {
                throw new ArgumentNullException("exePath");
            }

            if (requireAdmin && parent == null)
            {
                throw new ArgumentException("If requireAdmin is true, a parent window must be provided");
            }

            // If this action requires admin privilege, but the user does not have this
            // privilege and is not capable of acquiring this privilege, then we will
            // throw an exception.
            if (requireAdmin && !Security.IsAdministrator && !Security.CanElevateToAdministrator)
            {
                throw new SecurityException("Executable requires administrator privilege, but user is not an administrator and cannot elevate");
            }

			switch (executeWaitType){
			case ExecuteWaitType.ReturnImmediately:
				Process.Start (exePath, args);
				return;
				
			case ExecuteWaitType.RelaunchPdnOnExit:
				Console.WriteLine ("PORT: Execute does not support RelaunchPdnOnExit yet"); 
				goto case ExecuteWaitType.WaitForExit;
				
			case ExecuteWaitType.WaitForExit:	
				Process child = Process.Start (exePath, args);
				child.WaitForExit ();
				return;
			}
        }

        private static void RelaunchPdnHelperPart1(out string updateMonitorExePath)
        {
            const string updateExeFileName = "UpdateMonitor.exe";

            string srcDir = Application.StartupPath;
            string srcPath = Path.Combine(srcDir, updateExeFileName);

            string dstDir = Environment.ExpandEnvironmentVariables(@"%TEMP%\PdnSetup");
            string dstPath = Path.Combine(dstDir, updateExeFileName);

            if (!Directory.Exists(dstDir))
            {
                Directory.CreateDirectory(dstDir);
            }

            File.Copy(srcPath, dstPath, true);
            updateMonitorExePath = dstPath;
        }

        private static void RelaunchPdnHelperPart2(string updateMonitorExePath, IntPtr hProcess)
        {
            string args = hProcess.ToInt64().ToString(CultureInfo.InstalledUICulture);
            ProcessStartInfo psi = new ProcessStartInfo(updateMonitorExePath, args);
            psi.UseShellExecute = false;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            Process process = Process.Start(psi);
            process.Dispose();
        }

        /// <summary>
        /// Launches the default browser and opens the given URL.
        /// </summary>
        /// <remarks>
        /// This method will not present an error dialog if the URL could not be launched.
        /// Note: This method must only be used by Mono Paint, and not any plugins. It may
        /// change or be removed in future versions.
        /// </remarks>
        public static bool LaunchUrl(IWin32Window owner, string url)
        {
			Process.Start ("xdg-open", url);
            
            return true;
        }

        [Obsolete("Use PdnInfo.OpenUrl() instead. Shell.LaunchUrl() must only be used by Mono Paint code, not by plugins.", true)]
        public static bool OpenUrl(IWin32Window owner, string url)
        {
            return LaunchUrl(owner, url);
        }

		public static XmlNode make (XmlDocument d, string name, string value)
		{
			XmlNode e = d.CreateElement (name);
			e.InnerText = value;
			return e;
		}

		//
		// This is a hack, we should be probing the file for its type
		// but this will do for now
		//
		public static string GetMimeType (string filename)
		{
			Console.WriteLine ("PORT: GetMimeType is lame, its using the extension for the values, instead of probing the file");
			
			string s = filename.ToLower ();
			switch (Path.GetExtension (s)){
			case "png":
				return "image/png";
			case "jpg":
			case "jpeg":
			case "jpe":
				return "image/jpeg";
			case "gif":
				return "image/gif";
			case "tiff":
				return "image/tiff";
			case "bmp":
				return "image/bmp";
			}
			return "image/generic";
		}
		
        public static void AddToRecentDocumentsList(string fileName)
        {
            XmlDocument d = new XmlDocument ();
			XmlNode top = null;
			
			
			string home = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			string recent_files = Path.Combine (home, ".recently-used");
			
			try {
				d.Load (recent_files);
				top = d.SelectSingleNode ("/RecentFiles");
				if (top == null)
					throw new Exception ();
			} catch {
				top = d.CreateElement ("RecentFiles");
				d.AppendChild (top);
			}
			XmlNode recent = d.CreateElement ("RecentItem");
			top.AppendChild (recent);
			
			DateTime unix_epoch = new DateTime (1970, 1, 1, 0, 0, 0);
			
			recent.AppendChild (make (d, "URI", new Uri (fileName).ToString ()));
			recent.AppendChild (make (d, "Mime-Type", GetMimeType (fileName)));
			recent.AppendChild (make (d, "Timestamp", (DateTime.Now-unix_epoch).Seconds.ToString ()));
			
			try {
				d.Save (recent_files);
			} catch {
				Console.WriteLine ("error saving the ~/.recently-used file");
			}
        }
    }
}
