#region Using Namespaces
using AirlineReservationSystem.Models;
using Microsoft.EntityFrameworkCore;
#endregion

namespace AirlineReservationSystem.Repository
{
    #region Admin Repository
    public class AdminRepository : IAdminRepository
    {
        private readonly HawksAvaitionDBContext _dbContext;
        private readonly IExceptionRepository _exceptionServices;
        private readonly IMailService _mailService;
        public AdminRepository(HawksAvaitionDBContext dbContext, IExceptionRepository exceptionServices, IMailService mailService)
        {

            _dbContext = dbContext;
            _exceptionServices = exceptionServices;
            _mailService = mailService;
        }
        #region AddNewAdmin
        /// <summary>
        /// When this function is invoked we can add new Admin
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public int AddNewAdmin(Admin admin)
        {

            int reponse = StatusCodes.Status501NotImplemented;
            Users user = new Users();
            MailRequest mail = new MailRequest();
            try
            {
                Admin admin1 = _dbContext.Admin
                    .AsNoTracking()
                .FirstOrDefault(c => c.EmailId == admin.EmailId);
                if(admin1 != null)
                {
                    reponse = 750;
                    return reponse;
                }

                Admin admin2 = _dbContext.Admin
                    .AsNoTracking()
                .FirstOrDefault(c => c.Username == admin.Username);
                if (admin2 != null)
                {
                    reponse = 700;
                    return reponse;
                }

                //_dbContext.Database.BeginTransaction();
                _dbContext.Admin.Add(admin);
                _dbContext.SaveChanges();
                //_dbContext.Database.RollbackTransaction();
                user.Username = admin.Username;
                user.Password = admin.Password;
                user.FirstName = admin.FirstName;
                user.LastName = admin.LastName;
                user.EmailId = admin.EmailId;
                user.Role = admin.Role;
                user.UserId = admin.AdminId;
                //_dbContext.Database.BeginTransaction();
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                //_dbContext.Database.RollbackTransaction();
                reponse = StatusCodes.Status200OK;
                mail.To = admin.EmailId;
                mail.Subject = "Successful Employee Registration, Hawk Aviations";
                string body = "Hello " + admin.FirstName + ",\n \n" +
                    "You have been selected to work as " + admin.Role + " in the Hawk Aviations." +
                    "Please login into your account with your credentials and we are really looking forward to working with you." +
                    "\n \n Thanks and regards, \n" +
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
            return reponse;
        }
        #endregion

        #region GetAdminById
        /// <summary>
        /// When this function is invoked we can get Admin by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        public Admin GetAdminById(int Id)
        {


            Admin admin = null;

            try
            {
                admin = _dbContext.Admin
                    .AsNoTracking()
                        .FirstOrDefault(c => c.AdminId == Id);
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {

            }
            return admin;
        }
        #endregion

        #region UpdateAdmin
        /// <summary>
        /// When this function is invoked we can update the Admin details
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>

        public int UpdateAdmin(Admin admin)
        {

            int response = StatusCodes.Status501NotImplemented;

            try
            {
                Admin admin1 = _dbContext.Admin
                   .AsNoTracking() 
                .FirstOrDefault(c => c.AdminId == admin.AdminId);
                if (admin1 != null)
                {
                    _dbContext.Admin.Update(admin);
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

        #region ChangePasswordAdmin
        /// <summary>
        /// When this function is invoked we can change the password
        /// </summary>
        /// <param name="creds"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public int ChangePasswordAdmin(Login creds, string newPassword)
        {
            int response = StatusCodes.Status501NotImplemented;

            try
            {
                Admin admin1 = _dbContext.Admin
                                .AsNoTracking()
                    .FirstOrDefault(u =>
            (u.Username == creds.Username) && (u.Password == creds.Password));
                admin1.Username = creds.Username;
                admin1.Password = newPassword;

                Users user = _dbContext.Users
                                .AsNoTracking()
                                .FirstOrDefault(u =>
            (u.Username == creds.Username) && (u.Password == creds.Password));
                user.Username = creds.Username;
                user.Password = newPassword;
                
                if (admin1 != null)
                {
                    _dbContext.Admin.Update(admin1);
                    _dbContext.SaveChanges();
                    _dbContext.Users.Update(user);
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
    }
    #endregion
}

