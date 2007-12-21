pre-install-local-hook:
	mkdir -p $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	cp PaintDotNet.Strings*.resources $(DESTDIR)$(prefix)/lib/$(PACKAGE)
	cp ../bin/*/PaintDotNet.Strings.3.resources $(DESTDIR)$(prefix)/lib/$(PACKAGE)
