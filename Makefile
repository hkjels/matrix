

build: Readme.md
	@marked -gfm -i $^ | \
		cat layout/head.html - layout/tail.html \
		> index.html

clean:
	rm index.html


.PHONY: build clean

