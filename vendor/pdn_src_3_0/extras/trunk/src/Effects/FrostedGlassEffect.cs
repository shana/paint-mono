/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

// Original C++ implementation by Jason Waltman as part of "Filter Explorer," 
// http://www.jasonwaltman.com/thesis/index.html

using PaintDotNet;
using PaintDotNet.Effects;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PaintDotNet.Effects
{
    public sealed class FrostedGlassEffect
        : Effect
    {
        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("FrostedGlassEffect.Name");
            }
        }

        public static Image StaticImage
        {
            get
            {
                return PdnResources.GetImage("Icons.FrostedGlassEffect.png");
            }
        }

        private Random random = new Random();

        public FrostedGlassEffect() 
            : base(StaticName, 
                   StaticImage,
                   null,
                   EffectDirectives.None,
                   true)
        {
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            AmountEffectConfigDialog aecd = new AmountEffectConfigDialog();
            aecd.SliderMinimum = 1;
            aecd.SliderMaximum = 10;
            aecd.Text = StaticName;
            aecd.SliderLabel = PdnResources.GetString("FrostedGlassEffect.ConfigDialog.SliderLabel");
            aecd.SliderUnitsName = PdnResources.GetString("FrostedGlassEffect.ConfigDialog.SliderUnitsName");

            aecd.Icon = Utility.ImageToIcon(StaticImage, true);
            return aecd;
        }

        public unsafe override void Render(EffectConfigToken token, RenderArgs dstArgs, RenderArgs srcArgs, 
            Rectangle[] rois, int startIndex, int length)
        {
            AmountEffectConfigToken realToken = (AmountEffectConfigToken)token;
            Surface src = srcArgs.Surface;
            Surface dst = dstArgs.Surface;

            int width = src.Width;
            int height = src.Height;
            int r = realToken.Amount;
            Random localRandom = this.random;

            int* intensityCount = stackalloc int[256];
            uint* avgRed = stackalloc uint[256];
            uint* avgGreen = stackalloc uint[256];
            uint* avgBlue = stackalloc uint[256];
            uint* avgAlpha = stackalloc uint[256];
            byte* intensityChoices = stackalloc byte[(1 + (r * 2)) * (1 + (r * 2))];

            for (int ri = startIndex; ri < startIndex + length; ++ri)
            {
                Rectangle rect = rois[ri];

                int rectTop = rect.Top;
                int rectBottom = rect.Bottom;
                int rectLeft = rect.Left;
                int rectRight = rect.Right;

                for (int y = rectTop; y < rectBottom; ++y)
                {
                    ColorBgra *dstPtr = dst.GetPointAddress(rect.Left, y);
                    int top = y - r;
                    int bottom = y + r + 1;

                    if (top < 0)
                    {
                        top = 0;
                    }

                    if (bottom > height)
                    {
                        bottom = height;
                    }

                    for (int x = rectLeft; x < rectRight; ++x)
                    {
                        int intensityChoicesIndex = 0;

                        for (int i = 0; i < 256; ++i)
                        {
                            intensityCount[i] = 0;
                            avgRed[i] = 0;
                            avgGreen[i] = 0;
                            avgBlue[i] = 0;
                            avgAlpha[i] = 0;
                        }

                        int left = x - r;
                        int right = x + r + 1;

                        if (left < 0)
                        {
                            left = 0;
                        }

                        if (right > width)
                        {
                            right = width;
                        }

                        for (int j = top; j < bottom; ++j)
                        {
                            if (j < 0 || j >= height)
                            {
                                continue;
                            }

                            ColorBgra *srcPtr = src.GetPointAddressUnchecked(left, j);

                            for (int i = left; i < right; ++i)
                            {
                                byte intensity = srcPtr->GetIntensityByte();

                                intensityChoices[intensityChoicesIndex] = intensity;
                                ++intensityChoicesIndex;

                                ++intensityCount[intensity];

                                avgRed[intensity] += srcPtr->R;
                                avgGreen[intensity] += srcPtr->G;
                                avgBlue[intensity] += srcPtr->B;
                                avgAlpha[intensity] += srcPtr->A;

                                ++srcPtr;
                            }
                        }

                        int randNum;

                        lock (localRandom)
                        {
                            randNum = localRandom.Next(intensityChoicesIndex);
                        }

                        byte chosenIntensity = intensityChoices[randNum];

                        byte R = (byte)(avgRed[chosenIntensity] / intensityCount[chosenIntensity]);
                        byte G = (byte)(avgGreen[chosenIntensity] / intensityCount[chosenIntensity]);
                        byte B = (byte)(avgBlue[chosenIntensity] / intensityCount[chosenIntensity]);
                        byte A = (byte)(avgAlpha[chosenIntensity] / intensityCount[chosenIntensity]);

                        *dstPtr = ColorBgra.FromBgra(B, G, R, A);
                        ++dstPtr;

                        // prepare the array for the next loop iteration
                        for (int i = 0; i < intensityChoicesIndex; ++i)
                        {
                            intensityChoices[i] = 0;
                        }
                    }
                }
            }
        }
    }
}
