using DependencyInjectionDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionDemo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LifetimeController(IScopedService scopedService, ITransientService transientService, ISingletonService singletonService) : ControllerBase
{

    [HttpGet]
    public ActionResult Get([FromServices] ITransientService transientService)
    {
        var scopedServiceMessage = scopedService.SayHello();
        var transientServiceMessage = transientService.SayHello();
        var singletonServiceMessage = singletonService.SayHello();
        return Content($"{scopedServiceMessage}{Environment.NewLine}{transientServiceMessage}{Environment.NewLine}{singletonServiceMessage}");
    }
}
