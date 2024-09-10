using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Versioning.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("2.0")]
public class StringListMediaHeaderController : ControllerBase
{
    [HttpGet()]
    public IEnumerable<string> Get()
    {
        return Data.Summaries.Where(x => x.StartsWith("S"));
    }
}
