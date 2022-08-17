#region Using Namespaces
using AirlineReservationSystem.Models;
using AirlineReservationSystem.Services;
using Microsoft.AspNetCore.Mvc;
#endregion

namespace AirlineReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    #region Flights Controller
    public class FlightsController : ControllerBase
    {
        private readonly FlightService _flightService;
        public FlightsController(FlightService flightService)
        {
            _flightService = flightService;
        }
        #region Http GET
        /// <summary>
        /// Handles Http GET request 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="dest"></param>
        /// <param name="doj"></param>
        /// <returns></returns>
        [HttpGet("{start}/{dest}/{doj}")]

        public IActionResult SearchFlights(string start, string dest, DateTime doj)
        {
            if (string.IsNullOrEmpty(dest) && string.IsNullOrEmpty(start))
            {
                return BadRequest(); 
            }
            return Ok(_flightService.SearchFlight(start, dest, doj));
        }
        #endregion

        #region Http GET
        /// <summary>
        /// Handles Http GET request 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetFlightsContext()
        {
            if (_flightService == null)
            {
                return NotFound();
            }
            return Ok(_flightService.GetAllFlights());
        }
        #endregion

        #region Http GET
        /// <summary>
        /// Handles Http GET request 
        /// </summary>
        /// <param name="flightno"></param>
        /// <returns></returns>
        [HttpGet("{flightno}")]
        public IActionResult GetFlight(int flightno)
        {
            if (_flightService == null)
            {
                return NotFound();
            }
            var fli = _flightService.GetFlightById(flightno);

            if (fli == null)
            {
                return NotFound();
            }
            return Ok(fli);
        }
        #endregion

        #region Http POST
        /// <summary>
        /// Handles Http POST request 
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddNewFlight(Flights flight)
        {
            if (_flightService == null)
            {
                return Problem("Entity set 'PeekAirwaysDBContext.FlightsContext'  is null.");
            }
            int val = _flightService.AddFlight(flight);
            if (val != 200)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetFlight", new { flightno = flight.FlightNo }, flight);
        }
        #endregion

        #region Http PUT
        /// <summary>
        /// Handles Http PUT request
        /// </summary>
        /// <param name="flightno"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        [HttpPut("{flightno}")]
        public IActionResult UpdateFlight(int flightno, Flights flight)
        {
            if (flightno != flight.FlightNo)
            {
                return BadRequest();
            }

            if (_flightService == null)
            {
                return Problem("Entity set 'PeekAirwaysDBContext.FlightsContext'  is null.");
            }

            int val = _flightService.EditFlights(flight);
            if (val != 200)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetFlight", new { flightno = flight.FlightNo }, flight);
        }
        #endregion

        #region Http DELETE
        /// <summary>
        /// Handles Http DELETE request
        /// </summary>
        /// <param name="flightno"></param>
        /// <returns></returns>
        [HttpDelete("{flightno}")]
        public IActionResult DeleteFlight(int flightno)
        {
            if (_flightService == null)
            {
                return NotFound();
            }
            int val = _flightService.DelFlight(flightno);
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
