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
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PaintDotNet.Effects
{
    [EffectCategory(EffectCategory.Adjustment)]
    [EffectTypeHint(EffectTypeHint.Unary | EffectTypeHint.Fast)]
    public sealed class BrightnessAndContrastAdjustment
        : Effect
    {
        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("BrightnessAndContrastAdjustment.Name");
            }
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            return new BrightnessAndContrastAdjustmentConfigDialog();
        }

        public unsafe override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
        {
            BrightnessAndContrastAdjustmentConfigToken token = (BrightnessAndContrastAdjustmentConfigToken)parameters;
            int contrast = token.Contrast;
            int brightness = token.Brightness;
            int multiply = token.Multiply;
            int divide = token.Divide;
            byte[] rgbTable = token.RgbTable;

            for (int r = startIndex; r < startIndex + length; ++r)
            {
                Rectangle rect = rois[r];

                for (int y = rect.Top; y < rect.Bottom; ++y)
                {
                    ColorBgra *srcRowPtr = srcArgs.Surface.GetPointAddress(rect.Left, y);
                    ColorBgra *dstRowPtr = dstArgs.Surface.GetPointAddress(rect.Left, y);
                    ColorBgra *dstRowEndPtr = dstRowPtr + rect.Width;

                    if (divide == 0)
                    {
                        while (dstRowPtr < dstRowEndPtr)
                        {
                            ColorBgra col = *srcRowPtr;
                            int i = col.GetIntensityByte();
                            uint c = rgbTable[i];
                            dstRowPtr->Bgra = (col.Bgra & 0xff000000) | c | (c << 8) | (c << 16);

                            ++dstRowPtr;
                            ++srcRowPtr;
                        }
                    }
                    else
                    {
                        while (dstRowPtr < dstRowEndPtr)
                        {
                            ColorBgra col = *srcRowPtr;
                            int i = col.GetIntensityByte();
                            int shiftIndex = i * 256;

                            col.R = rgbTable[shiftIndex + col.R];
                            col.G = rgbTable[shiftIndex + col.G]; 
                            col.B = rgbTable[shiftIndex + col.B];

                            *dstRowPtr = col;
                            ++dstRowPtr;
                            ++srcRowPtr;
                        }
                    }
                }
            }
            
            return;
        }

        public BrightnessAndContrastAdjustment()
            : base(StaticName,
                   PdnResources.GetImage("Icons.BrightnessAndContrastAdjustment.png"),
                   true)
        {
        }
    }
}
