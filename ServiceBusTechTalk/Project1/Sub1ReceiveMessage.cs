using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Project1
{
    class Sub1ReceiveMessage
    {
        static string ServiceBusConnectionString = Environment.GetEnvironmentVariable("ConnectionString");
        const string TopicName = "notifications-ppe";
        const string SubscriptionName = "notifications-ppe-usl-notification";

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

                using (var stream = new MemoryStream(message.Body))
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        str =  streamReader.ReadToEnd();
                    }
                }


                writer.WriteLine("\n\n\n\n");

                writer.WriteLine(str);

                writer.WriteLine("\n\n\n\n");

                writer.WriteLine("End");
            }
            //await subClient.CompleteAsync(message.SystemProperties.LockToken);
            await subClient.AbandonAsync(message.SystemProperties.LockToken);
        }

        static Task ExceptionHandler(ExceptionReceivedEventArgs args)
        {
            using (StreamWriter writer = System.IO.File.AppendText("C:/Users/omliadi/Documents/log.txt"))
            {
                writer.WriteLine("An error occured");
            }
            return Task.CompletedTask;
        }

        public class Lcu
        {
            #region Original Properties from Media
            /// <summary>
            /// Type of the event. 
            /// </summary>
            public string EventType { get; set; }
            /// <summary>
            /// Release string.
            /// example: 2019 10B
            /// </summary>
            public string Release { get; set; }
            /// <summary>
            /// Release Ticket Id.
            /// </summary>
            public int ReleaseTicketId { get; set; }
            /// <summary>
            /// Kb Article number.
            /// </summary>
            public int KbNumber { get; set; }
            /// <summary>
            /// Product Name.
            /// </summary>
            public string Product { get; set; }
            /// <summary>
            /// Update Type.
            /// </summary>
            public string UpdateType { get; set; }
            /// <summary>
            /// Package Version.
            /// </summary>
            public string PackageVersion { get; set; }
            /// <summary>
            /// Version.
            /// </summary>
            /// <example>18362.175.19H1_release_svc_refresh.190612-0046</example>
            public string OsVersion { get; set; }
            /// <summary>
            /// Collection of architectures with blob uri as gotten from Media team.
            /// </summary>
            public Dictionary<LcuArchitectureType, string> BlobUris { get; } = new Dictionary<LcuArchitectureType, string>();
            #endregion Original Properties from Media

            #region Augmented Processing Properties
            /// <summary>
            /// Timestamp on when this message was received.
            /// </summary>
            public DateTimeOffset ReceivedTimestampUtc { get; set; }
            /// <summary>
            /// MessageId from the ServiceBus message.
            /// </summary>
            public string MessageId { get; set; }
            /// <summary>
            /// CorrelationId from the ServiceBus message.
            /// </summary>
            public string CorrelationId { get; set; }
            /// <summary>
            /// Processing error, if any.
            /// </summary>
            public string Error { get; set; }
            #endregion Augmented Processing Properties
            #region Constructor
            public Lcu() { }
            [JsonConstructor]
            public Lcu(
                string eventType,
                string release,
                int releaseTicketId,
                int kbNumber,
                string product,
                string updateType,
                string packageVersion,
                string osVersion,
                DateTimeOffset receivedTimestampUtc,
                string messageId,
                string correlationId,
                string error = null,
                Dictionary<LcuArchitectureType, string> blobUris = null)
            {
                EventType = eventType;
                Release = release;
                ReleaseTicketId = releaseTicketId;
                KbNumber = kbNumber;
                Product = product;
                UpdateType = updateType;
                PackageVersion = packageVersion;
                OsVersion = osVersion;
                ReceivedTimestampUtc = receivedTimestampUtc;
                MessageId = messageId;
                CorrelationId = correlationId;
                Error = error;
                BlobUris = blobUris;
            }
            #endregion Constructor

            public enum LcuArchitectureType
            {
                unknown = 0,
                arm64,
                x64,
                x86
            }
        }
    }
}
