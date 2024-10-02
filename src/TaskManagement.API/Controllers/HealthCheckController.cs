using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Helpers;

namespace TaskManagement.API.Controllers
{
    [Route("health")]
    public class HealthCheckController : ApiControllerBase
    {
        [HttpGet, Route("check")]
        public IActionResult HealthCheck()
        {
            return Ok("Alive...");
        }
    }
}
