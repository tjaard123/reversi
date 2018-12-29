module Reversi.Tests

// dotnet test

open System
open Xunit
open Reversi.Core

let startBoard = [|
    "--------";
    "--------";
    "--------";
    "---ox---";
    "---xo---";
    "--------";
    "--------";
    "--------";
|]

let testBoard = [|
    "----oxox";
    "---oxx-o";
    "-oxxxxxo";
    "--------";
    "--------";
    "-oxxxxxo";
    "---oxx-o";
    "----oxox";
|]

[<Fact>]
let ``Starting Moves`` () = Assert.Equal([|"E6"; "F5"; "C4"; "D3"|], getValidMoves startBoard 'x' 0 [])

[<Fact>]
let ``Valid Moves`` () = Assert.Equal([|"G7"; "H5"; "F5"; "E5"; "D5"; "B5"; "H4"; "F4"; "E4"; "D4"; "B4"; "G2"|], getValidMoves testBoard 'o' 0 [])