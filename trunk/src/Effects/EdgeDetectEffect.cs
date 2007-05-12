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
    public sealed class EdgeDetectEffect
        : ColorDifferenceEffect
    {
        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("EdgeDetectEffect.Name");
            }
        }

        public EdgeDetectEffect()
            : base(StaticName,
                   PdnResources.GetImage("Icons.EdgeDetectEffect.png"),
                   true)
        {
        }

        public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
        {
            EdgeDetectConfigToken token = (EdgeDetectConfigToken)parameters;
            base.RenderColorDifferenceEffect(token.Weights, dstArgs, srcArgs, rois, startIndex, length);
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            return new EdgeDetectConfigDialog();
        }
    }
}
