using api.Models.Events;
using api.Repositories;
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
    public class EnumController : ControllerBase
    {
        public EnumController(
            ICityRepository cityRepository,
            ICountryRepository countryRepo,
            IStateRepository StateRepository,
            ApiDataContext ctx,
            IConfiguration config,
            IElasticFactory elasticFactory
        )
        {
            CityRepository = cityRepository;
            CountryRepo = countryRepo;
            StateRepository = StateRepository;
            Ctx = ctx;
            Config = config;
            ElasticFactory = elasticFactory;
        }

        public ICityRepository CityRepository { get; }
        public ICountryRepository CountryRepo { get; }
        public IStateRepository StateRepository { get; }
        public ApiDataContext Ctx { get; }
        public IConfiguration Config { get; }
        public IElasticFactory ElasticFactory { get; }

        [HttpGet]
        [Route("[controller]/GetAllCities")]
        public IEnumerable<City> GetAllCities(
            int page = 0,
            int pageSize = 20
        )
        {
            return CityRepository.GetAll().Skip(page * pageSize).Take(pageSize).ToList();
        }

        [HttpGet]
        [Route("[controller]/GetAllCountries")]
        public IEnumerable<Country> GetAllCountries([FromQuery] string countryName)
        {
            var all = ElasticFactory.CreateElasticClient("countries").Search<Country>(s => s
                .Query(q => q
                    .QueryString(qs => qs
                        .Fields(f => f.Field(ff => ff.Name))
                        .Query(string.IsNullOrEmpty(countryName) ? "*" : $"*{countryName}*")
                    )
                )
            );

            return all.Documents.ToList();
        }
        // [HttpGet]
        // [Route("[controller]/GetAllStates")]
        // public IEnumerable<State> GetAllStates()
        // {
        //     return StateRepository.GetAll(c => c.Cities);
        // }


    }
}