using SmartIrrigation.Data;

namespace SmartIrrigation.MQTT;

public class TemperatureSubscriber : Subscriber
{
    public TemperatureSubscriber(TemperatureData temperatureData, ConfiguredData configuration) : base("temperature", temperatureData, configuration)
    {
        
    }
}