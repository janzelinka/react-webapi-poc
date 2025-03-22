using System.Text.Json.Serialization;
using api.Repositories;
using app.Services;
using appapi.Seeds;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
builder
.Services
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
builder.Services.AddDbContext<ApiDataContext>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<ICountryRepository, CountryRepository>();
builder.Services.AddTransient<IRegionRepository, RegionRepository>();
builder.Services.AddTransient<ICityRepository, CityRepository>();
builder.Services.AddTransient<IDistrictRepository, DistrictRepository>();
builder.Services.AddTransient<CitiesSeeder>();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUsersService, UsersService>();
/* authentication */
// var key = Encoding.ASCII.GetBytes("tD6GmsZZjCj6bvcbPDLM4gs5UhQcDOuOtD6GmsZZjCj6bvcbPDLM4gs5UhQcDOuO");

// builder.Services.AddAuthentication(options =>
//     {
//         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     }).AddJwtBearer(options =>
//     {
//         options.RequireHttpsMetadata = false; // In production, set this to true
//         options.SaveToken = true;
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuerSigningKey = true,
//             IssuerSigningKey = new SymmetricSecurityKey(key),
//             ValidateIssuer = false,
//             ValidateAudience = false
//         };
//     });

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            // Suppress the automatic model state validation response
            options.SuppressModelStateInvalidFilter = true;
        });;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



//seeding
using (var scope = app.Services.CreateScope()) {
    var seder = scope.ServiceProvider.GetRequiredService<CitiesSeeder>();
    seder.Seed();
}


// countryRepo.Create(new api.Models.Events.Country{ Code = "SK", Name = "Slovenska republika", })

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

builder.Services.AddCors();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:5262"));
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:8081"));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
