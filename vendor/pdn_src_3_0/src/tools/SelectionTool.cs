/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet.Actions;
using PaintDotNet.HistoryMementos;
using PaintDotNet.HistoryFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PaintDotNet.Tools
{
    public class SelectionTool
        : Tool
    {
        private bool tracking = false;
        private bool moveOriginMode = false;
        private Point lastXY;
        private SelectionHistoryMemento undoAction;
        private CombineMode combineMode;
        private List<Point> tracePoints = null;
        private DateTime startTime;
        private bool hasMoved = false;
        private bool append = false;
        private bool wasNotEmpty = false;

        protected override void OnActivate()
        {
            DocumentWorkspace.EnableSelectionTinting = true;
            base.OnActivate();
        }

        protected override void OnDeactivate()
        {
            DocumentWorkspace.EnableSelectionTinting = false;

            if (this.tracking)
            {
                Done();
            }

            base.OnDeactivate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (tracking)
            {
                moveOriginMode = true;
                lastXY = new Point(e.X, e.Y);
                OnMouseMove(e);
            }
            else if ((e.Button & MouseButtons.Left) == MouseButtons.Left ||
                (e.Button & MouseButtons.Right) == MouseButtons.Right)
            {
                tracking = true;
                hasMoved = false;
                startTime = DateTime.Now;

                tracePoints = new List<Point>();
                tracePoints.Add(new Point(e.X, e.Y));

                undoAction = new SelectionHistoryMemento("sentinel", this.Image, DocumentWorkspace);

                wasNotEmpty = !Selection.IsEmpty;

                // if the user is holding down the control key then we don't want to reset the path, merely append to it
                if ((ModifierKeys & Keys.Control) == Keys.Control)
                {
                    append = true;

                    if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
                    {
                        this.combineMode = CombineMode.Xor;
                    }
                    else
                    {
                        this.combineMode = CombineMode.Union;
                    }

                    Selection.ResetContinuation();
                }
                else if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
                {
                    append = true;
                    this.combineMode = CombineMode.Exclude;
                    Selection.ResetContinuation();
                }
                else
                {
                    append = false;
                    this.combineMode = CombineMode.Replace;
                    Selection.Reset();
                }
            }
        }

        protected virtual List<Point> TrimShapePath(List<Point> trimTheseTracePoints)
        {
            return trimTheseTracePoints;
        }

        protected virtual List<PointF> CreateShape(List<Point> inputTracePoints)
        {
            return Utility.PointListToPointFList(inputTracePoints);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (moveOriginMode)
            {
                Size delta = new Size(e.X - lastXY.X, e.Y - lastXY.Y);
                
                for (int i = 0; i < tracePoints.Count; ++i)
                {
                    Point pt = (Point)tracePoints[i];
                    pt.X += delta.Width;
                    pt.Y += delta.Height;
                    tracePoints[i] = pt;
                }

                lastXY = new Point(e.X, e.Y);
                Render();
            }
            else if (tracking)
            {
                Point mouseXY = new Point(e.X, e.Y);

                if (mouseXY != (Point)tracePoints[tracePoints.Count - 1])
                {
                    tracePoints.Add(mouseXY);
                }
                
                hasMoved = true;
                Render();
            }
        }

        private PointF[] CreateSelectionPolygon()
        {
            List<Point> trimmedTrace = this.TrimShapePath(tracePoints);
            List<PointF> shapePoints = CreateShape(trimmedTrace);
            List<PointF> polygon;

            switch (this.combineMode)
            {
                case CombineMode.Xor:
                case CombineMode.Exclude:
                    polygon = shapePoints;
                    break;

                default:
                case CombineMode.Complement:
                case CombineMode.Intersect:
                case CombineMode.Replace:
                case CombineMode.Union:
                    polygon = Utility.SutherlandHodgman(DocumentWorkspace.Document.Bounds, shapePoints);
                    break;
            }

            return polygon.ToArray();
        }

        private void Render()
        {
            if (tracePoints != null && tracePoints.Count > 2)
            {
                PointF[] polygon = CreateSelectionPolygon();

                if (polygon.Length > 2)
                {
                    DocumentWorkspace.ResetOutlineWhiteOpacity();
                    Selection.SetContinuation(polygon, this.combineMode);
                    Update();
                }
            }
        }

        protected override void OnPulse()
        {
            if (this.tracking)
            {
                DocumentWorkspace.ResetOutlineWhiteOpacity();
            }

            base.OnPulse();
        }

        private enum WhatToDo
        {
            Clear,
            Emit,
            Reset,
        }

        private void Done()
        {
            if (tracking)
            {
                // Truth table for what we should do based on three flags:
                //  append  | moved | tooQuick | result                             | optimized expression to yield true
                // ---------+-------+----------+-----------------------------------------------------------------------
                //     F    |   T   |    T     | clear selection                    | !append && (!moved || tooQuick)
                //     F    |   T   |    F     | emit new selected area             | !append && moved && !tooQuick
                //     F    |   F   |    T     | clear selection                    | !append && (!moved || tooQuick)
                //     F    |   F   |    F     | clear selection                    | !append && (!moved || tooQuick)
                //     T    |   T   |    T     | append to selection                | append && moved
                //     T    |   T   |    F     | append to selection                | append && moved
                //     T    |   F   |    T     | reset selection                    | append && !moved
                //     T    |   F   |    F     | reset selection                    | append && !moved
                //
                // append   --> If the user was holding control, then true. Else false.
                // moved    --> If they never moved the mouse, false. Else true.
                // tooQuick --> If they held the mouse button down for more than 50ms, false. Else true.
                //
                // "Clear selection" means to result in no selected area. If the selection area was previously empty,
                //    then no HistoryMemento is emitted. Otherwise a Deselect HistoryMemento is emitted.
                //
                // "Reset selection" means to reset the selected area to how it was before interaction with the tool,
                //    without a HistoryMemento.

                PointF[] polygon = CreateSelectionPolygon();
                this.hasMoved &= (polygon.Length > 1);

                // They were "too quick" if they weren't doing a selection for more than 50ms
                // This takes care of the case where someone wants to click to deselect, but accidentally moves
                // the mouse. This happens VERY frequently.
                bool tooQuick = Utility.TicksToMs((DateTime.Now - startTime).Ticks) <= 50;

                // If their selection was completedly out of bounds, it will be clipped
                bool clipped = (polygon.Length == 0);

                // What the user drew had no effect on the slection, e.g. subtraction where there was nothing in the first place
                bool noEffect = false;

                WhatToDo whatToDo;

                // If their selection gets completely clipped (i.e. outside the image canvas),
                // then result in a no-op
                if (append)
                {
                    if (!hasMoved || clipped || noEffect)
                    {   
                        whatToDo = WhatToDo.Reset;
                    }
                    else
                    {   
                        whatToDo = WhatToDo.Emit;
                    }
                }
                else
                {
                    if (hasMoved && !tooQuick && !clipped && !noEffect)
                    {   
                        whatToDo = WhatToDo.Emit;
                    }
                    else
                    {   
                        whatToDo = WhatToDo.Clear;
                    }
                }

                switch (whatToDo)
                {
                    case WhatToDo.Clear:
                        if (wasNotEmpty)
                        {
                            // emit a deselect history action
                            undoAction.Name = DeselectFunction.StaticName;
                            undoAction.Image = DeselectFunction.StaticImage;
                            HistoryStack.PushNewMemento(undoAction);
                        }

                        Selection.Reset();
                        break;

                    case WhatToDo.Emit:
                        // emit newly selected area
                        undoAction.Name = this.Name;
                        HistoryStack.PushNewMemento(undoAction);
                        Selection.CommitContinuation();
                        break;

                    case WhatToDo.Reset:
                        // reset selection, no HistoryMemento
                        Selection.ResetContinuation();
                        break;
                }

                DocumentWorkspace.ResetOutlineWhiteOpacity();
                this.tracking = false;
                DocumentWorkspace.InvalidateSurface(Utility.RoundRectangle(DocumentWorkspace.VisibleDocumentRectangleF));
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            OnMouseMove(e);

            if (moveOriginMode)
            {
                moveOriginMode = false;
            }
            else
            {
                Done();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (tracking)
            {
                Render();
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (tracking)
            {
                Render();
            }
        }

        protected override void OnClick()
        {
            base.OnClick();
            
            if (!moveOriginMode)
            {
                Done();
            }
        }

        public SelectionTool(
            DocumentWorkspace documentWorkspace,
            ImageResource toolBarImage,
            string name,
            string helpText,
            char hotKey,
            ToolBarConfigItems toolBarConfigItems)
            : base(documentWorkspace,
                   toolBarImage,
                   name,
                   helpText,
                   hotKey,
                   false,
                   toolBarConfigItems)
        {
            this.tracking = false;
        }
    }
}
