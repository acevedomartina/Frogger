namespace Frogger.Modulos

module Types = 

    type Player
        | PosX of int
        | PosY of int
        | Height of int
        | Width of int
        | Water of bool

    type Turtle
        | PosX of int
        | PosY of int
        | Height of int
        | Width of int
        | Speed of int
        | Timer of int