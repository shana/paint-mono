/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet.Effects;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace PaintDotNet.Effects
{
    public abstract class LocalHistogramEffect 
        : Effect
    {
        protected internal LocalHistogramEffect(string name, Image image, string subMenuName, bool isConfigurable)
            : base(name, image, subMenuName, isConfigurable)
        {
        }

        protected internal LocalHistogramEffect(string name, Image image, bool isConfigurable)
            : base(name, image, isConfigurable)
        {
        }

        public abstract unsafe ColorBgra Apply(ColorBgra src, int area, int* hb, int* hg, int* hr, int* ha);

        public static unsafe ColorBgra GetPercentile(int percentile, int area, int* hb, int* hg, int* hr, int* ha)
        {
            int minCount = area * percentile / 100;

            int b = 0;
            int bCount = 0;

            while (b < 255 && hb[b] == 0)
            {
                ++b;
            }

            while (b < 255 && bCount < minCount)
            {
                bCount += hb[b];
                ++b;
            }

            int g = 0;
            int gCount = 0;

            while (g < 255 && hg[g] == 0)
            {
                ++g;
            }

            while (g < 255 && gCount < minCount)
            {
                gCount += hg[g];
                ++g;
            }

            int r = 0;
            int rCount = 0;

            while (r < 255 && hr[r] == 0)
            {
                ++r;
            }

            while (r < 255 && rCount < minCount)
            {
                rCount += hr[r];
                ++r;
            }

            int a = 0;
            int aCount = 0;

            while (a < 255 && ha[a] == 0)
            {
                ++a;
            }

            while (a < 255 && aCount < minCount)
            {
                aCount += ha[a];
                ++a;
            }

            return ColorBgra.FromBgra((byte)b, (byte)g, (byte)r, (byte)a);
        }

        public unsafe void RenderRect(
            int rad,
            Surface src,
            Surface dst,
            Rectangle rect)
        {
            int width = src.Width;
            int height = src.Height;

            int* leadingEdgeX = stackalloc int[rad + 1];
            int stride = src.Stride / sizeof(ColorBgra);

            // approximately (rad + 0.5)^2
            int cutoff = ((rad * 2 + 1) * (rad * 2 + 1) + 2) / 4;

            for (int v = 0; v <= rad; ++v)
            {
                for (int u = 0; u <= rad; ++u)
                {
                    if (u * u + v * v <= cutoff)
                    {
                        leadingEdgeX[v] = u;
                    }
                }
            }

            for (int y = rect.Top; y < rect.Bottom; y++)
            {
                int* hb = stackalloc int[256];
                int* hg = stackalloc int[256];
                int* hr = stackalloc int[256];
                int* ha = stackalloc int[256];
                int area = 0;

                ColorBgra* ps = src.GetPointAddressUnchecked(rect.Left, y);
                ColorBgra* pd = dst.GetPointAddressUnchecked(rect.Left, y);

                // assert: v + y >= 0
                int top = -Math.Min(rad, y);

                // assert: v + y <= height - 1
                int bottom = Math.Min(rad, height - 1 - y);

                // assert: u + x >= 0
                int left = -Math.Min(rad, rect.Left);

                // assert: u + x <= width - 1
                int right = Math.Min(rad, width - 1 - rect.Left);

                for (int v = top; v <= bottom; ++v)
                {
                    ColorBgra* psamp = src.GetPointAddressUnchecked(rect.Left + left, y + v);

                    for (int u = left; u <= right; ++u)
                    {
                        if ((u * u + v * v) <= cutoff)
                        {
                            ++area;
                            ++hb[psamp->B];
                            ++hg[psamp->G];
                            ++hr[psamp->R];
                            ++ha[psamp->A];
                        }

                        ++psamp;
                    }
                }

                for (int x = rect.Left; x < rect.Right; x++)
                {
                    *pd = Apply(*ps, area, hb, hg, hr, ha);

                    // assert: u + x >= 0
                    left = -Math.Min(rad, x);

                    // assert: u + x <= width - 1
                    right = Math.Min(rad + 1, width - 1 - x);

                    // Subtract trailing edge top half
                    int v = -1;

                    while (v >= top)
                    {
                        int u = leadingEdgeX[-v];

                        if (-u >= left)
                        {
                            break;
                        }

                        --v;
                    }

                    while (v >= top)
                    {
                        int u = leadingEdgeX[-v];
                        ColorBgra* p = unchecked(ps + (v * stride)) - u;

                        --hb[p->B];
                        --hg[p->G];
                        --hr[p->R];
                        --ha[p->A];
                        --area;

                        --v;
                    }

                    // add leading edge top half
                    v = -1;
                    while (v >= top)
                    {
                        int u = leadingEdgeX[-v];

                        if (u + 1 <= right)
                        {
                            break;
                        }

                        --v;
                    }

                    while (v >= top)
                    {
                        int u = leadingEdgeX[-v];
                        ColorBgra* p = unchecked(ps + (v * stride)) + u + 1;

                        ++hb[p->B];
                        ++hg[p->G];
                        ++hr[p->R];
                        ++ha[p->A];
                        ++area;

                        --v;
                    }

                    // Subtract trailing edge bottom half
                    v = 0;

                    while (v <= bottom)
                    {
                        int u = leadingEdgeX[v];

                        if (-u >= left)
                        {
                            break;
                        }

                        ++v;
                    }

                    while (v <= bottom)
                    {
                        int u = leadingEdgeX[v];
                        ColorBgra* p = ps + v * stride - u;

                        --hb[p->B];
                        --hg[p->G];
                        --hr[p->R];
                        --ha[p->A];
                        --area;

                        ++v;
                    }

                    // add leading edge bottom half
                    v = 0;

                    while (v <= bottom)
                    {
                        int u = leadingEdgeX[v];

                        if (u + 1 <= right)
                        {
                            break;
                        }

                        ++v;
                    }

                    while (v <= bottom)
                    {
                        int u = leadingEdgeX[v];
                        ColorBgra* p = ps + v * stride + u + 1;

                        ++hb[p->B];
                        ++hg[p->G];
                        ++hr[p->R];
                        ++ha[p->A];
                        ++area;

                        ++v;
                    }

                    ++ps;
                    ++pd;
                }
            }
        }
    }    
}
