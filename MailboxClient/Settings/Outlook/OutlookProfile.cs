﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxClient.Settings.Outlook
{
    public class OutlookProfile
    {
        public string id { get; set; }

        public string name { get; set; }
        public OutlookEmail emails { get; set; }
    }
}
