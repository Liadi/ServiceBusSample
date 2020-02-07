using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using System;
using System.IO;

namespace Project1
{
    class Sub2ReceiveMessage
    {
        static string ServiceBusConnectionString = Environment.GetEnvironmentVariable("ConnectionString");
        const string TopicName = "Project1Topic";
        const string SubscriptionName = "sub2";

        static ISubscriptionClient subClient;

        public static async Task MainAsync()
        {
            // Subscribe
            subClient = new SubscriptionClient(ServiceBusConnectionString, TopicName, SubscriptionName);

            MessageHandlerOptions messageHandlerOptions = new MessageHandlerOptions(ExceptionHandler)
            {
                AutoComplete = false,
            };
            subClient.RegisterMessageHandler(MessageHandler, messageHandlerOptions);

            Console.ReadKey();
            await subClient.CloseAsync();
        }

        // Util Methods

        static async Task MessageHandler(Message message, CancellationToken token)
        {
            using (StreamWriter writer = System.IO.File.AppendText("C:/Users/omliadi/Documents/log.txt"))
            {
                writer.WriteLine("Start");
                var str = Encoding.Default.GetString(message.Body);
                writer.WriteLine(str);
                writer.WriteLine("End");
            }
            await subClient.CompleteAsync(message.SystemProperties.LockToken);
            //await subClient.AbandonAsync(message.SystemProperties.LockToken);
        }

        static Task ExceptionHandler(ExceptionReceivedEventArgs args)
        {
            using (StreamWriter writer = System.IO.File.AppendText("C:/Users/omliadi/Documents/log.txt"))
            {
                writer.WriteLine("An error occured");
            }
            return Task.CompletedTask;
        }
    }
}
