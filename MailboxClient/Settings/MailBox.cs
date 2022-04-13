using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Windows.Forms;
using System.ComponentModel;
using MailboxClient.MessageFrom.AUsers;
using System.Security.Cryptography;
using System.Threading;
using Microsoft.Win32;

namespace MailboxClient.Settings
{
    public static class MailBoxs
    {

        public static SQLiteConnection Conneciton
        {
            get
            {
                return new SQLiteConnection("Data Source=PitbullDb.db");
            }
            private set
            {
                Conneciton = value;
            }

        }
        public static void Pop3Send()
        {
            AddUsers AUser = new AddUsers();
            MailMessage MailYolla = new MailMessage();
            MailYolla.From = new MailAddress(AddUsers.MailAdres);
            MailYolla.To.Add(AddUsers.MailAdres);
            MailYolla.Subject = "Sınama E- Postası";
            MailYolla.Priority = MailPriority.High;
            MailYolla.Body = "Mailbox Client";
            SmtpClient Yolla = new SmtpClient();
            Yolla.SendCompleted += new SendCompletedEventHandler(Yolla_SendCompleted);
            Yolla.Credentials = new System.Net.NetworkCredential(AddUsers.MailAdres, AddUsers.Parola);
            Yolla.Host = AddUsers.Server;
            Yolla.Timeout = 50000;
            Yolla.Port = 587;
            Yolla.EnableSsl = false;

            string userState = "Mail Gönderiliyor"; Yolla.SendAsync(MailYolla, userState);
        }

        public static string MD5eDonustur(string metin)
        {
            MD5CryptoServiceProvider pwd = new MD5CryptoServiceProvider();
            return Sifrele(metin, pwd);
        }
        private static string Sifrele(string metin, HashAlgorithm alg)
        {
            byte[] byteDegeri = System.Text.Encoding.UTF8.GetBytes(metin);
            byte[] sifreliByte = alg.ComputeHash(byteDegeri);
            return Convert.ToBase64String(sifreliByte);
        }
        public static void Yolla_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string Durum = (string)e.UserState;
            if (e.Cancelled == true)
            {
                MessageBox.Show("Mail Gönderimi İptal Edildi");
            }
            if (e.Error != null)
            {


                MessageFrom.MailGönderimiptal SD = new MessageFrom.MailGönderimiptal();

                SD.ShowDialog();
            }
            else
            {

                using (SQLiteConnection dbConn = MailBoxs.Conneciton)
                {
                    string Md5Parola = MD5eDonustur(AddUsers.Parola);
                    dbConn.Open();
                    string tur = "INSERT INTO TBUsers(EncryptedPassword,Server_Host,Server_Port,Username,MailAdress,Ssl) VALUES (@EncryptedPassword,@Server_Host,@Server_Port,@Username,@MailAdress,@Ssl)";
                    SQLiteCommand SqlCmm = new SQLiteCommand(tur, dbConn);
                    SqlCmm.Parameters.AddWithValue("@EncryptedPassword", AddUsers.Parola);
                    SqlCmm.Parameters.AddWithValue("@Server_Host", AddUsers.Server);
                    SqlCmm.Parameters.AddWithValue("@Server_Port", AddUsers.Port);
                    SqlCmm.Parameters.AddWithValue("@Username", AddUsers.AdiSoyadi);
                    SqlCmm.Parameters.AddWithValue("@MailAdress", AddUsers.MailAdres);
                    SqlCmm.Parameters.AddWithValue("@Ssl", AddUsers.SslVarYok);


                    SqlCmm.ExecuteNonQuery();


                    dbConn.Close();
                    dbConn.Dispose();
                }

            }
        }
        public static bool AcilistaCalistir2;
        public static bool SimgeDurumunda2;
        public static bool BildirimlerAc2;
        public static int DilSecenegi2;
        public static int PitbullChat;
        public static void SettingsForPitbull()
        {
            using (SQLiteConnection DbConn = MailBoxs.Conneciton)
            {
                DbConn.Open();
                string Kab = "SELECT * FROM TBAYARLAR WHERE IDUsers='" + AddUsers.IDUser + "'  ORDER BY ID DESC LIMIT 1";
                SQLiteCommand Sqlcmm = new SQLiteCommand(Kab, DbConn);
                SQLiteDataReader Sqlread = Sqlcmm.ExecuteReader();
                if (Sqlread.Read())
                {

                    AcilistaCalistir2 = Convert.ToBoolean(Sqlread["AcilistaCalistir"]);
                    SimgeDurumunda2 = Convert.ToBoolean(Sqlread["SimgeDurumunda"]);
                    BildirimlerAc2 = Convert.ToBoolean(Sqlread["BildirimlerAc"]);
                    DilSecenegi2 = Convert.ToInt32(Sqlread["DilSecenegi"]);
                    PitbullChat = Convert.ToInt32(Sqlread["PitbullChat"]);

                    if (AcilistaCalistir2 == true)
                    {
                        AcilistaCalistir();
                    }
                    else
                    {
                        AcilistaKapat();
                    }
                    if (PitbullChat == 1)
                    {
                        //FrmMain sfg =new FrmMain();
                        //sfg.LnkLPitbulChat.Visible = false;

                    }
                  
                }
                else
                {



                }
                Sqlread.Close();

                DbConn.Close();
                DbConn.Dispose();


            }
        }
        public static string renk;
        public static string MailForm;
        public static string Sifreler;
        public static string Server_Host;
        public static string UserUid;

