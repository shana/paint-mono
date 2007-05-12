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
    internal sealed class NativeDelegates
    {
        internal delegate int InstallUiHandler(
            IntPtr pvContext,
            uint iMessageType,
            [MarshalAs(UnmanagedType.LPWStr)] string szMessage
            );

        private NativeDelegates()
        {
        }
    }
}
