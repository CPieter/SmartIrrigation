using System.Globalization;
using System.Text;
using MQTTnet;
using MQTTnet.Client;
using SmartIrrigation.Data;

namespace SmartIrrigation.MQTT;

public abstract class Subscriber
{
    private readonly ConfiguredData _configuration;
    
    private const string serverAddress = "localhost";
    protected float multiplier = 1f;

    protected Subscriber(string topic, IDataAccess dataAccess, ConfiguredData configuration)
    {
        _configuration = configuration;
        
        Subscribe(topic, dataAccess);
    }

    public async Task Subscribe(String topic, IDataAccess dataAccess)
    {
        var clientFactory = new MqttFactory();
        var mqttClient = clientFactory.CreateMqttClient();
        
        var mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer(serverAddress)
            .WithClientId(topic + "_subscriber")
            .Build();
        var response = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

        mqttClient.ApplicationMessageReceivedAsync += async e =>
        {
            if (_configuration.InDemoMode())
            {
                return;
            }
            var message = e.ApplicationMessage;
            if (message.Topic == topic)
            {
                var messageString = message.ConvertPayloadToString();
                var value = (float)double.Parse(messageString, CultureInfo.InvariantCulture.NumberFormat);
                dataAccess.Set(value * multiplier);
            }
        };
        
        var subscribeFactory = new MqttFactory();
        var mqttSubscribeOptions = subscribeFactory.CreateSubscribeOptionsBuilder()
            .WithTopicFilter(
                filter =>
                {
                    filter.WithTopic(topic);
                })
            .Build();

        await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
    }
}