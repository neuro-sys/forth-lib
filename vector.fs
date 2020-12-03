0 \ vector:struct
dup constant vector->length 1 cells +
dup constant vector->data   1 cells +
dup constant vector->stride 1 cells +
constant vector:struct

: vector:allocate  vector:struct allocate throw ;
: vector:length    vector->length + ;
: vector:data      vector->data + ;
: vector:stride    vector->stride + ;
: vector:length@   vector:length @ ;
: vector:data@     vector:data @ ;
: vector:stride@   vector:stride @ ;
: vector:erase     vector:struct erase ;

: vector:make ( size stride -- addr ) \ initial size
  { size stride }

  vector:allocate { vector }

  size vector vector:length !
  stride size * allocate throw vector vector:data !

  vector
;
