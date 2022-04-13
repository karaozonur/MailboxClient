using MailboxClient.Settings;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailboxClient
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }
        string ProgramAdi = "Pitbull Email Client";
        private void BrnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

  

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            CBAcilistaCalistir.Checked = MailBoxs.AcilistaCalistir2;
            CBSimgeDurumu.Checked = MailBoxs.SimgeDurumunda2;
            xuıCheckBox3.Checked = MailBoxs.AcilistaCalistir2;
            comboBox1.SelectedIndex = MailBoxs.DilSecenegi2;
            RenkleriDuzenle();
            //PnlGenel.Visible = true;
            //PnlGorunum.Visible = false;
            //PnlGorunum.Hide();
        }
        private void RenkleriDuzenle()
        {
            panel1.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            panel3.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            //pnlLoading.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            //panel4.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
          
            //panel13.ForeColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
        }
        private void BtnGenelSave_Click(object sender, EventArgs e)
        {
            if (PnlGenel.Visible == true)
            {
                SavesSettings();
            }
            else if (PnlGorunum.Visible==true)
            {
                UpdateTema();
            }

        }

        private void UpdateTema()
        {
            using (SQLiteConnection dbConn2 = MailBoxs.Conneciton)
            {
                dbConn2.Open();
                string guncel = "Update TBUsers set ColorHex='" + SecilenRenk + "' Where IDUsers = @IDUsers";
                SQLiteCommand SqlCmm = new SQLiteCommand(guncel, dbConn2);
               
                SqlCmm.Parameters.AddWithValue("@IDUsers", AddUsers.IDUser);

                SqlCmm.ExecuteNonQuery();
                dbConn2.Close();
                dbConn2.Dispose();
            }
               using (SQLiteConnection dbConn2 = MailBoxs.Conneciton)
            {
                dbConn2.Open();
                string guncel = "Update TBAYARLAR set PitbullChat='" + PitbulChat + "' Where IDUsers = @IDUsers";
                SQLiteCommand SqlCmm = new SQLiteCommand(guncel, dbConn2);
               
                SqlCmm.Parameters.AddWithValue("@IDUsers", AddUsers.IDUser);

                SqlCmm.ExecuteNonQuery();
                dbConn2.Close();
                dbConn2.Dispose();
            }
        }

        bool AcilisAyarlari;
        bool SimgeDurumu;
        bool Bildirimler;
        bool Dil;
        private void CBAcilistaCalistir_CheckedStateChanged(object sender, EventArgs e)
        {
            if (CBAcilistaCalistir.Checked == true)
            {
                AcilisAyarlari = true;
             
            }
            else
            {
                AcilisAyarlari = false;
                
            }
        }
        private void SavesSettings()
        {
            using (SQLiteConnection dbConn2 = MailBoxs.Conneciton)
            {
                dbConn2.Open();
                string tur = "INSERT INTO TBAYARLAR(AcilistaCalistir,SimgeDurumunda,BildirimlerAc,DilSecenegi,IDUsers) VALUES (@AcilistaCalistir,@SimgeDurumunda,@BildirimlerAc,@DilSecenegi,@IDUsers)";
                SQLiteCommand SqlCmm = new SQLiteCommand(tur, dbConn2);
                SqlCmm.Parameters.AddWithValue("@AcilistaCalistir", AcilisAyarlari);
                SqlCmm.Parameters.AddWithValue("@SimgeDurumunda", SimgeDurumu);
                SqlCmm.Parameters.AddWithValue("@BildirimlerAc", Bildirimler);
                SqlCmm.Parameters.AddWithValue("@DilSecenegi", Dil);
                SqlCmm.Parameters.AddWithValue("@IDUsers", AddUsers.IDUser);
                SqlCmm.ExecuteNonQuery();
                dbConn2.Close();
                dbConn2.Dispose();
            }
            
        }

        private void CBSimgeDurumu_CheckedStateChanged(object sender, EventArgs e)
        {
            if (CBSimgeDurumu.Checked == true)
            {
                SimgeDurumu = true;

            }
            else
            {
                SimgeDurumu = false;

            }
        }

        private void xuıCheckBox3_CheckedStateChanged(object sender, EventArgs e)
        {
            if (xuıCheckBox3.Checked == true)
            {
                Bildirimler = true;

            }
            else
            {
                Bildirimler = false;

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex==0)
            {
                Dil = true;

            }
            else if (comboBox1.SelectedIndex == 1)
            {
                Dil = false;

            }
        }
        private void BtnGenel_Click(object sender, EventArgs e)
        {
            PnlGenel.Location = new Point(3, 48);
            PnlGenel.Size = new Size(292, 423);
            PnlGorunum.Visible = false;
            PnlGenel.Visible = true;
            //if (PnlGenel.Visible == true)
            //{
            //    PnlGenel.Visible = true;
            //    //PnlGorunum.Visible = false;
            //}

            //PnlGenel.Visible = true;
            //PnlGorunum.Visible = false;
        }
        private void BtnGorunum_Click(object sender, EventArgs e)
        {

         
                PnlGorunum.Visible = true;
            PnlGenel.Visible = false;
            PnlGorunum.Location =new  Point(3,48);
            PnlGorunum.Size = new Size(292,423);

        }
        string SecilenRenk;
        private void PnlMavi_Click(object sender, EventArgs e)
        {
            SecilenRenk = "";
            SecilenRenk = "31;73;125";
            Settings1.Default.Renk = System.Drawing.Color.FromArgb(31, 73,125);
            Settings1.Default.Save();
        }

        private void PnlSiyah_Click(object sender, EventArgs e)
        {
            SecilenRenk = "";
            SecilenRenk = "48,48,48";

            Settings1.Default.Renk = System.Drawing.Color.FromArgb(48, 48, 48);
            Settings1.Default.Save();
        }

        private void PnlKirmizi_Click(object sender, EventArgs e)
        {
            SecilenRenk = "";
            SecilenRenk = "184,15,10";
            ;
            Settings1.Default.Renk = System.Drawing.Color.FromArgb(184, 15, 10);
            Settings1.Default.Save();
        }

        private void PnlTurkuaz_Click(object sender, EventArgs e)
        {
            SecilenRenk = "";
            SecilenRenk = "56,176,192";

            Settings1.Default.Renk = System.Drawing.Color.FromArgb(56, 176,192);
            Settings1.Default.Save();
        }

        private void PnlSiyah_Paint(object sender, PaintEventArgs e)
        {

        }
        bool PitbulChat;
        private void xuıCheckBox1_CheckedStateChanged(object sender, EventArgs e)
        {
            if (xuıCheckBox1.Checked == true)
            {
                PitbulChat = true;

            }
            else
            {
                PitbulChat = false;

            }
        }

        private void PnlMavi_Paint(object sender, PaintEventArgs e)
        {

        }

        //bool AcilistaCalistir2;
        //bool SimgeDurumunda2;
        //bool BildirimlerAc2;
        //int DilSecenegi2;
        //private void SettingsAll()
        //{
        //    using (SQLiteConnection DbConn = MailBoxs.Conneciton)
        //    {
        //        DbConn.Open();
        //        string Kab = "SELECT * FROM TBAYARLAR WHERE IDUsers='"+AddUsers.IDUser+"'";
        //        SQLiteCommand Sqlcmm = new SQLiteCommand(Kab, DbConn);
        //        SQLiteDataReader Sqlread = Sqlcmm.ExecuteReader();
        //        if (Sqlread.Read())
        //        {

        //            AcilistaCalistir2 =Convert.ToBoolean(Sqlread["AcilistaCalistir"]);
        //            SimgeDurumunda2 = Convert.ToBoolean(Sqlread["SimgeDurumunda"]);
        //            BildirimlerAc2 = Convert.ToBoolean(Sqlread["BildirimlerAc"]);
        //            DilSecenegi2 = Convert.ToInt32(Sqlread["DilSecenegi"]);
        //             CBAcilistaCalistir.Checked= AcilistaCalistir2;
        //            CBSimgeDurumu.Checked = AcilistaCalistir2;
        //            xuıCheckBox3.Checked = AcilistaCalistir2;
        //            comboBox1.SelectedIndex = DilSecenegi2;

        //        }
        //        else
        //        {



        //        }
        //        Sqlread.Close();

        //        DbConn.Close();
        //        DbConn.Dispose();


        //    }

        //}
    }
}
