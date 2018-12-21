module Reversi.Tests

// dotnet test

open System
open Xunit
open Reversi.Core

let board = [|
    "----oxox";
    "---oxx-o";
    "-oxxxxxo";
    "--------"; // oooooooo
    "--------";
    "--------";
    "--------";
    "--------";
|]

[<Fact>]
let ``A4 North`` () = Assert.False(IsValidMove board North "A4" 0 'o')
[<Fact>]
let ``B4 North`` () = Assert.False(IsValidMove board North "B4" 0 'o')
[<Fact>]
let ``C4 North`` () = Assert.False(IsValidMove board North "C4" 0 'o')
[<Fact>]
let ``D4 North`` () = Assert.True(IsValidMove board North "D4" 0 'o')
[<Fact>]
let ``E4 North`` () = Assert.True(IsValidMove board North "E4" 0 'o')
[<Fact>]
let ``F4 North`` () = Assert.False(IsValidMove board North "F4" 0 'o')
[<Fact>]
let ``G4 North`` () = Assert.False(IsValidMove board North "G4" 0 'o')
[<Fact>]
let ``H4 North`` () = Assert.False(IsValidMove board North "H4" 0 'o')
