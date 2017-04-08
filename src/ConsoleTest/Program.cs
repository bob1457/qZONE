using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Twilio;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var accountSid = "AC77d88a49c272f063fe810c7501361d4c";

            var authToken = "425fd6d9cb1b5667e8fe49bde2d6fe73";

            var twilio = new TwilioRestClient(accountSid, authToken);

            var request = new MessageListRequest();

            var messages = twilio.ListMessages(request);

            foreach (var message in messages.Messages)
            {
                Console.Write(message.Body);
            }
        }
    }
}
