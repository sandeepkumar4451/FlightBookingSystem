#region Using Namespaces
using AirlineReservationSystem.Models;
#endregion

namespace AirlineReservationSystem.Repository
{
    #region Booking Interface
    public interface IBookingRepository
    {
        int CreateNewBooking(Bookings bookings);
        List<Bookings> GetAllBookings();
        List<Bookings> GetAllCustBookings(int custId);
        Bookings GetBookingById(int Id);
        int CancelBookingbyId(int Id);
        int UpdateBooking(Bookings bookings);
        int CheckInBooking(int Id);
    }
    #endregion
}
