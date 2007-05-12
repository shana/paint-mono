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
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace PaintDotNet
{
    // TODO: remove, along with strings.resx:LoadProgressDialog.Description
    [Obsolete]
    public class LoadProgressDialog
        : CallbackWithProgressDialog
    {
        private SiphonStream siphonStream;
        private FileType fileType;
        private Document document;
        private long totalBytes;

        private void LoadCallback()
        {
            try
            {
                document = fileType.Load(siphonStream);
            }

            catch
            {
                if (document != null)
                {
                    document.Dispose();
                    document = null;
                }

                throw;
            }
        }

        protected override void OnCancelClick()
        {
            SiphonStream stream = siphonStream;

            if (stream != null)
            {
                stream.Abort(new ApplicationException("Aborted"));
            }

            base.OnCancelClick();
        }

        public LoadProgressDialog(Control owner, Stream stream, FileType fileType)
            : base(owner, 
                   PdnInfo.GetBareProductName(),
                   PdnResources.GetString("LoadProgressDialog.Description"))
        {
            this.fileType = fileType;
            this.siphonStream = new SiphonStream(stream);
            this.siphonStream.IOFinished += new IOEventHandler(siphonStream_IOFinished);
            this.Icon = Utility.ImageToIcon(PdnResources.GetImage("Icons.ImageFromDiskIcon.png"), Utility.TransparentKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// A new document, or null if the user cancelled.
        /// </returns>
        public Document Load()
        {
            totalBytes = 0;
            DialogResult dr = this.ShowDialog(true, false, new ThreadStart(LoadCallback));

            if (dr == DialogResult.Cancel)
            {
                if (this.document != null)
                {
                    this.document.Dispose();
                    this.document = null;
                }

                return null;
            }

            return document;
        }

        public Document Load(Point screenPos)
        {
            this.StartPos = screenPos;
            return Load();
        }

        private void siphonStream_IOFinished(object sender, IOEventArgs e)
        {
            totalBytes += (long)e.Count;
            Progress = Math.Max(0, Math.Min(100, (int)((totalBytes * 100) / siphonStream.Length))); 
        }
    }
}
