/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

// Original C++ implementation by Jason Waltman as part of "Filter Explorer," http://www.jasonwaltman.com/thesis/index.html

using PaintDotNet;
using PaintDotNet.Effects;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PaintDotNet.Effects
{
    public sealed class OilPaintingEffect
        : Effect
    {
        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("OilPaintingEffect.Name");
            }
        }

        public OilPaintingEffect()
            : base(StaticName,
                   PdnResources.GetImage("Icons.OilPaintingEffect.png"),
                   null,
                   EffectDirectives.None,
                   true)
        {
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            TwoAmountsConfigDialog tacg = new TwoAmountsConfigDialog();

            tacg.Text = StaticName;

            tacg.Amount1Minimum = 1;
            tacg.Amount1Maximum = 8;
            tacg.Amount1Default = 3;
            tacg.Amount1Label = PdnResources.GetString("OilPaintingEffect.ConfigDialog.Amount1Label");
            
            tacg.Amount2Minimum = 3;
            tacg.Amount2Maximum = 255;
            tacg.Amount2Default = 50;
            tacg.Amount2Label = PdnResources.GetString("OilPaintingEffect.ConfigDialog.Amount2Label");

            tacg.Icon = PdnResources.GetIconFromImage("Icons.OilPaintingEffect.png");

            return tacg;
        }

        public unsafe override void Render(EffectConfigToken properties, RenderArgs dstArgs, RenderArgs srcArgs, 
            Rectangle[] rois, int startIndex, int length)
        {
            TwoAmountsConfigToken token = (TwoAmountsConfigToken)properties;
            int brushSize = token.Amount1;
            byte smoothness = (byte)token.Amount2;
            Surface src = srcArgs.Surface;
            Surface dst = dstArgs.Surface;
            int width = src.Width;
            int height = src.Height;

            int arrayLens = 1 + smoothness;
            int* intensityCount = stackalloc int[arrayLens];
            uint* avgRed = stackalloc uint[arrayLens];
            uint* avgGreen = stackalloc uint[arrayLens];
            uint* avgBlue = stackalloc uint[arrayLens];
            uint* avgAlpha = stackalloc uint[arrayLens];

            byte maxIntensity = smoothness;

            for (int r = startIndex; r < startIndex + length; ++r)
            {
                Rectangle rect = rois[r];

                int rectTop = rect.Top;
                int rectBottom = rect.Bottom;
                int rectLeft = rect.Left;
                int rectRight = rect.Right;

                for (int y = rectTop; y < rectBottom; ++y)
                {
                    ColorBgra *dstPtr = dst.GetPointAddress(rect.Left, y);

                    int top = y - brushSize;
                    int bottom = y + brushSize + 1;

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
                        int left = x - brushSize;
                        int right = x + brushSize + 1;

                        if (left < 0)
                        {
                            left = 0;
                        }

                        if (right > width)
                        {
                            right = width;
                        }

                        for (int i = 0; i < arrayLens; ++i)
                        {
                            intensityCount[i] = 0;
                            avgRed[i] = 0;
                            avgGreen[i] = 0;
                            avgBlue[i] = 0;
                            avgAlpha[i] = 0;
                        }

                        int numInt = 0;

                        for (int j = top; j < bottom; ++j)
                        {
                            ColorBgra *srcPtr = src.GetPointAddress(left, j);

                            for (int i = left; i < right; ++i)
                            {
                                byte intensity = (byte)((srcPtr->GetIntensityByte() * maxIntensity) / 255);
                                ++intensityCount[intensity];
                                ++numInt;

                                avgRed[intensity] += srcPtr->R;
                                avgGreen[intensity] += srcPtr->G;
                                avgBlue[intensity] += srcPtr->B;
                                avgAlpha[intensity] += srcPtr->A;

                                ++srcPtr;
                            }
                        }

                        byte chosenIntensity = 0;
                        int maxInstance = 0;

                        for (int i = 0; i <= maxIntensity; ++i)
                        {
                            if (intensityCount[i] > maxInstance)
                            {
                                chosenIntensity = (byte)i;
                                maxInstance = intensityCount[i];
                            }
                        }

                        byte R = (byte)(avgRed[chosenIntensity] / maxInstance);
                        byte G = (byte)(avgGreen[chosenIntensity] / maxInstance);
                        byte B = (byte)(avgBlue[chosenIntensity] / maxInstance);
                        byte A = (byte)(avgAlpha[chosenIntensity] / maxInstance);

                        *dstPtr = ColorBgra.FromBgra(B, G, R, A); 
                        ++dstPtr;
                    }
                }
            }
        }

    }
}
