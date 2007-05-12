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
using System.Collections.Generic;
using System.Text;

namespace PaintDotNet.Effects
{
    public sealed class MedianEffect
        : LocalHistogramEffect
    {
        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("MedianEffect.Name");
            }
        }

        public static ImageResource StaticImage
        {
            get
            {
                return ImageResource.Get("Icons.MedianEffectIcon.png");
            }
        }

	    private int percentile;

        public MedianEffect() 
            : base(StaticName, 
                   StaticImage.Reference, 
                   PdnResources.GetString("Effects.Blurring.Submenu.Name"),
                   true)
        {
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
	        TwoAmountsConfigDialog tacd = new TwoAmountsConfigDialog();

	        tacd.Text = this.Name;

            tacd.Amount1Label = PdnResources.GetString("MedianEffect.ConfigDialog.RadiusLabel");
	        tacd.Amount1Default = 10;
	        tacd.Amount1Minimum = 1;
	        tacd.Amount1Maximum = 200;

            tacd.Amount2Label = PdnResources.GetString("MedianEffect.ConfigDialog.PercentileLabel");
	        tacd.Amount2Default = 50;
	        tacd.Amount2Minimum = 0;
	        tacd.Amount2Maximum = 100;

	        return tacd;
        }

        public unsafe override ColorBgra Apply(ColorBgra src, int area, int* hb, int* hg, int* hr, int* ha)
        {
	        ColorBgra c = GetPercentile(this.percentile, area, hb, hg, hr, ha);
            return c;
        }

        public unsafe override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
        {
	        TwoAmountsConfigToken token = (TwoAmountsConfigToken)parameters;
            this.percentile = token.Amount2;

	        foreach (Rectangle rect in rois)
	        {
		        RenderRect(token.Amount1, srcArgs.Surface, dstArgs.Surface, rect);
	        }
        }
    }
}
