#region Using Namespaces
using AirlineReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
#endregion


namespace AirlineReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    #region Authentication Controller
    public class AuthController : ControllerBase
    {
        private readonly HawksAvaitionDBContext _dbContext;

        public AuthController(HawksAvaitionDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login creds)
        {
            if (creds is null)
            {
                return BadRequest("Invalid client request");
            }

            Users user = _dbContext.Users.FirstOrDefault(u =>
            (u.Username == creds.Username) && (u.Password == creds.Password));
            
                

            if (user != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.SerialNumber, (user.UserId).ToString()),
                    new Claim(ClaimTypes.Email, user.EmailId),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:7178",
                    audience: "http://localhost:5178",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(100),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new AuthenticatedResponse { Token = tokenString });
            }
            return Unauthorized();
        }
    }
    #endregion
}
