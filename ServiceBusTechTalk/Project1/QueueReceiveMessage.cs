using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using System;
using System.IO;

namespace Project1
{
    public class QueueReceiveMessage
    {
        static string ServiceBusConnectionString = Environment.GetEnvironmentVariable("ConnectionString");
        const string QueueName = "Project1Queue";
        static IQueueClient queueClient;

        public static async Task MainAsync()
        {
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            MessageHandlerOptions messageHandlerOptions = new MessageHandlerOptions(ExceptionHandler) { AutoComplete = false };

            queueClient.RegisterMessageHandler(MessageHandler, messageHandlerOptions);

            Console.ReadKey();

            await queueClient.CloseAsync();
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
            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
            //await queueClient.AbandonAsync(message.SystemProperties.LockToken);
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
