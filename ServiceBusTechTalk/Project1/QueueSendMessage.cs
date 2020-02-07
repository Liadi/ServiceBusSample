using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using System;
using System.IO;

namespace Project1
{
    public class QueueSendMessage
    {
        static string ServiceBusConnectionString = Environment.GetEnvironmentVariable("ConnectionString");
        const string QueueName = "Project1Queue";
        static IQueueClient queueClient;

        public static async Task MainAsync()
        {
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            // Enqueue
            await SendMessageToQueue("Hello Queue!");


            await queueClient.CloseAsync();
        }


        // Util Methods
        static async Task SendMessageToQueue<T>(T message)
        {
            Message msg = new Message(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message)))
            {
                TimeToLive = new TimeSpan(2, 0, 0),
            };
            await queueClient.SendAsync(msg);
        }
    }
}
