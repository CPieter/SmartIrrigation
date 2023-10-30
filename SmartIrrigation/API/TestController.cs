using System.Collections;
using Microsoft.AspNetCore.Mvc;
using SmartIrrigation.MQTT;

namespace SmartIrrigation.API;

[ApiController]
[Route("[controller]")]
public class TestController
{
    private readonly ValvePublisher _valvePublisher;

    public TestController(ValvePublisher valvePublisher)
    {
        _valvePublisher = valvePublisher;
    }

    [HttpPost(Name = "ValveOpen")]
    public void ValveOpen(bool open)
    {
        string message = open ? "True" : "False";
        Task mqttResponse = _valvePublisher.PublishValveOpen(message);
        mqttResponse.Wait();
    }
}