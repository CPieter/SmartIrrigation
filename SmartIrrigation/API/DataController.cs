using Microsoft.AspNetCore.Mvc;
using SmartIrrigation.Data;

namespace SmartIrrigation.API;

[ApiController]
[Route("[controller]")]
public class DataController
{
    private readonly MoistureData _moistureData;
    private readonly HumidityData _humidityData;
    private readonly TemperatureData _temperatureData;

    public DataController(MoistureData moistureData, HumidityData humidityData, TemperatureData temperatureData)
    {
        _moistureData = moistureData;
        _humidityData = humidityData;
        _temperatureData = temperatureData;
    }
    
    [HttpGet(Name = "Data")]
    public Dictionary<string, string> GetData()
    {
        var dict = new Dictionary<string, string>();
        
        dict.Add("moisture", _moistureData.Get().Value.ToString());
        dict.Add("humidity", _humidityData.Get().Value.ToString());
        dict.Add("temperature", _temperatureData.Get().Value.ToString());

        return dict;
    }
    
    [HttpPost(Name = "Data")]
    public void PostManualData([FromForm] float moisture, [FromForm] float humidity, [FromForm] float temperature)
    {
        _moistureData.Set(moisture);
        _humidityData.Set(humidity);
        _temperatureData.Set(temperature);
    }
}