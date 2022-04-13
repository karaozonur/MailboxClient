using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxClient.Settings.MailBox
{
    public class ServerAlertException : Exception
    {
        public ServerAlertException() { }
        public ServerAlertException(string message) : base(message) { }
    }
}
