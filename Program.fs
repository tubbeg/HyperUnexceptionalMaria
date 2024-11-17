open Browser
open Fable.Core
open AranciniInterop
open Config
open Components
open PhaserInterop

let div = document.createElement "div"
div.innerHTML <- "hello hello hello!"
document.body.appendChild div |> ignore

let p  = {x=3;y=4}
let h  = {health=75}
let v = {velocity=1}

let w =
    let myWorld = new World()
    let e : MonsterEntity = {|velocity=v;position=p;health=h;monster={monster=true}|}
    myWorld.create e
    myWorld

let myMonsterQuery = queryMonsterEntities w

let printQuery (a : MonsterEntity array) =
    printfn "%A" a
    console.log(a)
    let printObject (e : MonsterEntity) =
        printfn "Printing object"
        console.log e
    a |> Array.map(fun e -> (printObject e))

printQuery myMonsterQuery.entities |> ignore


let defaultConfig scene =
    conf (800,600) scene physics

let addPlayerEntity (w : World) (factory : ObjFactory)  =
    let x, y = 400, 400
    let sprite = factory.sprite x y "player"
    let player : PlayerEntity =    
     {| position = {x=y;y=y};
        health={health=100};
        velocity={velocity=0};
        player={player=true}
        sprite={sprite=sprite}|}
    w.create player
    w

let initializeWorld factory   (w : World)  =
    addPlayerEntity w factory

let systemCreatePlayer (w : World) =
    ()


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
    

type MyScene (conf) =
    inherit Scene(conf)
    let mutable world : World option = None
    let mutable timeTrigger : float = 0
    let mutable trigger = true
    override this.create (): unit =
        let factory = this.add
        world <- new World() |> initializeWorld factory |> Some
        ()
    override this.preload (): unit =
        this.load.image "player" "./MySprite.png"
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
    let c = defaultConfig [|(new MyScene(sceneConf))|]
    let app = new Game(c)
    ()

launchApp ()