require string.fs
require list.fs

: string:debug:print ( string -- )
  { string }

  cr ." string = " string hex. ." { "
  cr ."  data: " string string:data@ hex.
  cr ."  length: " string string:length@ .
  cr ." }"
;

: run-test-0
     ." string:make, string:print"
  cr s" Hello, world!" string:make string:print
  cr s" 123" string:make string:to-number drop .

  cr ." string:tokenize, string:for-each"
  s" A,BC,DEF,GHIJ" string:make { str }
  [char] , str string:tokenize
  [: cr ." Token:" string:print ;] swap list:for-each

  cr ." char at index 4 is: " s" Hello, world" string:make 4 string:nth emit

  cr ." string chars 123 reduced to: "

  42 \ pass to xt
    s" 123" string:make
    0
    [: { foobar acc char } foobar foobar ;]
    string:reduce .
;

run-test-0

bye
