using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MailboxClient;

namespace MailboxClient.MessageFrom
{
    public partial class MailGönderimiptal : Form
    {
        public MailGönderimiptal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MailGönderimiptal sd = new MailGönderimiptal();
            sd.Close();
            this.Close();


            
        }
    }
}
