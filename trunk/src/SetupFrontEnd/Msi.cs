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
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PaintDotNet.Setup
{
    public sealed class Msi
    {
        public const string PropertyTable = "Property";
        public const string LaunchConditionTable = "LaunchCondition";
        public const string ProductCodeProperty = "ProductCode";

        public static void ThrowIfFailed(uint retVal, string message)
        {
            if (retVal != NativeConstants.ERROR_SUCCESS)
            {
                throw new Win32Exception(unchecked((int)retVal), message);
            }
        }

        public static void GetMsiProperties(string msiPath, string tableName, string[] properties, out string[] values)
        {
            values = new string[properties.Length];

            UIntPtr hDatabase = UIntPtr.Zero;
            UIntPtr hSummary = UIntPtr.Zero;

            try
            {
                uint retVal = NativeConstants.ERROR_SUCCESS;

                retVal = NativeMethods.MsiOpenDatabaseW(
                    msiPath,
                    (UIntPtr)NativeConstants.MSIDBOPEN_READONLY,
                    out hDatabase);

                ThrowIfFailed(retVal, "MsiOpenDatabaseW");

                string selectQuery = "SELECT * FROM `" + tableName + "`";

                UIntPtr hView = UIntPtr.Zero;
                retVal = NativeMethods.MsiDatabaseOpenViewW(
                    hDatabase,
                    selectQuery,
                    out hView);

                ThrowIfFailed(retVal, "MsiDatabaseOpenViewW");

                retVal = NativeMethods.MsiViewExecute(
                    hView,
                    UIntPtr.Zero);

                ThrowIfFailed(retVal, "MsiViewExecute(1)");

                // Loop through the properties and copy the ones passed in to this function
                while (true)
                {
                    UIntPtr hRecord;

                    retVal = NativeMethods.MsiViewFetch(
                        hView,
                        out hRecord);

                    if (retVal == NativeConstants.ERROR_NO_MORE_ITEMS)
                    {
                        break;
                    }

                    ThrowIfFailed(retVal, "MsiViewFetch");

                    string propertyName;

                    retVal = NativeMethods.MsiRecordGetStringW(hRecord, 1, out propertyName);
                    ThrowIfFailed(retVal, "MsiRecordGetStringW(1)");

                    int index = Array.IndexOf<string>(properties, propertyName);
                    if (index != -1)
                    {
                        retVal = NativeMethods.MsiRecordGetStringW(hRecord, 2, out values[index]);
                        ThrowIfFailed(retVal, "MsiRecordGetStringW(2)");
                    }

                    retVal = NativeMethods.MsiCloseHandle(hRecord);
                    ThrowIfFailed(retVal, "MsiCloseHandle(hRecord)");
                }

                retVal = NativeMethods.MsiCloseHandle(hView);
                ThrowIfFailed(retVal, "MsiCloseHandle");
            }

            finally
            {
                if (hDatabase != UIntPtr.Zero)
                {
                    NativeMethods.MsiCloseHandle(hDatabase);
                    hDatabase = UIntPtr.Zero;
                }
            }    
        }

        public static void SetMsiProperties(string msiPath, string tableName, string[] properties, string[] values)
        {
            if (properties.Length != values.Length)
            {
                throw new ArgumentException("properties.Length must equal values.Length");
            }
            
            UIntPtr hDatabase = UIntPtr.Zero;
            UIntPtr hSummary = UIntPtr.Zero;

            try
            {
                uint retVal = NativeConstants.ERROR_SUCCESS;

                retVal = NativeMethods.MsiOpenDatabaseW(
                    msiPath,
                    (UIntPtr)NativeConstants.MSIDBOPEN_DIRECT,
                    out hDatabase);

                ThrowIfFailed(retVal, "MsiOpenDatabaseW");

                string selectQuery = "SELECT * FROM `" + tableName + "`";

                UIntPtr hView = UIntPtr.Zero;
                retVal = NativeMethods.MsiDatabaseOpenViewW(
                    hDatabase,
                    selectQuery,
                    out hView);

                ThrowIfFailed(retVal, "MsiDatabaseOpenViewW");

                retVal = NativeMethods.MsiViewExecute(
                    hView,
                    UIntPtr.Zero);

                ThrowIfFailed(retVal, "MsiViewExecute(1)");

                // Loop through the properties and delete the ones passed in to this function
                while (true)
                {
                    UIntPtr hRecord;

                    retVal = NativeMethods.MsiViewFetch(
                        hView,
                        out hRecord);

                    if (retVal == NativeConstants.ERROR_NO_MORE_ITEMS)
                    {
                        break;
                    }

                    ThrowIfFailed(retVal, "MsiViewFetch");

                    string propertyName;

                    retVal = NativeMethods.MsiRecordGetStringW(hRecord, 1, out propertyName);
                    ThrowIfFailed(retVal, "MsiRecordGetStringW");

                    if (Array.IndexOf<string>(properties, propertyName) != -1)
                    {
                        retVal = NativeMethods.MsiViewModify(
                            hView,
                            NativeConstants.MSIMODIFY_DELETE,
                            hRecord);

                        ThrowIfFailed(retVal, "MsiViewModify");
                    }

                    retVal = NativeMethods.MsiCloseHandle(hRecord);
                    ThrowIfFailed(retVal, "MsiCloseHandle(hRecord)");
                }

                retVal = NativeMethods.MsiViewExecute(hView, UIntPtr.Zero);
                ThrowIfFailed(retVal, "MsiViewExecute(2)");

                // Loop through the properties and add them to the table
                for (int i = 0; i < properties.Length; ++i)
                {
                    if (values[i].Length == 0)
                    {
                        continue;
                    }

                    UIntPtr hRecord = NativeMethods.MsiCreateRecord(2);

                    if (hRecord == UIntPtr.Zero)
                    {
                        throw new Win32Exception("MsiCreateRecord");
                    }

                    retVal = NativeMethods.MsiRecordSetStringW(hRecord, 1, properties[i]);
                    ThrowIfFailed(retVal, "MsiRecordSetStringW(1, " + properties[i] + ")");

                    retVal = NativeMethods.MsiRecordSetStringW(hRecord, 2, values[i]);
                    ThrowIfFailed(retVal, "MsiRecordSetStringW(2, " + values[i] + ")");

                    retVal = NativeMethods.MsiViewModify(hView, NativeConstants.MSIMODIFY_INSERT, hRecord);
                    ThrowIfFailed(retVal, "MsiViewModify");

                    retVal = NativeMethods.MsiCloseHandle(hRecord);
                    ThrowIfFailed(retVal, "MsiCloseHandle(hRecord)");
                }

                retVal = NativeMethods.MsiCloseHandle(hView);
                ThrowIfFailed(retVal, "MsiCloseHandle");

                retVal = NativeMethods.MsiDatabaseCommit(hDatabase);
                ThrowIfFailed(retVal, "MsiDatabaseCommit");
            }

            finally
            {
                if (hDatabase != UIntPtr.Zero)
                {
                    NativeMethods.MsiCloseHandle(hDatabase);
                    hDatabase = UIntPtr.Zero;
                }
            }            
        }

        public static void SetMsiTargetPlatform(string msiPath, ProcessorArchitecture architecture)
        {
            const string x86 = "Intel;1033";
            const string x64 = "x64;1033";
            string platform;

            switch (architecture)
            {               
                case ProcessorArchitecture.X64:
                    platform = x64;
                    break;

                case ProcessorArchitecture.X86:
                    platform = x86;
                    break;

                default:
                    throw new InvalidOperationException("Platform must be X86 or X64 (" + architecture.ToString() + ")");
            }

            UIntPtr hDatabase = UIntPtr.Zero;
            UIntPtr hSummary = UIntPtr.Zero;

            try
            {
                uint retVal = NativeConstants.ERROR_SUCCESS;

                retVal = NativeMethods.MsiOpenDatabaseW(
                    msiPath,
                    (UIntPtr)NativeConstants.MSIDBOPEN_DIRECT,
                    out hDatabase);

                ThrowIfFailed(retVal, "MsiOpenDatabaseW");

                retVal = NativeMethods.MsiGetSummaryInformationW(
                    hDatabase,
                    null,
                    1,
                    out hSummary);

                ThrowIfFailed(retVal, "MsiGetSummaryInformationW");

                retVal = NativeMethods.MsiSummaryInfoSetPropertyW(
                    hSummary,
                    NativeConstants.PID_TEMPLATE,
                    NativeConstants.VT_LPSTR,
                    0,
                    UIntPtr.Zero,
                    platform);

                ThrowIfFailed(retVal, "MsiSummaryInfoSetPropertyW");

                retVal = NativeMethods.MsiSummaryInfoPersist(hSummary);
                ThrowIfFailed(retVal, "MsiSummaryInfoPersist");
            }

            finally
            {
                if (hSummary != UIntPtr.Zero)
                {
                    NativeMethods.MsiCloseHandle(hSummary);
                    hSummary = UIntPtr.Zero;
                }

                if (hDatabase != UIntPtr.Zero)
                {
                    NativeMethods.MsiCloseHandle(hDatabase);
                    hDatabase = UIntPtr.Zero;
                }
            }

            // Add a launch condition for x86 to prevent its installation on 64-bit Windows
            if (architecture == ProcessorArchitecture.X86)
            {
                string launchError = PdnResources.GetString("SetupWizard.x86Msi.LaunchCondition.NotAllowedOn64bitOS");

                SetMsiProperties(
                    msiPath,
                    LaunchConditionTable,
                    new string[] { "NOT VersionNT64" },
                    new string[] { launchError });
            }

            // Permute the ProductCode on non-x86 so that each platform has its own ProductCode
            string[] properties = new string[] { ProductCodeProperty };
            string[] values;
            GetMsiProperties(msiPath, PropertyTable, properties, out values);
            Guid productCode = new Guid(values[0]);
            byte[] productCodeBytes = productCode.ToByteArray();

            if (architecture == ProcessorArchitecture.X64)
            {
                unchecked
                {
                    productCodeBytes[productCodeBytes.Length - 1] += 1;
                }
            }

            Guid newProductCode = new Guid(productCodeBytes);

            if (newProductCode != productCode)
            {
                SetMsiProperties(msiPath, PropertyTable, new string[] { ProductCodeProperty }, 
                    new string[] { newProductCode.ToString("B").ToUpper() });
            }
        }
    }
}
