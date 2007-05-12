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
    public sealed class BlurEffect
        : Effect
    {
        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("BlurEffect.Name");
            }
        }

        public BlurEffect()
            : base(StaticName,
                   PdnResources.GetImage("Icons.BlurEffect.png"),
                   PdnResources.GetString("Effects.Blurring.Submenu.Name"),
                   EffectDirectives.None,
                   true)
        {
        }

        public override EffectConfigDialog CreateConfigDialog()
        {   
            AmountEffectConfigDialog aecg = new AmountEffectConfigDialog();
            aecg.Effect = this;
            aecg.Text = StaticName;
            aecg.SliderMinimum = 1;
            aecg.SliderMaximum = 200;
            aecg.SliderLabel = PdnResources.GetString("BlurEffect.ConfigDialog.SliderLabel");
            aecg.SliderUnitsName = PdnResources.GetString("BlurEffect.ConfigDialog.SliderUnitsName");

            aecg.Icon = PdnResources.GetIconFromImage("Icons.BlurEffect.png");
            return aecg;
        }

        public static int[] CreateGaussianBlurRow(int amount)
        {
            int size = 1 + (amount * 2);
            int[] weights = new int[size];

            for (int i = 0; i <= amount; ++i)
            {
                // 1 + aa - aa + 2ai - ii
                weights[i] = 16 * (i + 1);
                weights[weights.Length - i - 1] = weights[i];
            }

            return weights;
        }

        public static int[][] CreateGaussianBlurMatrix(int amount)
        {
            int size = 1 + (amount * 2);
            int center = size / 2;
            int[][] weights = new int[size][];

            for (int i = 0; i < size; ++i)
            {
                weights[i] = new int[size];

                for (int j = 0; j < size; ++j)
                {
                    weights[i][j] = (int)(16 * Math.Sqrt(((j - center) * (j - center)) + ((i - center) * (i - center))));
                }
            }

            int max = 0;
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    if (weights[i][j] > max)
                    {
                        max = weights[i][j];
                    }
                }
            }

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    weights[i][j] = max - weights[i][j];
                }
            }

            return weights;
        }

        public override unsafe void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
        {
            AmountEffectConfigToken bect = (AmountEffectConfigToken)parameters;

            Surface dst = dstArgs.Surface;
            Surface src = srcArgs.Surface;

            int r = bect.Amount;
            int[] w = CreateGaussianBlurRow(bect.Amount);
            int wlen = w.Length;

            for (int ri = startIndex; ri < startIndex + length; ++ri)
            {
                Rectangle rect = rois[ri];

                if (rect.Height >= 1 && rect.Width >= 1)
                {
                    for (int y = rect.Top; y < rect.Bottom; ++y)
                    {
                        long* waSums = stackalloc long[wlen];
                        long* wcSums = stackalloc long[wlen];
                        long* aSums = stackalloc long[wlen];
                        long* bSums = stackalloc long[wlen];
                        long* gSums = stackalloc long[wlen];
                        long* rSums = stackalloc long[wlen];
                        long waSum = 0;
                        long wcSum = 0;
                        long aSum = 0;
                        long bSum = 0;
                        long gSum = 0;
                        long rSum = 0;
                        ColorBgra *dstPtr = dst.GetPointAddressUnchecked(rect.Left, y);

                        for (int wx = 0; wx < wlen; ++wx)
                        {
                            int srcX = rect.Left + wx - r;
                            waSums[wx] = 0;
                            wcSums[wx] = 0;
                            aSums[wx] = 0;
                            bSums[wx] = 0;
                            gSums[wx] = 0;
                            rSums[wx] = 0;

                            if (srcX >= 0 && srcX < src.Width)
                            {
                                for (int wy = 0; wy < wlen; ++wy)
                                {
                                    int srcY = y + wy - r;

                                    if (srcY >= 0 && srcY < src.Height)
                                    {
                                        ColorBgra c = src.GetPointUnchecked(srcX, srcY);
                                        int wp = w[wy];

                                        waSums[wx] += wp;
                                        wp *= c.A + (c.A >> 7);
                                        wcSums[wx] += wp;
                                        wp >>= 8;

                                        aSums[wx] += wp * c.A;
                                        bSums[wx] += wp * c.B;
                                        gSums[wx] += wp * c.G;
                                        rSums[wx] += wp * c.R;
                                    }
                                }

                                int wwx = w[wx];
                                waSum += wwx * waSums[wx];
                                wcSum += wwx * wcSums[wx];
                                aSum += wwx * aSums[wx];
                                bSum += wwx * bSums[wx];
                                gSum += wwx * gSums[wx];
                                rSum += wwx * rSums[wx];
                            }
                        }

                        wcSum >>= 8;

                        if (waSum == 0 || wcSum == 0)
                        {
                            dstPtr->Bgra = 0;
                        }
                        else
                        {
                            int alpha = (int)(aSum / waSum);
                            int blue = (int)(bSum / wcSum);
                            int green = (int)(gSum / wcSum);
                            int red = (int)(rSum / wcSum);

                            dstPtr->Bgra = ColorBgra.BgraToUInt32(blue, green, red, alpha);
                        }

                        ++dstPtr;

                        for (int x = rect.Left + 1; x < rect.Right; ++x)
                        {
                            for (int i = 0; i < wlen - 1; ++i)
                            {
                                waSums[i] = waSums[i + 1];
                                wcSums[i] = wcSums[i + 1];
                                aSums[i] = aSums[i + 1];
                                bSums[i] = bSums[i + 1];
                                gSums[i] = gSums[i + 1];
                                rSums[i] = rSums[i + 1];
                            }

                            waSum = 0;
                            wcSum = 0;
                            aSum = 0;
                            bSum = 0;
                            gSum = 0;
                            rSum = 0;

                            int wx;
                            for (wx = 0; wx < wlen - 1; ++wx)
                            {
                                long wwx = (long)w[wx];
                                waSum += wwx * waSums[wx];
                                wcSum += wwx * wcSums[wx];
                                aSum += wwx * aSums[wx];
                                bSum += wwx * bSums[wx];
                                gSum += wwx * gSums[wx];
                                rSum += wwx * rSums[wx];
                            }

                            wx = wlen - 1;

                            waSums[wx] = 0;
                            wcSums[wx] = 0;
                            aSums[wx] = 0;
                            bSums[wx] = 0;
                            gSums[wx] = 0;
                            rSums[wx] = 0;

                            int srcX = x + wx - r;

                            if (srcX >= 0 && srcX < src.Width)
                            {
                                for (int wy = 0; wy < wlen; ++wy)
                                {
                                    int srcY = y + wy - r;

                                    if (srcY >= 0 && srcY < src.Height)
                                    {
                                        ColorBgra c = src.GetPointUnchecked(srcX, srcY);
                                        int wp = w[wy];

                                        waSums[wx] += wp;
                                        wp *= c.A + (c.A >> 7);
                                        wcSums[wx] += wp;
                                        wp >>= 8;

                                        aSums[wx] += wp * (long)c.A;
                                        bSums[wx] += wp * (long)c.B;
                                        gSums[wx] += wp * (long)c.G;
                                        rSums[wx] += wp * (long)c.R;
                                    }
                                }

                                int wr = w[wx];
                                waSum += (long)wr * waSums[wx];
                                wcSum += (long)wr * wcSums[wx];
                                aSum += (long)wr * aSums[wx];
                                bSum += (long)wr * bSums[wx];
                                gSum += (long)wr * gSums[wx];
                                rSum += (long)wr * rSums[wx];
                            }

                            wcSum >>= 8;

                            if (waSum == 0 || wcSum == 0)
                            {
                                dstPtr->Bgra = 0;
                            }
                            else
                            {
                                int alpha = (int)(aSum / waSum);
                                int blue = (int)(bSum / wcSum);
                                int green = (int)(gSum / wcSum);
                                int red = (int)(rSum / wcSum);

                                dstPtr->Bgra = ColorBgra.BgraToUInt32(blue, green, red, alpha); 
                            }

                            ++dstPtr;
                        }
                    }
                }
            }
        }
    }
}
