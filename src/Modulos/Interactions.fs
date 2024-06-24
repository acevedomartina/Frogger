namespace Frogger.Modulos
open Frogger.Modulos.Grid
open Frogger.Modulos.Fondo
open Frogger.Modulos.Player

module Interactions =     
    ////////////////// Funciones de chequeo de colisión o ahogo del jugador en la posición actual /////////////////////////

    // Chequeamos que haya colisión entre el jugador y el obstáculo
    let checkCollision (player : Player) (obstacleU : Obstacle) : bool=
        // No nos interesa si el obstáculo está arriba o abajo del agua, solo si hay colisión
        let obstacle = matchObstacle obstacleU
        // Si choca por la izquierda o por la derecha
        let collisionL = playerRight player >= obstacle.x_left && playerRight player <= obstacle.x_right
        let collisionR = playerLeft player >= obstacle.x_left && playerLeft player <= obstacle.x_right
        collisionL || collisionR

    // Chequeo si el jugador no está arriba de un tronco o una tortuga
    let checkNotUpLogTurtle (player : Player) (obstacleU : Obstacle) : bool = 
        match obstacleU with
        // Si el obstaculo no está sumergido veo que pasa
        | Afloat obstacle -> if obstacle.x_left > obstacle.x_right then
                                // Si el extremo derecho del jugador está a la derecha del obstáculo me ahogo entonces devuelve true
                                // En caso contrario no me ahogo y devuelve false
                                (obstacle.x_right < playerRight player) && (obstacle.x_left > playerLeft player)
                             else 
                                (obstacle.x_left > playerLeft player) || (obstacle.x_right < playerRight player)
        | Underwater obstacle -> true
