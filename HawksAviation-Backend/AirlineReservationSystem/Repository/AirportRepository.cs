#region Using Namespaces
using AirlineReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
#endregion

namespace AirlineReservationSystem.Repository
{
    #region Airport Repository
    public class AirportRepository : IAirportRepository
    {
        private readonly HawksAvaitionDBContext _dbContext;
        private readonly IExceptionRepository _exceptionServices;

        public AirportRepository(HawksAvaitionDBContext dbContext, IExceptionRepository exceptionServices)
        {
            _dbContext = dbContext;
            _exceptionServices = exceptionServices;
        }

        #region AddNewAirport
        /// <summary>
        /// When this function is invoked we can add airports
        /// </summary>
        /// <param name="airports"></param>
        /// <returns></returns>
        public int AddNewAirport(Airports airports)
        {
            int Response = StatusCodes.Status501NotImplemented;

            try
            {
                _dbContext.Airports.Add(airports);

                _dbContext.SaveChanges();

                Response = StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                //_exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {

            }
            return Response;
        }
        #endregion

        #region GetAllAirports
        /// <summary>
        /// When this function is invoked we can get all airports list
        /// </summary>
        /// <returns></returns>
        public List<Airports> GetAllAirports()
        {
            List<Airports> airports = null;
            try
            {
                airports = _dbContext.Airports.AsNoTracking()
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
            return airports;
        }
        #endregion

        #region GetAirportById
        /// <summary>
        /// When this function is invoked we can get airport by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Airports GetAirportById(string Id)
        {
            Airports airports;
            try
            {
                airports = _dbContext.Airports
                                     .AsNoTracking()
                                     .FirstOrDefault(c => c.AirportID == Id);
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {

            }
            return airports;
        }
        #endregion

        #region UpdateAirports
        /// <summary>
        /// When this function is invoked we can update the airport details
        /// </summary>
        /// <param name="airports"></param>
        /// <returns></returns>
        public int UpdateAirports(Airports airports)
        {
            int response = StatusCodes.Status501NotImplemented;
            try
            {
                Airports ap = _dbContext.Airports
                    .AsNoTracking()
                    .FirstOrDefault(c => c.AirportID == airports.AirportID);

                if (ap != null)
                {
                    _dbContext.Airports.Update(airports);
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
                throw;
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
