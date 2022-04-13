
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxClient.Settings.MailBox
{
    [DefaultValue(Normal)]
    public enum MessageImportance
    {
        Normal,
        High,
        Medium,
        Low
    }
}
