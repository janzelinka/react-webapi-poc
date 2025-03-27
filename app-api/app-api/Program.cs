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
                config.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
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

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        
          // Add MVC support if you're using controllers and views
        services
        .AddHttpContextAccessor()
        .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.Cookie.Name = "ZeloApiCookie";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Customize cookie expiration as needed
            options.SlidingExpiration = true; // Extends the cookie if the user is active
            options.Events = new CookieAuthenticationEvents
            {
                OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                },
                OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                },
            };
        });
        // Add services to the container.
        services.AddDbContext<ApiDataContext>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ICountryRepository, CountryRepository>();
        services.AddTransient<IRegionRepository, RegionRepository>();
        services.AddTransient<ICityRepository, CityRepository>();
        services.AddTransient<IDistrictRepository, DistrictRepository>();
        services.AddTransient<CitiesSeeder>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUsersService, UsersService>();

        services
        .AddControllersWithViews()
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

    }

    // This method is used to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
       if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Development"))
        {
            app.UseSwagger();
            app.UseSwaggerUI();

        }
        app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5000"));
        app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:8081"));
        
        // app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
        using (var scope = app.ApplicationServices.CreateScope()) {
            var seder = scope.ServiceProvider.GetRequiredService<CitiesSeeder>();
            seder.Seed();
        }
    }
}