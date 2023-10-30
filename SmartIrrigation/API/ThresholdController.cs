using Microsoft.AspNetCore.Mvc;
using SmartIrrigation.Data;
using SmartIrrigation.Logic;

namespace SmartIrrigation.API;

[ApiController]
[Route("[controller]")]
public class ThresholdController
{
    private readonly IrrigationAlgorithm _irrigationAlgorithm;
    
    public ThresholdController(IrrigationAlgorithm irrigationAlgorithm)
    {
        _irrigationAlgorithm = irrigationAlgorithm;
    }
    
    [HttpGet(Name = "Thresholds")]
    public Dictionary<string, float> GetThresholds()
    {
        var dict = new Dictionary<string, float>();
        
        dict.Add("lowerMoisture", _irrigationAlgorithm.LowerMoistureThreshold());
        dict.Add("upperMoisture", _irrigationAlgorithm.UpperMoistureThreshold());
        dict.Add("temperature", _irrigationAlgorithm.TemperatureThreshold());
        dict.Add("windSpeed", _irrigationAlgorithm.WindSpeedThreshold());
        dict.Add("humidity", _irrigationAlgorithm.HumidityThreshold());

        return dict;
    }
}