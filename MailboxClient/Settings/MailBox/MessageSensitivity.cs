using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxClient.Settings.MailBox
{
    [DefaultValue(None)]
    public enum MessageSensitivity
    {
        None,
        Personal,
        Private,
        CompanyConfidential
    }
}
