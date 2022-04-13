using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxClient.Settings.MailBox
{
    [Flags]
    public enum MessageFetchState : int
    {
        None = 1,
        Headers = 2,
        Body = 4
    }
}
