
#region Using Namespaces
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace AirlineReservationSystem.Models
{
    #region Bookings class
    public class Bookings
    {
        #region Class properties

        
        int _bookingId;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BookingID
        {
            get { return _bookingId; }
            set { _bookingId = value; }
        }

        [Required]
        [ForeignKey("Flights")]
        int _flightNo;
        public int FlightNo
        {
            get { return _flightNo; }
            set { _flightNo = value; }
        }

        [Required]
        [ForeignKey("Customers")]
        int _customerId;
        public int CustomerID
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        [Required]
        int _seats;
        public int Seats
        {
            get { return _seats; }
            set { _seats = value; }
        }

        [Required]
        double _bookingamount;
        public double BookingAmount
        {
            get { return _bookingamount; }
            set { _bookingamount = value; }
        }

        DateTime _arrival = DateTime.Now;
        public DateTime Arrival
        {
            get { return _arrival; }
            set { _arrival = value; }
        }

        DateTime _departure = DateTime.Now;
        public DateTime Departure
        {
            get { return _departure; }
            set { _departure = value; }
        }

        
        string _status = "Inprogress";
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        Boolean _isCancelled;
        public Boolean IsCancelled
        {
            get { return _isCancelled; }
            set
            {
                _isCancelled = value;
            }
        }

        Boolean _isCheckedIn;
        public Boolean IsCheckedIn
        {
            get { return _isCheckedIn; }
            set
            {
                _isCheckedIn = value;
            }
        }

        Boolean _outdated;
        public Boolean Outdated
        {
            get { return _outdated; }
            set
            {
                _outdated = value;
            }
        }

        #endregion
    }
    #endregion
}
