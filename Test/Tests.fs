module Tests

open Xunit
open Frogger.Modulos.Functions
open Frogger.Modulos.Types
open Frogger.Modulos

//////////////////////// Modulo initial conditions //////////////////////////////////
// Inicializamos las posiciones del jugador y de los obstáculos
let WIDTH : int= 800
let HEIGHT : int= 600

let external_width : int = 100

let player : Player = { PosX = WIDTH / 2;
                        PosY = Rows.One
                        Width = 40}

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

let row2 = [row2_Auto1; row2_Auto2; row2_Auto3] 

let row3_Auto1 = createObstacle (150, 200, Rows.Three, 2, false)
let row3_Auto2 = createObstacle (300, 350, Rows.Three, 2, false)
let row3_Auto3 = createObstacle (450, 500, Rows.Three, 2, false)

let row3 = [row3_Auto1; row3_Auto2; row3_Auto3]

let row4_Auto1 = createObstacle (100, 140, Rows.Four, 2, false)
let row4_Auto2 = createObstacle (400, 440, Rows.Four, 2, false)
let row4_Auto3 = createObstacle (700, 740, Rows.Four, 2, false)    

let row4 = [row4_Auto1; row4_Auto2; row4_Auto3]

let row5_Auto1 = createObstacle (400, 440, Rows.Five, 3, false)
let row5_Auto2 = createObstacle (500, 540, Rows.Five, 3, false)

let row5 = [row5_Auto1; row5_Auto2]

let row6_Auto1 = createObstacle (100, 160, Rows.Six, 1, false)
let row6_Auto2 = createObstacle (400, 460, Rows.Six, 1, false)

let row6 = [row6_Auto1; row6_Auto2]

// Cada tortuga mide 60 -> acá tenemos conjuntos de dos tortugas
let row8_Turtle1 = createObstacle (100, 190, Rows.Eight, 2, true)
let row8_Turtle2 = createObstacle (200, 290, Rows.Eight, 2, true)

let row8 = [row8_Turtle1; row8_Turtle2]

let row9_log1 = createObstacle (100, 170, Rows.Nine, 1, true)
let row9_log2 = createObstacle (300, 370, Rows.Nine, 1, true)
let row9_log3 = createObstacle (500, 570, Rows.Nine, 1, true)

let row9 = [row9_log1; row9_log2; row9_log3]

let row10_log1 = createObstacle (100, 230, Rows.Ten, 2, true)

let row10 = [row10_log1]

let row11_Turtle1 = createObstacle (100, 160, Rows.Eleven, 1, true)
let row11_Turtle2 = createObstacle (200, 260, Rows.Eleven, 1, true)
let row11_Turtle3 = createObstacle (300, 360, Rows.Eleven, 1, true)

let row11 = [row11_Turtle1; row11_Turtle2; row11_Turtle3]

let row12_log1 = createObstacle (250, 320, Rows.Twelve, 1, true)
let row12_log2 = createObstacle (350, 420, Rows.Twelve, 1, true)
let row12_log3 = createObstacle (450, 520, Rows.Twelve, 1, true)

let row12 = [row12_log1; row12_log2; row12_log3]

// En la fila final, definimos 5 espacios de meta
// Primero tenemos una porción de pasto vacía (len: 30)
// Después tenemos 5 espacios de meta (len: 60) espaciados en 60
let space1 = { PosX = 30; Width = 60; Ocupation = false }
let space2 = { PosX = 150; Width = 60; Ocupation = false }
let space3 = { PosX = 270; Width = 60; Ocupation = false }
let space4 = { PosX = 390; Width = 60; Ocupation = false }
let space5 = { PosX = 510; Width = 60; Ocupation = false }

let goal_spaces = [space1; space2; space3; space4; space5]
// Luego habrá otro espacio de pasto vacío (len: 30)

// Create the map of rows to lists of obstacles
let obstaclesMap = 
    [   (Rows.Two, row2)
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
      Time = 60 
    }

let game: GameState = 
    { Player = player; Final_row = goal_spaces; Score = 0; Lifes = ThreeLives; Fondo = initFondo }




