open System
open EWK.MudSrv.Game

[<EntryPoint>]
let main argv = 
    printfn "What is your name?"

    let name = Console.ReadLine();
    printfn "Hello, %s" name
    let character = { Name = name; Location = { X = 0; Y = 0 } }

    let newCharacter = GameLoop character
    printfn "Now at (%i, %i)." newCharacter.Location.X newCharacter.Location.Y

    0 // return an integer exit code