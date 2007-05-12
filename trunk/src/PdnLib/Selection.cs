/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;

namespace PaintDotNet
{
    /// <summary>
    /// Manages a selection for Paint.NET.
    /// There are five major components of a selection:
    /// * Base path
    /// * Continuation path
    /// * Continuation combination mode
    /// * Cumulative transform
    /// * Interim transform
    /// The selection itself, as returned by e.g. CreatePath(), is (base COMBINE continuation) x interimTransform.
    /// Whenever the interim transform is set, the continuation path is first committed, and vice versa.
    /// Because of this, you may think of the selection as operating in two editing 'modes': editing
    /// the path, and editing the transformation.
    /// When the continuation path is committed, it is appended to the base path using the given combination mode. 
    /// The continuation is then reset to empty.
    /// When the interim transform is committed, both the base path and the cumulative transform
    /// are multiplied by it. The interim transform is then reset to the identity matrix.
    /// If the selection is empty, then its "clip region" is the entire canvas as set by the ClipRectangle
    /// property.
    /// </summary>
    public sealed class Selection
    {
        private object syncRoot = new object();

        public object SyncRoot
        {
            get
            {
                return this.syncRoot;
            }
        }

        private class Data
            : ICloneable,
              IDisposable
        {
            public PdnGraphicsPath basePath;
            public PdnGraphicsPath continuation;
            public CombineMode continuationCombineMode;
            public Matrix cumulativeTransform; // resets whenever SetContinuation is called
            public Matrix interimTransform;

            public Data()
            {
                this.basePath = new PdnGraphicsPath();
                this.continuation = new PdnGraphicsPath();
                this.continuationCombineMode = CombineMode.Xor;
                this.cumulativeTransform = new Matrix();
                this.cumulativeTransform.Reset();
                this.interimTransform = new Matrix();
                this.interimTransform.Reset();            
            }

            ~Data()
            {
                Dispose(false);
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            public void Dispose(bool disposing)
            {
                if (disposing)
                {
                    if (this.basePath != null)
                    {
                        this.basePath.Dispose();
                        this.basePath = null;
                    }

                    if (this.continuation != null)
                    {
                        this.continuation.Dispose();
                        this.continuation = null;
                    }

                    if (this.cumulativeTransform != null)
                    {
                        this.cumulativeTransform.Dispose();
                        this.cumulativeTransform = null;
                    }

                    if (this.interimTransform != null)
                    {
                        this.interimTransform.Dispose();
                        this.interimTransform = null;
                    }
                }
            }

            public Data Clone()
            {
                Data newData = new Data();
                newData.basePath = this.basePath.Clone();
                newData.continuation = this.continuation.Clone();
                newData.continuationCombineMode = this.continuationCombineMode;
                newData.cumulativeTransform = this.cumulativeTransform.Clone();
                newData.interimTransform = this.interimTransform.Clone();
                return newData;
            }

            object ICloneable.Clone()
            {
                return (object)Clone();
            }
        }

        private Data data;
        private int alreadyChanging; // we don't want to nest Changing events -- consolidate them with this
        private Rectangle clipRectangle;

        public Selection()
        {
            this.data = new Data();
            this.alreadyChanging = 0;
            this.clipRectangle = new Rectangle(0, 0, 65535, 65535);
        }

        public Rectangle ClipRectangle
        {
            get
            {
                return this.clipRectangle;
            }

            set
            {
                this.clipRectangle = value;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return this.data.basePath.IsEmpty && this.data.continuation.IsEmpty;
            }
        }

        public bool IsVisible(Point pt)
        {
            using (PdnGraphicsPath path = CreatePath())
            {
                return path.IsVisible(pt);
            }
        }

        public object Save()
        {
            lock (this.syncRoot)
            {
                return this.data.Clone();
            }
        }

        public void Restore(object state)
        {
            lock (this.syncRoot)
            {
                OnChanging();
                this.data.Dispose();
                this.data = ((Data)state).Clone();
                OnChanged();
            }
        }

        public PdnGraphicsPath CreatePixelatedPath()
        {
            using (PdnGraphicsPath path = CreatePath())
            {
                using (PdnRegion region = new PdnRegion(path))
                {
                    PdnGraphicsPath pixellatedPath = PdnGraphicsPath.FromRegion(region);
                    return pixellatedPath;
                }
            }
        }

        public PdnGraphicsPath CreatePath()
        {
            return CreatePath(true);
        }

        public PdnGraphicsPath CreatePath(bool applyInterimTransform)
        {
            lock (this.syncRoot)
            {
                PdnGraphicsPath returnPath;

                switch (this.data.continuationCombineMode)
                {
                    case CombineMode.Complement:
                    case CombineMode.Intersect:
                        throw new NotSupportedException("Complement and Intersect are not supported");

                    case CombineMode.Replace:
                        returnPath = this.data.continuation.Clone();
                        break;

                    case CombineMode.Xor:
                        returnPath = this.data.basePath.Clone();
                        returnPath.CloseAllFigures();
                        returnPath.AddPath(this.data.continuation, false);
                        break;

                    case CombineMode.Union:
                        if (this.data.basePath.IsEmpty)
                        {
                            goto case CombineMode.Replace;
                        }
                        else
                        {
                            using (PdnRegion baseRegion = new PdnRegion(this.data.basePath))
                            {
                                using (PdnRegion continuationRegion = new PdnRegion(this.data.continuation))
                                {
                                    returnPath = PdnGraphicsPath.FromRegions(baseRegion, CombineMode.Union, continuationRegion);
                                }
                            }
                        }

                        break;

                    case CombineMode.Exclude:
                        if (this.data.basePath.IsEmpty)
                        {
                            returnPath = new PdnGraphicsPath();
                        }
                        else
                        {
                            using (PdnRegion baseRegion = new PdnRegion(this.data.basePath))
                            {
                                using (PdnRegion continuationRegion = new PdnRegion(this.data.continuation))
                                {
                                    returnPath = PdnGraphicsPath.FromRegions(baseRegion, CombineMode.Exclude, continuationRegion);
                                }
                            }
                        }

                        break;

                    default:
                        throw new InvalidEnumArgumentException();
                }

                if (applyInterimTransform)
                {
                    returnPath.Transform(this.data.interimTransform);
                }

                return returnPath;
            }
        }

        public void Reset()
        {
            lock (this.syncRoot)
            {
                OnChanging();
                this.data.basePath.Dispose();
                this.data.basePath = new PdnGraphicsPath();
                this.data.continuation.Dispose();
                this.data.continuation = new PdnGraphicsPath();
                this.data.cumulativeTransform.Reset();
                this.data.interimTransform.Reset();
                OnChanged();
            }
        }

        public void ResetContinuation()
        {
            lock (this.syncRoot)
            {
                OnChanging();
                CommitInterimTransform();
                ResetCumulativeTransform();
                this.data.continuation.Reset();
                OnChanged();
            }
        }

        public Rectangle GetBounds()
        {
            return GetBounds(true);
        }

        public Rectangle GetBounds(bool applyInterimTransformation)
        {
            return Utility.RoundRectangle(GetBoundsF(applyInterimTransformation));
        }

        public RectangleF GetBoundsF()
        {
            return GetBoundsF(true);
        }

        public RectangleF GetBoundsF(bool applyInterimTransformation)
        {
            using (PdnGraphicsPath path = this.CreatePath(applyInterimTransformation))
            {
                RectangleF bounds2 = path.GetBounds2();
                return bounds2;
            }
        }

        public void SetContinuation(Rectangle rect, CombineMode combineMode)
        {
            lock (this.syncRoot)
            {
                OnChanging();
                CommitInterimTransform();
                ResetCumulativeTransform();
                this.data.continuationCombineMode = combineMode;
                this.data.continuation.Reset();
                this.data.continuation.AddRectangle(rect);
                OnChanged();
            }
        }

        public void SetContinuation(Point[] linePoints, CombineMode combineMode)
        {
            lock (this.syncRoot)
            {
                OnChanging();
                CommitInterimTransform();
                ResetCumulativeTransform();
                this.data.continuationCombineMode = combineMode;
                this.data.continuation.Reset();
                this.data.continuation.AddLines(linePoints);
                OnChanged();
            }
        }

        public void SetContinuation(PointF[] linePointsF, CombineMode combineMode)
        {
            lock (this.syncRoot)
            {
                OnChanging();
                CommitInterimTransform();
                ResetCumulativeTransform();
                this.data.continuationCombineMode = combineMode;
                this.data.continuation.Reset();
                this.data.continuation.AddLines(linePointsF);
                OnChanged();
            }
        }

        public void SetContinuation(PointF[][] polygonSet, CombineMode combineMode)
        {
            lock (this.syncRoot)
            {
                OnChanging();
                CommitInterimTransform();
                ResetCumulativeTransform();
                this.data.continuationCombineMode = combineMode;
                this.data.continuation.Reset();
                this.data.continuation.AddPolygons(polygonSet);
                OnChanged();
            }
        }

        public void SetContinuation(Point[][] polygonSet, CombineMode combineMode)
        {
            lock (this.syncRoot)
            {
                OnChanging();
                CommitInterimTransform();
                ResetCumulativeTransform();
                this.data.continuationCombineMode = combineMode;
                this.data.continuation.Reset();
                this.data.continuation.AddPolygons(polygonSet);
                OnChanged();
            }
        }

        // only works if base is empty 
        public void SetContinuation(PdnGraphicsPath path, CombineMode combineMode, bool takeOwnership)
        {
            lock (this.syncRoot)
            {
                if (!this.data.basePath.IsEmpty)
                {
                    throw new InvalidOperationException("base path must be empty to use this overload of SetContinuation");
                }

                OnChanging();

                CommitInterimTransform();
                ResetCumulativeTransform();

                this.data.continuationCombineMode = combineMode;

                if (takeOwnership)
                {
                    this.data.continuation.Dispose();
                    this.data.continuation = path;
                }
                else
                {
                    this.data.continuation.Reset();
                    this.data.continuation.AddPath(path, false);
                }

                OnChanged();
            }
        }

        public void CommitContinuation()
        {
            lock (this.syncRoot)
            {
                OnChanging();
                this.data.continuation.CloseAllFigures();
                PdnGraphicsPath newBasePath = CreatePath();
                this.data.basePath.Dispose();
                this.data.basePath = newBasePath;
                this.data.continuation.Reset();
                this.data.continuationCombineMode = CombineMode.Xor;
                OnChanged();
            }
        }

        public Matrix GetCumulativeTransformCopy()
        {
            lock (this.syncRoot)
            {
                if (this.data.cumulativeTransform == null)
                {
                    Matrix m = new Matrix();
                    m.Reset();
                    return m;
                }
                else
                {
                    return this.data.cumulativeTransform.Clone();
                }
            }
        }

        public Matrix GetCumulativeTransformReadOnly()
        {
            lock (this.syncRoot)
            {
                return this.data.cumulativeTransform;
            }
        }

        private void ResetCumulativeTransform()
        {
            lock (this.syncRoot)
            {
                if (this.data.cumulativeTransform == null)
                {
                    this.data.cumulativeTransform = new Matrix();
                }

                this.data.cumulativeTransform.Reset();
            }
        }

        public Matrix GetInterimTransformCopy()
        {
            lock (this.syncRoot)
            {
                if (this.data.interimTransform == null)
                {
                    Matrix m = new Matrix();
                    m.Reset();
                    return m;
                }
                else
                {
                    return this.data.interimTransform.Clone();
                }
            }
        }

        public Matrix GetInterimTransformReadOnly()
        {
            lock (this.syncRoot)
            {
                return this.data.interimTransform;
            }
        }

        public void SetInterimTransform(Matrix m)
        {
            lock (this.syncRoot)
            {
                OnChanging();
                this.data.interimTransform.Dispose();
                this.data.interimTransform = m.Clone();
                OnChanged();
            }
        }

        public void CommitInterimTransform()
        {
            lock (this.syncRoot)
            {
                if (!this.data.interimTransform.IsIdentity)
                {
                    OnChanging();
                    this.data.basePath.Transform(this.data.interimTransform);
                    this.data.continuation.Transform(this.data.interimTransform);
                    this.data.cumulativeTransform.Multiply(this.data.interimTransform, MatrixOrder.Append);
                    this.data.interimTransform.Reset();
                    OnChanged();
                }
            }
        }

        public void ResetInterimTransform()
        {
            lock (this.syncRoot)
            {
                OnChanging();
                this.data.interimTransform.Reset();
                OnChanged();
            }
        }

        public PdnRegion CreateRegionRaw()
        {
            using (PdnGraphicsPath path = CreatePath())
            {
                return new PdnRegion(path);
            }
        }

        public PdnRegion CreateRegion()
        {
            lock (this.syncRoot)
            {
                if (IsEmpty)
                {
                    return new PdnRegion(this.clipRectangle);
                }
                else
                {
                    PdnRegion region = CreateRegionRaw();
                    region.Intersect(this.clipRectangle);
                    return region;
                }
            }
        }

        public event EventHandler Changing;
        private void OnChanging()
        {
            if (this.alreadyChanging == 0)
            {
                if (Changing != null)
                {
                    Changing(this, EventArgs.Empty);
                }
            }

            ++this.alreadyChanging;
        }

        public void PerformChanging()
        {
            OnChanging();
        }

        public event EventHandler Changed;
        private void OnChanged()
        {
            if (this.alreadyChanging <= 0)
            {
                throw new InvalidOperationException("Changed event was raised without corresponding Changing event beforehand");
            }

            --this.alreadyChanging;

            if (this.alreadyChanging == 0)
            {
                if (Changed != null)
                {
                    Changed(this, EventArgs.Empty);
                }
            }
        }

        public void PerformChanged()
        {
            OnChanged();
        }
    }
}
