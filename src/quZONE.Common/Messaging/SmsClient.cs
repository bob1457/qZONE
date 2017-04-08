using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.TwiML;

namespace quZONE.Common.Messaging
{
    public class SmsClient
    {
        public void SendSmsMessage() //Testing ONLY
        {
            var accountSid = ConfigurationManager.AppSettings["SMSAccountIdentification"];

            var authToken = ConfigurationManager.AppSettings["SMSAccountPassword"];

            var fromNumber = ConfigurationManager.AppSettings["SMSAccountFrom"];


            //var smsClient = new TwilioRestClient(authCode, authToken);

            //smsClient.SendMessage(fromNumber, "+16046195810", "Test sms");

            //var accountSid = "AC77d88a49c272f063fe810c7501361d4c"; // Your Account SID from www.twilio.com/console
            //var authToken = "425fd6d9cb1b5667e8fe49bde2d6fe73"; // Your Auth Token from www.twilio.com/console

            var twilio = new TwilioRestClient(accountSid, authToken);
            
            var message = twilio.SendMessage(
                fromNumber, // From (Replace with your Twilio number)
                "+16046195810", // To (Replace with your phone number)
                "Hello from C# quZONE web api with generaic settings..."
                );

        }

        public void SendSmsMessageLive(string to, string msg) //Live Production ONLY
        {
            var accountSid = ConfigurationManager.AppSettings["SMSAccountIdentification"];

            var authToken = ConfigurationManager.AppSettings["SMSAccountPassword"];

            var fromNumber = ConfigurationManager.AppSettings["SMSAccountFrom"];
            

            var twilio = new TwilioRestClient(accountSid, authToken);

            //the following method will be replaced with passed argument later when Twilio account is activated in production
            //
            var message = twilio.SendMessage(
                fromNumber, // From (Replace with your Twilio number)
                //"+16046195810", // To (Replace with your phone number)
                to,
                msg
                );

        }

        public string GetReceivedMessage(string sid)
        {
            var accountSid = ConfigurationManager.AppSettings["SMSAccountIdentification"];

            var authToken = ConfigurationManager.AppSettings["SMSAccountPassword"];

            var twilio = new TwilioRestClient(accountSid, authToken);

            var message = twilio.RedactMessage(sid);

            return message.Body;
        }

        public List<string> GetAllMessagesList()
        {
            var accountSid = ConfigurationManager.AppSettings["SMSAccountIdentification"];

            var authToken = ConfigurationManager.AppSettings["SMSAccountPassword"];

            var twilio = new TwilioRestClient(accountSid, authToken);

            var request = new MessageListRequest();

            var messages = twilio.ListMessages(request);

            var mList = new List<string>();

            foreach (var message in messages.Messages)
            {
                //Console.Write(message.Body);
                mList.Add(message.Body);
            }

            return mList;
        }

    }
}
