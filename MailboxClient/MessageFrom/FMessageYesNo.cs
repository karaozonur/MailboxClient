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
    public partial class FMessageYesNo : Form
    {
        public FMessageYesNo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FMessageYesNo sd = new FMessageYesNo();
            sd.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
