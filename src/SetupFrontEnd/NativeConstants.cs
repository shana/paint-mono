/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;

namespace PaintDotNet.Setup
{
    internal sealed class NativeConstants
    {
        internal const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;
        internal const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;
        internal const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;
        internal const ushort PROCESSOR_ARCHITECTURE_UNKNOWN = 0xFFFF;

        internal const uint MSIDBOPEN_READONLY = 0;      // database open read-only, no persistent changes
        internal const uint MSIDBOPEN_TRANSACT = 1;      // database read/write in transaction mode
        internal const uint MSIDBOPEN_DIRECT = 2;        // database direct read/write without transaction
        internal const uint MSIDBOPEN_CREATE = 3;        // create new database, transact mode read/write
        internal const uint MSIDBOPEN_CREATEDIRECT = 4;  // create new database, direct mode read/write

        internal const uint PID_TEMPLATE = 7;
        internal const uint VT_LPSTR = 30;
        
        internal const uint MB_ABORTRETRYIGNORE = 0x00000002;
        internal const uint MB_OK = 0x00000000;
        internal const uint MB_OKCANCEL = 0x00000001;
        internal const uint MB_RETRYCANCEL = 0x00000005;
        internal const uint MB_YESNO = 0x00000004;
        internal const uint MB_YESNOCANCEL = 0x00000003;

        internal const uint MB_ICONEXCLAMATION = 0x00000030;
        internal const uint MB_ICONWARNING = MB_ICONEXCLAMATION;
        internal const uint MB_ICONINFORMATION = MB_ICONASTERISK;
        internal const uint MB_ICONASTERISK = 0x00000040;
        internal const uint MB_ICONQUESTION = 0x00000020;
        internal const uint MB_ICONSTOP = MB_ICONHAND;
        internal const uint MB_ICONERROR = MB_ICONHAND;
        internal const uint MB_ICONHAND = 0x00000010;

        internal const uint MB_DEFBUTTON1 = 0x00000000;
        internal const uint MB_DEFBUTTON2 = 0x00000100;
        internal const uint MB_DEFBUTTON3 = 0x00000200;
        internal const uint MB_DEFBUTTON4 = 0x00000300;

        internal const uint MB_TYPEMASK = 0x0000000F;
        internal const uint MB_ICONMASK = 0x000000F0;
        internal const uint MB_DEFMASK = 0x00000F00;
        internal const uint MB_MODEMASK = 0x00003000;
        internal const uint MB_MISCMASK = 0x0000C000;

        internal const ushort LANG_NEUTRAL = 0;

        internal const int IDOK = 1;
        internal const int IDCANCEL = 2;

        internal const uint INSTALLMESSAGE_FATALEXIT = 0x00000000;          // premature termination, possibly fatal OOM
        internal const uint INSTALLMESSAGE_ERROR = 0x01000000;              // formatted error message
        internal const uint INSTALLMESSAGE_WARNING = 0x02000000;            // formatted warning message
        internal const uint INSTALLMESSAGE_USER = 0x03000000;               // user request message
        internal const uint INSTALLMESSAGE_INFO = 0x04000000;               // informative message for log
        internal const uint INSTALLMESSAGE_FILESINUSE = 0x05000000;         // list of files in use that need to be replaced
        internal const uint INSTALLMESSAGE_RESOLVESOURCE = 0x06000000;      // request to determine a valid source location
        internal const uint INSTALLMESSAGE_OUTOFDISKSPACE = 0x07000000;     // insufficient disk space message
        internal const uint INSTALLMESSAGE_ACTIONSTART = 0x08000000;        // start of action: action name & description
        internal const uint INSTALLMESSAGE_ACTIONDATA = 0x09000000;         // formatted data associated with individual action item
        internal const uint INSTALLMESSAGE_PROGRESS = 0x0A000000;           // progress gauge info: units so far, total
        internal const uint INSTALLMESSAGE_COMMONDATA = 0x0B000000;         // product info for dialog: language Id, dialog caption
        internal const uint INSTALLMESSAGE_INITIALIZE = 0x0C000000;         // sent prior to UI initialization, no string data
        internal const uint INSTALLMESSAGE_TERMINATE = 0x0D000000;          // sent after UI termination, no string data
        internal const uint INSTALLMESSAGE_SHOWDIALOG = 0x0E000000;         // sent prior to display or authored dialog or wizard

