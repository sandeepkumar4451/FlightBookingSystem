namespace AirlineReservationSystem.DTOs
{
    #region BookingCreateDTO
    public class BookingCreateDTO
    {
        int _flightNo;
        public int FlightNo
        {
            get { return _flightNo; }
            set { _flightNo = value; }
        }
        int _customerId;
        public int CustomerID
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        int _seats;
        public int Seats
        {
            get { return _seats; }
            set { _seats = value; }
        }
    }
    #endregion
}
