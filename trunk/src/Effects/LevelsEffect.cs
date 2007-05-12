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
    public sealed class LevelsEffect 
        : Effect
    {
        public override EffectConfigDialog CreateConfigDialog()
        {
            return new LevelsEffectConfigDialog();
        }

        public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
        {
            UnaryPixelOps.Level levels = (parameters as LevelsEffectConfigToken).Levels;
            levels.Apply(dstArgs.Surface, srcArgs.Surface, rois, startIndex, length);
        }

        public LevelsEffect()
            :
            base(PdnResources.GetString("LevelsEffect.Name"),
                 PdnResources.GetImage("Icons.LevelsEffect.png"),
                 true)
        {
        }
    }
}
