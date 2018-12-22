namespace Reversi.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc

type BoardState = { player: char; board: string[] }

[<Route("api/[controller]")>]
[<ApiController>]
type GameController () =
    inherit ControllerBase()

    [<HttpPost>]
    member this.Post([<FromBody>] state:BoardState) =
        let validMoves = Reversi.Core.GetValidMoves state.board state.player 0 []
        validMoves