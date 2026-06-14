using Microsoft.AspNetCore.Mvc;

namespace DG.SupportDesk.Api.Controllers
{
    [Route("api/ping")]
    [ApiController]
    public class PingController : ControllerBase
    {
        public PingController()
        {

        }

        [HttpGet("test")]
        public async ValueTask<IActionResult> Test()
        {
            //Log.Information("Ping Successful");
            throw new Exception("Hello world, Testing by Dev Musfiqur.");
            return Ok("Ping successfull");
        }
    }
}
