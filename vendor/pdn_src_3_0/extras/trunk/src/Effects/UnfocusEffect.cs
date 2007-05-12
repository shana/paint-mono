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
    public sealed class UnfocusEffect
        : LocalHistogramEffect
    {
        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("UnfocusEffect.Name");
            }
        }

        public static ImageResource StaticImage
        {
            get
            {
                return ImageResource.Get("Icons.UnfocusEffectIcon.png");
            }
        }

        public UnfocusEffect() 
            : base(StaticName, 
                   StaticImage.Reference,
                   PdnResources.GetString("Effects.Blurring.Submenu.Name"),
                   true)
        { 
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            AmountEffectConfigDialog acd = new AmountEffectConfigDialog();

            acd.Text = this.Name;

            acd.SliderLabel = PdnResources.GetString("UnfocusEffect.ConfigDialog.AmountLabel");
            acd.SliderMinimum = 1;
            acd.SliderMaximum = 200;
            acd.SliderInitialValue = 4;
            acd.SliderUnitsName = PdnResources.GetString("UnfocusEffect.ConfigDialog.UnitsLabel");

            return acd;
        }

        public unsafe override ColorBgra Apply(ColorBgra src, int area, int* hb, int* hg, int* hr, int* ha)
        {
            int b = 0;
            int g = 0;
            int r = 0;
            int a = 0;

            for (int i = 1; i < 256; ++i)
            {
                b += i * hb[i];
                g += i * hg[i];
                r += i * hr[i];
                a += i * ha[i];
            }

            ColorBgra c = ColorBgra.FromBgraClamped(b / area, g / area, r / area, a / area);
            return c;
        }

        public unsafe override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
        {
            AmountEffectConfigToken token = (AmountEffectConfigToken)parameters;

            foreach (Rectangle rect in rois)
            {
                RenderRect(token.Amount, srcArgs.Surface, dstArgs.Surface, rect);
            }
        }
    }
}
