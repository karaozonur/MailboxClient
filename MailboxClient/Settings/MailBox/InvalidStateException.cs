﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxClient.Settings.MailBox
{
    public class InvalidStateException : Exception
    {
        public InvalidStateException() { }
        public InvalidStateException(string message) : base(message) { }
    }
}