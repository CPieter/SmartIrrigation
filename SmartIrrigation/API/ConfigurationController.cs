using Microsoft.AspNetCore.Mvc;
using SmartIrrigation.Data;
using SmartIrrigation.Logic;

namespace SmartIrrigation.API;

[ApiController]
[Route("[controller]")]
public class ConfigurationController
{
    private readonly ConfiguredData _configuredData;

    public ConfigurationController(ConfiguredData configuredData)
    {
        _configuredData = configuredData;
    }
    
    [HttpGet(Name = "Configuration")]
    public Dictionary<string, string> GetConfiguration()
    {
        var dict = new Dictionary<string, string>();
        
        dict.Add("soil", _configuredData.GetSoilType().ToString());
        dict.Add("grass", _configuredData.GetGrassType().ToString());
        dict.Add("irrigationOutput", _configuredData.GetIrrigationOutput().ToString());
        dict.Add("demoMode", _configuredData.InDemoMode().ToString());

        return dict;
    }
    
    [HttpPost(Name = "Configuration")]
    public void PostConfiguration([FromForm] int soilType, [FromForm] int grassType, [FromForm] float irrigationOutput, [FromForm] string demoMode)
    {
        _configuredData.SetSoilType((SoilType)soilType);
        //_configuredData.SetGrassType((GrassType)grassType);
        _configuredData.SetIrrigationOutput(irrigationOutput);
        bool inDemoMode = demoMode.Equals("True");
        _configuredData.SetDemoMode(inDemoMode);
    }
    
    // [HttpPost(Name = "Data")]
    // public void PostManualData(string content)
    // {
    //     try
    //     {
    //         JObject json = JObject.Parse(content);
    //         
    //         _moistureData.Set((float)json["sunrise"]);
    //         _humidityData.Set((float)json["humidity"]);
    //         _temperatureData.Set((float)json["temperature"]);
    //     }
    //     catch
    //     {
    //         // Do nothing :)
    //     }
    // }
}