using System.Text.Json.Serialization;
using api.Repositories;
using app.Services;
using appapi.Seeds;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using Serilog.Sinks.Elasticsearch;

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
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        })
        .ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://elasticsearch:9200"))
            {
                AutoRegisterTemplate = true,
                IndexFormat = "logs-myapp-{0:yyyy.MM.dd}",
                NumberOfReplicas = 1,
                NumberOfShards = 2
            })
            .Enrich.FromLogContext()
            .CreateLogger();

    }

    // This method is used to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? throw new ArgumentException("Environment variable not specified = 'ASPNETCORE_ENVIRONMENT'");
       if (environmentName.Equals("Development"))
        {
            app.UseSwagger();
            app.UseSwaggerUI();
    
        }
        app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5000"));
        app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:8081"));
        
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