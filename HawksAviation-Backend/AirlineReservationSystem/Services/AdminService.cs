#region Using Namespaces
using AirlineReservationSystem.Models;
using AirlineReservationSystem.Repository;
#endregion

namespace AirlineReservationSystem.Services
{
    #region Admin Service
    public class AdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public int AddNewAdmin(Admin admin)
        {
            return _adminRepository.AddNewAdmin(admin);
        }
        public int EditAdmin(Admin admin)
        {
            return _adminRepository.UpdateAdmin(admin);
        }

        public int ChangePassword(Login creds, string newPassword)
        {
            return _adminRepository.ChangePasswordAdmin(creds, newPassword);
        }

        public Admin GetAdminById(int Id)
        {
            return _adminRepository.GetAdminById(Id);
        }
        public int UpdateAdmin(Admin admin)
        {
            return _adminRepository.UpdateAdmin(admin);
        }
    }
    #endregion
}