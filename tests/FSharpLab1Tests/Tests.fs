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
