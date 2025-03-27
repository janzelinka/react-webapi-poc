using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using api.Repositories;
using api.Models.Events;

namespace ing_app_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnumController : ControllerBase
    {
        public EnumController(
            ICityRepository cityRepository, 
            ICountryRepository countryRepo,
            IDistrictRepository districtRepository)
        {
            CityRepository = cityRepository;
            CountryRepo = countryRepo;
            DistrictRepository = districtRepository;
        }

        public ICityRepository CityRepository { get; }
        public ICountryRepository CountryRepo { get; }
        public IDistrictRepository DistrictRepository { get; }

        [HttpGet]
        [Route("[controller]/GetAllCities")]
        public IEnumerable<City> GetAllCities()
        {
            return CityRepository.GetAll();
        }

        [HttpGet]
        [Route("[controller]/GetAllCountries")]
        public IEnumerable<Country> GetAllCountries()
        {
            return CountryRepo.GetAll();
        }

        [HttpGet]
        [Route("[controller]/GetAllDistricts")]
        public IEnumerable<District> GetAllDistricts()
        {
            return DistrictRepository.GetAll();
        }


    }
}