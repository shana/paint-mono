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
    public struct Triple<T, U, V>
    {
        private T first;
        private U second;
        private V third;

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

        public V Third
        {
            get
            {
                return this.third;
            }
        }

        public override int GetHashCode()
        {
            return this.first.GetHashCode() ^ this.second.GetHashCode() ^ this.third.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return ((obj != null) && (obj is Triple<T, U, V>) && (this == (Triple<T, U, V>)obj));
        }

        public static bool operator ==(Triple<T, U, V> lhs, Triple<T, U, V> rhs)
        {
            return (lhs.First.Equals(rhs.First) && lhs.Second.Equals(rhs.Second) && lhs.Third.Equals(rhs.Third));
        }

        public static bool operator !=(Triple<T, U, V> lhs, Triple<T, U, V> rhs)
        {
            return !(lhs == rhs);
        }

        public Triple(T first, U second, V third)
        {
            this.first = first;
            this.second = second;
            this.third = third;
        }
    }
}
