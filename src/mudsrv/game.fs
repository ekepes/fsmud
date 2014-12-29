namespace EWK.MudSrv

module Game =

    open System

    type Direction = 
        | North
        | South
        | East
        | West

    let DirectionName direction =
        match direction with
        | North -> "north"
        | South -> "south"
        | East -> "east"
        | West -> "west"

    type Room = {
        Id: int;
        Name: string
        }

    type Hallway = {
        Direction: Direction
        StartRoom: int;
        EndRoom: int
        }

    type Map = {
        Rooms: list<Room>;
        Hallways: list<Hallway>
        }

    type Character = {
        Name: string;
        Location: Room
        }

    type Command = 
        | Move of direction : Direction
        | Quit
        | Illegal of message : string

    let CreateMap =
        let rooms = [ 
            { Id = 0; Name = "Gatehouse" }; 
            { Id = 1; Name = "Great Hall" }; 
            { Id = 2; Name = "Front Lawn" } ]

        let hallways = [
            { Direction = North; StartRoom = 0; EndRoom = 1 };
            { Direction = South; StartRoom = 1; EndRoom = 0 };
            { Direction = South; StartRoom = 0; EndRoom = 2 };
            { Direction = North; StartRoom = 2; EndRoom = 0 }]

        { Rooms = rooms; Hallways = hallways }

    let FindRoom map id = 
        List.find (fun r -> r.Id = id) map.Rooms

    let FindLegalMoves map id = 
        List.filter (fun h -> h.StartRoom = id) map.Hallways

    let GetLegalMoves map id =
        List.collect(fun hallway -> [ hallway.Direction ]) (FindLegalMoves map id)

    let GetLegalMoveStrings map id =
        List.collect(fun hallway -> [ DirectionName hallway.Direction ]) (FindLegalMoves map id)

    let GetLegalMovesMessage map id =
        String.concat ", " (GetLegalMoveStrings map id)

    let UpdateDisplay character map =
        printfn "Now at (%s). Valid Moves are (%s)." character.Location.Name (GetLegalMovesMessage map character.Location.Id)

    let AcceptCommand character = 
        printf "Enter command for %s: " character.Name
        let command = Console.ReadLine().ToLower();
        match command with
        | "north" -> Move(North)
        | "south" -> Move(South)
        | "east" -> Move(East)
        | "west" -> Move(West)
        | "quit" -> Quit
        | _ -> Illegal("I don't understand.")

    let Move map character direction = 
        match List.tryFind (fun hallway -> hallway.StartRoom = character.Location.Id && hallway.Direction = direction) (map.Hallways) with
        | Some hallway -> { Name = character.Name; Location = FindRoom map hallway.EndRoom }
        | None -> character

    let GameLoop name =
        let map = CreateMap
        let mutable character = { Name = name; Location = FindRoom map 0 }
        let mutable continueLooping = true 

        while continueLooping do
            UpdateDisplay character map

            let command = AcceptCommand character

            match command with
            | Move direction -> character <- Move map character direction
            | Quit -> continueLooping <- false
            | Illegal message -> printfn "%s" message

        printfn "Thanks for playing!"
        0