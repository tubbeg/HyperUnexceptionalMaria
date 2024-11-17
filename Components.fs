module Components
open Fable.Core
open AranciniInterop

type Position = {x:float;y:float}
type Velocity = {velocity:float}
type Health = {health:int}
type Monster = {|position:Position; health:Health;velocity:Velocity|}


[<Import("createMonsterQuery","./Queries.js")>]
let cmq (w : World) : Query<Monster>  = jsNative