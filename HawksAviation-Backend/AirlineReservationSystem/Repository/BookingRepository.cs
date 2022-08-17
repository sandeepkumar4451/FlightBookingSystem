#region Using Namespaces
using AirlineReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
#endregion


namespace AirlineReservationSystem.Repository
{
    #region Booking Repository
    public class BookingRepository : IBookingRepository
    {
        private readonly HawksAvaitionDBContext _dbContext;
        private readonly IMailService _mailService;
        private readonly IExceptionRepository _exceptionServices;

        public BookingRepository(HawksAvaitionDBContext dbContext, IExceptionRepository exceptionServices, IMailService mailService)
        {
            _dbContext = dbContext;
            _mailService = mailService; 
            _exceptionServices = exceptionServices;
            _dbContext.Database.ExecuteSqlRaw("[dbo].[DeleteOldFlights]");      
            _dbContext.Database.ExecuteSqlRaw("[dbo].[Fullybooked]");
            _dbContext.Database.ExecuteSqlRaw("[dbo].[FullyNotbooked]");
            _dbContext.Database.ExecuteSqlRaw("[dbo].[DeleteOldBookings]");
        }

        #region CancelBookingById
        /// <summary>
        /// When this function is invoked we can cancel booking by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int CancelBookingbyId(int Id)
        {
           
            int response = StatusCodes.Status400BadRequest;
            MailRequest mail = new MailRequest();
            Bookings booking;
            try
            {
                booking = _dbContext.Bookings
                      .FirstOrDefault(c => c.BookingID == Id);

                if (booking == null)
                {
                    response = StatusCodes.Status404NotFound;
                }
                var flight = _dbContext.Flights
                        .FirstOrDefault(c => c.FlightNo == booking.FlightNo);
                var customer = _dbContext.Customers
                                        .AsNoTracking()
                                         .FirstOrDefault(c => c.CustomerId == booking.CustomerID);
                booking.Status = "Cancelled and Refunded";
                booking.IsCancelled = true;
                flight.AvailableSeats = flight.AvailableSeats + booking.Seats;
                _dbContext.Bookings.Update(booking);
                _dbContext.Flights.Update(flight);
                _dbContext.Database.ExecuteSqlRaw("[dbo].[Fullybooked]");
                _dbContext.SaveChanges();
                response = StatusCodes.Status200OK;
                mail.To = customer.EmailId;
                mail.Subject = "Booking Cancellation Successful, Hawk Aviations";
                string body = "Hai " + customer.FirstName + "\n" +
                    "You have successfully cancelled your reservation tickets on the flight" + flight.Name + "(" + flight.FlightId + ") " + " flightno" + flight.FlightNo +
                    ". Your bookingId is " + booking.BookingID +
                    ". Your flight will start in " + flight.Start + " airport and arrivies at " + flight.Arrival +
                    " and flight departures at " + flight.Departure + " to the " + flight.Destination + " airport." +
                    " You have booked " + booking.Seats + " seats and the booking amount is Rs." + booking.BookingAmount +
                    ". You will be refunded in 3 to 5 business days and refunded in the form of our hawk credits which can be used in all our partner airports. We are really sad that you are not travelling with us." +
                    "\n \n Thanks and regards, \n" +
                    "Hawks Aviations.";
                mail.Body = body;
                _mailService.SendEmail(mail);
                _dbContext.Database.ExecuteSqlRaw("[dbo].[FullyNotbooked]");
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {
                
            }
            return response;
        }
        #endregion

        #region CreateNewBooking
        /// <summary>
        /// When this function is invoked we can create new booking
        /// </summary>
        /// <param name="bookings"></param>
        /// <returns>int</returns>
        public int CreateNewBooking(Bookings bookings)
        {
            
            int reponse = StatusCodes.Status501NotImplemented;
            MailRequest mail = new MailRequest();
            try
            {
                var flight = _dbContext.Flights
                        .FirstOrDefault(c => c.FlightNo == bookings.FlightNo);
                var customer = _dbContext.Customers
                                            .AsNoTracking()
                                             .FirstOrDefault(c => c.CustomerId == bookings.CustomerID);
                if (bookings.Seats > flight.AvailableSeats)
                {
                    reponse = StatusCodes.Status403Forbidden;
                    return reponse;
                }
                bookings.BookingAmount = bookings.Seats * flight.Fare;
                bookings.Arrival = flight.Arrival;
                bookings.Departure = flight.Departure;
                bookings.BookingID = 0;
                flight.AvailableSeats = flight.AvailableSeats - bookings.Seats;
                bookings.Status = "In Progress";
                _dbContext.Bookings.Add(bookings);
                _dbContext.Flights.Update(flight);
                _dbContext.SaveChanges();
                bookings.Status = "Booked";
                _dbContext.Bookings.Update(bookings);
                _dbContext.SaveChanges();
                reponse = StatusCodes.Status201Created;
                mail.To = customer.EmailId;
                mail.Subject = "Booking Successful, Hawk Aviations";
                string body = "Hai " + customer.FirstName +
                    "You have successfully reserved tickets on the flight" + flight.Name +"(" + flight.FlightId + ") " + " flightno" + flight.FlightNo+ 
                    ". Your bookingId is " + bookings.BookingID + 
                    ". Your flight will start in "+ flight.Start + " airport and arrivies at "+flight.Arrival+
                    " and flight departures at " + flight.Departure+ " to the "+flight.Destination+ " airport."+
                    " You have booked "+ bookings.Seats +" seats and the booking amount is Rs."+bookings.BookingAmount+
                    ". Have a safe and pleasant journey, Bon Voyage."+
                    " Thanks and regards, " +
                    "Hawks Aviations.";
                mail.Body = body;
                _mailService.SendEmail(mail);
                _dbContext.Database.ExecuteSqlRaw("[dbo].[Fullybooked]");
            }
            catch (Exception ex)
            {
                //_exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {
                
            }
            return reponse;
        }
        #endregion

        #region GetAllBookings
        /// <summary>
        /// When this function is invoked we can get all bookings
        /// </summary>
        /// <returns></returns>
        public List<Bookings> GetAllBookings()
        {

            List<Bookings> bookings = null ;

            try
            {
                bookings = _dbContext.Bookings.AsNoTracking()
                                            .OrderByDescending(x => x.Arrival)
                                              .ToList();
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {
                
            }
            return bookings;
        }
        #endregion

        #region GetAllCustBookings
        /// <summary>
        /// When this function is invoked we can get all bookings
        /// </summary>
        /// <returns></returns>
        public List<Bookings> GetAllCustBookings(int custId)
        {

            List<Bookings> bookings = null;

            try
            {
                bookings = _dbContext.Bookings.AsNoTracking()
                        .Where(c => c.CustomerID == custId)
                         .OrderByDescending(x => x.Arrival)
                                              .ToList();
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {

            }
            return bookings;
        }
        #endregion

        #region GetBookingById
        /// <summary>
        /// When this function is invoked we can get booking by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Bookings GetBookingById(int Id)
        {
            

            Bookings booking = null;

            try
            {
                booking = _dbContext.Bookings
                                    .FirstOrDefault(c => c.BookingID == Id);
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {
                
            }
            return booking;
        }
        #endregion

        #region UpdateBooking
        /// <summary>
        /// When this function is invoked we can update the booking details
        /// </summary>
        /// <param name="bookings"></param>
        /// <returns></returns>
        public int UpdateBooking(Bookings bookings)
        {


            int response = StatusCodes.Status501NotImplemented;

            try
            {
                Bookings bookings1 = _dbContext.Bookings
                .AsNoTracking()
                .FirstOrDefault(c => c.BookingID == bookings.BookingID);
                
                if (bookings1 != null)
                {
                    _dbContext.Bookings.Update(bookings);
                    _dbContext.SaveChanges();
                    response = StatusCodes.Status200OK;
                }
                else
                {
                    response = StatusCodes.Status404NotFound;
                }
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {
               
            }

            return response;
        }
        #endregion

        #region CheckInBooking
        /// <summary>
        /// When this function is invoked we can check in 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int CheckInBooking(int Id)
        {
            int response = StatusCodes.Status400BadRequest;
            MailRequest mail = new MailRequest();
            Bookings booking;
            try
            {
                
                booking = _dbContext.Bookings
                      .FirstOrDefault(c => c.BookingID == Id);
                var flight = _dbContext.Flights
                        .FirstOrDefault(c => c.FlightNo == booking.FlightNo);
                var customer = _dbContext.Customers
                                            .AsNoTracking()
                                             .FirstOrDefault(c => c.CustomerId == booking.CustomerID);

                if (booking == null)
                {
                    response = StatusCodes.Status404NotFound;
                }
                booking.Status = "CheckedIn";
                booking.IsCheckedIn = true;
                _dbContext.Bookings.Update(booking);
                _dbContext.SaveChanges();
                response = StatusCodes.Status200OK;
                mail.To = customer.EmailId;
                mail.Subject = "CheckedIn Successfully, Hawk Aviations";
                string body = "Hai " + customer.FirstName + "\n" +
                    "You have successfully checkedIn into your reservation on the flight" + flight.Name + "(" + flight.FlightId + ") " + " flightno" + flight.FlightNo +
                    ". Your bookingId is " + booking.BookingID +
                    ". Your flight will start in " + flight.Start + " airport and arrivies at " + flight.Arrival +
                    " and flight departures at " + flight.Departure + " to the " + flight.Destination + " airport." +
                    " You have booked " + booking.Seats + " seats and the booking amount is Rs." + booking.BookingAmount +
                    ". Have a safe and pleasant journey, Bon Voyage." +
                    "\n \n Thanks and regards, \n" +
                    "Hawks Aviations.";
                mail.Body = body;
                _mailService.SendEmail(mail);
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {

            }
            return response;
        }
        #endregion
    }
#endregion
}
