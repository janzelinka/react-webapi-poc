using api.Models.Events;
using api.Repositories;
using Nest;
using Newtonsoft.Json;

namespace appapi.Seeds
{
    public class CitiesSeeder
    {
        private string baseImportPath = "./Seeds/countries/sk2/";
        private readonly ApiDataContext ctx;
        private readonly IUserRepository usersRepo;
        private ElasticClient client;


        public CitiesSeeder(

            ApiDataContext ctx,
            IUserRepository usersRepo
        )
        {

            this.ctx = ctx;
            this.usersRepo = usersRepo;
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("countries");
            client = new ElasticClient(settings);
        }


        public void Seed_v2()
        {
            // CityRepo.DeleteRange(CityRepo.GetAll());
            // StateRepo.DeleteRange(StateRepo.GetAll());
            // StateRepo.DeleteRange(StateRepo.GetAll());
            // CountryRepo.DeleteRange(CountryRepo.GetAll());

            var countries = Import<CountryImport>($"{baseImportPath}countries.json");
            var states = Import<StateImport>($"{baseImportPath}states.json");
            var cities = Import<CityImport>(@$"{baseImportPath}cities.json");
            if (client.Indices.Exists("countries").Exists)
            {
                client.Indices.Delete("countries");
            }
            if (client.Indices.Exists("states").Exists)
            {
                client.Indices.Delete("states");
            }
            if (client.Indices.Exists("cities").Exists)
            {
                client.Indices.Delete("cities");
            }
            var countriesResponse = client.Bulk(c => c.Index("countries").IndexMany(countries));
            var statesResponse = client.Bulk(c => c.Index("states").IndexMany(states));
            var citiesResponse = client.Bulk(c => c.Index("cities").IndexMany(cities));
            // //filter for only cz and sk.
            // foreach(var country in countries) {
            //     var countryToImport = new Country() {
            //         Name = country.Name,
            //     };

            //     var statesToImport = new List<State>();
            //     foreach(var state in states.Where(s => s.CountryId.Equals(country.ImportCountryId))) {

            //         var citiesToImport = new List<City>();
            //         foreach(var city in cities.Where(c => c.StateId.Equals(state.StateImportId))) {
            //             citiesToImport.Add(new City {
            //                 Name = city.Name
            //             });
            //         } 

            //         statesToImport.Add(new State {
            //             Name = state.Name,
            //             Cities = citiesToImport,
            //         });

            //     }
            //     countryToImport.States = statesToImport;
            //     CountryRepo.Add(countryToImport);

            //     client.Indices.Delete("countries");
            //     var indexResponse = client.IndexDocument(countryToImport);
            // }

            usersRepo.CreateUser(new api.ViewModels.UsersViewModel
            {
                Email = "zelo",
                FirstName = "j",
                LastName = "z",
                Password = "12000"
            });
        }
        private T[] Import<T>(string jsonPath)
        {
            var jsonFile = File.ReadAllText(jsonPath);
            var parsedFile = JsonConvert.DeserializeObject<T[]>(jsonFile) ?? throw new ArgumentException("provided file is wrong!");
            return parsedFile;
        }
    }
}