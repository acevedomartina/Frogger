namespace Frogger.Modulos

let WIDTH = 800
let HEIGHT = 600
let WIDTH_PLAYABLE_LEFT = 100 
let WIDTH_PLAYABLE_RIGHT = 700

// Función que mueve de una fila hacia la fila de arriba
let moveUp (posY : Rows) = 
    match posY with
    | One -> Two
    | Two -> Three
    | Three -> Four
    | Four -> Five
    | Five -> Six
    | Six -> Seven
    | Seven -> Six
    | Eight -> Nine
    | Nine -> Ten
    | Ten -> Eleven
    | Eleven -> Twelve
    | Twelve -> Thirteen 
    | _ -> raise (System.Exception("Error no deberia pasar"))

// Función que mueve de una fila hacia la fila de abajo
let moveDown (posY : Rows) = 
    match posY with
    | One -> One
    | Two -> One
    | Three -> Two
    | Four -> Three
    | Five -> Four
    | Six -> Five
    | Seven -> Six
    | Eight -> Seven
    | Nine -> Eight
    | Ten -> Nine
    | Eleven -> Ten
    | Twelve -> Eleven
    | _ -> raise (System.Exception("Error no deberia pasar"))

// Función que mueve al jugador en la dirección indicada
let movePlayer (player : Player) (dir: Direction)  = 
    match dir with
    | Up -> let newPosY = moveUp player.PosY
            {player with PosY = newPosY}
    | Down -> let newPosY = moveDown player.PosY
              {player with PosY = newPosY}
    | Left -> if player.PosX - 50 < WIDTH_PLAYABLE_LEFT then player else {player with PosX = player.PosX - 50}    
    | Right -> if player.PosX + 50 > WIDTH_PLAYABLE_RIGHT then player else {player with PosX = player.PosX + 50}

// Chequeamos que haya colisión entre el jugador y el obstáculo
let checkCollision (player : Player) (obstacle : Obstacle) =
    (player.PosX + player.Width/2 > obstacle.x_left || player.PosX - player.Width/2 < obstacle.x_right)

// Chequeo si el jugador no está arriba de un tronco o una tortuga
let checkNotUpLogTurtle (player : Player) (obstacle : Obstacle) = 
    // Si la tortuga no está sumergida veo que pasa
    if not obstacle.Underwater then
        // Condición para que esté totalmente arriba del obsáculo
        if obstacle.x_left < player.PosX - player.Width / 2 && obstacle.x_right > player.PosX + player.Width / 2 then
            false
        // Si se teletransporta al otro lado el obstaculo pasa esto
        else if obstacle.x_left > obstacle.x_right then
            // Si el extremo derecho del jugador está a la derecha del obstáculo me ahogo entonces devuelve true
            // En caso contrario no me ahogo y devuelve false
            (obstacle.x_right < player.PosX + player.Width / 2)
        // Si el extremo izquierdo del jugador esta a la derecha del extremo izquierdo del obstáculo no me ahogo devuelve false
        // En caso contrario devuelve true
        else if obstacle.x_left < player.PosX - player.Width / 2 then
            false
        else
            true
    
    else
        true

// Función que mueve un obstáculo y establece las condiciones periódicas de contorno
let moveObstacle (obstacle: Obstacle) =
    let x_left_new : int = (obstacle.x_left + obstacle.Speed) % WIDTH 
    let x_right_new : int = (obstacle.x_right + obstacle.Speed) % WIDTH
    {obstacle with x_left = x_left_new; x_right = x_right_new}

// Función para cambiar el estado de la tortuga i-ésima cada 10 segundos
let state_tortuga (idx : int) (tiempo: int) (obstacles: Obstacle list) =
    if tiempo % 10 = 0 then
        obstacles |> List.mapi (fun i x -> if i = idx then {x with Underwater = not x.Underwater} else x)
    else
        obstacles

// Función para actualizar el fondo del juego, los obstáculos y el estado de las tortugas
let updateFondo (fondo : Fondo) = 
    // Cambiamos todos los obstáculos moviendolos
    let movedObstacles = Map.map (fun _ lst -> lst |> List.map moveObstacle) fondo.Obstacles
    
    // Cambiamos el estado de las tortugas en las filas 8 y 11 cada 10 segundos
    let idx = 1 // Como hay dos y tres tortugas en las filas 8 y 11 respectivamente cambiamos el estado de la segunda tortuga en cada fila
    let updatedObstcles = movedObstacles |> Map.map (fun key lst -> lst |> List.mapi (fun idx obs ->  if key = Rows.Eight || key = Rows.Eleven then
                                                                                                        state_tortuga idx fondo.Time lst |> List.item idx
                                                                                                      else
                                                                                                        obs))
    // Return the updated fondo
    {fondo with Obstacles = updatedObstcles}

