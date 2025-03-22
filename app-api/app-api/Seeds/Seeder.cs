using api.Repositories;
using Newtonsoft.Json;

namespace appapi.Seeds {
    public class CitiesSeeder {

        private string baseImportPath = "./Seeds/countries/sk/";
        public CitiesSeeder(
            ICityRepository cityRepo, 
            IRegionRepository regionRepo, 
            ICountryRepository countryRepo,
            IDistrictRepository districtRepo
        )
        {
            CityRepo = cityRepo;
            RegionRepo = regionRepo;
            CountryRepo = countryRepo;
            DistrictRepo = districtRepo;
        }

        public ICityRepository CityRepo { get; }
        public IRegionRepository RegionRepo { get; }
        public ICountryRepository CountryRepo { get; }
        public IDistrictRepository DistrictRepo { get; }

        public void Seed()
        {
            var countries = Import<CountryImport>($"{baseImportPath}countries.json");
            var regions = Import<RegionImport>($"{baseImportPath}regions.json");
            var districts = Import<DistrictImport>($"{baseImportPath}districts.json");
            var cities = Import<CityImport>(@$"{baseImportPath}villages.json");

            SeedCountries(countries);
            SeedRegions(regions);
            SeedDistricts(districts);
            SeedCities(cities);
        }

        private void SeedCities(CityImport[] cities)
        {
            cities.ToList().ForEach((city) =>
            {
                CityRepo.Create(new api.Models.Events.City
                {
                    District = DistrictRepo.GetAll().Where(district => district.Code.Equals(city.DistrictId)).First(),
                    Name = city.Name,
                    PostalCode = city.PostalCode
                });
            });
        }

        private void SeedDistricts(DistrictImport[] districts)
        {
            districts.ToList().ForEach(district =>
            {
                DistrictRepo.Create(new api.Models.Events.District
                {
                    Name = district.Name,
                    Region = RegionRepo.GetAll().Where(region => district.RegionId.Equals(region.Code)).First(),
                    Code = district.DistrictId
                });
            });
        }

        private void SeedRegions(RegionImport[] regions)
        {
            regions.ToList().ForEach((region) =>
            {
                RegionRepo.Create(new api.Models.Events.Region
                {
                    Code = region.RegionId,
                    Name = region.Name,
                    Country = CountryRepo.GetAll().First()
                });
            });
        }

        private void SeedCountries(CountryImport[] countries)
        {
            CountryRepo.Create(new api.Models.Events.Country
            {
                Code = "SK",
                Name = countries[0].Name
            });
        }

        private T[] Import<T>(string jsonPath) {
            var jsonFile = File.ReadAllText(jsonPath);
            var parsedFile = JsonConvert.DeserializeObject<T[]>(jsonFile);
            if (parsedFile == null) {
                throw new ArgumentException("provided file is wrong!");
            }
            return parsedFile;
        }
    }

    public class CountryImport {
        [JsonProperty("name")]
        public string Name {get;set;}= string.Empty;

        [JsonProperty("code")]
        public string Code {get;set;} = string.Empty;

    }
    public class RegionImport {

        [JsonProperty("name")]
        public string Name {get;set;}= string.Empty;

        [JsonProperty("id")]
        public string RegionId {get;set;} = string.Empty;
    }


    public class DistrictImport {

        [JsonProperty("name")]
        public string Name {get;set;} = string.Empty;

        [JsonProperty("region_id")]
        public string RegionId {get;set;}

        [JsonProperty("id")]
        public string DistrictId {get;set;}

    }

    public class CityImport {

        [JsonProperty("fullname")]
        public string Name {get;set;} = string.Empty;

        [JsonProperty("district_id")]
        public string DistrictId {get;set;}

        [JsonProperty("zip")]
        public string PostalCode {get;set;}

    }
}