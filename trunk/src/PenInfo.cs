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

namespace PaintDotNet
{
    /// <summary>
    /// Carries information about the subset of Pen configuration details that we support.
    /// Does not carry color information.
    /// </summary>
    [Serializable]
    public class PenInfo
        : ICloneable
    {
        private DashStyle dashStyle;
        public DashStyle DashStyle
        {
            get
            {
                return dashStyle;
            }

            set
            {
                dashStyle = value;
            }
        }

        private float width;
        public float Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public static bool operator==(PenInfo lhs, PenInfo rhs)
        {
            return (lhs.dashStyle == rhs.dashStyle && lhs.width == rhs.width);
        }

        public static bool operator!=(PenInfo lhs, PenInfo rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            PenInfo rhs = obj as PenInfo;

            if (rhs == null)
            {
                return false;
            }

            return this == rhs;
        }

        public override int GetHashCode()
        {
            return this.dashStyle.GetHashCode() ^ this.width.GetHashCode();
        }

        public Pen CreatePen(BrushInfo brushInfo, Color foreColor, Color backColor)
        {
            if (brushInfo.BrushType == BrushType.None)
            {
                return new Pen(foreColor, width);
            }
            else
            {
                return new Pen(brushInfo.CreateBrush(foreColor, backColor), width);
            }
        }

        public PenInfo(DashStyle dashStyle, float width)
        {
            this.dashStyle = dashStyle;
            this.width = width;
        }

        public PenInfo Clone()
        {
            return new PenInfo(this.dashStyle, this.width);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
