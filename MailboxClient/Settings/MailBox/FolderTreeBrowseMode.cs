using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailboxClient.Settings.MailBox
{
    [DefaultValue(FolderTreeBrowseMode.Lazy)]
    public enum FolderTreeBrowseMode
    {
        /// <summary>
        /// The subfolder list is only loaded when it is being needed
        /// </summary>
        Lazy,

        /// <summary>
        /// Full folder structure is loaded.
        /// WARNING: Will lead to infinite loops if the folder structure is circular!
        /// </summary>
        Full
    }
}