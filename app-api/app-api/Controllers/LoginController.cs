using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.ViewModels;
using app.Services;
using app.Responses;

namespace app_api.Controllers
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
            // var categories = Ctx.Categories.Include(c => c.Products).ToList();
            var result = await AuthService.LoginAsync(username, password);

            if (result)
            {
                return Ok();
            }

            return Unauthorized();
        }
    }
}