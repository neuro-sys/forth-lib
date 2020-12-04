require vector.fs
require list.fs

\ vector:struct
vector:struct constant string:struct

: string:erase      vector:erase ;
: string:data       vector:data ;
: string:length     vector:length ;
: string:data@      vector:data@ ;
: string:length@    vector:length@ ;

: string:raw ( string -- caddr u )
  { string }

  string string:data@
  string string:length@
;

: string:caddr string:raw drop ;

\ make a string from the string at caddr with u length returning a
\ string
: string:make ( caddr u -- string )
  { caddr u }

  u 1 chars vector:make { string }

  caddr
    string string:data@
    u
  cmove

  string
;

: string:to-number ( string -- u )
  { string }

  string string:data@ { caddr }
  string string:length@ { len }

  0 0 caddr len >number { ud0 ud1 u1 u2 }

  ud0 ud1
;

\ print string
: string:print ( string -- )
  { string }

  string string:data@
  string string:length@
  type
;

\ tokenize string delimited by d into tokens
: string:tokenize ( d string -- tokens )
  { d string }

  string string:data@   { caddr }
  string string:data@   { prev-caddr }
  list:make             { tokens }
  0                     { k }

  \ When current character equals the delimiter, push a string
  \ denoting the last caddr and current index k. Also set k to i and
  \ set prev caddr to _caddr.

  string string:length@ 0 ?do
    caddr c@ d = if
      tokens
        prev-caddr k string:make
      list:append to tokens

      caddr 1+ to prev-caddr
      -1 to k
    then

    k 1+ to k
    caddr 1+ to caddr
  loop

  tokens
    prev-caddr k string:make
  list:append to tokens

  tokens
;

\ return nth character in string
: string:nth ( string n -- c )
  { string n }

  string string:data@ n + c@
;

\ Apply func xt on every element accumulating result in acc
\ xt is called with ( acc char -- acc )
: string:reduce ( string acc xt -- acc )
  { string acc xt }

  string string:length@ 0 ?do
    string string:data@ i + c@ { c }
    acc c xt execute to acc
  loop

  acc
;


: string:append ( string1 string2 -- string3 )
  { string1 string2 }

  string1 string:length@
  string2 string:length@ + { u }

  u 1 chars vector:make { string3 }

  string1 string:caddr
    string3 string:caddr
    string1 string:length@
  cmove

  string2 string:caddr
    string3 string:caddr
      string1 string:length@ +
    string2 string:length@
  cmove

  string3
;
