namespace Frogger.Modulos

open Frogger.Modulos.Module_Player
open Frogger.Modulos.Module_Grid
open Frogger.Modulos.Module_Fondo
open Frogger.Modulos.Module_Interactions

module Module_Game =

    type GameState =
        {
            Player: Player // Jugador
            Final_row: List<GoalSpace> // Espacios de meta
            Score: int // Puntaje actual
            Lifes: LivesRemaining // Vidas restantes
            Fondo: Fondo // Fondo del juego
        }

////////////////// Funciones que auxiliares para la actualización del juego /////////////////////////

    // Función que actualiza la cantidad de vidas restantes
    let updateLives (game : GameState) = 
        let lifes = game.Lifes
        match lifes with
        | GameOver -> game
        | OneLife -> {game with Lifes = GameOver; Fondo = {game.Fondo with Time = 60}}
        | TwoLives -> {game with Lifes = OneLife; Fondo = {game.Fondo with Time = 60}}
        | ThreeLives -> {game with Lifes = TwoLives; Fondo = {game.Fondo with Time = 60}}

    // Chequear si se acabó el tiempo
    let CheckTime (game: GameState) : GameState = 
        if game.Fondo.Time = 0 then
            let newPlayer = {game.Player with PosX = WIDTH/2; PosY = Rows.One}
            let newGame = updateLives game
            {newGame with Player = newPlayer}
        else
            game
    
    // Función que chequea si el jugador llegó a alguna meta
    let CheckWinAux (player : Player) (goal_space : GoalSpace) : GoalSpace = 
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
            { game with Final_row = updatedGoalSpaces; Score = game.Score + 50; Fondo = {game.Fondo with Time = 60}}
        else
            updateLives game
    
    // Chequear si ganó el nivel, ie, si todos los elementos de finalrow son True
    let CheckWin (game : GameState) : GameState = 
        if List.forall (fun goal -> goal.Ocupation) game.Final_row then
            let newFinalRow = List.map (fun goal -> { goal with Ocupation = false }) game.Final_row
            {game with Score = game.Score + 1000; Lifes = ThreeLives; Player = { PosX = WIDTH/2; PosY = One; Width = 40 }; Final_row = newFinalRow}
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
                            let newScore = game.Score + 10
                            CheckTime {game with Player = newPlayer; Fondo = newFondo; Score = newScore}
                        
                | None -> let newFondo = updateFondo game.Fondo
                          CheckTime {game with Fondo = newFondo}
            | Error e -> raise (System.Exception(e))

    // Función que chequea si el jugador perdió una vida o si se acabó el juego
    let checkPlayerLose (game : GameState) : Result<GameState, string>=
        let player = game.Player
        let PosY = player.PosY
        match PosY with
        | One | Seven -> match game.Lifes with
                         | GameOver -> Error "Game Over"
                         | _ -> Ok game
                               
        | Two | Three | Four | Five | Six ->    let obstacles = Map.find PosY game.Fondo.Obstacles
                                                let collision = List.exists (checkCollision player) obstacles
                                                if collision then
                                                    let newGame = updateLives game
                                                    match newGame.Lifes with
                                                    | GameOver -> Error "Game Over"
                                                    | _ -> let newPlayer = {player with PosX = WIDTH/2; PosY = Rows.One}
                                                           let newScore = newGame.Score - 10
                                                           Ok {newGame with Player = newPlayer; Score = newScore}
                                                else
                                                    Ok game

        | Eight | Nine | Ten | Eleven | Twelve ->   let obstacles = Map.find PosY game.Fondo.Obstacles
                                                    let lista = List.map (checkNotUpLogTurtle player) obstacles
                                                    let drown = List.forall (fun x -> x) lista
                                                    if drown then
                                                        let newGame = updateLives game
                                                        match newGame.Lifes with
                                                            | GameOver -> Error "Game Over"
                                                            | _ ->  let newPlayer = {player with PosX = WIDTH/2; PosY = Rows.One}
                                                                    let newScore = newGame.Score - 10
                                                                    Ok {newGame with Player = newPlayer; Score = newScore}
                                                    else
                                                        Ok game 
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