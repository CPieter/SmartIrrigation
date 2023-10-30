using Newtonsoft.Json.Linq;

namespace SmartIrrigation;

public readonly struct WeatherForecast
{
    public DateTime DateTime { get; }
    public float PrecipitationProbability { get; } //unit: %
    public float Rainfall { get; } //unit: mm
    public float Temperature { get; } //unit: celsius
    public float TemperatureMin { get; } //unit: celsius
    public float TemperatureMax { get; } //unit: celsius
    public float Humidity { get; } //unit: ???
    public float WindSpeed { get; } //unit: m/s
    
    public WeatherForecast(DateTime dateTime, float precipitationProbability, float rainfall, float temperature, 
        float temperatureMin, float temperatureMax, float humidity, float windSpeed)
    {
        DateTime = dateTime;
        PrecipitationProbability = precipitationProbability;
        Rainfall = rainfall;
        Temperature = temperature;
        TemperatureMin = temperatureMin;
        TemperatureMax = temperatureMax;
        Humidity = humidity;
        WindSpeed = windSpeed;
    }

    public static WeatherForecast FromJObject(JObject json, string forecastHours = "3h")
    {
        long dt = (long)json["dt"];
        float precipitationProbability = (float)json["pop"];
        float rainfall = json["rain"]?[forecastHours] == null ? 0f : (float)json["rain"][forecastHours];
        float temperature = (float)json["main"]["temp"];
        float temperatureMin = (float)json["main"]["temp_min"];
        float temperatureMax = (float)json["main"]["temp_max"];
        float humidity = (float)json["main"]["humidity"] / 100;
        float windSpeed = (float)json["wind"]["speed"];

        DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds(dt).DateTime;

        return new WeatherForecast(dateTime, precipitationProbability, rainfall, temperature, 
            temperatureMin, temperatureMax, humidity, windSpeed);
    }
}