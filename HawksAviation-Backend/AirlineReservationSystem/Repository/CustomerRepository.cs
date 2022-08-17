#region Using Namespaces
using AirlineReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
#endregion

namespace AirlineReservationSystem.Repository
{
    #region Customer Repository
    public class CustomerRepository : ICustomerRepository
    {

        private readonly HawksAvaitionDBContext _dbContext;
        private readonly IMailService _mailService;
        private readonly IExceptionRepository _exceptionServices;

        public CustomerRepository(HawksAvaitionDBContext dbContext, IExceptionRepository exceptionServices,IMailService mailService)
        {
            _dbContext = dbContext;
            _mailService = mailService;
            _exceptionServices = exceptionServices;
        }

        #region AddNewCustomer
        /// <summary>
        /// when this function is invoked we can add new customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public int AddNewCustomer(Customers customer)
        {
            int response = StatusCodes.Status501NotImplemented;
            Users user = new Users();
            MailRequest mail = new MailRequest();
            try
            {
                Customers customer1 = _dbContext.Customers
                                        .AsNoTracking()
                .FirstOrDefault(c => c.EmailId == customer.EmailId);
                if (customer1 != null)
                {
                    response = 750;
                    return response;
                }

                Customers customer2 = _dbContext.Customers
                                        .AsNoTracking()
                .FirstOrDefault(c => c.Username == customer.Username);
                if (customer2 != null)
                {
                    response = 700;
                    return response;
                }

                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();
                user.Username = customer.Username;
                user.Password = customer.Password;
                user.FirstName = customer.FirstName;
                user.LastName = customer.LastName;
                user.EmailId = customer.EmailId;
                user.UserId = customer.CustomerId;
                user.Role = "Customer";
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                response = StatusCodes.Status200OK;
                mail.To = customer.EmailId;
                mail.Subject = "Successful Registration, Hawk Aviations";
                string body = "Hello " + customer.FirstName + ", \n" + 
                    "You have successfully registered a new account in the Hawk Aviations." + 
                    "Please login into your account and book a flight to one of your travel destinations." +
                    "\n \n Thanks and regards, \n"+
                    "Hawks Aviations.";
                mail.Body = body;
                _mailService.SendEmail(mail);
            }
            catch (Exception ex)
            {
                //_dbContext.Dispose();
                //_dbContext.Database.CloseConnection();
                //_exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {

            }
            return response;
        }
        #endregion

        #region GetAllCustomers
        /// <summary>
        /// when this function is invoked we get the list of all customers
        /// </summary>
        /// <returns></returns>
        public List<Customers> GetAllCustomers()
        {
            List<Customers> customer = null;
            try
            {
                customer = _dbContext.Customers.AsNoTracking()
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
            return customer;
        }
        #endregion

        #region Get Customer by Id
        /// <summary>
        /// when this function is invoked we can get customer by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Customers GetCustomerById(int Id)
        {
            Customers customer = null;

            try
            {
                customer = _dbContext.Customers
                                        .AsNoTracking()
                                         .FirstOrDefault(c => c.CustomerId == Id);
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {

            }
            return customer;
        }
        #endregion

        #region Update Customer
        /// <summary>
        /// when this function is invoked we can update the customer details
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public int UpdateCustomer(Customers customer)
        {
            int response = StatusCodes.Status501NotImplemented;
            MailRequest mail = new MailRequest();
            try
            {
                Customers customer1 = _dbContext.Customers
                    .AsNoTracking()
                     .FirstOrDefault(c => c.CustomerId == customer.CustomerId);

                if (customer1 != null)
                {
                    _dbContext.Customers.Update(customer);

                    _dbContext.SaveChanges();
                    response = StatusCodes.Status200OK;
                    mail.To = customer.EmailId;
                    mail.Subject = "Successfully Updated Profile, Hawk Aviations";
                    string body = "Hello " + customer.FirstName + ", \n" +
                        "You have successfully updated your account in the Hawk Aviations." +
                        "Please login into the account and book a flight to one of our many travel destinations." +
                        "\n \n Thanks and regards, \n" +
                        "Hawks Aviations.";
                    mail.Body = body;
                    _mailService.SendEmail(mail);
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

        #region Delete Customer by id
        /// <summary>
        /// when this function is invoked we can delete customer by id
        /// </summary>
        /// <param name="Id"></param>
        public int DeleteCustomerbyId(int Id)
        {
            int response = StatusCodes.Status400BadRequest;

            Customers customer1;
            try
            {
                customer1 = _dbContext.Customers
                    .AsNoTracking()
                    .FirstOrDefault(c => c.CustomerId == Id);


                if (customer1 != null)
                {
                    _dbContext.Customers.Remove(customer1);

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

        #region ChangePasswordCustomer
        /// <summary>
        /// When this function is invoked we can change password
        /// </summary>
        /// <param name="creds"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public int ChangePasswordCustomer(Login creds, string newPassword)
        {
            int response = StatusCodes.Status501NotImplemented;
            MailRequest mail = new MailRequest();
            try
            {
                Customers customer = _dbContext.Customers
                                .AsNoTracking()
                    .FirstOrDefault(u =>
            (u.Username == creds.Username) && (u.Password == creds.Password));
                customer.Username = creds.Username;
                customer.Password = newPassword;

                Users user = _dbContext.Users
                                .AsNoTracking()
                                .FirstOrDefault(u =>
            (u.Username == creds.Username) && (u.Password == creds.Password));
                user.Username = creds.Username;
                user.Password = newPassword;

                if (customer != null)
                {
                    _dbContext.Customers.Update(customer);
                    _dbContext.SaveChanges();
                    _dbContext.Users.Update(user);
                    _dbContext.SaveChanges();
                    response = StatusCodes.Status200OK;
                    mail.To = customer.EmailId;
                    mail.Subject = "Successfully Changed your Password, Hawk Aviations";
                    string body = "Hello " + customer.FirstName + ", \n" +
                        "You have successfully changed your account password in the Hawk Aviations. If it is not you please reset your password. " +
                        "Please login into the account and book a flight to one of our many travel destinations." +
                        "\n \n Thanks and regards, \n" +
                        "Hawks Aviations.";
                    mail.Body = body;
                    _mailService.SendEmail(mail);
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
    }
#endregion
}
