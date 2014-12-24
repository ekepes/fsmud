namespace EWK.MudSrv.Tests

module GameTests = 
    open System
    open NUnit.Framework
    open EWK.MudSrv.Game

    let CreateTestMap =
        let map = [ 
            { Name = "Gatehouse"; Location = { X = 2; Y = 0 } }; 
            { Name = "Great Hall"; Location = { X = 2; Y = -1 } }; 
            { Name = "Front Lawn"; Location = { X = 2; Y = 1 } } ]

        map

    [<TestFixture>]
    type Test() = 

        [<Test>]
        member x.Valid_Location_Is_Valid() =
            let map = CreateTestMap
            let location = { X = 2; Y = 0 }
            Assert.IsTrue(IsValidLocation map location)

        [<Test>]
        member x.Invalid_Location_Is_Not_Valid() =
            let map = CreateTestMap
            let location = { X = 2; Y = 2 }
            Assert.IsFalse(IsValidLocation map location)
