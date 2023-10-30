namespace SmartIrrigation.Data;

public class WeatherForecastData
{
    private readonly ConfiguredData _configuration;
    
    public WeatherForecastData(ConfiguredData configuration)
    {
        _configuration = configuration;
    }
    
    private ICollection<WeatherForecast> Forecasts;
    private WeatherForecast FakeForecast;
    private DateTime Sunrise;
    private DateTime Sunset;

    public ICollection<WeatherForecast> SetForecasts(ICollection<WeatherForecast> forecasts)
    {
        Forecasts = forecasts;
        return Forecasts;
    }

    public ICollection<WeatherForecast> GetForecasts()
    {
        return Forecasts;
    }

    public WeatherForecast SetFakeForecast(WeatherForecast forecast)
    {
        FakeForecast = forecast;
        return FakeForecast;
    }

    public WeatherForecast GetCurrentForecast()
    {
        if (_configuration.InDemoMode())
        {
            return FakeForecast;
        }
        return Forecasts.First();
    }

    public DateTime SetSunrise(DateTime dateTime)
    {
        Sunrise = dateTime;
        return Sunrise;
    }

    public DateTime GetSunrise()
    {
        return Sunrise;
    }

    public DateTime SetSunset(DateTime dateTime)
    {
        Sunset = dateTime;
        return Sunset;
    }

    public DateTime GetSunset()
    {
        return Sunset;
    }
}