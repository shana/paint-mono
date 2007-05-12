/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet.SystemLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace PaintDotNet.Tools
{
    public abstract class FloodToolBase
        : Tool
    {
        private bool contiguous;

        private bool limitToSelection = true;
        protected bool LimitToSelection
        {
            get 
            {
                return limitToSelection;
            }

            set
            {
                limitToSelection = value;
            }
        }

        public FloodToolBase(DocumentWorkspace documentWorkspace, ImageResource toolBarImage, string name, 
            string helpText, char hotKey, bool skipIfActiveOnHotKey, ToolBarConfigItems toolBarConfigItems)
            : base(documentWorkspace, toolBarImage, name, helpText, hotKey, skipIfActiveOnHotKey, 
              ToolBarConfigItems.Tolerance | toolBarConfigItems)
        {
        }

        private static bool CheckColor(ColorBgra a, ColorBgra b, int tolerance)
        {
            int sum = 0;
            int diff;

            diff = a.R - b.R;
            sum += (1 + diff * diff) * a.A / 256;

            diff = a.G - b.G;
            sum += (1 + diff * diff) * a.A / 256;

            diff = a.B - b.B;
            sum += (1 + diff * diff) * a.A / 256;

            diff = a.A - b.A;
            sum += diff * diff;

            return (sum <= tolerance * tolerance * 4);
        }

        public unsafe static void FillStencilByColor(Surface surface, IBitVector2D stencil, ColorBgra cmp, int tolerance, 
            out Rectangle boundingBox, PdnRegion limitRegion, bool limitToSelection)
        {
            int top = int.MaxValue;
            int bottom = int.MinValue;
            int left = int.MaxValue;
            int right = int.MinValue;
            Rectangle[] scans;
            
            stencil.Clear(false);

            if (limitToSelection)
            {
                using (PdnRegion excluded = new PdnRegion(new Rectangle(0, 0, stencil.Width, stencil.Height)))
                {
                    excluded.Xor(limitRegion);
                    scans = excluded.GetRegionScansReadOnlyInt();
                }
            }
            else
            {
                scans = new Rectangle[0];
            }

            foreach (Rectangle rect in scans)
            {
                stencil.Set(rect, true);
            }

            for (int y = 0; y < surface.Height; ++y)
            {
                bool foundPixelInRow = false;
                ColorBgra *ptr = surface.GetRowAddressUnchecked(y);
            
                for (int x = 0; x < surface.Width; ++x)
                {
                    if (CheckColor(cmp, *ptr, tolerance))
                    {
                        stencil.SetUnchecked(x, y, true);

                        if (x < left)
                        {
                            left = x;
                        }

                        if (x > right)
                        {
                            right = x;
                        }

                        foundPixelInRow = true;
                    }

                    ++ptr;
                }

                if (foundPixelInRow)
                {
                    if (y < top)
                    {
                        top = y;
                    }

                    if (y >= bottom)
                    {
                        bottom = y;
                    }
                }
            }

            foreach (Rectangle rect in scans)
            {
                stencil.Set(rect, false);
            }

            boundingBox = Rectangle.FromLTRB(left, top, right + 1, bottom + 1);
        }
        
        public unsafe static void FillStencilFromPoint(Surface surface, IBitVector2D stencil, Point start, 
            int tolerance, out Rectangle boundingBox, PdnRegion limitRegion, bool limitToSelection)
        {
            ColorBgra cmp = surface[start];
            int top = int.MaxValue;
            int bottom = int.MinValue;
            int left = int.MaxValue;
            int right = int.MinValue;
            Rectangle[] scans;
            
            stencil.Clear(false);

            if (limitToSelection)
            {
                using (PdnRegion excluded = new PdnRegion(new Rectangle(0, 0, stencil.Width, stencil.Height)))
                {
                    excluded.Xor(limitRegion);
                    scans = excluded.GetRegionScansReadOnlyInt();
                }
            }
            else
            {
                scans = new Rectangle[0];
            }

            foreach (Rectangle rect in scans)
            {
                stencil.Set(rect, true);
            }

            Queue<Point> queue = new Queue<Point>(16);
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                Point pt = queue.Dequeue();

                ColorBgra* rowPtr = surface.GetRowAddressUnchecked(pt.Y);
                int localLeft = pt.X - 1;
                int localRight = pt.X;

                while (localLeft >= 0 &&
                       !stencil.GetUnchecked(localLeft, pt.Y) &&
                       CheckColor(cmp, rowPtr[localLeft], tolerance))
                {
                    stencil.SetUnchecked(localLeft, pt.Y, true);
                    --localLeft;
                }

                while (localRight < surface.Width &&
                       !stencil.GetUnchecked(localRight, pt.Y) &&
                       CheckColor(cmp, rowPtr[localRight], tolerance))
                {
                    stencil.SetUnchecked(localRight, pt.Y, true);
                    ++localRight;
                }

                ++localLeft;
                --localRight;

                if (pt.Y > 0)
                {
                    int sleft = localLeft;
                    int sright = localLeft;
                    ColorBgra* rowPtrUp = surface.GetRowAddressUnchecked(pt.Y - 1);

                    for (int sx = localLeft; sx <= localRight; ++sx)
                    {
                        if (!stencil.GetUnchecked(sx, pt.Y - 1) &&
                            CheckColor(cmp, rowPtrUp[sx], tolerance))
                        {
                            ++sright;
                        }
                        else
                        {
                            if (sright - sleft > 0)
                            {
                                queue.Enqueue(new Point(sleft, pt.Y - 1));
                            }

                            ++sright;
                            sleft = sright;
                        }
                    }

                    if (sright - sleft > 0)
                    {
                        queue.Enqueue(new Point(sleft, pt.Y - 1));
                    }
                }

                if (pt.Y < surface.Height - 1)
                {
                    int sleft = localLeft;
                    int sright = localLeft;
                    ColorBgra* rowPtrDown = surface.GetRowAddressUnchecked(pt.Y + 1);

                    for (int sx = localLeft; sx <= localRight; ++sx)
                    {
                        if (!stencil.GetUnchecked(sx, pt.Y + 1) &&
                            CheckColor(cmp, rowPtrDown[sx], tolerance))
                        {
                            ++sright;
                        }
                        else
                        {
                            if (sright - sleft > 0)
                            {
                                queue.Enqueue(new Point(sleft, pt.Y + 1));
                            }

                            ++sright;
                            sleft = sright;
                        }
                    }

                    if (sright - sleft > 0)
                    {
                        queue.Enqueue(new Point(sleft, pt.Y + 1));
                    }
                }

                if (localLeft < left)
                {
                    left = localLeft;
                }

                if (localRight > right)
                {
                    right = localRight;
                }

                if (pt.Y < top)
                {
                    top = pt.Y;
                }

                if (pt.Y > bottom)
                {
                    bottom = pt.Y;
                }
            }

            foreach (Rectangle rect in scans)
            {
                stencil.Set(rect, false);
            }

            boundingBox = Rectangle.FromLTRB(left, top, right + 1, bottom + 1);
        }

        protected abstract void OnFillRegionComputed(Point[][] polygonSet);

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Point pos = new Point(e.X, e.Y);
            
            this.contiguous = ((ModifierKeys & Keys.Shift) == 0);

            if (Document.Bounds.Contains(pos))
            {
                base.OnMouseDown(e);

                PdnRegion currentRegion = Selection.CreateRegion();

                // See if the mouse click is valid
                if (!currentRegion.IsVisible(pos) && limitToSelection)
                {
                    currentRegion.Dispose();
                    currentRegion = null;
                    return;
                }
            
                // Set the current surface, color picked and color to draw
                Surface surface = ((BitmapLayer)ActiveLayer).Surface;

                IBitVector2D stencilBuffer = new BitVector2DSurfaceAdapter(this.ScratchSurface);

                Rectangle boundingBox;
                int tolerance = (int)(AppEnvironment.Tolerance * AppEnvironment.Tolerance * 256);

                if (contiguous)
                {
                    FillStencilFromPoint(surface, stencilBuffer, pos, tolerance, out boundingBox, currentRegion, limitToSelection);
                }
                else
                {
                    FillStencilByColor(surface, stencilBuffer, surface[pos], tolerance, out boundingBox, currentRegion, limitToSelection);
                }

                Point[][] polygonSet = PdnGraphicsPath.PolygonSetFromStencil(stencilBuffer, boundingBox, 0, 0);
                OnFillRegionComputed(polygonSet);
            }

            base.OnMouseDown(e);
        }
   }
}
