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

namespace PaintDotNet.HistoryFunctions
{
    public sealed class AddNewBlankLayerFunction
        : HistoryFunction
    {
        public override HistoryMemento OnExecute(IHistoryWorkspace historyWorkspace)
        {
            BitmapLayer newLayer = null;
            newLayer = new BitmapLayer(historyWorkspace.Document.Width, historyWorkspace.Document.Height);
            string newLayerNameFormat = PdnResources.GetString("AddNewBlankLayer.LayerName.Format");
            newLayer.Name = string.Format(newLayerNameFormat, (1 + historyWorkspace.Document.Layers.Count).ToString());

            NewLayerHistoryMemento ha = new NewLayerHistoryMemento(
                PdnResources.GetString("AddNewBlankLayer.HistoryMementoName"),
                ImageResource.Get("Icons.MenuLayersAddNewLayerIcon.png"),
                historyWorkspace,
                historyWorkspace.Document.Layers.Count);

            EnterCriticalRegion();
            historyWorkspace.Document.Layers.Add(newLayer);

            return ha;
        }

        public AddNewBlankLayerFunction()
            : base(ActionFlags.None)
        {
        }
    }
}
