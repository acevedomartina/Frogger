namespace Frogger.Modulos

open Frogger.Modulos.Grid

module Player =

    // Tipo que define al jugador
    type Player =
        {
            PosX: int // Posición del centro del jugador
            PosY: Rows // Posición en la fila del jugador
            Width: int // Ancho del jugador
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

    // Función que mueve una fila hacia arriba
    let moveUp (posY : Rows) : Rows= 
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
        | Thirteen -> Thirteen // Como uno no debería poder moverse en la fila 13, se queda en la fila 13

    // Función que mueve de una fila hacia la fila de abajo
    let moveDown (posY : Rows) : Rows= 
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
        | Thirteen -> Thirteen // Como uno no debería poder moverse en la fila 13, se queda en la fila 13

    // Funciones que devuelven los extremos del jugador
    let playerRight player = player.PosX + player.Width / 2
    let playerLeft player = player.PosX - player.Width / 2

    // Función que mueve al jugador en la dirección indicada
    let movePlayer (player : Player) (dir: Direction) : Player = 
        match dir with
        // Si se sale de la pantalla jugable no se mueve
        | Up -> let newPosY = moveUp player.PosY
                {player with PosY = newPosY}
        | Down -> let newPosY = moveDown player.PosY
                  {player with PosY = newPosY}
        | Left -> let nextPosX = player.PosX - JUMP_WIDTH
                  if nextPosX < WIDTH_PLAYABLE_LEFT then
                      player
                  else
                      {player with PosX = nextPosX}
        | Right -> let nextPosX = player.PosX + JUMP_WIDTH
                   if nextPosX > WIDTH_PLAYABLE_RIGHT then
                       player
                   else
                       {player with PosX = nextPosX}