[undefined] fp.fs [if]

vocabulary fp.fs also fp.fs definitions

8                               constant point      \ fractional point
1 point lshift                  constant fp-bitmask
fp-bitmask 1-                   constant fp-fmask   \ fractional bit mask
fp-fmask invert                 constant fp-imask   \ integer bit mask
fp-bitmask 1 rshift             constant fp.bit/2   \ 0.5 in fp
fp-bitmask negate fp-bitmask *  constant max-fp     \ maximum value
fp-bitmask negate fp-bitmask *  constant min-fp     \ minimum value
10 point lshift                 constant fp10

\ convert integer to fixed point
: i>fp ( n -- fp )            point lshift ;

\ convert fixed point to integer (rounded down)
: fp>i ( fp -- n )            point rshift ;

\ multiply two fixed point numbers
: fp* ( n0 n1 -- n2 )         fp-bitmask */ ;

\ divide two fixed point numbers
: fp/ ( n0 n1 -- n2 )         fp-bitmask swap */ ;

\ vector3 helper: convert 3 integers to 3 fixed point numbers
: i3>fp3 ( a b c -- a b c )   i>fp rot i>fp rot i>fp rot ;

\ vector3 helper: convert 3 fixed point numbers to 3 integers
: fp3>i3 ( a b c -- a b c )   fp>i rot fp>i rot fp>i rot ;

\ floor down fixed point number to nearest integer
: fpfloor ( n0 -- n1 )        fp-imask and ;

\ ceil up fixed point number to nearest integer
: fpceil ( n0 -- n1 )         fp-bitmask + fpfloor ;

\ round fixed point number to nearest integer
: fpround ( n0 -- n1 )        fp.bit/2 + fpfloor ;

\ return base 10 digits
: c10s ( a -- b )             0 begin 1+ swap 10 / swap over 0> 0=
                                  until swap drop ;

\ convert a decimal fractional number in the form integer fractional
: if>fp ( i f -- fp )
  dup i>fp swap
  c10s          ( i f 10s )
  0 do          ( i f )
    fp10 fp/
  loop
  swap i>fp +
;

\ supply the fractional digit count
: if2>fp ( i f c -- fp )
  >r ( i f )
  i>fp
  r> 0 do        ( i f )
    fp10 fp/
  loop
  swap i>fp +
;

previous definitions

[endif]
