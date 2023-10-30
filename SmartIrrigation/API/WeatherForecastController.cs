using Microsoft.AspNetCore.Mvc;
using SmartIrrigation.Data;

namespace SmartIrrigation.API;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController
{
    private readonly WeatherForecastData _forecastData;
    
    public WeatherForecastController(WeatherForecastData weatherForecastData)
    {
        _forecastData = weatherForecastData;
    }
    
    [HttpGet(Name = "Current")]
    public WeatherForecast GetCurrent()
    {
        return _forecastData.GetCurrentForecast();
    }
    
    [HttpPost(Name = "Current")]
    public WeatherForecast PostCurrent([FromForm] float precipitationProbability, [FromForm] float rainfall, [FromForm] float windSpeed)
    {
        WeatherForecast oldForecast = _forecastData.GetCurrentForecast();
        WeatherForecast newForecast = new WeatherForecast(DateTime.UtcNow, precipitationProbability, rainfall, 
            oldForecast.Temperature, oldForecast.TemperatureMin, oldForecast.TemperatureMax, oldForecast.Humidity, windSpeed);
        return _forecastData.SetFakeForecast(newForecast);
    }
}