open Reversi.Core

let board = [|
    "-xox---o";
    "-oxxx-o-";
    "-oxxxxxx";
    "--------";
    "--------";
    "--------";
    "--------";
    "--------";
|]

// Tests
let a4 = IsValidMove board North "A4" 0 'o' // false
let b4 = IsValidMove board North "B4" 0 'o' // false
let c4 = IsValidMove board North "C4" 0 'o' // true
let d4 = IsValidMove board North "D4" 0 'o' // false
let e4 = IsValidMove board North "E4" 0 'o' // false
let f4 = IsValidMove board North "F4" 0 'o' // false
let g4 = IsValidMove board North "G4" 0 'o' // true
let h4 = IsValidMove board North "H4" 0 'o' // false