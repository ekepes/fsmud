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

    type Room = {
        Name: string;
        Location: Point
        }

    type Delta = {
        dX: int;
        dY: int
        }

    type Command = 
        | Move of delta : Delta
        | Quit
        | Illegal of message : string

    let CreateMap =
        let map = [ 
            { Name = "Gatehouse"; Location = { X = 2; Y = 0 } }; 
            { Name = "Great Hall"; Location = { X = 2; Y = -1 } }; 
            { Name = "Front Lawn"; Location = { X = 2; Y = 1 } } ]

        map

    let isLocation checkLocation elem = (elem.Location.X = checkLocation.X && elem.Location.Y = checkLocation.Y)

    let IsValidLocation (map: list<Room>) (location: Point) : bool =
        (List.tryFindIndex (isLocation location) map) <> None

    let FindRoom (map: list<Room>) (location: Point) : Room = 
        List.find (isLocation location) map

    let UpdateDisplay character map =
        let currentLocation = FindRoom map character.Location
        printfn "Now at (%s)." currentLocation.Name

    let AcceptCommand character = 
        printf "Enter command for %s: " character.Name
        let command = Console.ReadLine().ToLower();
        match command with
        | "north" -> Move({ dX = 0; dY = -1 })
        | "south" -> Move({ dX = 0; dY = 1 })
        | "east" -> Move({ dX = 1; dY = 0 })
        | "west" -> Move({ dX = -1; dY = 0 })
        | "quit" -> Quit
        | _ -> Illegal("I don't understand.")

    let Move map character delta = 
        let newLocation = { X = (character.Location.X + delta.dX); Y = (character.Location.Y + delta.dY) }
        if IsValidLocation map newLocation then { Name = character.Name; Location = newLocation }
        else character

    let GameLoop name =
        let map = CreateMap
        let mutable character = { Name = name; Location = { X = 2; Y = 0 } }
        let mutable continueLooping = true 

        while continueLooping do
            UpdateDisplay character map

            let command = AcceptCommand character

            match command with
            | Move delta -> character <- Move map character delta
            | Quit -> continueLooping <- false
            | Illegal message -> printfn "%s" message

        printfn "Thanks for playing!"
        0