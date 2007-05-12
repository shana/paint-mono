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
using System.Windows.Forms;

namespace PaintDotNet.Effects
{
    [GuidAttribute("270DCBF1-CE42-411e-9885-162E2BFA8265")]
    public sealed class GlowEffect
        : Effect
    {
        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("GlowEffect.Name");
            }
        }

        public static Image StaticImage
        {
            get
            {
                return PdnResources.GetImage("Icons.GlowEffect.png");
            }
        }

        private BlurEffect blurEffect;
        private BrightnessAndContrastAdjustment bcAdjustment;
        private UserBlendOps.ScreenBlendOp screenBlendOp;

        public override unsafe void Render(
            EffectConfigToken parameters, 
            RenderArgs dstArgs, 
            RenderArgs srcArgs, 
            System.Drawing.Rectangle[] rois, 
            int startIndex, 
            int length)
        {
            // First we blur the source, and write the result to the destination surface
            // Then we apply Brightness/Contrast with the input as the dst, and the output as the dst
            // Third, we apply the Screen blend operation so that dst = dst OVER src

            ThreeAmountsConfigToken token = (ThreeAmountsConfigToken)parameters;

            AmountEffectConfigToken blurToken = new AmountEffectConfigToken(token.Amount1);
            this.blurEffect.Render(blurToken, dstArgs, srcArgs, rois, startIndex, length);

            BrightnessAndContrastAdjustmentConfigToken bcToken = new BrightnessAndContrastAdjustmentConfigToken(token.Amount2, token.Amount3);
            this.bcAdjustment.Render(bcToken, dstArgs, dstArgs, rois, startIndex, length);

            for (int i = startIndex; i < startIndex + length; ++i)
            {
                Rectangle roi = rois[i];

                for (int y = roi.Top; y < roi.Bottom; ++y)
                {
                    ColorBgra* dstPtr = dstArgs.Surface.GetPointAddressUnchecked(roi.Left, y);
                    ColorBgra* srcPtr = srcArgs.Surface.GetPointAddressUnchecked(roi.Left, y);

                    screenBlendOp.Apply(dstPtr, srcPtr, dstPtr, roi.Width);
                }
            }
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            ThreeAmountsConfigDialog dialog = new ThreeAmountsConfigDialog();

            dialog.Amount1Default = 6;
            dialog.Amount1Label = PdnResources.GetString("GlowEffect.Amount1.Name");
            dialog.Amount1Minimum = 1;
            dialog.Amount1Maximum = 100;

            dialog.Amount2Default = 10;
            dialog.Amount2Label = PdnResources.GetString("GlowEffect.Amount2.Name");
            dialog.Amount2Minimum = -100;
            dialog.Amount2Maximum = +100;

            dialog.Amount3Default = 10;
            dialog.Amount3Label = PdnResources.GetString("GlowEffect.Amount3.Name");
            dialog.Amount3Minimum = -100;
            dialog.Amount3Maximum = +100;

            dialog.Text = StaticName;

            return dialog;
        }

        public GlowEffect()
            : base(StaticName, StaticImage, null, EffectDirectives.None, true)
        {
            this.blurEffect = new BlurEffect();
            this.bcAdjustment = new BrightnessAndContrastAdjustment();
            this.screenBlendOp = new UserBlendOps.ScreenBlendOp();
        }
    }
}
