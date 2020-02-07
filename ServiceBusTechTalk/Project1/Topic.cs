//using System.Text;
//using System.Text.Json;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Azure.ServiceBus;
//using System;
//using System.IO;

//namespace Project1
//{
//    class Topic
//    {
//        static string ServiceBusConnectionString = "Endpoint=sb://tech-talk.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=tIPCU4Y4H8oYTeggZH3r3273ROScl/gVXFJdQWwMe0M=";
        
//        const string TopicName = "Project1Topic";
//        static ITopicClient topicClient;

//        static async Task Main(string[] args)
//        {
//            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);

//            // Enqueue Topic
//            await SendMessageToTopic("Hello Topic!");

//            // Delay
//            Thread.Sleep(10 * 1000);

//            // Subscribe to Queue
//            MessageHandlerOptions messageHandlerOptions = new MessageHandlerOptions(ExceptionHandler)
//            {
//                AutoComplete = false,
//            };
//            topicClient.(MessageHandler, messageHandlerOptions);
//        }


//        // Util Methods
//        static async Task SendMessageToQueue<T>(T message)
//        {
//            Message msg = new Message(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message)))
//            {
//                TimeToLive = new TimeSpan(0, 5, 0),
//            };
//            await queueClient.SendAsync(msg);
//        }

//        static async Task SendMessageToTopic<T>(T message)
//        {
//            Message msg = new Message(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message)))
//            {
//                TimeToLive = new TimeSpan(0, 5, 0),
//            };
//            await topicClient.SendAsync(msg);
//        }

//        static async Task MessageHandler(Message message, CancellationToken token)
//        {
//            using (StreamWriter writer = System.IO.File.CreateText("C:/Users/omliadi/Documents/log.txt"))
//            {
//                writer.WriteLine("Start");
//                writer.WriteLine(message.Body);
//                writer.WriteLine("End");
//            }
//            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
//        }

//        static async Task ExceptionHandler(ExceptionReceivedEventArgs args)
//        {
//        }
//    }
//}
