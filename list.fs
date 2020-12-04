\ linked list
\
\        next     data
\      +-------+-------+
\ 100  |  102  |  42   |          0 42 list:append 100
\      +-------+-------+
\ 102  |  104  |  666  |           666 list:append 102
\      +-------+-------+
\ 104  |  0    |  1337 | <- head  1337 list:append 104
\      +-------+-------+

begin-structure list:node:struct
  field: list:node:next
  field: list:node:data
end-structure

: list:node:next@     list:node:next @ ;
: list:node:data@     list:node:data @ ;
: list:node:end?      list:node:next 0= ;
: list:node:nend?     list:node:end? invert ;

begin-structure list:struct
  field: list:tail
  field: list:head
end-structure

: list:tail@   list:tail @ ;
: list:head@   list:head @ ;

\ allocate new list object
: list:make ( -- list )
  list:struct allocate throw { list }

  list list:struct erase

  list
;

\ allocate new node and set data to u
: list:node:make ( data -- node )
  { data }

  list:node:struct allocate throw { node }

  node list:node:struct erase
  data node list:node:data !

  node
;

\ allocate new node with data and append to list return list
: list:append ( list data -- list )
  { list data }

  data list:node:make { node }

  data node list:node:data !

  list list:tail@ 0= if
    node list list:tail !
  then

  list list:head@ 0<> if
    node list list:head@ list:node:next !
  then

  node list list:head !

  list
;

\ execute xt on every element of list
: list:for-each ( xt list -- )
  { xt list }
  list list:tail@ { iter }

  begin
    iter list:node:nend?
  while
    iter list:node:data@ xt execute
    iter list:node:next@ to iter
  repeat
;

\ execute xt on every element of list1 and create a new list2 and
\ return
: list:map ( list1 xt -- list2 )
  { list1 xt }

  list1 list:tail@ { iter }
  list:make { list2 }

  begin
    iter list:node:nend?
  while
    iter list:node:data@ xt execute { result }
    list2 result list:append drop
    iter list:node:next@ to iter
  repeat

  list2
;

: list:length ( list -- n )
  { list }

  0 { n }

  list list:tail@ { iter }

  begin
    iter list:node:nend?
  while
    n 1+ to n
    iter list:node:next@ to iter
  repeat

  n
;

\ return the nth data from list
: list:nth ( list n -- data )
  { list n }
  0 { k }

  list list:tail@ { iter }

  begin
    k n <>
  while
    iter list:node:next@ to iter
    k 1+ to k
  repeat

  iter list:node:data@
;

\ Apply func xt on every element accumulating result in acc
\ xt is called with ( acc element -- acc )
: list:reduce ( list acc xt -- acc )
  { list acc xt }

  list list:tail@ { iter }

  begin
    iter list:node:nend?
  while
    acc iter list:node:data@ xt execute to acc
    iter list:node:next@ to iter
  repeat

  acc
;