// TEST PARA MOVEDOWN
[<Fact>]
let ``moveDown Two should return One`` () =
    let result = Functions.moveDown Rows.Two
    Assert.Equal(Rows.One, result)

// TEST PARA MOVEUP
[<Fact>]
let ``moveUp Two should return Three`` () =
    let result = Functions.moveUp Rows.Two
    Assert.Equal(Rows.Three, result)

// TESTS PARA MOVEPLAYER

[<Fact>]
let ``Move player up test`` () =
    let initialPlayer = { PosX = 0; PosY = Rows.One; Width = 40 }
    let expectedPlayer = { initialPlayer with PosY = Rows.Two } // La posición esperada del jugador después de moverse hacia arriba

    let movedPlayer = movePlayer initialPlayer Up

    Assert.Equal(expectedPlayer, movedPlayer)

//Un caso límite:
[<Fact>]
let ``Move player beyond left limit test`` () =
    // Arrange
    let initialPlayer = { PosX = WIDTH_PLAYABLE_LEFT; PosY = Rows.One; Width = 40 }

    // Act
    let movedPlayer = movePlayer initialPlayer Left

    // Assert
    Assert.Equal(initialPlayer, movedPlayer) // Se espera que la posición del jugador no cambie


//TEST PARA MOVEOBSTACLE

[<Fact>]
let ``Move obstacle test`` () =
    // Arrange
    let initialObstacle = { x_left = 10; x_right = 20; PosY = Rows.Two; Speed = 5; Underwater = false }
    let expectedObstacle = { x_left = 15; x_right = 25; PosY = Rows.Two; Speed = 5; Underwater = false } // El obstáculo se espera que se mueva hacia la derecha

    // Act
    let movedObstacle = moveObstacle initialObstacle

    // Assert
    Assert.Equal(expectedObstacle, movedObstacle) // Verifica si el obstáculo se mueve correctamente

//Un caso límite:
[<Fact>]
let ``Move obstacle beyond screen width test`` () =
    // Arrange
    let screenWidth = 800 // Ancho de la pantalla
    let initialObstacle = { x_left = screenWidth - 10; x_right = screenWidth - 5; PosY = Rows.One; Speed = 10; Underwater = false }
    let expectedObstacle = { x_left = 0; x_right = 5; PosY = Rows.One; Speed = 10; Underwater = false } // El obstáculo se espera que vuelva al inicio de la pantalla

    // Act
    let movedObstacle = moveObstacle initialObstacle

    // Assert
    Assert.Equal(expectedObstacle, movedObstacle) // Verifica si el obstáculo se mueve correctamente más allá del ancho de la pantalla


//TEST PARA CHECKCOLLISION

[<Fact>]
let ``Check collision should detect collision correctly`` () =
    // Arrange
    let player = { PosX = 150; PosY = Rows.Five; Width = 10 } // Jugador cerca del obstáculo
    let obstacle = { x_left = 140; x_right = 160; PosY = Rows.Five; Speed = 0; Underwater = false } // Obstáculo cerca del jugador

    // Act
    let result = checkCollision player obstacle

    // Assert
    Assert.True(result) // Verifica que la función checkCollision detecta la colisión correctamente


[<Fact>]
let ``Check collision should detect collision right`` () =
    // Arrange
    let player = { PosX = 160; PosY = Rows.Five; Width = 10 } // El jugador está a la derecha del obstáculo
    let obstacle = { x_left = 140; x_right = 160; PosY = Rows.Five; Speed = 0; Underwater = false } // Obstáculo cerca del jugador

    // Act
    let result = checkCollision player obstacle

    // Assert
    Assert.True(result) // Verifica que la función checkCollision detecta la colisión correctamente

[<Fact>]
let ``Check collision should detect collision left`` () =
    // Arrange
    let player = { PosX = 140; PosY = Rows.Five; Width = 10 } // El jugador está a la izquierda del obstáculo
    let obstacle = { x_left = 140; x_right = 160; PosY = Rows.Five; Speed = 0; Underwater = false } // Obstáculo cerca del jugador

    // Act
    let result = checkCollision player obstacle

    // Assert
    Assert.True(result) // Verifica que la función checkCollision detecta la colisión correctamente

