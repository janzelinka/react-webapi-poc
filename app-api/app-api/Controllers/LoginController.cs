using Microsoft.AspNetCore.Mvc;
using app.Services;

namespace app_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUsersService usersService;

        public IAuthService AuthService { get; }
        public ApiDataContext Ctx { get; }
        public ILogger<LoginController> Logger { get; }

        public LoginController(IUsersService usersService, IAuthService authService, ApiDataContext ctx, ILogger<LoginController> logger)
        {
            this.usersService = usersService;
            AuthService = authService;
            Ctx = ctx;
            Logger = logger;
        }

        [HttpPost(Name = "Login")]
        public async Task<ActionResult> Login(string username, string password)
        {
            Logger.LogError("This is my error.");

            var result = await AuthService.LoginAsync(username, password);

            if (result)
            {
                return Ok();
            }

            return Unauthorized();
        }
    }
}