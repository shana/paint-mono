/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using Microsoft.Win32.SafeHandles;
using System;

namespace PaintDotNet.SystemLayer
{
    internal static class NativeConstants
    {
        public const uint DWMWA_NCRENDERING_ENABLED = 1;           // [get] Is non-client rendering enabled/disabled
        public const uint DWMWA_NCRENDERING_POLICY = 2;            // [set] Non-client rendering policy
        public const uint DWMWA_TRANSITIONS_FORCEDISABLED = 3;     // [set] Potentially enable/forcibly disable transitions
        public const uint DWMWA_ALLOW_NCPAINT = 4;                 // [set] Allow contents rendered in the non-client area to be visible on the DWM-drawn frame.
        public const uint DWMWA_CAPTION_BUTTON_BOUNDS = 5;         // [get] Bounds of the caption button area in window-relative space.
        public const uint DWMWA_NONCLIENT_RTL_LAYOUT = 6;          // [set] Is non-client content RTL mirrored
        public const uint DWMWA_FORCE_ICONIC_REPRESENTATION = 7;   // [set] Force this window to display iconic thumbnails.
        public const uint DWMWA_FLIP3D_POLICY = 8;                 // [set] Designates how Flip3D will treat the window.
        public const uint DWMWA_EXTENDED_FRAME_BOUNDS = 9;         // [get] Gets the extended frame bounds rectangle in screen space
        public const uint DWMWA_LAST = 10;

        public const uint DWMNCRP_USEWINDOWSTYLE = 0;
        public const uint DWMNCRP_DISABLED = 1;
        public const uint DWMNCRP_ENABLED = 2;
        public const uint DWMNCRP_LAST = 3;

        internal const byte VER_EQUAL = 1;
        internal const byte VER_GREATER = 2;
        internal const byte VER_GREATER_EQUAL = 3;
        internal const byte VER_LESS = 4;
        internal const byte VER_LESS_EQUAL = 5;
        internal const byte VER_AND = 6;
        internal const byte VER_OR = 7;

        internal const uint VER_CONDITION_MASK = 7;
        internal const uint VER_NUM_BITS_PER_CONDITION_MASK = 3;

        internal const uint VER_MINORVERSION = 0x0000001;
        internal const uint VER_MAJORVERSION = 0x0000002;
        internal const uint VER_BUILDNUMBER = 0x0000004;
        internal const uint VER_PLATFORMID = 0x0000008;
        internal const uint VER_SERVICEPACKMINOR = 0x0000010;
        internal const uint VER_SERVICEPACKMAJOR = 0x0000020;
        internal const uint VER_SUITENAME = 0x0000040;
        internal const uint VER_PRODUCT_TYPE = 0x0000080;

        internal const uint VER_PLATFORM_WIN32s = 0;
        internal const uint VER_PLATFORM_WIN32_WINDOWS = 1;
        internal const uint VER_PLATFORM_WIN32_NT = 2;

        internal const int THREAD_MODE_BACKGROUND_BEGIN = 0x10000;
        internal const int THREAD_MODE_BACKGROUND_END = 0x20000;

        private static uint CTL_CODE(uint deviceType, uint function, uint method, uint access)
        {
            return (deviceType << 16) | (access << 14) | (function << 2) | method;
        }

        internal const uint FILE_DEVICE_FILE_SYSTEM = 0x00000009;
        internal const uint METHOD_BUFFERED = 0;

        internal static readonly uint FSCTL_SET_COMPRESSION =
            CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 16, METHOD_BUFFERED, FILE_READ_DATA | FILE_WRITE_DATA);

        internal static ushort COMPRESSION_FORMAT_DEFAULT = 1;

        internal const int SW_HIDE = 0;
        internal const int SW_SHOWNORMAL = 1;
        internal const int SW_NORMAL = 1;
        internal const int SW_SHOWMINIMIZED = 2;
        internal const int SW_SHOWMAXIMIZED = 3;
        internal const int SW_MAXIMIZE = 3;
        internal const int SW_SHOWNOACTIVATE = 4;
        internal const int SW_SHOW = 5;
        internal const int SW_MINIMIZE = 6;
        internal const int SW_SHOWMINNOACTIVE = 7;
        internal const int SW_SHOWNA = 8;
        internal const int SW_RESTORE = 9;
        internal const int SW_SHOWDEFAULT = 10;
        internal const int SW_FORCEMINIMIZE = 11;
        internal const int SW_MAX = 11;

        internal const uint SEE_MASK_CLASSNAME = 0x00000001;
        internal const uint SEE_MASK_CLASSKEY = 0x00000003;
        internal const uint SEE_MASK_IDLIST = 0x00000004;
        internal const uint SEE_MASK_INVOKEIDLIST = 0x0000000c;
        internal const uint SEE_MASK_ICON = 0x00000010;
        internal const uint SEE_MASK_HOTKEY = 0x00000020;
        internal const uint SEE_MASK_NOCLOSEPROCESS = 0x00000040;
        internal const uint SEE_MASK_CONNECTNETDRV = 0x00000080;
        internal const uint SEE_MASK_FLAG_DDEWAIT = 0x00000100;
        internal const uint SEE_MASK_DOENVSUBST = 0x00000200;
        internal const uint SEE_MASK_FLAG_NO_UI = 0x00000400;
        internal const uint SEE_MASK_UNICODE = 0x00004000;
        internal const uint SEE_MASK_NO_CONSOLE = 0x00008000;
        internal const uint SEE_MASK_ASYNCOK = 0x00100000;
        internal const uint SEE_MASK_HMONITOR = 0x00200000;
        internal const uint SEE_MASK_NOZONECHECKS = 0x00800000;
        internal const uint SEE_MASK_NOQUERYCLASSSTORE = 0x01000000;
        internal const uint SEE_MASK_WAITFORINPUTIDLE = 0x02000000;
        internal const uint SEE_MASK_FLAG_LOG_USAGE = 0x04000000;

