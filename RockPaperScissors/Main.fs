open Game
open System

[<EntryPoint>]
let main argv = 

    let mutable playerScore = 0
    let mutable computerScore = 0

    let playerMoves = GetMovesFromInput (Console.ReadLine())
    let n = playerMoves.Length
    printfn "You played: "
    for i in 0 .. n - 1 do
        printfn "%A" (playerMoves.Item(i))

    printfn ""
    printfn "Generating computer moves"
    let computerMoves = GetRandomMove n
    printfn "Computer played: "
    for i in 0 .. n - 1 do
        printfn "%A" (computerMoves.Item(i))

    printfn ""
    printfn "Results:"    

    for i in 0 .. n - 1 do
        printfn  "%s" (GetRoundOutputText (playerMoves.Item(i), computerMoves.Item(i)))
        let diff = (int)(playerMoves.Item(i) - computerMoves.Item(i))
        if diff = 1 then playerScore <- playerScore + 1
        elif diff = 2 then computerScore <- computerScore + 1
        printfn "Player: %d \t Computer: %d" playerScore computerScore

    let s = Console.ReadLine()
    printfn "%s" s
    