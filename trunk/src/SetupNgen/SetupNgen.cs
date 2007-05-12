/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using Microsoft.Win32;
using PaintDotNet;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PaintDotNet
{
    class SetupNgen
    {
        [ComImport]
        [Guid("0000010c-0000-0000-c000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersist
        {
            [PreserveSig]
            void GetClassID(out Guid pClassID);
        }

        [ComImport]
        [Guid("0000010b-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersistFile
            : IPersist
        {
            new void GetClassID(out Guid pClassID);

            [PreserveSig]
            int IsDirty();

            [PreserveSig]
            void Load(
                [MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
                uint dwMode);

            [PreserveSig]
            void Save(
                [MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
                [MarshalAs(UnmanagedType.Bool)] bool fRemember);

            [PreserveSig]
            void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

            [PreserveSig]
            void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] string ppszFileName);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct FILETIME
        {
            uint dwLowDateTime;
            uint dwHighDateTime;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WIN32_FIND_DATA
        {
            public const int MAX_PATH = 260;

            uint dwFileAttributes;
            FILETIME ftCreationTime;
            FILETIME ftLastAccessTime;
            FILETIME ftLastWriteTime;
            uint nFileSizeHight;
            uint nFileSizeLow;
            uint dwOID;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            string cFileName;
        }

        [ComImport]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IShellLink
        {
            [PreserveSig]
            void GetPath(
                [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 1)] out string pszFile,
                int cch,
                ref WIN32_FIND_DATA pfd,
                uint fFlags);

            [PreserveSig]
            void GetIDList(out IntPtr ppidl);

            [PreserveSig]
            void SetIDList(IntPtr ppidl);

            [PreserveSig]
            void GetDescription(
                [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 1)] out string pszName,
                int cch);

            [PreserveSig]
            void SetDescription(
                [MarshalAs(UnmanagedType.LPWStr)] string pszName);

            [PreserveSig]
            void GetWorkingDirectory(
                [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 1)] out string pszDir,
                int cch);

            [PreserveSig]
            void SetWorkingDirectory(
                [MarshalAs(UnmanagedType.LPWStr)] string pszDir);

            [PreserveSig]
            void GetArguments(
                [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 1)] out string pszArgs,
                int cch);

            [PreserveSig]
            void SetArguments(
                [MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

            [PreserveSig]
            void GetHotkey(out ushort pwHotkey);

            [PreserveSig]
            void SetHotkey(ushort wHotkey);

            [PreserveSig]
            void GetShowCmd(out int piShowCmd);

            [PreserveSig]
            void SetShowCmd(int iShowCmd);

            [PreserveSig]
            void GetIconLocation(
                [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 1)] out string pszIconPath,
                int cch,
                out int piIcon);

            [PreserveSig]
            void SetIconLocation(
                [MarshalAs(UnmanagedType.LPWStr)] string pszIconPath,
                int iIcon);

            [PreserveSig]
            void SetRelativePath(
                [MarshalAs(UnmanagedType.LPWStr)] string pszPathRel,
                uint dwReserved);

            [PreserveSig]
            void Resolve(
                IntPtr hwnd,
                uint fFlags);

            [PreserveSig]
            void SetPath(
                [MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }

        [GuidAttribute("00021401-0000-0000-C000-000000000046")]
        [ClassInterfaceAttribute(ClassInterfaceType.None)]
        [ComImportAttribute()]
        public class CShellLink
        {
        }

        public const int SW_SHOWNORMAL = 1;

        private static readonly string ourPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

        static void InstallAssembly(string name, bool delete, bool queue)
        {
            InstallAssembly(name, delete, queue, false);
        }

        static void InstallAssembly(string name, bool delete, bool queue, bool force32bit)
        {
            // ngen it
            if (delete)
            {
                name = Path.GetFileName(name);
                name = Path.ChangeExtension(name, null);
            }
            else
            {
                name = Path.Combine(ourPath, name);
            }

            string actionArg = delete ? "uninstall" : "install";
            string nameArg = "\"" + name + "\"";

            string queueArg = (queue && !delete) ? "/queue" : "";
            string appBaseArg = " /AppBase:\"" + ourPath + "\"";
            string optionsArg = queueArg + appBaseArg;

            string argList = actionArg + " " + nameArg + " " + optionsArg;

            string ngenExe = PaintDotNet.PdnInfo.GetNgenPath(force32bit);
            ProcessStartInfo psi1 = new ProcessStartInfo(ngenExe, argList);

            psi1.UseShellExecute = false;
            psi1.CreateNoWindow = true;
            Console.WriteLine("run:" + ngenExe + " " + argList);
            Process process1 = Process.Start(psi1);
            process1.WaitForExit();
        }

        private enum Platform
        {
            X86,
            X64,
            Unknown
        }

        internal const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;
        internal const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;
        internal const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;
        internal const ushort PROCESSOR_ARCHITECTURE_UNKNOWN = 0xFFFF;

        [StructLayout(LayoutKind.Sequential)]
        internal struct SYSTEM_INFO
        {
            public ushort wProcessorArchitecture;
            public ushort wReserved;
            public uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public UIntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort wProcessorLevel;
            public ushort wProcessorRevision;
        };

        [DllImport("kernel32.dll")]
        internal static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo);        
        
        private static Platform GetPlatform()
        {
            SYSTEM_INFO sysInfo = new SYSTEM_INFO();
            GetNativeSystemInfo(ref sysInfo);

            switch (sysInfo.wProcessorArchitecture)
            {
                case PROCESSOR_ARCHITECTURE_AMD64:
                    return Platform.X64;

                case PROCESSOR_ARCHITECTURE_INTEL:
                    return Platform.X86;

                default:
                    return Platform.Unknown;
            }
        }

        public static bool IsPathInDirectory(string path, string directory)
        {
            try
            {
                string pathCanon = path.ToLowerInvariant();
                string directoryCanon = directory.ToLowerInvariant();

                string fullPath = Path.GetFullPath(pathCanon);
                string fullDirectory = Path.GetFullPath(directoryCanon);

                bool a = (fullPath.Length > fullDirectory.Length);
                bool b = (fullPath.IndexOf(fullDirectory) == 0);
                bool c = fullPath[fullDirectory.Length] == Path.DirectorySeparatorChar;

                if (a && b && c)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                MainImpl(args);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public static void CreateShortcut(string shortcutPath, string targetPath, string workingDirectory, string description)
        {
            CShellLink cShellLink = new CShellLink();
            IShellLink iShellLink = (IShellLink)cShellLink;
            iShellLink.SetDescription(description);
            iShellLink.SetShowCmd(SW_SHOWNORMAL);
            iShellLink.SetPath(targetPath);
            iShellLink.SetWorkingDirectory(workingDirectory);
            IPersistFile iPersistFile = (IPersistFile)iShellLink;
            iPersistFile.Save(shortcutPath, false);
            Marshal.ReleaseComObject(iPersistFile);
            iPersistFile = null;
            Marshal.ReleaseComObject(iShellLink);
            iShellLink = null;
            Marshal.ReleaseComObject(cShellLink);
            cShellLink = null;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void SHGetFolderPathW(
            IntPtr hwndOwner,
            int nFolder,
            IntPtr hToken,
            uint dwFlags,
            IntPtr pszPath);

        internal static string SHGetFolderPath(int nFolder)
        {
            string pszPath = new string(' ', MAX_PATH);
            IntPtr bstr = Marshal.StringToBSTR(pszPath);
            SHGetFolderPathW(IntPtr.Zero, nFolder, IntPtr.Zero, SHGFP_TYPE_CURRENT, bstr);
            string path = Marshal.PtrToStringBSTR(bstr);
            int index = path.IndexOf('\0');
            string path2 = path.Substring(0, index);
            Marshal.FreeBSTR(bstr);
            return path2;
        }

        internal const uint SHGFP_TYPE_CURRENT = 0;
        internal const int MAX_PATH = 260; 
        internal const uint CSIDL_COMMON_STARTMENU = 0x0016;              // All Users\Start Menu
        internal const uint CSIDL_COMMON_PROGRAMS = 0x0017;               // All Users\Start Menu\Programs
        internal const uint CSIDL_COMMON_DESKTOPDIRECTORY = 0x0019;       // All Users\Desktop
        internal const uint CSIDL_PROGRAM_FILES = 0x0026;                 // C:\Program Files
        internal const uint CSIDL_FLAG_CREATE = 0x8000;                   // new for Win2K, or this in to force creation of folder

        public static string GetSpecialFolderPath(uint csidl)
        {
            string path = SHGetFolderPath((int)(csidl | CSIDL_FLAG_CREATE));
            return path;
        }

        public static void SplitArg(string arg, out string name, out string value)
        {
            const char splitChar = '=';
            string[] fields = arg.Split(splitChar);
            name = fields[0];
            value = fields[1];
        }

        public static void MainImpl(string[] args)
        {
            //System.Windows.Forms.MessageBox.Show(Environment.CommandLine);

            // Syntax:
            //     SetupNgen </cleanUpStaging | </install | /delete> DESKTOPSHORTCUT=<0|1> PDNUPDATING=<0|1> SKIPCLEANUP=<0|1> PROGRAMSGROUP=relativeName QUEUENGEN=<0|1>>

            if (args.Length >= 2 && args[0] == "/cleanUpStaging")
            {
                // SetupNgen.exe is overloaded for cleaning up the "Staging" directory
                string stagingPath = args[1];

                // Sanity check: staging directory must ALWAYS have the word Staging in it (capitalized that way too)
                // It must exist, too.
                if (Directory.Exists(stagingPath) &&
                    -1 != stagingPath.IndexOf("Staging"))
                {
                    foreach (string filePath in Directory.GetFiles(stagingPath, "*.msi"))
                    {
                        try
                        {
                            Console.WriteLine("delete: " + filePath);
                            System.IO.File.Delete(filePath);
                        }

                        catch
                        {
                        }
                    }

                    try
                    {
                        Console.WriteLine("rmdir: " + stagingPath);
                        Directory.Delete(stagingPath);
                    }

                    catch
                    {
                    }
                }
            }
            else
            {
                bool delete = false;

                if (args.Length < 5)
                {
                    return;
                }

                if (args[0] == "/delete")
                {
                    delete = true;
                }

                // otherwise we assume args[0] == "/install"

                bool queueNgen;

                if (args[5] == "QUEUENGEN=1")
                {
                    queueNgen = true;
                }
                else
                {
                    queueNgen = false;
                }

                // Pre-JIT via ngen. These are in alphabetical order.
                string[] names = 
                    new string[] 
                    {
                        "ICSharpCode.SharpZipLib.dll",
                        "PaintDotNet.Data.dll",
                        "PaintDotNet.Effects.dll",
                        "PaintDotNet.exe",
                        "PaintDotNet.Resources.dll",
                        "PaintDotNet.StylusReader.dll",
                        "PaintDotNet.SystemLayer.dll",
                        "PdnLib.dll"
                    };

                string[] names32 =
                    new string[]
                    {
                        "Interop.WIA.dll",
                        "WiaProxy32.exe"
                    };

                foreach (string name in names)
                {
                    try
                    {
                        InstallAssembly(name, delete, queueNgen, false);
                    }

                    // We don't raise a stink if ngen fails.
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

                foreach (string name in names32)
                {
                    try
                    {
                        InstallAssembly(name, delete, queueNgen, true);
                    }

                    // We don't raise a stink if ngen fails.
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

                // Create desktop shortcut
                bool createDesktopShortcut = false;
                bool updating = false;
                bool skipCleanup = false;
                
                if (args[1] == "DESKTOPSHORTCUT=1")
                {
                    createDesktopShortcut = true;
                }

                if (args[2] == "PDNUPDATING=1")
                {
                    updating = true;
                }

                if (args[3] == "SKIPCLEANUP=1")
                {
                    skipCleanup = true;
                }

                string programsShortcutGroup = string.Empty;
                const string programsGroup = "PROGRAMSGROUP";
                if (args[4].StartsWith(programsGroup + "=")) // starts with "PROGRAMSGROUP="
                {
                    programsShortcutGroup = args[4].Substring(1 + programsGroup.Length);
                }

                // Create shortcuts

                // Set up out strings
                string desktopDir = GetSpecialFolderPath(CSIDL_COMMON_DESKTOPDIRECTORY);
                string programsDir = GetSpecialFolderPath(CSIDL_COMMON_PROGRAMS);

                string linkName = PaintDotNet.PdnResources.GetString("Setup.DesktopShortcut.LinkName");
                string description = PaintDotNet.PdnResources.GetString("Setup.DesktopShortcut.Description");
                string desktopLinkPath = Path.Combine(desktopDir, linkName) + ".lnk"; // if we just use ChangeExtension it will overwrite the .NET part of Paint.NET :)
                string programsShortcutDir = Path.Combine(programsDir, programsShortcutGroup);
                string programsLinkPath = Path.Combine(programsShortcutDir, linkName) + ".lnk";
                string workingDirectory = PdnInfo.GetApplicationDir();
                string targetPath = Path.Combine(workingDirectory, "PaintDotNet.exe");

                // Desktop shortcut
                if ((delete && !skipCleanup) || (!createDesktopShortcut && skipCleanup))
                {
                    if (System.IO.File.Exists(desktopLinkPath))
                    {
                        Console.WriteLine("delete: " + desktopLinkPath);

                        try
                        {
                            System.IO.File.Delete(desktopLinkPath);
                        }

                        catch
                        {
                        }
                    }
                }
                else if (createDesktopShortcut && !delete && !updating)
                {
                    CreateShortcut(desktopLinkPath, targetPath, workingDirectory, description);
                }
                
                // Programs shortcut
                const string programsShortcutPathKey = "ProgramsShortcutPath";
                const string pdnKey = @"SOFTWARE\Paint.NET";
                    
                if (delete && !skipCleanup)
                {
                    string path = programsLinkPath;

                    // For the purposes of deleting the shortcut, we actually store the file's location in the registry
                    using (RegistryKey hklmPDN = Registry.LocalMachine.OpenSubKey(pdnKey, false))
                    {
                        if (hklmPDN != null)
                        {
                            object pathObj = hklmPDN.GetValue(programsShortcutPathKey, programsLinkPath);

                            if (pathObj is string)
                            {
                                path = (string)pathObj;
                            }
                        }
                    }

                    // Do some quick checks to make sure we don't delete something we don't want to
                    bool allowDelete = true;

                    // Verify that it is a .lnk file
                    allowDelete &= (Path.GetExtension(path) == ".lnk");

                    // Verify that it is in a subdirectory of Programs
                    allowDelete &= IsPathInDirectory(path, programsDir);

                    // Delete it
                    if (allowDelete && System.IO.File.Exists(path))
                    {
                        Console.WriteLine("delete: " + path);
                        System.IO.File.Delete(path);

                        // If we're the last shortcut, then delete that directory.
                        string dir = Path.GetDirectoryName(path);

                        try
                        {
                            System.IO.Directory.Delete(dir, false);
                        }

                        catch
                        {
                        }
                    }
                }
                else if (!delete && !updating)
                {
                    // Create it
                    if (!Directory.Exists(programsShortcutDir))
                    {
                        Directory.CreateDirectory(programsShortcutDir);
                    }

                    CreateShortcut(programsLinkPath, targetPath, workingDirectory, description);

                    // Save the location to the registry
                    using (RegistryKey hklmPDN = Registry.LocalMachine.CreateSubKey(pdnKey))                        
                    {
                        if (hklmPDN != null)
                        {
                            hklmPDN.SetValue(programsShortcutPathKey, programsLinkPath);
                        }
                    }
                }

                // Register shell extension
                const string shellExtensionName_x86 = "ShellExtension_x86.dll";
                const string shellExtensionName_x64 = "ShellExtension_x64.dll";
                const string shellExtensionGuid = "{D292F82A-50BE-4351-96CC-E86F3F8049DA}";
                const string regKeyName = @"CLSID\" + shellExtensionGuid;
                const string regKeyWow64Name = @"Wow6432Node\" + regKeyName;
                const string shellExtension_regName = "Paint.NET Shell Extension";
                const string inProcServer32 = "InProcServer32";
                const string threadingModel = "ThreadingModel";
                const string apartment = "Apartment";

                string[] shellExtensionFileNames;
                string[] regKeyNames;

                if (UIntPtr.Size == 4)
                {
                    shellExtensionFileNames = new string[1] { shellExtensionName_x86 };
                    regKeyNames = new string[1] { regKeyName };
                }
                else
                {
                    Platform platform = GetPlatform();
                    string dll64bitName = shellExtensionName_x64;

                    shellExtensionFileNames = new string[2] { dll64bitName, shellExtensionName_x86 };
                    regKeyNames = new string[2] { regKeyName, regKeyWow64Name };
                }

                string[] shellExtensionPaths = new string[shellExtensionFileNames.Length];

                for (int i = 0; i < shellExtensionFileNames.Length; ++i)
                {
                    shellExtensionPaths[i] = Path.Combine(ourPath, shellExtensionFileNames[i]);
                }

                if (!delete)
                {
                    // Register the shell extension
                    try
                    {
                        for (int i = 0; i < shellExtensionPaths.Length; ++i)
                        {
                            RegistryKey clsidKey = Registry.ClassesRoot.CreateSubKey(regKeyNames[i], RegistryKeyPermissionCheck.ReadWriteSubTree);
                            RegistryKey ips32key = clsidKey.CreateSubKey(inProcServer32);

                            clsidKey.SetValue(null, shellExtension_regName);
                            ips32key.SetValue(threadingModel, apartment);
                            ips32key.SetValue(null, shellExtensionPaths[i]);
                        }
                    }

                    catch
                    {
                    }
                }
                else
                {
                    // Unregister the shell extension
                    try
                    {
                        for (int i = 0; i < regKeyNames.Length; ++i)
                        {
                            Registry.ClassesRoot.DeleteSubKeyTree(regKeyNames[i]);
                        }
                    }

                    catch
                    {
                    }
                }
            }
        }
    }
}
