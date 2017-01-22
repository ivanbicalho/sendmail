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
        public EmailParameters Parameters { get; set; }
        public List<EmailEntity> To { get; set; }
    }
}
