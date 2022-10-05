using AudioSwitcher.AudioApi.CoreAudio;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Dispatch.WebAPI.Controllers
{
    /// <summary>
    /// Courtesy of <see href="https://stackoverflow.com/a/40361942/3559724"/>
    /// </summary>
    [ApiController] [Route("api/[controller]/[action]")]
    public class VolumeController : ControllerBase
    {
        private const int Step = 5;
        private static readonly CoreAudioController coreAudioController = new();
        private CoreAudioDevice defaultPlaybackDevice;
        public VolumeController(ILogger<VolumeController> logger)
        {
            defaultPlaybackDevice = coreAudioController.DefaultPlaybackDevice;
        }

        [HttpPost(Name = "Slide")] public IActionResult Slide(int step)
        {
            Debug.WriteLine($"Current Volume: {defaultPlaybackDevice.Volume}");
            defaultPlaybackDevice.Volume += step;
            return Ok();
        }
        [HttpPost(Name = "Up")] public IActionResult Up(int step = Step)
        {
            Debug.WriteLine($"Current Volume: {defaultPlaybackDevice.Volume}");
            defaultPlaybackDevice.Volume += step;
            return Ok();
        }
        [HttpPost(Name = "Down")] public IActionResult Down(int step = Step)
        {
            Debug.WriteLine($"Current Volume: {defaultPlaybackDevice.Volume}");
            defaultPlaybackDevice.Volume -= step;
            return Ok();
        }
        /// <summary>
        /// Sets local Windows volume to volume.
        /// </summary>
        /// <param name="volume">A double between 0.0 and 100.0.</param>
        /// <returns>Nothing</returns>
        [HttpPost(Name = "Jump")]
        //public IActionResult Jump([FromBody] double volume)
        public IActionResult Jump(double volume)
        {
            Debug.WriteLine($"Current Volume: {defaultPlaybackDevice.Volume}");
            defaultPlaybackDevice.Volume = volume;
            return Ok();
        }
    }
}