module Beats

open System

let beats() =
    let utc = (DateTime.UtcNow.AddHours(1.0).TimeOfDay.TotalSeconds |> float) * 0.011_574 |> int

    let prefix =
        match utc with
        | i when i < 10 -> "00"
        | i when i < 100 -> "0"
        | _ -> ""

    sprintf "@%s%i.beats" prefix utc
