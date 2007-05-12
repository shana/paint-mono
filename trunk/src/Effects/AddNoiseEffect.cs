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
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PaintDotNet.Effects
{
    [EffectTypeHint(EffectTypeHint.Unary | EffectTypeHint.Fast)]
    public sealed class AddNoiseEffect
        : Effect
    {
        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("AddNoiseEffect.Name");
            }
        }

        public static Image StaticImage
        {
            get
            {
                return PdnResources.GetImage("Icons.AddNoiseEffect.png");
            }
        }

        static AddNoiseEffect()
        {
            InitLookup();
        }

        public AddNoiseEffect()
            : base(StaticName, StaticImage, true)
        {
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            TwoAmountsConfigDialog tacd = new TwoAmountsConfigDialog();

            tacd.Text = StaticName;
            tacd.Icon = Utility.ImageToIcon(StaticImage);

            // Standard Deviation
            tacd.Amount1Default = 64;
            tacd.Amount1Label = PdnResources.GetString("AddNoiseEffect.Amount1Label");
            tacd.Amount1Maximum = 100;
            tacd.Amount1Minimum = 0;

            // Saturaion
            tacd.Amount2Default = 100;
            tacd.Amount2Label = PdnResources.GetString("AddNoiseEffect.Amount2Label");
            tacd.Amount2Maximum = 400;
            tacd.Amount2Minimum = 0;

            return tacd;
        }

        private const int tableSize = 16384;
        private static int[] lookup;

        private static double NormalCurve(double x, double scale)
        {
            return scale * Math.Exp(-x * x / 2);
        }

        private static void InitLookup()
        {
            int[] curve = new int[tableSize];
            int[] integral = new int[tableSize];

            double l = 5;
            double r = 10;
            double scale = 50;
            double sum = 0;

            while (r - l > 0.0000001)
            {
                sum = 0;
                scale = (l + r) * 0.5;

                for (int i = 0; i < tableSize; ++i)
                {
                    sum += NormalCurve(16.0 * ((double)i - tableSize / 2) / tableSize, scale);

                    if (sum > 1000000)
                    {
                        break;
                    }
                }

                if (sum > tableSize)
                {
                    r = scale;
                }
                else if (sum < tableSize)
                {
                    l = scale;
                }
                else
                {
                    break;
                }
            }

            lookup = new int[tableSize];
            sum = 0;
            int roundedSum = 0, lastRoundedSum;

            for (int i = 0; i < tableSize; ++i)
            {
                sum += NormalCurve(16.0 * ((double)i - tableSize / 2) / tableSize, scale);
                lastRoundedSum = roundedSum;
                roundedSum = (int)sum;

                for (int j = lastRoundedSum; j < roundedSum; ++j)
                {
                    lookup[j] = (i - tableSize / 2) * 65536 / tableSize;
                }
            }
        }

        [ThreadStatic]
        private static Random threadRand = new Random();
        
        public override unsafe void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, 
            Rectangle[] rois, int startIndex, int length)
        {
            TwoAmountsConfigToken tact = (TwoAmountsConfigToken)parameters;
            int dev = tact.Amount1 * tact.Amount1 / 64; //dev = target stddev / 16
            int sat = tact.Amount2 * 4096 / 100;

            if (threadRand == null)
            {
                threadRand = new Random(unchecked(System.Threading.Thread.CurrentThread.GetHashCode() ^ 
                    unchecked((int)DateTime.Now.Ticks)));
            }

            Random localRand = threadRand;
            int[] localLookup = lookup;

            for (int ri = startIndex; ri < startIndex + length; ++ri)
            {
                Rectangle rect = rois[ri];

                for (int y = rect.Top; y < rect.Bottom; ++y)
                {
                    ColorBgra *srcPtr = srcArgs.Surface.GetPointAddressUnchecked(rect.Left, y);
                    ColorBgra *dstPtr = dstArgs.Surface.GetPointAddressUnchecked(rect.Left, y);

                    for (int x = 0; x < rect.Width; ++x)
                    {
                        int r;
                        int g;
                        int b;
                        int i;

                        r = localLookup[localRand.Next(tableSize)];
                        g = localLookup[localRand.Next(tableSize)];
                        b = localLookup[localRand.Next(tableSize)];

                        i = (1867 * r + 9618 * g + 4899 * b) >> 14;

                        r = i + (((r - i) * sat) >> 12);
                        g = i + (((g - i) * sat) >> 12);
                        b = i + (((b - i) * sat) >> 12);

                        dstPtr->R = Utility.ClampToByte(srcPtr->R + ((r * dev * 16 + 32768) >> 16));
                        dstPtr->G = Utility.ClampToByte(srcPtr->G + ((g * dev * 16 + 32768) >> 16));
                        dstPtr->B = Utility.ClampToByte(srcPtr->B + ((b * dev * 16 + 32768) >> 16));
                        dstPtr->A = srcPtr->A;

                        ++srcPtr;
                        ++dstPtr;
                    }
                }
            }
        }
    }
}