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
    public class AngleChooserConfigToken
        : EffectConfigToken
    {
        private double angle;
        public double Angle
        {
            get
            {
                return angle;
            }

            set
            {
                this.angle = value;
                OnAngleChanged();
            }
        }

        // Override to implement behavior for when the angle changes
        protected virtual void OnAngleChanged()
        {
        }

        public override object Clone()
        {
            return new AngleChooserConfigToken(this);
        }

        public AngleChooserConfigToken(double angle)
        {
            this.angle = angle;
        }

        protected AngleChooserConfigToken(AngleChooserConfigToken copyMe)
            : base(copyMe)
        {
            this.angle = copyMe.angle;
        }
    }
}
