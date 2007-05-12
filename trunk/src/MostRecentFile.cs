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

namespace PaintDotNet
{
    /// <summary>
    /// Encapsulates a filename and a thumbnail.
    /// </summary>
    public class MostRecentFile
    {
        private string fileName;
        private Image thumb;

        public string FileName
        {
            get
            {
                return fileName;
            }
        }

        public Image Thumb
        {
            get
            {
                return thumb;
            }
        }

        public MostRecentFile(string fileName, Image thumb)
        {
            this.fileName = fileName;
            this.thumb = thumb;
        }
    }
}