        internal const uint SHARD_PIDL = 0x00000001;
        internal const uint SHARD_PATHA = 0x00000002;
        internal const uint SHARD_PATHW = 0x00000003;

        internal const uint VER_NT_WORKSTATION = 0x0000001;
        internal const uint VER_NT_DOMAIN_CONTROLLER = 0x0000002;
        internal const uint VER_NT_SERVER = 0x0000003;

        internal const uint LWA_COLORKEY = 0x00000001;
        internal const uint LWA_ALPHA = 0x00000002;
        internal const uint WS_EX_LAYERED = 0x00080000;

        internal const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;
        internal const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;
        internal const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;
        internal const ushort PROCESSOR_ARCHITECTURE_UNKNOWN = 0xFFFF;

        internal const uint SHVIEW_THUMBNAIL = 0x702d;

        internal const uint MA_ACTIVATE = 1;
        internal const uint MA_ACTIVATEANDEAT = 2;
        internal const uint MA_NOACTIVATE = 3;
        internal const uint MA_NOACTIVATEANDEAT = 4;

        internal const uint IDI_APPLICATION = 32512;

        internal const int ERROR_SUCCESS = 0;
        internal const int ERROR_ALREADY_EXISTS = 183;
        internal const int ERROR_CANCELLED = 1223;
        internal const int ERROR_IO_PENDING = 0x3e5;
        internal const int ERROR_NO_MORE_ITEMS = 259;

        internal const uint DIGCF_PRESENT = 2;

        internal const int GWL_STYLE = -16;
        internal const int GWL_EXSTYLE = -20;

        internal const int GWLP_WNDPROC = -4;
        internal const int GWLP_HINSTANCE = -6;
        internal const int GWLP_HWNDPARENT = -8;
        internal const int GWLP_USERDATA = -21;
        internal const int GWLP_ID = -12;

        internal const uint PBS_SMOOTH = 0x01;
        internal const uint PBS_MARQUEE = 0x08;
        internal const int PBM_SETMARQUEE = WM_USER + 10;

        internal const int SBM_SETPOS = 0x00E0;
        internal const int SBM_SETRANGE = 0x00E2;
        internal const int SBM_SETRANGEREDRAW = 0x00E6;
        internal const int SBM_SETSCROLLINFO = 0x00E9;

        internal const int BCM_FIRST = 0x1600;
        internal const int BCM_SETSHIELD = BCM_FIRST + 0x000C;

        internal const int CB_SHOWDROPDOWN = 0x014f;

        internal const uint WM_COMMAND = 0x111;
        internal const uint WM_MOUSEACTIVATE = 0x21;
        internal const uint WM_COPYDATA = 0x004a;

        internal const uint SMTO_NORMAL = 0x0000;
        internal const uint SMTO_BLOCK = 0x0001;
        internal const uint SMTO_ABORTIFHUNG = 0x0002;
        internal const uint SMTO_NOTIMEOUTIFNOTHUNG = 0x0008;

        internal const int WM_USER = 0x400;
        internal const int WM_HSCROLL = 0x114;
        internal const int WM_VSCROLL = 0x115;
        internal const int WM_SETFOCUS = 7;
        internal const int WM_QUERYENDSESSION = 0x0011;
        internal const int WM_ACTIVATE = 0x006;
        internal const int WM_ACTIVATEAPP = 0x01C;
        internal const int WM_PAINT = 0x000f;
        internal const int WM_NCPAINT = 0x0085;
        internal const int WM_NCACTIVATE = 0x086;
        internal const int WM_SETREDRAW = 0x000B;

        internal const uint WS_VSCROLL = 0x00200000;
        internal const uint WS_HSCROLL = 0x00100000;

        internal const uint BS_MULTILINE = 0x00002000;

        internal const uint ANSI_CHARSET = 0;
        internal const uint DEFAULT_CHARSET = 1;
        internal const uint SYMBOL_CHARSET = 2;
        internal const uint SHIFTJIS_CHARSET = 128;
        internal const uint HANGEUL_CHARSET = 129;
        internal const uint HANGUL_CHARSET = 129;
        internal const uint GB2312_CHARSET = 134;
        internal const uint CHINESEBIG5_CHARSET = 136;
        internal const uint OEM_CHARSET = 255;
        internal const uint JOHAB_CHARSET = 130;
        internal const uint HEBREW_CHARSET = 177;
        internal const uint ARABIC_CHARSET = 178;
        internal const uint GREEK_CHARSET = 161;
        internal const uint TURKISH_CHARSET = 162;
        internal const uint VIETNAMESE_CHARSET = 163;
        internal const uint THAI_CHARSET = 222;
        internal const uint EASTEUROPE_CHARSET = 238;
        internal const uint RUSSIAN_CHARSET = 204;
        internal const uint MAC_CHARSET = 77;
        internal const uint BALTIC_CHARSET = 186;

