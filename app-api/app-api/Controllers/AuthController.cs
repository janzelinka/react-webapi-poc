using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace app_api.Controllers
{
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {

        public AuthController(ILogger<AuthController> logger)
        {
            Logger = logger;
        }

        public ILogger<AuthController> Logger { get; }

        [HttpGet]
        [Route("[controller]/IsValid")]
        public bool IsAuth()
        {

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
            {
                AutoRegisterTemplate = true,
                IndexFormat = "logs-myapp-{0:yyyy.MM.dd}",
                NumberOfReplicas = 1,
                NumberOfShards = 2,
                MinimumLogEventLevel = Serilog.Events.LogEventLevel.Verbose,
                
            })
            .Enrich.FromLogContext()
            .CreateLogger();

            Log.Logger.Error("test");
            return true;
        }

        [HttpGet]
        [Route("[controller]/Logout")]
        public async Task<bool> Logout()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
                await HttpContext.SignOutAsync();

            return true;
        }
    }
}