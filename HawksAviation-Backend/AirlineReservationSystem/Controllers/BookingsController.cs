#region Using Namespaces
using Microsoft.AspNetCore.Mvc;
using AirlineReservationSystem.Models;
using AirlineReservationSystem.Services;
using AirlineReservationSystem.DTOs;
#endregion

namespace AirlineReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    #region Bookings Controller
    public class BookingsController : ControllerBase
    {
        private readonly BookingService _bookingService;


        public BookingsController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        #region Http GET
        /// <summary>
        /// Handles Http GET request
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllBookings()
        {
            if (_bookingService == null)
            {
                return NotFound();
            }
            return Ok(_bookingService.GetAllBookings());
        }
        #endregion

        #region Http GET
        /// <summary>
        /// Handles Http GET request with string query
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>

        [HttpGet("custbooking/{cid}")]
        public IActionResult GetAllCustomerBookings(int cid)
        {
            if (_bookingService == null)
            {
                return NotFound();
            }
            return Ok(_bookingService.GetAllCustomerBookings(cid));
        }
        #endregion

        #region Http GET
        /// <summary>
        /// Handles Http GET request with string query
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetBookings(int id)
        {
            if (_bookingService == null)
            {
                return NotFound();
            }
            var bookings = _bookingService.GetBookingById(id);

            if (bookings == null)
            {
                return NotFound();
            }
            return Ok(bookings);
        }
        #endregion

        #region Http POST
        /// <summary>
        /// Handles Http POST request 
        /// </summary>
        /// <param name="bookingDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddNewBooking(BookingCreateDTO bookingDto)
        {
            if (_bookingService == null)
            {
                return Problem("Entity set 'HawksAvaitionDBContext.BookingsContext'  is null.");
            }
            Bookings bookings = new Bookings();
            bookings.CustomerID = bookingDto.CustomerID;
            bookings.FlightNo = bookingDto.FlightNo;
            bookings.Seats = bookingDto.Seats;

            int val = _bookingService.AddNewBooking(bookings);
            if (val != 201)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetBookings", new { id = bookings.BookingID }, bookings);
        }
        #endregion

        #region HTtp PUT
        /// <summary>
        /// Handles Http PUT request with string query
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookings"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateBooking(int id, Bookings bookings)
        {
            if (id != bookings.BookingID)
            {
                return BadRequest();
            }

            if (_bookingService == null)
            {
                return Problem("Entity set 'HawksAvaitionDBContext.BookingsContext'  is null.");
            }

            int val = _bookingService.EditBooking(bookings);
            if (val != 200)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetBookings", new { id = bookings.BookingID }, bookings);
        }
        #endregion

        #region Http PUT
        /// <summary>
        /// Handles Http PUT request with string query
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPut("/api/Bookings/CheckIn/{id}")]

        public IActionResult CheckInBooking(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            if (_bookingService == null)
            {
                return Problem("Entity set 'HawksAvaitionDBContext.BookingsContext'  is null.");
            }

            int val = _bookingService.CheckInBooking(id);
            if (val != 200)
            {
                return BadRequest();
            }

            return Ok();
        }
        #endregion

        #region Http Delete
        /// <summary>
        /// Handles Http DELETE request with string query
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        public IActionResult CancelBookings(int id)
        {
            if (_bookingService == null)
            {
                return NotFound();
            }
            int val = _bookingService.CancelBooking(id);
            if (val != 200)
            {
                return BadRequest();
            }

            return Ok();
        }
        #endregion

    }
    #endregion
}
