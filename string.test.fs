marker ---marker---

require string.fs
require list.fs

: between ( a b c -- t )
  { a b c }

  a b >=
  a c <= and ;

: must-equal <> if abort" " else ." OK" then cr ;

: run-test
  ." string:print -> "
  s" Hello, world! " string:make string:print ." OK?" cr

  ." string:to-number -> "
  s" 123" string:make string:to-number drop 123 must-equal

  ." string:tokenize -> "
  s" A,BC,DEF,GHIJ" string:make { str }
  [char] , str string:tokenize { tokens }
  tokens list:length 4 must-equal

  ." string:for-each -> "
  tokens [: ." Token: " string:print space ;] swap list:for-each ." OK?" cr

  ." string:nth -> "
  s" Hello, world" string:make 4 string:nth [char] o must-equal

  ." string:compare -> "
  s" foo" string:make
  s" bar" string:make
  string:compare false must-equal

  ." string:compare -> "
  s" foo" string:make
  s" barz" string:make
  string:compare false must-equal

  ." string:compare -> "
  s" foo" string:make
  s" foo" string:make
  string:compare true must-equal

  ." string:append -> "
  s" foo" string:make s" bar" string:make string:append s" foobar" string:make string:compare true must-equal

  ." string:from-char -> "
  [char] x string:from-char s" x" string:make string:compare true must-equal

  ." string:substring -> "
  s" foobar" string:make 2 6 string:substring s" oba" string:make string:compare true must-equal

  ." string:substring -> "
  s" xxxddddddddddddddd" string:make 2 6 string:substring s" xdd" string:make string:compare true must-equal

  ." string:index-of -> "
  s" foobar" string:make
  s" bar" string:make
  string:index-of 3 must-equal

  ." string:index-of -> "
  s" foobar" string:make
  s" xxx" string:make
  string:index-of -1 must-equal

  ." string:replace -> "
  s" foobarbuzz" string:make
  s" bar" string:make
  s" xxx" string:make
  string:replace
  s" fooxxxbuzz" string:make string:compare true must-equal

  ." string:replace -> "
  s" foobarbuzz" string:make
  s" 123" string:make
  s" xxx" string:make
  string:replace
  s" foobarbuzz" string:make string:compare true must-equal

  ." string:some -> "
  s" fooxbar" string:make
  [: [char] x = ;]
  string:some true must-equal

  ." string:some -> "
  s" fooxbar" string:make
  [: [char] y = ;]
  string:some false must-equal

  ." string:every -> "
  s" 0123456789" string:make
  [: [char] 0 [char] 9 between ;]
  string:every true must-equal

  ." string:every -> "
  s" 0123456789a" string:make
  [: [char] 0 [char] 9 between ;]
  string:every false must-equal

  ." string:ends-with -> "
  s" 158cm" string:make
  s" cm" string:make
  string:ends-with true must-equal

  ." string:ends-with -> "
  s" foobar" string:make
  s" bar" string:make
  string:ends-with true must-equal

  ." string:ends-with -> "
  s" foobar" string:make
  s" ba" string:make
  string:ends-with false must-equal

  ." string:reduce -> "
  s" 123" string:make
  0
  [: { acc char1 } char1 [char] 0 - acc + ;]
  string:reduce 6 must-equal
;

run-test

---marker---
bye
