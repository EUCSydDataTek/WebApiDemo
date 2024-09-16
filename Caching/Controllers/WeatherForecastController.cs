using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Caching.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController(IMemoryCache cache, ILogger<WeatherForecastController> logger) : ControllerBase
{
    private const string weatherForecastListCacheKey = "weatherForecastList";

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };


    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get()
    {
        logger.LogInformation("Trying to fetch the list of WeatherForecasts from cache.");
        if (cache.TryGetValue(weatherForecastListCacheKey, out IEnumerable<WeatherForecast>? weatherForecasts))
        {
            logger.LogInformation("WeatherForecasts list found in cache.");
        }
        else
        {
            logger.LogInformation("WeatherForecasts list not found in cache. Fetching from database.");
            weatherForecasts = await GetForecasts();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);
            cache.Set(weatherForecastListCacheKey, weatherForecasts, cacheEntryOptions);
        }
        return Ok(weatherForecasts);
    }


    [HttpPut(Name = "UpdateWeatherForecast")]
    public IActionResult Put()
    {
        cache.Remove(weatherForecastListCacheKey);
        logger.LogInformation("Cache is Invalidated");
        return Ok();
    }

    async Task<IEnumerable<WeatherForecast>?> GetForecasts()
    {
        await Task.Delay(5000);
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
