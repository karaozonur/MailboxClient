using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxClient.Settings.Google
{
    public class GoogleAccessToken
    {
        public string refresh_token { get; set; }
        public string expires_in { get; set; }
        public string access_token { get; set; }
    }
}
