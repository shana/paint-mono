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
    public abstract class Resource<T>
    {
        private string name;
        protected T resource;

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public T Reference
        {
            get
            {
                if (this.resource == null)
                {
                    this.resource = Load();
                }

                return this.resource;
            }
        }

        public T GetCopy()
        {
            return Load();
        }

        protected abstract T Load();

        protected Resource(string name)
        {
            this.name = name;
            this.resource = default(T);
        }

        protected Resource(string name, T resource)
        {
            this.name = name;
            this.resource = resource;
        }
    }
}
