open System
open EWK.MudSrv.Game

[<EntryPoint>]
let main argv = 

    printfn "What is your name?"

    let name = Console.ReadLine();
    printfn "Hello, %s" name

    GameLoop name