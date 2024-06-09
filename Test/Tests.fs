module Tests

open Xunit
open Frogger.Modulos.Functions


[<Fact>]
let ``moveDown Two should return One`` () =
    let result = Functions.moveDown Rows.Two
    Assert.Equal(Rows.One, result)

[<Fact>]
let ``moveUp Two should return Three`` () =
    let result = Functions.moveUp Rows.Two
    Assert.Equal(Rows.Three, result)