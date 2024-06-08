namespace Frogger.Modulos

module Types = 

    type Grass = Grass
    type Water = Water
    type Road = Road
    
    type GoalSpace =
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
    | Thirteen

    type Player =
    {
        PosX: int
        PosY: Rows
        Width: int
    }

    type Direction =
    | Up
    | Down
    | Left
    | Right

    type Obstacle =
    {
        x_left: int
        x_right: int
        PosY: Rows
        Width: int
        Speed: int
        Underwater: bool
    }

    type LivesRemaining =
    | OneLife
    | TwoLives
    | ThreeLives

    type Fondo = 
    {
        Obstacles: Obstacle list
        Time: int
    }

    type Game =
    {
        Player: Player
        Final_row: Final_row
        Score: int
        Lifes: LivesRemaining
        Fondo: Fondo
    }