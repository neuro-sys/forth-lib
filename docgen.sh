#!/bin/bash

cat <<EOF > README.md
# forth-libs

Collection of words to help with writing high level applications with
Forth language. The source code here is tested using
gforth. Suggestions are welcome.

Current revision: `git rev-parse HEAD`.

This file is generated by docgen.fs with docgen.sh runner.

EOF

gforth -e "s\" list.fs\"" docgen.fs >> README.md
gforth -e "s\" string.fs\"" docgen.fs >> README.md
