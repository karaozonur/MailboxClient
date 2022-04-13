using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailboxClient
{
    public partial class SendMail : Form
    {
        public SendMail()
        {
            InitializeComponent();

          
        }

        private void cbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            Font eski = richTextBox1.Font;
            richTextBox1.SelectionFont = new Font(cbFont.Text, eski.Size, eski.Style);

            switch (cbFont.SelectedIndex)

            {
               
                case 0:

                    richTextBox1.SelectionFont = new Font("Arial", 15);
             
                    break;
          
                case 1:

                    richTextBox1.SelectionFont = new Font("Microsoft Sans Serif",12);
           
                    break;

                    case 2:
                    richTextBox1.SelectionFont = new Font("Comic Sans MS", 12);
                    break;

                case 3:
                    richTextBox1.SelectionFont = new Font("Tahoma", 12);
                    break;
                case 4:
                    richTextBox1.SelectionFont = new Font("Trebuchet MS", 12);
                    break;
                case 5:
                    richTextBox1.SelectionFont = new Font("Verdana", 12);
                    break;

                    


            }

        }

        private void cbFont_DrawItem(object sender, DrawItemEventArgs e)
        {
        }

        private void SendMail_Load(object sender, EventArgs e)
        {
            //InstalledFontCollection inf = new InstalledFontCollection();
            //foreach (Font family font in inf.Families)
            //{
            //    combobox.Items.Add(font.Name); //filling the font name
            //                                   //get the font name of the rich text box text
            //    combobox.Text = this.richtextbox.Font.Name.ToString();
            //}
            RenkleriDuzenle();
            listView1.Columns.Add("Dosya yolu");
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            Settings.MailBoxs.SettingsForPitbullS();


            if (FrmMain.FromPublic == null)
            { }
            else
            {

                txtEmail.Text = FrmMain.FromPublic;
                txtkonu.Text = FrmMain.FromRe;
            }

        }
        private void RenkleriDuzenle()
        {
            panel1.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
        
            panel4.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
           

            BtnMinimized.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            BtnMaxsimized.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            BrnClose.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;

            //panel13.ForeColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;



        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbBoyut_SelectedIndexChanged(object sender, EventArgs e)
        {
            Font eski = richTextBox1.Font;
            //richTextBox1.Font = new Font(eski.FontFamily, float.Parse(cbBoyut.Text), eski.Style);
            richTextBox1.SelectionFont = new Font(eski.FontFamily, float.Parse(cbBoyut.Text), eski.Style);
        }



       
        private void stilAyarla(object sender, EventArgs e)
        {
            
        }

      

        private void cbKalin_CheckedChanged(object sender, EventArgs e)
        {
            FontStyle NFont = new FontStyle();
            richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.SelectionFont.Size, (FontStyle)(FontStyle.Regular));

            if (richTextBox1.SelectionFont.Italic)
            {
                NFont = (FontStyle)(NFont | FontStyle.Italic);
            }
            if (richTextBox1.SelectionFont.Underline)
            {
                NFont = (FontStyle)(NFont | FontStyle.Underline);
            }
            if (cbKalin.Checked)
            {
                NFont = (FontStyle)(NFont | FontStyle.Bold);
                //richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.SelectionFont.Size, (FontStyle)(FontStyle.Underline));
            }
            else
            {
                NFont = (FontStyle)(NFont & FontStyle.Underline);
            }
            richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.SelectionFont.Size, NFont);
        }
        private void cbAltiCizili_CheckedChanged(object sender, EventArgs e)
        {
            FontStyle NFont = new FontStyle();
            richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.SelectionFont.Size, (FontStyle)(FontStyle.Regular));

            if (richTextBox1.SelectionFont.Bold)
            {
                NFont = (FontStyle)(NFont | FontStyle.Bold);
            }
            if (richTextBox1.SelectionFont.Italic)
            {
                NFont = (FontStyle)(NFont | FontStyle.Italic);
            }
            if (cbAltiCizili.Checked)
            {
                NFont = (FontStyle)(NFont | FontStyle.Underline);
                //richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.SelectionFont.Size, (FontStyle)(FontStyle.Underline));
            }
            else
            {
                NFont = (FontStyle)(NFont & FontStyle.Underline);
            }
            richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.SelectionFont.Size, NFont);

        }
        private void cbItalik_CheckedChanged(object sender, EventArgs e)
        {
            FontStyle NFont = new FontStyle();
            richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.SelectionFont.Size, (FontStyle)(FontStyle.Regular));

            if (richTextBox1.SelectionFont.Bold)
            {
                NFont = (FontStyle)(NFont | FontStyle.Bold);
            }
            if (richTextBox1.SelectionFont.Underline)
            {
                NFont = (FontStyle)(NFont | FontStyle.Underline);
            }
            if (cbItalik.Checked)
            {
                NFont = (FontStyle)(NFont | FontStyle.Italic);
                //richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.SelectionFont.Size, (FontStyle)(FontStyle.Underline));
            }
            else
            {
                NFont = (FontStyle)(NFont & FontStyle.Underline);
            }
            richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.SelectionFont.Size, NFont);
        }

        private void btnBoyutKucult_Click(object sender, EventArgs e)
        {
           
            richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.SelectionFont.Size -1);

        }

        private void BtnBoyutArtir_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, richTextBox1.SelectionFont.Size + 1);

        }



        private void BTNRENK_Click(object sender, EventArgs e)
        {
            DialogResult DRS = colorDialog1.ShowDialog();
            if (DRS == DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorDialog1.Color;
            }
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != null && richTextBox1.SelectedText.Trim() != "")
            {

            }

            
        }

        private void BrnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            BtnMaxsimized.Visible = true;
            BtnMinimized.Visible = false;
        }

        private void BtnMaxsimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            BtnMinimized.Visible = true;
            BtnMaxsimized.Visible = false;
        }
        bool formTasiniyor = false;
        Point baslangicNoktasi = new Point(0, 0);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            formTasiniyor = true;
            baslangicNoktasi = new Point(e.X, e.Y);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            formTasiniyor = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (formTasiniyor)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.baslangicNoktasi.X, p.Y - this.baslangicNoktasi.Y);
            }
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = Settings.MailBoxs.Server_Host;
            sc.EnableSsl = false;

            string kime = txtEmail.Text;
            string konu = txtkonu.Text;
            string icerik = richTextBox1.Text;

            sc.Credentials = new NetworkCredential(Settings.MailBoxs.MailForm, Settings.MailBoxs.Sifreler);
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(txtEmail.Text, txtkonu.Text);
            mail.To.Add(kime);
            //mail.To.Add("alici2@mail.com");
            mail.Subject = konu;
            mail.IsBodyHtml = true;
            mail.Body = icerik;
            for (int i = 1; i <= listView1.Items.Count; i++)
            {
                mail.Attachments.Add(new Attachment(listView1.Items[i].Text));
            }
            sc.Send(mail);
        }
       
        private void btntfolder_Click(object sender, EventArgs e)
        {
            //Dosya seçebilmek ve kullanıcının işlem için onayı var mı öğrenebilmek için OpenFileDialog ve DialogResult sınıflarını tanımlıyoruz
            OpenFileDialog myFileDialog = new OpenFileDialog();
            DialogResult dr = new DialogResult();
            myFileDialog.Title = "Dosya ekle";

            //OpenFileDialog ile açılan pencere default olarak C:\ sürücüsünü açacak
            myFileDialog.InitialDirectory = @"C:";

            //Multiselect seçim yapabilmek için bu özelliği true olarak ayarlıyoruz
            myFileDialog.Multiselect = true;

            dr = myFileDialog.ShowDialog();

            //Seçilen dosya isimlerini fileNames dizisinde saklıyoruz
            string[] fileNames = myFileDialog.FileNames;

            //Dosya ve yolunun doğru olması gerektiğini true ile zorunlu tutuyoruz
            myFileDialog.CheckFileExists = true;
            myFileDialog.CheckPathExists = true;

            //Yukarıda tanımladığımız DialogResult sonucunda OK dönmüşse yani kullanıcı Cancel deyip ya da X e basıp çıkmamışsa
            if (dr == DialogResult.OK)
            {

                //Multiselect ile seçilen dosya isimlerini ve yollarını dosya isimli diziye atıp döngü ile ListViewItem e sırasıyla ekliyoruz
                string[] dosya = fileNames;

                for (int i = 0; i < fileNames.Length; i++)
                {
                    ListViewItem li = new ListViewItem(dosya[i]);
                    listView1.Items.Add(li);
                }
                label3.Text = dosya.Length.ToString() + " dosya eklendi.";
            }

            //DialogResult sonucunda kullanıcı Cancel deyip ya da X e basıp çıkmamışsa buraya düşecek return ile uygulamadan çıkıcak yani aslında hiçbir şey olmamaış olacak
            else
            {
                return;
            }
        }



    }

      
}

