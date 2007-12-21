
EXTRA_DIST = Makefile

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=..

include $(top_srcdir)/Makefile.include
include $(top_srcdir)/config.make

#include $(srcdir)/custom-hooks.make

ifeq ($(CONFIG),DEBUG_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ -debug "-define:DEBUG;TRACE"

ASSEMBLY = bin/Debug/WiaProxy32.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../Resources/bin/Debug/PaintDotNet.Resources.dll \
	../SystemLayer/bin/Debug/PaintDotNet.SystemLayer.dll
BUILD_DIR = bin/Debug/

PAINTDOTNET_RESOURCES_DLL_MDB_SOURCE=../Resources/bin/Debug/PaintDotNet.Resources.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE=../StylusReader/bin/Debug/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_SOURCE=../StylusReader/bin/Debug/PaintDotNet.StylusReader.dll
PAINTDOTNET_RESOURCES_DLL_SOURCE=../Resources/bin/Debug/PaintDotNet.Resources.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE=../SystemLayer/bin/Debug/PaintDotNet.SystemLayer.dll.mdb
INTEROP_WIA_DLL_SOURCE=../Interop.WIA/Interop.WIA.dll
MICROSOFT_INK_DLL_SOURCE=../Microsoft.Ink/Microsoft.Ink.dll
PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE=../SystemLayer/bin/Debug/PaintDotNet.SystemLayer.dll

endif

ifeq ($(CONFIG),RELEASE_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ -debug -define:DEBUG "-define:TRACE"

ASSEMBLY = bin/Release/WiaProxy32.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../Resources/bin/Release/PaintDotNet.Resources.dll \
	../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll
BUILD_DIR = bin/Release/

PAINTDOTNET_RESOURCES_DLL_MDB_SOURCE=../Resources/bin/Release/PaintDotNet.Resources.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll
PAINTDOTNET_RESOURCES_DLL_SOURCE=../Resources/bin/Release/PaintDotNet.Resources.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE=../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll.mdb
INTEROP_WIA_DLL_SOURCE=../Interop.WIA/Interop.WIA.dll
MICROSOFT_INK_DLL_SOURCE=../Microsoft.Ink/Microsoft.Ink.dll
PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE=../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll

endif

ifeq ($(CONFIG),RELEASE_AND_PACKAGE_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ -debug -define:DEBUG "-define:TRACE"

ASSEMBLY = bin/Release/WiaProxy32.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES =  \
	../Resources/bin/Release/PaintDotNet.Resources.dll \
	../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll
BUILD_DIR = bin/Release/

PAINTDOTNET_RESOURCES_DLL_MDB_SOURCE=../Resources/bin/Release/PaintDotNet.Resources.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_SOURCE=../StylusReader/bin/Release/PaintDotNet.StylusReader.dll
PAINTDOTNET_RESOURCES_DLL_SOURCE=../Resources/bin/Release/PaintDotNet.Resources.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE=../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll.mdb
INTEROP_WIA_DLL_SOURCE=../Interop.WIA/Interop.WIA.dll
MICROSOFT_INK_DLL_SOURCE=../Microsoft.Ink/Microsoft.Ink.dll
PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE=../SystemLayer/bin/Release/PaintDotNet.SystemLayer.dll

endif


PROGRAMFILES = \
	$(PAINTDOTNET_RESOURCES_DLL_MDB) \
	$(PAINTDOTNET_STYLUSREADER_DLL_MDB) \
	$(PAINTDOTNET_STYLUSREADER_DLL) \
	$(PAINTDOTNET_RESOURCES_DLL) \
	$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) \
	$(INTEROP_WIA_DLL) \
	$(MICROSOFT_INK_DLL) \
	$(PAINTDOTNET_SYSTEMLAYER_DLL)  

BINARIES = \
	$(WIAPROXY32)  



PAINTDOTNET_RESOURCES_DLL_MDB = $(BUILD_DIR)/PaintDotNet.Resources.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL_MDB = $(BUILD_DIR)/PaintDotNet.StylusReader.dll.mdb
PAINTDOTNET_STYLUSREADER_DLL = $(BUILD_DIR)/PaintDotNet.StylusReader.dll
PAINTDOTNET_RESOURCES_DLL = $(BUILD_DIR)/PaintDotNet.Resources.dll
PAINTDOTNET_SYSTEMLAYER_DLL_MDB = $(BUILD_DIR)/PaintDotNet.SystemLayer.dll.mdb
INTEROP_WIA_DLL = $(BUILD_DIR)/Interop.WIA.dll
WIAPROXY32 = $(BUILD_DIR)/wiaproxy32
MICROSOFT_INK_DLL = $(BUILD_DIR)/Microsoft.Ink.dll
PAINTDOTNET_SYSTEMLAYER_DLL = $(BUILD_DIR)/PaintDotNet.SystemLayer.dll


FILES = \
	WiaProxy32.cs \
	Properties/AssemblyInfo.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	wiaproxy32.in 

REFERENCES =  \
	System

DLL_REFERENCES =  \
	../Interop.WIA/Interop.WIA.dll

CLEANFILES += $(PROGRAMFILES) $(BINARIES) 

#Targets
all: $(ASSEMBLY) $(PROGRAMFILES) $(BINARIES)  $(top_srcdir)/config.make

$(PAINTDOTNET_RESOURCES_DLL_MDB): $(PAINTDOTNET_RESOURCES_DLL_MDB_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_STYLUSREADER_DLL_MDB): $(PAINTDOTNET_STYLUSREADER_DLL_MDB_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_STYLUSREADER_DLL): $(PAINTDOTNET_STYLUSREADER_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_RESOURCES_DLL): $(PAINTDOTNET_RESOURCES_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB): $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(INTEROP_WIA_DLL): $(INTEROP_WIA_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(WIAPROXY32): wiaproxy32
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'
	chmod u+x '$@'

$(MICROSOFT_INK_DLL): $(MICROSOFT_INK_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'

$(PAINTDOTNET_SYSTEMLAYER_DLL): $(PAINTDOTNET_SYSTEMLAYER_DLL_SOURCE)
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'



wiaproxy32: wiaproxy32.in $(top_srcdir)/config.make
	sed -e "s,@prefix@,$(prefix)," -e "s,@PACKAGE@,$(PACKAGE)," < wiaproxy32.in > wiaproxy32


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


install-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(PAINTDOTNET_RESOURCES_DLL_MDB) $(PAINTDOTNET_STYLUSREADER_DLL_MDB) $(PAINTDOTNET_STYLUSREADER_DLL) $(PAINTDOTNET_RESOURCES_DLL) $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) $(INTEROP_WIA_DLL) $(WIAPROXY32) $(MICROSOFT_INK_DLL) $(PAINTDOTNET_SYSTEMLAYER_DLL)
	make pre-install-local-hook prefix=$(prefix)
	mkdir -p $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	cp $(ASSEMBLY) $(ASSEMBLY_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_RESOURCES_DLL_MDB)' || cp $(PAINTDOTNET_RESOURCES_DLL_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL_MDB)' || cp $(PAINTDOTNET_STYLUSREADER_DLL_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL)' || cp $(PAINTDOTNET_STYLUSREADER_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_RESOURCES_DLL)' || cp $(PAINTDOTNET_RESOURCES_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB)' || cp $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(INTEROP_WIA_DLL)' || cp $(INTEROP_WIA_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	mkdir -p $(DESTDIR)$(prefix)/bin
	test -z '$(WIAPROXY32)' || cp $(WIAPROXY32) $(DESTDIR)$(prefix)/bin
	test -z '$(MICROSOFT_INK_DLL)' || cp $(MICROSOFT_INK_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL)' || cp $(PAINTDOTNET_SYSTEMLAYER_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(PAINTDOTNET_RESOURCES_DLL_MDB) $(PAINTDOTNET_STYLUSREADER_DLL_MDB) $(PAINTDOTNET_STYLUSREADER_DLL) $(PAINTDOTNET_RESOURCES_DLL) $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB) $(INTEROP_WIA_DLL) $(WIAPROXY32) $(MICROSOFT_INK_DLL) $(PAINTDOTNET_SYSTEMLAYER_DLL)
	make pre-uninstall-local-hook prefix=$(prefix)
	rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY))
	test -z '$(ASSEMBLY_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY_MDB))
	test -z '$(PAINTDOTNET_RESOURCES_DLL_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_RESOURCES_DLL_MDB))
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_STYLUSREADER_DLL_MDB))
	test -z '$(PAINTDOTNET_STYLUSREADER_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_STYLUSREADER_DLL))
	test -z '$(PAINTDOTNET_RESOURCES_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_RESOURCES_DLL))
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_SYSTEMLAYER_DLL_MDB))
	test -z '$(INTEROP_WIA_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(INTEROP_WIA_DLL))
	test -z '$(WIAPROXY32)' || rm -f $(DESTDIR)$(prefix)/bin/$(notdir $(WIAPROXY32))
	test -z '$(MICROSOFT_INK_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(MICROSOFT_INK_DLL))
	test -z '$(PAINTDOTNET_SYSTEMLAYER_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(PAINTDOTNET_SYSTEMLAYER_DLL))
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make
