require string.fs
require list.fs

\ Returns true if a is between [b,c]
: between ( a b c -- t )
  { a b c }

  a b >=
  a c <= and
;

: string:debug:print ( string -- )
  { string }

  cr ." string = " string hex. ." { "
  cr ."  data: " string string:data @ hex.
  cr ."  length: " string string:length @ .
  cr ." }"
;

: run-test-0
     ." string:make, string:print"
  cr s" Hello, world!" string:make string:print
  cr s" 123" string:make string:to-number drop .

  cr ." string:tokenize, string:for-each"
  s" A,BC,DEF,GHIJ" string:make { str }
  [char] , str string:tokenize
  [: cr ." Token:" string:print ;] swap list:for-each

  cr ." char at index 4 is: " s" Hello, world" string:make 4 string:nth emit

  cr ." concat two strings: "
  s" foo" string:make s" bar" string:make string:append string:print

  cr ." string:compare: "
  s" foo" string:make
  s" bar" string:make
  string:compare .

  cr ." string:compare: "
  s" foo" string:make
  s" barz" string:make
  string:compare .

  cr ." string:compare: "
  s" foo" string:make
  s" foo" string:make
  string:compare .

  cr ." string:from-char: "
  [char] x string:from-char string:print

  cr ." foobar 2 6 string:substring -> "
  s" foobar" string:make 2 6 string:substring string:print

  cr ." foobar 2 6 string:substring -> "
  s" xxxddddddddddddddd" string:make 2 6 string:substring string:print

  cr ." foobar bar string:index-of -> "
  s" foobar" string:make
  s" bar" string:make
  string:index-of .

  cr ." foobar bar string:index-of -> "
  s" foobar" string:make
  s" xxx" string:make
  string:index-of .

  cr ." foobarbuzz bar xxx string:replace -> "
  s" foobarbuzz" string:make
  s" bar" string:make
  s" xxx" string:make
  string:replace string:print

  cr ." foobarbuzz bar xxx string:replace -> "
  s" foobarbuzz" string:make
  s" 123" string:make
  s" xxx" string:make
  string:replace string:print

  cr ." fooxbar [: [char] x = ;] string:some -> "
  s" fooxbar" string:make
  [: [char] x = ;]
  string:some .

  cr ." fooxbar [: [char] x = ;] string:some -> "
  s" fooxbar" string:make
  [: [char] y = ;]
  string:some .

  cr ." 0123456789 [: [char] 0 [char] 9 between ;] string:every -> "
  s" 0123456789" string:make
  [: [char] 0 [char] 9 between ;]
  string:every .

  cr ." 0123456789a [: [char] 0 [char] 9 between ;] string:every -> "
  s" 0123456789a" string:make
  [: [char] 0 [char] 9 between ;]
  string:every .

  cr ." 158cm cm string:ends-with -> "
  s" 158cm" string:make
  s" cm" string:make
  string:ends-with .

  cr ." foobar bar string:ends-with -> "
  s" foobar" string:make
  s" bar" string:make
  string:ends-with .

  cr ." foobar ba string:ends-with -> "
  s" foobar" string:make
  s" ba" string:make
  string:ends-with .

  \ cr ." string chars 123 reduced to: "
  \ 42 \ pass to xt
  \   s" 123" string:make
  \   0
  \   [: { foobar acc char } foobar foobar ;]
  \   string:reduce .

;

run-test-0

bye
