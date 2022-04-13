using EAGetMail;
using MailboxClient.Settings;
using MailboxClient.Settings.MailBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailboxClient
{
    public partial class inbox : Form
    {
        public inbox()
        {
            InitializeComponent();
        }

        private void inbox_Load(object sender, EventArgs e)
        {
            GelenKutusu();
            DosyaSayisi();
        }
        int sayi;
        private void DosyaSayisi()
        {
            using (SQLiteConnection DbConn = MailBoxs.Conneciton)
            {
                //DbConn.Open();
                //string Kab = "SELECT count(*)  FROM TBGelenKutusu where IDUser='" + Form1.IDUser + "' ";
                //SQLiteCommand Sqlcmm = new SQLiteCommand(Kab, DbConn);


                //sayi = Convert.ToInt32(Sqlcmm.ExecuteScalar());


                //DbConn.Close();
                //DbConn.Dispose();
            }
        }

        private void GelenKutusu()
        {
            Settings.MailBox.ImapClient imap = new Settings.MailBox.ImapClient("mail.mailbox.com.tr",146, false,true);
            bool Islogin = imap.Login("info@comdizayn.com", "Kk3fyz***");
            List<Settings.MailBox.Message> messages =
            imap.Folders["INBOX"].Search("ALL", Settings.MailBox.MessageFetchMode.ClientDefault, 30).ToList();
            foreach (Settings.MailBox.Message m in messages)
            {

            }
                //// Create a folder named "inbox" under current directory
                //// to store the email file retrieved.
                //string curpath = Directory.GetCurrentDirectory();
                //string mailbox = String.Format("{0}\\inbox", curpath);

                //// If the folder is not existed, create it.
                //if (!Directory.Exists(mailbox))
                //{
                //    Directory.CreateDirectory(mailbox);
                //}

                //MailServer oServer = new MailServer(Form1.SunucuAdi, Form1.EMail, Form1.Sifre, ServerProtocol.Imap4);
                //MailClient oClient = new MailClient("TryIt");

                //// Set IMAP4 server port
                ////oServer.Port = 143;

                //// If your IMAP4 server requires SSL connection,
                //// Please add the following codes:
                //// oServer.SSLConnection = true;
                //// oServer.Port = 993;

                ////try
                ////{
                //oClient.Connect(oServer);
                //MailInfo[] infos = oClient.GetMailInfos();
                ////infos.

                //if (infos.Length > sayi)
                //{
                //    for (int i = 0; i < infos.Length; i++)
                //    {
                //        using (SQLiteConnection dbConn = MailBox.Conneciton)
                //        {
                //            dbConn.Open();
                //            MailInfo info = infos[i];
                //            Console.WriteLine("Index: {0}; Size: {1}; UIDL: {2}",
                //                info.Index, info.Size, info.UIDL);


                //            Mail oMail = oClient.GetMail(info);

                //            string denemek = oMail.Subject;
                //            System.DateTime d = System.DateTime.Now;
                //            System.Globalization.CultureInfo cur = new
                //                System.Globalization.CultureInfo("tr-TR");
                //            string sdate = d.ToString(cur);
                //            string fileName = String.Format("{0}\\{1}{2}{3}.eml", mailbox, sdate, d.Millisecond.ToString("d3"), i);




                //            string tur = "INSERT INTO TBGelenKutusu (IDUser,Gonderen,Konu,TarihSaat) VALUES (@IDUser,@Gonderen,@Konu,@TarihSaat)";
                //            SQLiteCommand SqlCmm = new SQLiteCommand(tur, dbConn);
                //            SqlCmm.Parameters.AddWithValue("@IDUser", Form1.IDUser);
                //            SqlCmm.Parameters.AddWithValue("@Gonderen", oMail.From.ToString());
                //            SqlCmm.Parameters.AddWithValue("@Konu", denemek.Substring(0, denemek.Length - 15));
                //            SqlCmm.Parameters.AddWithValue("@TarihSaat", sdate);
                //            SqlCmm.ExecuteNonQuery();
                //            dbConn.Close();
                //        }
                //        //oMail.SaveAs(fileName, true);
                //    }
                //    oClient.Quit();
                //}
                //// Quit and purge emails marked as deleted from IMAP4 server.

                ////}
                ////catch (Exception ep)
                ////{
                ////    Console.WriteLine(ep.Message);
                ////}

            }


    }


 }




