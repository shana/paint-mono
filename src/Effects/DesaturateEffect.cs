/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System.Drawing;
using System;
using System.Windows.Forms;

namespace PaintDotNet.Effects
{
    [EffectCategory(EffectCategory.Adjustment)]
    [EffectTypeHint(EffectTypeHint.Unary | EffectTypeHint.Fast)]
    public sealed class DesaturateEffect
        : Effect
    {
        private UnaryPixelOps.Desaturate desaturateOp;

        public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, 
            Rectangle[] rois, int startIndex, int length)
        {
            this.desaturateOp.Apply(dstArgs.Surface, srcArgs.Surface, rois, startIndex, length);
        }

        public DesaturateEffect()
            : base(PdnResources.GetString("DesaturateEffect.Name"),
                   PdnResources.GetImage("Icons.DesaturateEffect.png"))
        {
            this.desaturateOp = new UnaryPixelOps.Desaturate();
        }
    }
}
