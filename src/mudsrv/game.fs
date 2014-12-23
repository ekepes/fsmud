namespace EWK.MudSrv

module Game =

    open System

    type Point = {
        X: int;
        Y: int
        }

    type Character = {
        Name: string;
        Location: Point
        }


    let UpdateDisplay character =
        printfn "Now at (%i, %i)." character.Location.X character.Location.Y

    let AcceptCommand character = 
        printf "Enter command for %s: " character.Name
        let command = Console.ReadLine().ToLower();
        match command with
        | "north" -> Some({ X = 0; Y = -1 })
        | "south" -> Some({ X = 0; Y = 1 })
        | "east" -> Some({ X = 1; Y = 0 })
        | "west" -> Some({ X = -1; Y = 0 })
        | "quit" -> None
        | _ -> Some({ X = 0; Y = 0 })

    let Move character delta = 
        let newCharacter = { 
            Name = character.Name;
            Location = { X = (character.Location.X + delta.X); Y = (character.Location.Y + delta.Y) }
            }

        newCharacter

    let GameLoop name =
        let mutable character = { Name = name; Location = { X = 0; Y = 0 } }
        let mutable continueLooping = true 

        while continueLooping do
            let delta = AcceptCommand character

            if delta = None then 
                continueLooping <- false
            else
                character <- Move character delta.Value

                UpdateDisplay character

        printfn "Thanks for playing!"
        0