[<Fact>]
let ``Check collision should detect collision border right`` () =
    // Arrange
    let player = { PosX = 165; PosY = Rows.Five; Width = 10 } // El jugador está en el borde derecho del obstáculo
    let obstacle = { x_left = 140; x_right = 160; PosY = Rows.Five; Speed = 0; Underwater = false } // Obstáculo cerca del jugador

    // Act
    let result = checkCollision player obstacle

    // Assert
    Assert.True(result) // Verifica que la función checkCollision detecta la colisión correctamente

[<Fact>]
let ``Check collision should detect collision border left`` () =
    // Arrange
    let player = { PosX = 135; PosY = Rows.Five; Width = 10 } // El jugador está en el borde izquierdo del obstáculo
    let obstacle = { x_left = 140; x_right = 160; PosY = Rows.Five; Speed = 0; Underwater = false } // Obstáculo cerca del jugador

    // Act
    let result = checkCollision player obstacle

    // Assert
    Assert.True(result) // Verifica que la función checkCollision detecta la colisión correctamente

[<Fact>]
let ``Check collision should detect no collision left`` () =
    // Arrange
    let player = { PosX = 130; PosY = Rows.Five; Width = 10 } // El jugador está lejos del obstáculo a la izquierda
    let obstacle = { x_left = 140; x_right = 160; PosY = Rows.Five; Speed = 0; Underwater = false } // Obstáculo cerca del jugador

    // Act
    let result = checkCollision player obstacle

    // Assert
    Assert.False(result) // Verifica que la función checkCollision no detecta una colisión

[<Fact>]
let ``Check collision should detect no collision right`` () =
    // Arrange
    let player = { PosX = 170; PosY = Rows.Five; Width = 10 } // El jugador está lejos del obstáculo a la derecha
    let obstacle = { x_left = 140; x_right = 160; PosY = Rows.Five; Speed = 0; Underwater = false } // Obstáculo cerca del jugador

    // Act
    let result = checkCollision player obstacle

    // Assert
    Assert.False(result) // Verifica que la función checkCollision no detecta una colisión


//TEST PARA CHECKNOTUPLOGTURTLE

[<Fact>]
let ``Check not up log turtle should detect player completely above log`` () =
    // Arrange
    let player = { PosX = 150; PosY = Rows.Five; Width = 10 }
    let obstacle = { x_left = 100; x_right = 200; PosY = Rows.Five; Speed = 0; Underwater = false }

    // Act
    let result = checkNotUpLogTurtle player obstacle

    // Assert
    Assert.False(result)

[<Fact>]
let ``Check not up log turtle should detect player border right above log`` () =
    // Arrange
    let player = { PosX = 195; PosY = Rows.Five; Width = 10 }
    let obstacle = { x_left = 100; x_right = 200; PosY = Rows.Five; Speed = 0; Underwater = false }

    // Act
    let result = checkNotUpLogTurtle player obstacle

    // Assert
    Assert.False(result)

[<Fact>]
let ``Check not up log turtle should detect player border left above log`` () =
    // Arrange
    let player = { PosX = 105; PosY = Rows.Five; Width = 10 }
    let obstacle = { x_left = 100; x_right = 200; PosY = Rows.Five; Speed = 0; Underwater = false }

    // Act
    let result = checkNotUpLogTurtle player obstacle

    // Assert
    Assert.False(result)

[<Fact>]
let ``Check not up log turtle should detect player partially right above log`` () =
    // Arrange
    let player = { PosX = 200; PosY = Rows.Five; Width = 10 }
    let obstacle = { x_left = 100; x_right = 200; PosY = Rows.Five; Speed = 0; Underwater = false }

    // Act
    let result = checkNotUpLogTurtle player obstacle

    // Assert
    Assert.True(result)

[<Fact>]
let ``Check not up log turtle should detect player partially left above log`` () =
    // Arrange
    let player = { PosX = 100; PosY = Rows.Five; Width = 10 }
    let obstacle = { x_left = 100; x_right = 200; PosY = Rows.Five; Speed = 0; Underwater = false }

    // Act
    let result = checkNotUpLogTurtle player obstacle

    // Assert
    Assert.True(result)

