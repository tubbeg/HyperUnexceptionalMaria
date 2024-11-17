open Browser
open Fable.Core

type Position = float * float
type Entity =
    {position:Position;}


[<Import("entity","./Entity.js")>]
let entity : Entity = jsNative


let div = document.createElement "div"
div.innerHTML <- "hello hello hello!"
document.body.appendChild div |> ignore