        internal const uint SPI_GETBEEP = 0x0001;
        internal const uint SPI_SETBEEP = 0x0002;
        internal const uint SPI_GETMOUSE = 0x0003;
        internal const uint SPI_SETMOUSE = 0x0004;
        internal const uint SPI_GETBORDER = 0x0005;
        internal const uint SPI_SETBORDER = 0x0006;
        internal const uint SPI_GETKEYBOARDSPEED = 0x000A;
        internal const uint SPI_SETKEYBOARDSPEED = 0x000B;
        internal const uint SPI_LANGDRIVER = 0x000C;
        internal const uint SPI_ICONHORIZONTALSPACING = 0x000D;
        internal const uint SPI_GETSCREENSAVETIMEOUT = 0x000E;
        internal const uint SPI_SETSCREENSAVETIMEOUT = 0x000F;
        internal const uint SPI_GETSCREENSAVEACTIVE = 0x0010;
        internal const uint SPI_SETSCREENSAVEACTIVE = 0x0011;
        internal const uint SPI_GETGRIDGRANULARITY = 0x0012;
        internal const uint SPI_SETGRIDGRANULARITY = 0x0013;
        internal const uint SPI_SETDESKWALLPAPER = 0x0014;
        internal const uint SPI_SETDESKPATTERN = 0x0015;
        internal const uint SPI_GETKEYBOARDDELAY = 0x0016;
        internal const uint SPI_SETKEYBOARDDELAY = 0x0017;
        internal const uint SPI_ICONVERTICALSPACING = 0x0018;
        internal const uint SPI_GETICONTITLEWRAP = 0x0019;
        internal const uint SPI_SETICONTITLEWRAP = 0x001A;
        internal const uint SPI_GETMENUDROPALIGNMENT = 0x001B;
        internal const uint SPI_SETMENUDROPALIGNMENT = 0x001C;
        internal const uint SPI_SETDOUBLECLKWIDTH = 0x001D;
        internal const uint SPI_SETDOUBLECLKHEIGHT = 0x001E;
        internal const uint SPI_GETICONTITLELOGFONT = 0x001F;
        internal const uint SPI_SETDOUBLECLICKTIME = 0x0020;
        internal const uint SPI_SETMOUSEBUTTONSWAP = 0x0021;
        internal const uint SPI_SETICONTITLELOGFONT = 0x0022;
        internal const uint SPI_GETFASTTASKSWITCH = 0x0023;
        internal const uint SPI_SETFASTTASKSWITCH = 0x0024;
        internal const uint SPI_SETDRAGFULLWINDOWS = 0x0025;
        internal const uint SPI_GETDRAGFULLWINDOWS = 0x0026;
        internal const uint SPI_GETNONCLIENTMETRICS = 0x0029;
        internal const uint SPI_SETNONCLIENTMETRICS = 0x002A;
        internal const uint SPI_GETMINIMIZEDMETRICS = 0x002B;
        internal const uint SPI_SETMINIMIZEDMETRICS = 0x002C;
        internal const uint SPI_GETICONMETRICS = 0x002D;
        internal const uint SPI_SETICONMETRICS = 0x002E;
        internal const uint SPI_SETWORKAREA = 0x002F;
        internal const uint SPI_GETWORKAREA = 0x0030;
        internal const uint SPI_SETPENWINDOWS = 0x0031;
        internal const uint SPI_GETHIGHCONTRAST = 0x0042;
        internal const uint SPI_SETHIGHCONTRAST = 0x0043;
        internal const uint SPI_GETKEYBOARDPREF = 0x0044;
        internal const uint SPI_SETKEYBOARDPREF = 0x0045;
        internal const uint SPI_GETSCREENREADER = 0x0046;
        internal const uint SPI_SETSCREENREADER = 0x0047;
        internal const uint SPI_GETANIMATION = 0x0048;
        internal const uint SPI_SETANIMATION = 0x0049;
        internal const uint SPI_GETFONTSMOOTHING = 0x004A;
        internal const uint SPI_SETFONTSMOOTHING = 0x004B;
        internal const uint SPI_SETDRAGWIDTH = 0x004C;
        internal const uint SPI_SETDRAGHEIGHT = 0x004D;
        internal const uint SPI_SETHANDHELD = 0x004E;
        internal const uint SPI_GETLOWPOWERTIMEOUT = 0x004F;
        internal const uint SPI_GETPOWEROFFTIMEOUT = 0x0050;
        internal const uint SPI_SETLOWPOWERTIMEOUT = 0x0051;
        internal const uint SPI_SETPOWEROFFTIMEOUT = 0x0052;
        internal const uint SPI_GETLOWPOWERACTIVE = 0x0053;
        internal const uint SPI_GETPOWEROFFACTIVE = 0x0054;
        internal const uint SPI_SETLOWPOWERACTIVE = 0x0055;
        internal const uint SPI_SETPOWEROFFACTIVE = 0x0056;
        internal const uint SPI_SETCURSORS = 0x0057;
        internal const uint SPI_SETICONS = 0x0058;
        internal const uint SPI_GETDEFAULTINPUTLANG = 0x0059;
        internal const uint SPI_SETDEFAULTINPUTLANG = 0x005A;
        internal const uint SPI_SETLANGTOGGLE = 0x005B;
        internal const uint SPI_GETWINDOWSEXTENSION = 0x005C;
        internal const uint SPI_SETMOUSETRAILS = 0x005D;
        internal const uint SPI_GETMOUSETRAILS = 0x005E;
        internal const uint SPI_SETSCREENSAVERRUNNING = 0x0061;
        internal const uint SPI_SCREENSAVERRUNNING = SPI_SETSCREENSAVERRUNNING;
        internal const uint SPI_GETFILTERKEYS = 0x0032;
        internal const uint SPI_SETFILTERKEYS = 0x0033;
        internal const uint SPI_GETTOGGLEKEYS = 0x0034;
        internal const uint SPI_SETTOGGLEKEYS = 0x0035;
        internal const uint SPI_GETMOUSEKEYS = 0x0036;
        internal const uint SPI_SETMOUSEKEYS = 0x0037;
        internal const uint SPI_GETSHOWSOUNDS = 0x0038;
        internal const uint SPI_SETSHOWSOUNDS = 0x0039;
        internal const uint SPI_GETSTICKYKEYS = 0x003A;
        internal const uint SPI_SETSTICKYKEYS = 0x003B;
        internal const uint SPI_GETACCESSTIMEOUT = 0x003C;
        internal const uint SPI_SETACCESSTIMEOUT = 0x003D;
        internal const uint SPI_GETSERIALKEYS = 0x003E;
        internal const uint SPI_SETSERIALKEYS = 0x003F;
        internal const uint SPI_GETSOUNDSENTRY = 0x0040;
        internal const uint SPI_SETSOUNDSENTRY = 0x0041;
        internal const uint SPI_GETSNAPTODEFBUTTON = 0x005F;
        internal const uint SPI_SETSNAPTODEFBUTTON = 0x0060;
        internal const uint SPI_GETMOUSEHOVERWIDTH = 0x0062;
        internal const uint SPI_SETMOUSEHOVERWIDTH = 0x0063;
        internal const uint SPI_GETMOUSEHOVERHEIGHT = 0x0064;
        internal const uint SPI_SETMOUSEHOVERHEIGHT = 0x0065;
        internal const uint SPI_GETMOUSEHOVERTIME = 0x0066;
        internal const uint SPI_SETMOUSEHOVERTIME = 0x0067;
        internal const uint SPI_GETWHEELSCROLLLINES = 0x0068;
        internal const uint SPI_SETWHEELSCROLLLINES = 0x0069;
        internal const uint SPI_GETMENUSHOWDELAY = 0x006A;
        internal const uint SPI_SETMENUSHOWDELAY = 0x006B;
        internal const uint SPI_GETSHOWIMEUI = 0x006E;
        internal const uint SPI_SETSHOWIMEUI = 0x006F;
        internal const uint SPI_GETMOUSESPEED = 0x0070;
        internal const uint SPI_SETMOUSESPEED = 0x0071;
        internal const uint SPI_GETSCREENSAVERRUNNING = 0x0072;
        internal const uint SPI_GETDESKWALLPAPER = 0x0073;
        internal const uint SPI_GETACTIVEWINDOWTRACKING = 0x1000;
        internal const uint SPI_SETACTIVEWINDOWTRACKING = 0x1001;
        internal const uint SPI_GETMENUANIMATION = 0x1002;
        internal const uint SPI_SETMENUANIMATION = 0x1003;
        internal const uint SPI_GETCOMBOBOXANIMATION = 0x1004;
        internal const uint SPI_SETCOMBOBOXANIMATION = 0x1005;
        internal const uint SPI_GETLISTBOXSMOOTHSCROLLING = 0x1006;
        internal const uint SPI_SETLISTBOXSMOOTHSCROLLING = 0x1007;
        internal const uint SPI_GETGRADIENTCAPTIONS = 0x1008;
        internal const uint SPI_SETGRADIENTCAPTIONS = 0x1009;
        internal const uint SPI_GETKEYBOARDCUES = 0x100A;
        internal const uint SPI_SETKEYBOARDCUES = 0x100B;
        internal const uint SPI_GETMENUUNDERLINES = SPI_GETKEYBOARDCUES;
        internal const uint SPI_SETMENUUNDERLINES = SPI_SETKEYBOARDCUES;
        internal const uint SPI_GETACTIVEWNDTRKZORDER = 0x100C;
        internal const uint SPI_SETACTIVEWNDTRKZORDER = 0x100D;
        internal const uint SPI_GETHOTTRACKING = 0x100E;
        internal const uint SPI_SETHOTTRACKING = 0x100F;
        internal const uint SPI_GETMENUFADE = 0x1012;
        internal const uint SPI_SETMENUFADE = 0x1013;
        internal const uint SPI_GETSELECTIONFADE = 0x1014;
        internal const uint SPI_SETSELECTIONFADE = 0x1015;
        internal const uint SPI_GETTOOLTIPANIMATION = 0x1016;
        internal const uint SPI_SETTOOLTIPANIMATION = 0x1017;
        internal const uint SPI_GETTOOLTIPFADE = 0x1018;
        internal const uint SPI_SETTOOLTIPFADE = 0x1019;
        internal const uint SPI_GETCURSORSHADOW = 0x101A;
        internal const uint SPI_SETCURSORSHADOW = 0x101B;
        internal const uint SPI_GETMOUSESONAR = 0x101C;
        internal const uint SPI_SETMOUSESONAR = 0x101D;
        internal const uint SPI_GETMOUSECLICKLOCK = 0x101E;
        internal const uint SPI_SETMOUSECLICKLOCK = 0x101F;
        internal const uint SPI_GETMOUSEVANISH = 0x1020;
        internal const uint SPI_SETMOUSEVANISH = 0x1021;
        internal const uint SPI_GETFLATMENU = 0x1022;
        internal const uint SPI_SETFLATMENU = 0x1023;
        internal const uint SPI_GETDROPSHADOW = 0x1024;
        internal const uint SPI_SETDROPSHADOW = 0x1025;
        internal const uint SPI_GETBLOCKSENDINPUTRESETS = 0x1026;
        internal const uint SPI_SETBLOCKSENDINPUTRESETS = 0x1027;
        internal const uint SPI_GETUIEFFECTS = 0x103E;
        internal const uint SPI_SETUIEFFECTS = 0x103F;
        internal const uint SPI_GETFOREGROUNDLOCKTIMEOUT = 0x2000;
        internal const uint SPI_SETFOREGROUNDLOCKTIMEOUT = 0x2001;
        internal const uint SPI_GETACTIVEWNDTRKTIMEOUT = 0x2002;
        internal const uint SPI_SETACTIVEWNDTRKTIMEOUT = 0x2003;
        internal const uint SPI_GETFOREGROUNDFLASHCOUNT = 0x2004;
        internal const uint SPI_SETFOREGROUNDFLASHCOUNT = 0x2005;
        internal const uint SPI_GETCARETWIDTH = 0x2006;
        internal const uint SPI_SETCARETWIDTH = 0x2007;
        internal const uint SPI_GETMOUSECLICKLOCKTIME = 0x2008;
        internal const uint SPI_SETMOUSECLICKLOCKTIME = 0x2009;
        internal const uint SPI_GETFONTSMOOTHINGTYPE = 0x200A;
        internal const uint SPI_SETFONTSMOOTHINGTYPE = 0x200B;
        internal const uint SPI_GETFONTSMOOTHINGCONTRAST = 0x200C;
        internal const uint SPI_SETFONTSMOOTHINGCONTRAST = 0x200D;
        internal const uint SPI_GETFOCUSBORDERWIDTH = 0x200E;
        internal const uint SPI_SETFOCUSBORDERWIDTH = 0x200F;
        internal const uint SPI_GETFOCUSBORDERHEIGHT = 0x2010;
        internal const uint SPI_SETFOCUSBORDERHEIGHT = 0x2011;
        internal const uint SPI_GETFONTSMOOTHINGORIENTATION = 0x2012;
        internal const uint SPI_SETFONTSMOOTHINGORIENTATION = 0x2013;

