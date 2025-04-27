using api.Controllers;
using api.ViewModels;
using app.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app_api.Controllers
{
    [ApiController]
    [Authorize]
    public class UsersController : CrudController<UsersViewModel, IUsersService>
    {
        public UsersController(IUsersService viewService) : base(viewService)
        {
        }

        // [HttpGet]
        // [Route("[controller]/GetAll2")]
        // public IEnumerable<UsersViewModel> GetAll2()
        // {
        //     //in case of GetAll was overriden I can still define GetAll2, 
        //     //however it doesn't make sense to have 2 same endpoints, one private and one public
        //     return base.GetAll();
        // }
    }
}