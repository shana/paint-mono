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
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PaintDotNet.SystemLayer
{
    public sealed class SingleInstanceManager
        : IDisposable
    {
  
        public bool IsFirstInstance
        {
            get
            {
                return true;
            }
        }

        public bool AreMessagesPending
        {
            get
            {
					return false;
            }
        }

        public void SetWindow(Form newWindow)
        {
		Console.WriteLine ("SingleInstanceManager.SetWindow: Not implemented");
	}
			

        public string[] GetPendingInstanceMessages()
        {
            string[] messages = new string [0];
				
			Console.WriteLine ("SingleInstanceManager.GetPendingInstances: Not implemented");

            return messages;
        }

        public event EventHandler InstanceMessageReceived;

        public void SendInstanceMessage(string text)
        {
            SendInstanceMessage(text, 1);
        }

        public void SendInstanceMessage(string text, int timeoutSeconds)
        {
		Console.WriteLine ("SingleInstanceManager.SendInstanceMessage: Not implemented");
        }
	    
	public void FocusFirstInstance()
        {
		Console.WriteLine ("SingleInstanceManager.FocusFirstInstance: Not implemented");
        }

	static bool fm_warn_show;
        public void FilterMessage(ref Message m)
        {
		if (!fm_warn_show){
			Console.WriteLine ("SingleInstanceManager.FilterMessage: Not implemented");
			fm_warn_show = true;
		}
        }

        public SingleInstanceManager(string moniker)
        {
            Console.WriteLine ("SingleInstanceManager.SingleInstaceManager: Not implemented");
        }

        ~SingleInstanceManager()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
#if false
            if (disposing)
            {
                UnregisterWindow();
            }

            if (this.hFileMapping != IntPtr.Zero)
            {
                SafeNativeMethods.CloseHandle(this.hFileMapping);
                this.hFileMapping = IntPtr.Zero;
            }
#endif
        }
    }        
}
