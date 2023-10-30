using SmartIrrigation.Data;

namespace SmartIrrigation.MQTT;

public class HumiditySubscriber : Subscriber
{
    public HumiditySubscriber(HumidityData humidityData, ConfiguredData configuration) : base("humidity", humidityData, configuration)
    {
        
    }
}