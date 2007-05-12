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
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PaintDotNet
{
    // TODO: after 3.0, refactor into smaller ToolConfigStrip "Sections"
    public class ToolConfigStrip
        : ToolStripEx,
          IBrushConfig,
          IShapeTypeConfig, 
          IPenConfig, 
          IAntiAliasingConfig, 
          IAlphaBlendingConfig,
          ITextConfig,
          IToleranceConfig,
          IColorPickerConfig,
          IGradientConfig,          
          IResamplingConfig
    {
        private ToolBarConfigItems toolBarConfigItems = ToolBarConfigItems.None;

        private const float minPenSize = 1.0f;
        private const float maxPenSize = 100.0f;

        private EnumLocalizer hatchStyleNames = EnumLocalizer.Create(typeof(HatchStyle));
        private string solidBrushText;
        private ImageResource shapeOutlineImage = ImageResource.Get("Icons.ShapeOutlineIcon.png");
        private ImageResource shapeInteriorImage = ImageResource.Get("Icons.ShapeInteriorIcon.png");
        private ImageResource shapeBothImage = ImageResource.Get("Icons.ShapeBothIcon.png");

        private ToolStripSeparator brushSeparator;
        private ToolStripLabel brushStyleLabel;
        private ToolStripComboBox brushStyleComboBox;

        private ToolStripSeparator shapeSeparator;
        private ToolStripSplitButton shapeButton;

        private ToolStripSeparator penSeparator;
        private ToolStripLabel penSizeLabel;
        private ToolStripButton penSizeDecButton;
        private ToolStripComboBox penSizeComboBox;
        private ToolStripButton penSizeIncButton;

        private ToolStripSeparator blendingSeparator;
        private ToolStripSplitButton alphaBlendingSplitButton;
        private bool alphaBlendingEnabled = true;
        private ImageResource alphaBlendingEnabledImage;
        private ImageResource alphaBlendingOverwriteImage;

        private ToolStripSplitButton antiAliasingSplitButton;
        private bool antiAliasingEnabled = true;
        private ImageResource antiAliasingEnabledImage;
        private ImageResource antiAliasingDisabledImage;

        private EnumLocalizer resamplingAlgorithmNames = EnumLocalizer.Create(typeof(ResamplingAlgorithm));
        private ToolStripSeparator resamplingSeparator;
        private ToolStripLabel resamplingLabel;
        private ToolStripComboBox resamplingComboBox;

        private EnumLocalizer colorPickerBehaviorNames = EnumLocalizer.Create(typeof(ColorPickerClickBehavior));
        private ToolStripSeparator colorPickerSeparator;
        private ToolStripLabel colorPickerLabel;
        private ToolStripComboBox colorPickerComboBox;

        private ToolStripSeparator toleranceSeparator;
        private ToolStripLabel toleranceLabel;
        private ToolStripControlHost toleranceSliderStrip;
        private ToleranceSliderControl toleranceSlider;

        private const int minBrushSize = 1;
        private const int maxBrushSize = 100;
        private int[] brushSizes = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100 };
        private ShapeDrawType shapeDrawType;

        private EnumLocalizer gradientTypeNames = EnumLocalizer.Create(typeof(GradientType));
        private GradientInfo gradientInfo = new GradientInfo(GradientType.LinearClamped, false);
        private ToolStripSeparator gradientSeparator1;
        private ToolStripButton gradientLinearClampedButton;
        private ToolStripButton gradientLinearReflectedButton;
        private ToolStripButton gradientLinearDiamondButton;
        private ToolStripButton gradientRadialButton;
        private ToolStripButton gradientConicalButton;
        private ToolStripSeparator gradientSeparator2;
        private ToolStripSplitButton gradientChannelsSplitButton;
        private ImageResource gradientAllColorChannelsImage;
        private ImageResource gradientAlphaChannelOnlyImage;

        private EnumLocalizer fontSmoothingLocalizer = EnumLocalizer.Create(typeof(FontSmoothing));
        private const int maxFontSize = 2000;
        private const int minFontSize = 1;
        private const int initialFontSize = 12;

        private FontFamily arialFontFamily;
        private FontStyle fontStyle;
        private TextAlignment alignment;
        private float oldSizeValue;
        private Brush highlightBrush;
        private Brush highlightTextBrush;
        private Brush windowBrush;
        private Brush windowTextBrush;
        private Font arialFontBase;
        private const string arialName = "Arial";

        private static ManualResetEvent staticFontNamesPopulatedEvent = new ManualResetEvent(false);
        private static List<string> staticFontNames = null;
        private bool fontsComboBoxPopulated = false;

        private ToolStripSeparator fontSeparator;
        private ToolStripLabel fontLabel;
        private ToolStripComboBox fontFamilyComboBox;
        private ToolStripComboBox fontSizeComboBox;
        private ToolStripComboBox fontSmoothingComboBox;
        private ToolStripSeparator fontStyleSeparator;
        private ToolStripButton fontBoldButton;
        private ToolStripButton fontItalicsButton;
        private ToolStripButton fontUnderlineButton;
        private ToolStripSeparator fontAlignSeparator;
        private ToolStripButton fontAlignLeftButton;
        private ToolStripButton fontAlignCenterButton;
        private ToolStripButton fontAlignRightButton;

        private int[] defaultFontSizes =
            new int[] 
            { 
                8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 
            };

        public void LoadFromAppEnvironment(AppEnvironment appEnvironment)
        {
            AlphaBlending = appEnvironment.AlphaBlending;
            AntiAliasing = appEnvironment.AntiAliasing;
            BrushInfo = appEnvironment.BrushInfo;
            ColorPickerClickBehavior = appEnvironment.ColorPickerClickBehavior;
            GradientInfo = appEnvironment.GradientInfo;
            PenInfo = appEnvironment.PenInfo;
            ResamplingAlgorithm = appEnvironment.ResamplingAlgorithm;
            ShapeDrawType = appEnvironment.ShapeDrawType;
            FontInfo = appEnvironment.FontInfo;
            FontSmoothing = appEnvironment.FontSmoothing;
            FontAlignment = appEnvironment.TextAlignment;
            Tolerance = appEnvironment.Tolerance;
        }

        public event EventHandler BrushInfoChanged;
        protected virtual void OnBrushChanged()
        {
            if (BrushInfoChanged != null)
            {
                BrushInfoChanged(this, EventArgs.Empty);
            }
        }

        public BrushInfo BrushInfo
        {
            get
            {
                if (this.brushStyleComboBox.SelectedItem.ToString() == this.solidBrushText)
                {
                    return new BrushInfo(BrushType.Solid, HatchStyle.BackwardDiagonal);
                }

                if (this.brushStyleComboBox.SelectedIndex == -1)
                {
                    return new BrushInfo(BrushType.Solid, HatchStyle.BackwardDiagonal);
                }
                else
                {
                    return new BrushInfo(
                        BrushType.Hatch, 
                        (HatchStyle)this.hatchStyleNames.LocalizedNameToEnumValue(this.brushStyleComboBox.SelectedItem.ToString()));
                }
            }

            set
            {
                if (value.BrushType == BrushType.Solid)
                {
                    this.brushStyleComboBox.SelectedItem = this.solidBrushText;
                }
                else
                {
                    this.brushStyleComboBox.SelectedItem = this.hatchStyleNames.EnumValueToLocalizedName(value.HatchStyle);
                }
            }
        }

        public event EventHandler GradientInfoChanged;

        protected virtual void OnGradientInfoChanged()
        {
            if (GradientInfoChanged != null)
            {
                GradientInfoChanged(this, EventArgs.Empty);
            }
        }

        public void PerformGradientInfoChanged()
        {
            OnGradientInfoChanged();
        }

        public GradientInfo GradientInfo
        {
            get
            {
                return this.gradientInfo;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                this.gradientInfo = value;
                OnGradientInfoChanged();
                SyncGradientInfo();
            }
        }

        private void SyncGradientInfo()
        {
            this.gradientConicalButton.Checked = false;
            this.gradientRadialButton.Checked = false;
            this.gradientLinearClampedButton.Checked = false;
            this.gradientLinearReflectedButton.Checked = false;
            this.gradientLinearDiamondButton.Checked = false;

            switch (this.gradientInfo.GradientType)
            {
                case GradientType.Conical:
                    this.gradientConicalButton.Checked = true;
                    break;

                case GradientType.LinearClamped:
                    this.gradientLinearClampedButton.Checked = true;
                    break;

                case GradientType.LinearReflected:
                    this.gradientLinearReflectedButton.Checked = true;
                    break;

                case GradientType.LinearDiamond:
                    this.gradientLinearDiamondButton.Checked = true;
                    break;

                case GradientType.Radial:
                    this.gradientRadialButton.Checked = true;
                    break;

                default:
                    throw new InvalidEnumArgumentException();
            }

            if (this.gradientInfo.AlphaOnly)
            {
                this.gradientChannelsSplitButton.Image = this.gradientAlphaChannelOnlyImage.Reference;
            }
            else
            {
                this.gradientChannelsSplitButton.Image = this.gradientAllColorChannelsImage.Reference;
            }
        }

        private void ShapeButton_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem outlineMI = new ToolStripMenuItem();
            outlineMI.Text = PdnResources.GetString("ShapeDrawTypeConfigWidget.OutlineButton.ToolTipText");
            outlineMI.Image = this.shapeOutlineImage.Reference;
            outlineMI.Tag = (object)ShapeDrawType.Outline;
            outlineMI.Click += new EventHandler(ShapeMI_Click);

            ToolStripMenuItem interiorMI = new ToolStripMenuItem();
            interiorMI.Text = PdnResources.GetString("ShapeDrawTypeConfigWidget.InteriorButton.ToolTipText");
            interiorMI.Image = this.shapeInteriorImage.Reference;
            interiorMI.Tag = (object)ShapeDrawType.Interior;
            interiorMI.Click += new EventHandler(ShapeMI_Click);

            ToolStripMenuItem bothMI = new ToolStripMenuItem();
            bothMI.Text = PdnResources.GetString("ShapeDrawTypeConfigWidget.BothButton.ToolTipText");
            bothMI.Image = this.shapeBothImage.Reference;
            bothMI.Tag = (object)ShapeDrawType.Both;
            bothMI.Click += new EventHandler(ShapeMI_Click);

            switch (this.shapeDrawType)
            {
                case ShapeDrawType.Outline:
                    outlineMI.Checked = true;
                    break;

                case ShapeDrawType.Interior:
                    interiorMI.Checked = true;
                    break;

                case ShapeDrawType.Both:
                    bothMI.Checked = true;
                    break;

                default:
                    throw new InvalidEnumArgumentException();
            }

            this.shapeButton.DropDownItems.AddRange(
                new ToolStripItem[]
                {
                    outlineMI,
                    interiorMI,
                    bothMI
                });
        }

        private void ShapeMI_Click(object sender, EventArgs e)
        {
            this.ShapeDrawType = (ShapeDrawType)((ToolStripMenuItem)sender).Tag;
        }

        public ToolConfigStrip()
        {
            SuspendLayout();
            InitializeComponent();

            this.solidBrushText = PdnResources.GetString("BrushConfigWidget.SolidBrush.Text"); // "Solid Brush"
            this.brushStyleComboBox.Items.Add(this.solidBrushText);
            string[] styleNames = this.hatchStyleNames.GetLocalizedNames();
            Array.Sort(styleNames);

            foreach (string styleName in styleNames)
            {
                brushStyleComboBox.Items.Add(styleName);
            }

            brushStyleComboBox.SelectedIndex = 0;

            this.brushStyleLabel.Text = PdnResources.GetString("BrushConfigWidget.FillStyleLabel.Text");

            this.shapeDrawType = ShapeDrawType.Outline;
            this.shapeButton.Image = this.shapeOutlineImage.Reference;

            this.penSizeLabel.Text = PdnResources.GetString("PenConfigWidget.BrushWidthLabel");

            this.penSizeComboBox.ComboBox.SuspendLayout();

            for (int i = 0; i < this.brushSizes.Length; ++i)
            {
                this.penSizeComboBox.Items.Add(this.brushSizes[i].ToString());
            }

            this.penSizeComboBox.ComboBox.ResumeLayout(false);
            this.penSizeComboBox.SelectedIndex = 1; // default to brush size of 2

            this.penSizeDecButton.ToolTipText = PdnResources.GetString("ToolConfigStrip.PenSizeDecButton.ToolTipText");
            this.penSizeDecButton.Image = ImageResource.Get("Icons.MinusButtonIcon.png").Reference;
            this.penSizeIncButton.ToolTipText = PdnResources.GetString("ToolConfigStrip.PenSizeIncButton.ToolTipText");
            this.penSizeIncButton.Image = ImageResource.Get("Icons.PlusButtonIcon.png").Reference;

            this.gradientLinearClampedButton.ToolTipText = this.gradientTypeNames.EnumValueToLocalizedName(GradientType.LinearClamped);
            this.gradientLinearClampedButton.Image = ImageResource.Get("Icons.LinearClampedGradientIcon.png").Reference;
            this.gradientLinearReflectedButton.ToolTipText = this.gradientTypeNames.EnumValueToLocalizedName(GradientType.LinearReflected);
            this.gradientLinearReflectedButton.Image = ImageResource.Get("Icons.LinearReflectedGradientIcon.png").Reference;
            this.gradientLinearDiamondButton.ToolTipText = this.gradientTypeNames.EnumValueToLocalizedName(GradientType.LinearDiamond);
            this.gradientLinearDiamondButton.Image = ImageResource.Get("Icons.LinearDiamondGradientIcon.png").Reference;
            this.gradientRadialButton.ToolTipText = this.gradientTypeNames.EnumValueToLocalizedName(GradientType.Radial);
            this.gradientRadialButton.Image = ImageResource.Get("Icons.RadialGradientIcon.png").Reference;
            this.gradientConicalButton.ToolTipText = this.gradientTypeNames.EnumValueToLocalizedName(GradientType.Conical);
            this.gradientConicalButton.Image = ImageResource.Get("Icons.ConicalGradientIcon.png").Reference;

            this.gradientAllColorChannelsImage = ImageResource.Get("Icons.AllColorChannelsIcon.png");
            this.gradientAlphaChannelOnlyImage = ImageResource.Get("Icons.AlphaChannelOnlyIcon.png");
            this.gradientChannelsSplitButton.Image = this.gradientAllColorChannelsImage.Reference;

            this.antiAliasingEnabledImage = ImageResource.Get("Icons.AntiAliasingEnabledIcon.png");
            this.antiAliasingDisabledImage = ImageResource.Get("Icons.AntiAliasingDisabledIcon.png");
            this.antiAliasingSplitButton.Image = this.antiAliasingEnabledImage.Reference;

            this.alphaBlendingEnabledImage = ImageResource.Get("Icons.BlendingEnabledIcon.png");
            this.alphaBlendingOverwriteImage = ImageResource.Get("Icons.BlendingOverwriteIcon.png");
            this.alphaBlendingSplitButton.Image = this.alphaBlendingEnabledImage.Reference;

            this.penSizeComboBox.Size = new Size(UI.ScaleWidth(this.penSizeComboBox.Width), penSizeComboBox.Height);
            this.brushStyleComboBox.Size = new Size(UI.ScaleWidth(this.brushStyleComboBox.Width), brushStyleComboBox.Height);
            this.brushStyleComboBox.DropDownWidth = UI.ScaleWidth(this.brushStyleComboBox.DropDownWidth);
            this.brushStyleComboBox.DropDownHeight = UI.ScaleHeight(this.brushStyleComboBox.DropDownHeight);

            this.toleranceLabel.Text = PdnResources.GetString("ToleranceConfig.ToleranceLabel.Text");
            this.toleranceSlider.Tolerance = 0.5f;

            this.fontSizeComboBox.ComboBox.SuspendLayout();
            for (int i = 0; i < this.defaultFontSizes.Length; ++i)
            {
                this.fontSizeComboBox.Items.Add(this.defaultFontSizes[i].ToString());
            }
            this.fontSizeComboBox.ComboBox.ResumeLayout(false);

            this.fontSmoothingComboBox.Items.AddRange(
                new object[]
                {
                    this.fontSmoothingLocalizer.EnumValueToLocalizedName(FontSmoothing.Smooth),
                    this.fontSmoothingLocalizer.EnumValueToLocalizedName(FontSmoothing.Sharp)
                });

            this.fontSmoothingComboBox.SelectedIndex = 0;

            this.fontLabel.Text = PdnResources.GetString("TextConfigWidget.FontLabel.Text");

            try
            {
                this.arialFontFamily = new FontFamily(arialName);
            }

            catch (Exception)
            {
                this.arialFontFamily = new FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif);
            }

            try
            {
                this.arialFontBase = new Font(arialFontFamily, initialFontSize, FontStyle.Regular);
            }

            catch (Exception)
            {
                this.arialFontBase = new Font(FontFamily.GenericSansSerif, initialFontSize, FontStyle.Regular);
            }

            this.fontFamilyComboBox.ComboBox.DropDownHeight = 600;

            this.alignment = TextAlignment.Left;
            this.fontAlignLeftButton.Checked = true;
            this.oldSizeValue = initialFontSize;

            this.highlightBrush = new SolidBrush(SystemColors.Highlight);
            this.highlightTextBrush = new SolidBrush(SystemColors.HighlightText);
            this.windowBrush = new SolidBrush(SystemColors.Window);
            this.windowTextBrush = new SolidBrush(SystemColors.WindowText);

            // These buttons need a color key to maintain consistency with v2.5 language packs
            this.fontBoldButton.ImageTransparentColor = Utility.TransparentKey;
            this.fontItalicsButton.ImageTransparentColor = Utility.TransparentKey;
            this.fontUnderlineButton.ImageTransparentColor = Utility.TransparentKey;

            this.fontBoldButton.Image = PdnResources.GetImageBmpOrPng("Icons.FontBoldIcon");
            this.fontItalicsButton.Image = PdnResources.GetImageBmpOrPng("Icons.FontItalicIcon");
            this.fontUnderlineButton.Image = PdnResources.GetImageBmpOrPng("Icons.FontUnderlineIcon");

            this.fontAlignLeftButton.Image = PdnResources.GetImage("Icons.TextAlignLeftIcon.png");
            this.fontAlignCenterButton.Image = PdnResources.GetImage("Icons.TextAlignCenterIcon.png");
            this.fontAlignRightButton.Image = PdnResources.GetImage("Icons.TextAlignRightIcon.png");

            this.fontBoldButton.ToolTipText = PdnResources.GetString("TextConfigWidget.BoldButton.ToolTipText");
            this.fontItalicsButton.ToolTipText = PdnResources.GetString("TextConfigWidget.ItalicButton.ToolTipText");
            this.fontUnderlineButton.ToolTipText = PdnResources.GetString("TextConfigWidget.UnderlineButton.ToolTipText");
            this.fontAlignLeftButton.ToolTipText = PdnResources.GetString("TextConfigWidget.AlignLeftButton.ToolTipText");
            this.fontAlignCenterButton.ToolTipText = PdnResources.GetString("TextConfigWidget.AlignCenterButton.ToolTipText");
            this.fontAlignRightButton.ToolTipText = PdnResources.GetString("TextConfigWidget.AlignRightButton.ToolTipText");

            this.fontFamilyComboBox.Size = new Size(UI.ScaleWidth(this.fontFamilyComboBox.Width), fontFamilyComboBox.Height);
            this.fontFamilyComboBox.DropDownWidth = UI.ScaleWidth(this.fontFamilyComboBox.DropDownWidth);
            this.fontSizeComboBox.Size = new Size(UI.ScaleWidth(this.fontSizeComboBox.Width), fontSizeComboBox.Height);

            this.fontSmoothingComboBox.Size = new Size(UI.ScaleWidth(this.fontSmoothingComboBox.Width), fontSmoothingComboBox.Height);
            this.fontSmoothingComboBox.DropDownWidth = UI.ScaleWidth(this.fontSmoothingComboBox.DropDownWidth);

            this.resamplingLabel.Text = PdnResources.GetString("ToolConfigStrip.ResamplingLabel.Text");
            this.resamplingComboBox.BeginUpdate();
            this.resamplingComboBox.Items.Add(this.resamplingAlgorithmNames.EnumValueToLocalizedName(ResamplingAlgorithm.Bilinear));
            this.resamplingComboBox.Items.Add(this.resamplingAlgorithmNames.EnumValueToLocalizedName(ResamplingAlgorithm.NearestNeighbor));
            this.resamplingComboBox.EndUpdate();
            this.resamplingComboBox.SelectedIndex = 0; // bilinear

            this.resamplingComboBox.Size = new Size(UI.ScaleWidth(this.resamplingComboBox.Width), resamplingComboBox.Height);
            this.resamplingComboBox.DropDownWidth = UI.ScaleWidth(this.resamplingComboBox.DropDownWidth);

            this.colorPickerLabel.Text = PdnResources.GetString("ToolConfigStrip.ColorPickerLabel.Text");
            string[] colorPickerBehaviorNames = this.colorPickerBehaviorNames.GetLocalizedNames();

            // Make sure these items are sorted to be in the order specified by the enumeration
            Array.Sort<string>(
                colorPickerBehaviorNames,
                delegate(string lhs, string rhs)
                {
                    ColorPickerClickBehavior lhsE = (ColorPickerClickBehavior)this.colorPickerBehaviorNames.LocalizedNameToEnumValue(lhs);
                    ColorPickerClickBehavior rhsE = (ColorPickerClickBehavior)this.colorPickerBehaviorNames.LocalizedNameToEnumValue(rhs);

                    if ((int)lhsE < (int)rhsE)
                    {
                        return -1;
                    }
                    else if ((int)lhsE > (int)rhsE)
                    {
                        return +1;
                    }
                    else
                    {
                        return 0;
                    }
                });

            this.colorPickerComboBox.Items.AddRange(colorPickerBehaviorNames);
            this.colorPickerComboBox.SelectedIndex = 0;

            this.colorPickerComboBox.Size = new Size(UI.ScaleWidth(this.colorPickerComboBox.Width), colorPickerComboBox.Height);
            this.colorPickerComboBox.DropDownWidth = UI.ScaleWidth(this.colorPickerComboBox.DropDownWidth);

            this.toleranceSlider.Size = UI.ScaleSize(this.toleranceSlider.Size);

            ToolBarConfigItems = ToolBarConfigItems.None;
            ResumeLayout(false);
        }

        private void AsyncInitFontNames()
        {
            if (!IsHandleCreated)
            {
                CreateControl();
            }

            if (!this.fontFamilyComboBox.ComboBox.IsHandleCreated)
            {
                this.fontFamilyComboBox.ComboBox.CreateControl();
            }

            if (staticFontNames == null)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.PopulateFontsBackgroundThread), null);
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if ((this.toolBarConfigItems & ToolBarConfigItems.Text) == ToolBarConfigItems.Text)
            {
                AsyncInitFontNames();
            }

            base.OnHandleCreated(e);
        }

        private void InitializeComponent()
        {
            this.brushSeparator = new ToolStripSeparator();
            this.brushStyleLabel = new ToolStripLabel();
            this.brushStyleComboBox = new ToolStripComboBox();

            this.shapeSeparator = new ToolStripSeparator();
            this.shapeButton = new ToolStripSplitButton();

            this.gradientSeparator1 = new ToolStripSeparator();
            this.gradientLinearClampedButton = new ToolStripButton();
            this.gradientLinearReflectedButton = new ToolStripButton();
            this.gradientLinearDiamondButton = new ToolStripButton();
            this.gradientRadialButton = new ToolStripButton();
            this.gradientConicalButton = new ToolStripButton();
            this.gradientSeparator2 = new ToolStripSeparator();
            this.gradientChannelsSplitButton = new ToolStripSplitButton();

            this.penSeparator = new ToolStripSeparator();
            this.penSizeLabel = new ToolStripLabel();
            this.penSizeDecButton = new ToolStripButton();
            this.penSizeComboBox = new ToolStripComboBox();
            this.penSizeIncButton = new ToolStripButton();

            this.blendingSeparator = new ToolStripSeparator();
            this.antiAliasingSplitButton = new ToolStripSplitButton();
            this.alphaBlendingSplitButton = new ToolStripSplitButton();

            this.toleranceSeparator = new ToolStripSeparator();
            this.toleranceLabel = new ToolStripLabel();
            this.toleranceSlider = new ToleranceSliderControl();
            this.toleranceSliderStrip = new ToolStripControlHost(this.toleranceSlider);

            this.fontSeparator = new ToolStripSeparator();
            this.fontLabel = new ToolStripLabel();
            this.fontFamilyComboBox = new ToolStripComboBox();
            this.fontSizeComboBox = new ToolStripComboBox();
            this.fontSmoothingComboBox = new ToolStripComboBox();
            this.fontStyleSeparator = new ToolStripSeparator();
            this.fontBoldButton = new ToolStripButton();
            this.fontItalicsButton = new ToolStripButton();
            this.fontUnderlineButton = new ToolStripButton();
            this.fontAlignSeparator = new ToolStripSeparator();
            this.fontAlignLeftButton = new ToolStripButton();
            this.fontAlignCenterButton = new ToolStripButton();
            this.fontAlignRightButton = new ToolStripButton();

            this.resamplingSeparator = new ToolStripSeparator();
            this.resamplingLabel = new ToolStripLabel();
            this.resamplingComboBox = new ToolStripComboBox();

            this.colorPickerSeparator = new ToolStripSeparator();
            this.colorPickerLabel = new ToolStripLabel();
            this.colorPickerComboBox = new ToolStripComboBox();

            this.SuspendLayout();
            //
            // brushStyleLabel
            //
            this.brushStyleLabel.Name = "fillStyleLabel";
            //
            // brushStyleComboBox
            //
            this.brushStyleComboBox.Name = "styleComboBox";
            this.brushStyleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.brushStyleComboBox.DropDownWidth = 234;
            this.brushStyleComboBox.AutoSize = true;
            //
            // brushStyleComboBox.ComboBox
            //
            this.brushStyleComboBox.ComboBox.DrawMode = DrawMode.OwnerDrawVariable;
            this.brushStyleComboBox.ComboBox.MeasureItem += ComboBoxStyle_MeasureItem;
            this.brushStyleComboBox.ComboBox.SelectedValueChanged += ComboBoxStyle_SelectedValueChanged;
            this.brushStyleComboBox.ComboBox.DrawItem += ComboBoxStyle_DrawItem;
            //
            // shapeButton
            //
            this.shapeButton.Name = "shapeButton";
            this.shapeButton.DropDownOpening += new EventHandler(ShapeButton_DropDownOpening);
            this.shapeButton.DropDownClosed += 
                delegate(object sender, EventArgs e)
                {
                    this.shapeButton.DropDownItems.Clear();
                };
            this.shapeButton.ButtonClick +=
                delegate(object sender, EventArgs e)
                {
                    switch (ShapeDrawType)
                    {
                        case ShapeDrawType.Outline:
                            ShapeDrawType = ShapeDrawType.Interior;
                            break;

                        case ShapeDrawType.Interior:
                            ShapeDrawType = ShapeDrawType.Both;
                            break;

                        case ShapeDrawType.Both:
                            ShapeDrawType = ShapeDrawType.Outline;
                            break;

                        default:
                            throw new InvalidEnumArgumentException();
                    }
                };
            //
            // gradientSeparator
            //
            this.gradientSeparator1.Name = "gradientSeparator";
            //
            // gradientLinearClampedButton
            //
            this.gradientLinearClampedButton.Name = "gradientLinearClampedButton";
            this.gradientLinearClampedButton.Click += GradientTypeButtonClicked;
            this.gradientLinearClampedButton.Tag = GradientType.LinearClamped;
            //
            // gradientLinearReflectedButton
            //
            this.gradientLinearReflectedButton.Name = "gradientLinearReflectedButton";
            this.gradientLinearReflectedButton.Click += GradientTypeButtonClicked;
            this.gradientLinearReflectedButton.Tag = GradientType.LinearReflected;
            //
            // gradientLinearDiamondButton
            //
            this.gradientLinearDiamondButton.Name = "gradientLinearDiamondButton";
            this.gradientLinearDiamondButton.Click += GradientTypeButtonClicked;
            this.gradientLinearDiamondButton.Tag = GradientType.LinearDiamond;
            //
            // gradientRadialButton
            //
            this.gradientRadialButton.Name = "gradientRadialButton";
            this.gradientRadialButton.Click += GradientTypeButtonClicked;
            this.gradientRadialButton.Tag = GradientType.Radial;
            //
            // gradientConicalButton
            //
            this.gradientConicalButton.Name = "gradientConicalButton";
            this.gradientConicalButton.Click += GradientTypeButtonClicked;
            this.gradientConicalButton.Tag = GradientType.Conical;
            //
            // gradientSeparator2
            //
            this.gradientSeparator2.Name = "gradientSeparator2";
            //
            // gradientChannelsSplitButton
            //
            this.gradientChannelsSplitButton.Name = "gradientChannelsSplitButton";
            this.gradientChannelsSplitButton.DropDownOpening += new EventHandler(GradientChannelsSplitButton_DropDownOpening);
            this.gradientChannelsSplitButton.DropDownClosed +=
                delegate(object sender, EventArgs e)
                {
                    this.gradientChannelsSplitButton.DropDownItems.Clear();
                };
            this.gradientChannelsSplitButton.ButtonClick +=
                delegate(object sender, EventArgs e)
                {
                    GradientInfo = new GradientInfo(GradientInfo.GradientType, !GradientInfo.AlphaOnly);
                };
            //
            // penSeparator
            //
            this.penSeparator.Name = "penSeparator";
            //
            // penSizeLabel
            //
            this.penSizeLabel.Name = "brushSizeLabel";
            //
            // penSizeDecButton
            //
            this.penSizeDecButton.Name = "penSizeDecButton";
            this.penSizeDecButton.Click +=
                delegate(object sender, EventArgs e)
                {
                    AddToPenSize(-1.0f);
                };
            //
            // penSizeComboBox
            //
            this.penSizeComboBox.Name = "penSizeComboBox";
            this.penSizeComboBox.Validating += new CancelEventHandler(this.BrushSizeComboBox_Validating);
            this.penSizeComboBox.TextChanged += new EventHandler(this.SizeComboBox_TextChanged);
            this.penSizeComboBox.AutoSize = false;
            this.penSizeComboBox.Width = 44;
            //
            // penSizeIncButton
            //
            this.penSizeIncButton.Name = "penSizeIncButton";
            this.penSizeIncButton.Click +=
                delegate(object sender, EventArgs e)
                {
                    AddToPenSize(1.0f);
                };
            //
            // antiAliasingSplitButton
            //
            this.antiAliasingSplitButton.Name = "antiAliasingSplitButton";
            this.antiAliasingSplitButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.antiAliasingSplitButton.DropDownOpening += new EventHandler(AntiAliasingSplitButton_DropDownOpening);
            this.antiAliasingSplitButton.DropDownClosed +=
                delegate(object sender, EventArgs e)
                {
                    this.antiAliasingSplitButton.DropDownItems.Clear();
                };
            this.antiAliasingSplitButton.ButtonClick +=
                delegate(object sender, EventArgs e)
                {
                    AntiAliasing = !AntiAliasing;
                };
            //
            // alphaBlendingSplitButton
            //
            this.alphaBlendingSplitButton.Name = "alphaBlendingSplitButton";
            this.alphaBlendingSplitButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.alphaBlendingSplitButton.DropDownOpening += new EventHandler(AlphaBlendingSplitButton_DropDownOpening);
            this.alphaBlendingSplitButton.DropDownClosed +=
                delegate(object sender, EventArgs e)
                {
                    this.alphaBlendingSplitButton.DropDownItems.Clear();
                };
            this.alphaBlendingSplitButton.ButtonClick +=
                delegate(object sender, EventArgs e)
                {
                    AlphaBlending = !AlphaBlending;
                };
            //
            // toleranceLabel
            //
            this.toleranceLabel.Name = "toleranceLabel";
            //
            // toleranceSlider
            //
            this.toleranceSlider.Name = "toleranceSlider";
            this.toleranceSlider.ToleranceChanged += new EventHandler(ToleranceSlider_ToleranceChanged);
            this.toleranceSlider.Size = new Size(120, 16);
            //
            // toleranceSliderStrip
            //
            this.toleranceSliderStrip.Name = "toleranceSliderStrip";
            this.toleranceSliderStrip.AutoSize = false;
            //
            // fontLabel
            //
            this.fontLabel.Name = "fontLabel";
            //
            // fontFamilyComboBox
            //
            this.fontFamilyComboBox.Name = "fontComboBox";
            this.fontFamilyComboBox.AllowDrop = true;
            this.fontFamilyComboBox.DropDownWidth = 240;
            this.fontFamilyComboBox.MaxDropDownItems = 12;
            this.fontFamilyComboBox.Sorted = true;
            this.fontFamilyComboBox.GotFocus += new EventHandler(FontFamilyComboBox_GotFocus);
            this.fontFamilyComboBox.Items.Add(arialName);
            this.fontFamilyComboBox.SelectedItem = arialName;
            this.fontFamilyComboBox.SelectedIndexChanged += FontFamilyComboBox_SelectedIndexChanged;
            this.fontFamilyComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            //
            // fontFamilyComboBox.ComboBox
            //
            this.fontFamilyComboBox.ComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.fontFamilyComboBox.ComboBox.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.FontFamilyComboBox_MeasureItem);
            this.fontFamilyComboBox.ComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.FontFamilyComboBox_DrawItem);
            //
            // fontSizeComboBox
            //
            this.fontSizeComboBox.Name = "fontSizeComboBox";
            this.fontSizeComboBox.AutoSize = false;
            this.fontSizeComboBox.TextChanged += new EventHandler(FontSizeComboBox_TextChanged);
            this.fontSizeComboBox.Validating += new CancelEventHandler(FontSizeComboBox_Validating);
            this.fontSizeComboBox.Text = initialFontSize.ToString();
            this.fontSizeComboBox.Width = 44;
            //
            // fontSmoothingComboBox
            //
            this.fontSmoothingComboBox.Name = "smoothingComboBOx";
            this.fontSmoothingComboBox.AutoSize = false;
            this.fontSmoothingComboBox.Sorted = false;
            this.fontSmoothingComboBox.Width = 70;
            this.fontSmoothingComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.fontSmoothingComboBox.SelectedIndexChanged += new EventHandler(SmoothingComboBox_SelectedIndexChanged);
            //
            // fontBoldButton
            //
            this.fontBoldButton.Name = "boldButton";
            //
            // fontItalicsButton
            //
            this.fontItalicsButton.Name = "italicsButton";
            //
            // fontUnderlineButton
            //
            this.fontUnderlineButton.Name = "underlineButton";
            //
            // fontAlignLeftButton
            //
            this.fontAlignLeftButton.Name = "alignLeftButton";
            //
            // fontAlignCenterButton
            //
            this.fontAlignCenterButton.Name = "alignCenterButton";
            //
            // fontAlignRightButton
            //
            this.fontAlignRightButton.Name = "alignRightButton";
            //
            // resamplingSeparator
            //
            this.resamplingSeparator.Name = "resamplingSeparator";
            //
            // resamplingLabel
            //
            this.resamplingLabel.Name = "resamplingLabel";
            //
            // resamplingComboBox
            //
            this.resamplingComboBox.Name = "resamplingComboBox";
            this.resamplingComboBox.AutoSize = true;
            this.resamplingComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.resamplingComboBox.Sorted = false;
            this.resamplingComboBox.Width = 100;
            this.resamplingComboBox.DropDownWidth = 100;
            this.resamplingComboBox.SelectedIndexChanged += new EventHandler(ResamplingComboBox_SelectedIndexChanged);
            //
            // colorPickerSeparator
            //
            this.colorPickerSeparator.Name = "colorPickerSeparator";
            //
            // colorPickerLabel
            //
            this.colorPickerLabel.Name = "colorPickerLabel";
            //
            // colorPickerComboBox
            //
            this.colorPickerComboBox.Name = "colorPickerComboBox";
            this.colorPickerComboBox.AutoSize = true;
            this.colorPickerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.colorPickerComboBox.Width = 200;
            this.colorPickerComboBox.DropDownWidth = 200;
            this.colorPickerComboBox.Sorted = false;
            this.colorPickerComboBox.SelectedIndexChanged += new EventHandler(ColorPickerComboBox_SelectedIndexChanged);
            //
            // DrawConfigStrip
            //
            this.AutoSize = true;

            this.Items.Add(this.resamplingSeparator);
            this.Items.Add(this.resamplingLabel);
            this.Items.Add(this.resamplingComboBox);

            this.Items.Add(this.colorPickerSeparator);
            this.Items.Add(this.colorPickerLabel);
            this.Items.Add(this.colorPickerComboBox);

            this.Items.Add(this.fontSeparator);
            this.Items.Add(this.fontLabel);
            this.Items.Add(this.fontFamilyComboBox);
            this.Items.Add(this.fontSizeComboBox);
            this.Items.Add(this.fontSmoothingComboBox);
            this.Items.Add(this.fontStyleSeparator);
            this.Items.Add(this.fontBoldButton);
            this.Items.Add(this.fontItalicsButton);
            this.Items.Add(this.fontUnderlineButton);
            this.Items.Add(this.fontAlignSeparator);
            this.Items.Add(this.fontAlignLeftButton);
            this.Items.Add(this.fontAlignCenterButton);
            this.Items.Add(this.fontAlignRightButton);

            this.Items.Add(this.shapeSeparator);
            this.Items.Add(this.shapeButton);

            this.Items.Add(this.gradientSeparator1);
            this.Items.Add(this.gradientLinearClampedButton);
            this.Items.Add(this.gradientLinearReflectedButton);
            this.Items.Add(this.gradientLinearDiamondButton);
            this.Items.Add(this.gradientRadialButton);
            this.Items.Add(this.gradientConicalButton);
            this.Items.Add(this.gradientSeparator2);
            this.Items.Add(this.gradientChannelsSplitButton);

            this.Items.Add(this.penSeparator);
            this.Items.Add(this.penSizeLabel);
            this.Items.Add(this.penSizeDecButton);
            this.Items.Add(this.penSizeComboBox);
            this.Items.Add(this.penSizeIncButton);

            this.Items.Add(this.brushSeparator);
            this.Items.Add(this.brushStyleLabel);
            this.Items.Add(this.brushStyleComboBox);

            this.Items.Add(this.toleranceSeparator);
            this.Items.Add(this.toleranceLabel);
            this.Items.Add(this.toleranceSliderStrip);

            this.Items.Add(this.blendingSeparator);
            this.Items.Add(this.antiAliasingSplitButton);
            this.Items.Add(this.alphaBlendingSplitButton);

            this.ResumeLayout(false);            
        }

        private void GradientTypeButtonClicked(object sender, EventArgs e)
        {
            GradientType newType = (GradientType)((ToolStripButton)sender).Tag;
            GradientInfo = new GradientInfo(newType, GradientInfo.AlphaOnly);
        }

        private void ComboBoxStyle_SelectedValueChanged(object sender, EventArgs e)
        {
            OnBrushChanged();
        }

        public void PerformBrushChanged()
        {
            OnBrushChanged();
        }
        
        private void ComboBoxStyle_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            Rectangle r = e.Bounds;

            if (e.Index != -1)
            {
                string itemName = (string)this.brushStyleComboBox.Items[e.Index];

                if (itemName != this.solidBrushText)
                {
                    Rectangle rd = r;
                    rd.Width = rd.Left + 25;

                    Rectangle rt = r;
                    r.X = rd.Right;

                    string displayText = this.brushStyleComboBox.Items[e.Index].ToString();
                    HatchStyle hs = (HatchStyle)this.hatchStyleNames.LocalizedNameToEnumValue(displayText);

                    using (HatchBrush b = new HatchBrush(hs, e.ForeColor, e.BackColor))
                    {
                        e.Graphics.FillRectangle(b, rd);
                    }

                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Near;

                    using (SolidBrush sb = new SolidBrush(Color.White))
                    {
                        if ((e.State & DrawItemState.Focus) == 0)
                        {
                            sb.Color = SystemColors.Window;
                            e.Graphics.FillRectangle(sb, r);
                            sb.Color = SystemColors.WindowText;
                            e.Graphics.DrawString(displayText, this.Font, sb, r, sf);
                        }
                        else
                        {
                            sb.Color = SystemColors.Highlight;
                            e.Graphics.FillRectangle(sb, r);
                            sb.Color = SystemColors.HighlightText;
                            e.Graphics.DrawString(displayText, this.Font, sb, r, sf);
                        }
                    }

                    sf.Dispose();
                    sf = null;
                }
                else
                {
                    // Solid Brush
                    using (SolidBrush sb = new SolidBrush(Color.White))
                    {
                        if ((e.State & DrawItemState.Focus) == 0)
                        {
                            sb.Color = SystemColors.Window;
                            e.Graphics.FillRectangle(sb, e.Bounds);
                            string displayText = this.brushStyleComboBox.Items[e.Index].ToString();
                            sb.Color = SystemColors.WindowText;
                            e.Graphics.DrawString(displayText, this.Font, sb, e.Bounds);
                        }
                        else
                        {
                            sb.Color = SystemColors.Highlight;
                            e.Graphics.FillRectangle(sb, e.Bounds);
                            string displayText = this.brushStyleComboBox.Items[e.Index].ToString();
                            sb.Color = SystemColors.HighlightText;
                            e.Graphics.DrawString(displayText, this.Font, sb, e.Bounds);
                        }
                    }
                }

                e.DrawFocusRectangle();
            }
        }

        private void ComboBoxStyle_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
        {
            // Work out what the text will be
            string displayText = this.brushStyleComboBox.Items[e.Index].ToString();

            // Get width & height of string
            SizeF stringSize = e.Graphics.MeasureString(displayText, this.Font);

            // set height to text height
            e.ItemHeight = (int)stringSize.Height;

            // set width to text width
            e.ItemWidth = (int)stringSize.Width;
        }

        public event EventHandler ShapeDrawTypeChanged;
        protected virtual void OnShapeDrawTypeChanged()
        {
            if (ShapeDrawTypeChanged != null)
            {
                ShapeDrawTypeChanged(this, EventArgs.Empty);
            }
        }

        public void PerformShapeDrawTypeChanged()
        {
            OnShapeDrawTypeChanged();
        }

        public ShapeDrawType ShapeDrawType
        {
            get
            {
                return shapeDrawType;
            }

            set
            {
                if (shapeDrawType != value)
                {
                    shapeDrawType = value;

                    // if the user sets the shape the buttons must be updated
                    if (shapeDrawType == ShapeDrawType.Outline)
                    {
                        this.shapeButton.Image = shapeOutlineImage.Reference;
                    }
                    else if (shapeDrawType == ShapeDrawType.Both)
                    {
                        this.shapeButton.Image = shapeBothImage.Reference;
                    }
                    else if (shapeDrawType == ShapeDrawType.Interior)
                    {
                        this.shapeButton.Image = shapeInteriorImage.Reference;
                    }
                    else
                    {
                        // invalid shape
                        throw new InvalidOperationException("Shape draw type is invalid");
                    }

                    this.OnShapeDrawTypeChanged();
                }
            }
        }

        private void SetFontStyleButtons(FontStyle style)
        {
            bool bold = ((style & FontStyle.Bold) != 0);
            bool italic = ((style & FontStyle.Italic) != 0);
            bool underline = ((style & FontStyle.Underline) != 0);

            this.fontBoldButton.Checked = bold;
            this.fontItalicsButton.Checked = italic;
            this.fontUnderlineButton.Checked = underline;
        }

        protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == fontBoldButton)
            {
                this.fontStyle ^= FontStyle.Bold;
                SetFontStyleButtons(this.fontStyle);
                this.OnFontInfoChanged();
            }
            else if (e.ClickedItem == fontItalicsButton)
            {
                this.fontStyle ^= FontStyle.Italic;
                SetFontStyleButtons(this.fontStyle);
                this.OnFontInfoChanged();
            }
            else if (e.ClickedItem == fontUnderlineButton)
            {
                this.fontStyle ^= FontStyle.Underline;
                SetFontStyleButtons(this.fontStyle);
                this.OnFontInfoChanged();
            }
            else if (e.ClickedItem == fontAlignLeftButton)
            {
                this.FontAlignment = TextAlignment.Left;
            }
            else if (e.ClickedItem == fontAlignCenterButton)
            {
                this.FontAlignment = TextAlignment.Center;
            }
            else if (e.ClickedItem == fontAlignRightButton)
            {
                this.FontAlignment = TextAlignment.Right;
            }

            base.OnItemClicked(e);
        }

        public event EventHandler PenInfoChanged;
        protected virtual void OnPenChanged()
        {
            if (PenInfoChanged != null)
            {
                PenInfoChanged(this, EventArgs.Empty);
            }
        }

        public void PerformPenChanged()
        {
            OnPenChanged();
        }

        public void AddToPenSize(float delta)
        {
            if ((this.toolBarConfigItems & ToolBarConfigItems.Pen) == ToolBarConfigItems.Pen)
            {
                float newWidth = Utility.Clamp(PenInfo.Width + delta, minBrushSize, maxBrushSize);
                PenInfo = new PenInfo(PenInfo.DashStyle, newWidth);
            }
        }

        public PenInfo PenInfo
        {
            get
            {
                float width;

                try
                {
                    width = float.Parse(this.penSizeComboBox.Text);
                }

                catch (FormatException)
                {
                    // TODO: would much rather grab the value from AppEnvironment
                    //       or in some way that we keep track of the "last good value"
                    width = 2;
                }

                return new PenInfo(DashStyle.Solid, width);
            }

            set
            {
                if (this.PenInfo != value)
                {
                    this.penSizeComboBox.Text = value.Width.ToString();
                }
            }
        }

        private void SizeComboBox_TextChanged(object sender, System.EventArgs e)
        {
            BrushSizeComboBox_Validating(this, new CancelEventArgs());
        }

        private void BrushSizeComboBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            float penSize;
            bool valid = float.TryParse(this.penSizeComboBox.Text, out penSize);

            if (!valid)
            {
                this.penSizeComboBox.BackColor = Color.Red;
                this.penSizeComboBox.ToolTipText = PdnResources.GetString("PenConfigWidget.Error.InvalidNumber");
            }
            else
            {
                if (penSize < minPenSize)
                {
                    // Set the error if the size is too small.
                    this.penSizeComboBox.BackColor = Color.Red;
                    this.penSizeComboBox.ToolTipText = PdnResources.GetString("PenConfigWidget.Error.TooSmall");
                }
                else if (penSize > maxPenSize)
                {
                    // Set the error if the size is too large.
                    this.penSizeComboBox.BackColor = Color.Red;
                    this.penSizeComboBox.ToolTipText = PdnResources.GetString("PenConfigWidget.Error.TooLarge");
                }
                else
                {
                    // Clear the error, if any
                    this.penSizeComboBox.BackColor = SystemColors.Window;
                    this.penSizeComboBox.ToolTipText = string.Empty;
                    OnPenChanged();
                }
            }
        }

        public event EventHandler AlphaBlendingChanged;
        protected virtual void OnAlphaBlendingChanged()
        {
            if (AlphaBlendingChanged != null)
            {
                AlphaBlendingChanged(this, EventArgs.Empty);
            }
        }

        public void PerformAlphaBlendingChanged()
        {
            OnAlphaBlendingChanged();
        }

        private void GradientChannelsSplitButton_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem allChannels = new ToolStripMenuItem(
                PdnResources.GetString("GradientChannels.AllColorChannels.Text"),
                this.gradientAllColorChannelsImage.Reference,
                delegate(object sender2, EventArgs e2)
                {
                    GradientInfo = new GradientInfo(GradientInfo.GradientType, false);
                });

            ToolStripMenuItem alphaOnly = new ToolStripMenuItem(
                PdnResources.GetString("GradientChannels.AlphaChannelOnly.Text"),
                this.gradientAlphaChannelOnlyImage.Reference,
                delegate(object sender3, EventArgs e3)
                {
                    GradientInfo = new GradientInfo(GradientInfo.GradientType, true);
                });

            allChannels.Checked = !GradientInfo.AlphaOnly;
            alphaOnly.Checked = GradientInfo.AlphaOnly;

            this.gradientChannelsSplitButton.DropDownItems.Clear();
            this.gradientChannelsSplitButton.DropDownItems.AddRange(
                new ToolStripItem[]
                {
                    allChannels,
                    alphaOnly
                });
        }

        private void AlphaBlendingSplitButton_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem abEnabled = new ToolStripMenuItem(
                PdnResources.GetString("AlphaBlendingSplitButton.BlendingEnabled.Text"),
                this.alphaBlendingEnabledImage.Reference,
                delegate(object sender2, EventArgs e2)
                {
                    AlphaBlending = true;
                });

            ToolStripMenuItem abOverwrite = new ToolStripMenuItem(
                PdnResources.GetString("AlphaBlendingSplitButton.BlendingOverwrite.Text"),
                this.alphaBlendingOverwriteImage.Reference,
                delegate(object sender3, EventArgs e3)
                {
                    AlphaBlending = false;
                });

            abEnabled.Checked = AlphaBlending;
            abOverwrite.Checked = !AlphaBlending;

            this.alphaBlendingSplitButton.DropDownItems.Clear();
            this.alphaBlendingSplitButton.DropDownItems.AddRange(
                new ToolStripItem[]
                {
                    abEnabled,
                    abOverwrite
                });
        }

        public bool AlphaBlending
        {
            get
            {
                return this.alphaBlendingEnabled;
            }

            set
            {
                if (value != this.alphaBlendingEnabled)
                {
                    this.alphaBlendingEnabled = value;

                    if (value)
                    {
                        this.alphaBlendingSplitButton.Image = this.alphaBlendingEnabledImage.Reference;
                    }
                    else
                    {
                        this.alphaBlendingSplitButton.Image = this.alphaBlendingOverwriteImage.Reference;
                    }

                    OnAlphaBlendingChanged();
                }
            }
        }

        private void AlphaBlendingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnAlphaBlendingChanged();
        }

        public event EventHandler AntiAliasingChanged;
        protected virtual void OnAntiAliasingChanged()
        {
            if (AntiAliasingChanged != null)
            {
                AntiAliasingChanged(this, EventArgs.Empty);
            }
        }

        public void PerformAntiAliasingChanged()
        {
            OnAntiAliasingChanged();
        }

        public bool AntiAliasing
        {
            get
            {
                return this.antiAliasingEnabled;
            }

            set
            {
                if (this.antiAliasingEnabled != value)
                {
                    if (value)
                    {
                        this.antiAliasingSplitButton.Image = this.antiAliasingEnabledImage.Reference;
                    }
                    else
                    {
                        this.antiAliasingSplitButton.Image = this.antiAliasingDisabledImage.Reference;
                    }

                    this.antiAliasingEnabled = value;
                    OnAntiAliasingChanged();
                }
            }
        }

        private void AntiAliasingSplitButton_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem aaEnabled = new ToolStripMenuItem(
                PdnResources.GetString("AntiAliasingSplitButton.Enabled.Text"),
                this.antiAliasingEnabledImage.Reference,
                delegate(object sender2, EventArgs e2)
                {
                    AntiAliasing = true;
                });

            ToolStripMenuItem aaDisabled = new ToolStripMenuItem(
                PdnResources.GetString("AntiAliasingSplitButton.Disabled.Text"),
                this.antiAliasingDisabledImage.Reference,
                delegate(object sender3, EventArgs e3)
                {
                    AntiAliasing = false;
                });

            aaEnabled.Checked = AntiAliasing;
            aaDisabled.Checked = !AntiAliasing;

            this.antiAliasingSplitButton.DropDownItems.Clear();
            this.antiAliasingSplitButton.DropDownItems.AddRange(
                new ToolStripItem[]
                {
                    aaEnabled,
                    aaDisabled
                });            
        }

        private void SmoothingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnFontSmoothingChanged();
        }

        private void PopulateFontsBackgroundThread(object ignored)
        {
            PopulateFontsBackgroundThread();
        }

        private void PopulateFontsBackgroundThread()
        {
            try
            {
                using (new ThreadBackground(ThreadBackgroundFlags.Cpu))
                {
                    PopulateFonts();
                }
            }

            finally
            {
                staticFontNamesPopulatedEvent.Set();
            }
        }

        private void PopulateFonts()
        {
            List<string> fontNames = new List<string>();

            using (Graphics g = this.CreateGraphics())
            {
                FontFamily[] families = FontFamily.GetFamilies(g);

                foreach (FontFamily family in families)
                {
                    using (FontInfo fi = new FontInfo(family, 10, FontStyle.Regular))
                    {
                        if (!fontNames.Contains(family.Name) && fi.CanCreateFont())
                        {
                            fontNames.Add(family.Name);
                        }
                    }
                }
            }

            staticFontNames = fontNames;
        }

        public event EventHandler FontInfoChanged;
        protected virtual void OnFontInfoChanged()
        {
            if (FontInfoChanged != null)
            {
                FontInfoChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler FontAlignmentChanged;
        protected virtual void OnTextAlignmentChanged()
        {
            if (FontAlignmentChanged != null)
            {
                FontAlignmentChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler FontSmoothingChanged;
        protected virtual void OnFontSmoothingChanged()
        {
            if (FontSmoothingChanged != null)
            {
                FontSmoothingChanged(this, EventArgs.Empty);
            }
        }

        public FontSmoothing FontSmoothing
        {
            get
            {
                return (FontSmoothing)this.fontSmoothingLocalizer.LocalizedNameToEnumValue((string)this.fontSmoothingComboBox.SelectedItem);
            }

            set
            {
                if (value != this.FontSmoothing)
                {
                    this.fontSmoothingComboBox.SelectedItem = this.fontSmoothingLocalizer.EnumValueToLocalizedName(value);
                }
            }
        }

        public FontInfo FontInfo
        {
            get
            {
                return new FontInfo(this.FontFamily, this.FontSize, this.FontStyle);
            }

            set
            {
                this.FontFamily = value.FontFamily;
                this.FontSize = value.Size;
                this.FontStyle = value.FontStyle;
            }
        }

        /// <summary>
        /// Gets or sets the font style i.e. bold, italic, and underline
        /// </summary>
        public FontStyle FontStyle
        {
            get
            {
                return this.fontStyle;
            }

            set
            {
                if (this.fontStyle != value)
                {
                    this.fontStyle = value;
                    SetFontStyleButtons(FontStyle);
                    this.OnFontInfoChanged();
                }
            }
        }

        /// <summary>
        ///  Gets or sets the size of the font.
        /// </summary>
        public float FontSize
        {
            get
            {
                bool invalid = false;
                float number = oldSizeValue;

                try
                {
                    number = float.Parse(fontSizeComboBox.Text);
                }

                catch (FormatException)
                {
                    invalid = true;
                }

                catch (OverflowException)
                {
                    invalid = true;
                }

                // if the size is valid update the new size else return the last known good size.
                if (!invalid)
                {
                    oldSizeValue = number;
                }

                return oldSizeValue;
            }

            set
            {
                bool invalid = false;
                float number = oldSizeValue;

                try
                {
                    number = float.Parse(fontSizeComboBox.Text);
                }

                catch (FormatException)
                {
                    invalid = true;
                }

                catch (OverflowException)
                {
                    invalid = true;
                }

                // if the size is valid update the new size else return the last known good size.
                if (!invalid)
                {
                    if (float.Parse(fontSizeComboBox.Text) != value)
                    {
                        fontSizeComboBox.Text = value.ToString();
                        this.OnFontInfoChanged();
                    }
                }
            }
        }

        private FontFamily IndexToFontFamily(int i)
        {
            try
            {
                return new FontFamily((string)this.fontFamilyComboBox.Items[i]);
            }

            catch (Exception)
            {
                return FontFamily.GenericSansSerif;
            }
        }

        private Font IndexToFont(int i, float size, FontStyle style)
        {
            using (FontFamily ff = IndexToFontFamily(i))
            {
                return new FontInfo(ff, size, style).CreateFont();
            }
        }

        /// <summary>
        /// Gets or sets the font family.
        /// </summary>
        public FontFamily FontFamily
        {
            get
            {
                try
                {
                    return new FontFamily((string)this.fontFamilyComboBox.SelectedItem);
                }

                catch (ArgumentException)
                {
                    return FontFamily.GenericSansSerif;
                }
            }

            set
            {
                string current = (string)this.fontFamilyComboBox.SelectedItem;

                if (current != value.Name)
                {
                    int index = fontFamilyComboBox.Items.IndexOf(value.Name);

                    if (index != -1)
                    {
                        fontFamilyComboBox.SelectedIndex = index;
                        this.OnFontInfoChanged();
                    }
                    else
                    {
                        // Maybe they're just specifying a font we didn't add to the list yet? aka pre-list initialization
                        FontInfo oldFI = this.FontInfo;

                        FontFamily newFF;
                        try
                        {
                            newFF = new FontFamily(value.Name);
                        }

                        catch (Exception)
                        {
                            newFF = new FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif);
                        }

                        FontInfo newFI = new FontInfo(newFF, oldFI.Size, oldFI.FontStyle);

                        if (newFI.CanCreateFont())
                        {
                            this.fontFamilyComboBox.Items.Add(value.Name);
                            this.fontFamilyComboBox.SelectedItem = value.Name;
                        }
                        else
                        {
                            throw new InvalidOperationException("FontFamily is not valid");
                        }
                    }
                }
            }
        }

        public TextAlignment FontAlignment
        {
            get
            {
                return this.alignment;
            }
            set
            {
                if (this.alignment != value)
                {
                    this.alignment = value;

                    // if the user sets the text alignment the buttons must be updated
                    if (this.alignment == TextAlignment.Left)
                    {
                        this.fontAlignLeftButton.Checked = true;
                        this.fontAlignCenterButton.Checked = false;
                        this.fontAlignRightButton.Checked = false;
                    }
                    else if (this.alignment == TextAlignment.Center)
                    {
                        this.fontAlignLeftButton.Checked = false;
                        this.fontAlignCenterButton.Checked = true;
                        this.fontAlignRightButton.Checked = false;
                    }
                    else if (this.alignment == TextAlignment.Right)
                    {
                        this.fontAlignLeftButton.Checked = false;
                        this.fontAlignCenterButton.Checked = false;
                        this.fontAlignRightButton.Checked = true;
                    }
                    else
                    {
                        throw new InvalidOperationException("Text alignment type is invalid");
                    }

                    this.OnTextAlignmentChanged();
                }
            }
        }

        private void FontFamilyComboBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            if (e.Index != -1)
            {
                string displayText = (string)this.fontFamilyComboBox.Items[e.Index];

                SizeF stringSize = e.Graphics.MeasureString(displayText, arialFontBase);
                int size = (int)stringSize.Width;

                // set up two areas to draw
                const int leftMargin = 3;
                Rectangle bounds = e.Bounds;
                bounds.X += leftMargin;
                bounds.Width -= leftMargin;

                Rectangle r = bounds;

                Rectangle rd = r;
                rd.Width = rd.Left + size;

                Rectangle rt = r;
                r.X = rd.Right;

                using (Font myFont = IndexToFont(e.Index, 10, FontStyle.Regular))
                {
                    StringFormat sf = (StringFormat)StringFormat.GenericTypographic.Clone();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.FormatFlags &= ~StringFormatFlags.LineLimit;
                    sf.FormatFlags |= StringFormatFlags.NoWrap;

                    bool isSymbol = PaintDotNet.SystemLayer.Fonts.IsSymbolFont(myFont);
                    bool isSelected = ((e.State & DrawItemState.Selected) != 0);
                    Brush fillBrush;
                    Brush textBrush;

                    if (isSelected)
                    {
                        fillBrush = highlightBrush;
                        textBrush = highlightTextBrush;
                    }
                    else
                    {
                        fillBrush = windowBrush;
                        textBrush = windowTextBrush;
                    }

                    e.Graphics.FillRectangle(fillBrush, e.Bounds);

                    if (isSymbol)
                    {
                        e.Graphics.DrawString(displayText, arialFontBase, textBrush, bounds, sf);
                        e.Graphics.DrawString(displayText, myFont, textBrush, r, sf);
                    }
                    else
                    {
                        e.Graphics.DrawString(displayText, myFont, textBrush, bounds, sf);
                    }

                    sf.Dispose();
                    sf = null;
                }
            }

            e.DrawFocusRectangle();
        }

        private void FontFamilyComboBox_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
        {
            // Work out what the text will be
            string displayText = (string)this.fontFamilyComboBox.Items[e.Index];

            // Get width & height of string
            SizeF stringSize;
            using (Font font = IndexToFont(e.Index, 10, FontStyle.Regular))
            {
                stringSize = e.Graphics.MeasureString(displayText, font);
            }

            // Account for top margin
            stringSize.Height += UI.ScaleHeight(6);

            // set hight to text height
            e.ItemHeight = (int)stringSize.Height;
            int maxHeight = UI.ScaleHeight(20);

            if (e.ItemHeight > maxHeight)
            {
                e.ItemHeight = maxHeight;
            }

            // set width to text width
            e.ItemWidth = (int)stringSize.Width;
        }

        private void FontSizeComboBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                bool invalid = false;

                try
                {
                    float number = float.Parse(fontSizeComboBox.Text);
                }

                catch (FormatException)
                {
                    invalid = true;
                }

                catch (OverflowException)
                {
                    invalid = true;
                }

                if (invalid)
                {
                    this.fontSizeComboBox.BackColor = Color.Red;
                    this.fontSizeComboBox.ToolTipText = PdnResources.GetString("TextConfigWidget.Error.InvalidNumber");
                }
                else
                {
                    if ((float.Parse(fontSizeComboBox.Text) < minFontSize))
                    {
                        // Set the error if the size is too small.
                        this.fontSizeComboBox.BackColor = Color.Red;
                        string format = PdnResources.GetString("TextConfigWidget.Error.TooSmall.Format");
                        string text = string.Format(format, minFontSize);
                        this.fontSizeComboBox.ToolTipText = text;
                    }
                    else if ((float.Parse(fontSizeComboBox.Text) > maxFontSize))
                    {
                        // Set the error if the size is too large.
                        this.fontSizeComboBox.BackColor = Color.Red;
                        string format = PdnResources.GetString("TextConfigWidget.Error.TooLarge.Format");
                        string text = string.Format(format, maxFontSize);
                        this.fontSizeComboBox.ToolTipText = text;
                    }
                    else
                    {
                        // Clear the error, if any
                        this.fontSizeComboBox.ToolTipText = string.Empty;
                        this.fontSizeComboBox.BackColor = SystemColors.Window;
                        OnFontInfoChanged();
                    }
                }
            }

            catch (FormatException)
            {
                e.Cancel = true;
            }
        }

        private void FontSizeComboBox_TextChanged(object sender, System.EventArgs e)
        {
            FontSizeComboBox_Validating(sender, new CancelEventArgs());
        }

        private void FontFamilyComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            OnFontInfoChanged();
        }

        private void FontFamilyComboBox_GotFocus(object sender, EventArgs e)
        {
            if (!fontsComboBoxPopulated)
            {
                AsyncInitFontNames();

                using (new WaitCursorChanger(this))
                {
                    staticFontNamesPopulatedEvent.WaitOne();

                    this.fontFamilyComboBox.ComboBox.BeginUpdate();
                    List<string> fontNames = staticFontNames;

                    foreach (string name in fontNames)
                    {
                        if (!this.fontFamilyComboBox.Items.Contains(name))
                        {
                            this.fontFamilyComboBox.Items.Add(name);
                        }
                    }

                    this.fontFamilyComboBox.ComboBox.EndUpdate();
                }

                fontsComboBoxPopulated = true;
            }
        }

        public ToolBarConfigItems ToolBarConfigItems
        {
            get
            {
                return this.toolBarConfigItems;
            }

            set
            {
                this.toolBarConfigItems = value;

                SuspendLayout();

                bool showPen = ((value & ToolBarConfigItems.Pen) != ToolBarConfigItems.None);
                this.penSeparator.Visible = showPen;
                this.penSizeLabel.Visible = showPen;
                this.penSizeDecButton.Visible = showPen;
                this.penSizeComboBox.Visible = showPen;
                this.penSizeIncButton.Visible = showPen;

                bool showBrush = ((value & ToolBarConfigItems.Brush) != ToolBarConfigItems.None);
                this.brushSeparator.Visible = showBrush;
                this.brushStyleLabel.Visible = showBrush;
                this.brushStyleComboBox.Visible = showBrush;

                bool showShape = ((value & ToolBarConfigItems.ShapeType) != ToolBarConfigItems.None);
                this.shapeSeparator.Visible = showShape;
                this.shapeButton.Visible = showShape;

                bool showGradient = ((value & ToolBarConfigItems.Gradient) != ToolBarConfigItems.None);
                this.gradientSeparator1.Visible = showGradient;
                this.gradientLinearClampedButton.Visible = showGradient;
                this.gradientLinearReflectedButton.Visible = showGradient;
                this.gradientLinearDiamondButton.Visible = showGradient;
                this.gradientRadialButton.Visible = showGradient;
                this.gradientConicalButton.Visible = showGradient;
                this.gradientSeparator2.Visible = showGradient;
                this.gradientChannelsSplitButton.Visible = showGradient;

                bool showAA = ((value & ToolBarConfigItems.Antialiasing) != ToolBarConfigItems.None);
                this.antiAliasingSplitButton.Visible = showAA;

                bool showAB = ((value & ToolBarConfigItems.AlphaBlending) != ToolBarConfigItems.None);
                this.alphaBlendingSplitButton.Visible = showAB;

                bool showBlendingSep = ((value & (ToolBarConfigItems.AlphaBlending | ToolBarConfigItems.Antialiasing)) != ToolBarConfigItems.None);
                this.blendingSeparator.Visible = showBlendingSep;

                bool showTolerance = ((value & ToolBarConfigItems.Tolerance) != ToolBarConfigItems.None);
                this.toleranceSeparator.Visible = showTolerance;
                this.toleranceLabel.Visible = showTolerance;
                this.toleranceSliderStrip.Visible = showTolerance;

                bool showText = ((value & ToolBarConfigItems.Text) != ToolBarConfigItems.None);
                this.fontSeparator.Visible = showText;
                this.fontLabel.Visible = showText;
                this.fontFamilyComboBox.Visible = showText;
                this.fontSizeComboBox.Visible = showText;
                this.fontSmoothingComboBox.Visible = showText;
                this.fontStyleSeparator.Visible = showText;
                this.fontBoldButton.Visible = showText;
                this.fontItalicsButton.Visible = showText;
                this.fontUnderlineButton.Visible = showText;
                this.fontAlignSeparator.Visible = showText;
                this.fontAlignLeftButton.Visible = showText;
                this.fontAlignCenterButton.Visible = showText;
                this.fontAlignRightButton.Visible = showText;

                if (showText && IsHandleCreated)
                {
                    AsyncInitFontNames();
                }

                bool showResampling = ((value & ToolBarConfigItems.Resampling) != ToolBarConfigItems.None);
                this.resamplingSeparator.Visible = showResampling;
                this.resamplingLabel.Visible = showResampling;
                this.resamplingComboBox.Visible = showResampling;

                bool showColorPicker = ((value & ToolBarConfigItems.ColorPickerBehavior) != ToolBarConfigItems.None);
                this.colorPickerSeparator.Visible = showColorPicker;
                this.colorPickerLabel.Visible = showColorPicker;
                this.colorPickerComboBox.Visible = showColorPicker;

                if (value == ToolBarConfigItems.None)
                {
                    this.Visible = false;
                }
                else
                {
                    this.Visible = true;
                }

                ResumeLayout();
                PerformLayout();
            }
        }

        private void ToleranceSlider_ToleranceChanged(object sender, EventArgs e)
        {
            OnToleranceChanged();
        }

        public void PerformToleranceChanged()
        {
            OnToleranceChanged();
        }

        public float Tolerance
        {
            get
            {
                return this.toleranceSlider.Tolerance;
            }

            set
            {
                if (value != this.toleranceSlider.Tolerance)
                {
                    this.toleranceSlider.Tolerance = value;
                }                
            }
        }

        public event EventHandler ToleranceChanged;
        protected void OnToleranceChanged()
        {
            if (ToleranceChanged != null)
            {
                ToleranceChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler ColorPickerClickBehaviorChanged;
        protected void OnColorPickerClickBehaviorChanged()
        {
            if (ColorPickerClickBehaviorChanged != null)
            {
                ColorPickerClickBehaviorChanged(this, EventArgs.Empty);
            }
        }

        public ColorPickerClickBehavior ColorPickerClickBehavior
        {
            get
            {
                string selectedItem = (string)this.colorPickerComboBox.SelectedItem;
                ColorPickerClickBehavior cpcb = (ColorPickerClickBehavior)this.colorPickerBehaviorNames.LocalizedNameToEnumValue(selectedItem);
                return cpcb;
            }

            set
            {
                if (value != ColorPickerClickBehavior)
                {
                    this.colorPickerComboBox.SelectedItem = this.colorPickerBehaviorNames.EnumValueToLocalizedName(value);
                }
            }
        }

        public void PerformColorPickerClickBehaviorChanged()
        {
            OnColorPickerClickBehaviorChanged();
        }

        private void ColorPickerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnColorPickerClickBehaviorChanged();
        }

        private void ResamplingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnResamplingAlgorithmChanged();
        }

        public event EventHandler ResamplingAlgorithmChanged;
        protected void OnResamplingAlgorithmChanged()
        {
            if (ResamplingAlgorithmChanged != null)
            {
                ResamplingAlgorithmChanged(this, EventArgs.Empty);
            }
        }

        public ResamplingAlgorithm ResamplingAlgorithm
        {
            get
            {
                string selectedItem = (string)this.resamplingComboBox.SelectedItem;
                ResamplingAlgorithm ra = (ResamplingAlgorithm)this.resamplingAlgorithmNames.LocalizedNameToEnumValue(selectedItem);
                return ra;
            }

            set
            {
                if (value != ResamplingAlgorithm)
                {
                    if (value != ResamplingAlgorithm.NearestNeighbor && value != ResamplingAlgorithm.Bilinear)
                    {
                        throw new InvalidEnumArgumentException();
                    }

                    this.resamplingComboBox.SelectedItem = this.resamplingAlgorithmNames.EnumValueToLocalizedName(value);
                }
            }
        }

        public void PerformResamplingAlgorithmChanged()
        {
            OnResamplingAlgorithmChanged();
        }
    }
}
