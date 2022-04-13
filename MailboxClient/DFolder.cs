using MailboxClient.Settings.MailBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailboxClient
{
    public partial class DFolder : Form
    {
        public DFolder(string title, string text)
        {
            InitializeComponent();

            Text = title;
            lblTitle.Text = title;
            lblText.Text = text;

            trwFolders.Nodes.Add(Program.ImapClient.Host);
            trwFolders.Nodes[0].Nodes.AddRange(Program.ImapClient.Folders.Select(FolderToNode).ToArray());
            trwFolders.Nodes[0].Expand();

        }
        public Folder SelectedFolder
        {
            get { return trwFolders.SelectedNode.Tag as Folder; }
        }

        private TreeNode FolderToNode(Folder folder)
        {
            var node = new TreeNode(folder.Name);
            node.Nodes.AddRange(folder.SubFolders.Select(FolderToNode).ToArray());
            node.Tag =folder;
            return node;

            

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void trwFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //btnOK.Enabled = trwFolders.SelectedNode != null && trwFolders.SelectedNode != trwFolders.Nodes[0];

        }

        private void trwFolders_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            var folder = e.Node.Tag as Folder;
            if (folder == null || !folder.Selectable)
                e.Cancel = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public static Folder Show(string title, string text, IWin32Window owner = null)
        {
            using (var dlg = new DFolder(title, text))
                if (dlg.ShowDialog(owner) == DialogResult.OK)
                    return dlg.SelectedFolder;
            return null;
        }

        private void trwFolders_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (btnOK.Enabled)
                btnOK_Click(null, null);
        }

        //private void FolderBox_Load(object sender, EventArgs e)
        //{
        //    RenkleriDuzenle();
        //}
        private void RenkleriDuzenle()
        {
            lblTitle.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            lblText.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            //panel4.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            //panel5.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;
            panel2.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;

            this.BackColor = Color.FromArgb(Settings1.Default.Renk.ToArgb()); ;

        }
        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void DFolder_Load(object sender, EventArgs e)
        {
            RenkleriDuzenle();
            //trwFolders Node = new trwFolders();
            //TreeNode node = trwFolders.Nodes.Find("Deleted Items", true).FirstOrDefault();
            //trwFolders.SelectedNode = node;
            //var folder = node.Tag as Folder;
            //if (folder == null || !folder.Selectable)
            //    e.Cancel = true;
        }


    }
    
}