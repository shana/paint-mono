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
using System;
using System.Drawing.Drawing2D;

namespace PaintDotNet.HistoryFunctions
{
    public sealed class InvertSelectionFunction
        : HistoryFunction
    {
        public static string StaticName
        {
            get
            {
                return PdnResources.GetString("InvertSelectionAction.Name");
            }
        }

        public static ImageResource StaticImage
        {
            get
            {
                return ImageResource.Get("Icons.MenuEditInvertSelectionIcon.png");
            }
        }

        public override HistoryMemento OnExecute(IHistoryWorkspace historyWorkspace)
        {
            if (historyWorkspace.Selection.IsEmpty)
            {
                return null;
            }
            else
            {
                SelectionHistoryMemento sha = new SelectionHistoryMemento(
                    StaticName,
                    StaticImage,
                    historyWorkspace);

                PdnRegion selectedRegion = historyWorkspace.Selection.CreateRegion();
                selectedRegion.Xor(historyWorkspace.Document.Bounds);

                PdnGraphicsPath invertedSelection = PdnGraphicsPath.FromRegion(selectedRegion);
                selectedRegion.Dispose();

                EnterCriticalRegion();
                historyWorkspace.Selection.PerformChanging();
                historyWorkspace.Selection.Reset();
                historyWorkspace.Selection.SetContinuation(invertedSelection, CombineMode.Xor, true);
                historyWorkspace.Selection.CommitContinuation();
                historyWorkspace.Selection.PerformChanged();

                return sha;
            }
        }
 
        public InvertSelectionFunction()
            : base(ActionFlags.None)
        {
        }
    }
}
