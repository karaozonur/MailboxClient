﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxClient.Settings.MailBox
{
    public abstract class CommandProcessor
    {
        public bool TwoWayProcessing { get; protected set; }

        /// <summary>
        ///     Processes a command result part
        /// </summary>
        /// <param name="data">The command result part</param>
        public abstract void ProcessCommandResult(string data);

        public virtual byte[] AppendCommandData(string serverResponse)
        {
            throw new NotImplementedException();
        }
    }
}
