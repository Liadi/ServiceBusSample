using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class Starter
    {
        public static async Task Main(string[] args)
        {
            switch (args[0])
            {
                case "1":
                    await QueueSendMessage.MainAsync();
                    break;

                case "2":
                    await QueueReceiveMessage.MainAsync();
                    break;

                case "3":
                    await TopicSendMessage.MainAsync();
                    break;

                case "4":
                    await Sub1ReceiveMessage.MainAsync();
                    break;

                case "5":
                    await Sub2ReceiveMessage.MainAsync();
                    break;

                default:
                    break;
            }
        }
    }
}
