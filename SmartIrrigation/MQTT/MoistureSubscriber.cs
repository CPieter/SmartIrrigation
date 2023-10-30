using SmartIrrigation.Data;

namespace SmartIrrigation.MQTT;

public class MoistureSubscriber : Subscriber
{
    public MoistureSubscriber(MoistureData moistureData, ConfiguredData configuration) : base("moisture", moistureData, configuration)
    {
        multiplier = 0.01f;
    }
}