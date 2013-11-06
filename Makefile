
PREFIX?=/usr/local/opt

XBUILD:=xbuild
MONO:=mono

ASSEMBLY=src/Matrix.csproj
SRC=$(wildcard Matrix/*.cs)
DST=/usr/local/bin/matrix


release:
	@$(XBUILD) /p:Configuration=Release $(ASSEMBLY)

debug: $(SRC)
	@$(XBUILD) $(ASSEMBLY)

run:
	@$(MONO) bin/matrix.exe

install:
	@mkdir -p $(PREFIX)/matrix
	@cp bin/matrix.exe $(PREFIX)/matrix
	@echo "#!/usr/bin/env bash\n$(MONO) $(PREFIX)/matrix/matrix.exe" \
		> $(DST)
	@chmod +x $(DST)

uninstall:
	rm -fr $(PREFIX)/matrix
	rm /usr/local/bin/matrix

clean:
	rm -fr bin src/obj


.PHONY: release debug run install uninstall clean

