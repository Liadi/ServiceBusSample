using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using System;
using System.IO;

namespace Project1
{
    public static class UtilExtension
    {
        public static string ConvertBytesToString(this byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }

    class TopicSendMessage
    {
        static string ServiceBusConnectionString = Environment.GetEnvironmentVariable("ConnectionString");

        const string TopicName = "Project1Topic";
        static ITopicClient topicClient;

        public static async Task MainAsync()
        {
            //topicClient = new TopicClient(ServiceBusConnectionString, TopicName);

            //// Enqueue Topic
            //await SendMessageToTopic("Hello Topic!");

            //await topicClient.CloseAsync();

            await AddRule();

        }

        // Util Methods
        static async Task SendMessageToTopic<T>(T message)
        {
            Message msg = new Message(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message)))
            {
                TimeToLive = new TimeSpan(2, 0, 0),
            };
            msg.UserProperties["Decider"] = 3;
            await topicClient.SendAsync(msg);
        }

        static async Task AddRule()
        {
            ISubscriptionClient subClient = new SubscriptionClient(ServiceBusConnectionString, "sample", "main");

            //await subClient.AddRuleAsync(new RuleDescription("UpdateType_eq_CummulativeSecurity", new SqlFilter("UpdateType = 'Cumulative Security'" )));
            await subClient.AddRuleAsync(new RuleDescription("KbNumber_4536040", new SqlFilter("KbNumber=45360401234")));

            await subClient.CloseAsync();
            //Message msg = new Message(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message)))
            //{
            //    TimeToLive = new TimeSpan(2, 0, 0),
            //};
            //msg.UserProperties["Decider"] = 3;
            //await topicClient.SendAsync(msg);
            
        }

        static async Task RemoveRule<T>(T message)
        {
            Message msg = new Message(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message)))
            {
                TimeToLive = new TimeSpan(2, 0, 0),
            };
            msg.UserProperties["Decider"] = 3;
            await topicClient.SendAsync(msg);
        }
    }
}
