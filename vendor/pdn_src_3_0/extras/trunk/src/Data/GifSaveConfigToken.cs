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
    [Serializable]
    public class GifSaveConfigToken
        : SaveConfigToken
    {
        private const int minThreshold = 0;
        private const int maxThreshold = 255;
        private const int minDitherLevel = 0;
        private const int maxDitherLevel = 8;

        private int threshold;
        private int ditherLevel;
        private bool preMultiplyAlpha;

        public int Threshold
        {
            get
            {
                return threshold;
            }

            set
            {
                if (value < minThreshold || value > maxThreshold)
                {
                    throw new ArgumentOutOfRangeException("threshold must be " + minThreshold + " to " + maxThreshold +", inclusive");
                }

                this.threshold = value;
            }
        }

        public bool PreMultiplyAlpha
        {
            get
            {
                return this.preMultiplyAlpha;
            }

            set
            {
                this.preMultiplyAlpha = value;
            }
        }
        
        public int DitherLevel
        {
            get
            {
                return this.ditherLevel;
            }

            set
            {
                if (value < minDitherLevel || value > maxDitherLevel)
                {
                    throw new ArgumentOutOfRangeException("ditherLevel must be " + minDitherLevel + " to " + maxDitherLevel + ", inclusive");
                }

                this.ditherLevel = value;
            }
        }


        public GifSaveConfigToken(int threshold, bool preMultiplyAlpha, int ditherLevel)
        {
            this.ditherLevel = ditherLevel;
            this.threshold = threshold;
            this.preMultiplyAlpha = preMultiplyAlpha;
            Validate();
        }

        public override void Validate()
        {
            if (this.threshold < minThreshold || this.threshold > maxThreshold)
            {
                throw new ArgumentOutOfRangeException("threshold must be " + minThreshold + " to " + maxThreshold +", inclusive");
            }

            if (this.ditherLevel < minDitherLevel || this.ditherLevel > maxDitherLevel)
            {
                throw new ArgumentOutOfRangeException("ditherLevel must be " + minDitherLevel + " to " + maxDitherLevel + ", inclusive");
            }
        }

        protected GifSaveConfigToken(GifSaveConfigToken cloneMe)
        {
            this.threshold = cloneMe.threshold;
            this.preMultiplyAlpha = cloneMe.preMultiplyAlpha;
            this.ditherLevel = cloneMe.ditherLevel;
        }

        public override object Clone()
        {
            return new GifSaveConfigToken(this);
        }
    }
}
