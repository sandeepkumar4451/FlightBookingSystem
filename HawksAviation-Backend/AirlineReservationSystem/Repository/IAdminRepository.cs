#region Using Namespaces
using AirlineReservationSystem.Models;
#endregion

namespace AirlineReservationSystem.Repository
{
    #region Admin Interface
    public interface IAdminRepository
    {
        int AddNewAdmin(Admin admin);
        Admin GetAdminById(int Id);
        int UpdateAdmin(Admin admin);
        int ChangePasswordAdmin(Login creds, string newPassword);
    }
    #endregion
}