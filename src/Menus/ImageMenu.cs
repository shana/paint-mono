/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet.Actions;
using PaintDotNet.HistoryFunctions;
using System;
using System.Windows.Forms;

namespace PaintDotNet.Menus
{
    public sealed class ImageMenu
        : PdnMenuItem
    {
        private PdnMenuItem menuImageCrop;
        private PdnMenuItem menuImageResize;
        private PdnMenuItem menuImageCanvasSize;
        private ToolStripSeparator menuImageSeparator1;
        private PdnMenuItem menuImageFlip;
        private PdnMenuItem menuImageFlipHorizontal;
        private PdnMenuItem menuImageFlipVertical;
        private PdnMenuItem menuImageRotate;
        private PdnMenuItem menuImageRotate90CW;
        private PdnMenuItem menuImageRotate180CW;
        private PdnMenuItem menuImageRotate270CW;
        private ToolStripSeparator menuImageRotateSeparator;
        private PdnMenuItem menuImageRotate90CCW;
        private PdnMenuItem menuImageRotate180CCW;
        private PdnMenuItem menuImageRotate270CCW;
        private ToolStripSeparator menuImageSeparator2;
        private PdnMenuItem menuImageFlatten;

        public ImageMenu()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.menuImageCrop = new PdnMenuItem();
            this.menuImageResize = new PdnMenuItem();
            this.menuImageCanvasSize = new PdnMenuItem();
            this.menuImageSeparator1 = new ToolStripSeparator();
            this.menuImageFlip = new PdnMenuItem();
            this.menuImageFlipHorizontal = new PdnMenuItem();
            this.menuImageFlipVertical = new PdnMenuItem();
            this.menuImageRotate = new PdnMenuItem();
            this.menuImageRotate90CW = new PdnMenuItem();
            this.menuImageRotate180CW = new PdnMenuItem();
            this.menuImageRotate270CW = new PdnMenuItem();
            this.menuImageRotateSeparator = new ToolStripSeparator();
            this.menuImageRotate90CCW = new PdnMenuItem();
            this.menuImageRotate180CCW = new PdnMenuItem();
            this.menuImageRotate270CCW = new PdnMenuItem();
            this.menuImageSeparator2 = new ToolStripSeparator();
            this.menuImageFlatten = new PdnMenuItem();
            //
            // ImageMenu
            //
            this.DropDownItems.AddRange(
                new System.Windows.Forms.ToolStripItem[] 
                {
                    this.menuImageCrop,
                    this.menuImageResize,
                    this.menuImageCanvasSize,
                    this.menuImageSeparator1,
                    this.menuImageFlip,
                    this.menuImageRotate,
                    this.menuImageSeparator2,
                    this.menuImageFlatten 
                });
            this.Name = "Menu.Image";
            this.Text = PdnResources.GetString("Menu.Image.Text"); 
            // 
            // menuImageCrop
            // 
            this.menuImageCrop.Name = "Crop";
            this.menuImageCrop.Click += new System.EventHandler(this.MenuImageCrop_Click);
            this.menuImageCrop.ShortcutKeys = Keys.Control | Keys.Shift | Keys.X;
            // 
            // menuImageResize
            // 
            this.menuImageResize.Name = "Resize";
            this.menuImageResize.ShortcutKeys = Keys.Control | Keys.R;
            this.menuImageResize.Click += new System.EventHandler(this.MenuImageResize_Click);
            // 
            // menuImageCanvasSize
            // 
            this.menuImageCanvasSize.Name = "CanvasSize";
            this.menuImageCanvasSize.ShortcutKeys = Keys.Control | Keys.Shift | Keys.R;
            this.menuImageCanvasSize.Click += new System.EventHandler(this.MenuImageCanvasSize_Click);
            // 
            // menuImageFlip
            // 
            this.menuImageFlip.Name = "Flip";
            this.menuImageFlip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                                                                          this.menuImageFlipHorizontal,
                                                                                          this.menuImageFlipVertical});
            // 
            // menuImageFlipHorizontal
            // 
            this.menuImageFlipHorizontal.Name = "Horizontal";
            this.menuImageFlipHorizontal.Click += new System.EventHandler(this.MenuImageFlipHorizontal_Click);
            // 
            // menuImageFlipVertical
            // 
            this.menuImageFlipVertical.Name = "Vertical";
            this.menuImageFlipVertical.Click += new System.EventHandler(this.MenuImageFlipVertical_Click);
            // 
            // menuImageRotate
            // 
            this.menuImageRotate.Name = "Rotate";
            this.menuImageRotate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                                                                            this.menuImageRotate90CW,
                                                                                            this.menuImageRotate180CW,
                                                                                            this.menuImageRotate270CW,
                                                                                            this.menuImageRotateSeparator,
                                                                                            this.menuImageRotate90CCW,
                                                                                            this.menuImageRotate180CCW,
                                                                                            this.menuImageRotate270CCW});
            // 
            // menuImageRotate90CW
            // 
            this.menuImageRotate90CW.Name = "90CW";
            this.menuImageRotate90CW.ShortcutKeys = Keys.Control | Keys.H;
            this.menuImageRotate90CW.Click += new System.EventHandler(this.MenuImageRotate90CW_Click);
            // 
            // menuImageRotate180CW
            // 
            this.menuImageRotate180CW.Name = "180CW";
            this.menuImageRotate180CW.Click += new System.EventHandler(this.MenuImageRotate180CW_Click);
            // 
            // menuImageRotate270CW
            // 
            this.menuImageRotate270CW.Name = "270CW";
            this.menuImageRotate270CW.Click += new System.EventHandler(this.MenuImageRotate270CW_Click);
            // 
            // menuImageRotate90CCW
            // 
            this.menuImageRotate90CCW.Name = "90CCW";
            this.menuImageRotate90CCW.ShortcutKeys = Keys.Control | Keys.G;
            this.menuImageRotate90CCW.Click += new System.EventHandler(this.MenuImageRotate90CCW_Click);
            // 
            // menuImageRotate180CCW
            // 
            this.menuImageRotate180CCW.Name = "180CCW";
            this.menuImageRotate180CCW.Click += new System.EventHandler(this.MenuImageRotate180CCW_Click);
            // 
            // menuImageRotate270CCW
            // 
            this.menuImageRotate270CCW.Name = "270CCW";
            this.menuImageRotate270CCW.Click += new System.EventHandler(this.MenuImageRotate270CCW_Click);
            // 
            // menuImageFlatten
            // 
            this.menuImageFlatten.Name = "Flatten";
            this.menuImageFlatten.ShortcutKeys = Keys.Control | Keys.Shift | Keys.F;
            this.menuImageFlatten.Click += new System.EventHandler(this.MenuImageFlatten_Click);
        }

        protected override void OnDropDownOpening(EventArgs e)
        {
            if (AppWorkspace.ActiveDocumentWorkspace == null)
            {
                this.menuImageCrop.Enabled = false;
                this.menuImageResize.Enabled = false;
                this.menuImageCanvasSize.Enabled = false;
                this.menuImageFlipHorizontal.Enabled = false;
                this.menuImageFlipVertical.Enabled = false;
                this.menuImageRotate90CW.Enabled = false;
                this.menuImageRotate180CW.Enabled = false;
                this.menuImageRotate270CW.Enabled = false;
                this.menuImageRotate90CCW.Enabled = false;
                this.menuImageRotate180CCW.Enabled = false;
                this.menuImageRotate270CCW.Enabled = false;
                this.menuImageFlatten.Enabled = false;
            }
            else
            {
                this.menuImageCrop.Enabled = !AppWorkspace.ActiveDocumentWorkspace.Selection.IsEmpty;
                this.menuImageResize.Enabled = true;
                this.menuImageCanvasSize.Enabled = true;
                this.menuImageFlipHorizontal.Enabled = true;
                this.menuImageFlipVertical.Enabled = true;
                this.menuImageRotate90CW.Enabled = true;
                this.menuImageRotate180CW.Enabled = true;
                this.menuImageRotate270CW.Enabled = true;
                this.menuImageRotate90CCW.Enabled = true;
                this.menuImageRotate180CCW.Enabled = true;
                this.menuImageRotate270CCW.Enabled = true;
                this.menuImageFlatten.Enabled = (AppWorkspace.ActiveDocumentWorkspace.Document.Layers.Count > 1);
            }

            base.OnDropDownOpening(e);
        }

        private void MenuImageCrop_Click(object sender, System.EventArgs e)
        {
            if (AppWorkspace.ActiveDocumentWorkspace != null)
            {
                if (!AppWorkspace.ActiveDocumentWorkspace.Selection.IsEmpty)
                {
                    AppWorkspace.ActiveDocumentWorkspace.ExecuteFunction(new CropToSelectionFunction());
                }
            }
        }

        private void MenuImageResize_Click(object sender, System.EventArgs e)
        {
            if (AppWorkspace.ActiveDocumentWorkspace != null)
            {
                AppWorkspace.ActiveDocumentWorkspace.PerformAction(new ResizeAction());
            }
        }

        private void MenuImageCanvasSize_Click(object sender, System.EventArgs e)
        {
            if (AppWorkspace.ActiveDocumentWorkspace != null)
            {
                AppWorkspace.ActiveDocumentWorkspace.PerformAction(new CanvasSizeAction());
            }
        }

        private void MenuImageFlipHorizontal_Click(object sender, System.EventArgs e)
        {
            if (AppWorkspace.ActiveDocumentWorkspace != null)
            {
                AppWorkspace.ActiveDocumentWorkspace.ExecuteFunction(new FlipDocumentHorizontalFunction());
            }
        }

        private void MenuImageFlipVertical_Click(object sender, System.EventArgs e)
        {
            if (AppWorkspace.ActiveDocumentWorkspace != null)
            {
                AppWorkspace.ActiveDocumentWorkspace.ExecuteFunction(new FlipDocumentVerticalFunction());
            }
        }

        private void MenuImageRotate90CW_Click(object sender, System.EventArgs e)
        {
            if (AppWorkspace.ActiveDocumentWorkspace != null)
            {
                HistoryFunction da = new RotateDocumentFunction(RotateType.Clockwise90);
                AppWorkspace.ActiveDocumentWorkspace.ExecuteFunction(da);
            }
        }

        private void MenuImageRotate180CW_Click(object sender, System.EventArgs e)
        {
            if (AppWorkspace.ActiveDocumentWorkspace != null)
            {
                HistoryFunction da = new RotateDocumentFunction(RotateType.Clockwise180);
                AppWorkspace.ActiveDocumentWorkspace.ExecuteFunction(da);
            }
        }

        private void MenuImageRotate270CW_Click(object sender, System.EventArgs e)
        {
            if (AppWorkspace.ActiveDocumentWorkspace != null)
            {
                HistoryFunction da = new RotateDocumentFunction(RotateType.Clockwise270);
                AppWorkspace.ActiveDocumentWorkspace.ExecuteFunction(da);
            }
        }

        private void MenuImageRotate90CCW_Click(object sender, System.EventArgs e)
        {
            if (AppWorkspace.ActiveDocumentWorkspace != null)
            {
                HistoryFunction da = new RotateDocumentFunction(RotateType.CounterClockwise90);
                AppWorkspace.ActiveDocumentWorkspace.ExecuteFunction(da);
            }
        }

        private void MenuImageRotate180CCW_Click(object sender, System.EventArgs e)
        {
            if (AppWorkspace.ActiveDocumentWorkspace != null)
            {
                HistoryFunction da = new RotateDocumentFunction(RotateType.CounterClockwise180);
                AppWorkspace.ActiveDocumentWorkspace.ExecuteFunction(da);
            }
        }

        private void MenuImageRotate270CCW_Click(object sender, System.EventArgs e)
        {
            if (AppWorkspace.ActiveDocumentWorkspace != null)
            {
                HistoryFunction da = new RotateDocumentFunction(RotateType.CounterClockwise270);
                AppWorkspace.ActiveDocumentWorkspace.ExecuteFunction(da);
            }
        }

        private void MenuImageFlatten_Click(object sender, System.EventArgs e)
        {
            if (AppWorkspace.ActiveDocumentWorkspace != null)
            {
                if (AppWorkspace.ActiveDocumentWorkspace.Document.Layers.Count > 1)
                {
                    AppWorkspace.ActiveDocumentWorkspace.ExecuteFunction(new FlattenFunction());
                }
            }
        }
    }
}
