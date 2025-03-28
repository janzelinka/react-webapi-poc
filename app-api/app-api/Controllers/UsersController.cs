using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using api.ViewModels;
using app.Services;
using api.Controllers;

namespace app_api.Controllers
{
    [ApiController]
    [Authorize]
    public class UsersController : CrudController<UsersViewModel, IUsersService>
    {
        public UsersController(IUsersService viewService) : base(viewService)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("[controller]/GetAll")]
        public override IEnumerable<UsersViewModel> GetAll()
        {
            //example of allow anonymous, overriden
            //however only one endpoint will be used -> the base one will be replaced by this one
            return base.GetAll();
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