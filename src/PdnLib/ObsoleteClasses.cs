/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace PaintDotNet
{
    [Obsolete("Use Function<bool, object> instead.", true)]
    public delegate bool BoolObjectDelegate(object o);

    [Obsolete("Use Procedure<bool> instead.", true)]
    public delegate bool BoolVoidDelegate();

    [Obsolete("Use EventArgs<T> instead", true)]
    public class DataEventArgs<T>
        : EventArgs
    {
        private T data;
        public T Data
        {
            get
            {
                return data;
            }
        }

        public DataEventArgs(T data)
        {
            this.data = data;
        }
    }

    [Obsolete("Use EventHandler<T> instead", true)]
    public delegate void DataEventHandler<T>(object sender, EventArgs<T> e);

    [Obsolete("Use Procedure<object> instead", true)]
    public delegate void VoidObjectDelegate(object obj);

    [Obsolete("Use Procedure instead", true)]
    public delegate void VoidVoidDelegate();

    [Obsolete("Use Procedure instead", true)]
    public delegate void ProcedureDelegate();

    [Obsolete("Use Procedure instead", true)]
    public delegate void UnaryProcedureDelegate<T>(T parameter);

    [Obsolete("Use Procedure instead", true)]
    public delegate void BinaryProcedureDelegate<T, U>(T first, U second);

    [Obsolete("Use Function instead", true)]
    public delegate R UnaryFunctionDelegate<R, T>(T parameter);

    [Obsolete("Use Function instead", true)]
    public delegate R BinaryFunctionDelegate<R, T, U>(T first, U second);
}

