/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet.HistoryMementos;
using PaintDotNet.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PaintDotNet.Actions
{
    public sealed class PasteAction
    {
        private DocumentWorkspace documentWorkspace;

        /// <summary>
        /// Pastes from the clipboard into the document.
        /// </summary>
        /// <returns>true if the paste operation completed, false if there was an error or if it was cancelled for some reason</returns>
        public bool PerformAction()
        {
            SurfaceForClipboard surfaceForClipboard = null;
            IDataObject clipData = null;

            try
            {
                Utility.GCFullCollect();
                clipData = Clipboard.GetDataObject();
            }

            catch (ExternalException)
            {
                Utility.ErrorBox(this.documentWorkspace, PdnResources.GetString("PasteAction.Error.TransferFromClipboard"));
                return false;
            }

            catch (OutOfMemoryException)
            {
                Utility.ErrorBox(this.documentWorkspace, PdnResources.GetString("PasteAction.Error.OutOfMemory"));
                return false;
            }

            // First "ask" the current tool if it wants to handle it
            bool handledByTool = false;
            if (this.documentWorkspace.Tool != null)
            {
                this.documentWorkspace.Tool.PerformPaste(clipData, out handledByTool);
            }

            if (handledByTool)
            {
                return true;
            }

            if (clipData.GetDataPresent(typeof(SurfaceForClipboard)))
            {
                try
                {
                    Utility.GCFullCollect();
                    surfaceForClipboard = clipData.GetData(typeof(SurfaceForClipboard)) as SurfaceForClipboard;
                }

                catch (OutOfMemoryException)
                {
                    Utility.ErrorBox(this.documentWorkspace, PdnResources.GetString("PasteAction.Error.OutOfMemory"));
                    return false;
                }
            }

            if (surfaceForClipboard != null && surfaceForClipboard.MaskedSurface.IsDisposed)
            {
                // Have been getting crash reports where sfc contains a disposed MaskedSurface ...
                surfaceForClipboard = null;
            }

            if (surfaceForClipboard == null && 
                clipData.GetDataPresent(DataFormats.Bitmap))
            {
                Image image;

                try
                {
                    Utility.GCFullCollect();
                    image = (Image)clipData.GetData(DataFormats.Bitmap);
                }

                catch (OutOfMemoryException)
                {
                    Utility.ErrorBox(this.documentWorkspace, PdnResources.GetString("PasteAction.Error.OutOfMemory"));
                    return false;
                }

                // Sometimes we get weird errors if we're in, say, 16-bit mode but the image was copied
                // to the clipboard in 32-bit mode
                if (image == null)
                {
                    Utility.ErrorBox(this.documentWorkspace, PdnResources.GetString("PasteAction.Error.NotRecognized"));
                    return false;
                }

                MaskedSurface maskedSurface = null;

                try
                {
                    Utility.GCFullCollect();
                    Bitmap bitmap;
                    Surface surface = null;

                    if (image is Bitmap)
                    {
                        bitmap = (Bitmap)image;
                        image = null;
                    }
                    else
                    {
                        bitmap = new Bitmap(image);
                        image.Dispose();
                    }

                    surface = Surface.CopyFromBitmap(bitmap);
                    bitmap.Dispose();
                    bitmap = null;

                    maskedSurface = new MaskedSurface(surface, new PdnRegion(surface.Bounds));

                    surface.Dispose();
                    surface = null;
                }

                catch (Exception)
                {
                    Utility.ErrorBox(this.documentWorkspace, PdnResources.GetString("PasteAction.Error.OutOfMemory"));
                    return false;
                }

                surfaceForClipboard = new SurfaceForClipboard(maskedSurface);
            }

            if (surfaceForClipboard == null || surfaceForClipboard.MaskedSurface == null)
            {
                // silently fail: like what if a program overwrote the clipboard in between the time
                // we enabled the "Paste" menu item and the user actually clicked paste?
                // it could happen!
                Utility.ErrorBox(this.documentWorkspace, PdnResources.GetString("PasteAction.Error.NoImage"));
                return false;
            }

            // If the image is larger than the document, ask them if they'd like to make the image larger first
            Rectangle bounds = surfaceForClipboard.Bounds;

            if (bounds.Width > this.documentWorkspace.Document.Width ||
                bounds.Height > this.documentWorkspace.Document.Height)
            {
                DialogResult dr = MessageBox.Show(
                    this.documentWorkspace, 
                    PdnResources.GetString("PasteAction.Question.ExpandCanvas"),
                    PdnInfo.GetAppName(), 
                    MessageBoxButtons.YesNoCancel, 
                    MessageBoxIcon.Question);

                int layerIndex = this.documentWorkspace.ActiveLayerIndex;

                switch (dr)
                {
                    case DialogResult.Yes:
                        Size newSize = new Size(Math.Max(bounds.Width, this.documentWorkspace.Document.Width),
                                                Math.Max(bounds.Height, this.documentWorkspace.Document.Height));

                        Document newDoc = CanvasSizeAction.ResizeDocument(
                            this.documentWorkspace.Document, 
                            newSize,
                            AnchorEdge.TopLeft,
                            this.documentWorkspace.AppWorkspace.AppEnvironment.SecondaryColor);

                        if (newDoc == null)
                        {
                            return false; // user clicked cancel!
                        }
                        else
                        {
                            HistoryMemento rdha = new ReplaceDocumentHistoryMemento(
                                CanvasSizeAction.StaticName,
                                CanvasSizeAction.StaticImage,
                                this.documentWorkspace);

                            this.documentWorkspace.Document = newDoc;
                            this.documentWorkspace.History.PushNewMemento(rdha);
                            this.documentWorkspace.ActiveLayer = (Layer)this.documentWorkspace.Document.Layers[layerIndex];
                        }

                        break;

                    case DialogResult.No:
                        break;

                    case DialogResult.Cancel:
                        return false;

                    default:
                        throw new InvalidEnumArgumentException("Internal error: DialogResult was neither Yes, No, nor Cancel");
                }
            }

            // Decide where to paste to: If the paste is within bounds of the document, do as normal
            // Otherwise, center it.
            Rectangle docBounds = this.documentWorkspace.Document.Bounds;
            Rectangle intersect1 = Rectangle.Intersect(docBounds, bounds);
            bool doMove = intersect1 != bounds; //intersect1.IsEmpty;

            Point pasteOffset;

            if (doMove)
            {
                pasteOffset = new Point(-bounds.X + (docBounds.Width / 2) - (bounds.Width / 2),
                                        -bounds.Y + (docBounds.Height / 2) - (bounds.Height / 2));
            }
            else
            {
                pasteOffset = new Point(0, 0);
            }

            // Paste to the place it was originally copied from (for PDN-to-PDN transfers)
            // and then if its not pasted within the viewable rectangle we pan to that location
            RectangleF visibleDocRectF = this.documentWorkspace.VisibleDocumentRectangleF;
            Rectangle visibleDocRect = Utility.RoundRectangle(visibleDocRectF);
            Rectangle bounds2 = new Rectangle(new Point(bounds.X + pasteOffset.X, bounds.Y + pasteOffset.Y), bounds.Size);
            Rectangle intersect2 = Rectangle.Intersect(bounds2, visibleDocRect);
            bool doPan = intersect2.IsEmpty;

            this.documentWorkspace.SetTool(null);
            this.documentWorkspace.SetToolFromType(typeof(MoveTool));

            ((MoveTool)this.documentWorkspace.Tool).PasteMouseDown(surfaceForClipboard, pasteOffset);

            if (doPan)
            {
                Point centerPtView = new Point(visibleDocRect.Left + (visibleDocRect.Width / 2),
                                               visibleDocRect.Top + (visibleDocRect.Height / 2));

                Point centerPtPasted = new Point(bounds2.Left + (bounds2.Width / 2),
                                                 bounds2.Top + (bounds2.Height / 2));

                Size delta = new Size(centerPtPasted.X - centerPtView.X,
                                      centerPtPasted.Y - centerPtView.Y);

                PointF docScrollPos = this.documentWorkspace.DocumentScrollPositionF;

                PointF newDocScrollPos = new PointF(docScrollPos.X + delta.Width,
                                                    docScrollPos.Y + delta.Height);

                this.documentWorkspace.DocumentScrollPositionF = newDocScrollPos;
            }

            return true;
        }

        public PasteAction(DocumentWorkspace documentWorkspace)
        {
            this.documentWorkspace = documentWorkspace;
        }
    }
}
