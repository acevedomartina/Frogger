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
        // Si choca por la izquierda 
        let collisionL = player.PosX + player.Width/2 >= obstacle.x_left && player.PosX + player.Width/2 <= obstacle.x_right
        let collisionR = player.PosX - player.Width/2 >= obstacle.x_left && player.PosX - player.Width/2 <= obstacle.x_right
        collisionL || collisionR

    // Chequeo si el jugador no está arriba de un tronco o una tortuga
    let checkNotUpLogTurtle (player : Player) (obstacle : Obstacle) : bool = 
        // Si la tortuga no está sumergida veo que pasa
        if not obstacle.Underwater then
            if obstacle.x_left > obstacle.x_right then
            if obstacle.x_left > obstacle.x_right then
                // Si el extremo derecho del jugador está a la derecha del obstáculo me ahogo entonces devuelve true
                // En caso contrario no me ahogo y devuelve false
                (obstacle.x_right < player.PosX + player.Width / 2) && (obstacle.x_left > player.PosX - player.Width / 2)
            else 
                (obstacle.x_left > player.PosX - player.Width / 2) || (obstacle.x_right < player.PosX + player.Width / 2)    
                (obstacle.x_right < player.PosX + player.Width / 2) && (obstacle.x_left > player.PosX - player.Width / 2)
            else 
                (obstacle.x_left > player.PosX - player.Width / 2) || (obstacle.x_right < player.PosX + player.Width / 2)    
        else
            true

    ////////////////// Funciones de actualización del fondo /////////////////////////

    // Función para cambiar el estado de la tortuga i-ésima cada 10 segundos
    let changeTurtleState (tiempo : int) (turtle : Obstacle) : Obstacle = 
        if tiempo % 10 = 0 then
            {turtle with Underwater = not turtle.Underwater}
        else
            turtle
        
    // Función para actualizar el fondo del juego, los obstáculos y el estado de las tortugas
    let updateFondo (fondo : Fondo) = 
        // Cambiamos todos los obstáculos moviendolos
        let movedObstacles = Map.map (fun _ lst -> lst |> List.map moveObstacle) fondo.Obstacles
        
        // Cambiamos el estado de las tortugas en las filas 8 y 11 cada 10 segundos
        let idx = 1 // Como hay dos y tres tortugas en las filas 8 y 11 respectivamente cambiamos el estado de la segunda tortuga en cada fila

        // Tomamos el mapa de Rows, Obstacle list
        // A cada lista de obstáculos extraemos los valoes key y lst
        // A la lista lst le aplicamos un map en donde si la key es Rows.Eight o Rows.Eleven entonces cambiamos el estado de la tortuga idx-ésima
        // En caso contrario devolvemos el obstáculo
        let updatedObstacles = movedObstacles |> Map.map (fun k lst -> lst |> List.mapi (fun j obs -> if (k = Rows.Eight || k = Rows.Eleven) && j = idx then changeTurtleState fondo.Time obs else obs))
        // Return the updated fondo
        {fondo with Obstacles = updatedObstacles; Time = fondo.Time - 1}


    ////////////////// Funciones que auxiliares para la actualización del juego /////////////////////////

    // Chequear si se acabó el tiempo
    let CheckTime (game: GameState) : GameState = 
        if game.Fondo.Time = 0 then
            let newPlayer = {game.Player with PosX = WIDTH/2; PosY = Rows.One}
            let newGame = updateLives game
            {newGame with Player = newPlayer}
        else
            game
    
    // Función que chequea si el jugador llegó a alguna meta
    let CheckWinAux (player : Player) (goal_space : GoalSpace) : GoalSpace = 
        let player_xright = player.PosX + player.Width / 2
        let player_xleft = player.PosX - player.Width / 2

    if player_xleft > goal_space.PosX && player_xright < goal_space.PosX + goal_space.Width then
        { goal_space with Ocupation = true }          
    else
        { goal_space with Ocupation = false }

// Función que chequea si el jugador llegó a alguna meta
let CheckGoal (game : GameState) =
    let player = game.Player
    let goal_spaces = game.Final_row
    let updatedGoalSpaces = List.map (CheckWinAux player) goal_spaces
    let anyGoalSpaceChanged = List.exists (fun gs -> gs.Ocupation) updatedGoalSpaces

        if anyGoalSpaceChanged then
            { game with Final_row = updatedGoalSpaces; Score = game.Score + 50}
        else
            updateLives game
    
    // Función que actualiza la cantidad de vidas restantes
    let updateLives (game : GameState) : GameState = 
        let lifes: LivesRemaining= game.Lifes
        match lifes with
        | GameOver -> game
        | OneLife -> {game with Lifes = GameOver}
        | TwoLives -> {game with Lifes = OneLife}
        | ThreeLives -> {game with Lifes = TwoLives}


