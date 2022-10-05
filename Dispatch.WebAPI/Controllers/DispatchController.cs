using Dispatch.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Dispatch.WebAPI.Controllers
{
    // @K: To accept external connections (< Android), I had to open a port for incoming requests in Windows firewall Inbound Rules (easy),
    // and also replace 'localhost' with this computer's actual IP in either launchSettings.json or applicationhost.config.
    // That is because Android does not understand Windows-style hostnames (eg 'localhost')
    // See https://stackoverflow.com/a/8987482/3559724
    // This (IIS Express configuration) was not necessary: https://stackoverflow.com/a/28471888/3559724 because IIS was not used (WebAPI self-host works as-is).
    // But applicationhost.config might be useful: https://stackoverflow.com/a/8987482/3559724 (which is located in \.vs\Dispatch\config)
    //  More info on Bindings: https://docs.microsoft.com/en-us/iis/configuration/system.applicationhost/sites/site/bindings/

    // .NET Core routing.
    // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-6.0
    // https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/controller-methods-views?view=aspnetcore-6.0
    // Don't forget the `[action]` or you're going to have a bad time (see https://stackoverflow.com/a/61561512/3559724)
    [ApiController] [Route("api/[controller]/[action]")]
    public class DispatchController : ControllerBase
    {
        //private readonly ILogger<DispatchController> _logger;
        private readonly IEngine bl;

        public DispatchController(ILogger<DispatchController> logger, IEngine bl)
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

        [HttpGet(Name = "GetFileInfo")]
        public IActionResult GetFileInfo()
        {
            return Ok(Path.GetFileNameWithoutExtension(this.bl.CurrentFilePath));
        }
    }
}