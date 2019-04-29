// int -> (DateTimeOffset -> Reservation list) -> (Reservation -> int) -> Reservation
// -> int option
let tryAccept capacity readReservations createReservation reservation =
    let reservedSeats =
        readReservations reservation.Date |> List.sumBy (fun x -> x.Quantity)
    if reservedSeats + reservation.Quantity <= capacity
    then createReservation { reservation with IsAccepted = true } |> Some
    else None

    tryAccept :: Int -> (ZonedTime -> [Reservation]) -> (Reservation -> Int) -> Reservation
             -> Maybe Int
tryAccept capacity readReservations createReservation reservation =
  let reservedSeats = sum $ map quantity $ readReservations $ date reservation
  in  if reservedSeats + quantity reservation <= capacity
      then Just $ createReservation $ reservation { isAccepted = True }
      else Nothing