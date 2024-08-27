using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace RoutingConfig.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConfigurationController(IConfiguration configuration) : ControllerBase
{
    [HttpGet("my-key")]
    public ActionResult GetMyKey()
    {
        var myKey = configuration["MyKey"];
        return Ok(myKey);
    }

    [HttpGet("database-configuration")]
    public ActionResult GetDatabaseConfiguration()
    {
        var type = configuration["Database:Type"];
        var connectionString = configuration["Database:ConnectionString"];
        return Ok(new
        {
            Type = type,
            ConnectionString = connectionString
        });
    }

    [HttpGet("database-configuration-with-bind")]
    public ActionResult GetDatabaseConfigurationWithBind()
    {
        var databaseOption = new DatabaseOption();
        // The `SectionName` is defined in the `DatabaseOption` class, which shows the section name in the `appsettings.json` file.
        configuration.GetSection(DatabaseOption.SectionName).Bind(databaseOption);
        // You can also use the code below to achieve the same result
        // configuration.Bind(DatabaseOption.SectionName, databaseOption);
        return Ok(new
        {
            databaseOption.Type,
            databaseOption.ConnectionString
        });
    }

    [HttpGet("database-configuration-with-generic-type")]
    public ActionResult GetDatabaseConfigurationWithGenericType()
    {
        var databaseOption = configuration.GetSection(DatabaseOption.SectionName).Get<DatabaseOption>();
        return Ok(new
        {
            databaseOption!.Type,
            databaseOption.ConnectionString
        });
    }

    [HttpGet("database-configuration-with-ioptions")]
    public ActionResult GetDatabaseConfigurationWithIOptions([FromServices] IOptions<DatabaseOption> options)
    {
        var databaseOption = options.Value;
        return Ok(new
        {
            databaseOption.Type,
            databaseOption.ConnectionString
        });
    }
}
