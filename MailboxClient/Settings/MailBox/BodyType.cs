using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxClient.Settings.MailBox
{
    [DefaultValue(None), Flags]
    public enum BodyType
    {
        None = 1,
        Text = 2,
        Html = 4,
        TextAndHtml = Text | Html
    }
}
