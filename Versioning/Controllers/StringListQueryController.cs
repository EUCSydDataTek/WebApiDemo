using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Versioning.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0", Deprecated = false)]
public class StringListQueryController : ControllerBase
{
    [HttpGet()]
    public IEnumerable<string> Get()
    {
        return Data.Summaries.Where(x => x.StartsWith("B"));
    }
}
