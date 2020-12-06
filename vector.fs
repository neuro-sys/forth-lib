begin-structure vector:struct
  field: vector:length
  field: vector:data
  field: vector:stride
end-structure

: vector:allocate  vector:struct allocate throw ;
: vector:erase     vector:struct erase ;

: vector:make ( size stride -- addr ) \ initial size
  { size stride }

  here

  size ,       \ store size at length
  1 allot      \ reserve data cell
  stride ,     \ store stride at stride

  stride size * allocate throw over vector:data !
;
