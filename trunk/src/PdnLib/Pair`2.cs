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
    public struct Pair<T, U>
    {
        private T first;
        private U second;

        public T First
        {
            get
            {
                return this.first;
            }
        }

        public U Second
        {
            get
            {
                return this.second;
            }
        }

        public override int GetHashCode()
        {
            return this.first.GetHashCode() ^ this.second.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return ((obj != null) && (obj is Pair<T, U>) && (this == (Pair<T, U>)obj));
        }

        public static bool operator ==(Pair<T, U> lhs, Pair<T, U> rhs)
        {
            return (lhs.First.Equals(rhs.First) && lhs.Second.Equals(rhs.Second));
        }

        public static bool operator !=(Pair<T, U> lhs, Pair<T, U> rhs)
        {
            return !(lhs == rhs);
        }

        public Pair(T first, U second)
        {
            this.first = first;
            this.second = second;
        }
    }
}
