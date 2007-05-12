/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.ComponentModel;

namespace PaintDotNet
{
    public class HandledEventArgs<T>
        : HandledEventArgs
    {
        private T data;
        public T Data
        {
            get
            {
                return this.data;
            }
        }

        public HandledEventArgs(bool handled, T data)
            : base(handled)
        {
            this.data = data;
        }
    }
}
