#region Using Namespaces
using AirlineReservationSystem.Models;
using AirlineReservationSystem.Repository;
#endregion

namespace AirlineReservationSystem.Services
{
    #region Customer Service
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public int AddNewCustomer(Customers customer)
        {
            return _customerRepository.AddNewCustomer(customer);
        }
        public int UpdateCustomer(Customers customer)
        {
            return _customerRepository.UpdateCustomer(customer);
        }
        public int ChangePassword(Login creds, string newPassword)
        {
            return _customerRepository.ChangePasswordCustomer(creds, newPassword);
        }
        public Customers GetCustomerbyId(int Id)
        {
            return _customerRepository.GetCustomerById(Id);
        }
        public int DeleteCustomerbyId(int Id)
        {
            return _customerRepository.DeleteCustomerbyId(Id);
        }
        public List<Customers> GetAllCustomers()
        {
            return _customerRepository.GetAllCustomers();
        }
    }
    #endregion
}
