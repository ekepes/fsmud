open System

type Point = {
    X: int;
    Y: int
    }

type Character = {
    Name: string;
    Location: Point
    }

let AcceptCommand = 
    printf "Enter command: "
    let command = Console.ReadLine().ToLower();
    match command with
    | "north" -> Some({ X = 0; Y = -1 })
    | "south" -> Some({ X = 0; Y = 1 })
    | "east" -> Some({ X = 1; Y = 0 })
    | "west" -> Some({ X = -1; Y = 0 })
    | "quit" -> None
    | _ -> Some({ X = 0; Y = 0 })

let Move character delta : Character = 
    let newCharacter = { 
        Name = character.Name;
        Location = { X = (character.Location.X + delta.X); Y = (character.Location.Y + delta.Y) }
        }

    newCharacter

let GameLoop character =
    let command = AcceptCommand

    match command with
    | Some delta -> Move character delta
    | None -> character

[<EntryPoint>]
let main argv = 
    printfn "What is your name?"

    let name = Console.ReadLine();
    printfn "Hello, %s" name
    let character = { Name = name; Location = { X = 0; Y = 0 } }

    let newCharacter = GameLoop character
    printfn "Now at (%i, %i)." character.Location.X character.Location.Y

    0 // return an integer exit code