using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using EASendMail;
using SmtpClient = System.Net.Mail.SmtpClient;
using SmtpMail = System.Web.Mail.SmtpMail;

//using EASendMail;

namespace quZONE.Common.Email
{
    public class EmailNotification
    {
        public void SendEmail(string toAddresLists, string subject, string msgBody)
        {
            //SmtpMail oMail = new SmtpMail("TryIt");
            SmtpClient oSmtp = new SmtpClient();
            MailMessage msg = new MailMessage("admin@ezQ.com", toAddresLists, subject, msgBody);

            // Set sender email address, please change it to yours
            //oSmtp..From = "test@emailarchitect.net";
            // Set recipient email, please change it to yours
            //oSmtp..To = toAddresLists;// "support@emailarchitect.net";

            // Set email subject
            //oSmtp.Subject = subject; //"test email from c# project";
            oSmtp.Host = "smtp.gmail.com";

            // Set email body
            //oSmtp.TextBody = msgBody;//"this is a test email sent from c# project, do not reply";

            // Your SMTP server address
            //SmtpServer oServer = new SmtpServer("mail.smtp2go.com");

            // Set 465 port
            oSmtp.EnableSsl= true;
            

            // detect SSL/TLS automatically
            //oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;


            var account = WebConfigurationManager.AppSettings["gmailAccount"]; // "test@emailarchitect.net";
            var passwd = WebConfigurationManager.AppSettings["gmailCredential"]; //"testpassword";


            NetworkCredential credential = new NetworkCredential(account, passwd);//("bob.h.yuan@gmail.com", "570924MBA");
            // User and password for ESMTP authentication, if your server doesn't require
            // User authentication, please remove the following codes.
            
            oSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
               
            oSmtp.UseDefaultCredentials = false;
            oSmtp.Credentials = credential;
            oSmtp.Port = 587;

            try
            {
                Console.WriteLine("start to send email over SSL ...");
                //oSmtp.SendMail(oServer, oMail);
                oSmtp.Send(msg);
                Console.WriteLine("email was sent successfully!");
            }
            catch (Exception ep)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
            }
        }
    }
}
