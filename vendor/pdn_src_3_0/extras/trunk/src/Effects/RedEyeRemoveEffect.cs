/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;

namespace PaintDotNet.Effects
{
    [EffectTypeHint(EffectTypeHint.Unary | EffectTypeHint.Fast)]
    public sealed class RedEyeRemoveEffect
        : Effect
    {
        public override EffectConfigDialog CreateConfigDialog()
        {   
            RedEyeRemoveEffectDialog tacd = new RedEyeRemoveEffectDialog();

            tacd.Text = PdnResources.GetString("RedEyeRemoveEffect.Name");

            tacd.Amount1Minimum = 0;
            tacd.Amount1Maximum = 100;
            tacd.Amount1Default = 70;
            tacd.Amount1Label = PdnResources.GetString("RedEyeRemoveEffect.ConfigDialog.Amount1Label");
            
            tacd.Amount2Minimum = 0;
            tacd.Amount2Maximum = 100;
            tacd.Amount2Default = 90;
            tacd.Amount2Label = PdnResources.GetString("RedEyeRemoveEffect.ConfigDialog.Amount2Label");

            tacd.Icon = PdnResources.GetIconFromImage("Icons.RedEyeRemoveEffect.png");

            return tacd;
        }

        public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, System.Drawing.Rectangle[] rois, int startIndex, int length)
        {
            TwoAmountsConfigToken tact = (TwoAmountsConfigToken)parameters;
            PixelOp redEyeRemove = new UnaryPixelOps.RedEyeRemove(tact.Amount1, tact.Amount2);

            redEyeRemove.Apply(dstArgs.Surface, srcArgs.Surface, rois, startIndex, length);
        }

        public RedEyeRemoveEffect()
            : base(PdnResources.GetString("RedEyeRemoveEffect.Name"),
                   PdnResources.GetImage("Icons.RedEyeRemoveEffect.png"),
                   true)
        {
        }
    }
}