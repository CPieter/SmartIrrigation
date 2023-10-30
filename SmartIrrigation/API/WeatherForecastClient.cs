using Newtonsoft.Json.Linq;

namespace SmartIrrigation.API;

public class WeatherForecastClient
{
    private double Lat = 51.441643;
    private double Long = 5.469722;
    private string API_key = "a73959f18d0cbdc24595cc6ec3390eed";
    private string Units = "metric";
    private string URL;

    public WeatherForecastClient()
    {
        URL = $"https://api.openweathermap.org/data/2.5/forecast?lat={Lat}&lon={Long}&appid={API_key}&units={Units}";
    }
    
    public ICollection<WeatherForecast> GetForecasts()
    {
        //Make API call
        string content = GetStringResponse(URL);
        
        //To Json
        JObject json = JObject.Parse(content);
        List<JToken> tokens = json["list"].ToList();
        
        //To Forecast
        List<WeatherForecast> forecasts = new List<WeatherForecast>();
        foreach (var token in tokens)
        {
            JObject forecastJson = (JObject)token;
            WeatherForecast forecast = WeatherForecast.FromJObject(forecastJson);
            forecasts.Add(forecast);
        }
        
        return forecasts;
    }

    public DateTime GetSunrise()
    {
        //Make API call
        string content = GetStringResponse(URL);
        
        JObject json = JObject.Parse(content);
        JToken cityToken = json["city"];
        long sunrise = (long)cityToken["sunrise"];
        return DateTimeOffset.FromUnixTimeSeconds(sunrise).DateTime;
    }

    public DateTime GetSunset()
    {
        //Make API call
        string content = GetStringResponse(URL);
        
        JObject json = JObject.Parse(content);
        JToken cityToken = json["city"];
        long sunrise = (long)cityToken["sunset"];
        return DateTimeOffset.FromUnixTimeSeconds(sunrise).DateTime;
    }

    private string GetStringResponse(string url)
    {
        HttpClient _httpClient = new HttpClient();
        
        var httpResponse = _httpClient.GetAsync(url);
        httpResponse.Wait();
        var resultResponse = httpResponse.Result.Content.ReadAsStringAsync();
        resultResponse.Wait();
        return resultResponse.Result;
    }
}