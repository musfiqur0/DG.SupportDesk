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
            return Ok("Ping successfull");
        }
    }
}
