namespace Frogger.Modulos

module Types = 

    type Grass = Grass
    type Water = Water
    type Road = Road
    
    type Final_row =
    {
        PosX: int
        Width: int
        Ocupation: bool
    }

    type Rows = 
    | One of Grass
    | Two of Road
    | Three of Road
    | Four of Road
    | Five of Road
    | Six of Road
    | Seven of Grass
    | Eight of Water
    | Nine of Water
    | Ten of Water
    | Eleven of Water
    | Twelve of Water
    | Thirteen of Final_row

    type Player =
    {
        PosX: int
        PosY: Rows
        Width: int
        Water: bool
    }

    type Direction =
    | Up
    | Down
    | Left
    | Right

    type Obstacle =
    {
        PosX: int
        PosY: Rows
        Width: int
        Speed: int
    }

    type Trurtle = 
    {
        PosX: int
        PosY: Rows
        Width: int
        Speed: int
        Timer: int
    }

    type Game =
    {
        Player: Player
        Obstacles: Obstacle list
        Final_row: Final_row
        Sectors: Sector list
        Score: int
        GameOver: bool
    }