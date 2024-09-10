using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Versioning.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/StringListUri")]
[ApiVersion("3.0")]
[ApiVersion("4.0")]
public class StringListUriController : ControllerBase
{
    [MapToApiVersion("3.0")]
    [HttpGet()]
    public IEnumerable<string> Get3()
    {
        return Data.Summaries.Where(x => x.StartsWith("C"));
    }

    [MapToApiVersion("4.0")]
    [HttpGet()]
    public IEnumerable<string> Get4()
    {
        return Data.Summaries.Where(x => x.StartsWith("F"));
    }
}