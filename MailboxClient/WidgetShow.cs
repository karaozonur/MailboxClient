using MailboxClient.Settings.MailBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailboxClient
{
    public partial class WidgetShow : Form
    {
        public WidgetShow()
        {
            InitializeComponent();
        }
        private TreeNode _lastClickedNode;
        private List<MailboxClient.Settings.MailBox.Message> _messages;

        private readonly List<ListViewItem> _messageItems;
        public static readonly object lockobject = new object();

        private Folder _selectedFolder;
        public MailboxClient.Settings.MailBox.Message selectedMessage2;
        private void WidgetShow_Load(object sender, EventArgs e)
        {
            BackColor = Color.Black;
            TransparencyKey = Color.Black;

            this.Left = (Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width);
            panel2.Visible = false;
            panel3.Visible = false;

            BindFolders();
            FrmMain frm = new FrmMain();
            frm.Hide();
            frm.Visible = false;
        }
        bool formTasiniyor = false;
        Point baslangicNoktasi = new Point(0, 0);
      
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            formTasiniyor = true;
            baslangicNoktasi = new Point(e.X, e.Y);
        }
        string FolderName;
        private TreeNode FolderToNode(Folder folder)
        {
            //
            var node = new TreeNode(folder.Name)
            { Name = folder.Path };
            FolderName = folder.Name;
         


            node.Nodes.AddRange(folder.SubFolders.Select(FolderToNode).ToArray());
            node.Tag = folder;
            return node;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (formTasiniyor)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.baslangicNoktasi.X, p.Y - this.baslangicNoktasi.Y);
            }
        }
        private void BindFolders()
        {
            //lnkArchive.Visible = Program.ImapClient.Folders.Archive != null;
            //lnkAll.Visible = Program.ImapClient.Folders.All != null;
            //lnkTrash.Visible = Program.ImapClient.Folders.Trash != null;
            //lnkJunk.Visible = Program.ImapClient.Folders.Junk != null;
            //lnkFlagged.Visible = Program.ImapClient.Folders.Flagged != null;
            //lnkImportant.Visible = Program.ImapClient.Folders.Important != null;
            //lnkDrafts.Visible = Program.ImapClient.Folders.Drafts != null;
            //lnkSent.Visible = Program.ImapClient.Folders.Sent != null;
            //lnkInbox.Visible = Program.ImapClient.Folders.Inbox != null;
            trwFolders.Nodes.Add(Program.ImapClient.Host);
            trwFolders.Nodes[0].Nodes.AddRange(Program.ImapClient.Folders.Select(FolderToNode).ToArray());
            trwFolders.Nodes[0].Expand();
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            formTasiniyor = false;
            BackColor = Color.Lime;
        }

        private void lblGelenKutusu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel2.Visible = true;
            panel3.Visible = true;
          
            panel2.BackColor = Color.WhiteSmoke;
            panel3.BackColor = Color.WhiteSmoke;
            SelectFolder2();
        }
        public static bool WidgetCalisti;
        private void programıAçToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WidgetCalisti = true;



            FrmMain frm = new FrmMain();
            frm.Show();
            frm.Visible = true;

            this.Close();

        }

        private void programıKapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void trwFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trwFolders.SelectedNode == null || trwFolders.SelectedNode == trwFolders.Nodes[0])
            {
                e.Node.Text = "INBOX";

            }
            else
            {
                //timer1.Enabled = false;
                //timer1.Stop();
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
            SetFavoriteSelection(folder);

            TreeNode node = trwFolders.Nodes.Find(folder.Path, true).FirstOrDefault();
            if (node != null)
                trwFolders.SelectedNode = node;
            lsvMessages.VirtualListSize = 0;
            VirutialSize = 0;
            selectedMessage2 = null;
            lsvMessages.Items.Clear();
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
        private void SetFavoriteSelection(Folder folder)
        {
            if (folder == null) return;

        }

        private void trwFolders_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            var folder = e.Node.Tag as Folder;
            if (folder == null || !folder.Selectable)
                e.Cancel = true;
            if (trwFolders.SelectedNode == null) return;
        }
        private void SelectFolder2()
        {

            TreeNode node = trwFolders.Nodes.Find("INBOX", true).FirstOrDefault();
            if (node != null)
                trwFolders.SelectedNode = node;
          
            if (lsvMessages.Items.Count == 0)
            {

            }
            else
            {
                int fen = 12;
                if (lsvMessages.Items.Count < _messages.Count)
                {
                    
                }
            }


            lsvMessages.VirtualListSize = 0;
            VirutialSize = 0;
            //pnlInfo.Visible = wbrMain.Visible = pnlAttachments.Visible = false;

            //pnlLoading.Show();
            //pnlMessages.Hide();

            selectedMessage2 = null;
            //_messageItems.Clear();

            ////lblFolder.Text = _selectedFolder.Name;

            (new Thread(GetMails)).Start();
        }
        private void trwFolders_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }
        private void GetMails()
        {
            try
            {
                lock (lockobject)
                {
                    _messages = _selectedFolder.Search().OrderByDescending(_ => _.Date).ToList();
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
                label1.Text = _selectedFolder.Name + (count == 0 ? "" : string.Format(" ({0})", count));

                AutoResizeMessageListViewColumn();
            }
            else
            {
                using (var frm = new FrmError(e.Exception))
                    frm.ShowDialog();
            }
            //pnlLoading.Hide();
            //pnlMessages.Show();
            //trwFolders.Enabled =
            //    lsvMessages.Enabled =
                    //pnlFavorites.Enabled = true;


            //Cek();

        }
        private void AutoResizeMessageListViewColumn()
        {
            ScrollBars scrollBars = NativeMethods.GetVisibleScrollbars(lsvMessages);

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

            //if (_messageItems.ContainsKey(msg.UId))
            //    e.Item = _messageItems[msg.UId];
            //else
            //{
            ListViewItem item = new ListViewItem(string.IsNullOrEmpty(msg.From.DisplayName.ToString())
                    ? "(Konu yok)"
                    : msg.From.DisplayName.ToString())
            {
                //ForeColor = color,
              
            };
            item.Font = new Font(item.Font, msg.Seen ? FontStyle.Regular : FontStyle.Bold);

            item.SubItems.Add(msg.Subject.ToString());
            item.SubItems.Add(msg.Date.ToString());
            item.SubItems.Add(msg.Folder.Name.ToString());

            e.Item = item;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;

            //panel2.BackColor = Color.WhiteSmoke;
            //panel3.BackColor = Color.WhiteSmoke;
            //SelectFolder2();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Enabled = false;
            timer1.Stop();
            timer1.Interval = 10000;

            SelectFolder2();

            timer1.Enabled = true;
            timer1.Start();
        }
        
        private void lsvMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            //selectedMessage2 = _messages[lsvMessages.SelectedIndices[0]];


            //WidgetCalisti = true;



            //FrmMain frm = new FrmMain();
            //frm.Show();
            //frm.Visible = true;

            //this.Close();

        }
    }
}
