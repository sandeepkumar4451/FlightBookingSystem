#region Using Namespaces
using AirlineReservationSystem.DTOs;
using AirlineReservationSystem.Models;
using AirlineReservationSystem.Services;
using Microsoft.AspNetCore.Mvc;
#endregion

namespace AirlineReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    #region Customers Controller
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        #region Http GET
        /// <summary>
        /// Handles Http GET request
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            if (_customerService == null)
            {
                return NotFound();
            }
            return Ok(_customerService.GetAllCustomers());
        }
        #endregion

        #region Http GET
        /// <summary>
        /// Handles Http GET with string query
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetCustomerbyId(int id)
        {
            if (_customerService == null)
            {
                return NotFound();
            }
            var customer = _customerService.GetCustomerbyId(id);

            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        #endregion

        #region Http POST
        /// <summary>
        /// Handles Http POST request
        /// </summary>
        /// <param name="customerDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddNewCustomer(CustomerCreateDTO customerDto)
        {
            Customers customer = new Customers();
            customer.CustomerId = 0;
            customer.FirstName = customerDto.FirstName;
            customer.LastName = customerDto.LastName;
            customer.MobileNumber = customerDto.MobileNumber;
            customer.Gender = customerDto.Gender;
            customer.Age = customerDto.Age;
            customer.EmailId = customerDto.EmailId;
            customer.Username = customerDto.Username;
            customer.Password = customerDto.Password;
            if (_customerService == null)
            {
                return Problem("Entity set 'CGAirwaysDbContext.CustomerContext'  is null.");
            }
            int val = _customerService.AddNewCustomer(customer);
            if (val == 700)
            {
                return BadRequest("Username already taken!!");
            }
            else if (val == 750)
            {
                return BadRequest("Email account already present, please login!!");
            }
            else if (val != 200)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetCustomerbyId", new { id = customer.CustomerId }, customer);
        }
        #endregion

        #region Http PUT
        /// <summary>
        /// Handles Http PUT request with string query
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customers customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            if (_customerService == null)
            {
                return Problem("Entity set 'CGAirwaysDbContext.CustomerContext'  is null.");
            }

            int val = _customerService.UpdateCustomer(customer);
            if (val != 200)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetCustomerbyId", new { id = customer.CustomerId }, customer);
        }
        #endregion

        #region Http DELETE
        /// <summary>
        /// Handles Http DELETE request with string query
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomerbyId(int id)
        {
            if (_customerService == null)
            {
                return NotFound();
            }
            int val = _customerService.DeleteCustomerbyId(id);
            if (val != 200)
            {
                return BadRequest();
            }

            return Ok();
        }
        #endregion

        #region Http PUT
        /// <summary>
        /// Handles Http PUT request 
        /// </summary>
        /// <param name="chamodel"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult ChangePassword(Changepassword chamodel)
        {

            if (chamodel == null)
            {
                return BadRequest();
            }

            if (_customerService == null)
            {
                return Problem("Entity set 'HawksAvaitionDBContext.Customer'  is null.");
            }

            Login creds = new Login();
            creds.Username = chamodel.Username;
            creds.Password = chamodel.OldPassword;

            String newPwd = chamodel.NewPassword;

            int val = _customerService.ChangePassword(creds, newPwd);
            if (val != 200)
            {
                return BadRequest();
            }
            return Ok();
        }
        #endregion
    }
    #endregion
}
