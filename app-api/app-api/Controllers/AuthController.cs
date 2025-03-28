using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

namespace app_api.Controllers
{
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        [Route("[controller]/IsValid")]
        public bool IsAuth()
        {
            return true;
        }

        [HttpGet]
        [Route("[controller]/Logout")]
        public async Task<bool> Logout()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
                await HttpContext.SignOutAsync();

            return true;
        }
    }
}