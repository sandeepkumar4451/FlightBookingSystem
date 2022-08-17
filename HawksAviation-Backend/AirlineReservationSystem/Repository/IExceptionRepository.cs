namespace AirlineReservationSystem.Repository
{
    #region Exception Interface
    public interface IExceptionRepository
    {
        public Task CreateLog(Exception ex, object requestBody);

    }
    #endregion
}