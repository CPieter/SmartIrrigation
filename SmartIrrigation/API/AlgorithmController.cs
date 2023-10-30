using Microsoft.AspNetCore.Mvc;
using SmartIrrigation.Data;
using SmartIrrigation.Logic;

namespace SmartIrrigation.API;

[ApiController]
[Route("[controller]")]
public class AlgorithmController
{
    private readonly IrrigationAlgorithm _irrigationAlgorithm;
    
    public AlgorithmController(IrrigationAlgorithm irrigationAlgorithm)
    {
        _irrigationAlgorithm = irrigationAlgorithm;
    }
    
    [HttpGet(Name = "MinutesToIrrigate")]
    public int GetMinutesToIrrigate()
    {
        return _irrigationAlgorithm.GetMinutesToIrrigate();
    }
}