open System

[<EntryPoint>]
let main argv = 
    printfn "What is your name?"

    let name = Console.ReadLine();
    printfn "Hello, %s" name

    0 // return an integer exit code