        internal const uint INFINITE = 0xffffffff;
        internal const uint STATUS_WAIT_0 = 0;
        internal const uint STATUS_ABANDONED_WAIT_0 = 0x80;
        internal const uint WAIT_FAILED = 0xffffffff;
        internal const uint WAIT_TIMEOUT = 258;
        internal const uint WAIT_ABANDONED = STATUS_ABANDONED_WAIT_0 + 0;
        internal const uint WAIT_OBJECT_0 = STATUS_WAIT_0 + 0;
        internal const uint WAIT_ABANDONED_0 = STATUS_ABANDONED_WAIT_0 + 0;
        internal const uint STATUS_USER_APC = 0x000000C0;
        internal const uint WAIT_IO_COMPLETION = STATUS_USER_APC;

        internal const int SM_REMOTESESSION = 0x1000;
        internal const int WM_WTSSESSION_CHANGE = 0x2b1;
        internal const int WM_MOVING = 0x0216;
        internal const uint NOTIFY_FOR_ALL_SESSIONS = 1;
        internal const uint NOTIFY_FOR_THIS_SESSION = 0;

        internal const int BP_PUSHBUTTON = 1;
        internal const int PBS_NORMAL = 1;
        internal const int PBS_HOT = 2;
        internal const int PBS_PRESSED = 3;
        internal const int PBS_DISABLED = 4;
        internal const int PBS_DEFAULTED = 5;

