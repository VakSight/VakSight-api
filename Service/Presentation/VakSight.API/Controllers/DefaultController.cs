using Microsoft.AspNetCore.Mvc;

namespace VakSight.API.Controllers
{
    [ApiController]
    [ApiVersionNeutral]
    [Route("")]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<string> Get()
        {
            return Ok("Service is running normally...");
        }
    }
}
