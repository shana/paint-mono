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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PaintDotNet.SystemLayer
{
    /// <summary>
    /// Contains static methods related to the user interface.
    /// </summary>
    public static class UI
    {
        private static bool initScales = false;
        private static float xScale;
        private static float yScale;

	static MethodInfo object_from_handle;
	static MethodInfo get_clip_rectangles;

	static void init_types ()
	{
		Console.WriteLine ("-----HERE------");
		try {
			Type t = typeof (Control).Assembly.GetType ("System.Windows.Forms.Hwnd");
			if (t == null){
				Console.WriteLine ("PORT: Not able to get the intenral type Hwnd");
				return;
			}
			object_from_handle = t.GetMethod ("ObjectFromHandle");
			if (object_from_handle == null){
				Console.WriteLine ("PORT: No ObjectFromHandle available");
				return;
			}
			get_clip_rectangles = t.GetMethod ("get_ClipRectangles");
			if (get_clip_rectangles == null){
				Console.WriteLine ("PORT: Not able to get the intenral get_ClipRectangles");
				return;
			}
			Console.WriteLine ("Got {0} and {1}", get_clip_rectangles, object_from_handle);
		} finally {
			Console.WriteLine ("-------Finished--------");
		}
	}
	    
	static UI ()
	{
		init_types ();
	}
	    
        /// <summary>
        /// Indicates to the OS that this application is aware of high-DPI modes and handles
        /// it correctly. On some OS versions, if this function is not called then the application
        /// is virtualized to 96 DPI even if the system is running at 120 DPI. The rendering
        /// is then stretched by 125% (for 120 DPI) so that it is "physically" the correct size.
        /// </summary>
        /// <remarks>
        /// Note to implementors: This may be implemented as a no-op.
        /// </remarks>
        public static void EnableDpiAware()
        {
			// Nothing on Linux for now.
        }

        /// <summary>
        /// In some circumstances, the window manager will draw the window larger than it reports
        /// its size to be. You can use this function to retrieve the size of this extra border
        /// padding.
        /// </summary>
        /// <param name="window"></param>
        /// <returns>
        /// An integer greater than or equal to zero that describes the size of the border padding
        /// which is not reported via the window's Size or Bounds property.
        /// </returns>
        /// <remarks>
        /// Note to implementors: This method may simply return 0. It is provided for use in Windows
        /// Vista when DWM+Aero is enabled in which case sizable FloatingToolForm windows do not
        /// visibly dock to the correct locations.
        /// </remarks>
        public static int GetExtendedFrameBounds(Form window)
        {
            int returnVal;

            if (OS.IsVistaOrLater)
            {
                unsafe
                {
                    int* rcVal = stackalloc int[4];

                    int hr = SafeNativeMethods.DwmGetWindowAttribute(
                        window.Handle,
                        NativeConstants.DWMWA_EXTENDED_FRAME_BOUNDS,
                        (void*)rcVal,
                        4 * (uint)sizeof(int));

                    if (hr >= 0)
                    {
                        returnVal = -rcVal[0];
                    }
                    else
                    {
                        returnVal = 0;
                    }
                }
            }
            else
            {
                returnVal = 0;
            }

            GC.KeepAlive(window);
            return Math.Max(0, returnVal);
        }

        private static void InitScaleFactors(Control c)
        {
            if (c == null)
            {
                xScale = 1.0f;
                yScale = 1.0f;
            }
            else
            {
                using (Graphics g = c.CreateGraphics())
                {
                    xScale = g.DpiX / 96.0f;
                    yScale = g.DpiY / 96.0f;
                }
            }

            initScales = true;
        }

        public static void InitScaling(Control c)
        {
            if (!initScales)
            {
                InitScaleFactors(c);
            }
        }

        public static float ScaleWidth(float width)
        {
            return (float)Math.Round(width * GetXScaleFactor());
        }

        public static int ScaleWidth(int width)
        {
            return (int)Math.Round((float)width * GetXScaleFactor());
        }

        public static int ScaleHeight(int height)
        {
            return (int)Math.Round((float)height * GetYScaleFactor());
        }

        public static float ScaleHeight(float height)
        {
            return (float)Math.Round(height * GetYScaleFactor());
        }

        public static Size ScaleSize(Size size)
        {
            return new Size(ScaleWidth(size.Width), ScaleHeight(size.Height));
        }

        public static float GetXScaleFactor()
        {
            if (!initScales)
            {
                throw new InvalidOperationException("Must call InitScaling() first");
            }

            return xScale;
        }

        public static float GetYScaleFactor()
        {
            if (!initScales)
            {
                throw new InvalidOperationException("Must call InitScaling() first");
            }

            return yScale;
        }

        // This is a major hack to get the .NET's OFD to show with Thumbnail view by default!
        // Luckily for us this is a covert hack, and not one where we're working around a bug
        // in the framework or OS.
        // This hack works by retrieving a private property of the OFD class after it has shown
        // the dialog box.
        // Based off code found here: http://vbnet.mvps.org/index.html?code/hooks/fileopensavedlghooklvview.htm
        private static void EnableThumbnailView(FileDialog ofd)
        {
            
        }

        private delegate void EtvDelegate(FileDialog ofd);

        /// <summary>
        /// Shows a FileDialog with the initial view set to Thumbnails.
        /// </summary>
        /// <param name="ofd">The FileDialog to show.</param>
        /// <remarks>
        /// This method may or may not show the OpenFileDialog with the initial view set to Thumbnails,
        /// depending on the implementation and available feature set of the underlying operating system
        /// or shell.
        /// Note to implementors: This method may be implemented to simply call fd.ShowDialog(owner).
        /// </remarks>
        public static DialogResult ShowFileDialogWithThumbnailView(Control owner, FileDialog fd)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }

            if (fd == null)
            {
                throw new ArgumentNullException("fd");
            }

            // Bug 1495: Since we do a little 'hackery' to get Thumbnails to be the default view,
            // and since the shortcut for File->Save As is Ctrl+Shift+S, and since Explorer hides
            // the filenames if you hold down Shift when opening a folder in Thumbnail view, we
            // simply spin until the user lets go of shift!
            Cursor.Current = Cursors.WaitCursor;

            while ((Control.ModifierKeys & Keys.Shift) != 0)
            {
                System.Threading.Thread.Sleep(1);
                Application.DoEvents();
            }

            Cursor.Current = Cursors.Default;

            if (owner.IsHandleCreated)
            {
                owner.BeginInvoke(new EtvDelegate(EnableThumbnailView), new object[] { fd });
            }

            DialogResult result = fd.ShowDialog(owner);
            return result;
        }

        public enum ButtonState
        {
            Normal,
            Hot,
            Pressed,
            Disabled
        }

        // TODO: Use VisualStyles stuff in .NET 2.0 instead
        /// <summary>
        /// Draws a button in the appropriate system theme (Aero vs. Luna vs. Classic).
        /// </summary>
        /// <remarks>
        /// Note to implementors: This may be implemented as a simple thunk to ControlPaint.DrawButton().
        /// </remarks>
        public static void DrawThemedButton(
            Control hostControl, 
            Graphics g, 
            int x, 
            int y, 
            int width, 
            int height, 
            UI.ButtonState state)
        {
            
                System.Windows.Forms.ButtonState swfState;

                switch (state)
                {
                    case UI.ButtonState.Disabled:
                        swfState = System.Windows.Forms.ButtonState.Inactive;
                        break;

                    default:
                    case UI.ButtonState.Hot:
                    case UI.ButtonState.Normal:
                        swfState = System.Windows.Forms.ButtonState.Normal;
                        break;

                    case UI.ButtonState.Pressed:
                        swfState = System.Windows.Forms.ButtonState.Pushed;
                        break;
                }

                ControlPaint.DrawButton(g, x, y, width, height, swfState);
            

            GC.KeepAlive(hostControl);
        }

        /// <summary>
        /// This method is obsolete. Use SuspendControlPainting() and ResumeControlPainting() instead.
        /// </remarks>
        [Obsolete("Use SuspendControlPainting() and ResumeControlPainting() instead")]
        public static void SetControlRedraw(Control control, bool enabled)
        {
            SetControlRedrawImpl(control, enabled);
        }

        /// <summary>
        /// Sets the control's redraw state.
        /// </summary>
        /// <param name="control">The control whose state should be modified.</param>
        /// <param name="enabled">The new state for redrawing ability.</param>
        /// <remarks>
        /// Note to implementors: This method is used by SuspendControlPainting() and ResumeControlPainting().
        /// This may be implemented as a no-op.
        /// </remarks>
        private static void SetControlRedrawImpl(Control control, bool enabled)
        {
			if (ywarn_shown)
				return;
			Console.WriteLine ("UI.SetControlRedrawImpl: Not implemented");
			ywarn_shown = true;
		}
	
		static bool ywarn_shown;
	    	
        private static Dictionary<Control, int> controlRedrawStack = new Dictionary<Control, int>();

        /// <summary>
        /// Suspends the control's ability to draw itself.
        /// </summary>
        /// <param name="control">The control to suspend drawing for.</param>
        /// <remarks>
        /// When drawing is suspended, any painting performed in the control's WM_PAINT, OnPaint(),
        /// WM_ERASEBKND, or OnPaintBackground() handlers is completely ignored. Invalidation rectangles
        /// are not accumulated during this period, so when drawing is resumed (with 
        /// ResumeControlPainting()), it is usually a good idea to call Invalidate(true) on the control.
        /// This method must be matched at a later time by a corresponding call to ResumeControlPainting().
        /// If you call SuspendControlPainting() multiple times for the same control, then you must
        /// call ResumeControlPainting() once for each call.
        /// Note to implementors: Do not modify this method. Instead, modify SetControlRedrawImpl(),
        /// which may be implemented as a no-op.
        /// </remarks>
        public static void SuspendControlPainting(Control control)
        {
            int pushCount;

            if (controlRedrawStack.TryGetValue(control, out pushCount))
            {
                ++pushCount;
            }
            else
            {
                pushCount = 1;
            }

            if (pushCount == 1)
            {
                SetControlRedrawImpl(control, false);
            }

            controlRedrawStack[control] = pushCount;
        }

        /// <summary>
        /// Resumes the control's ability to draw itself.
        /// </summary>
        /// <param name="control">The control to suspend drawing for.</param>
        /// <remarks>
        /// This method must be matched by a preceding call to SuspendControlPainting(). If that method
        /// was called multiple times, then this method must be called a corresponding number of times
        /// in order to enable drawing.
        /// This method must be matched at a later time by a corresponding call to ResumeControlPainting().
        /// If you call SuspendControlPainting() multiple times for the same control, then you must
        /// call ResumeControlPainting() once for each call.
        /// Note to implementors: Do not modify this method. Instead, modify SetControlRedrawImpl(),
        /// which may be implemented as a no-op.
        /// </remarks>        
        public static void ResumeControlPainting(Control control)
        {
            int pushCount;

            if (controlRedrawStack.TryGetValue(control, out pushCount))
            {
                --pushCount;
            }
            else
            {
                throw new InvalidOperationException("There was no previous matching SuspendControlPainting() for this control");
            }

            if (pushCount == 0)
            {
                SetControlRedrawImpl(control, true);
                controlRedrawStack.Remove(control);
            }
            else
            {
                controlRedrawStack[control] = pushCount;
            }
        }

        /// <summary>
        /// Queries whether painting is enabled for the given control.
        /// </summary>
        /// <param name="control">The control to query suspension for.</param>
        /// <returns>
        /// false if the control's painting has been suspended via a call to SuspendControlPainting(),
        /// otherwise true.
        /// </returns>
        /// <remarks>
        /// You may use the return value of this method to optimize away painting. If this
        /// method returns false, then you may skip your entire OnPaint() method. This saves
        /// processor time by avoiding all of the non-painting drawing and resource initialization
        /// and destruction that is typically contained in OnPaint().
        /// This method assumes painting suspension is being exclusively managed with Suspend-
        /// and ResumeControlPainting().
        /// </remarks>
        public static bool IsControlPaintingEnabled(Control control)
        {
            int pushCount;

            if (!controlRedrawStack.TryGetValue(control, out pushCount))
            {
                pushCount = 0;
            }

            return (pushCount == 0);
        }

	private static IntPtr hRgn = IntPtr.Zero;

        /// <summary>
        /// This method retrieves the update region of a control.
        /// </summary>
        /// <param name="control">The control to retrieve the update region for.</param>
        /// <returns>
        /// An array of rectangles specifying the area that has been invalidated, or 
        /// null if this could not be determined.
        /// </returns>
        /// <remarks>
        /// Note to implementors: This method may be implemented as a no-op. In this case, just return null.
        /// </remarks>
        public static Rectangle[] GetUpdateRegion(Control control)
        {
#if false
		if (get_clip_rectangles != null){
			object hwnd = object_from_handle.Invoke (null, new object [] {control.Handle});
			if (hwnd != null){
				Rectangle [] rect = (Rectangle []) get_clip_rectangles.Invoke (hwnd, new object [0]);

				Console.WriteLine ("Returning {0} rectangles for {1} that has {2},{3}", rect.Length, control, control.Width, control.Height);
				foreach (Rectangle r in rect){
					Console.WriteLine (r);
				}
				return rect;
			}
		} 
		Console.WriteLine ("WARNING: Failed to calle the rect stuff");
		Console.WriteLine ("Bounds: {0}", control.Bounds);
#endif
		return new Rectangle[1] { new Rectangle(0, 0, control.Width, control.Height) };
        }
		static bool xwarn_shown;

        /// <summary>
        /// Sets a form's opacity.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="opacity"></param>
        /// <remarks>
        /// Note to implementors: This may be implemented as just "form.Opacity = opacity".
        /// This method works around some visual clumsiness in .NET 2.0 related to
        /// transitioning between opacity == 1.0 and opacity != 1.0.</remarks>
        public static void SetFormOpacity(Form form, double opacity)
        {
            if (opacity < 0.0 || opacity > 1.0)
            {
                throw new ArgumentOutOfRangeException("opacity", "must be in the range [0, 1]");
            }

			form.Opacity = opacity;
	    
        }

        /// <summary>
        /// This WndProc implements click-through functionality. Some controls (MenuStrip, ToolStrip) will not
        /// recognize a click unless the form they are hosted in is active. So the first click will activate the
        /// form and then a second is required to actually make the click happen.
        /// </summary>
        /// <param name="m">The Message that was passed to your WndProc.</param>
        /// <returns>true if the message was processed, false if it was not</returns>
        /// <remarks>
        /// You should first call base.WndProc(), and then call this method. This method is only intended to
        /// change a return value, not to change actual processing before that.
        /// </remarks>
        internal static bool ClickThroughWndProc(ref Message m)
        {
            bool returnVal = false;

            if (m.Msg == NativeConstants.WM_MOUSEACTIVATE)
            {
                if (m.Result == (IntPtr)NativeConstants.MA_ACTIVATEANDEAT)
                {
                    m.Result = (IntPtr)NativeConstants.MA_ACTIVATE;
                    returnVal = true;
                }
            }

            return returnVal;
        }

        public static bool IsOurAppActive
        {
            get
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form == Form.ActiveForm)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

		static bool warn_enable_shield_shown = false;
		
        public static void EnableShield(Button button, bool enableShield)
        {
			if (!warn_enable_shield_shown){
				Console.WriteLine ("EnableShieldButton not implemented");
				warn_enable_shield_shown = true;
				return;
			}
			
#if false
            IntPtr hWnd = button.Handle;

            SafeNativeMethods.SendMessageW(
                hWnd,
                NativeConstants.BCM_SETSHIELD,
                IntPtr.Zero,
                enableShield ? new IntPtr(1) : IntPtr.Zero);

            GC.KeepAlive(button);
#endif
        }

        // TODO: get rid of this somehow! (this will happen when Layers window is rewritten, post-3.0)
        public static bool HideHorizontalScrollBar(Control c)
        {
			if (!warn_shown){		
				Console.WriteLine ("PORT: HideHorizontalScrollBar missing");
				warn_shown = true;
			}	
			return true;
        }
        static bool warn_shown;

        public static void RestoreWindow(IWin32Window window)
        {
#if false
            IntPtr hWnd = window.Handle;
            SafeNativeMethods.ShowWindow(hWnd, NativeConstants.SW_RESTORE);
            GC.KeepAlive(window);
#endif
			Console.WriteLine ("PORT: RestoreWindow not implemented");
        }

        public static void ShowComboBox(ComboBox comboBox, bool show)
		{
			comboBox.DroppedDown = show;
        }
    }
}
