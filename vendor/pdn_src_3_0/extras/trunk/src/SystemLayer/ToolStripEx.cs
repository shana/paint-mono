/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;

namespace PaintDotNet.SystemLayer
{
    /// <summary>
    /// This class adds on to the functionality provided in System.Windows.Forms.ToolStrip.
    /// </summary>
    /// <remarks>
    /// The first aggravating thing I found out about ToolStrip is that it does not "click through."
    /// If the form that is hosting a ToolStrip is not active and you click on a button in the tool
    /// strip, it sets focus to the form but does NOT click the button. This makes sense in many
    /// situations, but definitely not for Paint.NET.
    /// </remarks>
    public class ToolStripEx
        : ToolStrip
    {
        private bool clickThrough = true;
        private bool managedFocus = true;
        private static int enteredComboBox = 0;

        public ToolStripEx()
        {
            ToolStripProfessionalRenderer tspr = this.Renderer as ToolStripProfessionalRenderer;

            if (tspr != null)
            {
                tspr.ColorTable.UseSystemColors = true;
                tspr.RoundedEdges = false;
            }

            this.ImageScalingSize = new System.Drawing.Size(UI.ScaleWidth(16), UI.ScaleHeight(16));
        }

        protected override bool ProcessCmdKey(ref Message m, Keys keyData)
        {
            bool processed = false;
            Form form = this.FindForm();
            FormEx formEx = FormEx.FindFormEx(form);

            if (formEx != null)
            {
                processed = formEx.RelayProcessCmdKey(keyData);
            }

            return processed;
        }

        /// <summary>
        /// Gets or sets whether the ToolStripEx honors item clicks when its containing form does
        /// not have input focus.
        /// </summary>
        /// <remarks>
        /// Default value is true, which is the opposite of the behavior provided by the base
        /// ToolStrip class.
        /// </remarks>
        public bool ClickThrough
        {
            get
            {
                return this.clickThrough;
            }

            set
            {
                this.clickThrough = value;
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (this.clickThrough)
            {
                UI.ClickThroughWndProc(ref m);
            }
        }

        /// <summary>
        /// This event is raised when this toolstrip instance wishes to relinquish focus.
        /// </summary>
        public event EventHandler RelinquishFocus;

        private void OnRelinquishFocus()
        {
            if (RelinquishFocus != null)
            {
                RelinquishFocus(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets whether the toolstrip manages focus.
        /// </summary>
        /// <remarks>
        /// If this is true, the toolstrip will capture focus when the mouse enters its client area. It will then
        /// relinquish focus (via the RelinquishFocus event) when the mouse leaves. It will not capture or
        /// attempt to relinquish focus if MenuStripEx.IsAnyMenuActive returns true.
        /// </remarks>
        public bool ManagedFocus
        {
            get
            {
                return this.managedFocus;
            }

            set
            {
                this.managedFocus = value;
            }
        }

        protected override void OnItemAdded(ToolStripItemEventArgs e)
        {
            ToolStripComboBox tscb = e.Item as ToolStripComboBox;

            if (tscb == null)
            {
                e.Item.MouseEnter += OnItemMouseEnter;
            }
            else
            {
                tscb.DropDownClosed += new EventHandler(ComboBox_DropDownClosed);
                tscb.Enter += new EventHandler(ComboBox_Enter);
                tscb.Leave += new EventHandler(ComboBox_Leave);
            }

            base.OnItemAdded(e);
        }

        private void ComboBox_Leave(object sender, EventArgs e)
        {
            --enteredComboBox;
        }

        private void ComboBox_Enter(object sender, EventArgs e)
        {
            ++enteredComboBox;
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            OnRelinquishFocus();
        }

        protected override void OnItemRemoved(ToolStripItemEventArgs e)
        {
            ToolStripComboBox tscb = e.Item as ToolStripComboBox;

            if (tscb == null)
            {
                e.Item.MouseEnter -= OnItemMouseEnter;
            }
            else
            {
                tscb.DropDownClosed -= new EventHandler(ComboBox_DropDownClosed);
                tscb.Enter -= new EventHandler(ComboBox_Enter);
                tscb.Leave -= new EventHandler(ComboBox_Leave);
            }

            base.OnItemRemoved(e);
        }

        private void OnItemMouseEnter(object sender, EventArgs e)
        {
            if (this.managedFocus && !MenuStripEx.IsAnyMenuActive && UI.IsOurAppActive && enteredComboBox == 0)
            {
                this.Focus();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (this.managedFocus && !MenuStripEx.IsAnyMenuActive && UI.IsOurAppActive && enteredComboBox == 0)
            {
                OnRelinquishFocus();
            }

            base.OnMouseLeave(e);
        }
    }
}
