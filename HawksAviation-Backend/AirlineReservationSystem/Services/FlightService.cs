#region Using Namespaces
using AirlineReservationSystem.Models;
using AirlineReservationSystem.Repository;
#endregion

namespace AirlineReservationSystem.Services
{
    #region Flight Service
    public class FlightService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public List<Flights> SearchFlight(string start, string dest, DateTime arrival)
        {
            return _flightRepository.SearchFlights(start, dest, arrival);
        }

        public int AddFlight(Flights flight)
        {
            return _flightRepository.AddNewFlight(flight);
        }

        public List<Flights> GetAllFlights()
        {
            return _flightRepository.GetAllFlights();
        }

        public Flights GetFlightById(int id)
        {
            return _flightRepository.GetFlightById(id);
        }

        public int EditFlights(Flights flight)
        {
            return _flightRepository.UpdateFlight(flight);
        }

        public int DelFlight(int flightno)
        {
            return _flightRepository.DeleteFlight(flightno);
        }
    }
    #endregion
}
