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
    public sealed class MotionBlurEffect
        : Effect
    {
        public MotionBlurEffect()
            : base(PdnResources.GetString("MotionBlurEffect.Name"),
                   PdnResources.GetImage("Icons.MotionBlurEffect.png"),
                   PdnResources.GetString("Effects.Blurring.Submenu.Name"),
                   EffectDirectives.None,
                   true)
        {
        }

        public override EffectConfigDialog CreateConfigDialog()
        {
            return new MotionBlurEffectConfigDialog();
        }

        private unsafe ColorBgra DoLineAverage(Point[] points, int x, int y, Surface dst, Surface src)
        {
            long bSum = 0;
            long gSum = 0;
            long rSum = 0;
            long aSum = 0;
            int cDiv = 0;
            int aDiv = 0;
                        
            foreach (Point p in points)
            {
                Point srcPoint = new Point(x + p.X, y + p.Y);

                if (src.Bounds.Contains(srcPoint))
                {
                    ColorBgra c = src.GetPointUnchecked(srcPoint.X, srcPoint.Y);

                    bSum += c.B * c.A;
                    gSum += c.G * c.A;
                    rSum += c.R * c.A;
                    aSum += c.A;

                    aDiv++;
                    cDiv += c.A;
                }
            }

            int b;
            int g;
            int r;
            int a;

            if (cDiv == 0)
            {
                b = 0;
                g = 0;
                r = 0;
                a = 0;
            }
            else
            {
                b = (int)(bSum /= cDiv);
                g = (int)(gSum /= cDiv);
                r = (int)(rSum /= cDiv);
                a = (int)(aSum /= aDiv);
            }

            return ColorBgra.FromBgra((byte)b, (byte)g, (byte)r, (byte)a);
        }

        private unsafe ColorBgra DoLineAverageUnclipped(Point[] points, int x, int y, Surface dst, Surface src)
        {
            long bSum = 0;
            long gSum = 0;
            long rSum = 0;
            long aSum = 0;
            int cDiv = 0;
            int aDiv = 0;
                        
            foreach (Point p in points)
            {
                Point srcPoint = new Point(x + p.X, y + p.Y);
                ColorBgra c = src.GetPointUnchecked(srcPoint.X, srcPoint.Y);

                bSum += c.B * c.A;
                gSum += c.G * c.A;
                rSum += c.R * c.A;
                aSum += c.A;

                aDiv++;
                cDiv += c.A;
            }

            int b;
            int g;
            int r;
            int a;

            if (cDiv == 0)
            {
                b = 0;
                g = 0;
                r = 0;
                a = 0;
            }
            else
            {
                b = (int)(bSum /= cDiv);
                g = (int)(gSum /= cDiv);
                r = (int)(rSum /= cDiv);
                a = (int)(aSum /= aDiv);
            }

            return ColorBgra.FromBgra((byte)b, (byte)g, (byte)r, (byte)a);
        }

        public override unsafe void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
        {
            Point[] points = ((MotionBlurEffectConfigToken)parameters).LinePoints;
            Surface dst = dstArgs.Surface;
            Surface src = srcArgs.Surface;

            for (int i = startIndex; i < startIndex + length; ++i)
            {
                Rectangle rect = rois[i];

                for (int y = rect.Top; y < rect.Bottom; ++y)
                {
                    ColorBgra *dstPtr = dst.GetPointAddress(rect.Left, y);

                    for (int x = rect.Left; x < rect.Right; ++x)
                    {
                        Point a = new Point(x + points[0].X, y + points[0].Y);
                        Point b = new Point(x + points[points.Length - 1].X, y + points[points.Length - 1].Y);

                        // If both ends of this line are in bounds, we don't need to do silly clipping
                        if (src.Bounds.Contains(a) && src.Bounds.Contains(b))
                        {
                            *dstPtr = DoLineAverageUnclipped(points, x, y, dst, src);
                        }
                        else
                        {
                            *dstPtr = DoLineAverage(points, x, y, dst, src);
                        }

                        ++dstPtr;
                    }
                }
            }
        }
    }
}