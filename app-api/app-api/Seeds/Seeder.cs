using api.Models.Events;
using api.Repositories;
using Newtonsoft.Json;

namespace appapi.Seeds {
    public class CitiesSeeder {
        private string baseImportPath = "./Seeds/countries/sk/";
        private readonly ApiDataContext ctx;
        public CitiesSeeder(
            ICityRepository cityRepo, 
            IRegionRepository regionRepo, 
            ICountryRepository countryRepo,
            IDistrictRepository districtRepo,
            ApiDataContext ctx
        )
        {
            CityRepo = cityRepo;
            RegionRepo = regionRepo;
            CountryRepo = countryRepo;
            DistrictRepo = districtRepo;
            this.ctx = ctx;
        }

        public ICityRepository CityRepo { get; }
        public IRegionRepository RegionRepo { get; }
        public ICountryRepository CountryRepo { get; }
        public IDistrictRepository DistrictRepo { get; }

        public void Seed()
        {
            CityRepo.DeleteRange(CityRepo.GetAll());
            DistrictRepo.DeleteRange(DistrictRepo.GetAll());
            RegionRepo.DeleteRange(RegionRepo.GetAll());
            CountryRepo.DeleteRange(CountryRepo.GetAll());

            var countries = Import<CountryImport>($"{baseImportPath}countries.json");
            var regions = Import<RegionImport>($"{baseImportPath}regions.json");
            var districts = Import<DistrictImport>($"{baseImportPath}districts.json");
            var cities = Import<CityImport>(@$"{baseImportPath}villages.json");

            SeedCountries(countries);
            SeedRegions(regions);

            var regionsDb = RegionRepo.GetAll();
            SeedDistricts(districts, regionsDb);

            var districtsDb = DistrictRepo.GetAll();
            SeedCities(cities, districtsDb);
        }

        private void SeedCities(
            CityImport[] cities, 
            IEnumerable<District> districts
        )
        {
            List<City> citiesToAdd = new List<City>();
            cities.ToList().ForEach((city) =>
            {
                var assignedDistrict = districts.Where(district => district.Code.Equals(city.DistrictId)).First();

                citiesToAdd.Add(new City
                {
                    District = assignedDistrict,
                    Name = city.Name,
                    PostalCode = city.PostalCode
                });
            });

            CityRepo.AddRange(citiesToAdd);
        }

        private void SeedDistricts(
            DistrictImport[] districts,
            IEnumerable<Region> regionsDb
        )
        {
            List<District> districtsToAdd = new List<District>();
            districts.ToList().ForEach(district =>
            {
                var assignedRegion = regionsDb.Where(region => district.RegionId.Equals(region.Code)).First();

                districtsToAdd.Add(new District
                {
                    Name = district.Name,
                    Region = assignedRegion,
                    Code = district.DistrictId
                });
            });

            DistrictRepo.AddRange(districtsToAdd);
        }

        private void SeedRegions(RegionImport[] regions)
        {
            regions.ToList().ForEach((region) =>
            {
                //we have only one country in dataset
                var assignedCountry = CountryRepo.GetAll().First();

                RegionRepo.Add(new Region
                {
                    Code = region.RegionId,
                    Name = region.Name,
                    Country = assignedCountry
                });
            });
        }

        private void SeedCountries(CountryImport[] countries)
        {
            CountryRepo.Add(new Country
            {
                Code = "SK",
                Name = countries[0].Name
            });
        }

        private T[] Import<T>(string jsonPath) {
            var jsonFile = File.ReadAllText(jsonPath);
            var parsedFile = JsonConvert.DeserializeObject<T[]>(jsonFile) ?? throw new ArgumentException("provided file is wrong!");
            return parsedFile;
        }
    }
}