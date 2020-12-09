begin-structure vector:struct
  field: vector:length
  field: vector:data
  field: vector:stride
end-structure

: vector:erase     vector:struct erase ;

: vector:make ( size stride -- addr ) \ initial size
  { size stride }

  here { vector }

  vector:struct allot

  size vector vector:length !
  stride vector vector:stride !

  here { data }

  stride size * allot

  data vector vector:data !

  vector
;
