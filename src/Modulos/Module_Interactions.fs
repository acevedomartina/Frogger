namespace Frogger.Modulos
open Frogger.Modulos.Module_Grid
open Frogger.Modulos.Module_Fondo
open Frogger.Modulos.Module_Player

module Module_Interactions =     
    ////////////////// Funciones de chequeo de colisión o ahogo del jugador en la posición actual /////////////////////////

    // Chequeamos que haya colisión entre el jugador y el obstáculo
    let checkCollision (player : Player) (obstacleU : Obstacle) : bool=
        // No nos interesa si el obstáculo está arriba o abajo del agua, solo si hay colisión
        let obstacle = matchObstacle obstacleU
        // Si choca por la izquierda o por la derecha
        let collisionL = player.PosX + player.Width/2 >= obstacle.x_left && player.PosX + player.Width/2 <= obstacle.x_right
        let collisionR = player.PosX - player.Width/2 >= obstacle.x_left && player.PosX - player.Width/2 <= obstacle.x_right
        collisionL || collisionR

    // Chequeo si el jugador no está arriba de un tronco o una tortuga
    let checkNotUpLogTurtle (player : Player) (obstacleU : Obstacle) : bool = 
        match obstacleU with
        // Si el obstaculo no está sumergido veo que pasa
        | Afloat obstacle -> if obstacle.x_left > obstacle.x_right then
                                // Si el extremo derecho del jugador está a la derecha del obstáculo me ahogo entonces devuelve true
                                // En caso contrario no me ahogo y devuelve false
                                (obstacle.x_right < player.PosX + player.Width / 2) && (obstacle.x_left > player.PosX - player.Width / 2)
                             else 
                                (obstacle.x_left > player.PosX - player.Width / 2) || (obstacle.x_right < player.PosX + player.Width / 2)
        | Underwater obstacle -> true