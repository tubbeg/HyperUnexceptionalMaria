open Browser
open Fable.Core

type Position = {|x:float;y:float|}
type Velocity = {velocity:float}
type Health = {|health:int|}
type Monster = {|position:Position; health:Health;velocity:Velocity|}

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

[<Import("createMonsterQuery","./Queries.js")>]
let cmq (w : World) : Query<Monster>  = jsNative

let div = document.createElement "div"
div.innerHTML <- "hello hello hello!"
document.body.appendChild div |> ignore

let p : Position = {|x=3;y=4|}
let h : Health = {|health=75|}
let v = {velocity=1}

let w =
    let myWorld = new World()
    let e : Monster = {|velocity=v;position=p;health=h|}
    myWorld.create e
    myWorld


[<Emit("$0.query((e) => e.all(\"health\", \"position\", \"velocity\"))")>]
let query2 (w : World) : Query<Monster> = jsNative
let myMonsterQuery = query2 w

[<Emit("$0.entities")>]
let printEntities a = jsNative
console.log(myMonsterQuery)

let printQuery (a : Monster array) =
    printfn "%A" a
    console.log(a)

printQuery myMonsterQuery.entities
