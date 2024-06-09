namespace Frogger.Modulos

let WIDTH = 800
let HEIGHT = 600
let WIDTH_PLAYABLE_LEFT = 100 
let WIDTH_PLAYABLE_RIGHT = 700

module Functions = 
    open Frogger.Types

    // Función que mueve de una fila hacia la fila de arriba
    let moveUp (posY : Rows) = 
        match posY with
        | One _ -> Two
        | Two _ -> Three
        | Three _ -> Four
        | Four _ -> Five
        | Five _ -> Six
        | Six _ -> Seven
        | Seven _ -> Six
        | Eight _ -> Nine
        | Nine _ -> Ten
        | Ten _ -> Eleven
        | Eleven _ -> Twelve
        | Twelve _ -> Thirteen 

    // Función que mueve de una fila hacia la fila de abajo
    let moveDown (posY : Rows) = 
        match posY with
        | One _ -> One
        | Two _ -> One
        | Three _ -> Two
        | Four _ -> Three
        | Five _ -> Four
        | Six _ -> Five
        | Seven _ -> Six
        | Eight _ -> Seven
        | Nine _ -> Eight
        | Ten _ -> Nine
        | Eleven _ -> Ten
        | Twelve _ -> Eleven

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
        return player.PosX + player.Width/2 > obstacle.x_left || player.PosX - player.Width/2 < obstacle.x_right

    let checkNotUpLogTurtle (player : Player) (obstacle : Obstacle) = 
        if not ostacle.Underwater then
            if obstacle.x_left < player.PosX - player.Width / 2 && obstacle.x_right > player.PosX + player.Width / 2 then
                false
            else if obstacle.x_left > obstacle.x_right then
                (obstacle.x_right < player.PosX + player.Width / 2)
            else if obstacle.x_left < player.PosX - player.Width / 2 then
                false
            else
                true
        
        else
            true

    let moveObstacle (obstacle: Obstacle) =
        let x_left_new : int = (obstacle.x_left + obstacle.Speed) % WIDTH 
        let x_right_new : int = (obstacle.x_right + obstacle.Speed) % WIDTH

        {obstacle with x_left = x_left_new; x_right = x_right_new}
    let state_tortuga(tiempo: int ) (obstacle: Obstacle list) (idx : int) =
        if tiempo % 10 = 0 then
            {obstacle with Underwater = not Underwater}
        else
            obstacle

    let updateFondo (fondo : Fondo) = 
        let newObstacles = Map.map (moveObstacle fondo.Obstacles)
        let newTime = fondo.Time - 1
        {fondo with Obstacles = newObstacles; Time = newTime}

    let updateGameState (game : gameState) (Option<Direction>) = 
            match Option<Direction> with
            | Some dir -> let newObstacles = List.map (moveObstacle game.Fondo.Obstacles)
                          let newFondo = {game.Fondo with Obstacles = newObstacles}
                          let newPlayer = movePlayer game.Player dir
                          {game with Player = newPlayer; Fondo = newFondo}
            | None -> let newObstacles = List.map (moveObstacle game.Fondo.Obstacles)
                      let newFondo = {game.Fondo with Obstacles = newObstacles}
                      {game with Fondo = newFondo}
            
    // Chequear si se acabó el tiempo
    let CheckTime (game: GameState) = 
        if game.Fondo.Time = 0 then
            {game with Lifes = GameOver}
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
            { game with Final_row = updatedGoalSpaces }
        else
            updateLives game
        

    // Chequear si ganó el nivel, ie, si todos los elementos de finalrow son True
    let CheckWin (game : GameState) = 
        if List.forall (fun goal -> goal.Ocupation) game.Final_row then
            let newFinalRow = List.map (fun goal -> { goal with Ocupation = false }) game.Final_row
            { game with Score = game.Score + 1000; Lifes = ThreeLives; Player = { PosX = WIDTH/2; PosY = One; Width = 40 }; Final_row = newFinalRow }
        else
            game

    let updateLives (game : GameState) = 
        let lifes = game.Lifes
        match lifes with
        | GameOver -> game
        | OneLife -> {game with Lifes = GameOver}
        | TwoLives -> {game with Lifes = OneLife}
        | ThreeLives -> {game with Lifes = TwoLives}

    let checkPlayerLose (game : GameState) =
        let player = game.Player
        let PosY = player.PosY
        match PosY with
        | One | Seven -> game
        | Two | Three | Four | Five | Six -> let obstacles = Map.find PosY game.Fondo.Obstacles
                                            let collision = List.exists (checkCollision player) obstacles
                                            if collision then
                                                let newGame = updateLives game
                                                let newPlayer = {player with PosX = WIDTH/2; PosY = Rows.One}
                                                {newGame with Player = newPlayer}
                                            else
                                                game
        | Eight | Nine | Ten | Eleven | Twelve -> let obstacles = Map.find PosY game.Fondo.Obstacles
                                                let drown = List.exists (checkNotUpLogTurtle player) obstacles
                                                if drown then
                                                    let newGame = updateLives game
                                                    let newPlayer = {player with PosX = WIDTH/2; PosY = Rows.One}
                                                    {newGame with Player = newPlayer}
                                                else
                                                    game

        | Thirteen -> //Chequear si ganó o perdío una vida
