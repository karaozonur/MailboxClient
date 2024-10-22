﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MailboxClient;
using MailboxClient.Settings;
using System.Data.SQLite;
using System.Data.SqlClient;
using EAGetMail;


using System.Runtime.InteropServices;
using OutLook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Reflection;

using System.Net;
using System.IO;
using MailboxClient.Settings.MailBox;
using System.Threading;
using System.Globalization;
using Mono.Web;
using System.Diagnostics;

namespace MailboxClient
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<long, ListViewItem> _messageItems;
        private TreeNode _lastClickedNode;
        private List<MailboxClient.Settings.MailBox.Message> _messages;
        private Folder _selectedFolder;
        private MailboxClient.Settings.MailBox.Message _selectedMessage;

        public Form1()
        {
            InitializeComponent();
            _messageItems = new Dictionary<long, ListViewItem>();

        }
        string UserID;

        private void Form1_Load(object sender, EventArgs e)
        {

            using (var frmStart = new AddUsers())
            {
                if (frmStart.ShowDialog() == DialogResult.OK)
                {
                    wbrMain.Navigate("about:blank");
                    BindFolders();
                    ConfigureClient();

                    UserID = AddUsers.IDUser;
                }
                else
                    Application.Exit();
            }
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
        //
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
                //MessageBox.Show("Failed to write data, file might be in use", "Error", MessageBoxButtons.OK,
                //    MessageBoxIcon.Error);
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
        string Deneme;
        int KatSayisi = -1;
        int sayi;
        string Uid;
        bool KayitVarYok;
        private TreeNode FolderToNode(Folder folder)
        {

            var node = new TreeNode(folder.Name)

            { Name = folder.Path };


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


            node.Nodes.AddRange(folder.SubFolders.Select(FolderToNode).ToArray());
            node.Tag = folder;

            using (SQLiteConnection dbConn2 = MailBoxs.Conneciton)
            {
                dbConn2.Open();
                SQLiteCommand cmd = new SQLiteCommand("select * from TBDosya where AccountId='" + AddUsers.IDUser + "' and  Name ='" + folder.Name + "'", dbConn2);
                SQLiteDataReader SQRead = cmd.ExecuteReader();

                if (SQRead.Read())
                {
                    KayitVarYok = false;
                    Uid = SQRead["Name"].ToString();
                }
                else
                {
                    //kayit yok
                    KayitVarYok = true;
                   
                }

                SQRead.Close();

                if (KayitVarYok == true)
                {

                    if (folder.Name == "Deleted Items")
                    {
                        Deneme = "Çöp Kutusu";
                    }
                    else if (folder.Name == "INBOX")
                    {
                        Deneme = "Gelen Kutusu";
                    }
                    else if (folder.Name == "Drafts")
                    {
                        Deneme = "Taslaklar";
                    }
                    else if (folder.Name == "Junk E-mail")
                    {
                        Deneme = "Gereksiz e-posta";
                    }
                    else if (folder.Name == "Sent Items")
                    {
                        Deneme = "Gönderilmiş Öğeler";
                    }
                    else if (folder.Name == "Public")
                    {
                        Deneme = "Public";
                    }
                    else if (folder.Name == "Notes")
                    {
                        Deneme = "Notlar";
                    }
                    else
                    {
                        Deneme = " ";
                    }


                    string tur = "INSERT INTO TBDosya(AccountId,Name,HEXNAME,UidValidity) VALUES (@AccountId,@Name,@HEXNAME,@UidValidity)";
                    SQLiteCommand SqlCmm = new SQLiteCommand(tur, dbConn2);
                    SqlCmm.Parameters.AddWithValue("@AccountId", AddUsers.IDUser);
                    SqlCmm.Parameters.AddWithValue("@Name", folder.Name);
                    SqlCmm.Parameters.AddWithValue("@HEXNAME", Deneme);
                    SqlCmm.Parameters.AddWithValue("@UidValidity", folder.UidValidity);
                    SqlCmm.ExecuteNonQuery();

                }
                else if (KayitVarYok == false)
                {
                    if (Uid == folder.Name)
                    {
                        Uid = "";
                    }
                    else
                    {
                        if (folder.Name == "Deleted Items")
                        {
                            Deneme = "Çöp Kutusu";
                        }
                        else if (folder.Name == "INBOX")
                        {
                            Deneme = "Gelen Kutusu";
                        }
                        else if (folder.Name == "Drafts")
                        {
                            Deneme = "Taslaklar";
                        }
                        else if (folder.Name == "Junk E-mail")
                        {
                            Deneme = "Gereksiz e-posta";
                        }
                        else if (folder.Name == "Sent Items")
                        {
                            Deneme = "Gönderilmiş Öğeler";
                        }
                        else if (folder.Name == "Public")
                        {
                            Deneme = "Public";
                        }
                        else if (folder.Name == "Notes")
                        {
                            Deneme = "Notlar";
                        }
                        else
                        {
                            Deneme = " ";
                        }

                        string tur = "INSERT INTO TBDosya(AccountId,Name,HEXNAME,UidValidity) VALUES (@AccountId,@Name,@HEXNAME,@UidValidity)";
                        SQLiteCommand SqlCmm = new SQLiteCommand(tur, dbConn2);
                        SqlCmm.Parameters.AddWithValue("@AccountId", AddUsers.IDUser);
                        SqlCmm.Parameters.AddWithValue("@Name", folder.Name);
                        SqlCmm.Parameters.AddWithValue("@HEXNAME", Deneme);
                        SqlCmm.Parameters.AddWithValue("@UidValidity", folder.UidValidity);
                        SqlCmm.ExecuteNonQuery();
                        Uid = "";
                    }
                }



                    dbConn2.Close();
                    dbConn2.Dispose();    
            }
           

             



            return node;

        }

        private void AutoResizeMessageListViewColumn()
        {
            ScrollBars scrollBars = NativeMethods.GetVisibleScrollbars(lsvMessages);
            clmMessages.Width = (scrollBars & ScrollBars.Vertical) == ScrollBars.Vertical
                ? lsvMessages.Width - SystemInformation.VerticalScrollBarWidth
                : lsvMessages.Width;



        }
        private LinkLabel FindFavoriteLnk(Folder folder)
        {
            if (folder == Program.ImapClient.Folders.All)
                return lnkAll;
            if (folder == Program.ImapClient.Folders.Archive)
                return lnkArchive;
            if (folder == Program.ImapClient.Folders.Drafts)
                return lnkDrafts;
            if (folder == Program.ImapClient.Folders.Flagged)
                return lnkFlagged;
            if (folder == Program.ImapClient.Folders.Important)
                return lnkImportant;
            if (folder == Program.ImapClient.Folders.Inbox)
                return lnkInbox;
            if (folder == Program.ImapClient.Folders.Junk)
                return lnkJunk;
            if (folder == Program.ImapClient.Folders.Sent)
                return lnkSent;

            return folder == Program.ImapClient.Folders.Trash ? lnkTrash : null;
        }
        private void BindFolders()
        {
            lnkArchive.Visible = Program.ImapClient.Folders.Archive != null;
            lnkAll.Visible = Program.ImapClient.Folders.All != null;
            lnkTrash.Visible = Program.ImapClient.Folders.Trash != null;
            lnkJunk.Visible = Program.ImapClient.Folders.Junk != null;
            lnkFlagged.Visible = Program.ImapClient.Folders.Flagged != null;
            lnkImportant.Visible = Program.ImapClient.Folders.Important != null;
            lnkDrafts.Visible = Program.ImapClient.Folders.Drafts != null;
            lnkSent.Visible = Program.ImapClient.Folders.Sent != null;
            lnkInbox.Visible = Program.ImapClient.Folders.Inbox != null;

            //List<String> firstlist = new List<String>();

            //foreach (String str in firstlist)
            //{
            //    Console.WriteLine(str);
            //}

            trwFolders.Nodes.Add(Program.ImapClient.Host);
            trwFolders.Nodes[0].Nodes.AddRange(Program.ImapClient.Folders.Select(FolderToNode).ToArray());
            trwFolders.Nodes[0].Expand();



        }

        private void trwFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trwFolders.SelectedNode == null || trwFolders.SelectedNode == trwFolders.Nodes[0])
            {

            }
            else
            {
                var folder = trwFolders.SelectedNode.Tag as Folder;

                trwFolders.SelectedNode.NodeFont = new Font(trwFolders.Font, FontStyle.Bold);


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
            SetFavoriteSelection(folder);

            TreeNode node = trwFolders.Nodes.Find(folder.Path, true).FirstOrDefault();
            if (node != null)
                trwFolders.SelectedNode = node;
            pnlSelectFolder.Hide();

            pnlInfo.Visible =
                pnlView.Visible =
                    pnlDownloadingBody.Visible =
                        trwFolders.Enabled =
                            lsvMessages.Enabled =
                                pnlFavorites.Enabled = false;

            lsvMessages.VirtualListSize = 0;

            //pnlInfo.Visible = wbrMain.Visible = pnlAttachments.Visible = false;

            pnlLoading.Show();
            pnlMessages.Hide();

            _selectedMessage = null;
            _messageItems.Clear();

            lblFolder.Text = _selectedFolder.Name;

            (new Thread(GetMails)).Start();
        }
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
        }
        private void lnkFavorite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sender.Equals(lnkAll))
                SelectFolder(Program.ImapClient.Folders.All);

            else if (sender.Equals(lnkArchive))
                SelectFolder(Program.ImapClient.Folders.Archive);

            else if (sender.Equals(lnkDrafts))
                SelectFolder(Program.ImapClient.Folders.Drafts);

            else if (sender.Equals(lnkFlagged))
                SelectFolder(Program.ImapClient.Folders.Flagged);

            else if (sender.Equals(lnkImportant))
                SelectFolder(Program.ImapClient.Folders.Important);

            else if (sender.Equals(lnkInbox))
                SelectFolder(Program.ImapClient.Folders.Inbox);

            else if (sender.Equals(lnkJunk))
                SelectFolder(Program.ImapClient.Folders.Junk);

            else if (sender.Equals(lnkSent))
                SelectFolder(Program.ImapClient.Folders.Sent);

            else if (sender.Equals(lnkTrash))
                SelectFolder(Program.ImapClient.Folders.Trash);
        }
        private void SetFavoriteSelection(Folder folder)
        {
            if (folder == null) return;

            LinkLabel lnk = FindFavoriteLnk(folder);

            lnkAll.Font =
                lnkArchive.Font =
                    lnkDrafts.Font =
                        lnkFlagged.Font =
                            lnkImportant.Font =
                                lnkInbox.Font =
                                    lnkJunk.Font =
                                        lnkSent.Font =
                                            lnkTrash.Font = new Font(lnkTrash.Font, FontStyle.Regular);

            if (lnk != null)
                lnk.Font = new Font(lnk.Font, FontStyle.Bold);
        }
        private void trwFolders_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            var folder = e.Node.Tag as Folder;
            if (folder == null || !folder.Selectable)
                e.Cancel = true;

            if (trwFolders.SelectedNode == null) return;

            //trwFolders.SelectedNode.NodeFont = new Font(trwFolders.Font, FontStyle.Regular);
        }
        private void GetMails()
        {
            try
            {
                _messages = _selectedFolder.Search().OrderByDescending(_ => _.Date).ToList();
                _selectedFolder.StartIdling();
                var args = new ServerCallCompletedEventArgs();
                Invoke(new EventHandler<ServerCallCompletedEventArgs>(GetMailsCompleted), Program.ImapClient, args);
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
        }
        private void FrmMainOrLsvMails_SizeChanged(object sender, EventArgs e)
        {
            AutoResizeMessageListViewColumn();
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

            if (_messageItems.ContainsKey(msg.UId))
                e.Item = _messageItems[msg.UId];
            else
            {
                var item = new ListViewItem(string.IsNullOrEmpty(msg.Subject)
                    ? "(Konu yok)"
                    : msg.Subject)
                {
                    ForeColor = color,
                    ImageIndex = msg.Attachments.Any() ? 0 : -1
                };
                item.Font = new Font(item.Font, msg.Seen ? FontStyle.Regular : FontStyle.Bold);
                _messageItems.Add(msg.UId, item);
                e.Item = item;
            }
        }
        private void lsvMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvMessages.SelectedIndices.Count == 0) return;

            _selectedMessage = _messages[lsvMessages.SelectedIndices[0]];

            lblDate.Text = _selectedMessage.Date.HasValue
                ? _selectedMessage.Date.Value.ToString("ddd, dd MMM yyyy HH:mm:ss")
                : "Bilinmeyen tarih";

            lblFrom.Text = "From: " + (_selectedMessage.From == null && _selectedMessage.Sender == null
                ? "Bilinmeyen gönderen"
                : (_selectedMessage.From ?? _selectedMessage.Sender).ToString());

            lblSubject.Text = string.IsNullOrEmpty(_selectedMessage.Subject)
                ? "( Konu yok )"
                : _selectedMessage.Subject;

            pnlInfo.Show();
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
                wbrMain.Document.OpenNew(true);
                wbrMain.Document.Write(_selectedMessage.Body.HasHtml ? body : HttpUtility.HtmlEncode(body).Replace(Environment.NewLine, "<br />"));
                if (wbrMain.Document.Body != null)
                    wbrMain.Document.Body.SetAttribute("scroll", "auto");
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

                wbrMain.Document.OpenNew(true);
                wbrMain.Document.Write(_selectedMessage.Body.HasHtml ? body : body.Replace("\n", "<br />"));
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
                LinkLabel lnk = FindFavoriteLnk(folder);
                if (lnk != null)
                    lnk.Hide();

                if (_selectedFolder == folder)
                {
                    lsvMessages.VirtualListSize = 0;
                    _messages.Clear();
                    trwFolders.SelectedNode = null;

                    pnlSelectFolder.Show();

                    pnlMessages.Visible =
                        pnlInfo.Visible =
                            pnlView.Visible =
                                pnlDownloadingBody.Visible = false;
                }

                _lastClickedNode.Remove();
            }
            else
                MessageBox.Show("Failed to delete folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void createNewMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
                _messageItems[msg.UId].Font = new Font(_messageItems[msg.UId].Font,
                    msg.Seen ? FontStyle.Regular : FontStyle.Bold);

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

        private void çIKIŞToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void lnkInbox_Click(object sender, EventArgs e, Folder folder)
        {
            //if (_selectedFolder != null)
            //    _selectedFolder.OnNewMessagesArrived -= _selectedFolder_OnNewMessagesArrived;

            //_selectedFolder = folder;
            //_selectedFolder.OnNewMessagesArrived += _selectedFolder_OnNewMessagesArrived;
            //SetFavoriteSelection(folder);

            //TreeNode node = trwFolders.Nodes.Find(folder.Path, true).FirstOrDefault();
            //if (node != null)
            //    trwFolders.SelectedNode = node;
            //pnlSelectFolder.Hide();
            //_SecilenDosyaAdi = "";
        }

        private void aYARLARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm SForm = new SettingsForm();
            SForm.ShowDialog();
        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show();
            //contextMenuStrip1.Opened();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
           
        }

        private void deleteToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if (
             MessageBox.Show("Do you really want to remove this message?", "Remove folder",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            trwFolders.Enabled = lsvMessages.Enabled = false;
            (new Thread(_ => RemoveMessage(_selectedMessage))).Start();
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {
                    }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FrmMain s = new FrmMain();
            s.Show();
        }
    }


}
