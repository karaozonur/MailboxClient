using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxClient.Settings.MailBox
{
    public enum ContentTransferEncoding
    {
        Unknown,
        SevenBit,
        EightBit,
        Binary,
        QuotedPrintable,
        Base64
    }
}
