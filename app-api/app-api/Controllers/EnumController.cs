using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using api.Repositories;
using api.Models.Events;
using Microsoft.EntityFrameworkCore;

namespace ing_app_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnumController : ControllerBase
    {
        public EnumController(
            ICityRepository cityRepository, 
            ICountryRepository countryRepo,
            IDistrictRepository districtRepository,
            ApiDataContext ctx,
            IConfiguration config
        )
        {
            CityRepository = cityRepository;
            CountryRepo = countryRepo;
            DistrictRepository = districtRepository;
            Ctx = ctx;
            Config = config;
        }

        public ICityRepository CityRepository { get; }
        public ICountryRepository CountryRepo { get; }
        public IDistrictRepository DistrictRepository { get; }
        public ApiDataContext Ctx { get; }
        public IConfiguration Config { get; }

        [HttpGet]
        [Route("[controller]/GetAllCities")]
        public IEnumerable<City> GetAllCities(
            int page = 0, 
            int pageSize = 20
        )
        {
            return CityRepository.GetAll(c => c.District).Skip(page * pageSize).Take(pageSize).ToList();
        }

        [HttpGet]
        [Route("[controller]/GetAllCountries")]
        public IEnumerable<Country> GetAllCountries()
        {
            return Ctx.Countries
            .Include(country => country.Regions)
            .ThenInclude(region => region.Districts)
            .ThenInclude(district => district.Cities);
        }

        [HttpGet]
        [Route("[controller]/GetAllDistricts")]
        public IEnumerable<District> GetAllDistricts()
        {
            return DistrictRepository.GetAll(c => c.Cities);
        }


    }
}