        internal const int PS_SOLID = 0;
        internal const int PS_DASH = 1;             /* -------  */
        internal const int PS_DOT = 2;              /* .......  */
        internal const int PS_DASHDOT = 3;          /* _._._._  */
        internal const int PS_DASHDOTDOT = 4;       /* _.._.._  */
        internal const int PS_NULL = 5;
        internal const int PS_INSIDEFRAME = 6;
        internal const int PS_USERSTYLE = 7;
        internal const int PS_ALTERNATE = 8;

        internal const int PS_ENDCAP_ROUND = 0x00000000;
        internal const int PS_ENDCAP_SQUARE = 0x00000100;
        internal const int PS_ENDCAP_FLAT = 0x00000200;
        internal const int PS_ENDCAP_MASK = 0x00000F00;

        internal const int PS_JOIN_ROUND = 0x00000000;
        internal const int PS_JOIN_BEVEL = 0x00001000;
        internal const int PS_JOIN_MITER = 0x00002000;
        internal const int PS_JOIN_MASK = 0x0000F000;

        internal const int PS_COSMETIC = 0x00000000;
        internal const int PS_GEOMETRIC = 0x00010000;
        internal const int PS_TYPE_MASK = 0x000F0000;

        internal const int BS_SOLID = 0;
        internal const int BS_NULL = 1;
        internal const int BS_HOLLOW = BS_NULL;
        internal const int BS_HATCHED = 2;
        internal const int BS_PATTERN = 3;
        internal const int BS_INDEXED = 4;
        internal const int BS_DIBPATTERN = 5;
        internal const int BS_DIBPATTERNPT = 6;
        internal const int BS_PATTERN8X8 = 7;
        internal const int BS_DIBPATTERN8X8 = 8;
        internal const int BS_MONOPATTERN = 9;

        internal const uint SRCCOPY = 0x00CC0020;     /* dest = source  */
        internal const uint SRCPAINT = 0x00EE0086;    /* dest = source OR dest */
        internal const uint SRCAND = 0x008800C6;      /* dest = source AND dest */
        internal const uint SRCINVERT = 0x00660046;   /* dest = source XOR dest */
        internal const uint SRCERASE = 0x00440328;    /* dest = source AND (NOT dest ) */
        internal const uint NOTSRCCOPY = 0x00330008;  /* dest = (NOT source) */
        internal const uint NOTSRCERASE = 0x001100A6; /* dest = (NOT src) AND (NOT dest) */
        internal const uint MERGECOPY = 0x00C000CA;   /* dest = (source AND pattern) */
        internal const uint MERGEPAINT = 0x00BB0226;  /* dest = (NOT source) OR dest */
        internal const uint PATCOPY = 0x00F00021;     /* dest = pattern  */
        internal const uint PATPAINT = 0x00FB0A09;    /* dest = DPSnoo  */
        internal const uint PATINVERT = 0x005A0049;   /* dest = pattern XOR dest */
        internal const uint DSTINVERT = 0x00550009;   /* dest = (NOT dest) */
        internal const uint BLACKNESS = 0x00000042;   /* dest = BLACK  */
        internal const uint WHITENESS = 0x00FF0062;   /* dest = WHITE  */

