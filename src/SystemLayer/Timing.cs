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
    /// <summary>
    /// Methods for keeping track of time in a high precision manner.
    /// </summary>
    /// <remarks>
    /// This class provides precision and accuracy of 1 millisecond.
    /// </remarks>
    public sealed class Timing
    {
        private ulong countsPerMs;
        private double countsPerMsDouble;
        private ulong birthTick;

        /// <summary>
        /// The number of milliseconds that elapsed between system startup
        /// and creation of this instance of Timing.
        /// </summary>
        public ulong BirthTick
        {
            get
            {
                return birthTick;
            }
        }

        /// <summary>
        /// Returns the number of milliseconds that have elapsed since
        /// system startup.
        /// </summary>
        public ulong GetTickCount()
        {
		return (ulong) DateTime.UtcNow.Ticks;
        }

        /// <summary>
        /// Returns the number of milliseconds that have elapsed since
        /// system startup.
        /// </summary>
        public double GetTickCountDouble()
        {
		return (double)DateTime.UtcNow.Ticks;
        }

        /// <summary>
        /// Constructs an instance of the Timing class.
        /// </summary>
        public Timing()
        {
		birthTick = GetTickCount();
        }
    }
}
