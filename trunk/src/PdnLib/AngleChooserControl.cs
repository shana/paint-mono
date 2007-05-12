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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PaintDotNet
{
    public class AngleChooserControl 
        : UserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private bool tracking = false;
        private Point lastMouseXY;

        public event EventHandler ValueChanged;
        protected virtual void OnValueChanged()
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, EventArgs.Empty);
            }
        }

        public double angleValue;
        public int Value
        {
            get
            {
                return (int)angleValue;
            }

            set
            {
                double v = value % 360;
                if (angleValue != v)
                {
                    angleValue = v;
                    OnValueChanged();
                    Invalidate();
                }
            }
        }
        /// <summary>
        /// ValueDouble exposes the double-precision angle
        /// </summary>
        public double ValueDouble
        {
            get
            {
                return angleValue;
            }

            set
            {
                double v = Math.IEEERemainder(value, 360.0);
                if (angleValue != v)
                {
                    angleValue = v;
                    OnValueChanged();
                    Invalidate();
                }
            }
        }

        private void DrawToGraphics(Graphics g)
        {
            g.Clear(this.BackColor);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle ourRect = Rectangle.Inflate(ClientRectangle, -2, -2);
            int diameter = Math.Min(ourRect.Width, ourRect.Height);

            Point center = new Point(ourRect.X + (diameter / 2), ourRect.Y + (diameter / 2));
            double radius = ((double)diameter / 2);
            double theta = ((double)angleValue * 2 * Math.PI) / 360.0;

            Point endPoint = new Point(center.X + (int)(radius * Math.Cos(theta)),
                center.Y - (int)(radius * Math.Sin(theta)));

            g.DrawLine(SystemPens.ControlDark, center, new Point(center.X + (diameter / 2), center.Y));
            g.DrawEllipse(SystemPens.ControlDarkDark, new Rectangle(new Point(ourRect.X - 1, ourRect.Y - 1), new Size(diameter, diameter)));
            g.DrawEllipse(SystemPens.ControlLightLight, new Rectangle(ourRect.Location, new Size(diameter, diameter)));
            g.DrawLine(SystemPens.ControlText, center, endPoint);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawToGraphics(e.Graphics);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown (e);
            tracking = true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp (e);
            tracking = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove (e);

            lastMouseXY = new Point(e.X, e.Y);

            if (tracking)
            {
                Rectangle ourRect = Rectangle.Inflate(ClientRectangle, -2, -2);
                int diameter = Math.Min(ourRect.Width, ourRect.Height);
                Point center = new Point(ourRect.X + (diameter / 2), ourRect.Y + (diameter / 2));

                int dx = e.X - center.X;
                int dy = e.Y - center.Y;
                double theta = Math.Atan2(-dy, dx);
                this.ValueDouble = (theta * 360) / (2 * Math.PI);

                Update();
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick (e);
            tracking = true;
            OnMouseMove(new MouseEventArgs(MouseButtons.Left, 1, lastMouseXY.X, lastMouseXY.Y, 0));
            tracking = false;
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick (e);
            tracking = true;
            OnMouseMove(new MouseEventArgs(MouseButtons.Left, 1, lastMouseXY.X, lastMouseXY.Y, 0));
            tracking = false;
        }

        public AngleChooserControl()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                    components = null;
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // AngleChooserControl
            // 
            this.Name = "AngleChooserControl";
            this.Size = new System.Drawing.Size(168, 144);

        }
        #endregion
    }
}
