/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet.Actions;
using PaintDotNet.SystemLayer;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PaintDotNet.Menus
{
    public sealed class FileMenu
        : PdnMenuItem
    {
        private PdnMenuItem menuFileNew;
        private PdnMenuItem menuFileOpen;
        private PdnMenuItem menuFileOpenRecent;
        private PdnMenuItem menuFileOpenRecentSentinel;
        private PdnMenuItem menuFileAcquire;
        private PdnMenuItem menuFileAcquireFromScannerOrCamera;
        private PdnMenuItem menuFileClose;
        private ToolStripSeparator menuFileSeparator1;
        private PdnMenuItem menuFileSave;
        private PdnMenuItem menuFileSaveAs;
        private ToolStripSeparator menuFileSeparator2;
        private PdnMenuItem menuFilePrint;
        private ToolStripSeparator menuFileSeparator3;
        private PdnMenuItem menuFileExit;

        private bool OnCtrlF4Typed(Keys keys)
        {
            this.menuFileClose.PerformClick();
            return true;
        }

        public FileMenu()
        {
            PdnBaseForm.RegisterFormHotKey(Keys.Control | Keys.F4, OnCtrlF4Typed);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.menuFileNew = new PdnMenuItem();
            this.menuFileOpen = new PdnMenuItem();
            this.menuFileOpenRecent = new PdnMenuItem();
            this.menuFileOpenRecentSentinel = new PdnMenuItem();
            this.menuFileAcquire = new PdnMenuItem();
            this.menuFileAcquireFromScannerOrCamera = new PdnMenuItem();
            this.menuFileClose = new PdnMenuItem();
            this.menuFileSeparator1 = new ToolStripSeparator();
            this.menuFileSave = new PdnMenuItem();
            this.menuFileSaveAs = new PdnMenuItem();
            this.menuFileSeparator2 = new ToolStripSeparator();
            this.menuFilePrint = new PdnMenuItem();
            this.menuFileSeparator3 = new ToolStripSeparator();
            this.menuFileExit = new PdnMenuItem();
            //
            // FileMenu
            //
            this.DropDownItems.AddRange(
                new ToolStripItem[] 
                {
                    this.menuFileNew,
                    this.menuFileOpen,
                    this.menuFileOpenRecent,
                    this.menuFileAcquire,
                    this.menuFileClose,
                    this.menuFileSeparator1,
                    this.menuFileSave,
                    this.menuFileSaveAs,
                    this.menuFileSeparator2,
                    this.menuFilePrint,
                    this.menuFileSeparator3,
                    this.menuFileExit
                }); 
            this.Name = "Menu.File";
            this.Text = PdnResources.GetString("Menu.File.Text");
            // 
            // menuFileNew
            // 
            this.menuFileNew.Name = "New";
            this.menuFileNew.ShortcutKeys = Keys.Control | Keys.N;
            this.menuFileNew.Click += new System.EventHandler(this.MenuFileNew_Click);
            // 
            // menuFileOpen
            // 
            this.menuFileOpen.Name = "Open";
            this.menuFileOpen.ShortcutKeys = Keys.Control | Keys.O;
            this.menuFileOpen.Click += new System.EventHandler(this.MenuFileOpen_Click);
            // 
            // menuFileOpenRecent
            // 
            this.menuFileOpenRecent.Name = "OpenRecent";
            this.menuFileOpenRecent.DropDownItems.AddRange(
                new ToolStripItem[] 
                {
                    this.menuFileOpenRecentSentinel
                });
            this.menuFileOpenRecent.DropDownOpening += new System.EventHandler(this.MenuFileOpenRecent_DropDownOpening);
            // 
            // menuFileOpenRecentSentinel
            // 
            this.menuFileOpenRecentSentinel.Text = "sentinel";
            // 
            // menuFileAcquire
            // 
            this.menuFileAcquire.Name = "Acquire";
            this.menuFileAcquire.DropDownItems.AddRange(
                new ToolStripItem[] 
                {
                    this.menuFileAcquireFromScannerOrCamera
                });
            this.menuFileAcquire.DropDownOpening += new System.EventHandler(this.MenuFileAcquire_DropDownOpening);
            // 
            // menuFileAcquireFromScannerOrCamera
            // 
            this.menuFileAcquireFromScannerOrCamera.Name = "FromScannerOrCamera";
            this.menuFileAcquireFromScannerOrCamera.Click += new System.EventHandler(this.MenuFileAcquireFromScannerOrCamera_Click);
            //
            // menuFileClose
            //
            this.menuFileClose.Name = "Close";
            this.menuFileClose.Click += new EventHandler(MenuFileClose_Click);
            this.menuFileClose.ShortcutKeys = Keys.Control | Keys.W;
            // 
            // menuFileSave
            // 
            this.menuFileSave.Name = "Save";
            this.menuFileSave.ShortcutKeys = Keys.Control | Keys.S;
            this.menuFileSave.Click += new System.EventHandler(this.MenuFileSave_Click);
            // 
            // menuFileSaveAs
            // 
            this.menuFileSaveAs.Name = "SaveAs";
            this.menuFileSaveAs.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            this.menuFileSaveAs.Click += new System.EventHandler(this.MenuFileSaveAs_Click);
            // 
            // menuFilePrint
            // 
            this.menuFilePrint.Name = "Print";
            this.menuFilePrint.ShortcutKeys = Keys.Control | Keys.P;
            this.menuFilePrint.Click += new System.EventHandler(this.MenuFilePrint_Click);
            // 
            // menuFileExit
            // 
            this.menuFileExit.Name = "Exit";
            this.menuFileExit.Click += new System.EventHandler(this.MenuFileExit_Click);
        }

        protected override void OnDropDownOpening(EventArgs e)
        {
            this.menuFileNew.Enabled = true;
            this.menuFileOpen.Enabled = true;
            this.menuFileOpenRecent.Enabled = true;
            this.menuFileOpenRecentSentinel.Enabled = true;
            this.menuFileAcquire.Enabled = true;
            this.menuFileAcquireFromScannerOrCamera.Enabled = true;
            this.menuFileExit.Enabled = true;

            if (AppWorkspace.ActiveDocumentWorkspace != null)
            {
                this.menuFileSave.Enabled = true;
                this.menuFileSaveAs.Enabled = true;
                this.menuFileClose.Enabled = true;
                this.menuFilePrint.Enabled = true;
            }
            else
            {
                this.menuFileSave.Enabled = false;
                this.menuFileSaveAs.Enabled = false;
                this.menuFileClose.Enabled = false;
                this.menuFilePrint.Enabled = false;
            }

            base.OnDropDownOpening(e);
        }

        private void MenuFileOpen_Click(object sender, System.EventArgs e)
        {
            AppWorkspace.PerformAction(new OpenFileAction());
        }

        private void MenuFileExit_Click(object sender, System.EventArgs e)
        {
            Startup.CloseApplication();
        }

        private void MenuFileClose_Click(object sender, EventArgs e)
        {
            this.AppWorkspace.PerformAction(new CloseWorkspaceAction());
        }

        private void MenuFileSaveAs_Click(object sender, System.EventArgs e)
        {
            if (AppWorkspace.ActiveDocumentWorkspace != null)
            {
                AppWorkspace.ActiveDocumentWorkspace.DoSaveAs();
            }
        }

        private void MenuFileSave_Click(object sender, System.EventArgs e)
        {
            if (AppWorkspace.ActiveDocumentWorkspace != null)
            {
                AppWorkspace.ActiveDocumentWorkspace.DoSave();
            }
        }

        private void MenuFileAcquire_DropDownOpening(object sender, System.EventArgs e)
        {
            // We only disable the scanner menu item if we know for sure a scanner is not available
            // If WIA isn't available we leave the menu item enabled. That way we can give an
            // informative error message when the user clicks on it and say "scanning requires XP SP1"
            // Otherwise the user is confused and will make scathing posts on our forum.
            bool scannerEnabled = true;

            if (ScanningAndPrinting.IsComponentAvailable)
            {
                if (!ScanningAndPrinting.CanScan)
                {
                    scannerEnabled = false;
                }
            }

            menuFileAcquireFromScannerOrCamera.Enabled = scannerEnabled;
        }

        private void MenuFilePrint_Click(object sender, System.EventArgs e)
        {
            if (this.AppWorkspace.ActiveDocumentWorkspace != null)
            {
                this.AppWorkspace.ActiveDocumentWorkspace.PerformAction(new PrintAction());
            }
        }

        private void MenuFileOpenInNewWindow_Click(object sender, System.EventArgs e)
        {
            string fileName;
            string startingDir = Path.GetDirectoryName(AppWorkspace.ActiveDocumentWorkspace.FilePath);
            DialogResult result = DocumentWorkspace.ChooseFile(AppWorkspace, out fileName, startingDir);

            if (result == DialogResult.OK)
            {
                Startup.StartNewInstance(AppWorkspace, fileName);
            }
        }

        private void MenuFileNewWindow_Click(object sender, System.EventArgs e)
        {
            Startup.StartNewInstance(AppWorkspace, null);
        }

        private void MenuFileOpenRecent_DropDownOpening(object sender, System.EventArgs e)
        {
            AppWorkspace.MostRecentFiles.LoadMruList();
            MostRecentFile[] filesReverse = AppWorkspace.MostRecentFiles.GetFileList();
            MostRecentFile[] files = new MostRecentFile[filesReverse.Length];
            int i;

            for (i = 0; i < filesReverse.Length; ++i)
            {
                files[files.Length - i - 1] = filesReverse[i];
            }

            foreach (ToolStripItem mi in menuFileOpenRecent.DropDownItems)
            {
                mi.Click -= new EventHandler(MenuFileOpenRecentFile_Click);
            }

            menuFileOpenRecent.DropDownItems.Clear();

            i = 0;

            foreach (MostRecentFile mrf in files)
            {
                string menuName;

                if (i < 9)
                {
                    menuName = "&";
                }
                else
                {
                    menuName = "";
                }

                menuName += (1 + i).ToString() + " " + Path.GetFileName(mrf.FileName);
                ToolStripMenuItem mi = new ToolStripMenuItem(menuName);
                mi.Click += new EventHandler(MenuFileOpenRecentFile_Click);
                mi.ImageScaling = ToolStripItemImageScaling.None;
                mi.Image = (Image)mrf.Thumb.Clone();
                menuFileOpenRecent.DropDownItems.Add(mi);
                ++i;
            }

            if (menuFileOpenRecent.DropDownItems.Count == 0)
            {
                ToolStripMenuItem none = new ToolStripMenuItem(PdnResources.GetString("Menu.File.OpenRecent.None"));
                none.Enabled = false;
                menuFileOpenRecent.DropDownItems.Add(none);
            }
            else
            {
                ToolStripSeparator separator = new ToolStripSeparator();
                menuFileOpenRecent.DropDownItems.Add(separator);

                ToolStripMenuItem clearList = new ToolStripMenuItem();
                clearList.Text = PdnResources.GetString("Menu.File.OpenRecent.ClearThisList");
                menuFileOpenRecent.DropDownItems.Add(clearList);
                Image deleteIcon = PdnResources.GetImage("Icons.MenuEditEraseSelectionIcon.png");
                clearList.ImageTransparentColor = Utility.TransparentKey;
                clearList.ImageAlign = ContentAlignment.MiddleCenter;
                clearList.ImageScaling = ToolStripItemImageScaling.None;
                int iconSize = AppWorkspace.MostRecentFiles.IconSize;
                Bitmap bitmap = new Bitmap(iconSize + 2, iconSize + 2);

                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(clearList.ImageTransparentColor);

                    Point offset = new Point((bitmap.Width - deleteIcon.Width) / 2,
                        (bitmap.Height - deleteIcon.Height) / 2);

                    g.CompositingMode = CompositingMode.SourceCopy;
                    g.DrawImage(deleteIcon, offset.X, offset.Y, deleteIcon.Width, deleteIcon.Height);
                }

                clearList.Image = bitmap;
                clearList.Click += new EventHandler(ClearList_Click);
            }
        }

        private void MenuFileOpenRecentFile_Click(object sender, System.EventArgs e)
        {
            try
            {
                ToolStripMenuItem mi = (ToolStripMenuItem)sender;
                int spaceIndex = mi.Text.IndexOf(" ");
                string indexString = mi.Text.Substring(1, spaceIndex - 1);
                int index = int.Parse(indexString) - 1;
                MostRecentFile[] recentFiles = AppWorkspace.MostRecentFiles.GetFileList();
                string fileName = recentFiles[recentFiles.Length - index - 1].FileName;
                AppWorkspace.OpenFileInNewWorkspace(fileName);
            }

            catch
            {
            }
        }

        private void MenuFileNew_Click(object sender, System.EventArgs e)
        {
            AppWorkspace.PerformAction(new NewImageAction());
        }

        private void MenuFileAcquireFromScannerOrCamera_Click(object sender, System.EventArgs e)
        {
            AppWorkspace.PerformAction(new AcquireFromScannerOrCameraAction());
        }

        private void ClearList_Click(object sender, EventArgs e)
        {
            AppWorkspace.PerformAction(new ClearMruListAction());
        }
    }
}
