using System.Text.Json.Serialization;
using api.Repositories;
using app.Services;
using appapi.Seeds;
using Microsoft.AspNetCore.Authentication.Cookies;
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
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

