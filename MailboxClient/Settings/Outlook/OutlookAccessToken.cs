﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxClient.Settings.Outlook
{
    public class OutlookAccessToken
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
        public string user_id { get; set; }
    }
}
