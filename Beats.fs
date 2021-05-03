module Beats

open System

type Beats =
    { beats: string
      centiBeats: string
      beatsInt: int
      beatsDecimal: float
      bielMeanTime: DateTime }

// Convert customary seconds to decimal minutes
let convertUTCBeats (bmt: DateTime) = bmt.TimeOfDay.TotalSeconds * 0.011_574

let getBmt () = DateTime.UtcNow.AddHours(1.0)

let beatsJson () =
    let bmt = getBmt ()
    let fltBeats = bmt |> convertUTCBeats
    let intBeats = fltBeats |> int

    { beats = intBeats |> sprintf "@%03i.beats"
      centiBeats = fltBeats |> sprintf "@%06.2f.beats"
      beatsInt = intBeats
      beatsDecimal = Math.Round(fltBeats, 2)
      bielMeanTime = bmt }

let beatsText () =
    getBmt ()
    |> convertUTCBeats
    |> int
    |> sprintf "@%03i.beats"
