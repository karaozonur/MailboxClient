using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailboxClient.MessageFrom
{
    public partial class FMailGonderimiiptal : Form
    {
        public FMailGonderimiiptal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUsers sd2 = new AddUsers();
            sd2.Close();

            //Form1 sdd = new Form1();
            //sdd.Show();

            AddUsers sd22 = new AddUsers();
            sd22.Close();

            FMailGonderimiiptal sd = new FMailGonderimiiptal();
            sd.Close();
            this.Close();


        }
    }
}
