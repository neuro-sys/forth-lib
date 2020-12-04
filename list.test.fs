require list.fs

: list:node:print ( node -- )
  { node }

  ." node = " node hex. ." { "
  ." next: " node list:node:next @ hex. ." , "
  ." data: " node list:node:data @ .
  ." }"
;

: list:print ( list -- )
  { list }

  cr ." list = " list hex. ." {"
  cr ."   tail: " list list:tail @ dup 0<> if list:node:print else ." NULL " then
  cr ."   head: " list list:head @ dup 0<> if list:node:print else ." NULL " then
  cr ." } "
;

: run-test-0
  list:make { list1 }

  list1 1 list:append to list1
  list1 2 list:append to list1
  list1 3 list:append to list1
  list1 4 list:append to list1
  list1 5 list:append to list1
  list1 6 list:append to list1

  [: ." list:for-each: " . cr ;] list1 list:for-each

  ." element at 2nd index: " list1 2 list:nth .

  cr ." List reduced to Factorial: " list1 1 [: * ;] list:reduce .

  cr ." Number of elements in list: " list1 list:length .

  list1 [: 2 * ;] list:map { list2 }

  cr ." Mapped list:" cr

  [: ." list:for-each: " . cr ;] list2 list:for-each

  list1 [: 3 = ;] list:some .
;


run-test-0

bye
