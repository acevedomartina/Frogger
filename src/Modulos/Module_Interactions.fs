namespace Frogger.Modulos.Interactions

module Module_Interactions = 
    open Frogger.Modulos.Module_Grid
    open Frogger.Modulos.Module_Fondo
    open Frogger.Modulos.Module_Player
    
    ////////////////// Funciones de cheque colisión o ahogamiento del jugador en la posición actual /////////////////////////

    // Chequeamos que haya colisión entre el jugador y el obstáculo
    let checkCollision (player : Player) (obstacleU : Obstacle) : bool=
        // Si choca por la izquierda 

        let obstacle = matchObstacle obstacleU
        let collisionL = player.PosX + player.Width/2 >= obstacle.x_left && player.PosX + player.Width/2 <= obstacle.x_right
        let collisionR = player.PosX - player.Width/2 >= obstacle.x_left && player.PosX - player.Width/2 <= obstacle.x_right
        collisionL || collisionR

    // Chequeo si el jugador no está arriba de un tronco o una tortuga
    let checkNotUpLogTurtle (player : Player) (obstacleU : Obstacle) : bool = 
        // Si la tortuga no está sumergida veo que pasa
        match obstacleU with
        | Afloat obstacle -> if obstacle.x_left > obstacle.x_right then
                                // Si el extremo derecho del jugador está a la derecha del obstáculo me ahogo entonces devuelve true
                                // En caso contrario no me ahogo y devuelve false
                                (obstacle.x_right < player.PosX + player.Width / 2) && (obstacle.x_left > player.PosX - player.Width / 2)
                             else 
                                (obstacle.x_left > player.PosX - player.Width / 2) || (obstacle.x_right < player.PosX + player.Width / 2)
        | Underwater obstacle -> true
