namespace Frogger.Modulos

let WIDTH = 800
let HEIGHT = 600
let WIDTH_PLAYABLE_LEFT = 100 
let WIDTH_PLAYABLE_RIGHT = 700

module Functions = 
    open Frogger.Types

    // Función que mueve de una fila hacia la fila de arriba
    let moveUp (posY : Rows) = 
        match posY with
        | One _ -> Two
        | Two _ -> Three
        | Three _ -> Four
        | Four _ -> Five
        | Five _ -> Six
        | Six _ -> Seven
        | Seven _ -> Six
        | Eight _ -> Nine
        | Nine _ -> Ten
        | Ten _ -> Eleven
        | Eleven _ -> Twelve
        | Twelve _ -> Thirteen

    // Función que mueve de una fila hacia la fila de abajo
    let moveDown (posY : Rows) = 
        match posY with
        | One _ -> One
        | Two _ -> One
        | Three _ -> Two
        | Four _ -> Three
        | Five _ -> Four
        | Six _ -> Five
        | Seven _ -> Six
        | Eight _ -> Seven
        | Nine _ -> Eight
        | Ten _ -> Nine
        | Eleven _ -> Ten
        | Twelve _ -> Eleven

    // Función que mueve al jugador en la dirección indicada
    let movePlayer (player : Player) (dir: Direction)  = 
        match dir with
        | Up -> let newPosY = moveUp player.PosY
                {player with PosY = newPosY}
        | Down -> let newPosY = moveDown player.PosY
                  {player with PosY = newPosY}
        | Left -> if player.PosX - 50 < WIDTH_PLAYABLE_LEFT then player else {player with PosX = player.PosX - 50}    
        | Right -> if player.PosX + 50 > WIDTH_PLAYABLE_RIGHT then player else {player with PosX = player.PosX + 50}

    // Chequeamos que haya colisión entre el jugador y el obstáculo
    let checkCollision (player : Player) (obstacle : Obstacle) =
        return player.PosX + player.Width/2 > obstacle.x_left || player.PosX - player.Width/2 < obstacle.x_right

    let checkUpLogTurtle (player : Player) (obstacle : Obstacle) = 
        if not ostacle.Underwater then
            if obstacle.x_left < player.PosX - player.Width / 2 && obstacle.x_right > player.PosX + player.Width / 2 then
                true
            else if obstacle.x_left > obstacle.x_right then
                (obstacle.x_right > player.PosX + player.Width / 2)
            else if obstacle.x_left < player.PosX - player.Width / 2 then
                true
            else
                false
        
        else
            false

    let moveObstacle (obstacle : Obstacle) = 
        let x_left_new : int = (obstacle.x_left + WIDTH) % WIDTH 
        let x_right_new : int = (obstacle.x_right + WIDTH) % WIDTH

        {obstacle with x_left = x_left_new; x_right = x_right_new}

    let state_tortuga(tiempo: int ) (obstacle: Obstacle list) (idx : int) =
        if tiempo % 10 = 0 then
            {obstacle with Underwater = not Underwater}
        else
            obstacle

    let updateFondo (fondo : Fondo) = 
        let newObstacles = List.map moveObstacle fondo.Obstacles
        let newTime = fondo.Time - 1
        {fondo with Obstacles = newObstacles; Time = newTime}

    let updateGameState (game : gameState) (Option<Direction>) = 
            match Option<Direction> with
            | Some dir -> let newObstacles = List.map (moveObstacle game.Fondo.Obstacles)
                          let newFondo = {game.Fondo with Obstacles = newObstacles}
                          let newPlayer = movePlayer game.Player dir
                          {game with Player = newPlayer; Fondo = newFondo}
            | None -> let newObstacles = List.map (moveObstacle game.Fondo.Obstacles)
                      let newFondo = {game.Fondo with Obstacles = newObstacles}
                      {game with Fondo = newFondo}
        
    // Chequear si el jugador colisiona con algún obstáculo
    
    // Chequear si se acabó el tiempo

    // Chequear si se ahogó

    // Chequear si llegó a la meta

    // Chequear si Ganó el nivel
    
    let checkPlayerStatus (player : Player) (obstacle : Map<Rows, Obstacle list>) = 
        let row = player.posY
        match row with
        | One -> {player}
        | Two -> list.map (checkCollision player) (obstacle.[Two]) 

