﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxClient.Settings.MailBox
{
    public class OperationFailedException : Exception
    {
        public OperationFailedException() { }
        public OperationFailedException(string message) : base(message) { }
    }
}
