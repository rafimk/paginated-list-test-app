using Microsoft.AspNetCore.Mvc;

namespace paginated_list_test_app.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("PaginatedList")]
    public async Task<IEnumerable<WeatherForecast>> PaginatedList()
    {
        var dbQuery = Enumerable.Range(1, 50).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });

        // var paginatedResult = await PaginatedList<WeatherForecast>.CreateAsync(dbQuery, 1, 10);
        // return Ok(paginatedResult<WeatherForecast>(paginatedResult.items, paginatedResult.totalCount, paginatedResult.pageIndex, paginatedResult.pageSize));

        return dbQuery.ToArray();
    }
}
