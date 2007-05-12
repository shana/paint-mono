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

namespace PaintDotNet.Effects
{
    [EffectTypeHint(EffectTypeHint.Fast)]
    public sealed class ReliefEffect
        : ColorDifferenceEffect
    {
        public ReliefEffect()
            : base(PdnResources.GetString("ReliefEffect.Name"),
                   PdnResources.GetImage("Icons.ReliefEffect.png"),
                   true)
        {
        }

        public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
        {
            ReliefEffectConfigToken token = (ReliefEffectConfigToken)parameters;
            base.RenderColorDifferenceEffect(token.Weights, dstArgs, srcArgs, rois, startIndex, length);
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            return new ReliefEffectConfigDialog();
        }
    }
}
