using MailboxClient.Settings;
using MailboxClient.Settings.MailBox;
using Mono.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailboxClient
{
    public partial class FrmMain : Form
    {
        //NotifyIcon MyIcon = new NotifyIcon();
        NotifyIcon MyIcon = new NotifyIcon();
        //private readonly Dictionary<long, ListViewItem> _messageItems;
        private TreeNode _lastClickedNode;
        private List<MailboxClient.Settings.MailBox.Message> _messages;
        private readonly List<ListViewItem> _messageItems;
        public static readonly object lockobject = new object();
        private Folder _selectedFolder;
        private MailboxClient.Settings.MailBox.Message _selectedMessage;
        bool MukerereDurum;
        public static string FolderName;
        public static string Deneme;
        bool kontrol = MailBoxs.InternetKontrol();
        public FrmMain()
        {
            InitializeComponent();
            //_messageItems = new Dictionary<long, ListViewItem>();
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (WidgetShow.WidgetCalisti == true)
            {
                WidgetShow.WidgetCalisti = false;
                BindFolders();
                ConfigureClient();  
            }
            else
            {
                WidgetShow.WidgetCalisti = false;
                CheckForIllegalCrossThreadCalls = false;
                using (var frmStart = new AddUsers())
                {
                    if (frmStart.ShowDialog() == DialogResult.OK)
                    {
                        lblOnemsiz.Navigate("about:blank");
                        BindFolders();
                        ConfigureClient();
                        MailBoxs.MaxsimizedOrMinimized();
                        MailBoxs.SettingsForPitbull();
                        if (MailBoxs.SimgeDurumunda2 == true)
                        {
                            FrmMain MainForm = new FrmMain();
                        }
                        else
                        {
                            this.WindowState = FormWindowState.Normal;
                        }
                        timer1.Enabled = true;
                        RenkleriDuzenle();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }

            AutoComplate();

        }
        private void RenkleriDuzenle()
        {
            //pnlFolders.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            //trwFolders.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            //pnlLoading.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            //panel4.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            //pgbMessages.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            //panel13.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            //BtnYeniMesaj.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            //BtnMini.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            //BtnAra.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;

            //BtnMinimized.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            //BtnMaxsimized.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            //BrnClose.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            //pnlLoading.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;

            //lblFolder.ForeColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;

            //pgbMessages.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;

            //panel14.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb());

            //lblSubject.ForeColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;

            //lblDate.ForeColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            //lblFrom.ForeColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            //BtnWidget.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;



        }
        void MyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            MyIcon.Visible = false;
        }
        private void BtnYeniMesaj_MouseHover(object sender, EventArgs e)
        {
            BtnYeniMesaj.Width = 150;
            BtnYeniMesaj.ImageAlign = ContentAlignment.MiddleLeft;
            BtnYeniMesaj.Text = "Oluştur";
            BtnAra.Location = new Point(175, 5);
            TxtAra.Location = new Point(250, 10);
        }
        private void BtnYeniMesaj_MouseLeave(object sender, EventArgs e)
        {
            BtnYeniMesaj.Width = 39;
            BtnYeniMesaj.ImageAlign = ContentAlignment.MiddleLeft;
            BtnYeniMesaj.Text = "";
            BtnAra.Location = new Point(55, 5);
            TxtAra.Location = new Point(100, 10);
        }
        private void BtnAra_MouseHover(object sender, EventArgs e)
        {
            TxtAra.Visible = true;
        }
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {}
        private void BrnClose_Click(object sender, EventArgs e)
        {
          Application.Exit();
        }
        private void BtnMaxsimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            BtnMinimized.Visible = true;
            BtnMaxsimized.Visible = false;
        }
        private void BtnMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            BtnMaxsimized.Visible = true;
            BtnMinimized.Visible = false;
        }
        private void BtnMini_Click(object sender, EventArgs e)
        {
          
            DialogResult cikis = new DialogResult();
            cikis = MessageBox.Show("Program widget Durumuna getirilsin mi", "Uyarı", MessageBoxButtons.YesNo);
            if (cikis == DialogResult.Yes)
            {
            
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
                
            }
        }
        private void pBoxLogo_Click(object sender, EventArgs e)
        {
            MenuMain.Show();
        }
        private void ToolCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void ToolAyarlar_Click(object sender, EventArgs e)
        {
            SettingsForm FSettings = new SettingsForm();
            FSettings.ShowDialog();
        }

        private void trwFolders_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            _lastClickedNode = e.Node;
            if (e.Node.Tag is Folder && e.Button == MouseButtons.Right)
            {
                mnuFolder.Show(trwFolders, e.Location);
            }
        }
        private void mnuMessage_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = lsvMessages.SelectedIndices.Count == 0;
            if (e.Cancel) return;
            seenToolStripMenuItem.Checked = _selectedMessage.Seen;
        }

        private void mnuAttachment_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = lsvAttachments.SelectedIndices.Count == 0;
            if (e.Cancel) return;
            downloadToolStripMenuItem.Visible =
            !_selectedMessage.Attachments[lsvAttachments.SelectedIndices[0]].Downloaded;
            openToolStripMenuItem.Visible =
            saveAsToolStripMenuItem.Visible =
                _selectedMessage.Attachments[lsvAttachments.SelectedIndices[0]].Downloaded;
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lsvAttachments.Enabled = lsvMessages.Enabled = trwFolders.Enabled = false;
            int index = lsvAttachments.SelectedItems[0].Index;
            Settings.MailBox.Attachment item = _selectedMessage.Attachments[index];
            if (!item.Downloaded)
                (new Thread(_ => DownloadAttachment(index))).Start();
        }
        private void DownloadAttachment(int index)
        {
            try
            {
                var args = new ServerCallCompletedEventArgs { Arg = index };

                _selectedMessage.Attachments[index].Download();

                Invoke(new EventHandler<ServerCallCompletedEventArgs>(DownloadAttachmentCompleted), Program.ImapClient,
                    args);
            }
            catch (Exception ex)
            {
                var args = new ServerCallCompletedEventArgs(false, ex);
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(DownloadAttachmentCompleted), Program.ImapClient,
                    args);
            }
        }
        private void DownloadAttachmentCompleted(object sender, ServerCallCompletedEventArgs e)
        {

            lsvAttachments.Enabled = lsvMessages.Enabled = trwFolders.Enabled = true;
            if (e.Result)
            {
                var index = (int)e.Arg;
                Settings.MailBox.Attachment file = _selectedMessage.Attachments[index];
                lsvAttachments.Items[index].Text = file.FileName + " (" + FormatFileSize(file.FileSize) + ")";
            }
            else
            {

                MessageFrom.AUsers.SunucuBosOlamaz SBO = new MessageFrom.AUsers.SunucuBosOlamaz();

                SBO.label1.Text = "İleti dışa aktarılamadı";
                SBO.ShowDialog();
            }

        }
        private string FormatFileSize(long bytes)
        {
            if (bytes < 1024)
                return bytes + " B";

            bytes = bytes / 1024;

            if (bytes < 1024)
                return bytes + " KB";

            bytes = bytes / 1024;


            return bytes + " MB";
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = lsvAttachments.SelectedItems[0].Index;
            Settings.MailBox.Attachment item = _selectedMessage.Attachments[index];

            string tmpDir = Path.Combine(Application.StartupPath, "tmp");
            string msgTmpDir = Path.Combine(tmpDir, _selectedMessage.UId.ToString(CultureInfo.InvariantCulture));

            string path = Path.Combine(msgTmpDir, item.FileName);
            try
            {
                if (!File.Exists(path) || (new FileInfo(path)).Length == 0)
                    File.WriteAllBytes(path, item.FileData);
            }
            catch
            {
                MessageFrom.AUsers.SunucuBosOlamaz SBO = new MessageFrom.AUsers.SunucuBosOlamaz();

                SBO.label1.Text = "Veri yazılamadı, dosya kullanılıyor olabilir";
                SBO.ShowDialog();
            }
            Process.Start(path);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdSaveAttachment.ShowDialog() != DialogResult.OK) return;
            int index = lsvAttachments.SelectedItems[0].Index;
            Settings.MailBox.Attachment item = _selectedMessage.Attachments[index];

            string path = Path.Combine(Path.GetTempPath(), item.FileName);

            if (!File.Exists(path))
                File.WriteAllBytes(sfdSaveAttachment.FileName, item.FileData);
            else
                File.Copy(path, sfdSaveAttachment.FileName);

            MessageFrom.AUsers.SunucuBosOlamaz SBO = new MessageFrom.AUsers.SunucuBosOlamaz();

            SBO.label1.Text = "Dosya kaydedildi!";
            SBO.ShowDialog();
        }
        private void ConfigureClient()
        {
            Program.ImapClient.Behavior.AutoDownloadBodyOnAccess = false;
            Program.ImapClient.Behavior.AutoPopulateFolderMessages = false;
            Program.ImapClient.Behavior.ExamineFolders = false;
            Program.ImapClient.Behavior.MessageFetchMode = MessageFetchMode.Tiny | MessageFetchMode.GMailLabels;
            Program.ImapClient.Behavior.RequestedHeaders = new[]
            {
                MessageHeader.MessageId,
                MessageHeader.From,
                MessageHeader.Date,
                MessageHeader.Subject,
                MessageHeader.ContentType,
                MessageHeader.Importance
            };
            Program.ImapClient.OnIdleStarted += ImapClient_OnIdleStarted;
            Program.ImapClient.OnIdlePaused += ImapClient_OnIdlePaused;
            Program.ImapClient.OnIdleStopped += ImapClient_OnIdleStopped;
        }
        void ImapClient_OnIdleStopped(object sender, IdleEventArgs e)
        {
            if (InvokeRequired)
                Invoke(new EventHandler<IdleEventArgs>(ImapClient_OnIdleStopped), new[] { sender, e });
            else
            {
                lblIdle.Text = "Durduruldu";
                lblIdle.BackColor = Color.Gray;
                lblIdle.ForeColor = Color.GhostWhite;
            }
        }
        void ImapClient_OnIdlePaused(object sender, IdleEventArgs e)
        {
            if (InvokeRequired)
                Invoke(new EventHandler<IdleEventArgs>(ImapClient_OnIdlePaused), new[] { sender, e });
            else
            {
                lblIdle.Text = "Durduruldu";
                lblIdle.BackColor = Color.Orange;
                lblIdle.ForeColor = Color.Black;
            }
        }
        void ImapClient_OnIdleStarted(object sender, IdleEventArgs e)
        {
            if (InvokeRequired)
                Invoke(new EventHandler<IdleEventArgs>(ImapClient_OnIdleStarted), new[] { sender, e });
            else
            {
                lblIdle.Text = "Bağlandı";
                lblIdle.BackColor = Color.Green;
                lblIdle.ForeColor = Color.White;
            }
        }
        private void UpdateAttachmentIcons(IEnumerable<string> files)
        {
            foreach (Image image in istAttachments.Images)
                image.Dispose();
            istAttachments.Images.Clear();

            foreach (string file in files)
            {
                string key = file.Split('.').Last();
                if (!istAttachments.Images.ContainsKey(key))
                    try
                    {
                        istAttachments.Images.Add(file.Split('.').Last(), NativeMethods.GetSystemIcon(file));
                    }
                    catch
                    {
                    }
            }
        }
       
        private TreeNode FolderToNode(Folder folder)
        {
          

                var node = new TreeNode(folder.Name)
                { Name = folder.Path };
                FolderName = folder.Name.ToString();
                Mukerrer();

                if (node.Name == "INBOX")
                {
                    node.ImageIndex = 5;
                    node.SelectedImageIndex = 5;
                    //node.Tag = "GELEN KUTUSU";
                }
                else if (node.Name == "Deleted Items")
                {
                    node.ImageIndex = 2;
                    node.SelectedImageIndex = 2;
                }
                else if (node.Name == "Drafts")
                {
                    node.ImageIndex = 11;
                    node.SelectedImageIndex = 11;
                }
                else if (node.Name == "Junk E-mail")
                {
                    node.ImageIndex = 6;
                    node.SelectedImageIndex = 6;
                }
                else if (node.Name == "Notes")
                {
                    node.ImageIndex = 14;
                    node.SelectedImageIndex = 14;
                }
                else if (node.Name == "Sent Items")
                {
                    node.ImageIndex = 8;
                    node.SelectedImageIndex = 8;
                }
                if (MukerereDurum == false)
                {

                }
                else
                {
                    MailBoxs.FolderNames();
                    using (SQLiteConnection dbConn = MailBoxs.Conneciton)
                    {
                        dbConn.Open();
                        string tur = "INSERT INTO TBDosya(AccountId,Name,HEXNAME,UidValidity) VALUES (@AccountId,@Name,@HEXNAME,@UidValidity)";
                        SQLiteCommand SqlCmm = new SQLiteCommand(tur, dbConn);
                        SqlCmm.Parameters.AddWithValue("@AccountId", AddUsers.IDUser);
                        SqlCmm.Parameters.AddWithValue("@Name", folder.Name);
                        SqlCmm.Parameters.AddWithValue("@HEXNAME", Deneme);
                        SqlCmm.Parameters.AddWithValue("@UidValidity", folder.UidValidity);
                        SqlCmm.ExecuteNonQuery();
                        dbConn.Clone();
                    }
                }

                node.Nodes.AddRange(folder.SubFolders.Select(FolderToNode).ToArray());
                node.Tag = folder;
                return node;
           
        }
        void Mukerrer()
        {
            using (SQLiteConnection dbConn = MailBoxs.Conneciton)
            {
                dbConn.Open();
                SQLiteCommand SLite = new SQLiteCommand("SELECT * FROM TBDosya WHERE Name=@Name and AccountId='" + AddUsers.IDUser + "' ", dbConn);
                SLite.Parameters.AddWithValue("@Name", FolderName);
                SQLiteDataReader SLRead = SLite.ExecuteReader();
                if (SLRead.Read())
                {
                    MukerereDurum = false;
                }
                else
                {
                    MukerereDurum = true;
                }
                SLRead.Close();
                dbConn.Clone();
            }
        }
        private void AutoResizeMessageListViewColumn()
        {
            ScrollBars scrollBars = NativeMethods.GetVisibleScrollbars(lsvMessages);
          
        }
       
        private void BindFolders()
        { 
            trwFolders.Nodes.Add(Program.ImapClient.Host);
            trwFolders.Nodes[0].Nodes.AddRange(Program.ImapClient.Folders.Select(FolderToNode).ToArray());
            trwFolders.Nodes[0].Expand();
        }
        private void trwFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trwFolders.SelectedNode == null || trwFolders.SelectedNode == trwFolders.Nodes[0])
            {
                //e.Node.Text = "INBOX";
                return;
            }
            else
            {
                timer1.Enabled = false;
                timer1.Stop();
                var folder = trwFolders.SelectedNode.Tag as Folder;
                if (_selectedFolder != folder)
                    SelectFolder(folder);
            }
        }
        private void SelectFolder(Folder folder)
        {
            if (_selectedFolder != null)
                _selectedFolder.OnNewMessagesArrived -= _selectedFolder_OnNewMessagesArrived;

            _selectedFolder = folder;
            _selectedFolder.OnNewMessagesArrived += _selectedFolder_OnNewMessagesArrived;
            //SetFavoriteSelection(folder);

            TreeNode node = trwFolders.Nodes.Find(folder.Path, true).FirstOrDefault();
            if (node != null)
                trwFolders.SelectedNode = node;
        
            lsvMessages.VirtualListSize = 0;
            VirutialSize = 0;
            pnlLoading.Show();
            pnlMessages.Hide();
            _selectedMessage = null;
            lsvMessages.Items.Clear();

            lblFolder.Text = _selectedFolder.Name;

            (new Thread(GetMails)).Start();
        }
        int VirutialSize;
        void _selectedFolder_OnNewMessagesArrived(object sender, IdleEventArgs e)
        {
            if (InvokeRequired)
                Invoke(new EventHandler<IdleEventArgs>(_selectedFolder_OnNewMessagesArrived), new[] { sender, e });
            else
            {
                _messages.InsertRange(0, e.Messages);
                lsvMessages.VirtualListSize = _messages.Count;
                lsvMessages.Invalidate();
              
                int count = _messages.Count(_ => !_.Seen);
                trwFolders.SelectedNode.Text = _selectedFolder.Name + (count == 0 ? "" : string.Format(" ({0})", count));

                AutoResizeMessageListViewColumn();

            }
            //Cek();
        }
       

        private void trwFolders_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            var folder = e.Node.Tag as Folder;
            if (folder == null || !folder.Selectable)
                e.Cancel = true;
            if (trwFolders.SelectedNode == null) return;
        }
        private void GetMails()
        {
           
            try
            {
                lock (lockobject)
                {
                    _messages = _selectedFolder.Search().OrderByDescending(_ => _.Date).ToList();
                    saveMail();
                    _selectedFolder.StartIdling();
                    var args = new ServerCallCompletedEventArgs();
                    Invoke(new EventHandler<ServerCallCompletedEventArgs>(GetMailsCompleted), Program.ImapClient, args);
                }
            }
            catch (Exception ex)
            {
                var args = new ServerCallCompletedEventArgs(false, ex);
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(GetMailsCompleted), Program.ImapClient, args);
            }
           
        }
        private void GetMailsCompleted(object sender, ServerCallCompletedEventArgs e)
        {
            if (e.Result)
            {
                lsvMessages.VirtualListSize = _messages.Count;
                
                lsvMessages.Invalidate();
                int count = _messages.Count(_ => !_.Seen);
                trwFolders.SelectedNode.Text = _selectedFolder.Name + (count == 0 ? "" : string.Format(" ({0})", count));

                AutoResizeMessageListViewColumn();
            }
            else
            {
                using (var frm = new FrmError(e.Exception))
                    frm.ShowDialog();
            }
            pnlLoading.Hide();
            pnlMessages.Show();
            trwFolders.Enabled =
                lsvMessages.Enabled =
                    pnlFavorites.Enabled = true;

         
                //Cek();
            
        }

        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            //AutoResizeMessageListViewColumn();
        }
        private void FrmMainOrLsvMails_SizeChanged(object sender, EventArgs e)
        {

            AutoResizeMessageListViewColumn();
        }
        private void saveMail()
        {
            MailboxClient.Settings.MailBox.Message msg = _messages[50];

            ListViewItem item = new ListViewItem(string.IsNullOrEmpty(msg.From.DisplayName.ToString())
                   ? "(Konu yok)"
                   : msg.From.DisplayName.ToString())

            {
                //ForeColor = color,
                ImageIndex = 0,
                Checked = true
            };
            item.Font = new Font(item.Font, msg.Seen ? FontStyle.Regular : FontStyle.Bold);

            item.SubItems.Add(msg.Subject.ToString());
            item.SubItems.Add(msg.Date.ToString());
            //item.SubItems.Add(msg.Folder.Name.ToString());

            //e.Item = item;

            Date = msg.Date.ToString();
            Subje = msg.Subject.ToString();
            gMail = msg.From.Address.ToString();

            Mukerrer2();
            Mukerrer3();
            //msg.

            //for (int i=0; i == _messages.Count; )
            //{


            if (MukerereDurum2 == false)
            {


            }
            else
            {
                using (SQLiteConnection dbConn = MailBoxs.Conneciton)
                {
                    dbConn.Open();
                    string tur = "INSERT INTO TBGelenKutusu(IDUser,FolderName,FolderUiVality,FolderUidNext,FromMailAdress,FromDisplayName,Day,Subject) VALUES (@IDUser,@FolderName,@FolderUiVality,@FolderUidNext,@FromMailAdress,@FromDisplayName,@Day,@Subject)";
                    SQLiteCommand SqlCmm = new SQLiteCommand(tur, dbConn);
                    SqlCmm.Parameters.AddWithValue("@IDUser", MailBoxs.UserUid);
                    SqlCmm.Parameters.AddWithValue("@FolderName", msg.Folder.Name.ToString());
                    SqlCmm.Parameters.AddWithValue("@FolderUiVality", msg.Folder.UidValidity.ToString());
                    SqlCmm.Parameters.AddWithValue("@FolderUidNext", msg.Folder.UidNext.ToString());

                    SqlCmm.Parameters.AddWithValue("@FromMailAdress", msg.From.Address.ToString());
                    SqlCmm.Parameters.AddWithValue("@FromDisplayName", msg.From.DisplayName.ToString());
                    SqlCmm.Parameters.AddWithValue("@Day", msg.Date.ToString());
                    SqlCmm.Parameters.AddWithValue("@Subject", msg.Subject.ToString());
                    SqlCmm.ExecuteNonQuery();

                    dbConn.Close();
                }
                ListViewsyazdir();
            }
            if (MukerereDurum3 == false)
            {

            }
            else
            {
                using (SQLiteConnection dbConn = MailBoxs.Conneciton)
                {
                    dbConn.Open();
                    string tur = "INSERT INTO TBAttachment(FromEmailAdress,FromDisplayName,UserID) VALUES (@FromEmailAdress,@FromDisplayName,@UserID)";
                    SQLiteCommand SqlCmm = new SQLiteCommand(tur, dbConn);
                    SqlCmm.Parameters.AddWithValue("@FromEmailAdress", msg.From.Address.ToString());
                    SqlCmm.Parameters.AddWithValue("@FromDisplayName", msg.From.DisplayName.ToString());
                    SqlCmm.Parameters.AddWithValue("@UserID", MailBoxs.UserUid);
                    SqlCmm.ExecuteNonQuery();
                    dbConn.Close();
                }
            }

        }
        string Date;
        string Subje;
        string gMail;

        Task DegerDondurmeyenAsyncMethod()
        {
            return Task.Run(() => {
                while (true)
                {

                    MailboxClient.Settings.MailBox.Message msg = _messages;
                    using (SQLiteConnection dbConn = MailBoxs.Conneciton)
                    {
                        dbConn.Open();
                        string tur = "INSERT INTO TBGelenKutusu(IDUser,FolderName,FolderUiVality,FolderUidNext,FromMailAdress,FromDisplayName,Day,Subject) VALUES (@IDUser,@FolderName,@FolderUiVality,@FolderUidNext,@FromMailAdress,@FromDisplayName,@Day,@Subject)";
                        SQLiteCommand SqlCmm = new SQLiteCommand(tur, dbConn);
                        SqlCmm.Parameters.AddWithValue("@IDUser", MailBoxs.UserUid);
                        SqlCmm.Parameters.AddWithValue("@FolderName", msg.Folder.Name.ToString());
                        SqlCmm.Parameters.AddWithValue("@FolderUiVality", msg.Folder.UidValidity.ToString());
                        SqlCmm.Parameters.AddWithValue("@FolderUidNext", msg.Folder.UidNext.ToString());

                        SqlCmm.Parameters.AddWithValue("@FromMailAdress", msg.From.Address.ToString());
                        SqlCmm.Parameters.AddWithValue("@FromDisplayName", msg.From.DisplayName.ToString());
                        SqlCmm.Parameters.AddWithValue("@Day", msg.Date.ToString());
                        SqlCmm.Parameters.AddWithValue("@Subject", msg.Subject.ToString());
                        SqlCmm.ExecuteNonQuery();

                        dbConn.Close();
                    }
                }
            });
        }
        private void lsvMessages_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
           
            if (e.ItemIndex >= _messages.Count) return;
            MailboxClient.Settings.MailBox.Message msg = _messages[e.ItemIndex];
          
            Color color = Color.Black;

            switch (msg.Importance)
            {
                case MessageImportance.High:
                    color = Color.Red;
                    break;
                case MessageImportance.Low:
                    color = Color.Gray;
                    break;
                case MessageImportance.Medium:
                    color = Color.Orange;
                    break;
            }
            ListViewItem item = new ListViewItem(string.IsNullOrEmpty(msg.From.DisplayName.ToString())
                    ? "(Konu yok)"
                    : msg.From.DisplayName.ToString())
           
                {
                    //ForeColor = color,
                    ImageIndex = 0 ,
                    Checked=true
                };
                item.Font = new Font(item.Font, msg.Seen ? FontStyle.Regular : FontStyle.Bold);
           
            item.SubItems.Add(msg.Subject.ToString());
            item.SubItems.Add(msg.Date.ToString());
            //item.SubItems.Add(msg.Folder.Name.ToString());

            e.Item = item;

            Date = msg.Date.ToString();
            Subje = msg.Subject.ToString();
            gMail = msg.From.Address.ToString();

            Mukerrer2();
            Mukerrer3();
            //msg.

            //for (int i=0; i == _messages.Count; )
            //{
          

            if (MukerereDurum2 == false)
            {
                

            }
            else
            {
                using (SQLiteConnection dbConn = MailBoxs.Conneciton)
                {
                    dbConn.Open();
                    string tur = "INSERT INTO TBGelenKutusu(IDUser,FolderName,FolderUiVality,FolderUidNext,FromMailAdress,FromDisplayName,Day,Subject) VALUES (@IDUser,@FolderName,@FolderUiVality,@FolderUidNext,@FromMailAdress,@FromDisplayName,@Day,@Subject)";
                    SQLiteCommand SqlCmm = new SQLiteCommand(tur, dbConn);
                    SqlCmm.Parameters.AddWithValue("@IDUser", MailBoxs.UserUid);
                    SqlCmm.Parameters.AddWithValue("@FolderName", msg.Folder.Name.ToString());
                    SqlCmm.Parameters.AddWithValue("@FolderUiVality", msg.Folder.UidValidity.ToString());
                    SqlCmm.Parameters.AddWithValue("@FolderUidNext", msg.Folder.UidNext.ToString());

                    SqlCmm.Parameters.AddWithValue("@FromMailAdress", msg.From.Address.ToString());
                    SqlCmm.Parameters.AddWithValue("@FromDisplayName", msg.From.DisplayName.ToString());
                    SqlCmm.Parameters.AddWithValue("@Day", msg.Date.ToString());
                    SqlCmm.Parameters.AddWithValue("@Subject", msg.Subject.ToString());
                    SqlCmm.ExecuteNonQuery();

                    dbConn.Close();
                }
                ListViewsyazdir();
            }
            if (MukerereDurum3 == false)
            {

            }
            else
            {
                using (SQLiteConnection dbConn = MailBoxs.Conneciton)
                {
                    dbConn.Open();
                    string tur = "INSERT INTO TBAttachment(FromEmailAdress,FromDisplayName,UserID) VALUES (@FromEmailAdress,@FromDisplayName,@UserID)";
                    SQLiteCommand SqlCmm = new SQLiteCommand(tur, dbConn);
                    SqlCmm.Parameters.AddWithValue("@FromEmailAdress", msg.From.Address.ToString());
                    SqlCmm.Parameters.AddWithValue("@FromDisplayName", msg.From.DisplayName.ToString());
                    SqlCmm.Parameters.AddWithValue("@UserID", MailBoxs.UserUid);
                    SqlCmm.ExecuteNonQuery();
                    dbConn.Close();
                }
            }


            //    i++;
            //}
            //}
        }
        bool MukerereDurum2;
        bool MukerereDurum3;
        void Mukerrer2()
        {
            using (SQLiteConnection dbConn = MailBoxs.Conneciton)
            {
                dbConn.Open();
                SQLiteCommand SLite = new SQLiteCommand("SELECT * FROM TBGelenKutusu WHERE Subject=@Subject and Day=@Day and IDUser='" + MailBoxs.UserUid + "' ", dbConn);
                SLite.Parameters.AddWithValue("@Day", Date);
                SLite.Parameters.AddWithValue("@Subject", Subje);
                SQLiteDataReader SLRead = SLite.ExecuteReader();
                if (SLRead.Read())
                {
                    MukerereDurum2 = false;
                }
                else
                {
                    MukerereDurum2 = true;
                }
                SLRead.Close();
                dbConn.Clone();
            }
        }
        void Mukerrer3()
        {
            using (SQLiteConnection dbConn = MailBoxs.Conneciton)
            {
                dbConn.Open();
                SQLiteCommand SLite = new SQLiteCommand("SELECT * FROM TBAttachment WHERE FromEmailAdress=@FromEmailAdress  and UserID='" + MailBoxs.UserUid + "' ", dbConn);
                SLite.Parameters.AddWithValue("@FromEmailAdress", gMail);
                
                SQLiteDataReader SLRead = SLite.ExecuteReader();
                if (SLRead.Read())
                {
                    MukerereDurum3 = false;
                }
                else
                {
                    MukerereDurum3 = true;
                }
                SLRead.Close();
                dbConn.Clone();
            }
        }
        private void lsvMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvMessages.SelectedIndices.Count == 0)
            {

                return;

            }
            else
            {


                _selectedMessage = _messages[lsvMessages.SelectedIndices[0]];

                lblDate.Text = _selectedMessage.Date.HasValue
                    ? _selectedMessage.Date.Value.ToString("dd MMM yyyy")
                    : "Bilinmeyen tarih";

                lblFrom.Text = "From: " + (_selectedMessage.From == null && _selectedMessage.Sender == null
                    ? "Bilinmeyen gönderen"
                    : (_selectedMessage.From ?? _selectedMessage.Sender).ToString());

                lblSubject.Text = string.IsNullOrEmpty(_selectedMessage.Subject)
                    ? "( Konu yok )"
                    : _selectedMessage.Subject;

                //pnlInfo.Show();
                lsvAttachments.Items.Clear();
                pnlAttachments.Visible = _selectedMessage.Attachments.Any();

                if (_selectedMessage.Attachments.Any())
                {
                    string tmpDir = Path.Combine(Application.StartupPath, "tmp");
                    string msgTmpDir = Path.Combine(tmpDir, _selectedMessage.UId.ToString(CultureInfo.InvariantCulture));

                    if (!Directory.Exists(msgTmpDir))
                        Directory.CreateDirectory(msgTmpDir);

                    var files = new List<string>();

                    foreach (Settings.MailBox.Attachment file in _selectedMessage.Attachments)
                    {
                        string path = Path.Combine(msgTmpDir, file.FileName);
                        if (!File.Exists(path))
                            File.Create(path);

                        files.Add(path);
                    }

                    UpdateAttachmentIcons(files);

                    lsvAttachments.Items.AddRange(_selectedMessage.Attachments.Select(file =>
                        new ListViewItem(string.Format("{0} ({1})", file.FileName,
                            file.Downloaded ? FormatFileSize(file.FileSize) : "..."))
                        {
                            ImageKey = file.FileName.Split('.').Last()
                        }).ToArray());
                }

                pnlView.Hide();
                lblFailedDownloadBody.Hide();
                lblDownloadingBody.Show();
                pnlDownloadingBody.Hide();

                if (_selectedMessage.Body == null)
                {
                    MessageFrom.AUsers.SunucuBosOlamaz SBO = new MessageFrom.AUsers.SunucuBosOlamaz();
                    SBO.label1.Text = "Mesaj vücut içermiyor.Bir şeyler yanlış gitti";
                    SBO.ShowDialog();
                }
                if (_selectedMessage.Body.Downloaded == BodyType.None)
                {
                    pnlDownloadingBody.Show();
                    trwFolders.Enabled = lsvMessages.Enabled = false;
                    (new Thread(DownloadBody)).Start();
                }
                else
                    DownloadBodyCompleted(Program.ImapClient, new ServerCallCompletedEventArgs());
            }
        }
      
           
      
        private void DownloadBody()
        {
            try
            {
                _selectedMessage.Body.Download();
                var args = new ServerCallCompletedEventArgs();
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(DownloadBodyCompleted), Program.ImapClient, args);
            }
            catch (Exception ex)
            {
                var args = new ServerCallCompletedEventArgs(false, ex);
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(DownloadBodyCompleted), Program.ImapClient, args);
            }
        }
        private void DownloadBodyCompleted(object sender, ServerCallCompletedEventArgs e)
        {
            trwFolders.Enabled = lsvMessages.Enabled = true;
            if (e.Result)
            {
                string body = _selectedMessage.Body.HasHtml ? _selectedMessage.Body.Html : _selectedMessage.Body.Text;
                lblOnemsiz.Document.OpenNew(true);
                lblOnemsiz.Document.Write(_selectedMessage.Body.HasHtml ? body : HttpUtility.HtmlEncode(body).Replace(Environment.NewLine, "<br />"));
                if (lblOnemsiz.Document.Body != null)
                    lblOnemsiz.Document.Body.SetAttribute("scroll", "auto");
                pnlDownloadingBody.Hide();
                pnlView.Show();
                if (_selectedMessage.Labels != null && _selectedMessage.Labels.Any())
                {
                    lblLabels.Text = "Labels:" + string.Join(", ", _selectedMessage.Labels.ToArray());
                    lblLabels.Visible = true;
                }
                else
                {
                    lblLabels.Visible = false;
                }

                pnlEmbeddedResources.Visible = _selectedMessage.EmbeddedResources.Any(_ => !_.Downloaded);
            }
            else
            {
                lblDownloadingBody.Hide();
                lblFailedDownloadBody.Show();
            }
        }
        private void DownloadEmbeddedResources()
        {
            try
            {
                foreach (Settings.MailBox.Attachment res in _selectedMessage.EmbeddedResources)
                {
                    if (!res.Downloaded)
                        res.Download();
                }
                var args = new ServerCallCompletedEventArgs();
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(DownloadEmbeddedResourcesCompleted),
                    Program.ImapClient, args);
            }
            catch (Exception ex)
            {
                var args = new ServerCallCompletedEventArgs(false, ex);
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(DownloadEmbeddedResourcesCompleted),
                    Program.ImapClient, args);
            }
        }
        private void DownloadEmbeddedResourcesCompleted(object sender, ServerCallCompletedEventArgs e)
        {
            lnkDownloadEmbeddedResources.Enabled = true;
            if (e.Result)
            {
                pnlEmbeddedResources.Hide();

                string body = _selectedMessage.Body.HasHtml ? _selectedMessage.Body.Html : _selectedMessage.Body.Text;

                string tmpDir = Path.Combine(Application.StartupPath, "tmp");
                string msgTmpDir = Path.Combine(tmpDir, _selectedMessage.UId.ToString(CultureInfo.InvariantCulture));

                if (!Directory.Exists(msgTmpDir))
                    Directory.CreateDirectory(msgTmpDir);

                foreach (Settings.MailBox.Attachment res in _selectedMessage.EmbeddedResources)
                {
                    try
                    {
                        string path = Path.Combine(msgTmpDir,
                            res.FileName);
                        File.WriteAllBytes(path, res.FileData);
                        body = body.Replace("cid:" + res.ContentId, path);
                    }
                    catch (Exception)
                    {
                    }
                }

                lblOnemsiz.Document.OpenNew(true);
                lblOnemsiz.Document.Write(_selectedMessage.Body.HasHtml ? body : body.Replace("\n", "<br />"));
            }
            else
            {
                MessageFrom.AUsers.SunucuBosOlamaz SBO = new MessageFrom.AUsers.SunucuBosOlamaz();
                SBO.label1.Text = " görüntüler indirilemedi";
                SBO.ShowDialog();

            }
        }

        private void lnkDownloadEmbeddedResources_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lnkDownloadEmbeddedResources.Enabled = false;
            (new Thread(DownloadEmbeddedResources)).Start();
        }

        private void addSubfolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var folder = _lastClickedNode.Tag as Folder;

            if (folder == null) return;

            string folderName = InputBox.Show("Alt klasör Oluştur", "Yeni bir klasör adı girin", "", this);

            if (folderName == null) return;

            if (string.IsNullOrEmpty(folderName.Trim()))
            {
                MessageFrom.AUsers.SunucuBosOlamaz SBO = new MessageFrom.AUsers.SunucuBosOlamaz();
                SBO.label1.Text = "Geçerli bir klasör adı gerekli";
                SBO.ShowDialog();
            }
            trwFolders.Enabled = false;

            (new Thread(_ => AddSubFolder(folder, folderName))).Start();

        }
        private void AddSubFolder(Folder folder, string folderName)
        {
            try
            {
                Folder subFolder = folder.SubFolders.Add(folderName);
                var args = new ServerCallCompletedEventArgs { Arg = subFolder };
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(AddSubFolderCompleted), Program.ImapClient, args);
            }
            catch (Exception ex)
            {
                var args = new ServerCallCompletedEventArgs(false, ex);
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(AddSubFolderCompleted), Program.ImapClient, args);
            }
        }
        private void AddSubFolderCompleted(object sender, ServerCallCompletedEventArgs e)
        {
            trwFolders.Enabled = true;
            if (e.Result)
            {
                _lastClickedNode.Nodes.Add(FolderToNode(e.Arg as Folder));
            }
            else
            {
                MessageFrom.AUsers.SunucuBosOlamaz SBO = new MessageFrom.AUsers.SunucuBosOlamaz();
                SBO.label1.Text = "Alt klasör oluşturulamadı";
                SBO.ShowDialog();
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var folder = _lastClickedNode.Tag as Folder;

            if (folder == null) return;

            string newName = InputBox.Show("Dosyayı yeniden adlandır", "Yeni bir klasör adı girin", folder.Name, this);

            if (newName == null) return;

            if (string.IsNullOrEmpty(newName.Trim()))
            {
                MessageFrom.AUsers.SunucuBosOlamaz SBO = new MessageFrom.AUsers.SunucuBosOlamaz();
                SBO.label1.Text = "Dosyayı yeniden adlandır";
                SBO.ShowDialog();

                trwFolders.Enabled = false;
            }
            (new Thread(_ => RenameFolder(folder, newName))).Start();

        }
        private void RenameFolder(Folder folder, string newName)
        {
            try
            {
                folder.Name = newName;
                var args = new ServerCallCompletedEventArgs { Arg = newName };
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(RenameFolderCompleted), Program.ImapClient, args);
            }
            catch (Exception ex)
            {
                var args = new ServerCallCompletedEventArgs(false, ex);
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(RenameFolderCompleted), Program.ImapClient, args);
            }
        }
        private void RenameFolderCompleted(object sender, ServerCallCompletedEventArgs e)
        {
            trwFolders.Enabled = true;
            if (e.Result)
            {
                _lastClickedNode.Text = (e.Arg as string ?? _lastClickedNode.Text);
                var folder = _lastClickedNode.Tag as Folder;

                if (folder == null || folder != _selectedFolder) return;
                lblFolder.Text = _lastClickedNode.Text;
                int count = _messages.Count(_ => !_.Seen);
                trwFolders.SelectedNode.Text = _selectedFolder.Name + (count == 0 ? "" : string.Format(" ({0})", count));
            }
            else
            {

                MessageFrom.AUsers.SunucuBosOlamaz SBO = new MessageFrom.AUsers.SunucuBosOlamaz();
                SBO.label1.Text = "Klasör yeniden adlandırılamadı";
                SBO.ShowDialog();

            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var folder = _lastClickedNode.Tag as Folder;

            if (folder == null)
                return;

            if (
                MessageBox.Show("Gerçekten klasörü silmek istiyor musun \"" + folder.Name + "\"?", "Klasörü kaldır",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            trwFolders.Enabled = false;

            (new Thread(_ => DeleteFolder(folder))).Start();
        }
        private void DeleteFolder(Folder folder)
        {
            try
            {
                var args = new ServerCallCompletedEventArgs { Arg = folder, Result = folder.Remove() };

                Invoke(new EventHandler<ServerCallCompletedEventArgs>(DeleteFolderCompleted), Program.ImapClient, args);
            }
            catch (Exception ex)
            {
                var args = new ServerCallCompletedEventArgs(false, ex);
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(DeleteFolderCompleted), Program.ImapClient, args);
            }
        }
        private void DeleteFolderCompleted(object sender, ServerCallCompletedEventArgs e)
        {
            trwFolders.Enabled = true;
            if (e.Result)
            {
                var folder = e.Arg as Folder;
                //LinkLabel lnk = FindFavoriteLnk(folder);
                //if (lnk != null)
                //    lnk.Hide();

                if (_selectedFolder == folder)
                {
                    lsvMessages.VirtualListSize = 0;
                    VirutialSize = 0;
                    _messages.Clear();
                    trwFolders.SelectedNode = null;

                    //pnlSelectFolder.Show();

                    pnlMessages.Visible =
                            //pnlInfo.Visible =
                            pnlView.Visible =
                                pnlDownloadingBody.Visible = false;
                }

                _lastClickedNode.Remove();
            }
            else
                MessageBox.Show("Failed to delete folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void importMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var folder = _lastClickedNode.Tag as Folder;

            if (folder == null)
                return;
            if (ofdImportMessage.ShowDialog() != DialogResult.OK) return;

            string eml = File.ReadAllText(ofdImportMessage.FileName);

            trwFolders.Enabled = false;

            (new Thread(_ => ImportMessage(folder, eml))).Start();
        }
        private void ImportMessage(Folder folder, string eml)
        {
            try
            {
                var args = new ServerCallCompletedEventArgs { Arg = folder, Result = folder.AppendMessage(eml) };

                Invoke(new EventHandler<ServerCallCompletedEventArgs>(ImportMessageCompleted), Program.ImapClient, args);
            }
            catch (Exception ex)
            {
                var args = new ServerCallCompletedEventArgs(false, ex);
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(ImportMessageCompleted), Program.ImapClient, args);
            }
        }

        private void ImportMessage(Folder folder, string[] paths)
        {
            foreach (var path in paths)
            {
                try
                {
                    var eml = File.ReadAllText(path);
                    var args = new ServerCallCompletedEventArgs { Arg = folder, Result = folder.AppendMessage(eml) };

                    Invoke(new EventHandler<ServerCallCompletedEventArgs>(ImportPartCompleted), Program.ImapClient, args);
                }
                catch (Exception ex)
                {
                    var args = new ServerCallCompletedEventArgs(false, ex);
                    Invoke(new EventHandler<ServerCallCompletedEventArgs>(ImportPartCompleted), Program.ImapClient, args);
                }
            }

            Invoke(new EventHandler<ServerCallCompletedEventArgs>(ImportMessageCompleted), Program.ImapClient, new ServerCallCompletedEventArgs { Arg = folder, Result = true });
        }

        private void ImportMessageCompleted(object sender, ServerCallCompletedEventArgs e)
        {
            trwFolders.Enabled = true;
            if (e.Result)
            {
                MessageBox.Show("Message imported!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
                MessageBox.Show("Failed to import message", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ImportPartCompleted(object sender, ServerCallCompletedEventArgs e)
        {

            if (e.Result)
            {
                Console.WriteLine("Message imported!");
            }
            else
                Console.WriteLine("Failed to import message");
        }

        private void emptyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var folder = _lastClickedNode.Tag as Folder;

            if (folder == null)
                return;

            if (
                MessageBox.Show("Do you really want to empty the folder \"" + folder.Name + "\"?", "Remove folder",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            trwFolders.Enabled = false;

            (new Thread(_ => EmptyFolder(folder))).Start();
        }
        private void EmptyFolder(Folder folder)
        {
            try
            {
                var args = new ServerCallCompletedEventArgs { Arg = folder, Result = folder.EmptyFolder() };

                Invoke(new EventHandler<ServerCallCompletedEventArgs>(EmptyFolderCompleted), Program.ImapClient, args);
            }
            catch (Exception ex)
            {
                var args = new ServerCallCompletedEventArgs(false, ex);
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(EmptyFolderCompleted), Program.ImapClient, args);
            }
        }

        private void EmptyFolderCompleted(object sender, ServerCallCompletedEventArgs e)
        {
            trwFolders.Enabled = true;
            if (e.Result)
            {
                var folder = e.Arg as Folder;

                if (_selectedFolder == folder)
                {
                    lsvMessages.VirtualListSize = 0;
                    VirutialSize = 0;
                    _messages.Clear();
                }
            }
            else
                MessageBox.Show("Failed to empty folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void seenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            trwFolders.Enabled = lsvMessages.Enabled = false;
            (new Thread(_ => ToggleSeen(_selectedMessage))).Start();
        }
        private void ToggleSeen(Settings.MailBox.Message message)
        {
            try
            {
                message.Seen = !message.Seen;
                var args = new ServerCallCompletedEventArgs { Arg = message };
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(ToggleSeenCompleted), Program.ImapClient, args);
            }
            catch (Exception ex)
            {
                var args = new ServerCallCompletedEventArgs(false, ex);
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(ToggleSeenCompleted), Program.ImapClient, args);
            }
        }
        private void ToggleSeenCompleted(object sender, ServerCallCompletedEventArgs e)
        {
            trwFolders.Enabled = lsvMessages.Enabled = true;
            if (e.Result)
            {
                var msg = e.Arg as Settings.MailBox.Message;

                if (msg == null) return;
                seenToolStripMenuItem.Checked = msg.Seen;
                //_messageItems[msg.UId].Font = new Font(_messageItems[msg.UId].Font,
                    //msg.Seen ? FontStyle.Regular : FontStyle.Bold);

                int count = _messages.Count(_ => !_.Seen);
                trwFolders.SelectedNode.Text = _selectedFolder.Name + (count == 0 ? "" : string.Format(" ({0})", count));
            }
            else
                MessageBox.Show("Failed to toggle \\SEEN flag", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void copyToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Folder folder = FolderBox.Show("Mesajı bir klasöre kopyalayın", "Lütfen hedef klasörü seçin", this);
            if (folder == null)
                return;
            trwFolders.Enabled = lsvMessages.Enabled = false;
            (new Thread(_ => CopyMessageToFolder(_selectedMessage, folder))).Start();
        }
        private void CopyMessageToFolder(Settings.MailBox.Message message, Folder folder)
        {
            try
            {
                var args = new ServerCallCompletedEventArgs { Result = message.CopyTo(folder) };
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(CopyMessageToFolderCompleted), Program.ImapClient,
                    args);
            }
            catch (Exception ex)
            {
                var args = new ServerCallCompletedEventArgs(false, ex);
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(CopyMessageToFolderCompleted), Program.ImapClient,
                    args);
            }
        }

        private void CopyMessageToFolderCompleted(object sender, ServerCallCompletedEventArgs e)
        {
            trwFolders.Enabled = lsvMessages.Enabled = true;
            if (e.Result)
                MessageBox.Show("Kopyalanan Mail", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else
                MessageBox.Show("Mesaj kopyalanamadı", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void moveToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Folder folder = FolderBox.Show("İletiyi klasöre taşı", "Lütfen hedef klasörü seçin", this);
            if (folder == null)
                return;
            trwFolders.Enabled = lsvMessages.Enabled = false;
            (new Thread(_ => MoveMessageToFolder(_selectedMessage, folder))).Start();
        }
       

        private void MoveMessageToFolderCompleted(object sender, ServerCallCompletedEventArgs e)
        {
            trwFolders.Enabled = lsvMessages.Enabled = true;
            if (e.Result)
            {
                lsvMessages.VirtualListSize--;
                _messages.Remove(_selectedMessage);
                lsvMessages.SelectedIndices.Clear();
                MessageBox.Show("Message moved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
                MessageBox.Show("Failed to move message", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void RemoveMessage(Settings.MailBox.Message message)
        {
            try
            {
                var args = new ServerCallCompletedEventArgs { Result = message.Remove() };
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(RemoveMessageCompleted), Program.ImapClient, args);
            }
            catch (Exception ex)
            {
                var args = new ServerCallCompletedEventArgs(false, ex);
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(RemoveMessageCompleted), Program.ImapClient, args);
            }
        }

        private void RemoveMessageCompleted(object sender, ServerCallCompletedEventArgs e)
        {
            trwFolders.Enabled = lsvMessages.Enabled = true;
            if (e.Result)
            {
                lsvMessages.VirtualListSize--;
                _messages.Remove(_selectedMessage);
                lsvMessages.SelectedIndices.Clear();
                MessageBox.Show("Message removed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
                MessageBox.Show("Failed to remove message", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void exportMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfdExportMessage.ShowDialog(this) != DialogResult.OK) return;

            trwFolders.Enabled = false;

            (new Thread(_ => ExportMessage(sfdExportMessage.FileName))).Start();
        }
        private void ExportMessage(string path)
        {
            string[] headers = Program.ImapClient.Behavior.RequestedHeaders;
            try
            {
                var args = new ServerCallCompletedEventArgs();
                Program.ImapClient.Behavior.RequestedHeaders = null;

                var data = _selectedMessage.DownloadRawMessage();
                File.WriteAllText(path, data);

                Invoke(new EventHandler<ServerCallCompletedEventArgs>(ExportMessageCompleted), Program.ImapClient, args);
            }
            catch (Exception ex)
            {
                var args = new ServerCallCompletedEventArgs(false, ex);
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(ExportMessageCompleted), Program.ImapClient, args);
            }
            finally
            {
                Program.ImapClient.Behavior.RequestedHeaders = headers;
            }
        }

        private void ExportMessageCompleted(object sender, ServerCallCompletedEventArgs e)
        {
            trwFolders.Enabled = true;
            if (e.Result)
            {
                MessageBox.Show("Message exported!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
                MessageBox.Show("Failed to export message", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void mnuExportAllMessages_Click(object sender, EventArgs e)
        {
            if (fbdExportMessages.ShowDialog(this) != DialogResult.OK) return;

            trwFolders.Enabled = false;

            (new Thread(_ => ExportMessages(fbdExportMessages.SelectedPath))).Start();
        }
        private void ExportMessages(string path)
        {
            string[] headers = Program.ImapClient.Behavior.RequestedHeaders;
            try
            {
                var args = new ServerCallCompletedEventArgs();
                Program.ImapClient.Behavior.RequestedHeaders = null;

                var i = 1;

                foreach (var message in _selectedFolder.Messages.OrderBy(_ => _.InternalDate))
                {
                    var data = message.DownloadRawMessage();
                    File.WriteAllText(Path.Combine(path, i + ".eml"), data);
                    i++;
                }

                Invoke(new EventHandler<ServerCallCompletedEventArgs>(ExportMessagesCompleted), Program.ImapClient, args);
            }
            catch (Exception ex)
            {
                var args = new ServerCallCompletedEventArgs(false, ex);
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(ExportMessagesCompleted), Program.ImapClient, args);
            }
            finally
            {
                Program.ImapClient.Behavior.RequestedHeaders = headers;
            }
        }
        private void ExportMessagesCompleted(object sender, ServerCallCompletedEventArgs e)
        {
            trwFolders.Enabled = true;
            if (e.Result)
            {
                MessageBox.Show("Message exported!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
                MessageBox.Show("Failed to export message", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void bulkImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var folder = _lastClickedNode.Tag as Folder;

            if (folder == null)
                return;
            ofdImportMessage.Multiselect = true;
            if (ofdImportMessage.ShowDialog() != DialogResult.OK) return;
            ofdImportMessage.Multiselect = false;
            trwFolders.Enabled = false;

            (new Thread(_ => ImportMessage(folder, ofdImportMessage.FileNames))).Start();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (
           MessageBox.Show("Do you really want to remove this message?", "Remove folder",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            trwFolders.Enabled = lsvMessages.Enabled = false;
            (new Thread(_ => RemoveMessage(_selectedMessage))).Start();
        }



        private void BtnYenile_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
            TreeNode node = trwFolders.Nodes.Find("INBOX", true).First();
            if (node != null)
                trwFolders.SelectedNode = node;
            pnlView.Visible =
                pnlDownloadingBody.Visible =
                            pnlFavorites.Enabled = false;
            lsvMessages.VirtualListSize = 0;
            VirutialSize = 0;
            pnlLoading.Show();
            pnlMessages.Hide();

            _selectedMessage = null;
            lblFolder.Text = _selectedFolder.Name;

            (new Thread(GetMails)).Start();
        }
        private void SelectFolder2()
        {

            TreeNode node = trwFolders.Nodes.Find("INBOX", true).FirstOrDefault();
            if (node != null)
                trwFolders.SelectedNode = node;
            pnlView.Visible =
                pnlDownloadingBody.Visible =
                    trwFolders.Enabled =
                        lsvMessages.Enabled =
                            pnlFavorites.Enabled = false;
          
            lsvMessages.VirtualListSize = 0;
            VirutialSize = 0;

            pnlLoading.Show();
            pnlMessages.Hide();
            _selectedMessage = null;
            lblFolder.Text = _selectedFolder.Name;
            (new Thread(GetMails)).Start();
        }

        private void GelenKutusuCek()
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
                this.Show();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          

            timer1.Enabled = false;
            timer1.Stop();
            timer1.Interval = 60000;

            SelectFolder2();
           
            timer1.Enabled = true;
            timer1.Start();
         
        }
        private void ListViewsyazdir()
        {
            using (SQLiteConnection dbConn = MailBoxs.Conneciton)
            {
                listView1.Items.Clear();

                dbConn.Open();
                SQLiteCommand cmd = new SQLiteCommand("Select * From TBGelenKutusu where IDUser ='"+MailBoxs.UserUid+"'", dbConn);
                SQLiteDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
           


                    ListViewItem item = new ListViewItem(dr["Subject"].ToString());
                    item.SubItems.Add(dr["Subject"].ToString());
                    item.SubItems.Add(dr["FolderName"].ToString());
                    item.SubItems.Add(dr["FolderUiVality"].ToString());  
                    item.SubItems.Add(dr["FolderUidNext"].ToString());
                    item.SubItems.Add(dr["FromMailAdress"].ToString());
                    item.SubItems.Add(dr["FromDisplayName"].ToString());
                    item.SubItems.Add(dr["Day"].ToString());
                    listView1.Items.Add(item);
                }
                dbConn.Close();
            }
        }
        private void açToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Normal;
            this.Visible = true;
            this.Show();
            //FrmMain sff = new FrmMain();
            //sff.Visible = true;
            //sff.Show();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TxtAra_Leave(object sender, EventArgs e)
        {
           
        
        }
        private void BtnYeniMesaj_Click(object sender, EventArgs e)
        {

        }
        AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
        SQLiteDataAdapter SlDaAd;
        private void TxtAra_TextChanged(object sender, EventArgs e)
        {
            //SELECT* FROM TBL1 WHERE ADSOYAD LIKE '%"+ KELIME +"%' OR ADRES LIKE '%"+ KELIME +"%'

        }

        private void AutoComplate()
        {
            using (SQLiteConnection dbConn = MailBoxs.Conneciton)
            {
                dbConn.Open();
                SlDaAd = new SQLiteDataAdapter("select Subject from TBGelenKutusu order by IDGelenKutusu", dbConn);
                DataTable dt = new DataTable();
                SlDaAd.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)

                    {

                        coll.Add(dt.Rows[i]["Subject"].ToString());

                    }

                }
                else

                {

                    MessageBox.Show("Name not found");

                }

                TxtAra.AutoCompleteMode = AutoCompleteMode.Suggest;

                TxtAra.AutoCompleteSource = AutoCompleteSource.CustomSource;

                TxtAra.AutoCompleteCustomSource = coll;

                dbConn.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (pnlFolders.Width == 55)
            {
                pnlFolders.Size = new Size(220, 826);
            }
            else
            {
                pnlFolders.Size = new Size(55, 826);
            }
           
        }

        private void LinkPitbulChat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (pnlPitbulChat.Width == 1)
            {
                pnlPitbulChat.Size = new Size(250, 826);
            }
            else
            {
                pnlPitbulChat.Size = new Size(1, 826);
            }
           
        }

        private void BTNHEPSINISEC_Click(object sender, EventArgs e)
        {

        }

        private void pBoxSettings_Click(object sender, EventArgs e)
        {
            SettingsForm sF = new SettingsForm();
            sF.ShowDialog();
            
        }
        public static string FromPublic;
        public static string FromRe;
        private void BTNYANITLA_Click(object sender, EventArgs e)
        {
            _selectedMessage = _messages[lsvMessages.SelectedIndices[0]];
            FromPublic= _selectedMessage.From.Address.ToString();
            FromRe = _selectedMessage.Subject.ToString();

            SendMail SMail = new SendMail();
            SMail.ShowDialog();

        }

        private void BTNOKUNDU_Click(object sender, EventArgs e)
        {
            trwFolders.Enabled = lsvMessages.Enabled = false;
            (new Thread(_ => ToggleSeen(_selectedMessage))).Start();
        }
        public static bool Sld;

        Settings.MailBox.Message message;
        //Folder folderd;
        private void BTNSIL_Click(object sender, EventArgs e)
        {
            Sld = true;
            TreeNode node = trwFolders.Nodes.Find("Deleted Items", true).FirstOrDefault();
            var folder = node.Tag as Folder;
            (new Thread(_ => MoveMessageToFolder(_selectedMessage, folder))).Start();
        }
         private void MoveMessageToFolder(Settings.MailBox.Message message, Folder folder)
        {
            try
            {
                var args = new ServerCallCompletedEventArgs { Result = message.MoveTo(folder) };
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(MoveMessageToFolderCompleted), Program.ImapClient,
                    args);
            }
            catch (Exception ex)
            {
                var args = new ServerCallCompletedEventArgs(false, ex);
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(MoveMessageToFolderCompleted), Program.ImapClient,
                    args);
            }
        }
        private void lblGelenKutusu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (kontrol == true)
            {
                timer1.Enabled = false;
                timer1.Stop();
                TreeNode node = trwFolders.Nodes.Find("INBOX", true).First();
                if (node != null)
                    trwFolders.SelectedNode = node;
                pnlView.Visible =
                    pnlDownloadingBody.Visible =
                                pnlFavorites.Enabled = false;
                lsvMessages.VirtualListSize = 0;
                VirutialSize = 0;
                pnlLoading.Show();
                pnlMessages.Hide();

                _selectedMessage = null;
                lblFolder.Text = _selectedFolder.Name;

                (new Thread(GetMails)).Start();
            }
           
        }
        private void LblGonderilen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();
            TreeNode node = trwFolders.Nodes.Find("Sent Items", true).First();
            if (node != null)
                trwFolders.SelectedNode = node;
            pnlView.Visible =
                pnlDownloadingBody.Visible =
                    trwFolders.Enabled =
                        lsvMessages.Enabled =
                            pnlFavorites.Enabled = false;
            lsvMessages.VirtualListSize = 0;
            VirutialSize = 0;
            pnlLoading.Show();
            pnlMessages.Hide();
            _selectedMessage = null;
            lblFolder.Text = _selectedFolder.Name;
            (new Thread(GetMails)).Start();
        }
        private void LnkTaslaklar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TreeNode node = trwFolders.Nodes.Find("Drafts", true).FirstOrDefault();
            if (node != null)
                trwFolders.SelectedNode = node;
            //pnlSelectFolder.Hide();

            //pnlInfo.Visible =
            pnlView.Visible =
                pnlDownloadingBody.Visible =
                    trwFolders.Enabled =
                        lsvMessages.Enabled =
                            pnlFavorites.Enabled = false;



            lsvMessages.VirtualListSize = 0;
            VirutialSize = 0;
            //pnlviInfo.Visible = wbrMain.Visible = pnlAttachments.Visible = false;

            pnlLoading.Show();
            pnlMessages.Hide();

            _selectedMessage = null;
            //_messageItems.Clear();

            lblFolder.Text = _selectedFolder.Name;

            (new Thread(GetMails)).Start();
        }

        private void LblSpan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TreeNode node = trwFolders.Nodes.Find("Junk E-mail", true).FirstOrDefault();
            if (node != null)
                trwFolders.SelectedNode = node;
            //pnlSelectFolder.Hide();

            //pnlInfo.Visible =
            pnlView.Visible =
                pnlDownloadingBody.Visible =
                    trwFolders.Enabled =
                        lsvMessages.Enabled =
                            pnlFavorites.Enabled = false;



            lsvMessages.VirtualListSize = 0;
            VirutialSize = 0;
            //pnlviInfo.Visible = wbrMain.Visible = pnlAttachments.Visible = false;

            pnlLoading.Show();
            pnlMessages.Hide();

            _selectedMessage = null;
            //_messageItems.Clear();

            lblFolder.Text = _selectedFolder.Name;

            (new Thread(GetMails)).Start();
        }

        private void LblCopKutusu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TreeNode node = trwFolders.Nodes.Find("Deleted Items", true).FirstOrDefault();
            if (node != null)
                trwFolders.SelectedNode = node;
            //pnlSelectFolder.Hide();

            //pnlInfo.Visible =
            pnlView.Visible =
                pnlDownloadingBody.Visible =
                    trwFolders.Enabled =
                        lsvMessages.Enabled =
                            pnlFavorites.Enabled = false;



            lsvMessages.VirtualListSize = 0;
            VirutialSize = 0;
            //pnlviInfo.Visible = wbrMain.Visible = pnlAttachments.Visible = false;

            pnlLoading.Show();
            pnlMessages.Hide();

            _selectedMessage = null;
            //_messageItems.Clear();

            lblFolder.Text = _selectedFolder.Name;

            (new Thread(GetMails)).Start();
        }

        private void btnolustur_Click(object sender, EventArgs e)
        {
            SendMail smail = new SendMail();
            smail.ShowDialog();
        }
        bool formTasiniyor = false;
        Point baslangicNoktasi = new Point(0, 0);
        private void panel13_MouseDown(object sender, MouseEventArgs e)
        {
            formTasiniyor = true;
            baslangicNoktasi = new Point(e.X, e.Y);
        }

        private void panel13_MouseMove(object sender, MouseEventArgs e)
        {
            if (formTasiniyor)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.baslangicNoktasi.X, p.Y - this.baslangicNoktasi.Y);
            }
        }

        private void panel13_MouseUp(object sender, MouseEventArgs e)
        {
            formTasiniyor = false;
        }

        private void panel13_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FrmMain mainf = new FrmMain();
            if (FormWindowState.Normal == mainf.WindowState)
            {
                //this.WindowState = FormWindowState.Maximized;
                BtnMinimized.Visible = true;
                BtnMaxsimized.Visible = false;

                WindowState = WindowState == FormWindowState.Maximized
                 ? FormWindowState.Normal
                 : FormWindowState.Maximized;
            }
             if (FormWindowState.Maximized == mainf.WindowState)
            {

                this.WindowState = FormWindowState.Normal;
                BtnMinimized.Visible = false;
                BtnMaxsimized.Visible = true;
            }

           

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SendMail SMail = new SendMail();
            SMail.ShowDialog();
        }

        private void BtnWidget_Click(object sender, EventArgs e)
        {
            FrmMain frm = new FrmMain();
            frm.Hide();
            frm.Visible = false;

            this.Hide();

            this.Visible = false;

            WidgetShow Ws = new WidgetShow();
            Ws.ShowDialog();
        }
    }
    
}
