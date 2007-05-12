/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet.HistoryMementos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace PaintDotNet.HistoryFunctions
{
    public sealed class RotateDocumentFunction
        : HistoryFunction
    {
        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("RotateAction.Name");
            }
        }

        private RotateType rotation;
        
        public override HistoryMemento OnExecute(IHistoryWorkspace historyWorkspace)
        {
            int newWidth;
            int newHeight;

            // Get new width and Height
            switch (rotation)
            {
                case RotateType.Clockwise90:
                case RotateType.Clockwise270:
                case RotateType.CounterClockwise90:
                case RotateType.CounterClockwise270:
                    newWidth = historyWorkspace.Document.Height;
                    newHeight = historyWorkspace.Document.Width;
                    break;

                case RotateType.Clockwise180:
                case RotateType.CounterClockwise180:
                    newWidth = historyWorkspace.Document.Width;
                    newHeight = historyWorkspace.Document.Height;
                    break;

                default:
                    throw new InvalidEnumArgumentException("invalid RotateType");
            }

            // Figure out which icon and text to use
            string iconResName;
            string suffix;

            switch (rotation)
            {
                case RotateType.Clockwise180:
                    iconResName = "Icons.MenuImageRotate180CWIcon.png";
                    suffix = PdnResources.GetString("RotateAction.180CW");
                    break;

                case RotateType.Clockwise270:
                    iconResName = "Icons.MenuImageRotate270CWIcon.png";
                    suffix = PdnResources.GetString("RotateAction.270CW");
                    break;

                case RotateType.Clockwise90:
                    iconResName = "Icons.MenuImageRotate90CWIcon.png";
                    suffix = PdnResources.GetString("RotateAction.90CW");
                    break;

                case RotateType.CounterClockwise180:
                    iconResName = "Icons.MenuImageRotate180CCWIcon.png";
                    suffix = PdnResources.GetString("RotateAction.180CCW");
                    break;

                case RotateType.CounterClockwise270:
                    iconResName = "Icons.MenuImageRotate270CCWIcon.png";
                    suffix = PdnResources.GetString("RotateAction.270CCW");
                    break;

                case RotateType.CounterClockwise90:
                    iconResName = "Icons.MenuImageRotate90CCWIcon.png";
                    suffix = PdnResources.GetString("RotateAction.90CCW");
                    break;

                default:
                    throw new InvalidEnumArgumentException("invalid RotateType");
            }

            // Initialize the new Doc
            string haNameFormat = PdnResources.GetString("RotateAction.HistoryMementoName.Format");
            string haName = string.Format(haNameFormat, StaticName, suffix);
            ImageResource haImage = ImageResource.Get(iconResName);

            List<HistoryMemento> actions = new List<HistoryMemento>();

            // do the memory allocation now: if this fails, we can still bail out cleanly
            Document newDoc = new Document(newWidth, newHeight);

            if (!historyWorkspace.Selection.IsEmpty)
            {
                DeselectFunction da = new DeselectFunction();
                EnterCriticalRegion();
                HistoryMemento hm = da.Execute(historyWorkspace);
                actions.Add(hm);
            }

            ReplaceDocumentHistoryMemento rdha = new ReplaceDocumentHistoryMemento(null, null, historyWorkspace);
            actions.Add(rdha);

            newDoc.ReplaceMetaDataFrom(historyWorkspace.Document);

            // TODO: serialize oldDoc to disk, and let the GC purge it if needed
            OnProgress(0.0);

            for (int i = 0; i < historyWorkspace.Document.Layers.Count; ++i)
            {
                Layer layer = historyWorkspace.Document.Layers.GetAt(i);

                double progressStart = 100.0 * ((double)i / (double)historyWorkspace.Document.Layers.Count);
                double progressEnd = 100.0 * ((double)(i + 1) / (double)historyWorkspace.Document.Layers.Count);

                if (layer is BitmapLayer)
                {
                    Layer nl = RotateLayer((BitmapLayer)layer, rotation, newWidth, newHeight, progressStart, progressEnd);
                    newDoc.Layers.Add(nl);
                }
                else
                {
                    throw new InvalidOperationException("Cannot Rotate non-BitmapLayers");
                }

                if (this.PleaseCancel)
                {
                    break;
                }

                OnProgress(progressEnd);
            }

            CompoundHistoryMemento chm = new CompoundHistoryMemento(
                haName,
                haImage,
                actions);

            if (this.PleaseCancel)
            {
                chm = null;
            }
            else
            {
                EnterCriticalRegion();
                historyWorkspace.Document = newDoc;
            }

            return chm;
        }

        private BitmapLayer RotateLayer(BitmapLayer layer, RotateType rotationType, int width, int height, double startProgress, double endProgress)
        {
            Surface surface = new Surface(width, height);

            if (rotationType == RotateType.Clockwise180 ||
                rotationType == RotateType.CounterClockwise180)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        surface[x, y] = layer.Surface[width - x - 1, height - y - 1];
                    }

                    OnProgress(((double)y / (double)height) * (endProgress - startProgress) + startProgress);
                }
            }
            else if (rotationType == RotateType.Clockwise270 ||
                     rotationType == RotateType.CounterClockwise90)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        surface[x, y] = layer.Surface[height - y - 1, x];
                    }

                    OnProgress(((double)y / (double)height) * (endProgress - startProgress) + startProgress);
                }
            }
            else if (rotationType == RotateType.Clockwise90 ||
                     rotationType == RotateType.CounterClockwise270)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        surface[x, y] = layer.Surface[y, width - 1 - x];
                    }

                    OnProgress(((double)y / (double)height) * (endProgress - startProgress) + startProgress);
                }
            }

            BitmapLayer returnMe = new BitmapLayer(surface, true);
            returnMe.LoadProperties(layer.SaveProperties());            
            return returnMe;
        }

        // TODO: because of ProgressDialog's incorrect code, ActionFlags.Cancellable doesn't work right
        //       fix this post-3.0. it is not a priority right now because this is the only action that
        //       uses this flag right now.
        public RotateDocumentFunction(RotateType rotation)
            : base(ActionFlags.ReportsProgress /*| ActionFlags.Cancellable*/)
        {
            this.rotation = rotation;           
        }
    }
}
