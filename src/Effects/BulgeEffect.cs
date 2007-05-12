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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using PaintDotNet;
using PaintDotNet.Effects;

namespace PaintDotNet.Effects
{
    [Guid("1445F876-356D-4a7c-B726-50457F6E7AEF")]
    public sealed class BulgeEffect 
        : Effect
    {
        public static Image StaticImage
        {
            get
            {
                return PdnResources.GetImage("Icons.BulgeEffect.png");
            }
        }

        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("BulgeEffect.Name");
            }
        }

        public static string StaticSubMenuName
        {
            get
            {
                return PdnResources.GetString("DistortSubmenu.Name");
            }
        }

        public BulgeEffect()
            : base(StaticName, StaticImage, StaticSubMenuName, true)
        {
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            AmountEffectConfigDialog aecd = new AmountEffectConfigDialog();

            aecd.Text = StaticName;
            aecd.SliderInitialValue = 45;
            aecd.SliderLabel = PdnResources.GetString("BulgeEffect.BulgeAmount.Text");
            aecd.SliderMaximum = 100;
            aecd.SliderMinimum = -200;
            aecd.SliderUnitsName = string.Empty;

            return aecd;
        }

        public unsafe override void Render(
            EffectConfigToken parameters, 
            RenderArgs dstArgs, 
            RenderArgs srcArgs, 
            System.Drawing.Rectangle[] rois, 
            int startIndex, 
            int length)
        {
            AmountEffectConfigToken token = (AmountEffectConfigToken)parameters;

            float bulge = token.Amount;
            Surface dst = dstArgs.Surface;
            Surface src = srcArgs.Surface;

            float hw = dst.Width / 2.0f;
            float hh = dst.Height / 2.0f;
            float maxrad = Math.Min(hw, hh);
            float maxrad2 = maxrad * maxrad;
            float amt = token.Amount / 100.0f;

            for (int n = startIndex; n < startIndex + length; ++n)
            {
                Rectangle rect = rois[n];
                
                for (int y = rect.Top; y < rect.Bottom; y++)
                {
                    ColorBgra* dstPtr = dst.GetPointAddressUnchecked(rect.Left, y);
                    ColorBgra* srcPtr = src.GetPointAddressUnchecked(rect.Left, y);
                    float v = y - hh;

                    for (int x = rect.Left; x < rect.Right; x++)
                    {
                        float u = x - hw;
                        float r = (float)Math.Sqrt(u * u + v * v);
                        float rscale;
                        rscale = (1.0f - (r / maxrad));

                        if (rscale > 0)
                        {
                            rscale = 1 - amt * rscale * rscale;

                            float xp = u * rscale;
                            float yp = v * rscale;

                            *dstPtr = src.GetBilinearSampleWrapped(xp + hw, yp + hh);
                        }
                        else
                        {
                            *dstPtr = *srcPtr;
                        }

                        ++dstPtr;
                        ++srcPtr;
                    }
                }
            }
        }
    }
}
