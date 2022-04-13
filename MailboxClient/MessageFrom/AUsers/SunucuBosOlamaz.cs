using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailboxClient.MessageFrom.AUsers
{
    public partial class SunucuBosOlamaz : Form
    {
        public SunucuBosOlamaz()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUsers sd22 = new AddUsers();
            sd22.Close();

            FMailGonderimiiptal sd = new FMailGonderimiiptal();
            sd.Close();
            this.Close();
        }
    }
}
