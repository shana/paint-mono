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

namespace PaintDotNet.Effects
{
    public sealed class SharpenEffect
        : LocalHistogramEffect
    {
        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("SharpenEffect.Name");
            }
        }

        public static ImageResource StaticImage
        {
            get
            {
                return ImageResource.Get("Icons.SharpenEffect.png");
            }
        }

        public SharpenEffect()
            : base(StaticName, StaticImage.Reference, true)
        {
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            AmountEffectConfigDialog acd = new AmountEffectConfigDialog();

            acd.Text = this.Name;

            acd.SliderLabel = PdnResources.GetString("SharpenEffect.ConfigDialog.SliderLabel");
            acd.SliderInitialValue = 2;
            acd.SliderMinimum = 1;
            acd.SliderMaximum = 20;
            acd.SliderUnitsName = string.Empty;

            return acd;
        }

        public unsafe override ColorBgra Apply(ColorBgra src, int area, int* hb, int* hg, int* hr, int* ha)
        {
            ColorBgra median = GetPercentile(50, area, hb, hg, hr, ha);
            return ColorBgra.Lerp(src, median, -0.5f);
        }

        public unsafe override void Render(
            EffectConfigToken parameters,
            RenderArgs dstArgs,
            RenderArgs srcArgs,
            Rectangle[] rois,
            int startIndex,
            int length)
        {
            AmountEffectConfigToken token = (AmountEffectConfigToken)parameters;

            foreach (Rectangle rect in rois)
            {
                RenderRect(token.Amount, srcArgs.Surface, dstArgs.Surface, rect);
            }
        }
    }
}
