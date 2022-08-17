#region Using Namespaces
using AirlineReservationSystem.Models;
#endregion

namespace AirlineReservationSystem.Repository
{
    #region Airport Interface
    public interface IAirportRepository
    {
        int AddNewAirport(Airports airports);
        List<Airports> GetAllAirports();
        Airports GetAirportById(string Id);
        int UpdateAirports(Airports airports);
    }
    #endregion
}