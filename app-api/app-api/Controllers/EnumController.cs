using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using api.Repositories;
using api.Models.Events;
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
            IConfiguration config
        )
        {
            CityRepository = cityRepository;
            CountryRepo = countryRepo;
            StateRepository = StateRepository;
            Ctx = ctx;
            Config = config;
        }

        public ICityRepository CityRepository { get; }
        public ICountryRepository CountryRepo { get; }
        public IStateRepository StateRepository { get; }
        public ApiDataContext Ctx { get; }
        public IConfiguration Config { get; }

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
        public IEnumerable<Country> GetAllCountries()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("countries");
            var client = new ElasticClient(settings);

            var searchResponse = client.Search<Country>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field("states.cities.name")  // Search in all cities
                        .Query("Banská Bystrica")
                    )
                )
            );


            return Ctx.Countries
            .Include(country => country.States)
            // .ThenInclude(region => region.States)
            .ThenInclude(State => State.Cities);
        }

        // [HttpGet]
        // [Route("[controller]/GetAllStates")]
        // public IEnumerable<State> GetAllStates()
        // {
        //     return StateRepository.GetAll(c => c.Cities);
        // }


    }
}