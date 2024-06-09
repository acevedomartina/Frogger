namespace Frogger.Modulos

module Types =     
    type GoalSpace =
    {
        PosX: int
        Width: int
        Ocupation: bool
    }

    type Rows = 
    | One
    | Two
    | Three
    | Four
    | Five
    | Six
    | Seven
    | Eight
    | Nine
    | Ten
    | Eleven
    | Twelve
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
        Obstacles: Map<Rows, Obstacle list>
        Time: int
    }

    type GameState =
    {
        Player: Player
        Final_row: GoalSpace list
        Score: int
        Lifes: LivesRemaining
        Fondo: Fondo
    }