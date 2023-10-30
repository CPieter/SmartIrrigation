using SmartIrrigation.Data;

namespace SmartIrrigation.Logic;

public class IrrigationAlgorithm
{
    private MoistureData _moistureData;
    private TemperatureData _temperatureData;
    private HumidityData _humidityData;
    private WeatherForecastData _weatherForecastData;
    private ConfiguredData _configuredData;
    
    public IrrigationAlgorithm(MoistureData moistureData, TemperatureData temperatureData, HumidityData humidityData, 
        WeatherForecastData weatherForecastData, ConfiguredData configuredData)
    {
        _moistureData = moistureData;
        _temperatureData = temperatureData;
        _humidityData = humidityData;
        _weatherForecastData = weatherForecastData;
        _configuredData = configuredData;
    }

    public int GetMinutesToIrrigate()
    {
        SoilType soilType = _configuredData.GetSoilType();
        float irrigationOutput = _configuredData.GetIrrigationOutput();

        float moisture = _moistureData.Get().Value;
        float temperature = _temperatureData.Get().Value;
        float humidity = _humidityData.Get().Value;

        WeatherForecast currentForecast = _weatherForecastData.GetCurrentForecast();
        float windSpeed = currentForecast.WindSpeed;
        
        DateTime sunrise = _weatherForecastData.GetSunrise();
        DateTime sunset = _weatherForecastData.GetSunset();
        
        DateTime now = DateTime.UtcNow;
        if ((IsOptimalTime(now) && ShouldIrrigate(moisture, temperature, humidity, windSpeed)) || moisture < soilType.PermanentWiltingPoint())
        {
            float requiredWater = GetRequiredWater(currentForecast, sunrise, sunset, moisture);
            int minutesToIrrigate = (int)Math.Ceiling(requiredWater / irrigationOutput);
            return Math.Max(0, minutesToIrrigate);
        }
        
        return 0;
    }

    //returns watering twime in minutes
    public float GetRequiredWater(WeatherForecast currentForecast, DateTime sunrise, DateTime sunset, float moisture)
    {
        float precipitationProbability = currentForecast.PrecipitationProbability;
        float rainfall = currentForecast.Rainfall;

        float precipitation = rainfall * precipitationProbability;
        float evaporation = PredictedEvaporation(currentForecast, sunrise, sunset);

        float waterBalance = precipitation - evaporation;
        float requiredWater = (UpperMoistureThreshold() - moisture) * 304.8f - waterBalance;
        return requiredWater;
    }

    public bool ShouldIrrigate(float moisture, float temperature, float humidity, float windSpeed)
    {
        //moisture thresholds based on soil type
        if (moisture < LowerMoistureThreshold()) return true;
        if (moisture > UpperMoistureThreshold()) return false;
        
        //sensor threshold based on weather forecast
        if (temperature > TemperatureThreshold()) return false;
        if (humidity < HumidityThreshold()) return false;
        if (windSpeed > WindSpeedThreshold()) return false;
        
        return true;
    }

    public float LowerMoistureThreshold()
    {
        SoilType soilType = _configuredData.GetSoilType();
        return soilType.PermanentWiltingPoint() * 1.1f;
    }

    public float UpperMoistureThreshold()
    {
        SoilType soilType = _configuredData.GetSoilType();
        return (soilType.FieldCapacity() + soilType.PermanentWiltingPoint()) * 0.5f;
    }

    public float TemperatureThreshold()
    {
        ICollection<WeatherForecast> forecasts = _weatherForecastData.GetForecasts();
        return forecasts.Select(x => x.Temperature).Average();
    }

    public float HumidityThreshold()
    {
        ICollection<WeatherForecast> forecasts = _weatherForecastData.GetForecasts();
        return forecasts.Select(x => x.Humidity).Average();
    }

    public float WindSpeedThreshold()
    {
        ICollection<WeatherForecast> forecasts = _weatherForecastData.GetForecasts();
        return forecasts.Select(x => x.WindSpeed).Average();
    }

    public bool IsOptimalTime(DateTime dateTime)
    {
        int hour = dateTime.Hour;
        //return hour > 6 && hour < 10;
        return true;
    }

    public float PredictedEvaporation(WeatherForecast forecast, DateTime sunrise, DateTime sunset)
    {
        float temperatureMin = forecast.TemperatureMax;
        float temperatureMax = forecast.TemperatureMin;
        int dayTimeHours = sunset.Hour - sunrise.Hour;
        float temperatureMean = (temperatureMax - temperatureMin) / 2f;
        float blaneyPrediction = (dayTimeHours / 24f) * (0.457f * temperatureMean + 8.128f);
        return blaneyPrediction;
    }
}