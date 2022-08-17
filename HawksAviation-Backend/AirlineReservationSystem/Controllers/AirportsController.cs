#region Using Namespaces
using AirlineReservationSystem.Models;
using AirlineReservationSystem.Services;
using Microsoft.AspNetCore.Mvc;
#endregion


namespace AirlineReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    #region Airports Controller
    public class AirportsController : ControllerBase
    {
        private readonly AirportService _airportService;

        public AirportsController(AirportService airportService)
        {
            _airportService = airportService;
        }
        #region Http GET
        /// <summary>
        /// Handles Http GET request
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAirportsContext()
        {
            if (_airportService == null)
            {
                return NotFound();
            }
            return Ok(_airportService.GetAllAirports());
        }
        #endregion

        #region Http GET
        /// <summary>
        /// Handles Http GET with string query
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetAirports(string id)
        {
            if (_airportService == null)
            {
                return NotFound();
            }
            var airports = _airportService.GetAirportById(id);

            if (airports == null)
            {
                return NotFound();
            }
            return Ok(airports);
        }
        #endregion

        #region Http POST
        /// <summary>
        /// Handles Http POSt request
        /// </summary>
        /// <param name="airports"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddNewAirport(Airports airports)
        {
            if (_airportService == null)
            {
                return Problem("Entity set 'FlyWithFalconDBContext.AirportsContext'  is null.");
            }
            int val = _airportService.AddNewAirport(airports);
            if (val != 200)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetAirports", new { id = airports.AirportID }, airports);
        }
        #endregion

        #region Http PUT
        /// <summary>
        /// Handles Http PUT with string query
        /// </summary>
        /// <param name="id"></param>
        /// <param name="airports"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateAirports(string id, Airports airports)
        {
            if (id != airports.AirportID)
            {
                return BadRequest();
            }

            if (_airportService == null)
            {
                return Problem("Entity set 'FlyWithFalconDBContext.AirportsContext'  is null.");
            }

            int val = _airportService.UpdateAirports(airports);
            if (val != 200)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetAirports", new { id = airports.AirportID }, airports);
        }
        #endregion
    }
    #endregion
}
