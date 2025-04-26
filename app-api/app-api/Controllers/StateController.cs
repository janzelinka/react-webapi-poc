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
        private readonly IElasticFactory elasticFactory;

        public StateController(IElasticFactory elasticFactory) : base("states", elasticFactory)
        {
            this.elasticFactory = elasticFactory;
        }


        [HttpGet]
        [Route("[controller]/GetAllByCountry")]
        public IEnumerable<StateImport> GetStatesByCountry([FromQuery] string countryId)
        {
            var all = elasticFactory.CreateElasticClient("states").Search<StateImport>(s => s
                .Query(q => q
                    .QueryString(qs => qs
                        .Fields(f => f.Field(ff => ff.CountryImportId))
                        .Query(countryId)
                    )
                )
            );

            return all.Documents.ToList();
        }

    }
}