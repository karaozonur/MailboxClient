using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxClient.Settings.MailBox
{
    public sealed class MessageHeaderSets
    {

        public static readonly string[] Minimal =
        {
            MessageHeader.From,
            MessageHeader.To,
            MessageHeader.Date,
            MessageHeader.Subject,
            MessageHeader.Cc,
            MessageHeader.ContentType
        };

    }
}
