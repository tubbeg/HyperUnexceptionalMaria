module PhaserInterop
open Fable.Core
open System


[<Import("Events.EventEmitter","phaser")>]
type EventEmitter () =
    class
    end

[<Import("Pointer","phaser")>]
type Pointer () =
    class
        member this.leftButtonReleased () : bool = jsNative
        member this.rightButtonReleased () : bool = jsNative
        member this.middleButtonReleased () : bool = jsNative
        member this.forwardButtonReleased () : bool = jsNative
        member this.backButtonReleased () : bool = jsNative
        member this.leftButtonDown () : bool = jsNative
        member this.rightButtonDown () : bool = jsNative
        member this.middleButtonDown () : bool = jsNative
        member this.forwardButtonDown () : bool = jsNative
        member this.backButtonDown () : bool = jsNative
    end

[<Import("Camera","phaser")>]
type Camera () =
    class
        member this.centerY : int = jsNative
        member this.centerX : int = jsNative
    end


[<Import("CameraManager","phaser")>]
type CameraManager () =
    class
        member this.main : Camera = jsNative
    end

[<Import("KeyboardPlugin","phaser")>]
type Keyboard () =
    class
        member this.on (key : string) (fn : obj -> unit ) = jsNative
    end
    

[<Import("InputPlugin","phaser")>]
type Input () =
    class
        member this.setDraggable s b  = jsNative
        member this.on event (funct )  = jsNative
        member this.keyboard : Keyboard = jsNative
    end

[<Import("GameObject","phaser")>]
type IGameObject =
        // be careful when setting
    abstract setPosition: x: int -> y: int -> unit
    abstract setData: k: string -> v: string ->   unit
    abstract getData : k : string ->  string
    abstract input : Input



[<Import("Arcade.Body","phaser")>]
type ArcadeBody () =
    class
        member this.allowGravity
            with get () : bool = jsNative
            and set (b : bool) = jsNative
    end




[<Import("Sprite","phaser")>]
type Sprite (scene,x,y,texture,frame) =
    class
        interface IGameObject with
            member this.setPosition x y = jsNative
            member this.setData k v = jsNative
            member this.getData k  = jsNative
            member this.input = jsNative
        member val x = 0 with get,set
        member val y = 0 with get,set
        //NOTE! THIS WILL NOT WORK WITH matter.js!!!
        //REPLACE THE LINE BELOW WITH AN IMPORT FROM MATTER!
        member this.body : ArcadeBody = jsNative
        member this.originX : int = jsNative
        member this.originy : int = jsNative
        member this.width : int = jsNative
        member this.height : int = jsNative
        member val name = "" with get,set
        member this.setOrigin x y  = jsNative
        member this.setTint value = jsNative
        member this.clearTint () = jsNative
        member this.setInteractive ()  = jsNative
    end

[<Import("SpriteWithDynamicBody","phaser")>]
type DyanmicSprite (scene,x,y,texture,frame) =
    class
        inherit Sprite(scene, x ,y, texture, frame)
        member this.setGravityY y  = jsNative
        member this.setImmovable bool  = jsNative
    end

[<Import("Container","phaser")>]
type Container (scene,x,y,children) =
    class
        interface IGameObject with
            member this.setPosition x y = jsNative
            member this.setData k v = jsNative
            member this.getData k  = jsNative
            member this.input = jsNative
        member val x = 0 with get,set
        member val y = 0 with get,set
        member val name = "" with get,set
        member this.add (child : IGameObject array) = jsNative
        member this.setVisible (b : bool) = jsNative
        member this.addToDisplayList (l : (obj array) option) = jsNative
        member this.setInteractive ()  = jsNative
        member this.list
            with get () : IGameObject array = jsNative
            and set (value : IGameObject array) = jsNative
    end


[<Import("GameObjectFactory","phaser")>]
type ObjFactory () =
    class
        member this.image x y (id : string)  : unit = jsNative
        member this.sprite x y (id : string) : Sprite = jsNative
        member this.container x y children : Container = jsNative
    end

[<Import("AUTO","phaser")>]
let auto : int = jsNative


[<Import("LoaderPlugin","phaser")>]
type Loader () =
    class
        member this.setBaseURL url : unit = jsNative
        member this.image (id : string) (url : string) : unit = jsNative
    end

[<Import("TweenManager","phaser")>]
type TweenManager () =
    class
        member this.add config = jsNative
    end

[<Import("Arcade.Collider","phaser")>]
type Collider () =
    class
    end


[<Import("Arcade.Factory","phaser")>]
type ArcadeFactory () =
    class
        member this.sprite x y (id : string) : DyanmicSprite = jsNative
        member this.collider (a : IGameObject) (b : IGameObject) : Collider = jsNative
    end

[<Import("ArcadePhysics","phaser")>]
type Physics () =
    class
        member this.add : ArcadeFactory = jsNative
    end

[<Import("Systems","phaser")>]
type Systems () =
    class
        member this.canvas  = jsNative
    end


type SceneConfig = {key:string option;active:bool option;}

[<Import("Scene","phaser")>]
type Scene (config : SceneConfig) =
    class
        member this.add : ObjFactory = jsNative
        member this.load : Loader = jsNative
        member this.input : Input = jsNative
        member this.physics : Physics = jsNative
        member this.cameras : CameraManager = jsNative
        member this.sys : Systems = jsNative
        member this.tweens : TweenManager = jsNative
        abstract preload : unit -> unit 
        abstract create : unit -> unit 
        abstract update : int -> float -> unit 
        default this.preload () : unit = jsNative
        default this.create () : unit = jsNative
        default this.update e dt : unit = jsNative
    end

[<Import("Game","phaser")>]
type Game (conf) =
    class
    end