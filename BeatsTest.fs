module BeatsTest

open FsUnit.Xunit
open Xunit
open System.Text.RegularExpressions

open Beats

[<Fact>]
let ``test beats format``() = Regex.IsMatch(beats(), @"@\d{3}.beats") |> should equal true
