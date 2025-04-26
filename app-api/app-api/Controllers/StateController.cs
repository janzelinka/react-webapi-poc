using api.Models.Events;
using api.Repositories;
using app.Models;
using appapi.Controllers.Base;
using appapi.Factories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace app_api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StateController : BaseEnumController<StateImport>
    {
        public StateController(IElasticFactory elasticFactory) : base("states", elasticFactory)
        {

        }





    }
}