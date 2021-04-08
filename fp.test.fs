require fp.fs

also fp.fs

: must-equal <> if abort" " else ." OK " then ;

: run-test
  cr 8 i>fp 2048 must-equal
  cr 2048 fp>i 8 must-equal
  cr 3 i>fp 4 i>fp fp* fp>i 12 must-equal
  cr 9 i>fp 3 i>fp fp/ fp>i 3 must-equal
  cr 13 i>fp 5 i>fp fp/ 2 6 if>fp must-equal
  cr 12345 c10s 5 must-equal
  cr 2 3 if>fp fpfloor 2 i>fp must-equal
  cr 2 3 if>fp fpceil 3 i>fp must-equal
  cr 2 3 if>fp fpround 2 i>fp must-equal
  cr 2 6 if>fp fpround 3 i>fp must-equal
;

run-test
