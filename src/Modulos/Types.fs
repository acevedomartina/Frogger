namespace Frogger.Modulos

module Types =
    
    // Tipo que define las filas del juego
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

    // Tipo que define al jugador
    type Player =
        {
            PosX: int // Posición del centro del jugador
            PosY: Rows // Posición en la fila del jugador
            Width: int // Ancho en píxeles del jugador
        }

    // Tipo que define las direcciones posibles de movimiento
    type Direction =
    | Up
    | Down
    | Left
    | Right

    // Tipo que define un obstáculo
    type ObstacleBase =
        {
            x_left: int // Extremo izquierdo del obstáculo
            x_right: int // Extremo derecho del obstáculo
            PosY: Rows // Posición de la fila del obstáculo
            Speed: int // Velocidad del obstáculo
        }

    type Obstacle =
        | Afloat of ObstacleBase
        | Underwater of ObstacleBase

    // Tipo que define la cantidad de vidas restantes
    type LivesRemaining =
    | GameOver
    | OneLife
    | TwoLives
    | ThreeLives

    // Tipo que define el fondo del juego
    type Fondo = 
        {
            Obstacles: Map<Rows,List<Obstacle>> // Cada fila tiene una lista de obstáculos distinta
            Time: int // Temporizador actual del juego
        }

    // Tipo que define cada espacio de meta
    type GoalSpace =
        {
            PosX: int // Posición del centro del espacio de meta
            Width: int // Ancho en píxeles del espacio de meta
            Ocupation: bool // Indica si el espacio de meta está ocupado
        }

    // Tipo que define el estado actual del juego
    type GameState =
        {
            Player: Player // Jugador
            Final_row: List<GoalSpace> // Espacios de meta
            Score: int // Puntaje actual
            Lifes: LivesRemaining // Vidas restantes
            Fondo: Fondo // Fondo del juego
        }