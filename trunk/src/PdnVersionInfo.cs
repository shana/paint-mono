/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;

namespace PaintDotNet
{
    /// <summary>
    /// Contains information pertaining to a release of Paint.NET
    /// </summary>
    public class PdnVersionInfo
    {
        private Version version;
        private string friendlyName;
        private Version netFxVersion;
        private string infoUrl;
        private string[] downloadUrls;
        private string[] fullDownloadUrls;
        private bool isFinal;

        public Version Version
        {
            get
            {
                return this.version;
            }
        }

        public string FriendlyName
        {
            get
            {
                return this.friendlyName;
            }
        }

        public Version NetFxVersion
        {
            get
            {
                return this.netFxVersion;
            }
        }

        public string InfoUrl
        {
            get
            {
                return this.infoUrl;
            }
        }
        
        public string[] DownloadUrls
        {
            get
            {
                return (string[])this.downloadUrls.Clone();
            }
        }

        public string[] FullDownloadUrls
        {
            get
            {
                return (string[])this.fullDownloadUrls.Clone();
            }
        }

        public bool IsFinal
        {
            get
            {
                return this.isFinal;
            }
        }

        public string ChooseDownloadUrl(bool full)
        {
            DateTime now = DateTime.Now;
            string[] urls;

            if (full)
            {
                urls = FullDownloadUrls;
            }
            else
            {
                urls = DownloadUrls;
            }

            int index = Math.Abs(now.Second % urls.Length);
            return urls[index];
        }

        public PdnVersionInfo(Version version, string friendlyName, Version netFxVersion, string infoUrl, 
            string[] downloadUrls, string[] fullDownloadUrls, bool isFinal)
        {
            this.version = version;
            this.friendlyName = friendlyName;
            this.netFxVersion = netFxVersion;
            this.infoUrl = infoUrl;
            this.downloadUrls = (string[])downloadUrls.Clone();
            this.fullDownloadUrls = (string[])fullDownloadUrls.Clone();
            this.isFinal = isFinal;
        }
    }
}
