namespace Frogger.Modulos

module Initial_Conditions =

    open Frogger.Types
    
    //Inicializamos las posiciones del jugador y de los obstaculos
    //Definimos el centro de la pantalla como el origen de coordenadas

    let WIDTH = 800
    let HEIGHT = 600

    let external_width = 100

    let player = { PosX = WIDTH/2 ; PosY = Rows.One Grass; Width = 40}

    // Definamos los obstaculos fila por fila 
    
    let row2_Auto1 = { PosX = 200; PosY = Rows.Two Road; Width = 40; Speed = 1}
    let row2_Auto2 = { PosX = 300; PosY = Rows.Two Road; Width = 40; Speed = 1}
    let row2_Auto3 = { PosX = 400; PosY = Rows.Two Road; Width = 40; Speed = 1}

    let row2 = [row2_Auto1; row2_Auto2; row2_Auto3] 

    let row3_Auto1 = { PosX = 150; PosY = Rows.Three Road; Width = 50; Speed = 2}
    let row3_Auto2 = { PosX = 300; PosY = Rows.Three Road; Width = 50; Speed = 2}
    let row3_Auto3 = { PosX = 450; PosY = Rows.Three Road; Width = 50; Speed = 2}

    let row3 = [row3_Auto1; row3_Auto2; row3_Auto3]

    let row4_Auto1 = { PosX = 100; PosY = Rows.Three Road; Width = 40; Speed = 2}
    let row4_Auto2 = { PosX = 400; PosY = Rows.Three Road; Width = 40; Speed = 2}
    let row4_Auto3 = { PosX = 700; PosY = Rows.Three Road; Width = 40; Speed = 2}    

    let row4 = [row4_Auto1; row4_Auto2; row4_Auto3]

    let row5_Auto1 = { PosX = 400; PosY = Rows.Three Road; Width = 40; Speed = 3}
    let row5_Auto2 = { PosX = 500; PosY = Rows.Three Road; Width = 40; Speed = 3}

    let row5 = [row5_Auto1; row5_Auto2]

    let row6_Auto1 = { PosX = 100; PosY = Rows.Three Road; Width = 60; Speed = 1}
    let row6_Auto2 = { PosX = 400; PosY = Rows.Three Road; Width = 60; Speed = 1}

    let row6 = [row6_Auto1; row6_Auto2]

    //cada tortuga mide 60 -> acá tenemos conjuntos de dos tortugas
    let row8_Turtle1 = { PosX = 100; PosY = Rows.Eight Water; Width = 90; Speed = 2; Timer = 0}
    let row8_Turtle2 = { PosX = 200; PosY = Rows.Eight Water; Width = 90; Speed = 2; Timer = 0}

    let row8 = [row8_Turtle1; row8_Turtle2]
    
    let row9_log1 = { PosX = 100; PosY = Rows.Nine Water; Width = 70; Speed = 1}
    let row9_log2 = { PosX = 300; PosY = Rows.Nine Water; Width = 70; Speed = 1}
    let row9_log3 = { PosX = 500; PosY = Rows.Nine Water; Width = 70; Speed = 1}

    let row9 = [row9_log1; row9_log2; row9_log3]

    let row10_log1 = { PosX = 100; PosY = Rows.Ten Water; Width = 130; Speed = 2}

    let row10 = [row10_log1]

    let row11_Turtle1 = { PosX = 100; PosY = Rows.Eleven Water; Width = 60; Speed = 1; Timer = 0}
    let row11_Turtle2 = { PosX = 200; PosY = Rows.Eleven Water; Width = 60; Speed = 1; Timer = 0}
    let row11_Turtle3 = { PosX = 300; PosY = Rows.Eleven; Water; Width = 60; Speed = 1; Timer = 0}

    let row11 = [row11_Turtle1; row11_Turtle2; row11_Turtle3]

    let row12_log1 = { PosX = 250; PosY = Rows.Twelve Water; Width = 70; Speed = 1}
    let row12_log2 = { PosX = 350; PosY = Rows.Twelve Water; Width = 70; Speed = 1}
    let row12_log3 = { PosX = 450; PosY = Rows.Twelve Water; Width = 70; Speed = 1}

    let row12 = [row12_log1; row12_log2; row12_log3]

    //En la fila final, definimos 5 espacios de meta
    //Primero tenemos una porción de pasto vacía (len: 30)
    //Después tenemos 5 espacios de meta (len: 60) espaciados en 60
    let space1 = { PosX = 30; Width = 60; Ocupation = False}
    let space2 = { PosX = 150 ; Width = 60; Ocupation = False}
    let space3 = { PosX = 270; Width = 60; Ocupation = False}
    let space4 = { PosX = 390; Width = 60; Ocupation = False}
    let space5 = { PosX = 510; Width = 60; Ocupation = False}

    let goal_spaces = [space1; space2; space3; space4; space5]
    //luego habrá otro espacio de pasto vacío (len: 30)

    let initFondo = 
    {
        Obstacles = row2 @ row3 @ row4 @ row5 @ row6 @ row8 @ row9 @ row10 @ row11 @ row12
        Time = 60
    }

    let gameState = 
    {
        Player = player
        Final_row = goal_spaces
        Score = 0
        Lifes = ThreeLives
        Fondo = initFondo
    }