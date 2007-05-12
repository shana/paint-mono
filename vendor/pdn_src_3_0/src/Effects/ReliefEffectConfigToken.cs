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
    public class ReliefEffectConfigToken
        : AngleChooserConfigToken
    {
        // the convolution filter weights
        private double[,] weights = null;
        public double[,] Weights
        {
            get
            {
                double[,] localWeights = this.weights;

                if (localWeights == null)
                {
                    // adjust and convert angle to radians
                    double r = (double)Angle * 2.0 * Math.PI / 360.0;

                    // angle delta for each weight
                    double dr = Math.PI / 4.0;

                    // for r = 0 this builds an Relief filter pointing straight left
                    localWeights = new double[3, 3];

                    localWeights[0,0] = Math.Cos(r + dr);
                    localWeights[0,1] = Math.Cos(r + 2.0*dr);
                    localWeights[0,2] = Math.Cos(r + 3.0*dr);
                    
                    localWeights[1,0] = Math.Cos(r);
                    localWeights[1,1] = 1;                     
                    localWeights[1,2] = Math.Cos(r + 4.0*dr);
                    
                    localWeights[2,0] = Math.Cos(r - dr);
                    localWeights[2,1] = Math.Cos(r - 2.0*dr);
                    localWeights[2,2] = Math.Cos(r - 3.0*dr);

                    this.weights = localWeights;
                }

                return localWeights;
            }
        }

        protected override void OnAngleChanged()
        {
            weights = null;
            base.OnAngleChanged();
        }

        public override object Clone()
        {
            return new ReliefEffectConfigToken(this);
        }

        public ReliefEffectConfigToken(double angle)
            : base(angle)
        {
        }

        protected ReliefEffectConfigToken(ReliefEffectConfigToken copyMe)
            : base(copyMe)
        {
        }
    }
}
