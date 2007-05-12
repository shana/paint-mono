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
    public enum OSType
    {
        Unknown = 0,
        Workstation = (int)NativeConstants.VER_NT_WORKSTATION,
        DomainController = (int)NativeConstants.VER_NT_DOMAIN_CONTROLLER,
        Server = (int)NativeConstants.VER_NT_SERVER,
    }
}