        internal const uint NOMIRRORBITMAP = 0x80000000; /* Do not Mirror the bitmap in this call */
        internal const uint CAPTUREBLT = 0x40000000;     /* Include layered windows */

        // StretchBlt() Modes
        internal const int BLACKONWHITE = 1;
        internal const int WHITEONBLACK = 2;
        internal const int COLORONCOLOR = 3;
        internal const int HALFTONE = 4;
        internal const int MAXSTRETCHBLTMODE = 4;

        internal const int HeapCompatibilityInformation = 0;
        internal const uint HEAP_NO_SERIALIZE = 0x00000001;
        internal const uint HEAP_GROWABLE = 0x00000002;
        internal const uint HEAP_GENERATE_EXCEPTIONS = 0x00000004;
        internal const uint HEAP_ZERO_MEMORY = 0x00000008;
        internal const uint HEAP_REALLOC_IN_PLACE_ONLY = 0x00000010;
        internal const uint HEAP_TAIL_CHECKING_ENABLED = 0x00000020;
        internal const uint HEAP_FREE_CHECKING_ENABLED = 0x00000040;
        internal const uint HEAP_DISABLE_COALESCE_ON_FREE = 0x00000080;
        internal const uint HEAP_CREATE_ALIGN_16 = 0x00010000;
        internal const uint HEAP_CREATE_ENABLE_TRACING = 0x00020000;
        internal const uint HEAP_MAXIMUM_TAG = 0x0FFF;
        internal const uint HEAP_PSEUDO_TAG_FLAG = 0x8000;
        internal const uint HEAP_TAG_SHIFT = 18;

        internal const int SM_TABLETPC = 86;

        internal const uint MONITOR_DEFAULTTONULL = 0x00000000;
        internal const uint MONITOR_DEFAULTTOPRIMARY = 0x00000001;
        internal const uint MONITOR_DEFAULTTONEAREST = 0x00000002;

        internal const uint WTD_UI_ALL = 1;
        internal const uint WTD_UI_NONE = 2;
        internal const uint WTD_UI_NOBAD = 3;
        internal const uint WTD_UI_NOGOOD = 4;

        internal const uint WTD_REVOKE_NONE = 0;
        internal const uint WTD_REVOKE_WHOLECHAIN = 1;

        internal const uint WTD_CHOICE_FILE = 1;
        internal const uint WTD_CHOICE_CATALOG = 2;
        internal const uint WTD_CHOICE_BLOB = 3;
        internal const uint WTD_CHOICE_SIGNER = 4;
        internal const uint WTD_CHOICE_CERT = 5;

        internal const uint WTD_STATEACTION_IGNORE = 0;
        internal const uint WTD_STATEACTION_VERIFY = 1;
        internal const uint WTD_STATEACTION_CLOSE = 2;
        internal const uint WTD_STATEACTION_AUTO_CACHE = 3;
        internal const uint WTD_STATEACTION_AUTO_CACHE_FLUSH = 4;

        internal const uint WTD_PROV_FLAGS_MASK = 0x0000FFFF;
        internal const uint WTD_USE_IE4_TRUST_FLAG = 0x00000001;
        internal const uint WTD_NO_IE4_CHAIN_FLAG = 0x00000002;
        internal const uint WTD_NO_POLICY_USAGE_FLAG = 0x00000004;
        internal const uint WTD_REVOCATION_CHECK_NONE = 0x00000010;
        internal const uint WTD_REVOCATION_CHECK_END_CERT = 0x00000020;
        internal const uint WTD_REVOCATION_CHECK_CHAIN = 0x00000040;
        internal const uint WTD_REVOCATION_CHECK_CHAIN_EXCLUDE_ROOT = 0x00000080;
        internal const uint WTD_SAFER_FLAG = 0x00000100;
        internal const uint WTD_HASH_ONLY_FLAG = 0x00000200;
        internal const uint WTD_USE_DEFAULT_OSVER_CHECK = 0x00000400;
        internal const uint WTD_LIFETIME_SIGNING_FLAG = 0x00000800;
        internal const uint WTD_CACHE_ONLY_URL_RETRIEVAL = 0x00001000;

        internal static readonly Guid WINTRUST_ACTION_GENERIC_VERIFY_V2 =
            new Guid(0xaac56b, 0xcd44, 0x11d0, 0x8c, 0xc2, 0x0, 0xc0, 0x4f, 0xc2, 0x95, 0xee);

        internal const uint FILE_SHARE_READ = 0x00000001;
        internal const uint FILE_SHARE_WRITE = 0x00000002;
        internal const uint FILE_SHARE_DELETE = 0x00000004;

        internal const uint FILE_READ_DATA = 0x0001;
        internal const uint FILE_LIST_DIRECTORY = 0x0001;
        internal const uint FILE_WRITE_DATA = 0x0002;
        internal const uint FILE_ADD_FILE = 0x0002;
        internal const uint FILE_APPEND_DATA = 0x0004;
        internal const uint FILE_ADD_SUBDIRECTORY = 0x0004;
        internal const uint FILE_CREATE_PIPE_INSTANCE = 0x0004;

