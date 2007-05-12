/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet;
using PaintDotNet.Effects;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PaintDotNet.Effects
{
    public class CloudsEffectConfigToken
        : TwoAmountsConfigToken
    {
        private int seed;
        public int Seed
        {
            get
            {
                return this.seed;
            }

            set
            {
                this.seed = value;
            }
        }

        private UserBlendOp blendOp;
        public UserBlendOp BlendOp
        {
            get
            {
                return this.blendOp;
            }

            set
            {
                this.blendOp = value;
            }
        }

        public override object Clone()
        {
            return new CloudsEffectConfigToken(this.Amount1, this.Amount2, this.seed, this.blendOp);
        }

        public CloudsEffectConfigToken(int scale, int power, int seed, UserBlendOp blendOp)
            : base(scale, power)
        {
            this.seed = seed;
            this.blendOp = blendOp;
        }

        protected CloudsEffectConfigToken(CloudsEffectConfigToken copyMe)
            : base(copyMe)
        {
            this.seed = copyMe.seed;
            this.blendOp = copyMe.blendOp;
        }
    }
}
