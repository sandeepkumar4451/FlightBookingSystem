#region Using Namespaces
using AirlineReservationSystem.Models;
#endregion

namespace AirlineReservationSystem.Repository
{
    #region MailService Interface
    public interface IMailService
    {
        void SendEmail(MailRequest mailRequest);
    }
    #endregion
}