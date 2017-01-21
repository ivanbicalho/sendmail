using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMail.Core
{
    public class EmailData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public string EmailSubject { get; set; }
        public string EmailText { get; set; }
    }
}
