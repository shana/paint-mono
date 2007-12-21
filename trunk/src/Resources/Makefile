
EXTRA_DIST = Makefile

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=..

include $(top_srcdir)/Makefile.include
include $(top_srcdir)/config.make

#include $(srcdir)/custom-hooks.make

ifeq ($(CONFIG),DEBUG_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize- -debug "-define:DEBUG;TRACE"

ASSEMBLY = bin/Debug/PaintDotNet.Resources.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../SystemLayer/bin/Debug/PaintDotNet.SystemLayer.dll
BUILD_DIR = bin/Debug/

PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE=../StylusReader/bin/Debug/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_SOURCE=../StylusReader/bin/Debug/PaintDotNet.StylusReader.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE=../SystemLayer/bin/Debug/PaintDotNet.SystemLayer.dll.mdb
MICROSOFT_INK_DLL_SOURCE=../Microsoft.Ink/Microsoft.Ink.dll
PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE=../SystemLayer/bin/Debug/PaintDotNet.SystemLayer.dll

endif

ifeq ($(CONFIG),RELEASE_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize+ -debug -define:DEBUG "-define:TRACE"

ASSEMBLY = bin/Release/PaintDotNet.Resources.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll
BUILD_DIR = bin/Release/

PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE=../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll.mdb
MICROSOFT_INK_DLL_SOURCE=../Microsoft.Ink/Microsoft.Ink.dll
PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE=../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll

endif

ifeq ($(CONFIG),RELEASE_AND_PACKAGE_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize+ -debug -define:DEBUG "-define:TRACE"

ASSEMBLY = bin/Release/PaintDotNet.Resources.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll
BUILD_DIR = bin/Release/

PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE=../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll.mdb
MICROSOFT_INK_DLL_SOURCE=../Microsoft.Ink/Microsoft.Ink.dll
PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE=../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll

endif


PROGRAMFILES = \
	$(PAINTDOTNET_STYLUSREADER_DLL_MDB) \
	$(PAINTDOTNET_STYLUSREADER_DLL) \
	$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) \
	$(MICROSOFT_INK_DLL) \
	$(PAINTDOTNET_SYSTEMLAYER_DLL)  

LINUX_PKGCONFIG = \
	$(RESOURCES_PC)  



PAINTDOTNET_STYLUSREADER_DLL_MDB = $(BUILD_DIR)/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL = $(BUILD_DIR)/PaintDotNet.StylusReader.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB = $(BUILD_DIR)/PaintDotNet.SystemLayer.dll.mdb
RESOURCES_PC = $(BUILD_DIR)/resources.pc
MICROSOFT_INK_DLL = $(BUILD_DIR)/Microsoft.Ink.dll
PAINTDOTNET_SYSTEMLAYER_DLL = $(BUILD_DIR)/PaintDotNet.SystemLayer.dll


FILES = \
	AssemblyInfo.cs \
	ImageResource.cs \
	InvariantStrings.cs \
	PdnInfo.cs \
	PdnResources.cs \
	Resource`1.cs \
	StartupTestType.cs 

DATA_FILES = 

RESOURCES = \
	Cursors/CloneStampToolCursor.cur,PaintDotNet.Cursors.CloneStampToolCursor.cur \
	Cursors/CloneStampToolCursorSetSource.cur,PaintDotNet.Cursors.CloneStampToolCursorSetSource.cur \
	Cursors/ColorPickerToolCursor.cur,PaintDotNet.Cursors.ColorPickerToolCursor.cur \
	Cursors/EllipseSelectToolCursor.cur,PaintDotNet.Cursors.EllipseSelectToolCursor.cur \
	Cursors/EllipseSelectToolCursorMouseDown.cur,PaintDotNet.Cursors.EllipseSelectToolCursorMouseDown.cur \
	Cursors/EllipseToolCursor.cur,PaintDotNet.Cursors.EllipseToolCursor.cur \
	Cursors/EraserToolCursor.cur,PaintDotNet.Cursors.EraserToolCursor.cur \
	Cursors/EraserToolCursorMouseDown.cur,PaintDotNet.Cursors.EraserToolCursorMouseDown.cur \
	Cursors/FreeformShapeToolCursor.cur,PaintDotNet.Cursors.FreeformShapeToolCursor.cur \
	Cursors/GenericToolCursorMouseDown.cur,PaintDotNet.Cursors.GenericToolCursorMouseDown.cur \
	Cursors/LassoSelectToolCursor.cur,PaintDotNet.Cursors.LassoSelectToolCursor.cur \
	Cursors/LineToolCursor.cur,PaintDotNet.Cursors.LineToolCursor.cur \
	Cursors/MagicWandToolCursor.cur,PaintDotNet.Cursors.MagicWandToolCursor.cur \
	Cursors/MoveSelectionToolCursor.cur,PaintDotNet.Cursors.MoveSelectionToolCursor.cur \
	Cursors/MoveToolCursor.cur,PaintDotNet.Cursors.MoveToolCursor.cur \
	Cursors/PaintBrushToolCursor.cur,PaintDotNet.Cursors.PaintBrushToolCursor.cur \
	Cursors/PaintBrushToolCursorMouseDown.cur,PaintDotNet.Cursors.PaintBrushToolCursorMouseDown.cur \
	Cursors/PaintBucketToolCursor.cur,PaintDotNet.Cursors.PaintBucketToolCursor.cur \
	Cursors/PanToolCursor.cur,PaintDotNet.Cursors.PanToolCursor.cur \
	Cursors/PanToolCursorInvalid.cur,PaintDotNet.Cursors.PanToolCursorInvalid.cur \
	Cursors/PanToolCursorMouseDown.cur,PaintDotNet.Cursors.PanToolCursorMouseDown.cur \
	Cursors/PencilToolCursor.cur,PaintDotNet.Cursors.PencilToolCursor.cur \
	Cursors/RecoloringToolCursor.cur,PaintDotNet.Cursors.RecoloringToolCursor.cur \
	Cursors/RecoloringToolCursorAdjustColor.cur,PaintDotNet.Cursors.RecoloringToolCursorAdjustColor.cur \
	Cursors/RecoloringToolCursorPickColor.cur,PaintDotNet.Cursors.RecoloringToolCursorPickColor.cur \
	Cursors/RectangleSelectToolCursor.cur,PaintDotNet.Cursors.RectangleSelectToolCursor.cur \
	Cursors/RectangleSelectToolCursorMouseDown.cur,PaintDotNet.Cursors.RectangleSelectToolCursorMouseDown.cur \
	Cursors/RectangleToolCursor.cur,PaintDotNet.Cursors.RectangleToolCursor.cur \
	Cursors/RoundedRectangleToolCursor.cur,PaintDotNet.Cursors.RoundedRectangleToolCursor.cur \
	Cursors/ShapeToolCursor.cur,PaintDotNet.Cursors.ShapeToolCursor.cur \
	Cursors/ShapeToolCursorMouseDown.cur,PaintDotNet.Cursors.ShapeToolCursorMouseDown.cur \
	Cursors/TextToolCursor.cur,PaintDotNet.Cursors.TextToolCursor.cur \
	Cursors/ZoomInToolCursor.cur,PaintDotNet.Cursors.ZoomInToolCursor.cur \
	Cursors/ZoomOutToolCursor.cur,PaintDotNet.Cursors.ZoomOutToolCursor.cur \
	Cursors/ZoomToolCursor.cur,PaintDotNet.Cursors.ZoomToolCursor.cur \
	Files/AboutCredits.rtf,PaintDotNet.Files.AboutCredits.rtf \
	Files/License.txt,PaintDotNet.Files.License.txt \
	Images/AnchorChooserControl.AnchorImage.png,PaintDotNet.Images.AnchorChooserControl.AnchorImage.png \
	Images/TransparentLogo.png,PaintDotNet.Images.TransparentLogo.png \
	Icons/AddNoiseEffect.png,PaintDotNet.Icons.AddNoiseEffect.png \
	Icons/AutoLevel.png,PaintDotNet.Icons.AutoLevel.png \
	Icons/BlackAndWhiteIcon.png,PaintDotNet.Icons.BlackAndWhiteIcon.png \
	Icons/BlurEffect.png,PaintDotNet.Icons.BlurEffect.png \
	Icons/BrightnessAndContrastAdjustment.png,PaintDotNet.Icons.BrightnessAndContrastAdjustment.png \
	Icons/CloneStampToolIcon.png,PaintDotNet.Icons.CloneStampToolIcon.png \
	Icons/ColorPickerToolIcon.png,PaintDotNet.Icons.ColorPickerToolIcon.png \
	Icons/CursorXYIcon.png,PaintDotNet.Icons.CursorXYIcon.png \
	Icons/CurvesEffect.png,PaintDotNet.Icons.CurvesEffect.png \
	Icons/DesaturateEffect.png,PaintDotNet.Icons.DesaturateEffect.png \
	Icons/EdgeDetectEffect.png,PaintDotNet.Icons.EdgeDetectEffect.png \
	Icons/EllipseSelectToolIcon.png,PaintDotNet.Icons.EllipseSelectToolIcon.png \
	Icons/EllipseToolIcon.png,PaintDotNet.Icons.EllipseToolIcon.png \
	Icons/EmbossEffect.png,PaintDotNet.Icons.EmbossEffect.png \
	Icons/EraserToolIcon.png,PaintDotNet.Icons.EraserToolIcon.png \
	Icons/FontBoldIcon.png,PaintDotNet.Icons.FontBoldIcon.png \
	Icons/FontItalicIcon.png,PaintDotNet.Icons.FontItalicIcon.png \
	Icons/FontUnderlineIcon.png,PaintDotNet.Icons.FontUnderlineIcon.png \
	Icons/FreeformShapeToolIcon.png,PaintDotNet.Icons.FreeformShapeToolIcon.png \
	Icons/FrostedGlassEffect.png,PaintDotNet.Icons.FrostedGlassEffect.png \
	Icons/HistoryFastForwardIcon.png,PaintDotNet.Icons.HistoryFastForwardIcon.png \
	Icons/HistoryRewindIcon.png,PaintDotNet.Icons.HistoryRewindIcon.png \
	Icons/HueAndSaturationAdjustment.png,PaintDotNet.Icons.HueAndSaturationAdjustment.png \
	Icons/ImageFromDiskIcon.png,PaintDotNet.Icons.ImageFromDiskIcon.png \
	Icons/ImageSizeIcon.png,PaintDotNet.Icons.ImageSizeIcon.png \
	Icons/InvertColorsEffect.png,PaintDotNet.Icons.InvertColorsEffect.png \
	Icons/LassoSelectToolIcon.png,PaintDotNet.Icons.LassoSelectToolIcon.png \
	Icons/LevelsEffect.png,PaintDotNet.Icons.LevelsEffect.png \
	Icons/LineToolIcon.png,PaintDotNet.Icons.LineToolIcon.png \
	Icons/MagicWandToolIcon.png,PaintDotNet.Icons.MagicWandToolIcon.png \
	Icons/MenuEditCopyIcon.png,PaintDotNet.Icons.MenuEditCopyIcon.png \
	Icons/MenuEditCutIcon.png,PaintDotNet.Icons.MenuEditCutIcon.png \
	Icons/MenuEditDeselectIcon.png,PaintDotNet.Icons.MenuEditDeselectIcon.png \
	Icons/MenuEditEraseSelectionIcon.png,PaintDotNet.Icons.MenuEditEraseSelectionIcon.png \
	Icons/MenuEditInvertSelectionIcon.png,PaintDotNet.Icons.MenuEditInvertSelectionIcon.png \
	Icons/MenuEditPasteIcon.png,PaintDotNet.Icons.MenuEditPasteIcon.png \
	Icons/MenuEditPasteInToNewLayerIcon.png,PaintDotNet.Icons.MenuEditPasteInToNewLayerIcon.png \
	Icons/MenuEditRedoIcon.png,PaintDotNet.Icons.MenuEditRedoIcon.png \
	Icons/MenuEditSelectAllIcon.png,PaintDotNet.Icons.MenuEditSelectAllIcon.png \
	Icons/MenuEditUndoIcon.png,PaintDotNet.Icons.MenuEditUndoIcon.png \
	Icons/MenuFileAcquireFromClipboardIcon.png,PaintDotNet.Icons.MenuFileAcquireFromClipboardIcon.png \
	Icons/MenuFileAcquireFromScannerOrCameraIcon.png,PaintDotNet.Icons.MenuFileAcquireFromScannerOrCameraIcon.png \
	Icons/MenuFileNewIcon.png,PaintDotNet.Icons.MenuFileNewIcon.png \
	Icons/MenuFileOpenIcon.png,PaintDotNet.Icons.MenuFileOpenIcon.png \
	Icons/MenuFilePrintIcon.png,PaintDotNet.Icons.MenuFilePrintIcon.png \
	Icons/MenuFileSaveIcon.png,PaintDotNet.Icons.MenuFileSaveIcon.png \
	Icons/MenuHelpAboutIcon.png,PaintDotNet.Icons.MenuHelpAboutIcon.png \
	Icons/MenuHelpHelpTopicsIcon.png,PaintDotNet.Icons.MenuHelpHelpTopicsIcon.png \
	Icons/MenuHelpLanguageIcon.png,PaintDotNet.Icons.MenuHelpLanguageIcon.png \
	Icons/MenuHelpSendFeedbackIcon.png,PaintDotNet.Icons.MenuHelpSendFeedbackIcon.png \
	Icons/MenuHelpCheckForUpdatesIcon.png,PaintDotNet.Icons.MenuHelpCheckForUpdatesIcon.png \
	Icons/MenuImageCanvasSizeIcon.png,PaintDotNet.Icons.MenuImageCanvasSizeIcon.png \
	Icons/MenuImageCropIcon.png,PaintDotNet.Icons.MenuImageCropIcon.png \
	Icons/MenuImageFlattenIcon.png,PaintDotNet.Icons.MenuImageFlattenIcon.png \
	Icons/MenuImageFlipHorizontalIcon.png,PaintDotNet.Icons.MenuImageFlipHorizontalIcon.png \
	Icons/MenuImageFlipVerticalIcon.png,PaintDotNet.Icons.MenuImageFlipVerticalIcon.png \
	Icons/MenuImageResizeIcon.png,PaintDotNet.Icons.MenuImageResizeIcon.png \
	Icons/MenuImageRotate180CCWIcon.png,PaintDotNet.Icons.MenuImageRotate180CCWIcon.png \
	Icons/MenuImageRotate180CWIcon.png,PaintDotNet.Icons.MenuImageRotate180CWIcon.png \
	Icons/MenuImageRotate270CCWIcon.png,PaintDotNet.Icons.MenuImageRotate270CCWIcon.png \
	Icons/MenuImageRotate270CWIcon.png,PaintDotNet.Icons.MenuImageRotate270CWIcon.png \
	Icons/MenuImageRotate90CCWIcon.png,PaintDotNet.Icons.MenuImageRotate90CCWIcon.png \
	Icons/MenuImageRotate90CWIcon.png,PaintDotNet.Icons.MenuImageRotate90CWIcon.png \
	Icons/MenuLayersAddNewLayerIcon.png,PaintDotNet.Icons.MenuLayersAddNewLayerIcon.png \
	Icons/MenuLayersDeleteLayerIcon.png,PaintDotNet.Icons.MenuLayersDeleteLayerIcon.png \
	Icons/MenuLayersDuplicateLayerIcon.png,PaintDotNet.Icons.MenuLayersDuplicateLayerIcon.png \
	Icons/MenuLayersFlipHorizontalIcon.png,PaintDotNet.Icons.MenuLayersFlipHorizontalIcon.png \
	Icons/MenuLayersFlipVerticalIcon.png,PaintDotNet.Icons.MenuLayersFlipVerticalIcon.png \
	Icons/MenuLayersImportFromFileIcon.png,PaintDotNet.Icons.MenuLayersImportFromFileIcon.png \
	Icons/MenuLayersLayerPropertiesIcon.png,PaintDotNet.Icons.MenuLayersLayerPropertiesIcon.png \
	Icons/MenuLayersMoveLayerDownIcon.png,PaintDotNet.Icons.MenuLayersMoveLayerDownIcon.png \
	Icons/MenuLayersMoveLayerUpIcon.png,PaintDotNet.Icons.MenuLayersMoveLayerUpIcon.png \
	Icons/MenuViewActualSizeIcon.png,PaintDotNet.Icons.MenuViewActualSizeIcon.png \
	Icons/MenuViewGridIcon.png,PaintDotNet.Icons.MenuViewGridIcon.png \
	Icons/MenuViewRulersIcon.png,PaintDotNet.Icons.MenuViewRulersIcon.png \
	Icons/MenuViewZoomInIcon.png,PaintDotNet.Icons.MenuViewZoomInIcon.png \
	Icons/MenuViewZoomOutIcon.png,PaintDotNet.Icons.MenuViewZoomOutIcon.png \
	Icons/MenuViewZoomToSelectionIcon.png,PaintDotNet.Icons.MenuViewZoomToSelectionIcon.png \
	Icons/MenuViewZoomToWindowIcon.png,PaintDotNet.Icons.MenuViewZoomToWindowIcon.png \
	Icons/MenuWindowColorsIcon.png,PaintDotNet.Icons.MenuWindowColorsIcon.png \
	Icons/MenuWindowHistoryIcon.png,PaintDotNet.Icons.MenuWindowHistoryIcon.png \
	Icons/MenuWindowLayersIcon.png,PaintDotNet.Icons.MenuWindowLayersIcon.png \
	Icons/MenuWindowToolsIcon.png,PaintDotNet.Icons.MenuWindowToolsIcon.png \
	Icons/MotionBlurEffect.png,PaintDotNet.Icons.MotionBlurEffect.png \
	Icons/MoveSelectionToolIcon.png,PaintDotNet.Icons.MoveSelectionToolIcon.png \
	Icons/MoveToolIcon.png,PaintDotNet.Icons.MoveToolIcon.png \
	Icons/OilPaintingEffect.png,PaintDotNet.Icons.OilPaintingEffect.png \
	Icons/PaintBrushToolIcon.png,PaintDotNet.Icons.PaintBrushToolIcon.png \
	Icons/PaintBucketIcon.png,PaintDotNet.Icons.PaintBucketIcon.png \
	Icons/PaintDotNet.ico,PaintDotNet.Icons.PaintDotNet.ico \
	Icons/PanToolIcon.png,PaintDotNet.Icons.PanToolIcon.png \
	Icons/PencilToolIcon.png,PaintDotNet.Icons.PencilToolIcon.png \
	Icons/PixelateEffect.png,PaintDotNet.Icons.PixelateEffect.png \
	Icons/RadialBlurEffect.png,PaintDotNet.Icons.RadialBlurEffect.png \
	Icons/RecoloringToolIcon.png,PaintDotNet.Icons.RecoloringToolIcon.png \
	Icons/RectangleSelectToolIcon.png,PaintDotNet.Icons.RectangleSelectToolIcon.png \
	Icons/RectangleToolIcon.png,PaintDotNet.Icons.RectangleToolIcon.png \
	Icons/RedEyeRemoveEffect.png,PaintDotNet.Icons.RedEyeRemoveEffect.png \
	Icons/ReliefEffect.png,PaintDotNet.Icons.ReliefEffect.png \
	Icons/RotateZoomIcon.png,PaintDotNet.Icons.RotateZoomIcon.png \
	Icons/RoundedRectangleToolIcon.png,PaintDotNet.Icons.RoundedRectangleToolIcon.png \
	Icons/SelectionIcon.png,PaintDotNet.Icons.SelectionIcon.png \
	Icons/SepiaEffect.png,PaintDotNet.Icons.SepiaEffect.png \
	Icons/ShapeBothIcon.png,PaintDotNet.Icons.ShapeBothIcon.png \
	Icons/ShapeInteriorIcon.png,PaintDotNet.Icons.ShapeInteriorIcon.png \
	Icons/ShapeOutlineIcon.png,PaintDotNet.Icons.ShapeOutlineIcon.png \
	Icons/SharpenEffect.png,PaintDotNet.Icons.SharpenEffect.png \
	Icons/SwapIcon.png,PaintDotNet.Icons.SwapIcon.png \
	Icons/TextAlignCenterIcon.png,PaintDotNet.Icons.TextAlignCenterIcon.png \
	Icons/TextAlignLeftIcon.png,PaintDotNet.Icons.TextAlignLeftIcon.png \
	Icons/TextAlignRightIcon.png,PaintDotNet.Icons.TextAlignRightIcon.png \
	Icons/TextToolIcon.png,PaintDotNet.Icons.TextToolIcon.png \
	Icons/ZoomToolIcon.png,PaintDotNet.Icons.ZoomToolIcon.png \
	Icons/todo.png,PaintDotNet.Icons.todo.png \
	Icons/MenuFileSaveAsIcon.png,PaintDotNet.Icons.MenuFileSaveAsIcon.png \
	Icons/BulgeEffect.png,PaintDotNet.Icons.BulgeEffect.png \
	Icons/GlowEffect.png,PaintDotNet.Icons.GlowEffect.png \
	Icons/PolarInversionEffect.png,PaintDotNet.Icons.PolarInversionEffect.png \
	Icons/TileEffect.png,PaintDotNet.Icons.TileEffect.png \
	Icons/TwistEffect.png,PaintDotNet.Icons.TwistEffect.png \
	Icons/ZoomBlurEffect.png,PaintDotNet.Icons.ZoomBlurEffect.png \
	Icons/ColorAddOverlay.png,PaintDotNet.Icons.ColorAddOverlay.png \
	Icons/ColorPalettes.png,PaintDotNet.Icons.ColorPalettes.png \
	Images/Banner.png,PaintDotNet.Images.Banner.png \
	Icons/MenuEditPasteInToNewImageIcon.png,PaintDotNet.Icons.MenuEditPasteInToNewImageIcon.png \
	Icons/GradientToolIcon.png,PaintDotNet.Icons.GradientToolIcon.png \
	Icons/UnsavedChangesDialog.CancelButton.png,PaintDotNet.Icons.UnsavedChangesDialog.CancelButton.png \
	Icons/UnsavedChangesDialog.SaveButton.png,PaintDotNet.Icons.UnsavedChangesDialog.SaveButton.png \
	Icons/Question.ico,PaintDotNet.Icons.Question.ico \
	Icons/CancelIcon.png,PaintDotNet.Icons.CancelIcon.png \
	Icons/MenuFileCloseIcon.png,PaintDotNet.Icons.MenuFileCloseIcon.png \
	Icons/WarningIcon.png,PaintDotNet.Icons.WarningIcon.png \
	Images/RoundedEdgeLL.png,PaintDotNet.Images.RoundedEdgeLL.png \
	Images/RoundedEdgeLR.png,PaintDotNet.Images.RoundedEdgeLR.png \
	Images/RoundedEdgeUL.png,PaintDotNet.Images.RoundedEdgeUL.png \
	Images/RoundedEdgeUR.png,PaintDotNet.Images.RoundedEdgeUR.png \
	Icons/MenuWindowOpenMdiListIcon.png,PaintDotNet.Icons.MenuWindowOpenMdiListIcon.png \
	Images/Icon50x50.png,PaintDotNet.Images.Icon50x50.png \
	Images/PayPalDonate.gif,PaintDotNet.Images.PayPalDonate.gif \
	Cursors/GenericToolCursor.cur,PaintDotNet.Cursors.GenericToolCursor.cur \
	Icons/ConicalGradientIcon.png,PaintDotNet.Icons.ConicalGradientIcon.png \
	Icons/RadialGradientIcon.png,PaintDotNet.Icons.RadialGradientIcon.png \
	Icons/LinearClampedGradientIcon.png,PaintDotNet.Icons.LinearClampedGradientIcon.png \
	Icons/LinearReflectedGradientIcon.png,PaintDotNet.Icons.LinearReflectedGradientIcon.png \
	Icons/LinearDiamondGradientIcon.png,PaintDotNet.Icons.LinearDiamondGradientIcon.png \
	Icons/MenuLayersMergeLayerDownIcon.png,PaintDotNet.Icons.MenuLayersMergeLayerDownIcon.png \
	Icons/AntiAliasingEnabledIcon.png,PaintDotNet.Icons.AntiAliasingEnabledIcon.png \
	Icons/AntiAliasingDisabledIcon.png,PaintDotNet.Icons.AntiAliasingDisabledIcon.png \
	Icons/BlendingEnabledIcon.png,PaintDotNet.Icons.BlendingEnabledIcon.png \
	Icons/BlendingOverwriteIcon.png,PaintDotNet.Icons.BlendingOverwriteIcon.png \
	Icons/AllColorChannelsIcon.png,PaintDotNet.Icons.AllColorChannelsIcon.png \
	Icons/AlphaChannelOnlyIcon.png,PaintDotNet.Icons.AlphaChannelOnlyIcon.png \
	Icons/SettingsIcon.png,PaintDotNet.Icons.SettingsIcon.png \
	Icons/UnfocusEffectIcon.png,PaintDotNet.Icons.UnfocusEffectIcon.png \
	Icons/MedianEffectIcon.png,PaintDotNet.Icons.MedianEffectIcon.png \
	Icons/OutlineEffectIcon.png,PaintDotNet.Icons.OutlineEffectIcon.png \
	Icons/SavePaletteIcon.png,PaintDotNet.Icons.SavePaletteIcon.png \
	Icons/MenuEditFillSelectionIcon.png,PaintDotNet.Icons.MenuEditFillSelectionIcon.png \
	Icons/MinusButtonIcon.png,PaintDotNet.Icons.MinusButtonIcon.png \
	Icons/PlusButtonIcon.png,PaintDotNet.Icons.PlusButtonIcon.png \
	Icons/SwatchIcon.png,PaintDotNet.Icons.SwatchIcon.png \
	Icons/CloudsEffect.png,PaintDotNet.Icons.CloudsEffect.png \
	Icons/MenuHelpTutorialsIcon.png,PaintDotNet.Icons.MenuHelpTutorialsIcon.png \
	Icons/MenuHelpPluginsIcon.png,PaintDotNet.Icons.MenuHelpPluginsIcon.png \
	Icons/MenuHelpForumIcon.png,PaintDotNet.Icons.MenuHelpForumIcon.png \
	Icons/MenuHelpPdnWebsiteIcon.png,PaintDotNet.Icons.MenuHelpPdnWebsiteIcon.png \
	Icons/MenuHelpDonateIcon.png,PaintDotNet.Icons.MenuHelpDonateIcon.png 

EXTRAS = \
	resources.pc.in 

REFERENCES =  \
	System \
	System.Drawing \
	System.Windows.Forms

DLL_REFERENCES = 

CLEANFILES += $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

#Targets
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG)  $(top_srcdir)/config.make

$(PAINTDOTNET_STYLUSREADER_DLL_MDB): $(PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_STYLUSREADER_DLL): $(PAINTDOTNET_STYLUSREADER_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB): $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(RESOURCES_PC): resources.pc
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(MICROSOFT_INK_DLL): $(MICROSOFT_INK_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_SYSTEMLAYER_DLL): $(PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'



resources.pc: resources.pc.in $(top_srcdir)/config.make
	sed -e "s,@prefix@,$(prefix)," -e "s,@PACKAGE@,$(PACKAGE)," < resources.pc.in > resources.pc


$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(build_resx_resources) : %.resources: %.resx
	resgen2 '$<' '$@'



$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list)
	make pre-all-local-hook prefix=$(prefix)
	mkdir -p $(dir $(ASSEMBLY))
	make $(CONFIG)_BeforeBuild
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
	make $(CONFIG)_AfterBuild
	make post-all-local-hook prefix=$(prefix)


install-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(PAINTDOTNET_STYLUSREADER_DLL_MDB) $(PAINTDOTNET_STYLUSREADER_DLL) $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) $(RESOURCES_PC) $(MICROSOFT_INK_DLL) $(PAINTDOTNET_SYSTEMLAYER_DLL)
	make pre-install-local-hook prefix=$(prefix)
	mkdir -p $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	cp $(ASSEMBLY) $(ASSEMBLY_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL_MDB)' || cp $(PAINTDOTNET_STYLUSREADER_DLL_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL)' || cp $(PAINTDOTNET_STYLUSREADER_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB)' || cp $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	mkdir -p $(DESTDIR)$(prefix)/lib/pkgconfig
	test -z '$(RESOURCES_PC)' || cp $(RESOURCES_PC) $(DESTDIR)$(prefix)/lib/pkgconfig
	test -z '$(MICROSOFT_INK_DLL)' || cp $(MICROSOFT_INK_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL)' || cp $(PAINTDOTNET_SYSTEMLAYER_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(PAINTDOTNET_STYLUSREADER_DLL_MDB) $(PAINTDOTNET_STYLUSREADER_DLL) $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) $(RESOURCES_PC) $(MICROSOFT_INK_DLL) $(PAINTDOTNET_SYSTEMLAYER_DLL)
	make pre-uninstall-local-hook prefix=$(prefix)
	rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY))
	test -z '$(ASSEMBLY_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY_MDB))
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_STYLUSREADER_DLL_MDB))
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_STYLUSREADER_DLL))
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB))
	test -z '$(RESOURCES_PC)' || rm -f $(DESTDIR)$(prefix)/lib/pkgconfig/$(notdir $(RESOURCES_PC))
	test -z '$(MICROSOFT_INK_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(MICROSOFT_INK_DLL))
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_SYSTEMLAYER_DLL))
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make
