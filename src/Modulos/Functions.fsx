namespace Frogger.Modulos

let WIDTH = 800
let HEIGHT = 600

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
        | Left -> if player.PosX - 50 < 100 then player else {player with PosX = player.PosX - 50}    
        | Right -> if player.PosX + 50 > 700 then player else {player with PosX = player.PosX + 50}

    
    let checkCollision (player : Player) (obstacle : Obstacle) =
        let x_collision = (abs(player.PosX - obstacle.PosX) * 2) < (player.Width + obstacle.Width)
        let y_collision = player.PosY = obstacle.PosY
        x_collision && y_collision

    let moveObstacle (obstacle : Obstacle) = 
        { obstacle with PosX = obstacle.PosX + obstacle.Speed }
        
    let obstacle_inside (x_left : int, x_right: int) = 

        let x_left_new : int = (x_left + WIDTH) % WIDTH 
        let x_right_new : int = (x_right + WIDTH) % WIDTH
        

    let moveObstacle (obstacle : Obstacle) = 
        let x_left_new : int = (obstacle.x_left + WIDTH) % WIDTH 
        let x_right_new : int = (obstacle.x_right + WIDTH) % WIDTH

        {obstacle with x_left = x_left_new; x_right = x_right_new}

    let state_tortuga(tiempo: int, obstacle: Obstacle) = 
        if tiempo % 10 = 0 then
            {obstacle with Underwater = not Underwater}
        else
            obstacle

