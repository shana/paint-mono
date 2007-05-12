/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using PaintDotNet.Effects;
using PaintDotNet.HistoryMementos;
using PaintDotNet.SystemLayer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace PaintDotNet.Menus
{
    public abstract class EffectMenuBase
        : PdnMenuItem
    {
        private const int tilesPerCpu = 50;
        private int renderingThreadCount = Math.Max(2, SystemLayer.Processor.LogicalCpuCount);
        private const int effectRefreshInterval = 25;
        private PdnRegion[] progressRegions;
        private int progressRegionsStartIndex;

        private PdnMenuItem sentinel;
        private bool menuPopulated = false;
        private EffectsCollection effects;

        private Effect lastEffect = null;
        private EffectConfigToken lastEffectToken = null;
        private Dictionary<Type, EffectConfigToken> effectTokens = new Dictionary<Type, EffectConfigToken>();

        private System.Windows.Forms.Timer invalidateTimer;
        private System.ComponentModel.Container components = null;

        protected abstract bool EnableEffectShortcuts
        {
            get;
        }

        protected abstract bool EnableRepeatEffectMenuItem
        {
            get;
        }

        protected abstract bool FilterEffects(Effect effect);

        public EffectMenuBase()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.sentinel = new PdnMenuItem();
            //
            // sentinel
            //
            this.sentinel.Name = null;
            //
            // components
            //
            this.components = new System.ComponentModel.Container();
            //
            // invalidateTimer
            //
            this.invalidateTimer = new System.Windows.Forms.Timer(this.components);
            this.invalidateTimer.Enabled = false;
            this.invalidateTimer.Tick += InvalidateTimer_Tick;
            this.invalidateTimer.Interval = effectRefreshInterval;
            //
            // EffectMenuBase
            //
            this.DropDownItems.Add(sentinel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                    this.components = null;
                }
            }

 	        base.Dispose(disposing);
        }

        protected override void OnDropDownOpening(EventArgs e)
        {
            if (!this.menuPopulated)
            {
                PopulateMenu();
            }

            bool enabled = (AppWorkspace.ActiveDocumentWorkspace != null);

            foreach (ToolStripItem item in this.DropDownItems)
            {
                item.Enabled = enabled;
            }

            base.OnDropDownOpening(e);
        }

        public EffectsCollection Effects
        {
            get
            {
                if (this.effects == null)
                {
                    this.effects = GatherEffects();
                }

                return this.effects;
            }
        }

        public void PopulateEffects()
        {
            PopulateMenu(false);
        }

        private void PopulateMenu(bool forceRepopulate)
        {
            if (forceRepopulate)
            {
                this.menuPopulated = false;
            }

            PopulateMenu();
        }

        private void PopulateMenu()
        {
            this.DropDownItems.Clear();

            if (EnableRepeatEffectMenuItem && this.lastEffect != null)
            {
                string repeatFormat = PdnResources.GetString("Effects.RepeatMenuItem.Format");
                string menuName = string.Format(repeatFormat, lastEffect.Name);
                PdnMenuItem pmi = new PdnMenuItem(menuName, this.lastEffect.Image, RepeatEffectMenuItem_Click);
                pmi.ShortcutKeys = Keys.Control | Keys.F; 
                this.DropDownItems.Add(pmi);

                ToolStripSeparator tss = new ToolStripSeparator();
                this.DropDownItems.Add(tss);
            }

            AddEffectsToMenu();
        }

        protected virtual Keys GetEffectShortcutKeys(Effect effect)
        {
            return Keys.None;
        }

        private void AddEffectToMenu(Effect effect, bool withShortcut)
        {
            if (!FilterEffects(effect))
            {
                return;
            }

            string name = effect.Name;

            if (effect.IsConfigurable)
            {
                string configurableFormat = PdnResources.GetString("Effects.Name.Format.Configurable");
                name = string.Format(configurableFormat, name);
            }

            PdnMenuItem mi = new PdnMenuItem(name, effect.Image, EffectMenuItem_Click);

            if (withShortcut)
            {
                mi.ShortcutKeys = GetEffectShortcutKeys(effect);
            }
            else
            {
                mi.ShortcutKeys = Keys.None;
            }

            mi.Tag = (object)effect.GetType();

            PdnMenuItem addEffectHere = this;

            if (effect.SubMenuName != null)
            {
                PdnMenuItem subMenu = null;

                // search for this subMenu
                foreach (ToolStripItem sub in this.DropDownItems)
                {
                    PdnMenuItem subpmi = sub as PdnMenuItem;

                    if (subpmi != null)
                    {
                        if (subpmi.Text == effect.SubMenuName)
                        {
                            subMenu = subpmi;
                            break;
                        }
                    }
                }

                if (subMenu == null)
                {
                    subMenu = new PdnMenuItem(effect.SubMenuName, null, null);
                    this.DropDownItems.Add(subMenu);
                }

                addEffectHere = subMenu;
            }

            addEffectHere.DropDownItems.Add(mi);
        }

        private void AddEffectsToMenu()
        {
            // Fill the menu with the effect names, and "..." if it is configurable
            EffectsCollection effectsCollection = this.Effects;
            Type[] effectTypes = effectsCollection.Effects;
            bool withShortcuts = EnableEffectShortcuts;

            List<Effect> newEffects = new List<Effect>();
            foreach (Type type in effectsCollection.Effects)
            {
                try
                {
                    ConstructorInfo ci = type.GetConstructor(Type.EmptyTypes);
                    Effect effect = (Effect)ci.Invoke(null);

                    if (FilterEffects(effect))
                    {
                        newEffects.Add(effect);
                    }
                }

                catch
                {
                    // We don't want a DLL that can't be figured out to cause the app to crash
                    continue;
                }
            }

            newEffects.Sort(
                delegate(Effect lhs, Effect rhs)
                {
                    return string.Compare(lhs.Name, rhs.Name, true);
                });

            List<string> subMenuNames = new List<string>();

            foreach (Effect effect in newEffects)
            {
                if (!string.IsNullOrEmpty(effect.SubMenuName))
                {
                    subMenuNames.Add(effect.SubMenuName);
                }
            }

            subMenuNames.Sort(
                delegate(string lhs, string rhs)
                {
                    return string.Compare(lhs, rhs, true);
                });

            string lastSubMenuName = null;
            foreach (string subMenuName in subMenuNames)
            {
                if (subMenuName == lastSubMenuName)
                {
                    // skip duplicate names
                    continue;
                }

                PdnMenuItem subMenu = new PdnMenuItem(subMenuName, null, null);
                DropDownItems.Add(subMenu);
                lastSubMenuName = subMenuName;
            }

            foreach (Effect effect in newEffects)
            {
                AddEffectToMenu(effect, withShortcuts);
            }
        }

        private static EffectsCollection GatherEffects()
        {
            List<Assembly> assemblies = new List<Assembly>();

            // this assembly
            assemblies.Add(Assembly.GetExecutingAssembly());

            // PaintDotNet.Effects.dll
            assemblies.Add(Assembly.GetAssembly(typeof(Effect)));

            // TARGETDIR\Effects\*.dll
            string homeDir = PdnInfo.GetApplicationDir();
            string effectsDir = Path.Combine(homeDir, InvariantStrings.EffectsSubDir);
            bool dirExists;

            try
            {
                dirExists = Directory.Exists(effectsDir);
            }

            catch
            {
                dirExists = false;
            }

            if (dirExists)
            {
                string fileSpec = "*" + InvariantStrings.DllExtension;
                string[] filePaths = Directory.GetFiles(effectsDir, fileSpec);

                foreach (string filePath in filePaths)
                {
                    Assembly pluginAssembly = null;

                    try
                    {
                        pluginAssembly = Assembly.LoadFrom(filePath);
                        assemblies.Add(pluginAssembly);
                    }

                    catch (Exception ex)
                    {
                        Tracing.Ping("Exception while loading " + filePath + ": " + ex.ToString());
                    }
                }
            }

            EffectsCollection ec = new EffectsCollection(assemblies);
            return ec;
        }

        private void RepeatEffectMenuItem_Click(object sender, EventArgs e)
        {
            DocumentWorkspace activeDW = AppWorkspace.ActiveDocumentWorkspace;

            if (activeDW != null)
            {
                using (new PushNullToolMode(activeDW))
                {
                    Surface copy = activeDW.BorrowScratchSurface(this.GetType() + ".RepeatEffectMenuItem_Click() utilizing scratch for rendering");

                    try
                    {
                        using (new WaitCursorChanger(AppWorkspace))
                        {
                            copy.CopySurface(((BitmapLayer)activeDW.ActiveLayer).Surface);
                        }

                        PdnRegion selectedRegion = activeDW.Selection.CreateRegion();

                        EffectEnvironmentParameters eep = new EffectEnvironmentParameters(
                            AppWorkspace.AppEnvironment.PrimaryColor,
                            AppWorkspace.AppEnvironment.SecondaryColor,
                            AppWorkspace.AppEnvironment.PenInfo.Width,
                            selectedRegion);

                        Effect effect = (Effect)Activator.CreateInstance(this.lastEffect.GetType());
                        effect.EnvironmentParameters = eep;

                        EffectConfigToken token;

                        if (this.lastEffectToken == null)
                        {
                            token = null;
                        }
                        else
                        {
                            token = (EffectConfigToken)this.lastEffectToken.Clone();
                        }

                        DoEffect(effect, token, selectedRegion, selectedRegion, copy);
                    }

                    finally
                    {
                        activeDW.ReturnScratchSurface(copy);
                    }
                }
            }
        }

        private void EffectMenuItem_Click(object sender, EventArgs e)
        {
            if (AppWorkspace.ActiveDocumentWorkspace == null)
            {
                return; 
            }

            PdnMenuItem pmi = (PdnMenuItem)sender;
            Type effectType = (Type)pmi.Tag;

            RunEffect(effectType);
        }

        public void RunEffect(Type effectType)
        {
            bool oldDirtyValue = AppWorkspace.ActiveDocumentWorkspace.Document.Dirty;
            bool resetDirtyValue = false;

            AppWorkspace.Update(); // make sure the window is done 'closing'
            AppWorkspace.Widgets.StatusBarProgress.ResetProgressStatusBar();
            DocumentWorkspace activeDW = AppWorkspace.ActiveDocumentWorkspace;

            PdnRegion selectedRegion;

            if (activeDW.Selection.IsEmpty)
            {
                selectedRegion = new PdnRegion(activeDW.Document.Bounds);
            }
            else
            {
                selectedRegion = activeDW.Selection.CreateRegion();
            }

            BitmapLayer layer = (BitmapLayer)activeDW.ActiveLayer;

            using (new PushNullToolMode(activeDW))
            {
                try
                {
                    Effect effect = (Effect)Activator.CreateInstance(effectType);

                    EffectEnvironmentParameters eep = new EffectEnvironmentParameters(
                        AppWorkspace.AppEnvironment.PrimaryColor,
                        AppWorkspace.AppEnvironment.SecondaryColor,
                        AppWorkspace.AppEnvironment.PenInfo.Width,
                        selectedRegion);

                    string name = effect.Name;
                    EffectConfigToken newLastToken = null;
                    effect.EnvironmentParameters = eep;

                    if (!(effect.IsConfigurable))
                    {
                        Surface copy = activeDW.BorrowScratchSurface(this.GetType() + ".RunEffect() using scratch surface for non-configurable rendering");

                        try
                        {
                            using (new WaitCursorChanger(AppWorkspace))
                            {
                                copy.CopySurface(layer.Surface);
                            }

                            DoEffect(effect, null, selectedRegion, selectedRegion, copy);
                        }

                        finally
                        {
                            activeDW.ReturnScratchSurface(copy);
                        }
                    }
                    else
                    {
                        PdnRegion previewRegion = (PdnRegion)selectedRegion.Clone();
                        previewRegion.Intersect(RectangleF.Inflate(activeDW.VisibleDocumentRectangleF, 1, 1));

                        Surface originalSurface = activeDW.BorrowScratchSurface(this.GetType() + ".RunEffect() using scratch surface for rendering during configuration");

                        try
                        {
                            using (new WaitCursorChanger(AppWorkspace))
                            {
                                originalSurface.CopySurface(layer.Surface);
                            }

                            //
                            AppWorkspace.SuspendThumbnailUpdates();
                            //

                            using (EffectConfigDialog configDialog = effect.CreateConfigDialog())
                            {
                                configDialog.Opacity = 0.9;
                                configDialog.Effect = effect;
                                configDialog.EffectSourceSurface = originalSurface;
                                configDialog.Selection = selectedRegion;

                                EventHandler eh = new EventHandler(EffectConfigTokenChangedHandler);
                                configDialog.EffectTokenChanged += eh;

                                if (this.effectTokens.ContainsKey(effectType))
                                {
                                    EffectConfigToken oldToken = (EffectConfigToken)effectTokens[effectType].Clone();
                                    configDialog.EffectToken = oldToken;
                                }

                                BackgroundEffectRenderer ber = new BackgroundEffectRenderer(
                                    effect,
                                    configDialog.EffectToken,
                                    new RenderArgs(layer.Surface),
                                    new RenderArgs(originalSurface),
                                    previewRegion,
                                    tilesPerCpu * renderingThreadCount,
                                    renderingThreadCount);

                                ber.RenderedTile += new RenderedTileEventHandler(RenderedTileHandler);
                                ber.StartingRendering += new EventHandler(StartingRenderingHandler);
                                ber.FinishedRendering += new EventHandler(FinishedRenderingHandler);
                                configDialog.Tag = ber;

                                invalidateTimer.Enabled = true;
                                DialogResult dr = Utility.ShowDialog(configDialog, AppWorkspace);
                                invalidateTimer.Enabled = false;

                                this.InvalidateTimer_Tick(invalidateTimer, EventArgs.Empty);

                                if (dr == DialogResult.OK)
                                {
                                    this.effectTokens[effectType] = (EffectConfigToken)configDialog.EffectToken.Clone();
                                }

                                using (new WaitCursorChanger(AppWorkspace))
                                {
                                    ber.Abort();
                                    ber.Join();
                                    ber.Dispose();
                                    ber = null;

                                    if (dr != DialogResult.OK)
                                    {
                                        ((BitmapLayer)activeDW.ActiveLayer).Surface.CopySurface(originalSurface);
                                        activeDW.ActiveLayer.Invalidate();
                                    }

                                    configDialog.EffectTokenChanged -= eh;
                                    configDialog.Hide();
                                    AppWorkspace.Update();
                                    previewRegion.Dispose();
                                }

                                //
                                AppWorkspace.ResumeThumbnailUpdates();
                                //

                                if (dr == DialogResult.OK)
                                {
                                    PdnRegion remainingToRender = selectedRegion.Clone();
                                    PdnRegion alreadyRendered = PdnRegion.CreateEmpty();

                                    for (int i = 0; i < this.progressRegions.Length; ++i)
                                    {
                                        if (this.progressRegions[i] == null)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            remainingToRender.Exclude(this.progressRegions[i]);
                                            alreadyRendered.Union(this.progressRegions[i]);
                                        }
                                    }

                                    activeDW.ActiveLayer.Invalidate(alreadyRendered);
                                    newLastToken = (EffectConfigToken)configDialog.EffectToken.Clone();
                                    AppWorkspace.Widgets.StatusBarProgress.ResetProgressStatusBar();
                                    DoEffect(effect, newLastToken, selectedRegion, remainingToRender, originalSurface);
                                }
                                else // if (dr == DialogResult.Cancel)
                                {
                                    using (new WaitCursorChanger(AppWorkspace))
                                    {
                                        activeDW.ActiveLayer.Invalidate();
                                        Utility.GCFullCollect();
                                    }

                                    resetDirtyValue = true;
                                    return;
                                }
                            }
                        }

                        finally
                        {
                            activeDW.ReturnScratchSurface(originalSurface);
                        }
                    }

                    // if it was from the Effects menu, save it as the "Repeat ...." item
                    if (effect.Category == EffectCategory.Effect)
                    {
                        this.lastEffect = effect;

                        if (newLastToken == null)
                        {
                            this.lastEffectToken = null;
                        }
                        else
                        {
                            this.lastEffectToken = (EffectConfigToken)newLastToken.Clone();
                        }

                        PopulateMenu(true);
                    }

                }

                finally
                {
                    selectedRegion.Dispose();
                    AppWorkspace.Widgets.StatusBarProgress.ResetProgressStatusBar();
                    AppWorkspace.Widgets.StatusBarProgress.EraseProgressStatusBar();
                    AppWorkspace.ActiveDocumentWorkspace.EnableOutlineAnimation = true;

                    for (int i = 0; i < this.progressRegions.Length; ++i)
                    {
                        if (this.progressRegions[i] != null)
                        {
                            this.progressRegions[i].Dispose();
                            this.progressRegions[i] = null;
                        }
                    }

                    if (resetDirtyValue)
                    {
                        AppWorkspace.ActiveDocumentWorkspace.Document.Dirty = oldDirtyValue;
                    }
                }
            }
        }

        private void RenderedTileHandler(object sender, RenderedTileEventArgs e)
        {
            if (this.progressRegions[e.TileNumber] == null)
            {
                this.progressRegions[e.TileNumber] = e.RenderedRegion;
            }
        }

        private void InvalidateTimer_Tick(object sender, System.EventArgs e)
        {
            if (AppWorkspace.FindForm().WindowState == FormWindowState.Minimized)
            {
                return;
            }

            lock (this.progressRegions)
            {
                int min = this.progressRegionsStartIndex;
                int max;

                for (max = min; max < progressRegions.Length; ++max)
                {
                    if (this.progressRegions[max] == null)
                    {
                        break;
                    }
                }

                if (min != max)
                {
                    using (PdnRegion updateRegion = PdnRegion.CreateEmpty())
                    {
                        for (int i = min; i < max; ++i)
                        {
                            updateRegion.Union(this.progressRegions[i]);
                        }

                        using (PdnRegion simplified = Utility.SimplifyAndInflateRegion(updateRegion))
                        {
                            AppWorkspace.ActiveDocumentWorkspace.ActiveLayer.Invalidate(simplified);
                        }

                        this.progressRegionsStartIndex = max;
                    }
                }

                double progress = 100.0 * (double)max / (double)progressRegions.Length;
                AppWorkspace.Widgets.StatusBarProgress.SetProgressStatusBar(progress);
            }
        }

        private void EffectConfigTokenChangedHandler(object sender, System.EventArgs e)
        {
            EffectConfigDialog ecf = (EffectConfigDialog)sender;
            BackgroundEffectRenderer ber = (BackgroundEffectRenderer)ecf.Tag;

            if (ber != null)
            {
                AppWorkspace.Widgets.StatusBarProgress.ResetProgressStatusBarAsync();
                ber.Start();
            }
        }

        private void FinishedRenderingHandler(object sender, EventArgs e)
        {
            if (AppWorkspace.InvokeRequired)
            {
                AppWorkspace.BeginInvoke(new EventHandler(FinishedRenderingHandler), new object[] { sender, e });
            }
            else
            {
                AppWorkspace.ActiveDocumentWorkspace.EnableOutlineAnimation = true;
            }
        }

        private void StartingRenderingHandler(object sender, EventArgs e)
        {
            AppWorkspace.Widgets.StatusBarProgress.ResetProgressStatusBarAsync();
            AppWorkspace.ActiveDocumentWorkspace.EnableOutlineAnimation = false;

            if (this.progressRegions == null)
            {
                this.progressRegions = new PdnRegion[tilesPerCpu * renderingThreadCount];
            }

            lock (this.progressRegions)
            {
                for (int i = 0; i < progressRegions.Length; ++i)
                {
                    progressRegions[i] = null;
                }

                this.progressRegionsStartIndex = 0;
            }
        }

        private bool DoEffect(Effect effect, EffectConfigToken token, PdnRegion selectedRegion,
            PdnRegion regionToRender, Surface originalSurface)
        {
            bool oldDirtyValue = AppWorkspace.ActiveDocumentWorkspace.Document.Dirty;
            bool resetDirtyValue = false;

            bool returnVal = false;
            AppWorkspace.ActiveDocumentWorkspace.EnableOutlineAnimation = false;

            try
            {
                using (ProgressDialog aed = new ProgressDialog())
                {
                    if (effect.Image != null)
                    {
                        aed.Icon = Utility.ImageToIcon(effect.Image, Utility.TransparentKey);
                    }

                    aed.Opacity = 0.9;
                    aed.Value = 0;
                    aed.Text = effect.Name;
                    aed.Description = string.Format(PdnResources.GetString("Effects.ApplyingDialog.Description"), effect.Name);

                    invalidateTimer.Enabled = true;

                    using (new WaitCursorChanger(AppWorkspace))
                    {
                        HistoryMemento ha = null;
                        DialogResult result = DialogResult.None;

                        AppWorkspace.Widgets.StatusBarProgress.ResetProgressStatusBar();
                        AppWorkspace.Widgets.LayerControl.SuspendLayerPreviewUpdates();

                        try
                        {
                            ManualResetEvent saveEvent = new ManualResetEvent(false);
                            BitmapHistoryMemento bha = null;

                            // perf bug #1445: save this data in a background thread
                            PdnRegion selectedRegionCopy = selectedRegion.Clone();
                            PaintDotNet.Threading.ThreadPool.Global.QueueUserWorkItem(
                                delegate(object context)
                                {
                                    try
                                    {
                                        ImageResource image;

                                        if (effect.Image == null)
                                        {
                                            image = null;
                                        }
                                        else
                                        {
                                            image = ImageResource.FromImage(effect.Image);
                                        }

                                        bha = new BitmapHistoryMemento(effect.Name, image, this.AppWorkspace.ActiveDocumentWorkspace,
                                            this.AppWorkspace.ActiveDocumentWorkspace.ActiveLayerIndex, selectedRegionCopy, originalSurface);
                                    }

                                    finally
                                    {
                                        saveEvent.Set();
                                        selectedRegionCopy.Dispose();
                                        selectedRegionCopy = null;
                                    }
                                });

                            BackgroundEffectRenderer ber = new BackgroundEffectRenderer(
                                effect,
                                token,
                                new RenderArgs(((BitmapLayer)AppWorkspace.ActiveDocumentWorkspace.ActiveLayer).Surface),
                                new RenderArgs(originalSurface),
                                regionToRender,
                                tilesPerCpu * renderingThreadCount,
                                renderingThreadCount);

                            aed.Tag = ber;
                            ber.RenderedTile += new RenderedTileEventHandler(aed.RenderedTileHandler);
                            ber.RenderedTile += new RenderedTileEventHandler(RenderedTileHandler);
                            ber.StartingRendering += new EventHandler(StartingRenderingHandler);
                            ber.FinishedRendering += new EventHandler(aed.FinishedRenderingHandler);
                            ber.FinishedRendering += new EventHandler(FinishedRenderingHandler);
                            ber.Start();

                            result = Utility.ShowDialog(aed, AppWorkspace);

                            if (result == DialogResult.Cancel)
                            {
                                resetDirtyValue = true;

                                using (new WaitCursorChanger(AppWorkspace))
                                {
                                    ber.Abort();
                                    ber.Join();
                                    ((BitmapLayer)AppWorkspace.ActiveDocumentWorkspace.ActiveLayer).Surface.CopySurface(originalSurface);
                                }
                            }

                            invalidateTimer.Enabled = false;

                            ber.Join();
                            ber.Dispose();

                            saveEvent.WaitOne();
                            saveEvent.Close();
                            saveEvent = null;

                            ha = bha;
                        }

                        catch
                        {
                            using (new WaitCursorChanger(AppWorkspace))
                            {
                                ((BitmapLayer)AppWorkspace.ActiveDocumentWorkspace.ActiveLayer).Surface.CopySurface(originalSurface);
                                ha = null;
                            }
                        }

                        finally
                        {
                            AppWorkspace.Widgets.LayerControl.ResumeLayerPreviewUpdates();
                        }

                        using (PdnRegion simplifiedRenderRegion = Utility.SimplifyAndInflateRegion(selectedRegion))
                        {
                            using (new WaitCursorChanger(AppWorkspace))
                            {
                                AppWorkspace.ActiveDocumentWorkspace.ActiveLayer.Invalidate(simplifiedRenderRegion);
                            }
                        }

                        using (new WaitCursorChanger(AppWorkspace))
                        {
                            if (result == DialogResult.OK)
                            {
                                if (ha != null)
                                {
                                    AppWorkspace.ActiveDocumentWorkspace.History.PushNewMemento(ha);
                                }

                                AppWorkspace.Update();
                                returnVal = true;
                            }
                            else
                            {
                                Utility.GCFullCollect();
                            }
                        }
                    } // using
                } // using
            }

            finally
            {
                AppWorkspace.ActiveDocumentWorkspace.EnableOutlineAnimation = true;

                if (resetDirtyValue)
                {
                    AppWorkspace.ActiveDocumentWorkspace.Document.Dirty = oldDirtyValue;
                }
            }

            AppWorkspace.Widgets.StatusBarProgress.EraseProgressStatusBarAsync();
            return returnVal;
        }
    }
}
