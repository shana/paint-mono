
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

ASSEMBLY = bin/Debug/PaintDotNet.Effects.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../Data/bin/Debug/PaintDotNet.Data.dll \
	../PdnLib/bin/Debug/PdnLib.dll \
	../Resources/bin/Debug/PaintDotNet.Resources.dll \
	../SystemLayer/bin/Debug/PaintDotNet.SystemLayer.dll
BUILD_DIR = bin/Debug/

PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE=../SystemLayer/bin/Debug/PaintDotNet.SystemLayer.dll
PAINTDOTNET_DATA_DLL_MDB_SOURCE=../Data/bin/Debug/PaintDotNet.Data.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE=../StylusReader/bin/Debug/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_SOURCE=../StylusReader/bin/Debug/PaintDotNet.StylusReader.dll
PAINTDOTNET_RESOURCES_DLL_SOURCE=../Resources/bin/Debug/PaintDotNet.Resources.dll
PDNLIB_DLL_MDB_SOURCE=../PdnLib/bin/Debug/PdnLib.dll.mdb
PAINTDOTNET_DATA_DLL_SOURCE=../Data/bin/Debug/PaintDotNet.Data.dll
PDNLIB_DLL_SOURCE=../PdnLib/bin/Debug/PdnLib.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE=../SystemLayer/bin/Debug/PaintDotNet.SystemLayer.dll.mdb
PAINTDOTNET_RESOURCES_DLL_MDB_SOURCE=../Resources/bin/Debug/PaintDotNet.Resources.dll.mdb
MICROSOFT_INK_DLL_SOURCE=../Microsoft.Ink/Microsoft.Ink.dll

endif

