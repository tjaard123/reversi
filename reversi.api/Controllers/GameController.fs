namespace reversi.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc

[<Route("api/[controller]")>]
[<ApiController>]
type GameController () =
    inherit ControllerBase()

    let gameState = [|
        "--------";
        "--------";
        "--------";
        "---ox---";
        "---xo---";
        "--------";
        "--------";
        "--------";
    |]

    let AtN gameState x y = if x = 0 then '-' else gameState[x - 1][y]
    let AtNE gameState x y = if x = 0 || y = 7  then '-'  else gameState[x - 1][y + 1]
    let AtE gameState x y = if y = 7 then '-'  else gameState[x][y + 1]
    let AtSE gameState x y = if x = 7 || y = 7 then '-'  else gameState[x + 1][y + 1]
    let AtS gameState x y = if x = 7 then '-'  else gameState[x + 1][y]
    let AtSW gameState x y = if x = 7 || y = 0 then '-'  else gameState[x + 1][y - 1]
    let AtW gameState x y = if y = 0 then '-'  else gameState[x][y - 1]
    let AtNW gameState x y = if x = 0 || y = 0 then '-'  else gameState[x - 1][y - 1]

    let otherPlayer player = if player = 'o' then 'x' else 'o'

    let rec IsValidNorth board x y i player =
        match (i, x + i) with
        | (0, _) -> IsValidNorth board x y (i + 1) player
        | (1, 8) -> false
        | (1, xi) ->
            if board[xi][y] = player
            then false
            else IsValidNorth board x y (i + 1) player
        | _ -> 
            if board[x + i][y] = player
            then true
            else IsValidNorth board x y (i + 1) player

    [<HttpGet>]
    member this.Get() =
        ActionResult<string[]>(gameState)
