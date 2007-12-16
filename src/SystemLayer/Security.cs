/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using Microsoft.Win32;
using System;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using Mono.Unix.Native;

namespace PaintDotNet.SystemLayer
{
    /// <summary>
    /// Security related static methods and properties.
    /// </summary>
    public static class Security
    {
        private static bool isAdmin = GetIsAdministrator();

        private static bool GetIsAdministrator()
        {
			return (Syscall.getuid () == 0);
        }

        /// <summary>
        /// Gets a flag indicating whether the user has administrator-level privileges.
        /// </summary>
        /// <remarks>
        /// This is used to control access to actions that require the user to be an administrator.
        /// An example is checking for and installing updates, actions which are not normally able
        /// to be performed by normal or "limited" users. A user must also be an administrator in
        /// order to write to any Settings.SystemWide entries.
        /// </remarks>
        public static bool IsAdministrator
        {
            get
            {
                return isAdmin;
            }
        }
		
        /// <summary>
        /// Gets a flag indicating whether the current user is able to elevate to obtain
        /// administrator-level privileges.
        /// </summary>
        /// <remarks>
        /// This flag has no meaning if IsAdministrator returns true.
        /// This flag indicates whether a new process may be spawned which has administrator
        /// privilege. It does not indicate the ability to elevate the current process to
        /// administrator privilege. For Windows this indicates that the user is running
        /// Vista and has UAC enabled. This property should be used instead of checking
        /// the OS version anytime this check must be performed.
        /// Note to implementors: This may be written to simply return false.
        /// </remarks>
        public static bool CanElevateToAdministrator
        {
            get
            {
				return isAdmin;
            }
        }

        /// <summary>
        /// Verifies that a file has a valid digital signature.
        /// </summary>
        /// <param name="owner">The parent/owner window for any UI that may be shown.</param>
        /// <param name="fileName">The path to the file to be validate.</param>
        /// <param name="showNegativeUI">Whether or not to show a UI in the case that the signature can not be found or validated.</param>
        /// <param name="showPositiveUI">Whether or not to show a UI in the case that the signature is successfully found and validated.</param>
        /// <returns>true if the file has a digital signature that validates up to a trusted root, or false otherwise</returns>
        public static bool VerifySignedFile(IWin32Window owner, string fileName, bool showNegativeUI, bool showPositiveUI)
        {
			Console.WriteLine ("POR: VerifySignedFile has not been implemented");
			return true;
        }
    }
}
