using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;


/**************************************/
/*          NOT BEING USED            */
/**************************************/


namespace quZONE.Api.Infrastructure
{
    public class EmailNotification
    {
        public void SendEmail(string toAddresLists, string subject, string msgBody)
        {
            var notificationMessage = new IdentityMessage();

            notificationMessage.Destination = toAddresLists;
            notificationMessage.Subject = subject;
            notificationMessage.Body = msgBody;


            var emailService = new EmailService();

            emailService.SendAsync(notificationMessage);

        }
    }
}
