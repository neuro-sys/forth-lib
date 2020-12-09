#!/bin/bash

cat <<EOF > README.md
# forth-libs
EOF

echo "# \`list.fs\`" >> README.md

gforth -e "s\" list.fs\"" docgen.fs >> README.md

echo "# \`string.fs\`" >> README.md

gforth -e "s\" string.fs\"" docgen.fs >> README.md

sed -i -e "s/(\(.*?\))/\`\1\`/g" README.md
