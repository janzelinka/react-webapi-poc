using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ing_app_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public LoginController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            // Validate username and password against Dynamics 365 or your authentication system
            // Assuming successful authentication
            var userId = "123"; // Replace with user ID obtained from Dynamics 365 or your system
            var userName = "janko";
            var roleId = 1;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("tD6GmsZZjCj6bvcbPDLM4gs5UhQcDOuOtD6GmsZZjCj6bvcbPDLM4gs5UhQcDOuO"); // Replace with your secret key from config
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { 
                    new Claim("id", userId)
                    //, 
                    // new Claim("userName", userName),  
                    // new Claim("roleId", roleId)
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        [HttpGet]
        [Authorize]
        public IActionResult Auth() {
            return Ok();
        }

  
    }
}