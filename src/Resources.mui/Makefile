
EXTRA_DIST = $(srcdir)/custom-hooks.make Makefile

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=..

include $(top_srcdir)/Makefile.include
include $(top_srcdir)/config.make

ifeq ($(CONFIG),DEBUG_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG;TRACE"

ASSEMBLY = bin/Debug/Resources.mui.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = bin/Debug/


endif

ifeq ($(CONFIG),RELEASE_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ -debug -define:DEBUG "-define:TRACE"

ASSEMBLY = bin/Release/Resources.mui.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release/


endif

ifeq ($(CONFIG),RELEASE_AND_PACKAGE_ANY_CPU)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ -debug -define:DEBUG "-define:TRACE"

ASSEMBLY = bin/Release/Resources.mui.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release/


endif


BINARIES = \
	$(RESOURCES_MUI)  



RESOURCES_MUI = $(BUILD_DIR)/resources.mui

FILES = \
	Stub.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	resx/PaintDotNet.Strings.3.DE.resx \
	resx/PaintDotNet.Strings.3.ES.resx \
	resx/PaintDotNet.Strings.3.FR.resx \
	resx/PaintDotNet.Strings.3.JA.resx \
	resx/PaintDotNet.Strings.3.KO.resx \
	resx/PaintDotNet.Strings.3.PT-BR.resx \
	resx/PaintDotNet.Strings.3.ZH-CN.resx \
	Resources/de/Files.AboutCredits.rtf \
	Resources/es/Files.AboutCredits.rtf \
	Resources/fr/Files.AboutCredits.rtf \
	Resources/fr/Images.PayPalDonate.gif \
	Resources/ja/Files.AboutCredits.rtf \
	Resources/ko/Files.AboutCredits.rtf \
	Resources/pt-br/Files.AboutCredits.rtf \
	Resources/zh-cn/Files.AboutCredits.rtf \
	PaintDotNet.Strings.3.DE.resources \
	PaintDotNet.Strings.3.ES.resources \
	PaintDotNet.Strings.3.FR.resources \
	PaintDotNet.Strings.3.JA.resources \
	PaintDotNet.Strings.3.KO.resources \
	PaintDotNet.Strings.3.PT-BR.resources \
	PaintDotNet.Strings.3.ZH-CN.resources \
	resources.mui.in 

REFERENCES =  \
	System

DLL_REFERENCES = 

CLEANFILES += $(BINARIES) 

#Targets
all: $(ASSEMBLY) $(BINARIES)  $(top_srcdir)/config.make

include $(srcdir)/custom-hooks.make



$(eval $(call emit-deploy-wrapper,RESOURCES_MUI,resources.mui,x))


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

install-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(RESOURCES_MUI)
	make pre-install-local-hook prefix=$(prefix)
	mkdir -p $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	cp $(ASSEMBLY) $(ASSEMBLY_MDB) $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	mkdir -p $(DESTDIR)$(prefix)/bin
	test -z '$(RESOURCES_MUI)' || cp $(RESOURCES_MUI) $(DESTDIR)$(prefix)/bin
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(RESOURCES_MUI)
	make pre-uninstall-local-hook prefix=$(prefix)
	rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY))
	test -z '$(ASSEMBLY_MDB)' || rm -f $(DESTDIR)$(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY_MDB))
	test -z '$(RESOURCES_MUI)' || rm -f $(DESTDIR)$(prefix)/bin/$(notdir $(RESOURCES_MUI))
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make
