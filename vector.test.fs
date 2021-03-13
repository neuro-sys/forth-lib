require vector.fs

\ test

: vector-debug ( vector -- )
  { vector }
  ." vector = " vector hex. ." {"                cr
  ."   length: "      vector vector:length @    . cr
  ."   data: "        vector vector:data @   hex. cr
  ." }" cr
;

42 1 chars vector:create vector-debug
