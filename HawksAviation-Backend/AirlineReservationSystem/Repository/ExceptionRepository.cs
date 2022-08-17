#region Using Namespaces
using AirlineReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
#endregion

namespace AirlineReservationSystem.Repository
{
    #region Exception Repository
    public class ExceptionRepository : IExceptionRepository
    {
        public IConfiguration _configuration { get; }
        private readonly HawksAvaitionDBContext _context;

        public ExceptionRepository(HawksAvaitionDBContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        #region CreateLog
        public async Task CreateLog(Exception ex, object requestBodyJson)
        {
            _context.Database.OpenConnection();
            //_context.Database.BeginTransaction();
            var exceptionLogObj = new ExceptionLog();
            exceptionLogObj.Data = JsonConvert.SerializeObject(requestBodyJson, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            exceptionLogObj.DataTime = DateTime.Now;
            exceptionLogObj.ErrorDescription = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            exceptionLogObj.StackTrace = ex.StackTrace;
            _context.ExceptionLog.Add(exceptionLogObj);
            await _context.SaveChangesAsync();
            //_context.Database.RollbackTransaction();
            _context.Database.CloseConnection();
        }
        #endregion
    }
    #endregion
}
