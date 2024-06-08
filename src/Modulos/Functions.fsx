namespace Frogger.Modulos

let WIDTH = 800
let HEIGHT = 600

module Functions = 
    open Frogger.Types

    let movePlayer (player : Player) (dir : Direction) = 
        match dir with
        | Up -> { player with PosY = player.PosY - 50 }
        | Down -> { player with PosY = player.PosY + 50 }
        | Left -> { player with PosX = player.PosX - 50 }
        | Right -> { player with PosX = player.PosX + 50 }

    let moveObstacle (obstacle : Obstacle) = 
        { obstacle with PosX = obstacle.PosX + obstacle.Speed }

    let checkCollision (player : Player) (obstacle : Obstacle) =
        let x_collision = (abs(player.PosX - obstacle.PosX) * 2) < (player.Width + obstacle.Width)
        let y_collision = (abs(player.PosY - obstacle.PosY) * 2) < (player.Height + obstacle.Height)

        x_collision && y_collision

    let checkBorderPlayer (player : Player) = 
        match player.PosX, player.PosY with
    

    

