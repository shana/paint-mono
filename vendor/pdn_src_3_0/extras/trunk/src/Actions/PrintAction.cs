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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace PaintDotNet.Actions
{
    public class PrintAction
        : DocumentWorkspaceAction
    {
        public override HistoryMemento PerformAction(DocumentWorkspace documentWorkspace)
        {
            if (!ScanningAndPrinting.CanPrint)
            {
                Utility.ShowWiaError(documentWorkspace);
                return null;
            }

            using (new PushNullToolMode(documentWorkspace))
            {
                // render image to a bitmap, save it to disk
                Surface scratch = documentWorkspace.BorrowScratchSurface(this.GetType().Name + ".PerformAction()");

                try
                {
                    scratch.Clear();
                    RenderArgs ra = new RenderArgs(scratch);

                    documentWorkspace.Update();

                    using (new WaitCursorChanger(documentWorkspace))
                    {
                        ra.Surface.Clear(ColorBgra.White);
                        documentWorkspace.Document.Render(ra, false);
                    }

                    string tempName = Path.GetTempFileName() + ".bmp";
                    ra.Bitmap.Save(tempName, ImageFormat.Bmp);

                    ScanningAndPrinting.Print(documentWorkspace, tempName);

                    // Try to delete the temp file but don't worry if we can't
                    try
                    {
                        File.Delete(tempName);
                    }

                    catch
                    {
                    }
                }

                finally
                {
                    documentWorkspace.ReturnScratchSurface(scratch);
                }
            }

            return null;
        }

        public PrintAction()
            : base(ActionFlags.KeepToolActive)
        {
        }
    }
}
