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
    public sealed class SepiaEffect
        : Effect
    {
        private UnaryPixelOp levels;
        private UnaryPixelOp desaturate;

        public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, 
            Rectangle[] rois, int startIndex, int length)
        {
            this.desaturate.Apply(dstArgs.Surface, srcArgs.Surface, rois, startIndex, length);
            this.levels.Apply(dstArgs.Surface, dstArgs.Surface, rois, startIndex, length);
        }

        public SepiaEffect()
            : base(PdnResources.GetString("SepiaEffect.Name"),
                   PdnResources.GetImage("Icons.SepiaEffect.png"))
        {
            this.desaturate = new UnaryPixelOps.Desaturate();

            this.levels = new UnaryPixelOps.Level(
                ColorBgra.Black, 
                ColorBgra.White,
                new float[] { 1.2f, 1.0f, 0.8f },
                ColorBgra.Black,
                ColorBgra.White);
        }
    }
}
