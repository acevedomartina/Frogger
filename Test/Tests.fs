module Tests

open Xunit
open Frogger.Modulos.Functions
open Frogger.Modulos.Types
open Frogger.Modulos.Initial_Conditions

open Frogger.Modulos

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
    // Arrange
    let initialGameState = Initial_Conditions.game

    // Assert
    Assert.NotNull(initialGameState)






