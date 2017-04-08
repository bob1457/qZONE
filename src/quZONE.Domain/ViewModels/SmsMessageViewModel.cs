using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quZONE.Domain.ViewModels
{
    public class SmsMessageViewModel
    {
        public string From { get; set; }

        public string To { get; set; }

        public DateTime SentDate { get; set; }

        public string MessageBody { get; set; }

        //public string MessageUri { get; set; }

        public string Sid { get; set; }

        public string AccountSid { get; set; }

    }
}
