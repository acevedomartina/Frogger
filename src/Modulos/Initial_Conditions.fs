namespace Frogger.Modulos

module Initial_Conditions =
    open Frogger.Types

    let WIDTH = 600
    let HEIGHT = 800

    let external_width = 100

    let player = {PosX = WIDTH/2, posY = Rows.One, Width = 40}

    let row2_Auto1 = { x_left = 200; x_right = 240; PosY = Rows.Two; Width = 40; Speed = 1; Underwater = false }
    let row2_Auto2 = { x_left = 300; x_right = 340; PosY = Rows.Two; Width = 40; Speed = 1; Underwater = false }
    let row2_Auto3 = { x_left = 400; x_right = 440; PosY = Rows.Two; Width = 40; Speed = 1; Underwater = false }

    let row2 = [row2_Auto1; row2_Auto2; row2_Auto3] 

    let row3_Auto1 = { x_left = 150; x_right = 200; PosY = Rows.Three; Width = 50; Speed = 2; Underwater = false }
    let row3_Auto2 = { x_left = 300; x_right = 350; PosY = Rows.Three; Width = 50; Speed = 2; Underwater = false }
    let row3_Auto3 = { x_left = 450; x_right = 500; PosY = Rows.Three; Width = 50; Speed = 2; Underwater = false }

    let row3 = [row3_Auto1; row3_Auto2; row3_Auto3]

    let row4_Auto1 = { x_left = 100; x_right = 140; PosY = Rows.Four; Width = 40; Speed = 2; Underwater = false }
    let row4_Auto2 = { x_left = 400; x_right = 440; PosY = Rows.Four; Width = 40; Speed = 2; Underwater = false }
    let row4_Auto3 = { x_left = 700; x_right = 740; PosY = Rows.Four; Width = 40; Speed = 2; Underwater = false }    

    let row4 = [row4_Auto1; row4_Auto2; row4_Auto3]

    let row5_Auto1 = { x_left = 400; x_right = 440; PosY = Rows.Five; Width = 40; Speed = 3; Underwater = false }
    let row5_Auto2 = { x_left = 500; x_right = 540; PosY = Rows.Five; Width = 40; Speed = 3; Underwater = false }

    let row5 = [row5_Auto1; row5_Auto2]

    let row6_Auto1 = { x_left = 100; x_right = 160; PosY = Rows.Six; Width = 60; Speed = 1; Underwater = false }
    let row6_Auto2 = { x_left = 400; x_right = 460; PosY = Rows.Six; Width = 60; Speed = 1; Underwater = false }

    let row6 = [row6_Auto1; row6_Auto2]

    let row8_Turtle1 = { x_left = 100; x_right = 190; PosY = Rows.Eight; Width = 90; Speed = 2; Underwater = false }
    let row8_Turtle2 = { x_left = 200; x_right = 290; PosY = Rows.Eight; Width = 90; Speed = 2; Underwater = false }

    let row8 = [row8_Turtle1; row8_Turtle2]

    let row9_log1 = { x_left = 100; x_right = 170; PosY = Rows.Nine; Width = 70; Speed = 1; Underwater = false }
    let row9_log2 = { x_left = 300; x_right = 370; PosY = Rows.Nine; Width = 70; Speed = 1; Underwater = false }
    let row9_log3 = { x_left = 500; x_right = 570; PosY = Rows.Nine; Width = 70; Speed = 1; Underwater = false }

    let row9 = [row9_log1; row9_log2; row9_log3]

    let row10_log1 = { x_left = 100; x_right = 230; PosY = Rows.Ten; Width = 130; Speed = 2; Underwater = false }

    let row10 = [row10_log1]

    let row11_Turtle1 = { x_left = 100; x_right = 160; PosY = Rows.Eleven; Width = 60; Speed = 1; Underwater = false }
    let row11_Turtle2 = { x_left = 200; x_right = 260; PosY = Rows.Eleven; Width = 60; Speed = 1; Underwater = false }
    let row11_Turtle3 = { x_left = 300; x_right = 360; PosY = Rows.Eleven; Width = 60; Speed = 1; Underwater = false }

    let row11 = [row11_Turtle1; row11_Turtle2; row11_Turtle3]

    let row12_log1 = { x_left = 250; x_right = 320; PosY = Rows.Twelve; Width = 70; Speed = 1; Underwater = false }
    let row12_log2 = { x_left = 350; x_right = 420; PosY = Rows.Twelve; Width = 70; Speed = 1; Underwater = false }
    let row12_log3 = { x_left = 450; x_right = 520; PosY = Rows.Twelve; Width = 70; Speed = 1; Underwater = false }

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
        Obstacles = Map.ofList [Rows.Two, row2; Rows.Three, row3; Rows.Four, row4; Rows.Five, row5; Rows.Six, row6; Rows.Eight, row8; Rows.Nine, row9; Rows.Ten, row10; Rows.Eleven, row11; Rows.Twelve, row12]
        Time = 60
    }

    let game = 
    {
        Player = player
        Final_row = goal_spaces
        Score = 0
        Lifes = ThreeLives
        Fondo = initFondo
    }