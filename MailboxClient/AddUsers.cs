using MailboxClient.MessageFrom.AUsers;
using MailboxClient.Settings;
using MailboxClient.Settings.Google;
using MailboxClient.Settings.MailBox;
using MailboxClient.Settings.Outlook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailboxClient
{
    [DefaultValue(Simple)]
    public enum AuthMode
    {
        Simple,
        Google,
        Outlook,
        Yahoo,
        none
    }

    public partial class AddUsers : Form
    {
        private AuthMode _authMode = AuthMode.Simple;
        private Regex _rexCode = new Regex(@"code=([^&]+)", RegexOptions.IgnoreCase);

        private string _oAuth2Code;
        public AddUsers()
        {
            InitializeComponent();
            //cmbPort.SelectedIndex = 1;
            cmbEncryption.SelectedIndex = 1;
        }
        private void picLogo_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.pitbull.com.tr/");
        }

        private void picGMailLogin_Click(object sender, EventArgs e)
        {
            _authMode = AuthMode.Google;

            wbrMain.Show();
            pnlLogin.Hide();
            //btnDefaultAuth.Show();
            //picGMailLogin.Hide();
            pnlLogin.Hide();
            //picOutlook.Hide();
            //picYahoo.Hide();
            wbrMain.Navigate(GoogleOAuth2Provider.BuildAuthenticationUri());
        }

        private void wbrMain_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                _oAuth2Code = "";

                if (_authMode == AuthMode.Google)
                {
                    HtmlElement element = wbrMain.Document.GetElementById("code");
                    if (element == null) return;
                    _oAuth2Code = element.GetAttribute("value");
                }
                else if (_authMode == AuthMode.Outlook && wbrMain.Url.ToString().StartsWith(OutlookOAuth2Provider.REDIRECT_URI))
                {
                    var match = _rexCode.Match(wbrMain.Url.Query);
                    if (match.Success)
                    {
                        _oAuth2Code = match.Groups[1].Value;
                    }
                }
                //else if (_authMode == AuthMode.Yahoo && wbrMain.Url.ToString().StartsWith(YahooOAuth2Provider.REDIRECT_URI))
                //{
                //    var match = _rexCode.Match(wbrMain.Url.Query);
                //    if (match.Success)
                //    {
                //        _oAuth2Code = match.Groups[1].Value;
                //    }
                //}

                if (!string.IsNullOrWhiteSpace(_oAuth2Code))
                {
                    wbrMain.Hide();
                    lblWait.Text = "Connecting...";
                    lblWait.Show(); /*btnDefaultAuth.Hide();*/
                   

                    InitClient();

                    (new Thread(Connect)).Start(true);
                }

            }
            catch
            {
            }
        }

        private void btnDefaultAuth_Click(object sender, EventArgs e)
        {
            _authMode = AuthMode.Simple;
            //btnDefaultAuth.Hide();
            //picGMailLogin.Show();
            //picOutlook.Show();
            //picYahoo.Show();
            wbrMain.Navigate("about:blank");
            wbrMain.Hide();

            pnlLogin.Show();
        }

        private void InitClient()
        {
            if (KullaniciVar == true)
            {
                if (Program.ImapClient == null)
                    Program.ImapClient = new ImapClient();


                SslProtocols ssl = cmbEncryption.SelectedIndex == 0
                    ? SslProtocols.None
                    : (cmbEncryption.SelectedIndex == 1 ? SslProtocols.Default : SslProtocols.Tls);

                Program.ImapClient.Host = SunucuAdi;
            Program.ImapClient.Port = Convert.ToInt32(PortNumber);
                Program.ImapClient.SslProtocol = SslProtocols.None;
                Program.ImapClient.ValidateServerCertificate = !chkValidateCertificate.Enabled ||
                                                               chkValidateCertificate.Checked;

                Program.ImapClient.IsDebug = true;

            }
            else
            {
                if (Program.ImapClient == null)
                    Program.ImapClient = new ImapClient();


                SslProtocols ssl = cmbEncryption.SelectedIndex == 0
                    ? SslProtocols.None
                    : (cmbEncryption.SelectedIndex == 1 ? SslProtocols.Default : SslProtocols.Tls);

                Program.ImapClient.Host = TxtSunucu.Text;
                Program.ImapClient.Port = Convert.ToInt32(TxtPort.Text);
                Program.ImapClient.SslProtocol = ssl;
                Program.ImapClient.ValidateServerCertificate = !chkValidateCertificate.Enabled ||
                                                               chkValidateCertificate.Checked;

                Program.ImapClient.IsDebug = true;
            }
           
        }

        private void Connect(object arg)
        {
            try
            {


                if (Program.ImapClient.Connect())
                {
                    Invoke(new SuccessDelegate(OnConnectSuccessful));

                }
                else
                {
                    Invoke(new FailedDelegate(OnConnectFailed));
                }
                    
            }
            catch (Exception ex)
            {
                Invoke(new FailedDelegate(OnConnectFailed));
            }
        }
        bool KullaniciVar=false;
     public static string IDUser;
        string SunucuAdi;
        string Emailx;
        string Sifre;
        string PortNumber;
        string KullaniciAdi;
        bool Ssl;
        bool kontrol = MailBoxs.InternetKontrol();
        private void KullanciVArYok()
        {
            using (SQLiteConnection DbConn = MailBoxs.Conneciton)
            {
                DbConn.Open();
                string Kab = "SELECT * FROM TBUsers ";
                SQLiteCommand Sqlcmm = new SQLiteCommand(Kab, DbConn);
                SQLiteDataReader Sqlread = Sqlcmm.ExecuteReader();
                if (Sqlread.Read())
                {
                    KullaniciVar = true;
                    IDUser = Sqlread["IDUsers"].ToString();
                    Sifre = Sqlread["EncryptedPassword"].ToString();
                    SunucuAdi = Sqlread["Server_Host"].ToString();
                    PortNumber = Sqlread["Server_Port"].ToString();
                    KullaniciAdi = Sqlread["Username"].ToString();
                    Emailx = Sqlread["MailAdress"].ToString();
                    Ssl = Convert.ToBoolean(Sqlread["Ssl"]);
                    MailBoxs.SettingsForPitbullS();
                    RenkleriDuzenle();
                    //FrmMain sa = new FrmMain();c
                    //sa.Visible = true;
                    if (kontrol == true)
                    {
                        InitClient();
                        (new Thread(Connect)).Start(false);
                    }
                    else
                    {
                        FrmMain ds = new FrmMain();
                        ds.Show();
                    }
                   
                }
                else
                {
                    KullaniciVar = false;
                   

                }
                Sqlread.Close();

                DbConn.Close();
                DbConn.Dispose();


            }


            //DosyaSayisi();
            //DosyaOku();
        }
        private void Authenticate(object arg)
        {
            try
            {
               

                if (Program.ImapClient.Login())
                    Invoke(new SuccessDelegate(OnAuthenticateSuccessful));
                else
                    Invoke(new FailedDelegate(OnAuthenticateFailed));
            }
            catch (Exception ex)
            {
                Invoke(new FailedDelegate(OnAuthenticateFailed), new[] { ex });
            }
        }

        private void OnAuthenticateSuccessful()
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OnAuthenticateFailed()
        {
            MessageBox.Show("Authentication failed",
                "Authentication failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            lblWait.Hide();

            Program.ImapClient.Disconnect();

            if (_authMode == AuthMode.Simple)
            {
                pnlLogin.Show();
                //picGMailLogin.Show();
                //picOutlook.Show();
                //picYahoo.Show();
                return;
            }

            //btnDefaultAuth.Show();

            if (_authMode == AuthMode.Google)
                picGMailLogin_Click(null, null);
            else if (_authMode == AuthMode.Outlook)
                picOutlook_Click(null, null);
            else if (_authMode == AuthMode.Yahoo)
                picYahoo_Click(null, null);
        }

        private void OnConnectSuccessful()
        {
            if (KullaniciVar == true)
            {
                lblWait.Text = "Bağlandı. Kimlik doğrulanıyor ...";

                if (_authMode == AuthMode.Simple)
                    Program.ImapClient.Credentials = new PlainCredentials(Emailx, Sifre);

                (new Thread(Authenticate)).Start();
            }
            else
            {
                lblWait.Text = "Bağlandı. Kimlik doğrulanıyor ...";

                if (_authMode == AuthMode.Simple)
                    Program.ImapClient.Credentials = new PlainCredentials(txtLogin.Text, txtPassword.Text);

                (new Thread(Authenticate)).Start();
            }
          
        }

        private void OnConnectFailed()
        {
            MessageBox.Show("Connection failed",
                "Connection failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            lblWait.Hide();

            if (_authMode == AuthMode.Simple)
            {
                pnlLogin.Show();
               // picGMailLogin.Show();
                //picOutlook.Show();
                //picYahoo.Show();
                return;
            }

            //btnDefaultAuth.Show();

            if (_authMode == AuthMode.Google)
                picGMailLogin_Click(null, null);
            else if (_authMode == AuthMode.Outlook)
                picOutlook_Click(null, null);
            else if (_authMode == AuthMode.Yahoo)
                picYahoo_Click(null, null);
        }

        private delegate void FailedDelegate();

        private delegate void SuccessDelegate();

        private void picOutlook_Click(object sender, EventArgs e)
        {
            _authMode = AuthMode.Outlook;

            wbrMain.Show();
            pnlLogin.Hide();
            //btnDefaultAuth.Show();
            //picGMailLogin.Hide();
            //picOutlook.Hide();
            //picYahoo.Hide();
            wbrMain.Navigate(OutlookOAuth2Provider.BuildAuthenticationUri());
        }
        private void cmbPort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSignIn_Click(null, null);
            else
                e.SuppressKeyPress = !(e.KeyValue >= 48 && e.KeyValue <= 57);
        }
        private void cmbEncryption_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkValidateCertificate.Enabled = cmbEncryption.SelectedIndex > 0;
            if (cmbEncryption.SelectedIndex > 1) return;
            //cmbPort.SelectedIndex = cmbEncryption.SelectedIndex;
        }
        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSignIn_Click(null, null);
        }
        private void picYahoo_Click(object sender, EventArgs e)
        {
            _authMode = AuthMode.Yahoo;

            wbrMain.Show();
            pnlLogin.Hide();
            //btnDefaultAuth.Show();
            //picGMailLogin.Hide();
            pnlLogin.Hide();
            //picOutlook.Hide();
            //picYahoo.Hide();
            //wbrMain.Navigate(YahooOAuth2Provider.BuildAuthenticationUri());
        }
        public static string AdiSoyadi;
        public static string Server;
        public static int Port;
        public static bool SslVarYok;
        public static string MailAdres;
        public static string Parola;
          
        bool BDegeri = false;
       

        private void btnSignIn_Click(object sender, EventArgs e)
        {
       

            SunucuBosOlamaz SBO = new SunucuBosOlamaz();
            bool kontrol = internetKontol();
            if (kontrol == true)
            {
                if (string.IsNullOrEmpty(TxtSunucu.Text))
                {

                    SBO.label1.Text = "SUNUCU BOŞ BIRAKILAMAZ";
                    SBO.ShowDialog();

                }
                else if (string.IsNullOrEmpty(TxtPort.Text))

                {
                    SBO.label1.Text = "PORT BOŞ BIRAKILAMAZ";
                    SBO.ShowDialog();
                } 

                else if (string.IsNullOrEmpty(txtLogin.Text))
                {
                    SBO.label1.Text = "MAİL ADRESİ BOŞ BIRAKILAMAZ";
                    SBO.ShowDialog();
                }
                else if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    SBO.label1.Text = "PAROLA BOŞ BIRAKILAMAZ";
                    SBO.ShowDialog();
                }
                else
                {
                    pnlLogin.Hide();
                    lblWait.Text = "Bağlanıyor...";
                    lblWait.Show();

                    AdiSoyadi = TxtAdiSoyadi.Text;
                    Server = TxtSunucu.Text;
                    Port =Convert.ToInt32(TxtPort.Text);
                    if (cmbEncryption.SelectedIndex == 0)
                    {
                        BDegeri = false;
                    }
                    else if (cmbEncryption.SelectedIndex == 1)
                    {
                        BDegeri = true;
                    }
                    SslVarYok = BDegeri;
                    MailAdres = txtLogin.Text;
                    Parola = txtPassword.Text;

                   MailBoxs.Pop3Send();
                   MessageFrom.FMailGonderimiiptal sdffs = new MessageFrom.FMailGonderimiiptal();
                    sdffs.ShowDialog();
                    if (KullaniciVar == true)
                    { }
                    else
                    {
                        InitClient();
                        (new Thread(Connect)).Start(false);
                    }






                }
            }
            else
            {
                SBO.label1.Text = "İNTERNET BAĞLANTINIZI KONTROL EDİN";
            }
        }





         
        private void AddUsers_Load(object sender, EventArgs e)
        {
            KullanciVArYok();


            RenkleriDuzenle();


        }

        private void RenkleriDuzenle()
        {
            btnSignIn.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb());;
            button1.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            label1.ForeColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool internetKontol()
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
       
        
        
    }
}