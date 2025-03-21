using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.ViewModels;
using app.Services;
using System.Net;
using app.Responses;
using Microsoft.EntityFrameworkCore;

namespace ing_app_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUsersService usersService;

        public IAuthService AuthService { get; }
        public ApiDataContext Ctx { get; }

        public LoginController(IUsersService usersService, IAuthService authService, ApiDataContext ctx)
        {
            this.usersService = usersService;
            AuthService = authService;
            Ctx = ctx;
        }

        [HttpPost(Name = "Login")]
        public async Task<ActionResult> Login(string username, string password)
        {
            var categories = Ctx.Categories.Include(c => c.Products).ToList();
            var result = await AuthService.LoginAsync(username, password);

            if (result)
            {
                return Ok();
            }

            return Unauthorized();
        }

        [HttpGet]
        [Authorize]
        [Route("[controller]/GetAllUsers")]
        public ActionResult<List<GetAllUsersViewModel>> GetAllUsers()
        {
            var users = usersService.GetAll().ToList();
            return Ok(users);
        }

        [HttpPost]
        [Route("[controller]/CreateUser")]
        public ActionResult<CreateUserResponse> Create(CreateUserViewModel user)
        {
            if (!ModelState.IsValid) {
                var errorList = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                );
                return Ok(new CreateUserResponse { ErrorList = errorList });
            }
            usersService.Create(user);
            return Ok("Created");
        }

    }
}