namespace Reversi.Controllers

open Microsoft.AspNetCore.Mvc

type Game = {
    player: char;
    move: string;
    board: string[];
    turns: string[];
    validMoves: string list;
}

[<Route("api/[controller]")>]
[<ApiController>]
type GameController () =
    inherit ControllerBase()

    [<HttpPost>]
    member this.Post([<FromBody>] state:Game) =
        let turns = Reversi.Core.getTurns state.board state.player state.move 
        let newBoard = Reversi.Core.move state.board state.player state.move turns 0 ""
        let newPlayer = if state.player = 'o' then 'x' else 'o'
        let validMoves = Reversi.Core.getValidMoves newBoard newPlayer 0 []

        {
            player = newPlayer;
            move = state.move;
            board = newBoard;
            turns = turns;
            validMoves = validMoves;
        }         