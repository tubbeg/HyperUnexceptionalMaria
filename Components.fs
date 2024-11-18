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
type MonsterComp = {monster:string}
type PlayerComp = {player:string}
type GroundComp = {ground:string}

//Phasers built in Sprite class keeps track of positioning
//and also handles physics. No need for me to build
//my own physics system
type MonsterEntity =
    {|
    health:HealthComp;
    sprite:SpriteComp;
    monster:MonsterComp|}

type PlayerEntity =
    {|health:HealthComp;
    player:PlayerComp;
    sprite:SpriteComp|}

type GroundEntity =
    {|ground:GroundComp;
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


[<Import("createGroundQuery","./Queries.js")>]
let queryGroundEntities (w : World) : Query<GroundEntity>  = jsNative