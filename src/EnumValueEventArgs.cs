/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;

namespace PaintDotNet
{
    // TODO: use EventHandler<T> instead
    public sealed class EnumValueEventArgs
        : System.EventArgs
    {
        private System.Enum enumValue;
        public System.Enum EnumValue
        {
            get
            {
                return enumValue;
            }
        }

        public EnumValueEventArgs(System.Enum enumValue)
        {
            this.enumValue = enumValue;
        }
    }
}
