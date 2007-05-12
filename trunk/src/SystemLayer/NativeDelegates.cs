/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;

namespace PaintDotNet.SystemLayer
{
    internal static class NativeDelegates
    {
        internal delegate void OVERLAPPED_COMPLETION_ROUTINE(
            uint dwErrorCode,
            uint dwNumberOfBytesTransferred,
            ref NativeStructs.OVERLAPPED lpOverlapped
            );
    }
}
