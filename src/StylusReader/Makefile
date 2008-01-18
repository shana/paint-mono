
EXTRA_DIST =  Makefile

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=..

include $(top_srcdir)/Makefile.include
include $(top_srcdir)/config.make

ifeq ($(CONFIG),DEBUG_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG;TRACE"

ASSEMBLY = bin/Debug/PaintDotNet.StylusReader.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Debug/

MICROSOFT_INK_DLL_SOURCE=../Microsoft.Ink/Microsoft.Ink.dll

endif

ifeq ($(CONFIG),RELEASE_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ -debug -define:DEBUG "-define:TRACE"

ASSEMBLY = bin/Release/PaintDotNet.StylusReader.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release/

MICROSOFT_INK_DLL_SOURCE=../Microsoft.Ink/Microsoft.Ink.dll

endif

ifeq ($(CONFIG),RELEASE_AND_PACKAGE_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ -debug -define:DEBUG "-define:TRACE"

ASSEMBLY = bin/Release/PaintDotNet.StylusReader.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release/

MICROSOFT_INK_DLL_SOURCE=../Microsoft.Ink/Microsoft.Ink.dll

endif


PROGRAMFILES = \
	$(MICROSOFT_INK_DLL)  

LINUX_PKGCONFIG = \
	$(STYLUSREADER_PC)  



STYLUSREADER_PC = $(BUILD_DIR)/stylusreader.pc
MICROSOFT_INK_DLL = $(BUILD_DIR)/Microsoft.Ink.dll

FILES = \
	AssemblyInfo.cs \
	IStylusReaderHooks.cs \
	StylusAsyncPlugin.cs \
	StylusReader.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	stylusreader.pc.in 

REFERENCES =  \
	System \
	System.Drawing \
	System.Windows.Forms

DLL_REFERENCES =  \
	../Microsoft.Ink/Microsoft.Ink.dll

CLEANFILES += $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

#Targets
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG)  $(top_srcdir)/config.make

#include $(srcdir)/custom-hooks.make



$(eval $(call emit-deploy-wrapper,STYLUSREADER_PC,stylusreader.pc))
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

install-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(STYLUSREADER_PC) $(MICROSOFT_INK_DLL)
	make pre-install-local-hook prefix=$(prefix)
	mkdir -p $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	cp $(ASSEMBLY) $(ASSEMBLY_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	mkdir -p $(DESTDIR)$(prefix)/lib/pkgconfig
	test -z '$(STYLUSREADER_PC)' || cp $(STYLUSREADER_PC) $(DESTDIR)$(prefix)/lib/pkgconfig
	test -z '$(MICROSOFT_INK_DLL)' || cp $(MICROSOFT_INK_DLL) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(STYLUSREADER_PC) $(MICROSOFT_INK_DLL)
	make pre-uninstall-local-hook prefix=$(prefix)
	rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY))
	test -z '$(ASSEMBLY_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY_MDB))
	test -z '$(STYLUSREADER_PC)' || rm -f $(DESTDIR)$(prefix)/lib/pkgconfig/$(notdir $(STYLUSREADER_PC))
	test -z '$(MICROSOFT_INK_DLL)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(MICROSOFT_INK_DLL))
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make
