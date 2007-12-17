

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=.

include $(top_srcdir)/Makefile.include
include $(top_srcdir)/config.make

ifeq ($(CONFIG),DEBUG_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize- -debug "-define:DEBUG;TRACE"

ASSEMBLY = ../../..//cvs/pdn3/src/../../..//cvs/pdn3/src/../../..//cvs/pdn3/src/../../..//cvs/pdn3/src/bin/Debug/PaintDotNet.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = winexe
PROJECT_REFERENCES =  \
	../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/bin/Debug/PaintDotNet.Data.dll \
	../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/bin/Debug/PaintDotNet.Effects.dll \
	../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/bin/Debug/PdnLib.dll \
	../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/bin/Debug/PaintDotNet.Resources.dll \
	../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/bin/Debug/PaintDotNet.SystemLayer.dll
BUILD_DIR = ../../..//cvs/pdn3/src/../../..//cvs/pdn3/src/../../..//cvs/pdn3/src/../../..//cvs/pdn3/src/bin/Debug/

PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE=../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/bin/Debug/PaintDotNet.SystemLayer.dll
PAINTDOTNET_DATA_DLL_MDB_SOURCE=../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/bin/Debug/PaintDotNet.Data.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE=../../..//cvs/pdn3/src/StylusReader/../../../..//cvs/pdn3/src/StylusReader/../../../..//cvs/pdn3/src/StylusReader/../../../..//cvs/pdn3/src/StylusReader/bin/Debug/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_EFFECTS_DLL_MDB_SOURCE=../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/bin/Debug/PaintDotNet.Effects.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_SOURCE=../../..//cvs/pdn3/src/StylusReader/../../../..//cvs/pdn3/src/StylusReader/../../../..//cvs/pdn3/src/StylusReader/../../../..//cvs/pdn3/src/StylusReader/bin/Debug/PaintDotNet.StylusReader.dll
PAINTDOTNET_RESOURCES_DLL_SOURCE=../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/bin/Debug/PaintDotNet.Resources.dll
PDNLIB_DLL_MDB_SOURCE=../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/bin/Debug/PdnLib.dll.mdb
PAINTDOTNET_DATA_DLL_SOURCE=../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/bin/Debug/PaintDotNet.Data.dll
PDNLIB_DLL_SOURCE=../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/bin/Debug/PdnLib.dll
PAINTDOTNET_EFFECTS_DLL_SOURCE=../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/bin/Debug/PaintDotNet.Effects.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE=../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/bin/Debug/PaintDotNet.SystemLayer.dll.mdb
PAINTDOTNET_RESOURCES_DLL_MDB_SOURCE=../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/bin/Debug/PaintDotNet.Resources.dll.mdb
MICROSOFT_INK_DLL_SOURCE=Microsoft.Ink/Microsoft.Ink.dll

endif

ifeq ($(CONFIG),RELEASE_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize+ -debug -define:DEBUG "-define:TRACE"

ASSEMBLY = ../../..//cvs/pdn3/src/../../..//cvs/pdn3/src/../../..//cvs/pdn3/src/../../..//cvs/pdn3/src/bin/Release/PaintDotNet.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = winexe
PROJECT_REFERENCES =  \
	../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/bin/Release/PaintDotNet.Data.dll \
	../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/bin/Release/PaintDotNet.Effects.dll \
	../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/bin/Release/PdnLib.dll \
	../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/bin/Release/PaintDotNet.Resources.dll \
	../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll
BUILD_DIR = ../../..//cvs/pdn3/src/../../..//cvs/pdn3/src/../../..//cvs/pdn3/src/../../..//cvs/pdn3/src/bin/Release/

PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE=../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll
PAINTDOTNET_DATA_DLL_MDB_SOURCE=../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/bin/Release/PaintDotNet.Data.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE=../../..//cvs/pdn3/src/StylusReader/../../../..//cvs/pdn3/src/StylusReader/../../../..//cvs/pdn3/src/StylusReader/../../../..//cvs/pdn3/src/StylusReader/bin/Release/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_EFFECTS_DLL_MDB_SOURCE=../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/bin/Release/PaintDotNet.Effects.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_SOURCE=../../..//cvs/pdn3/src/StylusReader/../../../..//cvs/pdn3/src/StylusReader/../../../..//cvs/pdn3/src/StylusReader/../../../..//cvs/pdn3/src/StylusReader/bin/Release/PaintDotNet.StylusReader.dll
PAINTDOTNET_RESOURCES_DLL_SOURCE=../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/bin/Release/PaintDotNet.Resources.dll
PDNLIB_DLL_MDB_SOURCE=../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/bin/Release/PdnLib.dll.mdb
PAINTDOTNET_DATA_DLL_SOURCE=../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/bin/Release/PaintDotNet.Data.dll
PDNLIB_DLL_SOURCE=../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/bin/Release/PdnLib.dll
PAINTDOTNET_EFFECTS_DLL_SOURCE=../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/bin/Release/PaintDotNet.Effects.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE=../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll.mdb
PAINTDOTNET_RESOURCES_DLL_MDB_SOURCE=../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/bin/Release/PaintDotNet.Resources.dll.mdb
MICROSOFT_INK_DLL_SOURCE=Microsoft.Ink/Microsoft.Ink.dll

endif

ifeq ($(CONFIG),RELEASE_AND_PACKAGE_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize+ -debug -define:DEBUG "-define:TRACE"

ASSEMBLY = ../../..//cvs/pdn3/src/../../..//cvs/pdn3/src/../../..//cvs/pdn3/src/../../..//cvs/pdn3/src/bin/Release/PaintDotNet.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = winexe
PROJECT_REFERENCES =  \
	../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/bin/Release/PaintDotNet.Data.dll \
	../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/bin/Release/PaintDotNet.Effects.dll \
	../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/bin/Release/PdnLib.dll \
	../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/bin/Release/PaintDotNet.Resources.dll \
	../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll
BUILD_DIR = ../../..//cvs/pdn3/src/../../..//cvs/pdn3/src/../../..//cvs/pdn3/src/../../..//cvs/pdn3/src/bin/Release/

PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE=../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll
PAINTDOTNET_DATA_DLL_MDB_SOURCE=../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/bin/Release/PaintDotNet.Data.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE=../../..//cvs/pdn3/src/StylusReader/../../../..//cvs/pdn3/src/StylusReader/../../../..//cvs/pdn3/src/StylusReader/../../../..//cvs/pdn3/src/StylusReader/bin/Release/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_EFFECTS_DLL_MDB_SOURCE=../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/bin/Release/PaintDotNet.Effects.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_SOURCE=../../..//cvs/pdn3/src/StylusReader/../../../..//cvs/pdn3/src/StylusReader/../../../..//cvs/pdn3/src/StylusReader/../../../..//cvs/pdn3/src/StylusReader/bin/Release/PaintDotNet.StylusReader.dll
PAINTDOTNET_RESOURCES_DLL_SOURCE=../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/bin/Release/PaintDotNet.Resources.dll
PDNLIB_DLL_MDB_SOURCE=../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/bin/Release/PdnLib.dll.mdb
PAINTDOTNET_DATA_DLL_SOURCE=../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/../../../..//cvs/pdn3/src/Data/bin/Release/PaintDotNet.Data.dll
PDNLIB_DLL_SOURCE=../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/../../../..//cvs/pdn3/src/PdnLib/bin/Release/PdnLib.dll
PAINTDOTNET_EFFECTS_DLL_SOURCE=../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/../../../..//cvs/pdn3/src/Effects/bin/Release/PaintDotNet.Effects.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE=../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/../../../..//cvs/pdn3/src/SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll.mdb
PAINTDOTNET_RESOURCES_DLL_MDB_SOURCE=../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/../../../..//cvs/pdn3/src/Resources/bin/Release/PaintDotNet.Resources.dll.mdb
MICROSOFT_INK_DLL_SOURCE=Microsoft.Ink/Microsoft.Ink.dll

endif


PROGRAMFILES = \
	$(PAINTDOTNET_SYSTEMLAYER_DLL) \
	$(PAINTDOTNET_DATA_DLL_MDB) \
	$(PAINTDOTNET_STYLUSREADER_DLL_MDB) \
	$(PAINTDOTNET_EFFECTS_DLL_MDB) \
	$(PAINTDOTNET_STYLUSREADER_DLL) \
	$(PAINTDOTNET_RESOURCES_DLL) \
	$(PDNLIB_DLL_MDB) \
	$(PAINTDOTNET_DATA_DLL) \
	$(PDNLIB_DLL) \
	$(PAINTDOTNET_EFFECTS_DLL) \
	$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) \
	$(PAINTDOTNET_RESOURCES_DLL_MDB) \
	$(MICROSOFT_INK_DLL)  

BINARIES = \
	$(PAINTDOTNET)  



PAINTDOTNET_SYSTEMLAYER_DLL = $(BUILD_DIR)/PaintDotNet.SystemLayer.dll
PAINTDOTNET_DATA_DLL_MDB = $(BUILD_DIR)/PaintDotNet.Data.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_MDB = $(BUILD_DIR)/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_EFFECTS_DLL_MDB = $(BUILD_DIR)/PaintDotNet.Effects.dll.mdb
PAINTDOTNET = $(BUILD_DIR)/paintdotnet
PAINTDOTNET_STYLUSREADER_DLL = $(BUILD_DIR)/PaintDotNet.StylusReader.dll
PAINTDOTNET_RESOURCES_DLL = $(BUILD_DIR)/PaintDotNet.Resources.dll
PDNLIB_DLL_MDB = $(BUILD_DIR)/PdnLib.dll.mdb
PAINTDOTNET_DATA_DLL = $(BUILD_DIR)/PaintDotNet.Data.dll
PDNLIB_DLL = $(BUILD_DIR)/PdnLib.dll
PAINTDOTNET_EFFECTS_DLL = $(BUILD_DIR)/PaintDotNet.Effects.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB = $(BUILD_DIR)/PaintDotNet.SystemLayer.dll.mdb
PAINTDOTNET_RESOURCES_DLL_MDB = $(BUILD_DIR)/PaintDotNet.Resources.dll.mdb
MICROSOFT_INK_DLL = $(BUILD_DIR)/Microsoft.Ink.dll


FILES = \
	AboutDialog.cs \
	ActionFlags.cs \
	Actions/AcquireFromScannerOrCameraAction.cs \
	Actions/CanvasSizeAction.cs \
	Actions/ClearHistoryAction.cs \
	Actions/ClearMruListAction.cs \
	Actions/CloseAllWorkspacesAction.cs \
	Actions/CloseWorkspaceAction.cs \
	Actions/CopyToClipboardAction.cs \
	Actions/CutAction.cs \
	Actions/DuplicateActiveLayerAction.cs \
	Actions/FlipLayerHorizontalAction.cs \
	Actions/FlipLayerVerticalAction.cs \
	Actions/HistoryFastForwardAction.cs \
	Actions/HistoryRedoAction.cs \
	Actions/HistoryRewindAction.cs \
	Actions/HistoryUndoAction.cs \
	Actions/ImportFromFileAction.cs \
	Actions/MoveActiveLayerDownAction.cs \
	Actions/MoveActiveLayerUpAction.cs \
	Actions/NewImageAction.cs \
	Actions/OpenActiveLayerPropertiesAction.cs \
	Actions/OpenFileAction.cs \
	Actions/PasteAction.cs \
	Actions/PasteInToNewImageAction.cs \
	Actions/PasteInToNewLayerAction.cs \
	Actions/PrintAction.cs \
	Actions/ResizeAction.cs \
	CanvasControl.cs \
	GradientInfo.cs \
	GradientType.cs \
	HistoryFunctions/FillSelectionFunction.cs \
	HistoryFunctions/MergeLayerDownFunction.cs \
	HistoryMementos/SelectionHistoryMemento.cs \
	Actions/SendFeedbackAction.cs \
	Actions/ZoomInAction.cs \
	Actions/ZoomOutAction.cs \
	Actions/ZoomToSelectionAction.cs \
	Actions/ZoomToWindowAction.cs \
	AnchorChooserControl.cs \
	AnchorEdge.cs \
	AppWorkspaceAction.cs \
	AssemblyInfo.cs \
	BrushInfo.cs \
	BrushPreviewRenderer.cs \
	BrushType.cs \
	CallbackWithProgressDialog.cs \
	CanvasSizeDialog.cs \
	ChooseToolDefaultsDialog.cs \
	ColorDisplayWidget.cs \
	ColorEventArgs.cs \
	ColorEventHandler.cs \
	ColorPickerClickBehavior.cs \
	ColorRectangleControl.cs \
	ColorsForm.cs \
	ColorWheel.cs \
	CommonAction.cs \
	CommonActionsStrip.cs \
	DocumentStrip.cs \
	DocumentWorkspaceAction.cs \
	DocumentWorkspace.cs \
	AppEnvironment.cs \
	HistoryFunction.cs \
	HistoryFunctionNonFatalException.cs \
	HistoryFunctionResult.cs \
	HistoryFunctions/AddNewBlankLayerFunction.cs \
	HistoryFunctions/CropToSelectionFunction.cs \
	HistoryFunctions/DeleteLayerFunction.cs \
	HistoryFunctions/DeselectFunction.cs \
	HistoryFunctions/EraseSelectionFunction.cs \
	HistoryFunctions/FlattenFunction.cs \
	HistoryFunctions/FlipDocumentFunction.cs \
	HistoryFunctions/FlipDocumentHorizontalFunction.cs \
	HistoryFunctions/FlipDocumentVerticalFunction.cs \
	HistoryFunctions/FlipLayerFunction.cs \
	HistoryFunctions/InvertSelectionFunction.cs \
	HistoryFunctions/RotateDocumentFunction.cs \
	HistoryFunctions/SelectAllFunction.cs \
	HistoryFunctions/SwapLayerFunction.cs \
	HistoryMementos/BitmapHistoryMemento.cs \
	HistoryMementos/CompoundHistoryMemento.cs \
	HistoryMementos/DeleteLayerHistoryMemento.cs \
	HistoryMementos/FlipLayerHistoryMemento.cs \
	HistoryMementos/LayerPropertyHistoryMemento.cs \
	HistoryMementos/MetaDataHistoryMemento.cs \
	HistoryMementos/NewLayerHistoryMemento.cs \
	HistoryMementos/NullHistoryMemento.cs \
	HistoryMementos/ReplaceDocumentHistoryMemento.cs \
	HistoryMementos/SwapLayerHistoryMemento.cs \
	HistoryMementos/ToolHistoryMemento.cs \
	IAlphaBlendingConfig.cs \
	IAntiAliasingConfig.cs \
	IBrushConfig.cs \
	IColorPickerConfig.cs \
	IGradientConfig.cs \
	IHistoryWorkspace.cs \
	IDocumentList.cs \
	IPenConfig.cs \
	IResamplingConfig.cs \
	IShapeTypeConfig.cs \
	IStatusBarProgress.cs \
	ITextConfig.cs \
	IToleranceConfig.cs \
	IToolChooser.cs \
	Menus/AdjustmentsMenu.cs \
	Menus/CheckForUpdatesMenuItem.cs \
	Menus/EditMenu.cs \
	Menus/EffectMenuBase.cs \
	Menus/EffectsMenu.cs \
	Menus/FileMenu.cs \
	Menus/HelpMenu.cs \
	Menus/ImageMenu.cs \
	Menus/LayersMenu.cs \
	Menus/PdnMainMenu.cs \
	Menus/ToolsMenu.cs \
	Menus/ViewMenu.cs \
	Menus/WindowMenu.cs \
	PaletteCollection.cs \
	PdnSettings.cs \
	PushNullToolMode.cs \
	SavePaletteDialog.cs \
	tools/CloneStampTool.cs \
	tools/ColorPickerTool.cs \
	tools/EllipseSelectTool.cs \
	tools/EllipseTool.cs \
	tools/EraserTool.cs \
	tools/FloodToolBase.cs \
	tools/FreeformShapeTool.cs \
	tools/GradientTool.cs \
	tools/LassoSelectTool.cs \
	tools/LineTool.cs \
	tools/MagicWandTool.cs \
	tools/MoveSelectionTool.cs \
	tools/MoveTool.cs \
	tools/MoveToolBase.cs \
	tools/PaintBrushTool.cs \
	tools/PaintBucketTool.cs \
	tools/PanTool.cs \
	tools/PencilTool.cs \
	tools/RecoloringTool.cs \
	tools/RectangleSelectTool.cs \
	tools/RectangleTool.cs \
	tools/RoundedRectangleTool.cs \
	tools/SelectionTool.cs \
	tools/ShapeTool.cs \
	tools/TextTool.cs \
	tools/ZoomTool.cs \
	UnsavedChangesDialog.cs \
	Updates/AbortedState.cs \
	Updates/CheckingState.cs \
	Updates/DoneState.cs \
	Updates/DownloadingState.cs \
	Updates/ErrorState.cs \
	Updates/ExtractingState.cs \
	Updates/INewVersionInfo.cs \
	Updates/InstallingState.cs \
	Updates/PrivateInput.cs \
	Updates/ReadyToCheckState.cs \
	Updates/ReadyToInstallState.cs \
	Updates/StartupState.cs \
	Updates/UpdateAvailableState.cs \
	Updates/UpdatesAction.cs \
	Updates/UpdatesDialog.cs \
	Updates/UpdatesOptionsDialog.cs \
	Updates/UpdatesState.cs \
	Updates/UpdatesStateMachine.cs \
	WorkspaceWidgets.cs \
	AppWorkspace.cs \
	ToolConfigStrip.cs \
	EnumValueEventArgs.cs \
	EnumValueEventHandler.cs \
	ExecutedHistoryMementoEventArgs.cs \
	ExecutedHistoryMementoEventHandler.cs \
	ExecutingHistoryMementoEventArgs.cs \
	ExecutingHistoryMementoEventHandler.cs \
	FileTypes.cs \
	FlipType.cs \
	FloatingToolForm.cs \
	FlowPanel.cs \
	FontInfo.cs \
	GraphicsPathWrapper.cs \
	HistoryMemento.cs \
	HistoryMementoData.cs \
	HistoryControl.cs \
	HistoryForm.cs \
	HistoryStack.cs \
	IconBox.cs \
	LayerControl.cs \
	LayerElement.cs \
	LayerEventArgs.cs \
	LayerEventHandler.cs \
	LayerForm.cs \
	LoadProgressDialog.cs \
	MainForm.cs \
	ToolsControl.cs \
	ToolsForm.cs \
	MostRecentFile.cs \
	MostRecentFiles.cs \
	MoveNubRenderer.cs \
	MoveNubShape.cs \
	NameEventArgs.cs \
	NameEventHandler.cs \
	NewFileDialog.cs \
	PdnMenuItem.cs \
	PdnStatusBar.cs \
	PdnToolBar.cs \
	PdnVersionInfo.cs \
	PdnVersionManifest.cs \
	PenInfo.cs \
	ProgressDialog.cs \
	ResizeDialog.cs \
	RotateNubRenderer.cs \
	RotateType.cs \
	SaveConfigDialog.cs \
	SaveProgressDialog.cs \
	SelectionRenderer.cs \
	ShapeDrawType.cs \
	SplashForm.cs \
	Startup.cs \
	SurfaceForClipboard.cs \
	TextAlignment.cs \
	ToleranceSliderControl.cs \
	Tool.cs \
	ToolBarConfigItems.cs \
	ToolChooserStrip.cs \
	ToolClickedEventArgs.cs \
	ToolClickedEventHandler.cs \
	ToolInfo.cs \
	WhichUserColor.cs \
	ZoomBasis.cs \
	ViewConfigStrip.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	readme.txt \
	HistoryForm.resx \
	LayerForm.resx \
	AboutDialog.resx \
	ChooseToolDefaultsDialog.resx \
	ColorsForm.resx \
	SavePaletteDialog.resx \
	UnsavedChangesDialog.resx \
	paintdotnet.in 

REFERENCES =  \
	ICSharpCode.SharpZipLib \
	System \
	System.Data \
	System.Drawing \
	System.Windows.Forms \
	System.Xml

DLL_REFERENCES = 

CLEANFILES += $(PROGRAMFILES) $(BINARIES) 

#Targets
all-local: $(ASSEMBLY) $(PROGRAMFILES) $(BINARIES)  $(top_srcdir)/config.make

$(PAINTDOTNET_SYSTEMLAYER_DLL): $(PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_DATA_DLL_MDB): $(PAINTDOTNET_DATA_DLL_MDB_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_STYLUSREADER_DLL_MDB): $(PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_EFFECTS_DLL_MDB): $(PAINTDOTNET_EFFECTS_DLL_MDB_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET): paintdotnet
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'
	chmod u+x '$@'

$(PAINTDOTNET_STYLUSREADER_DLL): $(PAINTDOTNET_STYLUSREADER_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_RESOURCES_DLL): $(PAINTDOTNET_RESOURCES_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PDNLIB_DLL_MDB): $(PDNLIB_DLL_MDB_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_DATA_DLL): $(PAINTDOTNET_DATA_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PDNLIB_DLL): $(PDNLIB_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_EFFECTS_DLL): $(PAINTDOTNET_EFFECTS_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB): $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_RESOURCES_DLL_MDB): $(PAINTDOTNET_RESOURCES_DLL_MDB_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(MICROSOFT_INK_DLL): $(MICROSOFT_INK_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'



paintdotnet: paintdotnet.in $(top_srcdir)/config.make
	sed -e "s,@prefix@,$(prefix)," -e "s,@PACKAGE@,$(PACKAGE)," < paintdotnet.in > paintdotnet


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


install-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(PAINTDOTNET_SYSTEMLAYER_DLL) $(PAINTDOTNET_DATA_DLL_MDB) $(PAINTDOTNET_STYLUSREADER_DLL_MDB) $(PAINTDOTNET_EFFECTS_DLL_MDB) $(PAINTDOTNET) $(PAINTDOTNET_STYLUSREADER_DLL) $(PAINTDOTNET_RESOURCES_DLL) $(PDNLIB_DLL_MDB) $(PAINTDOTNET_DATA_DLL) $(PDNLIB_DLL) $(PAINTDOTNET_EFFECTS_DLL) $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) $(PAINTDOTNET_RESOURCES_DLL_MDB) $(MICROSOFT_INK_DLL)
	make pre-install-local-hook prefix=$(prefix)
	mkdir -p $(prefix)/lib/$(PACKAGE)
	cp $(ASSEMBLY) $(ASSEMBLY_MDB) $(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL)' || cp $(PAINTDOTNET_SYSTEMLAYER_DLL) $(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_DATA_DLL_MDB)' || cp $(PAINTDOTNET_DATA_DLL_MDB) $(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL_MDB)' || cp $(PAINTDOTNET_STYLUSREADER_DLL_MDB) $(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_EFFECTS_DLL_MDB)' || cp $(PAINTDOTNET_EFFECTS_DLL_MDB) $(prefix)/lib/$(PACKAGE)
	mkdir -p $(prefix)/bin
	test -z '$(PAINTDOTNET)' || cp $(PAINTDOTNET) $(prefix)/bin
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL)' || cp $(PAINTDOTNET_STYLUSREADER_DLL) $(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_RESOURCES_DLL)' || cp $(PAINTDOTNET_RESOURCES_DLL) $(prefix)/lib/$(PACKAGE)
	test -z '$(PDNLIB_DLL_MDB)' || cp $(PDNLIB_DLL_MDB) $(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_DATA_DLL)' || cp $(PAINTDOTNET_DATA_DLL) $(prefix)/lib/$(PACKAGE)
	test -z '$(PDNLIB_DLL)' || cp $(PDNLIB_DLL) $(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_EFFECTS_DLL)' || cp $(PAINTDOTNET_EFFECTS_DLL) $(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB)' || cp $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) $(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_RESOURCES_DLL_MDB)' || cp $(PAINTDOTNET_RESOURCES_DLL_MDB) $(prefix)/lib/$(PACKAGE)
	test -z '$(MICROSOFT_INK_DLL)' || cp $(MICROSOFT_INK_DLL) $(prefix)/lib/$(PACKAGE)
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(PAINTDOTNET_SYSTEMLAYER_DLL) $(PAINTDOTNET_DATA_DLL_MDB) $(PAINTDOTNET_STYLUSREADER_DLL_MDB) $(PAINTDOTNET_EFFECTS_DLL_MDB) $(PAINTDOTNET) $(PAINTDOTNET_STYLUSREADER_DLL) $(PAINTDOTNET_RESOURCES_DLL) $(PDNLIB_DLL_MDB) $(PAINTDOTNET_DATA_DLL) $(PDNLIB_DLL) $(PAINTDOTNET_EFFECTS_DLL) $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) $(PAINTDOTNET_RESOURCES_DLL_MDB) $(MICROSOFT_INK_DLL)
	make pre-uninstall-local-hook prefix=$(prefix)
	rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY))
	test -z '$(ASSEMBLY_MDB)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY_MDB))
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_SYSTEMLAYER_DLL))
	test -z '$(PAINTDOTNET_DATA_DLL_MDB)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_DATA_DLL_MDB))
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL_MDB)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_STYLUSREADER_DLL_MDB))
	test -z '$(PAINTDOTNET_EFFECTS_DLL_MDB)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_EFFECTS_DLL_MDB))
	test -z '$(PAINTDOTNET)' || rm -f $(prefix)/bin/$(notdir $(PAINTDOTNET))
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_STYLUSREADER_DLL))
	test -z '$(PAINTDOTNET_RESOURCES_DLL)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_RESOURCES_DLL))
	test -z '$(PDNLIB_DLL_MDB)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(PDNLIB_DLL_MDB))
	test -z '$(PAINTDOTNET_DATA_DLL)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_DATA_DLL))
	test -z '$(PDNLIB_DLL)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(PDNLIB_DLL))
	test -z '$(PAINTDOTNET_EFFECTS_DLL)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_EFFECTS_DLL))
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB))
	test -z '$(PAINTDOTNET_RESOURCES_DLL_MDB)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_RESOURCES_DLL_MDB))
	test -z '$(MICROSOFT_INK_DLL)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(MICROSOFT_INK_DLL))
	make post-uninstall-local-hook prefix=$(prefix)
