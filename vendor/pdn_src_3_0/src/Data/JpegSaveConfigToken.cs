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
    public class JpegSaveConfigToken
        : SaveConfigToken
    {
        private int quality;

        public int Quality
        {
            get
            {
                return quality;
            }

            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("quality must be 0 to 100, inclusive");
                }

                this.quality = value;
            }
        }

        public JpegSaveConfigToken(int quality)
        {
            this.quality = quality;
            Validate();
        }

        protected JpegSaveConfigToken(JpegSaveConfigToken cloneMe)
        {
            this.quality = cloneMe.quality;
        }

        public override void Validate()
        {
            if (this.quality < 0 || this.quality > 100)
            {
                throw new ArgumentOutOfRangeException("quality must be 0 to 100, inclusive");
            }

            base.Validate();
        }

        public override object Clone()
        {
            return new JpegSaveConfigToken(this);
        }
    }
}