ifeq ($(CONFIG),RELEASE_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize+ -debug -define:DEBUG "-define:TRACE"

ASSEMBLY = bin/Release/PaintDotNet.Effects.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../Data/bin/Release/PaintDotNet.Data.dll \
	../PdnLib/bin/Release/PdnLib.dll \
	../Resources/bin/Release/PaintDotNet.Resources.dll \
	../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll
BUILD_DIR = bin/Release/

PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE=../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll
PAINTDOTNET_DATA_DLL_MDB_SOURCE=../Data/bin/Release/PaintDotNet.Data.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll
PAINTDOTNET_RESOURCES_DLL_SOURCE=../Resources/bin/Release/PaintDotNet.Resources.dll
PDNLIB_DLL_MDB_SOURCE=../PdnLib/bin/Release/PdnLib.dll.mdb
PAINTDOTNET_DATA_DLL_SOURCE=../Data/bin/Release/PaintDotNet.Data.dll
PDNLIB_DLL_SOURCE=../PdnLib/bin/Release/PdnLib.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE=../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll.mdb
PAINTDOTNET_RESOURCES_DLL_MDB_SOURCE=../Resources/bin/Release/PaintDotNet.Resources.dll.mdb
MICROSOFT_INK_DLL_SOURCE=../Microsoft.Ink/Microsoft.Ink.dll

endif

ifeq ($(CONFIG),RELEASE_AND_PACKAGE_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize+ -debug -define:DEBUG "-define:TRACE"

ASSEMBLY = bin/Release/PaintDotNet.Effects.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../Data/bin/Release/PaintDotNet.Data.dll \
	../PdnLib/bin/Release/PdnLib.dll \
	../Resources/bin/Release/PaintDotNet.Resources.dll \
	../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll
BUILD_DIR = bin/Release/

PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE=../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll
PAINTDOTNET_DATA_DLL_MDB_SOURCE=../Data/bin/Release/PaintDotNet.Data.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll
PAINTDOTNET_RESOURCES_DLL_SOURCE=../Resources/bin/Release/PaintDotNet.Resources.dll
PDNLIB_DLL_MDB_SOURCE=../PdnLib/bin/Release/PdnLib.dll.mdb
PAINTDOTNET_DATA_DLL_SOURCE=../Data/bin/Release/PaintDotNet.Data.dll
PDNLIB_DLL_SOURCE=../PdnLib/bin/Release/PdnLib.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE=../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll.mdb
PAINTDOTNET_RESOURCES_DLL_MDB_SOURCE=../Resources/bin/Release/PaintDotNet.Resources.dll.mdb
MICROSOFT_INK_DLL_SOURCE=../Microsoft.Ink/Microsoft.Ink.dll

endif


PROGRAMFILES = \
	$(PAINTDOTNET_SYSTEMLAYER_DLL) \
	$(PAINTDOTNET_DATA_DLL_MDB) \
	$(PAINTDOTNET_STYLUSREADER_DLL_MDB) \
	$(PAINTDOTNET_STYLUSREADER_DLL) \
	$(PAINTDOTNET_RESOURCES_DLL) \
	$(PDNLIB_DLL_MDB) \
	$(PAINTDOTNET_DATA_DLL) \
	$(PDNLIB_DLL) \
	$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) \
	$(PAINTDOTNET_RESOURCES_DLL_MDB) \
	$(MICROSOFT_INK_DLL)  

LINUX_PKGCONFIG = \
	$(EFFECTS_PC)  



PAINTDOTNET_SYSTEMLAYER_DLL = $(BUILD_DIR)/PaintDotNet.SystemLayer.dll
PAINTDOTNET_DATA_DLL_MDB = $(BUILD_DIR)/PaintDotNet.Data.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_MDB = $(BUILD_DIR)/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL = $(BUILD_DIR)/PaintDotNet.StylusReader.dll
EFFECTS_PC = $(BUILD_DIR)/effects.pc
PAINTDOTNET_RESOURCES_DLL = $(BUILD_DIR)/PaintDotNet.Resources.dll
PDNLIB_DLL_MDB = $(BUILD_DIR)/PdnLib.dll.mdb
PAINTDOTNET_DATA_DLL = $(BUILD_DIR)/PaintDotNet.Data.dll
PDNLIB_DLL = $(BUILD_DIR)/PdnLib.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB = $(BUILD_DIR)/PaintDotNet.SystemLayer.dll.mdb
PAINTDOTNET_RESOURCES_DLL_MDB = $(BUILD_DIR)/PaintDotNet.Resources.dll.mdb
MICROSOFT_INK_DLL = $(BUILD_DIR)/Microsoft.Ink.dll


FILES = \
	AddNoiseEffect.cs \
	AmountEffectConfigDialog.cs \
	AmountEffectConfigToken.cs \
	AngleChooserConfigDialog.cs \
	AngleChooserConfigToken.cs \
	AssemblyInfo.cs \
	AutoLevelEffect.cs \
	BackgroundEffectRenderer.cs \
	BlurEffect.cs \
	BrightnessAndContrastAdjustment.cs \
	BrightnessAndContrastAdjustmentConfigDialog.cs \
	BrightnessAndContrastAdjustmentConfigToken.cs \
	BulgeEffect.cs \
	CloudsEffect.cs \
	ColorDifferenceEffect.cs \
	ConvolutionFilterEffect.cs \
	CloudsEffectConfigDialog.cs \
	CloudsEffectConfigToken.cs \
	CurvesEffect.cs \
	CurvesEffectConfigDialog.cs \
	CurvesEffectConfigToken.cs \
	DesaturateEffect.cs \
	EdgeDetectConfigDialog.cs \
	EdgeDetectConfigToken.cs \
	EdgeDetectEffect.cs \
	Effect.cs \
	EffectCategory.cs \
	EffectCategoryAttribute.cs \
	EffectConfigDialog.cs \
	EffectConfigToken.cs \
	EffectDirectives.cs \
	EffectEnvironmentParameters.cs \
	EffectsCollection.cs \
	EffectTypeHint.cs \
	EffectTypeHintAttribute.cs \
	EmbossEffect.cs \
	EmbossEffectConfigDialog.cs \
	EmbossEffectConfigToken.cs \
	FrostedGlassEffect.cs \
	GlowEffect.cs \
	HueAndSaturationAdjustment.cs \
	IConfigurableEffect.cs \
	InvertColorsEffect.cs \
	LevelsEffect.cs \
	LevelsEffectConfigDialog.cs \
	LevelsEffectConfigToken.cs \
	LocalHistogramEffect.cs \
	MedianEffect.cs \
	MotionBlurEffect.cs \
	MotionBlurEffectConfigDialog.cs \
	MotionBlurEffectConfigToken.cs \
	OilPaintingEffect.cs \
	OutlineEffect.cs \
	PanControl.cs \
	PixelateEffect.cs \
	PolarInversionEffect.cs \
	RadialBlurEffect.cs \
	RedEyeRemoveEffect.cs \
	RedEyeRemoveEffectDialog.cs \
	ReliefEffect.cs \
	ReliefEffectConfigDialog.cs \
	ReliefEffectConfigToken.cs \
	RollControl.cs \
	RotateZoomEffect.cs \
	RotateZoomEffectConfigDialog.cs \
	RotateZoomEffectConfigToken.cs \
	SepiaEffect.cs \
	SharpenEffect.cs \
	ThreeAmountsConfigDialog.cs \
	ThreeAmountsConfigToken.cs \
	TileEffect.cs \
	TwistEffect.cs \
	TwoAmountsConfigDialog.cs \
	TwoAmountsConfigToken.cs \
	UnfocusEffect.cs \
	ZoomBlurEffect.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	effects.pc.in 

REFERENCES =  \
	System \
	System.Data \
	System.Drawing \
	System.Windows.Forms \
	System.Xml

DLL_REFERENCES = 

CLEANFILES += $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

#Targets
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG)  $(top_srcdir)/config.make

