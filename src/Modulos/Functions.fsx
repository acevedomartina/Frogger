namespace Frogger.Modulos

open Frogger.Modulos.Types

module Functions =

    // Ancho y alto de la pantalla
    let WIDTH = 800
    let HEIGHT = 600
    
    // Ancho de la parte jugable de la pantalla
    let WIDTH_PLAYABLE_LEFT = 100 
    let WIDTH_PLAYABLE_RIGHT = 700


    ////////////////// Funciones de movimiento del jugador y obstáculos /////////////////////////
    
    // Función que mueve de una fila hacia la fila de arriba
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
        | Up -> 
            let newPosY = moveUp player.PosY
            in {player with PosY = newPosY}
        | Down -> 
            let newPosY = moveDown player.PosY
            in {player with PosY = newPosY}
        // Si se sale de la pantalla jugable no se mueve
        | Left -> if player.PosX - 50 < WIDTH_PLAYABLE_LEFT then player else {player with PosX = player.PosX - 50}
        | Right -> if player.PosX + 50 > WIDTH_PLAYABLE_RIGHT then player else {player with PosX = player.PosX + 50}


    // Función que mueve un obstáculo y establece las condiciones periódicas de contorno
    let moveObstacle (obstacle: Obstacle) : Obstacle =
        let x_left_new : int = (obstacle.x_left + obstacle.Speed) % WIDTH 
        let x_right_new : int = (obstacle.x_right + obstacle.Speed) % WIDTH
        {obstacle with x_left = x_left_new; x_right = x_right_new}

    ////////////////// Funciones de cheque colisión o ahogamiento del jugador en la posición actual /////////////////////////
    let checkCollision (player : Player) (obstacle : Obstacle) : bool=
        // Si choca por la izquierda 
        let collisionL = player.PosX + player.Width/2 >= obstacle.x_left && player.PosX + player.Width/2 <= obstacle.x_right
        let collisionR = player.PosX - player.Width/2 >= obstacle.x_left && player.PosX - player.Width/2 <= obstacle.x_right
        collisionL || collisionR

    // Chequeo si el jugador no está arriba de un tronco o una tortuga
    let checkNotUpLogTurtle (player : Player) (obstacle : Obstacle) : bool = 
        // Si la tortuga no está sumergida veo que pasa
        if not obstacle.Underwater then
            if obstacle.x_left > obstacle.x_right then
                // Si el extremo derecho del jugador está a la derecha del obstáculo me ahogo entonces devuelve true
                // En caso contrario no me ahogo y devuelve false
                (obstacle.x_right < player.PosX + player.Width / 2) && (obstacle.x_left > player.PosX - player.Width / 2)
            else 
                (obstacle.x_left > player.PosX - player.Width / 2) || (obstacle.x_right < player.PosX + player.Width / 2)    
        else
            true
    
    // Función que actualiza la cantidad de vidas restantes
    let updateLives (game : GameState) : GameState = 
        let lifes: LivesRemaining= game.Lifes
        match lifes with
        | GameOver -> game
        | OneLife -> {game with Lifes = GameOver}
        | TwoLives -> {game with Lifes = OneLife}
        | ThreeLives -> {game with Lifes = TwoLives}


