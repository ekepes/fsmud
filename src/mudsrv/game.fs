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
        | "north" -> Some({ X = 0; Y = -1 })
        | "south" -> Some({ X = 0; Y = 1 })
        | "east" -> Some({ X = 1; Y = 0 })
        | "west" -> Some({ X = -1; Y = 0 })
        | "quit" -> None
        | _ -> Some({ X = 0; Y = 0 })

    let Move map character delta = 
        let newLocation = { X = (character.Location.X + delta.X); Y = (character.Location.Y + delta.Y) }
        if IsValidLocation map newLocation then { Name = character.Name; Location = newLocation }
        else character

    let GameLoop name =
        let map = CreateMap
        let mutable character = { Name = name; Location = { X = 2; Y = 0 } }
        let mutable continueLooping = true 

        while continueLooping do
            UpdateDisplay character map

            let delta = AcceptCommand character

            if delta = None then 
                continueLooping <- false
            else
                character <- Move map character delta.Value

        printfn "Thanks for playing!"
        0