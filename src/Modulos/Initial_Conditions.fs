namespace Frogger.Modulos
open Frogger.Modulos.Types

module Initial_Conditions =

    // Inicializamos las posiciones del jugador y de los obstáculos
    let WIDTH : int= 800
    let HEIGHT : int= 600

    let external_width : int = 100

    let player : Player = { PosX = WIDTH / 2;
                            PosY = Rows.One;
                            Width = 40
                          }

    // Función para crear un obstáculo dado su posición central
    // Función para crear un obstáculo
    let createObstacle (x_left, x_right, posY, speed, underwater) : Obstacle  = 
        let obstacle: Obstacle = 
            { 
                x_left = x_left; 
                x_right = x_right; 
                PosY = posY;
                Speed = speed; 
                Underwater = underwater 
            }
        obstacle

    // Definamos los obstáculos fila por fila 
    let row2_Auto1 = createObstacle (200, 240, Rows.Two, 1, false)
    let row2_Auto2 = createObstacle (300, 340, Rows.Two, 1, false)
    let row2_Auto3 = createObstacle (400, 440, Rows.Two, 1, false)

    let row2 :Obstacle list = [row2_Auto1; row2_Auto2; row2_Auto3] 

    let row3_Auto1 = createObstacle (150, 200, Rows.Three, 2, false)
    let row3_Auto2 = createObstacle (300, 350, Rows.Three, 2, false)
    let row3_Auto3 = createObstacle (450, 500, Rows.Three, 2, false)

    let row3: Obstacle list = [row3_Auto1; row3_Auto2; row3_Auto3]

    let row4_Auto1 = createObstacle (100, 140, Rows.Four, 2, false)
    let row4_Auto2 = createObstacle (400, 440, Rows.Four, 2, false)
    let row4_Auto3 = createObstacle (700, 740, Rows.Four, 2, false)    

    let row4: Obstacle list = [row4_Auto1; row4_Auto2; row4_Auto3]

    let row5_Auto1 = createObstacle (400, 440, Rows.Five, 3, false)
    let row5_Auto2 = createObstacle (500, 540, Rows.Five, 3, false)

    let row5: Obstacle list = [row5_Auto1; row5_Auto2]

    let row6_Auto1 = createObstacle (100, 160, Rows.Six, 1, false)
    let row6_Auto2 = createObstacle (400, 460, Rows.Six, 1, false)

    let row6: Obstacle list = [row6_Auto1; row6_Auto2]

    // Cada tortuga mide 60 -> acá tenemos conjuntos de dos tortugas
    let row8_Turtle1 = createObstacle (100, 190, Rows.Eight, 2, true)
    let row8_Turtle2 = createObstacle (200, 290, Rows.Eight, 2, true)

    let row8: Obstacle list = [row8_Turtle1; row8_Turtle2]

    let row9_log1 = createObstacle (100, 170, Rows.Nine, 1, true)
    let row9_log2 = createObstacle (300, 370, Rows.Nine, 1, true)
    let row9_log3 = createObstacle (500, 570, Rows.Nine, 1, true)

    let row9: Obstacle list = [row9_log1; row9_log2; row9_log3]

    let row10_log1 = createObstacle (100, 230, Rows.Ten, 2, true)

    let row10 : Obstacle list = [row10_log1]

    let row11_Turtle1 = createObstacle (100, 160, Rows.Eleven, 1, true)
    let row11_Turtle2 = createObstacle (200, 260, Rows.Eleven, 1, true)
    let row11_Turtle3 = createObstacle (300, 360, Rows.Eleven, 1, true)

    let row11 : Obstacle list = [row11_Turtle1; row11_Turtle2; row11_Turtle3]

    let row12_log1 = createObstacle (250, 320, Rows.Twelve, 1, true)
    let row12_log2 = createObstacle (350, 420, Rows.Twelve, 1, true)
    let row12_log3 = createObstacle (450, 520, Rows.Twelve, 1, true)

    let row12: Obstacle list = [row12_log1; row12_log2; row12_log3]

    // En la fila final, definimos 5 espacios de meta
    // Primero tenemos una porción de pasto vacía (len: 30)
    // Después tenemos 5 espacios de meta (len: 60) espaciados en 60

    let space1: GoalSpace = { PosX = 30; Width = 60; Ocupation = false }
    let space2: GoalSpace = { PosX = 150; Width = 60; Ocupation = false }
    let space3: GoalSpace = { PosX = 270; Width = 60; Ocupation = false }
    let space4: GoalSpace = { PosX = 390; Width = 60; Ocupation = false }
    let space5: GoalSpace = { PosX = 510; Width = 60; Ocupation = false }

    let goal_spaces: GoalSpace list = [space1; space2; space3; space4; space5]
    // Luego habrá otro espacio de pasto vacío (len: 30)

    // Create the map of rows to lists of obstacles
    let obstaclesMap = 
        [
            (Rows.Two, row2)
            (Rows.Three, row3)
            (Rows.Four, row4)
            (Rows.Five, row5)
            (Rows.Six, row6)
            (Rows.Eight, row8)
            (Rows.Nine, row9)
            (Rows.Ten, row10)
            (Rows.Eleven, row11)
            (Rows.Twelve, row12)
        ] |> Map.ofList

    // Initialize `initFondo`
    let initFondo = 
        { Obstacles = obstaclesMap
          Time = 60 }

    let game: GameState = 
        { Player = player; Final_row = goal_spaces; Score = 0; Lifes = ThreeLives; Fondo = initFondo }

