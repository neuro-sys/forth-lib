# forth-libs

Collection of words to help with writing high level applications with
Forth language. The source code here is tested using
gforth. Suggestions are welcome.

Allocations are done on Dictionary, use marker to free after usage.

Current revision: 1e505589d0564333568871777be7c3f0ef4f1633.

This file is generated by docgen.fs with docgen.sh runner.

## require list.fs also list.fs

### `list:create ( -- list )`
allot new list object

### `list:node:create ( data -- node )`
allot new node and set data to u

### `list:append ( list data -- list )`
allot new node with data and append to list return list

### `list:for-each ( xt list -- )`
execute xt on every element of list

### `list:map ( list1 xt -- list2 )`
execute xt on every element of list1 and create a new list2 and return

### `list:length ( list -- n )`
return list length

### `list:nth ( list n -- data )`
return the nth data from list

### `list:reduce ( list acc xt -- acc )`
apply func xt on every element accumulating result in acc. xt is called with ( acc element -- acc )

### `list:some ( list xt -- t )`
execute xt on every node and return true if at least one returns true. xt is called with ( element -- t )

## require string.fs also string.fs

### `string:raw ( string -- caddr u )`
return counted string from string

### `string:caddr ( string -- caddr )`
return caddr from string

### `string:create ( caddr u -- string )`
make a string from the string at counted string

### `string:to-number ( string -- u )`
convert string into number

### `string:print ( string -- )`
print string

### `string:tokenize ( d string -- tokens )`
tokenize string delimited by d into list of tokens

### `string:nth ( string n -- c )`
return nth character in string

### `string:reduce ( string acc xt -- acc )`
execute xt on every node accumulating result in acc. xt is called with ( acc char -- acc )

### `string:some ( string xt -- t )`
execute xt on every node and return true if at least one returns true

### `string:every ( string xt -- t )`
execute xt on every node and return true if all returns true

### `string:append ( string1 string2 -- string3 )`
append string2 to string1 and return string3

### `string:compare ( string1 string2 -- t )`
compare string1 with string2 and return boolean

### `string:from-char ( c -- string )`
make string for char

### `string:substring ( string1 a b -- string2 )`
exctract string2 from string1 with offsets [a,b) (FIXME)

### `string:index-of ( string1 string2 -- b )`
return the index of string2 within string1 otherwise -1

### `string:replace ( string1 string2 string3 -- string4 )`
replace string2 in string1 with string3 and return string4

### `string:ends-with ( string1 string2 -- t )`
returns true if string1 ends with string2

## require fp.fs also fp.fs

### `i>fp ( n -- fp )`
convert integer to fixed point

### `fp>i ( fp -- n )`
convert fixed point to integer (rounded down)

### `fp* ( n0 n1 -- n2 )`
multiply two fixed point numbers

### `fp/ ( n0 n1 -- n2 )`
divide two fixed point numbers

### `i3>fp3 ( a b c -- a b c )`
vector3 helper: convert 3 integers to 3 fixed point numbers

### `fp3>i3 ( a b c -- a b c )`
vector3 helper: convert 3 fixed point numbers to 3 integers

### `fpfloor ( n0 -- n1 )`
floor down fixed point number to nearest integer

### `fpceil ( n0 -- n1 )`
ceil up fixed point number to nearest integer

### `fpround ( n0 -- n1 )`
round fixed point number to nearest integer

### `c10s ( a -- b )`
return base 10 digits

### `if>fp ( i f -- fp )`
convert a decimal fractional number in the form integer fractional

### `if2>fp ( i f c -- fp )`
supply the fractional digit count

