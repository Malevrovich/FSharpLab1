module Tests

open System
open Xunit

[<Fact>]
let ``My test`` () = Assert.True(true)

[<Fact>]
let ``Check Problem 9`` () =
    Problem9.solutions
    |> List.iter (fun solution ->
        let res: int = solution ()
        Assert.Equal(res, 31875000))

[<Fact>]
let ``Check Problem 22`` () =
    Problem22.solutions
    |> List.iter (fun solution ->
        let sortedEnumeratedNames = (Problem22.getSortedEnumeratedNames Problem22.namesPath)
        let res: int = solution sortedEnumeratedNames
        Assert.Equal(res, 871198282))
