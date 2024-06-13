namespace Frogger.Modulos

open Frogger.Modulos.Module_Grid

module Module_Player =

    // Tipo que define al jugador
    type Player =
        {
            PosX: int // Posición del centro del jugador
            PosY: Rows // Posición en la fila del jugador
            Width: int // Ancho en píxeles del jugador
        }

    // Ancho del salto
    let JUMP_WIDTH = 50

    // Tipo que define las direcciones posibles de movimiento
    type Direction =
    | Up
    | Down
    | Left
    | Right

    type LivesRemaining =
    | GameOver
    | OneLife
    | TwoLives
    | ThreeLives

    // FUNCIONES

    let moveUp (posY : Rows) = 
        match posY with
        | One -> Two
        | Two -> Three
        | Three -> Four
        | Four -> Five
        | Five -> Six
        | Six -> Seven
        | Seven -> Six
        | Eight -> Nine
        | Nine -> Ten
        | Ten -> Eleven
        | Eleven -> Twelve
        | Twelve -> Thirteen 
        | _ -> raise (System.Exception("Error no deberia pasar"))

    // Función que mueve de una fila hacia la fila de abajo
    let moveDown (posY : Rows) = 
        match posY with
        | One -> One
        | Two -> One
        | Three -> Two
        | Four -> Three
        | Five -> Four
        | Six -> Five
        | Seven -> Six
        | Eight -> Seven
        | Nine -> Eight
        | Ten -> Nine
        | Eleven -> Ten
        | Twelve -> Eleven
        | _ -> raise (System.Exception("Error no deberia pasar"))

    // Función que mueve al jugador en la dirección indicada
    let movePlayer (player : Player) (dir: Direction) : Player = 
        match dir with
        // Si se sale de la pantalla jugable no se mueve
        | Up -> let newPosY = moveUp player.PosY
                {player with PosY = newPosY}
        | Down -> let newPosY = moveDown player.PosY
                  {player with PosY = newPosY}
         // Si se sale de la pantalla jugable no se mueve
        | Left -> if player.PosX - JUMP_WIDTH < WIDTH_PLAYABLE_LEFT then player else {player with PosX = player.PosX - JUMP_WIDTH}
        | Right -> if player.PosX + JUMP_WIDTH > WIDTH_PLAYABLE_RIGHT then player else {player with PosX = player.PosX + JUMP_WIDTH} 