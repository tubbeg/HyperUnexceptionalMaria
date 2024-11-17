module Config
open PhaserInterop
open Fable.Core.JsInterop

let arcade = {|gravity={|y=200|}|}

let physics =
    createObj [
        "default","arcade";
        "arcade", arcade
    ]

let conf (x,y) (c : array<Scene>) p =
    createObj [
        "type", auto
        "width", x
        "height", y
        "scene", c
        "physics",p
    ]

