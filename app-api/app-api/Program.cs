using Serilog;
using Serilog.Sinks.Elasticsearch;
public class Program
{
    public static void Main(string[] args)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? throw new ArgumentException("Environment variable not specified = 'ASPNETCORE_ENVIRONMENT'");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environmentName}.json")
            .Build();

        var elasticUrl = configuration.GetSection("Configuration").GetValue<string>("ElasticSearchUri") ?? throw new ArgumentException($"ElasticSearchUri is missing in configuration for environment {environmentName}");

        var logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUrl)))
            .CreateLogger();

        CreateHostBuilder(args, environmentName)
        .UseSerilog(logger)
        .Build()
        .Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args, string environmentName) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseUrls("http://0.0.0.0:5000") // Make sure it listens on all network interfaces
                    .UseStartup<Startup>();
            });
}

