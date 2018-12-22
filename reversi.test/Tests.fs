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

let randomTest = [|
    "--------";
    "--------";
    "--------";
    "-oxox---";
    "---xo---";
    "--------";
    "--------";
    "--------";
|]

[<Fact>]
let ``Starting Moves`` () = Assert.Equal([|"E6"; "F5"; "C4"; "D3"|], GetValidMoves startBoard 'x' 0 [])

[<Fact>]
let ``Random Test`` () = Assert.Equal([|"E3"; "B4"; "F4"; "C5"; "D6"|], GetValidMoves randomTest 'x' 0 [])

let board = [|
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
let ``North Moves`` () =
    Assert.False(IsValidDirection board North "A4" 0 'o')
    Assert.False(IsValidDirection board North "B4" 0 'o')
    Assert.False(IsValidDirection board North "C4" 0 'o')
    Assert.True(IsValidDirection board North "D4" 0 'o')
    Assert.True(IsValidDirection board North "E4" 0 'o')
    Assert.False(IsValidDirection board North "F4" 0 'o')
    Assert.False(IsValidDirection board North "G4" 0 'o')
    Assert.False(IsValidDirection board North "H4" 0 'o')

[<Fact>]
let ``South Moves`` () =
    Assert.False(IsValidDirection board South "A5" 0 'o')
    Assert.False(IsValidDirection board South "B5" 0 'o')
    Assert.False(IsValidDirection board South "C5" 0 'o')
    Assert.True(IsValidDirection board South "D5" 0 'o')
    Assert.True(IsValidDirection board South "E5" 0 'o')
    Assert.False(IsValidDirection board South "F5" 0 'o')
    Assert.False(IsValidDirection board South "G5" 0 'o')
    Assert.False(IsValidDirection board South "H5" 0 'o')