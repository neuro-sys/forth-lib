marker ---marker---

( argv0 ) constant input-file

require string.fs
require list.fs

begin-structure doc:struct
  field: doc:word
  field: doc:comment
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

: docgen-marker s\" \n\\ " string:make ;

: doc-make   here doc:struct allot ;

\ ( doc -- )
: doc-render-item
  ." ## `" dup doc:word @ string:print ." `" cr

  \ ." ```" cr
  doc:comment @ string:print cr cr
  \ ." ```" cr
;

\ ( word-list -- ) 
: doc-render 
  ['] doc-render-item over list:for-each ;

: docgen
  load-file

  'src @ #src @ string:make { src }

  list:make { word-list }

  begin
    src docgen-marker string:index-of { index }

    index -1 <>
  while
    index docgen-marker string:length @ + to index

    src index src string:length @ string:substring to src

    src 10 string:from-char string:index-of { end }

    src 0 end 1+ string:substring { comment }

    index end 3 + to index

    src index src string:length @ string:substring to src

    src 32 string:from-char string:index-of { end1 }
    src 10 string:from-char string:index-of { end2 }

    end1 end2 min to end

    src 0 end 1+ string:substring { word-name }

    doc-make { doc }

    word-name doc doc:word !
    comment doc doc:comment !

    word-list doc list:append

    index 1+ to index
  repeat

  word-list doc-render
;

docgen

---marker---

bye
