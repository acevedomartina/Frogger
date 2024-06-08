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
        | One _ -> Two Road
        | Two _ -> Three Road
        | Three _ -> Four Road
        | Four _ -> Five Road
        | Five _ -> Six Road
        | Six _ -> Seven Grass
        | Seven _ -> Six Road
        | Eight _ -> Nine Water
        | Nine _ -> Ten Water
        | Ten _ -> Eleven Water
        | Eleven _ -> Twelve Water
        | Twelve _ -> Thirteen

    // Función que mueve de una fila hacia la fila de abajo
    let moveDown (posY : Rows) = 
        match posY with
        | One _ -> One Grass
        | Two _ -> One Grass
        | Three _ -> Two Road
        | Four _ -> Three Road
        | Five _ -> Four Road
        | Six _ -> Five Road
        | Seven _ -> Six Road
        | Eight _ -> Seven Grass
        | Nine _ -> Eight Water
        | Ten _ -> Nine Water
        | Eleven _ -> Ten Water
        | Twelve _ -> Eleven Water

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
        if obstacle.Underwater then
            if obstacle.x_left < player.PosX - player.Width / 2 && obstacle.x_right > player.PosX + player.Width / 2 then
                return true
            else if obstacle.x_left > obstacle.x_right then
                return (obstacle.x_right > player.PosX + player.Width / 2)
            else if obstacle.x_left < player.PosX - player.Width / 2 then
                return true
            else
                return false
        
        else
            return false

    let moveObstacle (obstacle : Obstacle) = 
        { obstacle with PosX = obstacle.PosX + obstacle.Speed }

    let moveTurtle (turtle : Turtle) = 
        { turtle with PosX = turtle.PosX + turtle.Speed }

    