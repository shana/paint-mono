/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace PaintDotNet.Effects
{
    public sealed class OutlineEffect
        : LocalHistogramEffect
    {
        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("OutlineEffect.Name");
            }
        }

        public static ImageResource StaticImage
        {
            get
            {
                return ImageResource.Get("Icons.OutlineEffectIcon.png");
            }
        }

        private int radius;
        private int spread;

        public OutlineEffect()
            : base(StaticName, StaticImage.Reference, true)
        {
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            TwoAmountsConfigDialog tacd = new TwoAmountsConfigDialog();

            tacd.Text = this.Name;

            tacd.Amount1Label = PdnResources.GetString("OutlineEffect.ConfigDialog.ThicknessLabel");
            tacd.Amount1Default = 3;
            tacd.Amount1Minimum = 1;
            tacd.Amount1Maximum = 200;

            tacd.Amount2Label = PdnResources.GetString("OutlineEffect.ConfigDialog.IntensityLabel");
            tacd.Amount2Default = 50;
            tacd.Amount2Minimum = 0;
            tacd.Amount2Maximum = 100;

            return tacd;
        }

        public unsafe override ColorBgra Apply(ColorBgra src, int area, int* hb, int* hg, int* hr, int* ha)
        {
            int minCount1 = area * (100 - this.spread) / 200;
            int minCount2 = area * (100 + this.spread) / 200;

            int bCount = 0;
            int b1 = 0;
            while (b1 < 255 && hb[b1] == 0)
            {
                ++b1;
            }

            while (b1 < 255 && bCount < minCount1)
            {
                bCount += hb[b1];
                ++b1;
            }

            int b2 = b1;
            while (b2 < 255 && bCount < minCount2)
            {
                bCount += hb[b2];
                ++b2;
            }

            int gCount = 0;
            int g1 = 0;
            while (g1 < 255 && hg[g1] == 0)
            {
                ++g1;
            }

            while (g1 < 255 && gCount < minCount1)
            {
                gCount += hg[g1];
                ++g1;
            }

            int g2 = g1;
            while (g2 < 255 && gCount < minCount2)
            {
                gCount += hg[g2];
                ++g2;
            }

            int rCount = 0;
            int r1 = 0;
            while (r1 < 255 && hr[r1] == 0)
            {
                ++r1;
            }

            while (r1 < 255 && rCount < minCount1)
            {
                rCount += hr[r1];
                ++r1;
            }

            int r2 = r1;
            while (r2 < 255 && rCount < minCount2)
            {
                rCount += hr[r2];
                ++r2;
            }

            int aCount = 0;
            int a1 = 0;
            while (a1 < 255 && hb[a1] == 0)
            {
                ++a1;
            }

            while (a1 < 255 && aCount < minCount1)
            {
                aCount += ha[a1];
                ++a1;
            }

            int a2 = a1;
            while (a2 < 255 && aCount < minCount2)
            {
                aCount += ha[a2];
                ++a2;
            }

            return ColorBgra.FromBgra(
                (byte)(255 - (b2 - b1)),
                (byte)(255 - (g2 - g1)),
                (byte)(255 - (r2 - r1)),
                (byte)(a2));
        }

        public unsafe override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
        {
            TwoAmountsConfigToken token = (TwoAmountsConfigToken)parameters;
            this.radius = token.Amount1;
            this.spread = token.Amount2;

            foreach (Rectangle rect in rois)
            {
                RenderRect(this.radius, srcArgs.Surface, dstArgs.Surface, rect);
            }
        }
    }
}
