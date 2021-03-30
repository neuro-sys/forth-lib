marker ---marker---

require string.fs
require list.fs

also list.fs
also string.fs

: between ( a b c -- t )
  rot dup >r
  swap <=
  swap r> swap >=
  and
;

: must-equal <> if abort" " else ." OK" then ;

variable str
variable tokens

: run-test
  ." string:print -> "
  s" Hello, world! " string:create string:print ." OK?" cr

  ." string:to-number -> "
  s" 123" string:create string:to-number drop 123 must-equal cr

  ." string:tokenize -> "
  s" A,BC,DEF,GHIJ" string:create str !
  [char] , str @ string:tokenize tokens !
  tokens @ list:length 4 must-equal cr

  ." string:for-each -> "
  tokens [: ." Token: " string:print space ;] swap list:for-each ." OK?" cr

  ." string:nth -> "
  s" Hello, world" string:create 4 string:nth [char] o must-equal cr

  ." string:compare -> "
  s" foo" string:create
  s" bar" string:create
  string:compare false must-equal cr

  ." string:compare -> "
  s" foo" string:create
  s" barz" string:create
  string:compare false must-equal cr

  ." string:compare -> "
  s" foo" string:create
  s" foo" string:create
  string:compare true must-equal cr

  ." string:append -> "
  s" foo" string:create s" bar" string:create string:append s" foobar" string:create string:compare true must-equal cr

  ." string:from-char -> "
  [char] x string:from-char s" x" string:create string:compare true must-equal cr

  ." string:substring -> "
  s" foobar" string:create 2 6 string:substring s" oba" string:create string:compare true must-equal cr

  ." string:substring -> "
  s" xxxddddddddddddddd" string:create 2 6 string:substring s" xdd" string:create string:compare true must-equal cr

  ." string:index-of -> "
  s" foobar" string:create
  s" bar" string:create
  string:index-of 3 must-equal cr

  ." string:index-of -> "
  s" foobar" string:create
  s" xxx" string:create
  string:index-of -1 must-equal cr

  ." string:replace -> "
  s" foobarbuzz" string:create
  s" bar" string:create
  s" xxx" string:create
  string:replace
  s" fooxxxbuzz" string:create string:compare true must-equal cr

  ." string:replace -> "
  s" foobarbuzz" string:create
  s" 123" string:create
  s" xxx" string:create
  string:replace
  s" foobarbuzz" string:create string:compare true must-equal cr

  ." string:some -> "
  s" fooxbar" string:create
  [: [char] x = ;]
  string:some true must-equal cr

  ." string:some -> "
  s" fooxbar" string:create
  [: [char] y = ;]
  string:some false must-equal cr

  ." string:every -> "
  s" 0123456789" string:create
  [: [char] 0 [char] 9 between ;]
  string:every true must-equal cr

  ." string:every -> "
  s" 0123456789a" string:create
  [: [char] 0 [char] 9 between ;]
  string:every false must-equal cr

  ." string:ends-with -> "
  s" 158cm" string:create
  s" cm" string:create
  string:ends-with true must-equal cr

  ." string:ends-with -> "
  s" foobar" string:create
  s" bar" string:create
  string:ends-with true must-equal cr

  ." string:ends-with -> "
  s" foobar" string:create
  s" ba" string:create
  string:ends-with false must-equal cr

  ." string:reduce -> "
  s" 123" string:create
  0
  [: { acc char1 } char1 [char] 0 - acc + ;]
  string:reduce 6 must-equal cr
;

run-test

---marker---
