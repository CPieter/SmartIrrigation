using SmartIrrigation.API;
using SmartIrrigation.Data;

namespace SmartIrrigation.Logic;

public class WeatherForecastScheduler
{
    private readonly WeatherForecastClient _client;
    private readonly WeatherForecastData _data;
    private readonly ConfiguredData _configuration;

    public WeatherForecastScheduler(WeatherForecastClient client, WeatherForecastData data, ConfiguredData configuration)
    {
        _client = client;
        _data = data;
        _configuration = configuration;

        var forecasts = _client.GetForecasts();
        _data.SetForecasts(forecasts);
        _data.SetFakeForecast(forecasts.First());
        _data.SetSunrise(_client.GetSunrise());
        _data.SetSunset(_client.GetSunset());

        HandleTimer();
    }

    private async void HandleTimer()
    {
        using var cts = new CancellationTokenSource();
        using var periodicTimer = new PeriodicTimer(TimeSpan.FromMinutes(5));
        while (await periodicTimer.WaitForNextTickAsync(cts.Token))
        {
            _data.SetForecasts(_client.GetForecasts());
            _data.SetSunrise(_client.GetSunrise());
            _data.SetSunset(_client.GetSunset());
        }
    }
}