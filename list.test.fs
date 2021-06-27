require list.fs

also list.fs

: must-equal <> if abort" " else ." OK " then ;

variable list1
variable list2
: run-test
  list:create list1 !

  list1 @ 1 list:append list1 !
  list1 @ 2 list:append list1 !
  list1 @ 3 list:append list1 !
  list1 @ 4 list:append list1 !
  list1 @ 5 list:append list1 !
  list1 @ 6 list:append list1 !

  ." list:for-each -> "
  [: . ;] list1 @ list:for-each ." OK?" cr

  ." list:nth -> "
  list1 @ 2 list:nth 3 must-equal cr

  ." list:reduce -> "
  list1 @ 1 [: * ;] list:reduce 720 must-equal cr

  ." list:length -> "
  list1 @ list:length 6 must-equal cr

  ." list:map -> "
  list1 @ [: 2 * ;] list:map list2 !

  list2 @ list:length list1 @ list:length must-equal
  list2 @ 0 list:nth list1 @ 0 list:nth 2 * must-equal
  list2 @ 1 list:nth list1 @ 1 list:nth 2 * must-equal
  list2 @ 2 list:nth list1 @ 2 list:nth 2 * must-equal
  list2 @ 3 list:nth list1 @ 3 list:nth 2 * must-equal cr

  ." list:some -> "
  list1 @ [: 3 = ;] list:some true must-equal cr
;

run-test
