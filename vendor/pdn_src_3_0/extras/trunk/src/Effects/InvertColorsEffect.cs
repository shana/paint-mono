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
using System.Windows.Forms;

namespace PaintDotNet.Effects
{
    [EffectCategory(EffectCategory.Adjustment)]
    [EffectTypeHint(EffectTypeHint.Unary | EffectTypeHint.Fast)]
    public sealed class InvertColorsEffect
        : Effect
    {
        private UnaryPixelOps.Invert invertOp;

        public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, 
            Rectangle[] rois, int startIndex, int length)
        {
            this.invertOp.Apply(dstArgs.Surface, srcArgs.Surface, rois, startIndex, length);
        }

        public InvertColorsEffect()
            : base(PdnResources.GetString("InvertColorsEffect.Name"),
                   PdnResources.GetImage("Icons.InvertColorsEffect.png"))
        {
            this.invertOp = new UnaryPixelOps.Invert();
        }
    }
}
