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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PaintDotNet.Effects
{
    [EffectCategory(EffectCategory.Adjustment)]
    [EffectTypeHint(EffectTypeHint.Unary | EffectTypeHint.Fast)]
    public sealed class HueAndSaturationAdjustment
        : Effect
    {
        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("HueAndSaturationAdjustment.Name");
            }
        }

        public static Image StaticImage
        {
            get
            {
                return PdnResources.GetImage("Icons.HueAndSaturationAdjustment.png");
            }
        }

        public HueAndSaturationAdjustment()
            : base(StaticName,
                   StaticImage,
                   true)
        {
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            ThreeAmountsConfigDialog tacg = new ThreeAmountsConfigDialog();

            tacg.Text = HueAndSaturationAdjustment.StaticName;

            tacg.Amount1Default = 0;
            tacg.Amount1Label = PdnResources.GetString("HueAndSaturationAdjustment.Amount1Label");
            tacg.Amount1Maximum = 180;
            tacg.Amount1Minimum = -180;

            tacg.Amount2Default = 100;
            tacg.Amount2Label = PdnResources.GetString("HueAndSaturationAdjustment.Amount2Label");
            tacg.Amount2Maximum = 200;
            tacg.Amount2Minimum = 0;

            tacg.Amount3Default = 0;
            tacg.Amount3Label = PdnResources.GetString("HueAndSaturationAdjustment.Amount3Label");
            tacg.Amount3Maximum = 100;
            tacg.Amount3Minimum = -100;

            tacg.Icon = PdnResources.GetIconFromImage("Icons.HueAndSaturationAdjustment.png");

            return tacg;
        }

        public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
        {
            ThreeAmountsConfigToken token = (ThreeAmountsConfigToken)parameters;
            int hueDelta = token.Amount1;
            int satDelta = token.Amount2;
            int lightness = token.Amount3;

            // map the range [0,100] -> [0,100] and the range [101,200] -> [103,400]
            if (satDelta > 100)
            {
                satDelta = ((satDelta - 100) * 3) + 100;
            }

            UnaryPixelOp op;

            Surface dst = dstArgs.Surface;
            Surface src = srcArgs.Surface;

            if (hueDelta == 0 && satDelta == 100 && lightness == 0)
            {
                op = new UnaryPixelOps.Identity();
            }
            else
            {
                op = new UnaryPixelOps.HueSaturationLightness(hueDelta, satDelta, lightness);
            }

            op.Apply(dst, src, rois, startIndex, length);
        }
    }
}
