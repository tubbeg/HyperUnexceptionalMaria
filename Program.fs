open Browser
open Fable.Core
open AranciniInterop
open Config
open Components
open PhaserInterop


(*
This game is just supposed to be a very
short platformer, essentially just to demo
the functionality of F# Fable + Phaser + Arancini


I will need at least 2 scenes:
    * UI
    * Game objects
Maybe one additional scene for the environment
*)

let div = document.createElement "div"
div.innerHTML <- "hello hello hello!"
document.body.appendChild div |> ignore

let p  = {x=3;y=4}
let h  = {health=75}
let v = {velocity=1}

let defaultConfig scene =
    conf (800,600) scene physics

let addCollideToSprite (t : Scene) sprite w =
    let groundEntities= queryGroundEntities w
    let accFN (w : World) (e : GroundEntity) =
        let c = t.physics.add.collider (e.sprite.sprite) sprite
        w
    //could do this function without fold, it's a side effect
    groundEntities.entities |> Array.fold accFN w

let addGroundEntitity pos (this : Scene)  (w : World) =
    let x, y = pos
    let id = "ground"
    let sprite = 
        this.physics.add.sprite x y id
    sprite.setImmovable true
    sprite.body.allowGravity <- false
    sprite.x <- sprite.x + (sprite.width / 2)
    let next = sprite.width + x
    let player : GroundEntity =
        let s = {sprite=sprite}
        let g = {ground=id}
        {|ground=g;sprite=s|}
    w.create player
    w,next

[<Emit("$0.sys.canvas.width")>]
let getSceneWidth scene : int = jsNative

let addGroundEntities (this : Scene)  (w : World) =
    let width = getSceneWidth this
    let initPos = (0, 550)
    let rec age world p  =
        let x,y = p
        let newWorld,next = addGroundEntitity p  this world
        match x with
        | n when (n >= width) -> newWorld
        | _ -> age newWorld (next, y)
    age w initPos

let addPlayerEntity (this : Scene) (w : World)  =
    //I know that using this (the scene instance) is bad practice
    //It would be a lot easier if Phaser was more functional-oriented
    let x, y = 400, 400
    let id = "player"
    let sprite = 
        this.physics.add.sprite x y id
    sprite.setGravityY 50
    let player : PlayerEntity =
        let s = {sprite=sprite}
        let p = {player=id}
        let h = {health=100}
        {|health=h;player=p;sprite=s|}
    w.create player
    addCollideToSprite this sprite w

let initializeWorld this   (w : World)  =
    w |> addGroundEntities this |> addPlayerEntity this

let tintPlayer (p : PlayerEntity) =
    p.sprite.sprite.setTint "0xfb00ff"

let clearTintPlayer (p : PlayerEntity) =
    p.sprite.sprite.clearTint ()

let tintPlayersSystem (w : World option) isTint =
    //good example of a system with side effects
    match w with
    | Some world ->
        let players = queryPlayerEntities world
        match isTint with
        | true ->
            for player in players.entities do
                tintPlayer player
        | false ->
            for player in players.entities do
                clearTintPlayer player
    | _ -> ()
    

type ForegroundScene (conf) =
    inherit Scene(conf)
    let mutable world : World option = None
    let mutable timeTrigger : float = 0
    let mutable trigger = true
    override this.create (): unit =
        this.input.keyboard.on "keydown-A" (fun (e) -> (printfn "A is down! %A" e))
        world <- new World() |> initializeWorld this |> Some
        ()
    override this.preload (): unit =
        this.load.image "player" "./MySprite.png"
        this.load.image "ground" "./MySprite.png"
    override this.update (e: int) (dt: float): unit =
        timeTrigger <- timeTrigger + dt
        match timeTrigger with
        | n when (n > 1000) -> 
            timeTrigger <- 0
            trigger <- (not trigger)
            tintPlayersSystem world trigger
        | _ -> ()

let launchApp () =
    let sceneConf = {active=Some true;key=Some "MyScene"}
    let c = defaultConfig [|(new ForegroundScene(sceneConf))|]
    let app = new Game(c)
    ()

launchApp () 