        internal const uint FILE_READ_EA = 0x0008;
        internal const uint FILE_WRITE_EA = 0x0010;
        internal const uint FILE_EXECUTE = 0x0020;
        internal const uint FILE_TRAVERSE = 0x0020;
        internal const uint FILE_DELETE_CHILD = 0x0040;
        internal const uint FILE_READ_ATTRIBUTES = 0x0080;
        internal const uint FILE_WRITE_ATTRIBUTES = 0x0100;
        internal const uint FILE_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | SYNCHRONIZE | 0x1FF);
        internal const uint FILE_GENERIC_READ = (STANDARD_RIGHTS_READ | FILE_READ_DATA | FILE_READ_ATTRIBUTES | FILE_READ_EA | SYNCHRONIZE);
        internal const uint FILE_GENERIC_WRITE = (STANDARD_RIGHTS_WRITE | FILE_WRITE_DATA | FILE_WRITE_ATTRIBUTES | FILE_WRITE_EA | FILE_APPEND_DATA | SYNCHRONIZE);
        internal const uint FILE_GENERIC_EXECUTE = (STANDARD_RIGHTS_EXECUTE | FILE_READ_ATTRIBUTES | FILE_EXECUTE | SYNCHRONIZE);

        internal const uint READ_CONTROL = 0x00020000;
        internal const uint SYNCHRONIZE = 0x00100000;
        internal const uint STANDARD_RIGHTS_READ = READ_CONTROL;
        internal const uint STANDARD_RIGHTS_WRITE = READ_CONTROL;
        internal const uint STANDARD_RIGHTS_EXECUTE = READ_CONTROL;
        internal const uint STANDARD_RIGHTS_REQUIRED = 0x000F0000;

        internal const uint GENERIC_READ = 0x80000000;
        internal const uint GENERIC_WRITE = 0x40000000;
        internal const uint GENERIC_EXECUTE = 0x20000000;

        internal const uint CREATE_NEW = 1;
        internal const uint CREATE_ALWAYS = 2;
        internal const uint OPEN_EXISTING = 3;
        internal const uint OPEN_ALWAYS = 4;
        internal const uint TRUNCATE_EXISTING = 5;

        internal const uint FILE_ATTRIBUTE_READONLY = 0x00000001;
        internal const uint FILE_ATTRIBUTE_HIDDEN = 0x00000002;
        internal const uint FILE_ATTRIBUTE_SYSTEM = 0x00000004;
        internal const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
        internal const uint FILE_ATTRIBUTE_ARCHIVE = 0x00000020;
        internal const uint FILE_ATTRIBUTE_DEVICE = 0x00000040;
        internal const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;
        internal const uint FILE_ATTRIBUTE_TEMPORARY = 0x00000100;
        internal const uint FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200;
        internal const uint FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400;
        internal const uint FILE_ATTRIBUTE_COMPRESSED = 0x00000800;
        internal const uint FILE_ATTRIBUTE_OFFLINE = 0x00001000;
        internal const uint FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000;
        internal const uint FILE_ATTRIBUTE_ENCRYPTED = 0x00004000;

        internal const uint FILE_FLAG_WRITE_THROUGH = 0x80000000;
        internal const uint FILE_FLAG_OVERLAPPED = 0x40000000;
        internal const uint FILE_FLAG_NO_BUFFERING = 0x20000000;
        internal const uint FILE_FLAG_RANDOM_ACCESS = 0x10000000;
        internal const uint FILE_FLAG_SEQUENTIAL_SCAN = 0x08000000;
        internal const uint FILE_FLAG_DELETE_ON_CLOSE = 0x04000000;
        internal const uint FILE_FLAG_BACKUP_SEMANTICS = 0x02000000;
        internal const uint FILE_FLAG_POSIX_SEMANTICS = 0x01000000;
        internal const uint FILE_FLAG_OPEN_REPARSE_POINT = 0x00200000;
        internal const uint FILE_FLAG_OPEN_NO_RECALL = 0x00100000;
        internal const uint FILE_FLAG_FIRST_PIPE_INSTANCE = 0x00080000;

        internal const uint FILE_BEGIN = 0;
        internal const uint FILE_CURRENT = 1;
        internal const uint FILE_END = 2;

        internal static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        internal const uint HANDLE_FLAG_INHERIT = 0x1;
        internal const uint HANDLE_FLAG_PROTECT_FROM_CLOSE = 0x2;

        internal const uint MEM_COMMIT = 0x1000;
        internal const uint MEM_RESERVE = 0x2000;
        internal const uint MEM_DECOMMIT = 0x4000;
        internal const uint MEM_RELEASE = 0x8000;
        internal const uint MEM_RESET = 0x80000;
        internal const uint MEM_TOP_DOWN = 0x100000;
        internal const uint MEM_PHYSICAL = 0x400000;

        internal const uint PAGE_NOACCESS = 0x01;
        internal const uint PAGE_READONLY = 0x02;
        internal const uint PAGE_READWRITE = 0x04;
        internal const uint PAGE_WRITECOPY = 0x08;
        internal const uint PAGE_EXECUTE = 0x10;
        internal const uint PAGE_EXECUTE_READ = 0x20;
        internal const uint PAGE_EXECUTE_READWRITE = 0x40;
        internal const uint PAGE_EXECUTE_WRITECOPY = 0x80;
        internal const uint PAGE_GUARD = 0x100;
        internal const uint PAGE_NOCACHE = 0x200;
        internal const uint PAGE_WRITECOMBINE = 0x400;

        internal const uint SEC_IMAGE = 0x1000000;
        internal const uint SEC_RESERVE = 0x4000000;
        internal const uint SEC_COMMIT = 0x8000000;
        internal const uint SEC_NOCACHE = 0x10000000;

        internal const uint SECTION_QUERY = 0x0001;
        internal const uint SECTION_MAP_WRITE = 0x0002;
        internal const uint SECTION_MAP_READ = 0x0004;
        internal const uint SECTION_MAP_EXECUTE_EXPLICIT = 0x0020;

        internal const uint FILE_MAP_COPY = SECTION_QUERY;
        internal const uint FILE_MAP_WRITE = SECTION_MAP_WRITE;
        internal const uint FILE_MAP_READ = SECTION_MAP_READ;
        internal const uint FILE_MAP_EXECUTE = SECTION_MAP_EXECUTE_EXPLICIT;

        internal const uint GMEM_FIXED = 0x0000;
        internal const uint GMEM_MOVEABLE = 0x0002;
        internal const uint GMEM_ZEROINIT = 0x0040;
        internal const uint GHND = 0x0042;
        internal const uint GPTR = 0x0040;

        internal const uint DIB_RGB_COLORS = 0; /* color table in RGBs */
        internal const uint DIB_PAL_COLORS = 1; /* color table in palette indices */

        internal const uint BI_RGB = 0;
        internal const uint BI_RLE8 = 1;
        internal const uint BI_RLE4 = 2;
        internal const uint BI_BITFIELDS = 3;
        internal const uint BI_JPEG = 4;
        internal const uint BI_PNG = 5;

        internal const uint DT_TOP = 0x00000000;
        internal const uint DT_LEFT = 0x00000000;
        internal const uint DT_CENTER = 0x00000001;
        internal const uint DT_RIGHT = 0x00000002;
        internal const uint DT_VCENTER = 0x00000004;
        internal const uint DT_BOTTOM = 0x00000008;
        internal const uint DT_WORDBREAK = 0x00000010;
        internal const uint DT_SINGLELINE = 0x00000020;
        internal const uint DT_EXPANDTABS = 0x00000040;
        internal const uint DT_TABSTOP = 0x00000080;
        internal const uint DT_NOCLIP = 0x00000100;
        internal const uint DT_EXTERNALLEADING = 0x00000200;
        internal const uint DT_CALCRECT = 0x00000400;
        internal const uint DT_NOPREFIX = 0x00000800;
        internal const uint DT_INTERNAL = 0x00001000;

        internal const uint DT_EDITCONTROL = 0x00002000;
        internal const uint DT_PATH_ELLIPSIS = 0x00004000;
        internal const uint DT_END_ELLIPSIS = 0x00008000;
        internal const uint DT_MODIFYSTRING = 0x00010000;
        internal const uint DT_RTLREADING = 0x00020000;
        internal const uint DT_WORD_ELLIPSIS = 0x00040000;
        internal const uint DT_NOFULLWIDTHCHARBREAK = 0x00080000;
        internal const uint DT_HIDEPREFIX = 0x00100000;
        internal const uint DT_PREFIXONLY = 0x00200000;

        internal const uint FW_DONTCARE = 0;
        internal const uint FW_THIN = 100;
        internal const uint FW_EXTRALIGHT = 200;
        internal const uint FW_LIGHT = 300;
        internal const uint FW_NORMAL = 400;
        internal const uint FW_MEDIUM = 500;
        internal const uint FW_SEMIBOLD = 600;
        internal const uint FW_BOLD = 700;
        internal const uint FW_EXTRABOLD = 800;
        internal const uint FW_HEAVY = 900;

        internal const uint OUT_DEFAULT_PRECIS = 0;
        internal const uint OUT_STRING_PRECIS = 1;
        internal const uint OUT_CHARACTER_PRECIS = 2;
        internal const uint OUT_STROKE_PRECIS = 3;
        internal const uint OUT_TT_PRECIS = 4;
        internal const uint OUT_DEVICE_PRECIS = 5;
        internal const uint OUT_RASTER_PRECIS = 6;
        internal const uint OUT_TT_ONLY_PRECIS = 7;
        internal const uint OUT_OUTLINE_PRECIS = 8;
        internal const uint OUT_SCREEN_OUTLINE_PRECIS = 9;
        internal const uint OUT_PS_ONLY_PRECIS = 10;

        internal const uint CLIP_DEFAULT_PRECIS = 0;
        internal const uint CLIP_CHARACTER_PRECIS = 1;
        internal const uint CLIP_STROKE_PRECIS = 2;
        internal const uint CLIP_MASK = 0xf;
        internal const uint CLIP_LH_ANGLES = (1 << 4);
        internal const uint CLIP_TT_ALWAYS = (2 << 4);
        internal const uint CLIP_EMBEDDED = (8 << 4);

        internal const uint DEFAULT_QUALITY = 0;
        internal const uint DRAFT_QUALITY = 1;
        internal const uint PROOF_QUALITY = 2;
        internal const uint NONANTIALIASED_QUALITY = 3;
        internal const uint ANTIALIASED_QUALITY = 4;

        internal const uint CLEARTYPE_QUALITY = 5;

        internal const uint CLEARTYPE_NATURAL_QUALITY = 6;

        internal const uint DEFAULT_PITCH = 0;
        internal const uint FIXED_PITCH = 1;
        internal const uint VARIABLE_PITCH = 2;
        internal const uint MONO_FONT = 8;

        internal const uint FF_DONTCARE = (0 << 4);
        internal const uint FF_ROMAN = (1 << 4);
        internal const uint FF_SWISS = (2 << 4);
        internal const uint FF_MODERN = (3 << 4);
        internal const uint FF_SCRIPT = (4 << 4);
        internal const uint FF_DECORATIVE = (5 << 4);

        internal const int SB_HORZ = 0;
    }  
}
