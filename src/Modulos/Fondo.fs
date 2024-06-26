namespace Frogger.Modulos

open Frogger.Modulos.Grid

module Fondo = 

    ////////////////////////////////////////// Tipos //////////////////////////////////////////
    
    // Tipo que define la base del obstáculo
    type ObstacleBase =
        {
            x_left: int // Extremo izquierdo del obstáculo
            x_right: int // Extremo derecho del obstáculo
            PosY: Rows // Posición de la fila del obstáculo
            Speed: int // Velocidad del obstáculo
        }

    // Tipo de obtáculo que puede estar arriba o abajo del agua
    type Obstacle =
        | Afloat of ObstacleBase
        | Underwater of ObstacleBase

    // Tipo que define el fondo del juego
    type Fondo = 
        {
            Obstacles: Map<Rows,List<Obstacle>> // Cada fila tiene una lista de obstáculos distinta
            Time: int // Temporizador actual del juego
        }

    // Tipo que define cada espacio de meta
    type GoalSpace =
        {
            PosX: int // Posición del centro del espacio de meta
            Width: int // Ancho del espacio de meta
            Ocupation: bool // Indica si el espacio de meta está ocupado
        }

    let TIME_AFLOAT_UNDERWATER = 10 // Tiempo que tarda una tortuga en cambiar de estado

    ////////////////////////////////////////// Funciones //////////////////////////////////////////
    
    // Función auxiliar para extraer el obstáculo de un tipo Obstacle cuando no se necesite distinguir entre Afloat y Underwater
    let matchObstacle (obstacle : Obstacle) : ObstacleBase = 
        match obstacle with
        | Afloat obstacle -> obstacle
        | Underwater obstacle -> obstacle

    // Función que mueve un obstáculo y establece las condiciones periódicas de contorno
    let moveObstacle (obstacleU: Obstacle) : Obstacle =
        let updatePosition (obstacle: ObstacleBase): ObstacleBase =
            let x_left_new : int = (obstacle.x_left + obstacle.Speed) % WIDTH 
            let x_right_new : int = (obstacle.x_right + obstacle.Speed) % WIDTH
            {obstacle with x_left = x_left_new; x_right = x_right_new}

        match obstacleU with
        | Afloat obstacle ->  Afloat (updatePosition obstacle)
        | Underwater obstacle ->  Underwater (updatePosition obstacle)

    // Función para cambiar el estado de una tortuga cada cierto tiempo
    let changeTurtleState (tiempo : int) (turtle: Obstacle) : Obstacle = 
        if tiempo % TIME_AFLOAT_UNDERWATER = 0 then
            match turtle with
            | Underwater turtle -> Afloat turtle
            | Afloat turtle -> Underwater turtle
        else
            turtle
        
    // Función para actualizar el fondo del juego, los obstáculos, el estado de las tortugas y el tiempo
    let updateFondo (fondo : Fondo) = 
        // Cambiamos todos los obstáculos moviendolos
        let movedObstacles = Map.map (fun _ lst -> lst |> List.map moveObstacle) fondo.Obstacles
        
        // Cambiamos el estado de las tortugas en las filas 8 y 11 cada 10 segundos
        let idx = 1 // Como hay dos y tres tortugas en las filas 8 y 11 respectivamente cambiamos el estado de la segunda tortuga en cada fila
        
        // Función que actualiza el estado de las tortugas en una fila dada para el elemento idx de la fila
        let updateTurtles row =
            row
            |> List.mapi (fun j obs -> if  j = idx then
                                            changeTurtleState fondo.Time obs
                                        else
                                            obs)
        // Actualizamos el estado de las tortugas en las filas 8 y 11
        let newRow8 = movedObstacles[Eight] |> updateTurtles
        let newRow11 = movedObstacles[Eleven] |> updateTurtles

        // Actualizamos el mapa de obstáculos
        let updatedObstacles =
            movedObstacles
            |> Map.add Eight newRow8
            |> Map.add Eleven newRow11

        // Devolvemos el fondo actualizado
        {fondo with Obstacles = updatedObstacles; Time = fondo.Time - 1}