[<Fact>]
let ``Check not up log turtle should detect player completely right from log`` () =
    // Arrange
    let player = { PosX = 250; PosY = Rows.Five; Width = 10 }
    let obstacle = { x_left = 100; x_right = 200; PosY = Rows.Five; Speed = 0; Underwater = false }

    // Act
    let result = checkNotUpLogTurtle player obstacle

    // Assert
    Assert.True(result)

[<Fact>]
let ``Check not up log turtle should detect player completely left from log`` () =
    // Arrange
    let player = { PosX = 50; PosY = Rows.Five; Width = 10 }
    let obstacle = { x_left = 100; x_right = 200; PosY = Rows.Five; Speed = 0; Underwater = false }

    // Act
    let result = checkNotUpLogTurtle player obstacle

    // Assert
    Assert.True(result)

[<Fact>]
let ``Check not up log turtle should detect player inside submerged turtle`` () =
    // Arrange
    let player = { PosX = 150; PosY = Rows.Five; Width = 50 }
    let obstacle = { x_left = 100; x_right = 200; PosY = Rows.Five; Speed = 0; Underwater = true }

    // Act
    let result = checkNotUpLogTurtle player obstacle

    // Assert
    Assert.True(result)

[<Fact>]
let ``Check not up log turtle should detect player outside submerged turtle`` () =
    // Arrange
    let player = { PosX = 250; PosY = Rows.Five; Width = 50 }
    let obstacle = { x_left = 100; x_right = 200; PosY = Rows.Five; Speed = 0; Underwater = true }

    // Act
    let result = checkNotUpLogTurtle player obstacle

    // Assert
    Assert.True(result)
[<Fact>]
let ``border checkNotUpLogTurtle should detect player completely above log left`` () =
    // Arrange
    let player = { PosX = 50; PosY = Rows.Five; Width = 10 }
    let obstacle = { x_left = 700; x_right = 100; PosY = Rows.Five; Speed = 0; Underwater = false }

    // Act
    let result = checkNotUpLogTurtle player obstacle

    // Assert
    Assert.False(result)

[<Fact>]
let ``border checkNotUpLogTurtle should detect player completely above log right`` () =
    // Arrange
    let player = { PosX = 750; PosY = Rows.Five; Width = 10 }
    let obstacle = { x_left = 700; x_right = 100; PosY = Rows.Five; Speed = 0; Underwater = false }

    // Act
    let result = checkNotUpLogTurtle player obstacle

    // Assert
    Assert.False(result)

[<Fact>]
let ``border checkNotUpLogTurtle should detect player border right above log left`` () =
    // Arrange
    let player = { PosX = 95; PosY = Rows.Five; Width = 10 }
    let obstacle = { x_left = 700; x_right = 100; PosY = Rows.Five; Speed = 0; Underwater = false }

    // Act
    let result = checkNotUpLogTurtle player obstacle

    // Assert
    Assert.False(result)

[<Fact>]
let ``border checkNotUpLogTurtle should detect player border left above log right`` () =
    // Arrange
    let player = { PosX = 705; PosY = Rows.Five; Width = 10 }
    let obstacle = { x_left = 700; x_right = 100; PosY = Rows.Five; Speed = 0; Underwater = false }

    // Act
    let result = checkNotUpLogTurtle player obstacle

    // Assert
    Assert.False(result)

[<Fact>]
let ``border checkNotUpLogTurtle should detect player partially right above log left`` () =
    // Arrange
    let player = { PosX = 100; PosY = Rows.Five; Width = 10 }
    let obstacle = { x_left = 700; x_right = 100; PosY = Rows.Five; Speed = 0; Underwater = false }

    // Act
    let result = checkNotUpLogTurtle player obstacle

    // Assert
    Assert.True(result)

[<Fact>]
let ``border checkNotUpLogTurtle should detect player partially left above log right`` () =
    // Arrange
    let player = { PosX = 700; PosY = Rows.Five; Width = 10 }
    let obstacle = { x_left = 700; x_right = 100; PosY = Rows.Five; Speed = 0; Underwater = false }

    // Act
    let result = checkNotUpLogTurtle player obstacle

    // Assert
    Assert.True(result)

