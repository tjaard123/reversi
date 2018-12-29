module Reversi.Core

type Direction = North | NorthWest | West | SouthWest | South | SouthEast | East | NorthEast
let Positions = [|
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

let rec getTurnsInDirection board player direction pos i turns =
    match i with
    | 0 -> getTurnsInDirection board player direction (advance pos direction 1) (i + 1) turns
    | 1 ->
        match at board pos with
        | Some(v) when v = player || v = '-' -> [||]
        | Some(_) -> getTurnsInDirection board player direction (advance pos direction 1) (i + 1) (Array.append [|pos|] turns)
        | None -> [||]
    | _ ->
        match at board pos with
        | Some(v) when v = player -> turns
        | Some(v) when v = '-' -> [||]
        | Some(_) -> getTurnsInDirection board player direction (advance pos direction 1) (i + 1) (Array.append [|pos|] turns)
        | None -> [||]

let getTurns board player move =
    Array.concat [|
        getTurnsInDirection board player North move 0 [||];
        getTurnsInDirection board player NorthWest move 0 [||];
        getTurnsInDirection board player West move 0 [||];
        getTurnsInDirection board player SouthWest move 0 [||];
        getTurnsInDirection board player South move 0 [||];
        getTurnsInDirection board player SouthEast move 0 [||];
        getTurnsInDirection board player East move 0 [||];
        getTurnsInDirection board player NorthEast move 0 [||]
    |]

let rec getValidMoves board player i validMoves =
    if i = Positions.Length then validMoves
    else
        if (at board Positions.[i] = Some('-')) && (getTurns board player Positions.[i]).Length > 0
        then getValidMoves board player (i + 1) (Positions.[i] :: validMoves)
        else getValidMoves board player (i + 1) validMoves

let rec move board player pos turns i (newBoard:string) =
    if i = Positions.Length then [|newBoard.[0..7]; newBoard.[8..15]; newBoard.[16..23]; newBoard.[24..31]; newBoard.[32..39]; newBoard.[40..47]; newBoard.[48..55]; newBoard.[56..63] |]
    else
        if Positions.[i] = pos || Array.exists (fun x -> x = Positions.[i]) turns then move board player pos turns (i + 1) (newBoard + string player)
        else move board player pos turns (i + 1) (newBoard + string (at board Positions.[i]).Value)  // Unchanged