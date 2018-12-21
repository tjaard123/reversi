namespace Reversi

module Core

    type Direction = North | NorthWest | West | SouthWest | South | SouthEast | East | NorthEast

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

    let rec IsValidMove board direction pos i player =
        printfn "i%d: %s > %A" i pos (at board pos)
        match i with
        | 0 -> IsValidMove board direction (advance pos direction 1) (i + 1) player
        | 1 ->
            match at board pos with
            | Some(v) when v = player || v = '-' -> false
            | Some(_) -> IsValidMove board direction (advance pos direction 1) (i + 1) player
            | None -> false
        | _ ->
            match at board pos with
            | Some(v) when v = player -> true
            | Some(v) when v = '-' -> false
            | Some(_) -> IsValidMove board direction (advance pos direction 1) (i + 1) player
            | None -> false