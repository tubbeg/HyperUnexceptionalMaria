module Components
open Fable.Core
open AranciniInterop
open PhaserInterop

//the queried entities must be anonymous records, because
//they translate directly into objects. The queries uses
//the key identifier in the object
type PositionComp = {x:float;y:float}
type VelocityComp = {velocity:float}
type HealthComp = {health:int}
type SpriteComp = {sprite:Sprite}
type MonsterComp = {monster:bool}
type PlayerComp = {player:bool}

type MonsterEntity =
    {|position:PositionComp;
    health:HealthComp;
    velocity:VelocityComp;
    monster:MonsterComp|}

type PlayerEntity =
    {|position:PositionComp;
    health:HealthComp;
    velocity:VelocityComp;
    player:PlayerComp;
    sprite:SpriteComp|}


(*
due to the way that js works, querying any type is not a problem.
It's one of the advantages of dynamic typing. This will not work in
F# where typing is very strict. That's why I need to either import
query functions and specify a suitable return type, or use Fable Emit.
*)
[<Import("createMonsterQuery","./Queries.js")>]
let queryMonsterEntities (w : World) : Query<MonsterEntity>  = jsNative


[<Import("createPlayerQuery","./Queries.js")>]
let queryPlayerEntities (w : World) : Query<PlayerEntity>  = jsNative