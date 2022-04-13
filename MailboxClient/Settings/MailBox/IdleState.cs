using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxClient.Settings.MailBox
{
    [DefaultValue(Off)]
    public enum IdleState
    {
        Off = 0,
        On = 1,
        Paused = 2,
        Starting = 4,
        Stopping = 8
    }
}

