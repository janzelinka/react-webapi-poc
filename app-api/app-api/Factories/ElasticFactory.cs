using Nest;

namespace appapi.Factories {

    public interface IElasticFactory {
        ElasticClient CreateElasticClient(string defaultIndex);
    }
    public class ElasticFactory : IElasticFactory {

        private readonly string _ElasticUrl;
        public ElasticFactory(IConfiguration config)
        {
            _ElasticUrl = config.GetSection("Configuration").GetValue<string>("ElasticSearchUri") ?? throw new ArgumentException($"Missing ElasticSearchUri in configuration for environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Docker"}" );
        }
        public ElasticClient CreateElasticClient(string defaultIndex) {
            var settings = new ConnectionSettings(new Uri(_ElasticUrl)).DefaultIndex(defaultIndex);
            var client = new ElasticClient(settings);
            return client;
        }
    }
}