        public static void SettingsForPitbullS()
        {
            using (SQLiteConnection DbConn = MailBoxs.Conneciton)
            {
                DbConn.Open();
                string Kab = "SELECT * FROM TBUsers WHERE IDUsers='" + AddUsers.IDUser + "' ";
                SQLiteCommand Sqlcmm = new SQLiteCommand(Kab, DbConn);
                SQLiteDataReader Sqlread = Sqlcmm.ExecuteReader();
                if (Sqlread.Read())
                {


                    UserUid = Sqlread["IDUsers"].ToString();
                    renk = Sqlread["ColorHex"].ToString();
                    MailForm = Sqlread["MailAdress"].ToString();
                    Sifreler = Sqlread["EncryptedPassword"].ToString();
                    Server_Host = Sqlread["Server_Host"].ToString();

                    if (AcilistaCalistir2 == true)
                    {
                        AcilistaCalistir();
                    }
                    else
                    {
                        AcilistaKapat();
                    }
                }
                else
                {



                }
                Sqlread.Close();

                DbConn.Close();
                DbConn.Dispose();


            }
        }
        public static void MaxsimizedOrMinimized()
        {
            FrmMain MainForm = new FrmMain();
            if (MainForm.WindowState == FormWindowState.Maximized)
            {
                MainForm.WindowState = FormWindowState.Normal;
                MainForm.BtnMaxsimized.Visible = false;
                MainForm.BtnMinimized.Visible = true;
            }
            else if (MainForm.WindowState == FormWindowState.Minimized)
            {
                //MainForm.WindowState = FormWindowState.Normal;
                //BtnMaxsimized.Visible = false;
                //BtnMinimized.Visible = true;
            }
            else if (MainForm.WindowState == FormWindowState.Normal)
            {
                MainForm.WindowState = FormWindowState.Maximized;
                MainForm.BtnMaxsimized.Visible = true;
                MainForm.BtnMinimized.Visible = false;
            }
        }
        public static void AcilistaCalistir()
        {
            //RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            //key.SetValue("PitbullEmail Client", "\"" + Application.ExecutablePath + "\"");
        }
        public static void AcilistaKapat()
        {
            //RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false);
            //key.DeleteValue("PitbullEmail Client");
        }
        public static bool InternetKontrol()
        {
            try
            {
                System.Net.Sockets.TcpClient kontrol_client = new System.Net.Sockets.TcpClient("www.google.com.tr", 80);
                kontrol_client.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static void  FolderNames()
        {
             if (FrmMain.FolderName == "Deleted Items")
                {
                FrmMain.Deneme = "Çöp Kutusu";
                }
                else if (FrmMain.FolderName == "INBOX")
                {
                FrmMain.Deneme = "Gelen Kutusu";
                }
                else if (FrmMain.FolderName == "Drafts")
                {
                FrmMain.Deneme = "Taslaklar";
                }
                else if (FrmMain.FolderName == "Junk E-mail")
                {
                FrmMain.Deneme = "Gereksiz e-posta";
                }
                else if (FrmMain.FolderName == "Sent Items")
                {
                FrmMain.Deneme = "Gönderilmiş Öğeler";
                }
                else if (FrmMain.FolderName == "Public")
                {
                FrmMain.Deneme = "Public";
                }
                else if (FrmMain.FolderName == "Notes")
                {
                FrmMain.Deneme = "Notlar";
                }
                else
                {
                FrmMain.Deneme = " ";
                }           
        }

    }
}