        internal const uint INSTALLLOGMODE_FATALEXIT      = ((uint)1 << (int)(INSTALLMESSAGE_FATALEXIT      >> 24));
        internal const uint INSTALLLOGMODE_ERROR          = ((uint)1 << (int)(INSTALLMESSAGE_ERROR          >> 24));
        internal const uint INSTALLLOGMODE_WARNING        = ((uint)1 << (int)(INSTALLMESSAGE_WARNING        >> 24));
        internal const uint INSTALLLOGMODE_USER           = ((uint)1 << (int)(INSTALLMESSAGE_USER           >> 24));
        internal const uint INSTALLLOGMODE_INFO           = ((uint)1 << (int)(INSTALLMESSAGE_INFO           >> 24));
        internal const uint INSTALLLOGMODE_RESOLVESOURCE  = ((uint)1 << (int)(INSTALLMESSAGE_RESOLVESOURCE  >> 24));
        internal const uint INSTALLLOGMODE_OUTOFDISKSPACE = ((uint)1 << (int)(INSTALLMESSAGE_OUTOFDISKSPACE >> 24));
        internal const uint INSTALLLOGMODE_ACTIONSTART    = ((uint)1 << (int)(INSTALLMESSAGE_ACTIONSTART    >> 24));
        internal const uint INSTALLLOGMODE_ACTIONDATA     = ((uint)1 << (int)(INSTALLMESSAGE_ACTIONDATA     >> 24));
        internal const uint INSTALLLOGMODE_COMMONDATA     = ((uint)1 << (int)(INSTALLMESSAGE_COMMONDATA     >> 24));
        internal const uint INSTALLLOGMODE_PROPERTYDUMP   = ((uint)1 << (int)(INSTALLMESSAGE_PROGRESS       >> 24)); // log only
        internal const uint INSTALLLOGMODE_VERBOSE        = ((uint)1 << (int)(INSTALLMESSAGE_INITIALIZE     >> 24)); // log only
        internal const uint INSTALLLOGMODE_EXTRADEBUG     = ((uint)1 << (int)(INSTALLMESSAGE_TERMINATE      >> 24)); // log only
        internal const uint INSTALLLOGMODE_PROGRESS       = ((uint)1 << (int)(INSTALLMESSAGE_PROGRESS       >> 24)); // external handler only
        internal const uint INSTALLLOGMODE_INITIALIZE     = ((uint)1 << (int)(INSTALLMESSAGE_INITIALIZE     >> 24)); // external handler only
        internal const uint INSTALLLOGMODE_TERMINATE      = ((uint)1 << (int)(INSTALLMESSAGE_TERMINATE      >> 24)); // external handler only
        internal const uint INSTALLLOGMODE_SHOWDIALOG     = ((uint)1 << (int)(INSTALLMESSAGE_SHOWDIALOG     >> 24)); // external handler only

        internal const uint INSTALLUILEVEL_NOCHANGE = 0;           // UI level is unchanged
        internal const uint INSTALLUILEVEL_DEFAULT  = 1;           // default UI is used
        internal const uint INSTALLUILEVEL_NONE     = 2;           // completely silent installation
        internal const uint INSTALLUILEVEL_BASIC    = 3;           // simple progress and error handling
        internal const uint INSTALLUILEVEL_REDUCED  = 4;           // authored UI; wizard dialogs suppressed
        internal const uint INSTALLUILEVEL_FULL     = 5;           // authored UI with wizards; progress; errors
        internal const uint INSTALLUILEVEL_ENDDIALOG    = 0x80;    // display success/failure dialog at end of install
        internal const uint INSTALLUILEVEL_PROGRESSONLY = 0x40;    // display only progress dialog
        internal const uint INSTALLUILEVEL_HIDECANCEL   = 0x20;    // do not display the cancel button in basic UI
        internal const uint INSTALLUILEVEL_SOURCERESONLY = 0x100;  // force display of source resolution even if quiet

        internal const int MSIMODIFY_SEEK = -1;             // reposition to current record primary key
        internal const int MSIMODIFY_REFRESH = 0;           // refetch current record data
        internal const int MSIMODIFY_INSERT = 1;            // insert new record, fails if matching key exists
        internal const int MSIMODIFY_UPDATE = 2;            // update existing non-key data of fetched record
        internal const int MSIMODIFY_ASSIGN = 3;            // insert record, replacing any existing record
        internal const int MSIMODIFY_REPLACE = 4;           // update record, delete old if primary key edit
        internal const int MSIMODIFY_MERGE = 5;             // fails if record with duplicate key not identical
        internal const int MSIMODIFY_DELETE = 6;            // remove row referenced by this record from table
        internal const int MSIMODIFY_INSERT_TEMPORARY = 7;  // insert a temporary record
        internal const int MSIMODIFY_VALIDATE = 8;          // validate a fetched record
        internal const int MSIMODIFY_VALIDATE_NEW = 9;      // validate a new record
        internal const int MSIMODIFY_VALIDATE_FIELD = 10;   // validate field(s) of an incomplete record
        internal const int MSIMODIFY_VALIDATE_DELETE = 11;  // validate before deleting record

        internal const uint FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x100;
        internal const uint FORMAT_MESSAGE_IGNORE_INSERTS = 0x200;
        internal const uint FORMAT_MESSAGE_FROM_STRING = 0x400;
        internal const uint FORMAT_MESSAGE_FROM_HMODULE = 0x800;
        internal const uint FORMAT_MESSAGE_FROM_SYSTEM = 0x1000;
        internal const uint FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x2000;

        internal const uint ERROR_SUCCESS = 0;
        internal const uint ERROR_SUCCESS_REBOOT_REQUIRED = 3010;
        internal const uint ERROR_SUCCESS_REBOOT_INITIATED = 1641;
        internal const uint ERROR_NO_MORE_ITEMS = 259;

        internal const int MAX_PATH = 260;
        internal const uint SHGFP_TYPE_CURRENT = 0;
        internal const uint SHGFP_TYPE_DEFAULT = 1;

        internal const uint CSIDL_PROGRAM_FILES = 0x0026;  // C:\Program Files
        internal const uint CSIDL_FLAG_CREATE = 0x8000;    // new for Win2K, or this in to force creation of folder

        private NativeConstants()
        {
        }
    }
}
