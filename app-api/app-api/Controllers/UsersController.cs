using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using api.ViewModels;
using app.Services;

namespace ing_app_api.Controllers
{
    [ApiController]
    [Authorize]
    public class UsersController : CrudController<UsersViewModel, IUsersService>
    {
        public UsersController(IUsersService viewService) : base(viewService)
        {
        }
    }
}