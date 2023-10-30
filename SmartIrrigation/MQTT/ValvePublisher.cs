using System.ComponentModel;
using MQTTnet;
using MQTTnet.Client;

namespace SmartIrrigation.MQTT;

public class ValvePublisher
{
    private const string serverAddress = "localhost";
    private const string valveOpenTopic = "valve/open";
    
    private readonly IMqttClient _mqttClient;

    public ValvePublisher()
    {
        var mqttFactory = new MqttFactory();
        _mqttClient = mqttFactory.CreateMqttClient();
        
        var mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer(serverAddress)
            .WithClientId("valve_publisher")
            .Build();
        _mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
    }

    public async Task PublishValveOpen(string open)
    {
        await _mqttClient.PublishAsync(new MqttApplicationMessageBuilder()
            .WithTopic(valveOpenTopic)
            .WithPayload(open)
            .Build());
    }

    public async Task PublishMinutesToIrrigate(int minutesToIrrigate)
    {
        string payload = minutesToIrrigate.ToString();
        
        await _mqttClient.PublishAsync(new MqttApplicationMessageBuilder()
            .WithTopic(valveOpenTopic)
            .WithPayload(payload)
            .Build());
    }
}