let updateGameState (game : GameState) (dir : Option<Direction>) = 
        match dir with
        | Some d -> let newFondo = updateFondo game.Fondo
                    let newPlayer = movePlayer game.Player d
                    {game with Player = newPlayer; Fondo = newFondo}
                
        | None -> let newFondo = updateFondo game.Fondo
                  {game with Fondo = newFondo}
        
// Chequear si se acabó el tiempo
let CheckTime (game: GameState) = 
    if game.Fondo.Time = 0 then
        {game with Lifes = GameOver}
    else
        game
    
//////////////////////////////////////////////////////////////////////////////7

    // Chequear si se acabó el tiempo
let CheckTime (game: GameState) = 
    if game.Fondo.Time = 0 then
        let newPlayer = {game.Player with PosX = WIDTH/2; PosY = Rows.One}
        let newGame = updateLives game
        {newGame with Player = newPlayer}
    else
        game


let CheckWinAux (player : Player) (goal_space : GoalSpace) = 
    let player_xright = player.PosX + player.Width / 2
    let player_xleft = player.PosX - player.Width / 2

    if player_xleft > goal_space.PosX && player_xright < goal_space.PosX + goal_space.Width then
        { goal_space with Ocupation = true }          
    else
        { goal_space with Ocupation = false }

// Función que chequea si el jugador llegó a alguna meta
let CheckGoal (game : GameState) =
    let player = game.Player
    let goal_spaces = game.Final_row
    let updatedGoalSpaces = List.map (CheckWinAux player) goal_spaces
    let anyGoalSpaceChanged = List.exists (fun gs -> gs.Ocupation) updatedGoalSpaces

    if anyGoalSpaceChanged then
        { game with Final_row = updatedGoalSpaces; Score = game.Score + 50 }
    else
        updateLives game
    

// Chequear si ganó el nivel, ie, si todos los elementos de finalrow son True
let CheckWin (game : GameState) = 
    if List.forall (fun goal -> goal.Ocupation) game.Final_row then
        let newFinalRow = List.map (fun goal -> { goal with Ocupation = false }) game.Final_row
        { game with Score = game.Score + 1000; Lifes = ThreeLives; Player = { PosX = WIDTH/2; PosY = One; Width = 40}; Final_row = newFinalRow }
    else
        game

let updateLives (game : GameState) = 
    let lifes = game.Lifes
    match lifes with
    | GameOver -> game
    | OneLife -> {game with Lifes = GameOver}
    | TwoLives -> {game with Lifes = OneLife}
    | ThreeLives -> {game with Lifes = TwoLives}

let checkLoseGame (game : GameState) = 
    let lifes = game.Lifes
    match lifes with
    | GameOver -> Error "Game Over"
    | _ -> Ok game

let checkPlayerLose (game : GameState) =
    let player = game.Player
    let PosY = player.PosY
    match PosY with
    | One | Seven -> game
    | Two | Three | Four | Five | Six ->    let obstacles = Map.find PosY game.Fondo.Obstacles
                                            let collision = List.exists (checkCollision player) obstacles
                                            if collision then
                                                let newGame = checkLoseGame (updateLives game)
                                                if newGame.IsOk then
                                                    let newPlayer = {player with PosX = WIDTH/2; PosY = Rows.One}
                                                    let newScore = game.Score + 10 
                                                    Ok {newGame with Player = newPlayer, Score = newScore}
                                                else
                                                    newGame
                                            else
                                                Ok game
    | Eight | Nine | Ten | Eleven | Twelve ->   let obstacles = Map.find PosY game.Fondo.Obstacles
                                                let drown = List.exists (checkNotUpLogTurtle player) obstacles
                                                if drown then
                                                    let newGame = checkLoseGame (updateLives game)
                                                    if newGame.IsOk then
                                                        let newPlayer = {player with PosX = WIDTH/2; PosY = Rows.One}
                                                        let newScore = game.Score + 10
                                                        Ok {newGame with Player = newPlayer, Score = newScore}
                                                    else
                                                        newGame
                                                else
                                                    Ok game

    | Thirteen -> //Chequear si ganó o perdío una vida