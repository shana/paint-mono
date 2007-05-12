/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet;
using System;

namespace PaintDotNet.Data
{
    [Serializable]
    public class TgaSaveConfigToken
        : SaveConfigToken
    {
        public override object Clone()
        {
            return new TgaSaveConfigToken(this);
        }

        private int bitDepth;
        public int BitDepth
        {
            get
            {
                return bitDepth;
            }

            set
            {
                if (value != 16 && value != 24 && value != 32)
                {
                    throw new NotSupportedException("bitDepth not one of { 16, 24, 32 }");
                }

                this.bitDepth = value;
            }
        }

        private bool rleCompress;
        public bool RleCompress
        {
            get
            {
                return this.rleCompress;
            }

            set
            {
                this.rleCompress = value;
            }
        }

        public TgaSaveConfigToken(int bitDepth, bool rleCompress)
        {
            this.BitDepth = bitDepth;
            this.RleCompress = rleCompress;
        }

        protected TgaSaveConfigToken(TgaSaveConfigToken copyMe)
        {
            this.bitDepth = copyMe.bitDepth;
            this.rleCompress = copyMe.rleCompress;
        }

        public override void Validate()
        {
            if (this.bitDepth != 16 && this.bitDepth != 24 && this.bitDepth != 32)
            {
                throw new NotSupportedException("bitDepth not one of { 16, 24, 32 }");
            }

            base.Validate();
        }

    }
}