$(PAINTDOTNET_SYSTEMLAYER_DLL): $(PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_DATA_DLL_MDB): $(PAINTDOTNET_DATA_DLL_MDB_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_STYLUSREADER_DLL_MDB): $(PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_STYLUSREADER_DLL): $(PAINTDOTNET_STYLUSREADER_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(EFFECTS_PC): effects.pc
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

$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB): $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_RESOURCES_DLL_MDB): $(PAINTDOTNET_RESOURCES_DLL_MDB_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(MICROSOFT_INK_DLL): $(MICROSOFT_INK_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'



effects.pc: effects.pc.in $(top_srcdir)/config.make
	sed -e "s,@prefix@,$(prefix)," -e "s,@PACKAGE@,$(PACKAGE)," < effects.pc.in > effects.pc


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


install-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(PAINTDOTNET_SYSTEMLAYER_DLL) $(PAINTDOTNET_DATA_DLL_MDB) $(PAINTDOTNET_STYLUSREADER_DLL_MDB) $(PAINTDOTNET_STYLUSREADER_DLL) $(EFFECTS_PC) $(PAINTDOTNET_RESOURCES_DLL) $(PDNLIB_DLL_MDB) $(PAINTDOTNET_DATA_DLL) $(PDNLIB_DLL) $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) $(PAINTDOTNET_RESOURCES_DLL_MDB) $(MICROSOFT_INK_DLL)
	make pre-install-local-hook prefix=$(prefix)
	mkdir -p $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	cp $(ASSEMBLY) $(ASSEMBLY_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL)' || cp $(PAINTDOTNET_SYSTEMLAYER_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_DATA_DLL_MDB)' || cp $(PAINTDOTNET_DATA_DLL_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL_MDB)' || cp $(PAINTDOTNET_STYLUSREADER_DLL_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL)' || cp $(PAINTDOTNET_STYLUSREADER_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	mkdir -p $(DESTDIR)$(prefix)/lib/pkgconfig
	test -z '$(EFFECTS_PC)' || cp $(EFFECTS_PC) $(DESTDIR)$(prefix)/lib/pkgconfig
	test -z '$(PAINTDOTNET_RESOURCES_DLL)' || cp $(PAINTDOTNET_RESOURCES_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PDNLIB_DLL_MDB)' || cp $(PDNLIB_DLL_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_DATA_DLL)' || cp $(PAINTDOTNET_DATA_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PDNLIB_DLL)' || cp $(PDNLIB_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB)' || cp $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_RESOURCES_DLL_MDB)' || cp $(PAINTDOTNET_RESOURCES_DLL_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(MICROSOFT_INK_DLL)' || cp $(MICROSOFT_INK_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(PAINTDOTNET_SYSTEMLAYER_DLL) $(PAINTDOTNET_DATA_DLL_MDB) $(PAINTDOTNET_STYLUSREADER_DLL_MDB) $(PAINTDOTNET_STYLUSREADER_DLL) $(EFFECTS_PC) $(PAINTDOTNET_RESOURCES_DLL) $(PDNLIB_DLL_MDB) $(PAINTDOTNET_DATA_DLL) $(PDNLIB_DLL) $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) $(PAINTDOTNET_RESOURCES_DLL_MDB) $(MICROSOFT_INK_DLL)
	make pre-uninstall-local-hook prefix=$(prefix)
	rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY))
	test -z '$(ASSEMBLY_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY_MDB))
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_SYSTEMLAYER_DLL))
	test -z '$(PAINTDOTNET_DATA_DLL_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_DATA_DLL_MDB))
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_STYLUSREADER_DLL_MDB))
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_STYLUSREADER_DLL))
	test -z '$(EFFECTS_PC)' || rm -f $(DESTDIR)$(prefix)/lib/pkgconfig/$(notdir $(EFFECTS_PC))
	test -z '$(PAINTDOTNET_RESOURCES_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_RESOURCES_DLL))
	test -z '$(PDNLIB_DLL_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PDNLIB_DLL_MDB))
	test -z '$(PAINTDOTNET_DATA_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_DATA_DLL))
	test -z '$(PDNLIB_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PDNLIB_DLL))
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB))
	test -z '$(PAINTDOTNET_RESOURCES_DLL_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_RESOURCES_DLL_MDB))
	test -z '$(MICROSOFT_INK_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(MICROSOFT_INK_DLL))
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make
