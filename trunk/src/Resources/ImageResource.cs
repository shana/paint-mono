/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Drawing;

namespace PaintDotNet
{
    public sealed class ImageResource
        : Resource<Image>
    {
        private static Dictionary<string, ImageResource> images;

        protected override Image Load()
        {
            return PdnResources.GetImage(this.Name);
        }

        public static ImageResource Get(string name)
        {
            ImageResource ir;

            if (!images.TryGetValue(name, out ir))
            {
                ir = new ImageResource(name);
                images.Add(name, ir);
            }

            return ir;
        }

        public static ImageResource FromImage(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            ImageResource resource = new ImageResource(image);
            return resource;
        }

        static ImageResource()
        {
            images = new Dictionary<string, ImageResource>();
        }

        private ImageResource(string name)
            : base(name)
        {
        }

        private ImageResource(Image image)
            : base(null, image)
        {
        }
    }
}
