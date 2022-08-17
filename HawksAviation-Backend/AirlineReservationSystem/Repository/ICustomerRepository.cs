#region Using Namespaces
using AirlineReservationSystem.Models;
#endregion

namespace AirlineReservationSystem.Repository
{
    #region Customer Interface
    public interface ICustomerRepository
    {
        int AddNewCustomer(Customers customer);
        List<Customers> GetAllCustomers();
        Customers GetCustomerById(int Id);
        int DeleteCustomerbyId(int Id);
        int UpdateCustomer(Customers customer);
        int ChangePasswordCustomer(Login creds, string newPassword);
    }
    #endregion
}