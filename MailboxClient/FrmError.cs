using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailboxClient
{
    public partial class FrmError : Form
    {
        public FrmError()
        {
            InitializeComponent();
        }
        public FrmError(Exception ex)
        {
            InitializeComponent();
            lblMessage.Text = ex.Message;
            txtStacktrace.Text = ex.ToString();
        }
    }
}
