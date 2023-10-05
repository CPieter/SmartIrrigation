using MQTTnet;
using MQTTnet.Internal;
using MQTTnet.Server;

namespace SmartIrrigation.Mqtt;

public class Broker
{
    public Broker()
    {
        Console.WriteLine("Let's go");
        Intercept_Application_Messages();
    }
    
    public static async Task Intercept_Application_Messages()
    {
        Console.WriteLine("Let's go");
        /*
         * This sample starts a simple MQTT server which manipulate all processed application messages.
         * Please see _Server_Simple_Samples_ for more details on how to start a server.
         */

        var mqttFactory = new MqttFactory();
        var mqttServerOptions = new MqttServerOptionsBuilder().WithDefaultEndpoint().Build();

        using (var mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions))
        {
            mqttServer.InterceptingPublishAsync += args =>
            {
                // Here we only change the topic of the received application message.
                // but also changing the payload etc. is required. Changing the QoS after
                // transmitting is not supported and makes no sense at all.
                args.ApplicationMessage.Topic += "/manipulated";

                return CompletedTask.Instance;
            };

            await mqttServer.StartAsync();

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
            await mqttServer.StopAsync();
        }
    }
}