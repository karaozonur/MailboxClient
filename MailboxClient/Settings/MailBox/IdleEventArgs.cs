using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxClient.Settings.MailBox
{
    public class IdleEventArgs : EventArgs
    {
        public ImapClient Client { get; set; }
        public Folder Folder { get; set; }
        public Message[] Messages { get; set; }
    }
}
