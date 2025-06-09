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

        if (model.Fraction != null)
        {
            var fractionString = model.Fraction.Format(Thread.CurrentThread.CurrentCulture);
            var doubleValue = model.Fraction.ToDouble();

            return $"Received fraction value: {fractionString}, which is approximately: {doubleValue}";
        }

        return "Received no fraction value.";
    }
}