[<Fact>]
let ``border checkNotUpLogTurtle should detect player completely right from log left`` () =
    // Arrange
    let player = { PosX = 105; PosY = Rows.Five; Width = 10 }
    let obstacle = { x_left = 700; x_right = 100; PosY = Rows.Five; Speed = 0; Underwater = false }

    // Act
    let result = checkNotUpLogTurtle player obstacle

    // Assert
    Assert.True(result)

[<Fact>]
let ``border checkNotUpLogTurtle should detect player completely left from log right`` () =
    // Arrange
    let player = { PosX = 695; PosY = Rows.Five; Width = 10 }
    let obstacle = { x_left = 700; x_right = 100; PosY = Rows.Five; Speed = 0; Underwater = false }

    // Act
    let result = checkNotUpLogTurtle player obstacle

    // Assert
    Assert.True(result)

//TEST PARA UPDATELIVES
[<Fact>]
let ``UpdateLives should correctly update the number of lives`` () = 
    let gameInit = game
    let gameAfterCollision = {gameInit with Lifes = LivesRemaining.TwoLives}
    let gameAfterUpdate = updateLives gameAfterCollision
    Assert.Equal(OneLife, gameAfterUpdate.Lifes) // Verifica si la función updateLives actualiza correctamente el número de vidas


[<Fact>]
// Test de `updateFondo` en el para el cambio del estado de las tortugas y update de los obstáculos
// Me fijo que solo cambien las tortugas en el índice 1 de las filas 8 y 11
let ``updateFondo should move obstacles and change turtle states`` () = 
    let fondo = 
        {
            Obstacles = 
                Map.ofList 
                    [ 
                        Rows.Two, [{ x_left = 100; x_right = 200; PosY = Two; Speed = 2; Underwater = false }];
                        Rows.Eight, [{ x_left = 100; x_right = 200; PosY = Eight; Speed = 2; Underwater = false }; {x_left = 250; x_right = 350; PosY = Eight; Speed = 2; Underwater = false}];
                        Rows.Eleven, [{ x_left = 100; x_right = 200; PosY = Eleven; Speed = 2; Underwater = false}; {x_left = 250; x_right = 350; PosY = Eight; Speed = 2; Underwater = false}]
                    ]
            Time = 10
        }
    let updatedFondo = updateFondo fondo
    let notUpdatedRowTwo = updatedFondo.Obstacles.[Rows.Two].[0].Underwater
    let notUpdatedRowEight = updatedFondo.Obstacles.[Rows.Eight].[0].Underwater
    let UpdatedRowEight = updatedFondo.Obstacles.[Rows.Eight].[1].Underwater
    let notUpdatedRowEleven = updatedFondo.Obstacles.[Rows.Eleven].[0].Underwater
    let UpdatedRowEleven = updatedFondo.Obstacles.[Rows.Eleven].[1].Underwater
    Assert.False(notUpdatedRowTwo)
    Assert.False(notUpdatedRowEight)
    Assert.True(UpdatedRowEight)
    Assert.False(notUpdatedRowEleven)
    Assert.True(UpdatedRowEleven)

//TEST CHECKTIME

[<Fact>]
let ``CheckTime should update game state when time is zero`` () =
    // Arrange
    let gameWithTimeZero = {game with Fondo = {game.Fondo with Time = 0 } }
    
    // Act
    let updatedGameStateTime = CheckTime gameWithTimeZero
    
    // Assert
    Assert.NotEqual(gameWithTimeZero, updatedGameStateTime)
    // Aquí puedes agregar más aserciones específicas según la lógica de CheckTime

[<Fact>]
let ``CheckTime should not update game state when time is non-zero`` () =
    // Arrange
    let gameWithTimeNonZero = { game with Fondo = {game.Fondo with Time = 10 } }
    
    // Act
    let updatedGameStateTimeNonZero = CheckTime gameWithTimeNonZero
    
    // Assert
    Assert.Equal(gameWithTimeNonZero, updatedGameStateTimeNonZero)
    // Aquí puedes agregar más aserciones específicas según la lógica de CheckTime

    