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
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace PaintDotNet.Effects
{
    [Guid("9A1EB3D9-0A36-4d32-9BB2-707D6E5A9D2C")]
    public sealed class TwistEffect 
        : Effect
    {
        public static Image StaticImage
        {
            get
            {
                return PdnResources.GetImage("Icons.TwistEffect.png");
            }
        }

        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("TwistEffect.Name");
            }
        }

        public static string StaticSubMenuName
        {
            get
            {
                return PdnResources.GetString("DistortSubmenu.Name");
            }
        }

        public TwistEffect()
            : base(StaticName, StaticImage, StaticSubMenuName, true)
        {
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            TwoAmountsConfigDialog tacd = new TwoAmountsConfigDialog();

            tacd.Text = StaticName;
            tacd.Amount1Default = 45;
            tacd.Amount1Label = PdnResources.GetString("TwistEffect.TwistAmount.Text");
            tacd.Amount1Maximum = 100;
            tacd.Amount1Minimum = -100;
            tacd.Amount2Default = 2;
            tacd.Amount2Label = PdnResources.GetString("TwistEffect.Antialias.Text");
            tacd.Amount2Maximum = 5;
            tacd.Amount2Minimum = 0;

            return tacd;
        }

        public unsafe override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, System.Drawing.Rectangle[] rois, int startIndex, int length)
        {
            TwoAmountsConfigToken token = (TwoAmountsConfigToken)parameters;

            float twist = token.Amount1;
            Surface dst = dstArgs.Surface;
            Surface src = srcArgs.Surface;

            float hw = dst.Width / 2.0f;
            float hh = dst.Height / 2.0f;
            float maxrad = Math.Min(hw, hh);

            twist = twist * twist * Math.Sign(twist);

            int aaLevel = token.Amount2;
            int aaSamples = aaLevel * aaLevel + 1;
            PointF* aaPoints = stackalloc PointF[aaSamples];

            for (int i = 0; i < aaSamples; ++i)
            {
                PointF pt = new PointF(
                    ((i * aaLevel) / (float)aaSamples),
                    i / (float)aaSamples);

                pt.X -= (int)pt.X;
                aaPoints[i] = pt;
            }

            for (int n = startIndex; n < startIndex + length; ++n)
            {
                Rectangle rect = rois[n];
                for (int y = rect.Top; y < rect.Bottom; y++)
                {
                    float j = y - hh;
                    ColorBgra* dstPtr = dst.GetPointAddressUnchecked(rect.Left, y);
                    ColorBgra* srcPtr = src.GetPointAddressUnchecked(rect.Left, y);

                    for (int x = rect.Left; x < rect.Right; x++)
                    {
                        float i = x - hw;

                        if (i * i + j * j > (maxrad + 1) * (maxrad + 1))
                        {
                            *dstPtr = *srcPtr;
                        }
                        else
                        {
                            int b = 0;
                            int g = 0;
                            int r = 0;
                            int a = 0;

                            for (int p = 0; p < aaSamples; ++p)
                            {
                                float u = i + aaPoints[p].X;
                                float v = j + aaPoints[p].Y;
                                double rad = Math.Sqrt(u * u + v * v);
                                double theta = Math.Atan2(v, u);

                                double t = 1 - rad / maxrad;

                                t = (t < 0) ? 0 : (t * t * t);

                                theta += (t * twist) / 100;

                                ColorBgra sample = src.GetPointUnchecked(
                                    (int)(hw + (float)(rad * Math.Cos(theta))),
                                    (int)(hh + (float)(rad * Math.Sin(theta))));

                                b += sample.B;
                                g += sample.G;
                                r += sample.R;
                                a += sample.A;
                            }

                            *dstPtr = ColorBgra.FromBgra(
                                (byte)(b / aaSamples),
                                (byte)(g / aaSamples),
                                (byte)(r / aaSamples),
                                (byte)(a / aaSamples));
                        }

                        ++dstPtr;
                        ++srcPtr;
                    }
                }
            }
        }
    }
}
