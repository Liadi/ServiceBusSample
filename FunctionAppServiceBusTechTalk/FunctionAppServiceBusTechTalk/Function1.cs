using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionAppServiceBusTechTalk
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([ServiceBusTrigger("project1topic", "sub2", Connection = "ConnectionString")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
