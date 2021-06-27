[undefined] list.fs [if]

vocabulary list.fs also list.fs definitions

0
dup constant list:node:next cell +
dup constant list:node:data cell +
constant list:node:struct

: list:node:nend?     list:node:next + @ 0<> ;

0
dup constant list:tail cell +
dup constant list:head cell +
constant list:struct

( list:node:struct                                             )
( +-----------+        +-----------+        +-----------+      )
( |   next    |------->|   next    |------->|   next    |--> 0 )
( +-----------+        +-----------+        +-----------+      )
( |   data    |        |   data    |        |   data    |      )
( +-----------+        +-----------+        +-----------+      )
(       ^---------+                               ^            )
(                 |                               |            )
( list:struct     |                               |            )
( +-----------+   |                               |            )
( |   tail    |<--+                               |            )
( +-----------+                                   |            )
( |   head    |<----------------------------------+            )
( +-----------+                                                )

: list:.node ( node -- )
  hex
  ." node: { "
  dup list:node:next + @ .
      list:node:data + @ .
  ." } "
  decimal
;

\ allot new list object
: list:create ( -- list )
  here
  dup list:struct allot
      list:struct erase
;

\ allot new node and set data to u
: list:node:create ( data -- node )
  here dup >r
  list:node:struct allot
  dup list:node:struct erase
  list:node:data + !
  r>
;

\ allot new node with data and append to list return list
: list:append ( list data -- list )
  dup list:node:create dup >r

  list:node:data + !

  dup list:tail + @ 0= if
    r@ over list:tail + !
  then

  dup list:head + @ 0<> if
    r@ over list:head + @ list:node:next + !
  then

  r> over list:head + !
;

\ execute xt on every element of list
: list:for-each ( xt list -- )
  list:tail + @ >r

  begin
    r@ list:node:data + @ over execute
    r@ list:node:nend?
  while
    r> list:node:next + @ >r
  repeat
  rdrop drop
;

\ execute xt on every element of list1 and create a new list2 and return
: list:map ( list1 xt -- list2 )
  swap list:tail + @ >r
  list:create ( xt list2 )

  begin
    over r@ list:node:data + @ swap execute ( xt list2 result )
    2dup list:append 2drop
    r@ list:node:nend?
  while
    r> list:node:next + @ >r
  repeat
  rdrop nip
;

\ return list length
: list:length ( list -- n )
  0
  swap list:tail + @ >r

  begin
    1+
    r@ list:node:nend?
  while
    r> list:node:next + @ >r
  repeat
  rdrop
;

\ return the nth data from list
: list:nth ( list n -- data )
  0
  rot list:tail + @ >r

  begin
    2dup <>
  while
    r> list:node:next + @ >r
    1+
  repeat
  2drop
  r> list:node:data + @
;

\ apply func xt on every element accumulating result in acc. xt is called with ( acc element -- acc )
: list:reduce ( list acc xt -- acc )
  rot list:tail + @ >r ( acc xt ) ( R: iter )

  r@ 0= if rdrop drop exit then

  begin
    r@ list:node:data + @ swap dup >r execute r> ( acc xt )
    r@ list:node:nend?
  while
    r> list:node:next + @ >r
  repeat
  rdrop drop
;

\ execute xt on every node and return true if at least one returns true. xt is called with ( element -- t )
: list:some ( list xt -- t )
  swap list:tail + @ >r ( xt ) ( R: iter )

  begin
    r@ list:node:nend?
  while
    r@ list:node:data + @ over execute ( xt t )
    0<> if rdrop drop true exit then
    r> list:node:next + @ >r
  repeat
  rdrop false
;

previous definitions

[endif]
