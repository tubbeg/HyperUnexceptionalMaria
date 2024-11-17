open Browser
open Fable.Core
open AranciniInterop
open Config
open Components

let div = document.createElement "div"
div.innerHTML <- "hello hello hello!"
document.body.appendChild div |> ignore

let p : Position = {x=3;y=4}
let h : Health = {health=75}
let v = {velocity=1}

let w =
    let myWorld = new World()
    let e : Monster = {|velocity=v;position=p;health=h|}
    myWorld.create e
    myWorld

let myMonsterQuery = cmq w

let printQuery (a : Monster array) =
    printfn "%A" a
    console.log(a)
    let printObject (e : Monster) =
        printfn "Printing object"
        console.log e
    a |> Array.map(fun e -> (printObject e))

printQuery myMonsterQuery.entities |> ignore


let defaultConfig scene =
    conf (800,600) scene physics