

using Application.Interfaces.API;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Validator.Controllers
{
    [AllowAnonymous]
    [Route("/Info")]
    [ApiVersion("1.0")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        IInfoService _service;

        public InfoController(IInfoService service)
        {
            _service = service;
        }

        [Route("/Info/healthcheck")]
        [HttpGet]
        public async Task<IActionResult> DbHealthCheck()
        {
            var result = await _service.HealthCheckAsync();
            return StatusCode(result.Status == "Failed" ? StatusCodes.Status503ServiceUnavailable : StatusCodes.Status200OK, result);
        }
    }
}
