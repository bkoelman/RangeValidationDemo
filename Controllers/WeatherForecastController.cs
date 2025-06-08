using Microsoft.AspNetCore.Mvc;

namespace RangeDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpPost]
    public string Post([FromBody] ExampleModel model)
    {
        logger.LogInformation("Incoming {Fraction}", model.Fraction?.ToString());

        var fractionString = model.Fraction != null
            ? Convert.ToString(model.Fraction, Thread.CurrentThread.CurrentCulture)
            : "(none)";

        return $"Received fraction value: {fractionString}";
    }
}
