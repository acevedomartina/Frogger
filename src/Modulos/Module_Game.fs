namespace Frogger.Modulos

open Frogger.Modulos.Module_Player
open Frogger.Modulos.Module_Grid
open Frogger.Modulos.Module_Fondo
open Frogger.Modulos.Module_Interactions

module Module_Game =
////////////////// Tipo principal del estado de juego /////////////////////////

    type GameState =
        {
            Player: Player // Jugador
            Final_row: List<GoalSpace> // Espacios de meta
            Score: int // Puntaje actual
            Lifes: LivesRemaining // Vidas restantes
            Fondo: Fondo // Fondo del juego
        }

    let TIME_LIMIT = 60 // Tiempo de vida de cada ranita
    let CORRECT_MOVEMENT = 10 // Puntaje por cada movimiento correcto
    let FILL_ONE_GOAL = 50 // Puntaje por llenar un espacio de meta
    let FILL_ALL_GOALS = 1000 // Puntaje por llenar todos los espacios de meta

////////////////// Funciones que auxiliares para la actualización del juego dentro del main /////////////////////////

    // Función que actualiza la cantidad de vidas restantes, si aún qudean vidas reinicia el fondo
    let updateLives (game : GameState) : GameState = 
        let lifes = game.Lifes
        match lifes with
        | GameOver -> game
        | OneLife -> {game with Lifes = GameOver; Fondo = {game.Fondo with Time = TIME_LIMIT}}
        | TwoLives -> {game with Lifes = OneLife; Fondo = {game.Fondo with Time = TIME_LIMIT}}
        | ThreeLives -> {game with Lifes = TwoLives; Fondo = {game.Fondo with Time = TIME_LIMIT}}

    // Chequear si se acabó el tiempo, si es así, se actualiza la posición del jugador y el fondo con las vidas restantes
    let CheckTime (game: GameState) : GameState = 
        if game.Fondo.Time = 0 then
            let newPlayer = {game.Player with PosX = WIDTH/2; PosY = Rows.One}
            let newGame = updateLives game
            {newGame with Player = newPlayer}
        else
            game
    
    // Función que chequea si el jugador llegó a alguna meta, debe entrar por completo en el espacio de meta
    let CheckWinAux (player : Player) (goal_space : GoalSpace) : GoalSpace = 
        let player_xright = player.PosX + player.Width / 2
        let player_xleft = player.PosX - player.Width / 2
        if player_xleft > goal_space.PosX && player_xright < goal_space.PosX + goal_space.Width then
            { goal_space with Ocupation = true }          
        else
            { goal_space with Ocupation = false }

    // Función que chequea si el jugador llegó a alguna meta, si lo hace se actualiza el puntaje y se reinicia el fondo
    // En caso que no haya llegado a ninguna meta, se actualiza la cantidad de vidas restantes
    let CheckGoal (game : GameState) =
        let player = game.Player
        let goal_spaces = game.Final_row
        let updatedGoalSpaces = List.map (CheckWinAux player) goal_spaces
        let anyGoalSpaceChanged = List.exists (fun gs -> gs.Ocupation) updatedGoalSpaces
        if anyGoalSpaceChanged then
            { game with Final_row = updatedGoalSpaces; Score = game.Score + FILL_ONE_GOAL; Fondo = {game.Fondo with Time = TIME_LIMIT}}
        else
            updateLives game
    
    // Chequear si ganó el nivel, ie, si todos los elementos de finalrow son True
    let CheckWin (game : GameState) : GameState = 
        if List.forall (fun goal -> goal.Ocupation) game.Final_row then
            let newFinalRow = List.map (fun goal -> { goal with Ocupation = false }) game.Final_row
            {game with Score = game.Score + FILL_ALL_GOALS; Lifes = ThreeLives; Player = { PosX = WIDTH/2; PosY = One; Width = game.Player.Width }; Final_row = newFinalRow}
        else
            game

////////////////// Funciones que actualizan el estado del juego/////////////////////////

    // Función que actualiza el estado del juego según una dirección de movimiento o no movimiento
    let updateGameState (game : Result<GameState, string>) (dir : Option<Direction>) : GameState = 
            match game with
            | Ok game ->
                match dir with
                | Some d -> let newFondo = updateFondo game.Fondo
                            let newPlayer = movePlayer game.Player d
                            let newScore = game.Score + CORRECT_MOVEMENT
                            CheckTime {game with Player = newPlayer; Fondo = newFondo; Score = newScore}
                        
                | None -> let newFondo = updateFondo game.Fondo
                          CheckTime {game with Fondo = newFondo}
            | Error e -> raise (System.Exception(e))

    // Función que chequea si el jugador perdió una vida o si se acabó el juego, dado que damos diez puntos por cada paso correcto lo restamos si es que el movimiento fue incorrecto
    let checkPlayerLose (game : GameState) : Result<GameState, string>=
        let player = game.Player
        let PosY = player.PosY
        match PosY with
        // Si el jugador está en la fila 1, 7 unicamente chequeamos que no se le hayan acabado las vidas por tiempo
        | One | Seven -> match game.Lifes with
                         | GameOver -> Error "Game Over"
                         | _ -> Ok game
        // Si el jugador está en la fila 2, 3, 4, 5, 6 chequeamos que no haya colisión con los obstáculos de la calle
        | Two | Three | Four | Five | Six ->    let obstacles = Map.find PosY game.Fondo.Obstacles
                                                let collision = List.exists (checkCollision player) obstacles
                                                if collision then
                                                    let newGame = updateLives game
                                                    match newGame.Lifes with
                                                    | GameOver -> Error "Game Over"
                                                    | _ -> let newPlayer = {player with PosX = WIDTH/2; PosY = Rows.One}
                                                           let newScore = newGame.Score - CORRECT_MOVEMENT
                                                           Ok {newGame with Player = newPlayer; Score = newScore}
                                                else
                                                    Ok game
        // Si el jugador está en la fila 8, 9, 10, 11, 12 chequeamos que no esté arriba de un tronco o una tortuga o sea que se ahogue
        | Eight | Nine | Ten | Eleven | Twelve ->   let obstacles = Map.find PosY game.Fondo.Obstacles
                                                    let lista = List.map (checkNotUpLogTurtle player) obstacles
                                                    let drown = List.forall (fun x -> x) lista
                                                    if drown then
                                                        let newGame = updateLives game
                                                        match newGame.Lifes with
                                                            | GameOver -> Error "Game Over"
                                                            | _ ->  let newPlayer = {player with PosX = WIDTH/2; PosY = Rows.One}
                                                                    let newScore = newGame.Score - CORRECT_MOVEMENT
                                                                    Ok {newGame with Player = newPlayer; Score = newScore}
                                                    else
                                                        Ok game 
        // Si el jugador está en la fila 13, chequeamos si llegó a la meta o si justo perdió una vida o si ganó el juego
        | Thirteen ->   let newGame = {CheckGoal game with Player = { PosX = WIDTH/2; PosY = One; Width = game.Player.Width }}
                        match newGame.Lifes with
                        | GameOver -> Error "Game Over"
                        | _ ->  let newGame = CheckWin newGame
                                match newGame.Lifes with
                                | GameOver -> Error "Game Over"
                                | _ -> Ok newGame
    
    // Función final que actualiza el estado del juego para volver a usar updateGameState
    let final (gameR : Result<GameState, string>) : Result<GameState, string> = 
        match gameR with
        | Ok game -> Ok (CheckWin game)
        | Error e -> Error e