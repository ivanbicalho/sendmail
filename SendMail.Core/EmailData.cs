using SendMail.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMail.Core
{
    public class EmailData
    {
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string FromPassword { get; set; }

        public string EmailSubject { get; set; }
        public string EmailText { get; set; }

        public string ToName { get; set; }
        public string ToEmail { get; set; }
    }
}
