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

    #region Admins Controller
    public class AdminsController : ControllerBase
    {
        private readonly AdminService _adminService;
      

        public AdminsController(AdminService adminService)
        {
            _adminService = adminService;
            
        }

        #region Http GET
        /// <summary>
        /// Handles Http GET request with string query
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public IActionResult GetAdmin(int id)
        {
            if (_adminService == null)
            {
                return NotFound();
            }
            var admin = _adminService.GetAdminById(id);

            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }
        #endregion

        #region Http POST
        /// <summary>
        /// Handles Http POST request
        /// </summary>
        /// <param name="adminDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddNewAdmin(AdminCreateDTO adminDto)
        {
            Admin admin = new Admin();
            admin.AdminId = 0;
            admin.FirstName = adminDto.FirstName;
            admin.LastName = adminDto.LastName;
            admin.EmailId = adminDto.EmailId;
            admin.Username = adminDto.Username;
            admin.Password = adminDto.Password;
            admin.Role = adminDto.Role;

            if (_adminService == null)
            {
                return Problem("Entity set 'HawksAvaitionDBContext.AdminContext'  is null.");
            }
            int val = _adminService.AddNewAdmin(admin);
            
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
            return CreatedAtAction("GetAdmin", new { id = admin.AdminId }, admin);
        }
        #endregion

        #region Http PUT
        /// <summary>
        /// Handles Http PUT with string query
        /// </summary>
        /// <param name="id"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateAdmin(int id, Admin admin)
        {
            if (id != admin.AdminId)
            {
                return BadRequest();
            }

            if (_adminService == null)
            {
                return Problem("Entity set 'HawksAvaitionDBContext.AdminContext'  is null.");
            }

            int val = _adminService.EditAdmin(admin);
            if (val != 200)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetAdmin", new { id = admin.AdminId }, admin);
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

            if (_adminService == null)
            {
                return Problem("Entity set 'HawksAvaitionDBContext.AdminContext'  is null.");
            }
            Login creds = new Login();
            creds.Username = chamodel.Username;
            creds.Password = chamodel.OldPassword;

            String newPwd = chamodel.NewPassword;
            int val = _adminService.ChangePassword(creds, newPwd);
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
