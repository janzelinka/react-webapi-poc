using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using app_api.ViewModels;
using app.Repositories;
using app.Models;
using api.ViewModels;
using api.Repositories;
using app.Services;

namespace ing_app_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUsersService usersService;

        public IAuthService AuthService { get; }

        public LoginController(IUsersService usersService, IAuthService authService)
        {
            this.usersService = usersService;
            AuthService = authService;
        }

        [HttpPost(Name = "Login")]
        public async Task<ActionResult> Login(string username, string password)
        {
            await AuthService.LoginAsync(username, password);
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("[controller]/GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = usersService.GetAll();
            return Ok(users);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("[controller]/CreateUser")]
        public IActionResult Create(CreateUserViewModel user)
        {
            usersService.Create(user);
            return Ok("Created");
        }

    }
}