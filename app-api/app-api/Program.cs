using System.Text.Json.Serialization;
using api.Repositories;
using app.Services;
using appapi.Seeds;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using Serilog.Sinks.Elasticsearch;
public class Program
{
    public static void Main(string[] args)
    {
        var isDocker = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Docker";
        var logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(isDocker ? "http://elasticsearch:9200" : "http://localhost:9200")))
            .CreateLogger();

        CreateHostBuilder(args).UseSerilog(logger).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? throw new ArgumentException("Environment variable not specified = 'ASPNETCORE_ENVIRONMENT'");
                config.AddJsonFile($"appsettings.{environmentName}.json",
                    optional: true,
                    reloadOnChange: false);
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseUrls("http://0.0.0.0:5000") // Make sure it listens on all network interfaces
                    .UseStartup<Startup>();
            });
}

