using Dispatch.Domain;
using Dispatch.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Dispatch.WebAPI.Controllers
{
    // K: To accept external connections (from Android) I had to open a port in Windows firewall rules (easy to do),
    // and specify this computer's IP instead of just "localhost" in either launchSettings.json or applicationhost.config
    // See https://stackoverflow.com/a/8987482/3559724

    // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-6.0
    // https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/controller-methods-views?view=aspnetcore-6.0
    // Don't forget the `[action]` or you're going to have a bad time (see https://stackoverflow.com/a/61561512/3559724)
    [ApiController] [Route("api/[controller]/[action]")]
    public class DispatchController : ControllerBase
    {
        //private readonly ILogger<DispatchController> _logger;
        private readonly IEngine bl;

        public DispatchController(ILogger<WeatherForecastController> logger, IEngine bl)
        {
            //_logger = logger;
            this.bl = bl;
        }

        [HttpPost(Name = "Next")] public async Task<IActionResult> Next()
        {
            await this.bl.Next();
            return Ok();
        }
        [HttpPost(Name = "ReRun")] public async Task<IActionResult> ReRun()
        {
            await this.bl.RunCurrent();
            return Ok();
        }
        [HttpPost(Name = "Move")] public async Task<IActionResult> Move()
        {
            await this.bl.Move();
            return Ok();
        }
        [HttpPost(Name = "Delete")] public async Task<IActionResult> Delete()
        {
            await this.bl.Delete();
            return Ok();
        }
    }
}