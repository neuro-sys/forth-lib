[undefined] string.fs [if]

vocabulary string.fs also string.fs definitions

require list.fs

also list.fs

0
dup constant string:length cell +
dup constant string:data cell +
constant string:struct

: string:erase      string:struct erase ;

\ return counted string from string
: string:raw ( string -- caddr u )
  { string }

  string string:data + @
  string string:length + @
;

\ return caddr from string
: string:caddr ( string -- caddr ) string:raw drop ;

\ make a string from the string at counted string
: string:create ( caddr u -- string )
  { caddr u }

  here { string }

  string:struct allot

  u string string:length + !

  here { data }

  u allot

  data string string:data + !

  caddr
    string string:data + @
    u
  cmove

  string
;

\ convert string into number
: string:to-number ( string -- u )
  { string }

  string string:data + @ { caddr }
  string string:length + @ { len }

  0 0 caddr len >number { ud0 ud1 u1 u2 }

  ud0 ud1
;

\ print string
: string:print ( string -- )
  { string }

  string string:data + @
  string string:length + @
  type
;

\ tokenize string delimited by d into list of tokens
: string:tokenize ( d string -- tokens )
  { d string }

  string string:data + @   { caddr }
  string string:data + @   { prev-caddr }
  list:create             { tokens }
  0                     { k }

  \ When current character equals the delimiter, push a string
  \ denoting the last caddr and current index k. Also set k to i and
  \ set prev caddr to _caddr.

  string string:length + @ 0 ?do
    caddr c@ d = if
      tokens
        prev-caddr k string:create
      list:append to tokens

      caddr 1+ to prev-caddr
      -1 to k
    then

    k 1+ to k
    caddr 1+ to caddr
  loop

  tokens
    prev-caddr k string:create
  list:append to tokens

  tokens
;

\ return nth character in string
: string:nth ( string n -- c )
  { string n }

  string string:data + @ n + c@
;

\ execute xt on every node accumulating result in acc. xt is called with ( acc char -- acc )
: string:reduce ( string acc xt -- acc )
  { string acc xt }

  string string:length + @ 0 ?do
    string string:data + @ i + c@ { c }
    acc c xt execute to acc
  loop

  acc
;

\ execute xt on every node and return true if at least one returns true
: string:some ( string xt -- t )
  { string xt }

  false { some? }

  string string:length + @ 0 ?do
    string string:data + @ i + c@ { c }
    c xt execute if
      true to some?
      leave
    then
  loop

  some?
;

\ execute xt on every node and return true if all returns true
: string:every ( string xt -- t )
  { string xt }

  true { every? }

  string string:length + @ 0 ?do
    string string:data + @ i + c@ { c }
    c xt execute invert if
      false to every?
      leave
    then
  loop

  every?
;

\ append string2 to string1 and return string3
: string:append ( string1 string2 -- string3 )
  { string1 string2 }

  string1 string:length + @
  string2 string:length + @ + { u }

  here u string:create { string3 }

  string1 string:caddr
    string3 string:caddr
    string1 string:length + @
  cmove

  string2 string:caddr
    string3 string:caddr
      string1 string:length + @ +
    string2 string:length + @
  cmove

  string3
;

\ compare string1 with string2 and return boolean
: string:compare ( string1 string2 -- t )
  { string1 string2 }

  true { equal? }

  string1 string:length + @
  string2 string:length + @ <> if false exit then

  string1 string:length + @ 0 ?do
    string1 i string:nth
    string2 i string:nth <> if false to equal? then
  loop

  equal?
;

\ make string for char
: string:from-char ( c -- string )
  { c }

  s"  " string:create { string }

  c string string:data + @ c!

  string
;

\ exctract string2 from string1 with offsets [a,b) (FIXME)
: string:substring ( string1 a b -- string2 )
  { string1 a b }

  \ FIXME: handle reverse indices
  b a < if ." Not implemented" abort then

  b a - 1-                   { length }
  here length string:create { string2 }

  length 0 ?do
    i a + string1 string:length + @ = if leave then

    string1 i a + string:nth { c }

    c string2 string:caddr i + c!
  loop

  string2
;

\ return the index of string2 within string1 otherwise -1
: string:index-of ( string1 string2 -- b )
  { string1 string2 }

  -1 { index }

  string1 string:raw
  string2 string:raw
  search { c-addr3 u found }

  found if
    c-addr3 string1 string:caddr - to index
  then

  index
;

\ replace string2 in string1 with string3 and return string4
: string:replace ( string1 string2 string3 -- string4 )
  { string1 string2 string3 }

  string1 string:length + @
  string2 string:length + @
  min { length }

  string1 string2 string:index-of { index }

  index -1 = if
    string1 exit
  then

  string1 string:caddr index string:create { first }

  first string3 string:append { string4 }

  string1 string:caddr index + length +
  length index length - 1- -
  string:create { remaining }

  string4 remaining string:append to string4

  string4
;

\ returns true if string1 ends with string2
: string:ends-with ( string1 string2 -- t )
  { string1 string2 }

  string1 string:length + @
  string2 string:length + @
  < if false exit then

  string1 string:length + @
  string2 string:length + @
  - { offset }

  string1 string:caddr offset + { string3-caddr }

  string3-caddr string2 string:length + @
  string2 string:raw
  compare 0= if true exit then

  false
;

previous definitions

[endif]
