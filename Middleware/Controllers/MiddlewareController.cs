using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Middleware.Controllers;
[Route("[controller]")]
[ApiController]
public class MiddlewareController : ControllerBase
{
    [HttpGet("rate-limiting")]
    [EnableRateLimiting(policyName: "fixed")]
    public ActionResult RateLimitingDemo()
    {
        return Ok($"Hello {DateTime.Now.Ticks.ToString()}");
    }
}
