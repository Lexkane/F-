module Game

type Move = 
    | Rock
    | Paper
    | Scissors
    | Lizard
    | Spock

    static member (-) (x, y) =
        match x, y with
        | Move.Scissors, Move.Paper -> 1
        | Move.Scissors, Move.Lizard -> 1
        | Move.Scissors, Move.Rock -> 2
        | Move.Scissors, Move.Spock -> 2
        | Move.Lizard, Move.Paper -> 1
        | Move.Lizard, Move.Rock -> 2
        | Move.Lizard, Move.Scissors -> 2
        | Move.Lizard, Move.Spock -> 1
        | Move.Paper, Move.Lizard -> 1
        | Move.Paper, Move.Rock -> 2
        | Move.Paper, Move.Scissors -> 2
        | Move.Paper, Move.Spock -> 1
        | Move.Spock, Move.Paper -> 2
        | Move.Spock, Move.Rock -> 1
        | Move.Spock, Move.Scissors -> 1
        | Move.Spock, Move.Lizard -> 2
        | Move.Rock, Move.Paper -> 2
        | Move.Rock, Move.Spock -> 2
        | Move.Rock, Move.Scissors -> 1
        | Move.Rock, Move.Lizard -> 1
        | (x, y) when (x = y) -> 0

let GetRandomMove n = 
    let rnd = System.Random();
    let output = [ for i in 1 .. n -> 
        let index = rnd.Next(0, 5)
        match index with
        | 0 -> Move.Rock
        | 1 -> Move.Paper
        | 2 -> Move.Scissors
        | 3 -> Move.Lizard
        | 4 -> Move.Spock
    ]
    output

let GetRoundOutputText moves =
    match moves with
    | (Move.Scissors, Move.Paper) | (Move.Paper, Move.Scissors) -> "Scissors cuts Paper"
    | (Move.Paper, Move.Rock) | (Move.Rock, Move.Paper) -> "Paper covers Rock"
    | (Move.Rock, Move.Lizard) | (Move.Lizard, Move.Rock) -> "Rock crushes Lizard"
    | (Move.Lizard, Move.Spock) | (Move.Spock, Move.Lizard) -> "Lizard poisons Spock"
    | (Move.Spock, Move.Scissors) | (Move.Scissors, Move.Spock) -> "Spock smashes Scissors"
    | (Move.Scissors, Move.Lizard) | (Move.Lizard, Move.Scissors) -> "Scissors decapitates Lizard"
    | (Move.Lizard, Move.Paper) | (Move.Paper, Move.Lizard) -> "Lizard eats Paper"
    | (Move.Paper, Move.Spock) | (Move.Spock, Move.Paper) -> "Paper disproves Spock"
    | (Move.Spock, Move.Rock) | (Move.Rock, Move.Spock) -> "Spock vaporizes Rock"
    | (Move.Rock, Move.Scissors) | (Move.Scissors, Move.Rock) -> "Rock crushes scissors"
    | (x, y) when (x = y) -> "Draw"
    | (_, _) -> "Unknown move pair"
    
let GetMovesFromInput (input : string) =
    let inputList = Array.toList (input.Trim().Split [|' '|])
    let moves = List.map (fun (x: string) -> 
        match x.ToUpper() with
        | "R" -> Move.Rock
        | "P" -> Move.Paper
        | "S" -> Move.Scissors
        | "L" -> Move.Lizard
        | "M" -> Move.Spock ) inputList
    moves