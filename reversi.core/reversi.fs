module Reversi.Core

type Direction = North | NorthWest | West | SouthWest | South | SouthEast | East | NorthEast
let allMoves = [|
    "A1"; "B1"; "C1"; "D1"; "E1"; "F1"; "G1"; "H1";
    "A2"; "B2"; "C2"; "D2"; "E2"; "F2"; "G2"; "H2";
    "A3"; "B3"; "C3"; "D3"; "E3"; "F3"; "G3"; "H3";
    "A4"; "B4"; "C4"; "D4"; "E4"; "F4"; "G4"; "H4";
    "A5"; "B5"; "C5"; "D5"; "E5"; "F5"; "G5"; "H5";
    "A6"; "B6"; "C6"; "D6"; "E6"; "F6"; "G6"; "H6";
    "A7"; "B7"; "C7"; "D7"; "E7"; "F7"; "G7"; "H7";
    "A8"; "B8"; "C8"; "D8"; "E8"; "F8"; "G8"; "H8";
    |]

let c2i = System.Globalization.CharUnicodeInfo.GetDigitValue
let pos2xy (pos:string) = (int pos.[0] - int 'A', (c2i pos.[1]) - 1)

let at (board: string[]) pos =
    match pos2xy pos with
    | (x, y) when x >= 0 && x <= 7 && y >= 0 && y <= 7 -> Some(board.[y].[x])
    | _ -> None

let advance (pos : string) direction n = 
    match direction with
    | North -> string pos.[0] + string ((c2i pos.[1]) - n)
    | NorthWest -> string (char (int pos.[0] + n)) + string ((c2i pos.[1]) - n)
    | West -> string (char (int pos.[0] + n)) + string pos.[1]
    | SouthWest -> string (char (int pos.[0] + n)) + string ((c2i pos.[1]) + n)
    | South -> string pos.[0] + string ((c2i pos.[1]) + n)
    | SouthEast -> string (char (int pos.[0] - n)) + string ((c2i pos.[1]) + n)
    | East -> string (char (int pos.[0] - n)) + string pos.[1]
    | NorthEast -> string (char (int pos.[0] - n)) + string ((c2i pos.[1]) - n)

let rec IsValidDirection board direction pos i player =
    printfn "i%d: %s > %A" i pos (at board pos)
    match i with
    | 0 -> IsValidDirection board direction (advance pos direction 1) (i + 1) player
    | 1 ->
        match at board pos with
        | Some(v) when v = player || v = '-' -> false
        | Some(_) -> IsValidDirection board direction (advance pos direction 1) (i + 1) player
        | None -> false
    | _ ->
        match at board pos with
        | Some(v) when v = player -> true
        | Some(v) when v = '-' -> false
        | Some(_) -> IsValidDirection board direction (advance pos direction 1) (i + 1) player
        | None -> false

let IsValidMove board pos player =
    if at board pos = Some('-') && (IsValidDirection board North pos 0 player
        || IsValidDirection board NorthWest pos 0 player
        || IsValidDirection board West pos 0 player
        || IsValidDirection board SouthWest pos 0 player
        || IsValidDirection board South pos 0 player
        || IsValidDirection board SouthEast pos 0 player
        || IsValidDirection board East pos 0 player
        || IsValidDirection board NorthEast pos 0 player)
    then true
    else false

let rec GetValidMoves board player i validMoves =
    if i = allMoves.Length then validMoves
    else
        if IsValidMove board allMoves.[i] player
        then GetValidMoves board player (i + 1) (allMoves.[i] :: validMoves)
        else GetValidMoves board player (i + 1) validMoves


// let startBoard = [|
//     "--------";
//     "--------";
//     "--------";
//     "-oxox---";
//     "---xo---";
//     "--------";
//     "--------";
//     "--------";
// |]
// printfn "%A" (GetValidMoves startBoard 'x' 0 [])
// printfn "%A" (IsValidMove startBoard "C4" 'x')