using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Formatter;

namespace SmartIrrigation.Mqtt;

public class Client
{
    public static async Task Connect_Client_Using_MQTTv5()
    {
        /*
         * This sample creates a simple MQTT client and connects to a public broker using MQTTv5.
         *
         * This is a modified version of the sample _Connect_Client_! See other sample for more details.
         */

        var mqttFactory = new MqttFactory();

        using (var mqttClient = mqttFactory.CreateMqttClient())
        {
            var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("broker.hivemq.com").WithProtocolVersion(MqttProtocolVersion.V500).Build();

            // In MQTTv5 the response contains much more information.
            var response = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            Console.WriteLine("The MQTT client is connected.");

            //response.DumpToConsole();
        }
    }
}