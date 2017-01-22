using SendMail.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMail.Core
{
    public class EmailResult
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
