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
        let turns = Reversi.Core.GetTurns' state.board state.move state.player
        let newBoard = Reversi.Core.Move state.board state.move turns state.player 0 ""
        let newPlayer = if state.player = 'o' then 'x' else 'o'
        let validMoves = Reversi.Core.GetValidMoves newBoard newPlayer 0 []

        {
            player = newPlayer;
            move = state.move;
            board = newBoard;
            turns = turns;
            validMoves = validMoves;
        }         