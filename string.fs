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
  >r
  r@ string:data + @
  r> string:length + @
;

\ return caddr from string
: string:caddr ( string -- caddr ) string:raw drop ;

\ make a string from the string at counted string
: string:create ( caddr u -- string )
  here >r ( caddr u ) ( R: string )
  string:struct allot
  dup r@ string:length + !
  here ( caddr u data )
  over allot
  r@ string:data + ! ( caddr u )
  r@ string:data + @
  swap
  cmove
  r>
;

\ convert string into number
: string:to-number ( string -- u )
  >r
  r@ string:data + @ ( caddr )
  r> string:length + @ ( caddr len )
  0 0 2swap >number ( ud0 ud1 u1 u2 )
  drop nip
;

\ print string
: string:print ( string -- )
  >r
  r@ string:data + @
  r@ string:length + @
  type rdrop
;


variable d
variable string
variable caddr
variable prev-caddr
variable tokens
variable k
\ tokenize string delimited by d into list of tokens
: string:tokenize ( d string -- tokens )
  string ! d !

  string @ string:data + @   caddr !
  string @ string:data + @   prev-caddr !
  list:create                tokens !
  0                          k !

  \ When current character equals the delimiter, push a string
  \ denoting the last caddr and current index k. Also set k to i and
  \ set prev caddr to _caddr.

  string @ string:length + @ 0 ?do
    caddr @ c@ d @ = if
      tokens @
        prev-caddr @ k @ string:create
      list:append tokens !

      caddr @ 1+ prev-caddr !
      -1 k !
    then

    k @ 1+ k !
    caddr @ 1+ caddr !
  loop

  tokens @
    prev-caddr @ k @ string:create
  list:append tokens !

  tokens @
;

\ return nth character in string
: string:nth ( string n -- c )
  swap
  string:data + @ + c@
;

\ execute xt on every node accumulating result in acc. xt is called with ( acc char -- acc )
: string:reduce ( string acc xt -- acc )
  rot ( acc xt string )

  dup string:length + @ 0 ?do
    dup string:data + @ i + c@ ( acc xt string c )
    over >r nip
    over >r nip swap r@ ( xt acc c ) execute r> r> ( acc )
  loop
  2drop
;

\ execute xt on every node and return true if at least one returns true
: string:some ( string xt -- t )
  swap ( xt string )
  dup string:length + @ 0 ?do
    dup string:data + @ i + c@ ( xt string c )
    rot dup >r execute 0<> if
      rdrop drop
      true
      unloop
      exit
    then
    r> swap
  loop

  2drop
  false
;

\ execute xt on every node and return true if all returns true
: string:every ( string xt -- t )
  over string:length + @ 0 ?do
    over string:data + @ i + c@ 
    over execute invert if
      false
      unloop
      exit
    then
  loop
  2drop true
;

variable string1
variable string2
variable string3
variable u
\ append string2 to string1 and return string3
: string:append ( string1 string2 -- string3 )
  string2 ! string1 !

  string1 @ string:length + @
  string2 @ string:length + @ + u !

  here u @ string:create string3 !

  string1 @ string:caddr
    string3 @ string:caddr
    string1 @ string:length + @
  cmove

  string2 @ string:caddr
    string3 @ string:caddr
      string1 @ string:length + @ +
    string2 @ string:length + @
  cmove

  string3 @
;

variable string1
variable string2
\ compare string1 with string2 and return boolean
: string:compare ( string1 string2 -- t )
  string2 ! string1 !

  string1 @ string:length + @
  string2 @ string:length + @ <> if false exit then

  string1 @ string:length + @ 0 ?do
    string1 @ i string:nth
    string2 @ i string:nth <> if false unloop exit then
  loop

  true
;

\ make string for char
: string:from-char ( c -- string )
  s"  " string:create dup >r

  string:data + @ c!

  r>
;

variable string1
variable a
variable b
variable c
variable length
\ exctract string2 from string1 with offsets [a,b)
: string:substring ( string1 a b -- string2 )
  b ! a ! string1 !

  b @ a @ -                    length  !
  here length @ string:create  string2 !

  length @ 0 ?do
    i a @ + string1 @ string:length + @ = if leave then

    string1 @ i a @ + string:nth c !

    c @ string2 @ string:caddr i + c!
  loop

  string2 @
;

variable string1
variable string2
variable index
variable caddr3
variable u
variable found
\ return the index of string2 within string1 otherwise -1
: string:index-of ( string1 string2 -- b )
  string2 ! string1 !

  -1 index !

  string1 @ string:raw
  string2 @ string:raw
  search found ! u ! caddr3 !

  found @ if
    caddr3 @ string1 @ string:caddr - index !
  then

  index @
;

variable string1
variable string2
variable string3
variable string4
variable length
variable index
variable first
variable remaining
\ replace string2 in string1 with string3 and return string4
: string:replace ( string1 string2 string3 -- string4 )
  string3 ! string2 ! string1 !

  string1 @ string:length + @
  string2 @ string:length + @
  min length !

  string1 @ string2 @ string:index-of index !

  index @ -1 = if
    string1 @ exit
  then

  string1 @ string:caddr index @ string:create first !

  first @ string3 @ string:append string4 !

  string1 @ string:caddr index @ + length @ +
  length @ index @ length @ - 1- -
  string:create remaining !

  string4 @ remaining @ string:append string4 !

  string4 @
;

variable string1
variable string2
variable string3-caddr
variable offset
\ returns true if string1 ends with string2
: string:ends-with ( string1 string2 -- t )
  string2 ! string1 !

  string1 @ string:length + @
  string2 @ string:length + @
  < if false exit then

  string1 @ string:length + @
  string2 @ string:length + @
  - offset !

  string1 @ string:caddr offset @ + string3-caddr !

  string3-caddr @ string2 @ string:length + @
  string2 @ string:raw
  compare 0= if true exit then

  false
;

previous definitions

[endif]
