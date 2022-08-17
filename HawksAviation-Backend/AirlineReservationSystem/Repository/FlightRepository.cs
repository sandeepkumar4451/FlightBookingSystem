#region Using Namespaces
using AirlineReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
#endregion


namespace AirlineReservationSystem.Repository
{
    #region Flight Repository
    public class FlightRepository : IFlightRepository
    {
        private readonly HawksAvaitionDBContext _dbContext;
        private readonly IExceptionRepository _exceptionServices;

        public FlightRepository(HawksAvaitionDBContext dbContext, IExceptionRepository exceptionServices)
        {
            _dbContext = dbContext;
            _exceptionServices = exceptionServices;
            _dbContext.Database.ExecuteSqlRaw("[dbo].[DeleteOldFlights]");
            _dbContext.Database.ExecuteSqlRaw("[dbo].[Fullybooked]");
            _dbContext.Database.ExecuteSqlRaw("[dbo].[FullyNotbooked]");
        }

        #region SearchFlights
        /// <summary>
        /// When this function is invoked we can search flights
        /// </summary>
        /// <param name="start"></param>
        /// <param name="dest"></param>
        /// <param name="arrival"></param>
        /// <returns></returns>
        public List<Flights> SearchFlights(string start, string dest, DateTime arrival)
        {

            List<Flights> flights;

            try
            {
                flights = _dbContext.Flights
                                .AsNoTracking()
                                .Where(f =>
                                f.Arrival.Date == arrival.Date
                                &&
                                f.Start == start
                                &&
                                f.Destination == dest
                                )
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
            return flights;
        }
        #endregion

        #region AddNewFlight
        /// <summary>
        /// When this function is invoked we can add new flights
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public int AddNewFlight(Flights flight)
        {
            int response = StatusCodes.Status501NotImplemented;
            try
            {
                flight.IsActive = true;
                _dbContext.Flights.Add(flight);
                _dbContext.SaveChanges();
                response = StatusCodes.Status200OK;

            }
            catch (Exception ex)
            {
                //_exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {
             
            }
            return response;
        }
        #endregion

        #region GetAllFlights
        /// <summary>
        /// When this function is invoked we can get all the flight details
        /// </summary>
        /// <returns></returns>

        public List<Flights> GetAllFlights()
        {
            List<Flights> flight = null;
            try
            {
                flight = _dbContext.Flights.AsNoTracking()
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
            return flight;
        }
        #endregion

        #region GetFlightById
        /// <summary>
        /// When this function is invoked we can get flight by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Flights GetFlightById(int id)
        {
            Flights flight = null;
            try
            {
                flight = _dbContext.Flights.FirstOrDefault(c => c.FlightNo == id);
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {
             
            }
            return flight;
        }
        #endregion

        #region UpdateFlight
        /// <summary>
        /// When this function is invoked we can update the flight details
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public int UpdateFlight(Flights flight)
        {
            int response = StatusCodes.Status501NotImplemented;
            try
            {
                Flights fli = _dbContext.Flights
                    .AsNoTracking()
                    .FirstOrDefault(c => c.FlightNo == flight.FlightNo);
                if (fli != null)
                {
                    _dbContext.Flights.Update(flight);
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

        #region DeleteFlight
        /// <summary>
        /// When this function is invoked we can delete the flight details
        /// </summary>
        /// <param name="flightno"></param>
        /// <returns></returns>
        public int DeleteFlight(int flightno)
        {
            int response = StatusCodes.Status400BadRequest;
            Flights fli;
            try
            {
                fli = _dbContext.Flights.FirstOrDefault(c => c.FlightNo == flightno);
                if (fli == null)
                {
                    response = StatusCodes.Status404NotFound;
                }
                else
                {
                    fli.Cancelled = true;
                    _dbContext.Flights.Update(fli);
                    _dbContext.SaveChanges();
                    response = StatusCodes.Status200OK;
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
    }
#endregion
}
