marker ---marker---

( argv0 ) constant input-file

require string.fs
require list.fs

also list.fs
also string.fs

begin-structure doc:struct
  field: doc:word
  field: doc:comment
  field: doc:stack-effect
end-structure

variable 'fd
variable 'src
variable #src

: read       begin here 4096 'fd @ read-file throw dup allot 0= until ;
: open       input-file r/o open-file throw 'fd ! ;
: close      'fd @ close-file throw ;
: start      here 'src ! ;
: finish     here 'src @ - #src ! ;
: load-file  open start read finish close ;

\ returns a string marking the beginning of a docgen item; newline followed by a "\" character.
: docgen-marker s\" \n\\ " string:create ;

: doc-allot here doc:struct allot ;

\ Make a doc:struct from the provided word-name, comment and stack-effect
: doc-make ( word-name comment stack-effect -- doc )
  doc-allot dup >r doc:stack-effect ! r@ doc:comment ! r@ doc:word ! r> ;

\ Takes a doc:struct and prints it in markdown
: doc-render-item ( doc -- )
  ." ## `" dup doc:word @ string:print space
           dup doc:stack-effect @ string:print ." `" cr

  doc:comment @ string:print cr cr
;

\ return the index of docgen marker from the given source string
: find-docgen-marker ( src -- index ) docgen-marker string:index-of ;

: parse-comment
  { src index }

  src index src string:length + @ string:substring to src

  \ find index of char after newline
  src 10 string:from-char string:index-of 1+ to index

  src 0 index string:substring { comment }

  src index comment
;

: parse-word-name
  { src index }

  \ skip newline and comma after comment line
  index 2 + to index

  src index src string:length + @ string:substring to src

  \ find index of space or newline
  src 32 string:from-char string:index-of { index1 }
  src 10 string:from-char string:index-of { index2 }

  \ use nearest
  index1 index2 min to index

  src 0 index 1+ string:substring { word-name }

  src index word-name
;

: parse-stack-effect
  { src index }

  index 1 + to index

  \ if not "(" then exit early
  src index string:nth 40 <> if src index s" " string:create exit then

  src index src string:length + @ string:substring to src

  src 41 string:from-char string:index-of 2 + to index
  
  src 0 index string:substring { stack-effect }

  src index stack-effect
;

: doc-render 
  ['] doc-render-item over list:for-each ;

: parse-words
  { src word-list }

  begin
    src find-docgen-marker { index }

    index -1 <>
  while
    \ skip over the marker
    index docgen-marker string:length + @ + to index

    src index parse-comment { comment } to index to src

    src index parse-word-name { word-name } to index to src

    src index parse-stack-effect { stack-effect } to index to src

    word-name comment stack-effect doc-make { doc }

    word-list doc list:append
  repeat
;

: docgen
  load-file

  'src @ #src @ string:create { src }

  list:create { word-list }

  src word-list parse-words

  word-list doc-render
;

docgen

---marker---

bye
