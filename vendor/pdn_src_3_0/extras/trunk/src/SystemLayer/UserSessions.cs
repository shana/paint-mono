/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Threading;
using System.Windows.Forms;

namespace PaintDotNet.SystemLayer
{
    /// <summary>
    /// Encapsulates information and events about the current user session.
    /// This relates to Terminal Services in Windows.
    /// </summary>
    public static class UserSessions
    {
 
        /// <summary>
        /// Occurs when the user changes between sessions. This event will only be
        /// raised when the value returned by IsRemote() changes.
        /// </summary>
        /// <remarks>
        /// For example, if the user is currently logged in at the console, and then
        /// switches to a remote session (they use Remote Desktop from another computer),
        /// then this event will be raised.
        /// Note to implementors: This may be implemented as a no-op.
        /// </remarks>
        public static event EventHandler SessionChanged;

		/// <summary>
        /// Determines whether the user is running within a remoted session (Terminal Server, Remote Desktop).
        /// </summary>
        /// <returns>
        /// <b>true</b> if we're running in a remote session, <b>false</b> otherwise.
        /// </returns>
        /// <remarks>
        /// You can use this to optimize the presentation of visual elements. Remote sessions
        /// are often bandwidth limited and less suitable for complex drawing.
        /// Note to implementors: This may be implemented as a no op; in this case, always return false.
        /// </remarks>
        public static bool IsRemote()
        {
            return false;
        }
    }
}
