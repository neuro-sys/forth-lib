begin-structure vector:struct
  field: vector:length
  field: vector:data
  field: vector:stride
end-structure

: vector:allocate  vector:struct allocate throw ;
: vector:erase     vector:struct erase ;

: vector:make ( size stride -- addr ) \ initial size
  { size stride }

  vector:allocate { vector }

  size vector vector:length !

  stride size * allocate throw vector vector:data !

  vector
;
