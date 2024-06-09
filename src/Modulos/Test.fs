module Tests

open Xunit

[<Fact>]
let ``My test should pass`` () =
    let result = 1 + 1
    Assert.Equal(2, result)
