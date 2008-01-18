
EXTRA_DIST =  Makefile

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=..

include $(top_srcdir)/Makefile.include
include $(top_srcdir)/config.make

ifeq ($(CONFIG),DEBUG_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize- -debug "-define:DEBUG;TRACE"

ASSEMBLY = bin/Debug/PaintDotNet.SystemLayer.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../StylusReader/bin/Debug/PaintDotNet.StylusReader.dll
BUILD_DIR = bin/Debug/

PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE=../StylusReader/bin/Debug/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_SOURCE=../StylusReader/bin/Debug/PaintDotNet.StylusReader.dll
MICROSOFT_INK_DLL_SOURCE=../Microsoft.Ink/Microsoft.Ink.dll

endif

ifeq ($(CONFIG),RELEASE_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize+ -debug -define:DEBUG "-define:TRACE"

ASSEMBLY = bin/Release/PaintDotNet.SystemLayer.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../StylusReader/bin/Release/PaintDotNet.StylusReader.dll
BUILD_DIR = bin/Release/

PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll
MICROSOFT_INK_DLL_SOURCE=../Microsoft.Ink/Microsoft.Ink.dll

endif

ifeq ($(CONFIG),RELEASE_AND_PACKAGE_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -unsafe -warn:4 -optimize+ -debug -define:DEBUG "-define:TRACE"

ASSEMBLY = bin/Release/PaintDotNet.SystemLayer.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../StylusReader/bin/Release/PaintDotNet.StylusReader.dll
BUILD_DIR = bin/Release/

PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll
MICROSOFT_INK_DLL_SOURCE=../Microsoft.Ink/Microsoft.Ink.dll

endif


PROGRAMFILES = \
	$(PAINTDOTNET_STYLUSREADER_DLL_MDB) \
	$(PAINTDOTNET_STYLUSREADER_DLL) \
	$(MICROSOFT_INK_DLL)  

LINUX_PKGCONFIG = \
	$(SYSTEMLAYER_PC)  



PAINTDOTNET_STYLUSREADER_DLL_MDB = $(BUILD_DIR)/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL = $(BUILD_DIR)/PaintDotNet.StylusReader.dll
SYSTEMLAYER_PC = $(BUILD_DIR)/systemlayer.pc
MICROSOFT_INK_DLL = $(BUILD_DIR)/Microsoft.Ink.dll

FILES = \
	AssemblyInfo.cs \
	FileSystem.cs \
	Fonts.cs \
	FontSmoothing.cs \
	FormEx.cs \
	SingleInstaceManager.cs \
	ISimpleCollection.cs \
	IInkHooks.cs \
	Ink.cs \
	Memory.cs \
	MenuStripEx.cs \
	NativeConstants.cs \
	NativeDelegates.cs \
	NativeMethods.cs \
	NativeStructs.cs \
	Network.cs \
	NullGraphics.cs \
	OS.cs \
	OSType.cs \
	PdnGraphics.cs \
	ProcessorArchitecture.cs \
	Processor.cs \
	PropertyItem2.cs \
	RealParentWndProcDelegate.cs \
	SafeNativeMethods.cs \
	ScanningAndPrinting.cs \
	ScanResult.cs \
	ScrollPanel.cs \
	Security.cs \
	SerializationFallbackBinder.cs \
	Settings.cs \
	Shell.cs \
	ThreadBackground.cs \
	ThreadBackgroundFlags.cs \
	Timing.cs \
	ToolStripEx.cs \
	Tracing.cs \
	UI.cs \
	UserSessions.cs \
	WaitHandleArray.cs 

DATA_FILES = 

RESOURCES = \
	PropertyItem.png,PaintDotNet.SystemLayer.PropertyItem.png 

EXTRAS = \
	systemlayer.pc.in 

REFERENCES =  \
	System \
	System.Data \
	System.Drawing \
	System.Windows.Forms \
	System.Xml \
	Mono.Posix

DLL_REFERENCES = 

CLEANFILES += $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

#Targets
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG)  $(top_srcdir)/config.make

#include $(srcdir)/custom-hooks.make



$(eval $(call emit-deploy-target,PAINTDOTNET_STYLUSREADER_DLL_MDB))
$(eval $(call emit-deploy-target,PAINTDOTNET_STYLUSREADER_DLL))
$(eval $(call emit-deploy-wrapper,SYSTEMLAYER_PC,systemlayer.pc))
$(eval $(call emit-deploy-target,MICROSOFT_INK_DLL))


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

install-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(PAINTDOTNET_STYLUSREADER_DLL_MDB) $(PAINTDOTNET_STYLUSREADER_DLL) $(SYSTEMLAYER_PC) $(MICROSOFT_INK_DLL)
	make pre-install-local-hook prefix=$(prefix)
	mkdir -p $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	cp $(ASSEMBLY) $(ASSEMBLY_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL_MDB)' || cp $(PAINTDOTNET_STYLUSREADER_DLL_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL)' || cp $(PAINTDOTNET_STYLUSREADER_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	mkdir -p $(DESTDIR)$(prefix)/lib/pkgconfig
	test -z '$(SYSTEMLAYER_PC)' || cp $(SYSTEMLAYER_PC) $(DESTDIR)$(prefix)/lib/pkgconfig
	test -z '$(MICROSOFT_INK_DLL)' || cp $(MICROSOFT_INK_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(PAINTDOTNET_STYLUSREADER_DLL_MDB) $(PAINTDOTNET_STYLUSREADER_DLL) $(SYSTEMLAYER_PC) $(MICROSOFT_INK_DLL)
	make pre-uninstall-local-hook prefix=$(prefix)
	rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY))
	test -z '$(ASSEMBLY_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY_MDB))
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_STYLUSREADER_DLL_MDB))
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_STYLUSREADER_DLL))
	test -z '$(SYSTEMLAYER_PC)' || rm -f $(DESTDIR)$(prefix)/lib/pkgconfig/$(notdir $(SYSTEMLAYER_PC))
	test -z '$(MICROSOFT_INK_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(MICROSOFT_INK_DLL))
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make
