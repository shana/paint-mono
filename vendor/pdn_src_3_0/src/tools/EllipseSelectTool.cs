/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace PaintDotNet.Tools
{
    public class EllipseSelectTool
        : SelectionTool
    {
        private Cursor cursorMouseUp, cursorMouseDown;

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            Cursor = cursorMouseDown;
            base.OnMouseDown (e);
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            Cursor = cursorMouseUp;
            base.OnMouseUp (e);
        }

        protected override List<Point> TrimShapePath(List<Point> tracePoints)
        {
            List<Point> array = new List<Point>();

            if (tracePoints.Count > 0)
            {
                array.Add(tracePoints[0]);

                if (tracePoints.Count > 1)
                {
                    array.Add(tracePoints[tracePoints.Count - 1]);
                }
            }

            return array;
        }

        protected override List<PointF> CreateShape(List<Point> tracePoints)
        {
            Point a = tracePoints[0];
            Point b = tracePoints[tracePoints.Count - 1];
            Point dir = new Point(b.X - a.X, b.Y - a.Y);
            float len = (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);

            RectangleF rectF;

            if ((ModifierKeys & Keys.Shift) != 0)
            {
                PointF center = new PointF((float)(a.X + b.X) / 2.0f, (float)(a.Y + b.Y) / 2.0f);
                float radius = len / 2;
                rectF = Rectangle.Truncate(Utility.RectangleFromCenter(center, radius));
            }
            else
            {
                rectF = Utility.PointsToRectangle(a, b);
            }

            Rectangle rect = Utility.RoundRectangle(rectF);
            PdnGraphicsPath path = new PdnGraphicsPath();
            path.AddEllipse(rect);

            // Avoid asymmetrical circles where the left or right side of the ellipse has a pixel jutting out
            using (Matrix m = new Matrix())
            {
                m.Reset();
                m.Translate(-0.5f, -0.5f, MatrixOrder.Append);
                path.Transform(m);
            }

            path.Flatten(Utility.IdentityMatrix, 0.1f);

            PointF[] pointsF = path.PathPoints;
            path.Dispose();

            return new List<PointF>(pointsF);
        }

        protected override void OnActivate()
        {
            this.cursorMouseUp = new Cursor(PdnResources.GetResourceStream("Cursors.EllipseSelectToolCursor.cur"));
            this.cursorMouseDown = new Cursor(PdnResources.GetResourceStream("Cursors.EllipseSelectToolCursorMouseDown.cur"));
            this.Cursor = cursorMouseUp;
            base.OnActivate();
        }

        protected override void OnDeactivate()
        {
            if (cursorMouseUp != null)
            {
                cursorMouseUp.Dispose();
                cursorMouseUp = null;
            }

            if (cursorMouseDown != null)
            {
                cursorMouseDown.Dispose();
                cursorMouseDown = null;
            }
            
            base.OnDeactivate();
        }

        public EllipseSelectTool(DocumentWorkspace documentWorkspace)
            : base(documentWorkspace,
                   ImageResource.Get("Icons.EllipseSelectToolIcon.png"),
                   PdnResources.GetString("EllipseSelectTool.Name"),
                   PdnResources.GetString("EllipseSelectTool.HelpText"),
                   's',
                   ToolBarConfigItems.None)
        {
        }
    }
}
