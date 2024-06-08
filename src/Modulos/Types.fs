namespace Frogger.Modulos

module Types = 

    type Player =
    {
        PosX: int
        PosY: int
        Height: int
        Width: int
        Water: bool
    }

    type Move =
    | Up
    | Down
    | Left
    | Right

    type Obstacle =
    {
        PosX: int
        PosY: int
        Height: int
        Width: int
        Speed: int
    }

    type Final_row =
    {
        PosX: int
        Width: int
        Ocupation: bool
    }