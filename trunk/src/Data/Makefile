
EXTRA_DIST =  Makefile

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=..

include $(top_srcdir)/Makefile.include
include $(top_srcdir)/config.make

ifeq ($(CONFIG),DEBUG_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize- -debug "-define:DEBUG;TRACE"

ASSEMBLY = bin/Debug/PaintDotNet.Data.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../PdnLib/bin/Debug/PdnLib.dll \
	../Resources/bin/Debug/PaintDotNet.Resources.dll \
	../SystemLayer/bin/Debug/PaintDotNet.SystemLayer.dll
BUILD_DIR = bin/Debug/

PAINTDOTNET_RESOURCES_DLL_MDB_SOURCE=../Resources/bin/Debug/PaintDotNet.Resources.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE=../StylusReader/bin/Debug/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_SOURCE=../StylusReader/bin/Debug/PaintDotNet.StylusReader.dll
PDNLIB_DLL_MDB_SOURCE=../PdnLib/bin/Debug/PdnLib.dll.mdb
PAINTDOTNET_RESOURCES_DLL_SOURCE=../Resources/bin/Debug/PaintDotNet.Resources.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE=../SystemLayer/bin/Debug/PaintDotNet.SystemLayer.dll.mdb
PDNLIB_DLL_SOURCE=../PdnLib/bin/Debug/PdnLib.dll
MICROSOFT_INK_DLL_SOURCE=../Microsoft.Ink/Microsoft.Ink.dll
PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE=../SystemLayer/bin/Debug/PaintDotNet.SystemLayer.dll

endif

ifeq ($(CONFIG),RELEASE_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize+ -debug -define:DEBUG "-define:TRACE"

ASSEMBLY = bin/Release/PaintDotNet.Data.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../PdnLib/bin/Release/PdnLib.dll \
	../Resources/bin/Release/PaintDotNet.Resources.dll \
	../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll
BUILD_DIR = bin/Release/

PAINTDOTNET_RESOURCES_DLL_MDB_SOURCE=../Resources/bin/Release/PaintDotNet.Resources.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll
PDNLIB_DLL_MDB_SOURCE=../PdnLib/bin/Release/PdnLib.dll.mdb
PAINTDOTNET_RESOURCES_DLL_SOURCE=../Resources/bin/Release/PaintDotNet.Resources.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE=../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll.mdb
PDNLIB_DLL_SOURCE=../PdnLib/bin/Release/PdnLib.dll
MICROSOFT_INK_DLL_SOURCE=../Microsoft.Ink/Microsoft.Ink.dll
PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE=../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll

endif

ifeq ($(CONFIG),RELEASE_AND_PACKAGE_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize+ -debug -define:DEBUG "-define:TRACE"

ASSEMBLY = bin/Release/PaintDotNet.Data.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../PdnLib/bin/Release/PdnLib.dll \
	../Resources/bin/Release/PaintDotNet.Resources.dll \
	../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll
BUILD_DIR = bin/Release/

PAINTDOTNET_RESOURCES_DLL_MDB_SOURCE=../Resources/bin/Release/PaintDotNet.Resources.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll
PDNLIB_DLL_MDB_SOURCE=../PdnLib/bin/Release/PdnLib.dll.mdb
PAINTDOTNET_RESOURCES_DLL_SOURCE=../Resources/bin/Release/PaintDotNet.Resources.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE=../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll.mdb
PDNLIB_DLL_SOURCE=../PdnLib/bin/Release/PdnLib.dll
MICROSOFT_INK_DLL_SOURCE=../Microsoft.Ink/Microsoft.Ink.dll
PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE=../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll

endif


PROGRAMFILES = \
	$(PAINTDOTNET_RESOURCES_DLL_MDB) \
	$(PAINTDOTNET_STYLUSREADER_DLL_MDB) \
	$(PAINTDOTNET_STYLUSREADER_DLL) \
	$(PDNLIB_DLL_MDB) \
	$(PAINTDOTNET_RESOURCES_DLL) \
	$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) \
	$(PDNLIB_DLL) \
	$(MICROSOFT_INK_DLL) \
	$(PAINTDOTNET_SYSTEMLAYER_DLL)  

LINUX_PKGCONFIG = \
	$(DATA_PC)  



PAINTDOTNET_RESOURCES_DLL_MDB = $(BUILD_DIR)/PaintDotNet.Resources.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_MDB = $(BUILD_DIR)/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL = $(BUILD_DIR)/PaintDotNet.StylusReader.dll
DATA_PC = $(BUILD_DIR)/data.pc
PDNLIB_DLL_MDB = $(BUILD_DIR)/PdnLib.dll.mdb
PAINTDOTNET_RESOURCES_DLL = $(BUILD_DIR)/PaintDotNet.Resources.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB = $(BUILD_DIR)/PaintDotNet.SystemLayer.dll.mdb
PDNLIB_DLL = $(BUILD_DIR)/PdnLib.dll
MICROSOFT_INK_DLL = $(BUILD_DIR)/Microsoft.Ink.dll
PAINTDOTNET_SYSTEMLAYER_DLL = $(BUILD_DIR)/PaintDotNet.SystemLayer.dll

FILES = \
	AssemblyInfo.cs \
	BitmapLayer.cs \
	BitmapLayerPropertiesDialog.cs \
	BmpFileType.cs \
	Document.cs \
	DocumentView.cs \
	Exif.cs \
	ExifTagID.cs \
	ExifTagType.cs \
	FileType.cs \
	FileTypeCollection.cs \
	GdiPlusFileType.cs \
	GifFileType.cs \
	GifSaveConfigToken.cs \
	GifSaveConfigWidget.cs \
	IFileTypeFactory.cs \
	JpegFileType.cs \
	JpegSaveConfigToken.cs \
	JpegSaveConfigWidget.cs \
	Layer.cs \
	LayerList.cs \
	LayerPropertiesDialog.cs \
	MetaData.cs \
	NoSaveConfigWidget.cs \
	PdnFileType.cs \
	PdnFileTypes.cs \
	PngFileType.cs \
	Quantize/OctreeQuantizer.cs \
	Quantize/PaletteQuantizer.cs \
	Quantize/PaletteTable.cs \
	Quantize/Quantizer.cs \
	SaveConfigToken.cs \
	SaveConfigWidget.cs \
	TgaFileType.cs \
	TgaSaveConfigToken.cs \
	TgaSaveConfigWidget.cs \
	UserBlendOp.cs \
	UserBlendOps.cs \
	UserBlendOps.Generated.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	UserBlendOps.Generated.H.cs \
	GeneratedCodeWarning.h \
	data.pc.in 

REFERENCES =  \
	System \
	System.Drawing \
	System.Runtime.Serialization.Formatters.Soap \
	System.Windows.Forms \
	System.Xml

DLL_REFERENCES = 

CLEANFILES += $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

#Targets
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG)  $(top_srcdir)/config.make

#include $(srcdir)/custom-hooks.make



$(eval $(call emit-deploy-target,PAINTDOTNET_RESOURCES_DLL_MDB))
$(eval $(call emit-deploy-target,PAINTDOTNET_STYLUSREADER_DLL_MDB))
$(eval $(call emit-deploy-target,PAINTDOTNET_STYLUSREADER_DLL))
$(eval $(call emit-deploy-wrapper,DATA_PC,data.pc))
$(eval $(call emit-deploy-target,PDNLIB_DLL_MDB))
$(eval $(call emit-deploy-target,PAINTDOTNET_RESOURCES_DLL))
$(eval $(call emit-deploy-target,PAINTDOTNET_SYSTEMLAYER_DLL_MDB))
$(eval $(call emit-deploy-target,PDNLIB_DLL))
$(eval $(call emit-deploy-target,MICROSOFT_INK_DLL))
$(eval $(call emit-deploy-target,PAINTDOTNET_SYSTEMLAYER_DLL))


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

install-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(PAINTDOTNET_RESOURCES_DLL_MDB) $(PAINTDOTNET_STYLUSREADER_DLL_MDB) $(PAINTDOTNET_STYLUSREADER_DLL) $(DATA_PC) $(PDNLIB_DLL_MDB) $(PAINTDOTNET_RESOURCES_DLL) $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) $(PDNLIB_DLL) $(MICROSOFT_INK_DLL) $(PAINTDOTNET_SYSTEMLAYER_DLL)
	make pre-install-local-hook prefix=$(prefix)
	mkdir -p $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	cp $(ASSEMBLY) $(ASSEMBLY_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_RESOURCES_DLL_MDB)' || cp $(PAINTDOTNET_RESOURCES_DLL_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL_MDB)' || cp $(PAINTDOTNET_STYLUSREADER_DLL_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL)' || cp $(PAINTDOTNET_STYLUSREADER_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	mkdir -p $(DESTDIR)$(prefix)/lib/pkgconfig
	test -z '$(DATA_PC)' || cp $(DATA_PC) $(DESTDIR)$(prefix)/lib/pkgconfig
	test -z '$(PDNLIB_DLL_MDB)' || cp $(PDNLIB_DLL_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_RESOURCES_DLL)' || cp $(PAINTDOTNET_RESOURCES_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB)' || cp $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PDNLIB_DLL)' || cp $(PDNLIB_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(MICROSOFT_INK_DLL)' || cp $(MICROSOFT_INK_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL)' || cp $(PAINTDOTNET_SYSTEMLAYER_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(PAINTDOTNET_RESOURCES_DLL_MDB) $(PAINTDOTNET_STYLUSREADER_DLL_MDB) $(PAINTDOTNET_STYLUSREADER_DLL) $(DATA_PC) $(PDNLIB_DLL_MDB) $(PAINTDOTNET_RESOURCES_DLL) $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) $(PDNLIB_DLL) $(MICROSOFT_INK_DLL) $(PAINTDOTNET_SYSTEMLAYER_DLL)
	make pre-uninstall-local-hook prefix=$(prefix)
	rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY))
	test -z '$(ASSEMBLY_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY_MDB))
	test -z '$(PAINTDOTNET_RESOURCES_DLL_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_RESOURCES_DLL_MDB))
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_STYLUSREADER_DLL_MDB))
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_STYLUSREADER_DLL))
	test -z '$(DATA_PC)' || rm -f $(DESTDIR)$(prefix)/lib/pkgconfig/$(notdir $(DATA_PC))
	test -z '$(PDNLIB_DLL_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PDNLIB_DLL_MDB))
	test -z '$(PAINTDOTNET_RESOURCES_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_RESOURCES_DLL))
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB))
	test -z '$(PDNLIB_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PDNLIB_DLL))
	test -z '$(MICROSOFT_INK_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(MICROSOFT_INK_DLL))
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_SYSTEMLAYER_DLL))
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make
