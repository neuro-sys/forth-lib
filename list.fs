begin-structure list:node:struct
  field: list:node:next
  field: list:node:data
end-structure

: list:node:end?      list:node:next 0= ;
: list:node:nend?     list:node:end? invert ;

begin-structure list:struct
  field: list:tail
  field: list:head
end-structure

\ ( -- list ) allot new list object
: list:make
  here { list }

  list:struct allot

  list list:struct erase

  list
;

\ ( data -- node ) allot new node and set data to u
: list:node:make
  { data }

  here { node }

  list:node:struct allot

  node list:node:struct erase
  data node list:node:data !

  node
;

\ ( list data -- list ) allot new node with data and append to list return list
: list:append
  { list data }

  data list:node:make { node }

  data node list:node:data !

  list list:tail @ 0= if
    node list list:tail !
  then

  list list:head @ 0<> if
    node list list:head @ list:node:next !
  then

  node list list:head !

  list
;

\ ( xt list -- ) execute xt on every element of list
: list:for-each
  { xt list }
  list list:tail @ { iter }

  begin
    iter list:node:nend?
  while
    iter list:node:data @ xt execute
    iter list:node:next @ to iter
  repeat
;

\ ( list1 xt -- list2 ) execute xt on every element of list1 and create a new list2 and return
: list:map
  { list1 xt }

  list1 list:tail @ { iter }
  list:make { list2 }

  begin
    iter list:node:nend?
  while
    iter list:node:data @ xt execute { result }
    list2 result list:append drop
    iter list:node:next @ to iter
  repeat

  list2
;

\ ( list -- n ) return list length
: list:length
  { list }

  0 { n }

  list list:tail @ { iter }

  begin
    iter list:node:nend?
  while
    n 1+ to n
    iter list:node:next @ to iter
  repeat

  n
;

\ ( list n -- data ) return the nth data from list
: list:nth
  { list n }
  0 { k }

  list list:tail @ { iter }

  begin
    k n <>
  while
    iter list:node:next @ to iter
    k 1+ to k
  repeat

  iter list:node:data @
;

\ ( list acc xt -- acc ) apply func xt on every element accumulating result in acc. xt is called with ( acc element -- acc )
: list:reduce
  { list acc xt }

  list list:tail @ { iter }

  begin
    iter list:node:nend?
  while
    acc iter list:node:data @ xt execute to acc
    iter list:node:next @ to iter
  repeat

  acc
;

\ ( list xt -- t ) execute xt on every node and return true if at least one returns true. xt is called with ( element -- t )
: list:some
  { list xt }

  false { some? }

  list list:tail @ { iter }

  begin
    iter list:node:nend?
  while
    iter list:node:data @ xt execute { result }

    \ FIXME, exit early
    result if
      true to some?
    then

    iter list:node:next @ to iter
  repeat
  some?
;
