module Beats

open System

let beats() =
    //  Biel Mean Time is UTC+1 and does not observe DST
    let bmt = DateTime.UtcNow.AddHours(1.0)
    // Convert customary seconds to decimal minutes
    let decimalMinute = bmt.TimeOfDay.TotalSeconds * 0.011_574 |> int
    
    // zero pad the three digit representation
    sprintf "@%03i.beats" decimalMinute
