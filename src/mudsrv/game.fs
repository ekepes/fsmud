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

    type Exit = {
        Direction: Direction
        StartRoom: int;
        EndRoom: int
        }

    type Map = {
        Rooms: list<Room>;
        Exits: list<Exit>
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
            { Id = 0; Name = "Reception Desk" }; 
            { Id = 1; Name = "Water Cooler" }; 
            { Id = 2; Name = "Parking Lot" };
            { Id = 3; Name = "Boss's Office"};
            { Id = 4; Name = "Copy Room"};
            { Id = 5; Name = "Cube Farm"}
            ]

        let exits = [
            { Direction = North; StartRoom = 0; EndRoom = 5 };
            { Direction = South; StartRoom = 5; EndRoom = 0 };
            { Direction = South; StartRoom = 0; EndRoom = 2 };
            { Direction = North; StartRoom = 2; EndRoom = 0 };
            { Direction = East; StartRoom = 5; EndRoom = 1 };
            { Direction = West; StartRoom = 1; EndRoom = 5 };
            { Direction = West; StartRoom = 5; EndRoom = 4 };
            { Direction = East; StartRoom = 4; EndRoom = 5 };
            { Direction = North; StartRoom = 1; EndRoom = 3 };
            { Direction = South; StartRoom = 3; EndRoom = 1 }
            ]

        { Rooms = rooms; Exits = exits }

    let FindRoom map id = 
        List.find (fun r -> r.Id = id) map.Rooms

    let FindLegalMoves map id = 
        List.filter (fun h -> h.StartRoom = id) map.Exits

    let GetLegalMoves map id =
        List.collect(fun exit -> [ exit.Direction ]) (FindLegalMoves map id)

    let GetLegalMoveStrings map id =
        List.collect(fun exit -> [ DirectionName exit.Direction ]) (FindLegalMoves map id)

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
        match List.tryFind (fun exit -> exit.StartRoom = character.Location.Id && exit.Direction = direction) (map.Exits) with
        | Some exit -> { Name = character.Name; Location = FindRoom map exit.EndRoom }
        | None ->
            printfn "That is not a valid exit."
            character

    let rec playGame map character = 
        UpdateDisplay character map        
        match (AcceptCommand character) with
        | Move direction -> playGame map (Move map character direction)
        | Quit -> printfn "Thanks for playing!"
        | Illegal message -> 
            printfn "%s" message
            playGame map character        

    let GameLoop name =
        let map = CreateMap
        let character = { Name = name; Location = FindRoom map 0 }
        playGame map character
        0