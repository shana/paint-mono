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
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PaintDotNet
{
    public class CommonActionsStrip 
        : ToolStripEx
    {
        private ToolStripSeparator separator0;
        private ToolStripButton newButton;
        private ToolStripButton openButton;
        private ToolStripButton saveButton;
        private ToolStripButton printButton;
        private ToolStripSeparator separator1;
        private ToolStripButton cutButton;
        private ToolStripButton copyButton;
        private ToolStripButton pasteButton;
        private ToolStripButton cropButton;
        private ToolStripButton deselectButton;
        private ToolStripSeparator separator2;
        private ToolStripButton undoButton;
        private ToolStripButton redoButton;

        public CommonActionsStrip()
        {
            InitializeComponent();

            this.newButton.Image = PdnResources.GetImage("Icons.MenuFileNewIcon.png");
            this.openButton.Image = PdnResources.GetImage("Icons.MenuFileOpenIcon.png");
            this.saveButton.Image = PdnResources.GetImage("Icons.MenuFileSaveIcon.png");
            this.printButton.Image = PdnResources.GetImage("Icons.MenuFilePrintIcon.png");
            this.cutButton.Image = PdnResources.GetImage("Icons.MenuEditCutIcon.png");
            this.copyButton.Image = PdnResources.GetImage("Icons.MenuEditCopyIcon.png");
            this.pasteButton.Image = PdnResources.GetImage("Icons.MenuEditPasteIcon.png");
            this.cropButton.Image = PdnResources.GetImage("Icons.MenuImageCropIcon.png");
            this.deselectButton.Image = PdnResources.GetImage("Icons.MenuEditDeselectIcon.png");
            this.undoButton.Image = PdnResources.GetImage("Icons.MenuEditUndoIcon.png");
            this.redoButton.Image = PdnResources.GetImage("Icons.MenuEditRedoIcon.png");

            this.newButton.ToolTipText = PdnResources.GetString("CommonAction.New");
            this.openButton.ToolTipText = PdnResources.GetString("CommonAction.Open");
            this.saveButton.ToolTipText = PdnResources.GetString("CommonAction.Save");
            this.printButton.ToolTipText = PdnResources.GetString("CommonAction.Print");
            this.cutButton.ToolTipText = PdnResources.GetString("CommonAction.Cut");
            this.copyButton.ToolTipText = PdnResources.GetString("CommonAction.Copy");
            this.pasteButton.ToolTipText = PdnResources.GetString("CommonAction.Paste");
            this.cropButton.ToolTipText = PdnResources.GetString("CommonAction.CropToSelection");
            this.deselectButton.ToolTipText = PdnResources.GetString("CommonAction.Deselect");
            this.undoButton.ToolTipText = PdnResources.GetString("CommonAction.Undo");
            this.redoButton.ToolTipText = PdnResources.GetString("CommonAction.Redo");

            this.newButton.Tag = CommonAction.New;
            this.openButton.Tag = CommonAction.Open;
            this.saveButton.Tag = CommonAction.Save;
            this.printButton.Tag = CommonAction.Print;
            this.cutButton.Tag = CommonAction.Cut;
            this.copyButton.Tag = CommonAction.Copy;
            this.pasteButton.Tag = CommonAction.Paste;
            this.cropButton.Tag = CommonAction.CropToSelection;
            this.deselectButton.Tag = CommonAction.Deselect;
            this.undoButton.Tag = CommonAction.Undo;
            this.redoButton.Tag = CommonAction.Redo;
        }

        private void InitializeComponent()
        {
            this.separator0 = new ToolStripSeparator();
            this.newButton = new ToolStripButton();
            this.openButton = new ToolStripButton();
            this.saveButton = new ToolStripButton();
            this.printButton = new ToolStripButton();
            this.separator1 = new ToolStripSeparator();
            this.cutButton = new ToolStripButton();
            this.copyButton = new ToolStripButton();
            this.pasteButton = new ToolStripButton();
            this.cropButton = new ToolStripButton();
            this.deselectButton = new ToolStripButton();
            this.separator2 = new ToolStripSeparator();
            this.undoButton = new ToolStripButton();
            this.redoButton = new ToolStripButton();

            this.SuspendLayout();

            this.Items.Add(this.separator0);
            this.Items.Add(this.newButton);
            this.Items.Add(this.openButton);
            this.Items.Add(this.saveButton);
            this.Items.Add(this.printButton);
            this.Items.Add(this.separator1);
            this.Items.Add(this.cutButton);
            this.Items.Add(this.copyButton);
            this.Items.Add(this.pasteButton);
            this.Items.Add(this.cropButton);
            this.Items.Add(this.deselectButton);
            this.Items.Add(this.separator2);
            this.Items.Add(this.undoButton);
            this.Items.Add(this.redoButton);

            this.ResumeLayout(false);
        }

        public event EnumValueEventHandler ButtonClick;
        protected void OnButtonClick(CommonAction action)
        {
            if (ButtonClick != null)
            {
                ButtonClick(this, new EnumValueEventArgs(action));
            }
        }

        protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem is ToolStripButton)
            {
                CommonAction action = (CommonAction)e.ClickedItem.Tag;
                OnButtonClick(action);
            }

            base.OnItemClicked(e);
        }

        public void SetButtonEnabled(CommonAction action, bool enabled)
        {
            ToolStripButton button = FindButton(action);
            button.Enabled = enabled;
        }

        public void SetButtonVisible(CommonAction action, bool visible)
        {
            ToolStripButton button = FindButton(action);
            button.Visible = visible;
        }

        public bool GetButtonEnabled(CommonAction action)
        {
            ToolStripButton button = FindButton(action);
            return button.Enabled;
        }

        public bool GetButtonVisible(CommonAction action)
        {
            ToolStripButton button = FindButton(action);
            return button.Visible;
        }

        private ToolStripButton FindButton(CommonAction action)
        {
            ToolStripButton button;

            switch (action)
            {
                case CommonAction.New:
                    button = this.newButton;
                    break;

                case CommonAction.Open:
                    button = this.openButton;
                    break;

                case CommonAction.Save:
                    button = this.saveButton;
                    break;

                case CommonAction.Print:
                    button = this.printButton;
                    break;

                case CommonAction.Cut:
                    button = this.cutButton;
                    break;

                case CommonAction.Copy:
                    button = this.copyButton;
                    break;

                case CommonAction.Paste:
                    button = this.pasteButton;
                    break;
                    
                case CommonAction.CropToSelection:
                    button = this.cropButton;
                    break;

                case CommonAction.Deselect:
                    button = this.deselectButton;
                    break;

                case CommonAction.Undo:
                    button = this.undoButton;
                    break;

                case CommonAction.Redo:
                    button = this.redoButton;
                    break;

                default:
                    throw new InvalidEnumArgumentException();
            }

            return button;
        }
    }
}
