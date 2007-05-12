/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Runtime.InteropServices;

namespace PaintDotNet.Setup
{
    internal sealed class NativeMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern unsafe uint FormatMessageW(
            uint dwFlags,
            void *lpSource,
            uint dwMessageId,
            uint dwLanguageId,
            void *lpBuffer,
            uint nSize,
            void *pArguments);

        internal static string FormatMessageW(uint dwErrorCode)
        {
            unsafe
            {
                void *lpBuffer = null;

                uint dwResult = FormatMessageW(
                    NativeConstants.FORMAT_MESSAGE_ALLOCATE_BUFFER | NativeConstants.FORMAT_MESSAGE_FROM_SYSTEM,
                    null,
                    dwErrorCode,
                    0,
                    &lpBuffer,
                    0,
                    null);

                if (dwResult != 0)
                {
                    string message = new string((char *)lpBuffer);
                    LocalFree(new IntPtr(lpBuffer));
                    return message;
                }
                else
                {
                    return dwErrorCode.ToString();
                }
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr LocalFree(IntPtr hMem);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        internal static extern uint MsiDatabaseOpenViewW(
            UIntPtr hDatabase,
            [MarshalAs(UnmanagedType.LPWStr)] string szQuery,
            out UIntPtr phView);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        internal static extern uint MsiViewExecute(
            UIntPtr hView,
            UIntPtr hRecord);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        internal static extern uint MsiViewFetch(
            UIntPtr hView,
            out UIntPtr phRecord);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        internal static extern uint MsiRecordGetStringW(
            UIntPtr hRecord,
            uint iField,
            IntPtr szValueBuf,
            ref uint pcchValueBuf);

        internal static uint MsiRecordGetStringW(
            UIntPtr hRecord,
            uint iField,
            out string szValueBuf)
        {
            uint len = 256;
            string sz = new string(' ', (int)len);
            IntPtr bstr = Marshal.StringToBSTR(sz);
            uint retVal = MsiRecordGetStringW(hRecord, iField, bstr, ref len);

            if (retVal == NativeConstants.ERROR_SUCCESS)
            {
                szValueBuf = Marshal.PtrToStringUni(bstr);
            }
            else
            {
                szValueBuf = null;
            }

            Marshal.FreeBSTR(bstr);
            bstr = IntPtr.Zero;

            return retVal;
        }

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        internal static extern uint MsiRecordSetStringW(
            UIntPtr hRecord,
            uint iField,
            [MarshalAs(UnmanagedType.LPWStr)] string szValue);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        internal static extern UIntPtr MsiCreateRecord(uint cParams);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        internal static extern uint MsiViewModify(
            UIntPtr hView,
            int eModifyMode,
            UIntPtr hRecord);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        internal static extern uint MsiOpenDatabaseW(
            [MarshalAs(UnmanagedType.LPWStr)] string szDatabasePath,
            UIntPtr szPersist,
            out UIntPtr phDatabase);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        internal static extern uint MsiGetSummaryInformationW(
            UIntPtr hDatabase,
            [MarshalAs(UnmanagedType.LPWStr)] string szDatabasePath,
            uint uiUpdateCount,
            out UIntPtr phSummaryInfo);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        internal static extern uint MsiSummaryInfoSetPropertyW(
            UIntPtr hSummaryInfo,
            uint uiProperty,
            uint uiDataType,
            int iValue,
            UIntPtr pftValue, // set to UIntPtr.Zero
            [MarshalAs(UnmanagedType.LPWStr)] string szValue);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        internal static extern uint MsiSummaryInfoPersist(UIntPtr hSummaryInfo);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        internal static extern uint MsiDatabaseCommit(UIntPtr hDatabase);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        internal static extern uint MsiCloseHandle(UIntPtr hSummaryInfo);

        [DllImport("msi.dll")]
        internal static extern uint MsiSetInternalUI(
            uint dwUILevel,
            ref IntPtr phWnd);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.FunctionPtr)]
        internal static extern NativeDelegates.InstallUiHandler MsiSetExternalUIW(
            [MarshalAs(UnmanagedType.FunctionPtr)] NativeDelegates.InstallUiHandler puiHandler,
            uint dwMessageFilter,
            IntPtr pvContext);

        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        internal static extern uint MsiInstallProduct(
            string szPackagePath,
            string szCommandLine);

        [DllImport("shell32.dll", CharSet=CharSet.Unicode, PreserveSig=false)]
        internal static extern void SHGetFolderPathW(
            IntPtr hwndOwner,
            int nFolder,
            IntPtr hToken,
            uint dwFlags,
            IntPtr pszPath);

        internal static string SHGetFolderPath(int nFolder)
        {
            string pszPath = new string(' ', NativeConstants.MAX_PATH);
            IntPtr bstr = Marshal.StringToBSTR(pszPath);
            SHGetFolderPathW(IntPtr.Zero, nFolder, IntPtr.Zero, NativeConstants.SHGFP_TYPE_CURRENT, bstr);
            string path = Marshal.PtrToStringBSTR(bstr);
            int index = path.IndexOf('\0');
            string path2 = path.Substring(0, index);
            Marshal.FreeBSTR(bstr);
            return path2;
        }

        private NativeMethods()
        {
        }
    }
}
