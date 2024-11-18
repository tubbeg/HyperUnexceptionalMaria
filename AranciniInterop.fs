module AranciniInterop
open Fable.Core



[<Import("World","arancini")>]
type World () =
    class
        member this.create (entity : obj) = jsNative
        member this.add (entity : obj) (id : string) (prop : obj) = jsNative
    end

[<Import("EntityCollection","arancini")>]
type Query<'T> () =
    class
        member this.entities : array<'T> = jsNative 
        member this.version : int = jsNative
    end