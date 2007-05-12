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
using System.Windows.Forms;

namespace PaintDotNet.Effects
{
    [EffectCategory(EffectCategory.Adjustment)]
    [EffectTypeHint(EffectTypeHint.Fast)]
    public sealed class AutoLevelEffect
        : Effect
    {
        private UnaryPixelOps.Level levels = null;

        public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, 
            Rectangle[] rois, int startIndex, int length)
        {
            if (levels == null) 
            {
                HistogramRgb histogram = new HistogramRgb();
                histogram.UpdateHistogram(srcArgs.Surface, this.EnvironmentParameters.GetSelection(dstArgs.Bounds));
                levels = histogram.MakeLevelsAuto();
            }

            if (levels.isValid)
            {
                levels.Apply(dstArgs.Surface, srcArgs.Surface, rois, startIndex, length);
            }
        }

        public AutoLevelEffect()
            : base(PdnResources.GetString("AutoLevel.Name"),
                   PdnResources.GetImage("Icons.AutoLevel.png")) 
        {
        }
    }
}
