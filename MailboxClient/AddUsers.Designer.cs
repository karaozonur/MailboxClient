namespace MailboxClient
{
    partial class AddUsers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddUsers));
            this.tltMain = new System.Windows.Forms.ToolTip(this.components);
            this.chkValidateCertificate = new System.Windows.Forms.CheckBox();
            this.wbrMain = new System.Windows.Forms.WebBrowser();
            this.lblWait = new System.Windows.Forms.Label();
            this.btnSignIn = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.cmbEncryption = new System.Windows.Forms.ComboBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtAdiSoyadi = new System.Windows.Forms.TextBox();
            this.TxtPort = new System.Windows.Forms.TextBox();
            this.TxtSunucu = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.pnlLogin.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // chkValidateCertificate
            // 
            this.chkValidateCertificate.AutoSize = true;
            this.chkValidateCertificate.Location = new System.Drawing.Point(384, 121);
            this.chkValidateCertificate.Name = "chkValidateCertificate";
            this.chkValidateCertificate.Size = new System.Drawing.Size(18, 17);
            this.chkValidateCertificate.TabIndex = 3;
            this.tltMain.SetToolTip(this.chkValidateCertificate, "Validate server certificate");
            this.chkValidateCertificate.UseVisualStyleBackColor = true;
            // 
            // wbrMain
            // 
            this.wbrMain.Location = new System.Drawing.Point(3, 282);
            this.wbrMain.Margin = new System.Windows.Forms.Padding(3, 49, 3, 3);
            this.wbrMain.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbrMain.Name = "wbrMain";
            this.wbrMain.Size = new System.Drawing.Size(537, 84);
            this.wbrMain.TabIndex = 18;
            this.wbrMain.Visible = false;
            // 
            // lblWait
            // 
            this.lblWait.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(55)))), ((int)(((byte)(95)))));
            this.lblWait.ForeColor = System.Drawing.Color.White;
            this.lblWait.Location = new System.Drawing.Point(189, -12);
            this.lblWait.Name = "lblWait";
            this.lblWait.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.lblWait.Size = new System.Drawing.Size(212, 37);
            this.lblWait.TabIndex = 15;
            this.lblWait.Text = "Connecting...";
            this.lblWait.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblWait.Visible = false;
            // 
            // btnSignIn
            // 
            this.btnSignIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(55)))), ((int)(((byte)(95)))));
            this.btnSignIn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSignIn.ForeColor = System.Drawing.Color.White;
            this.btnSignIn.Location = new System.Drawing.Point(191, 210);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(143, 49);
            this.btnSignIn.TabIndex = 6;
            this.btnSignIn.Text = "Giriş";
            this.btnSignIn.UseVisualStyleBackColor = false;
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(98, 182);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 21);
            this.lblPassword.TabIndex = 12;
            this.lblPassword.Text = "Parola";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(191, 179);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(210, 28);
            this.txtPassword.TabIndex = 5;
            // 
            // cmbEncryption
            // 
            this.cmbEncryption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEncryption.FormattingEnabled = true;
            this.cmbEncryption.Items.AddRange(new object[] {
            "None",
            "SSL",
            "TLS"});
            this.cmbEncryption.Location = new System.Drawing.Point(299, 113);
            this.cmbEncryption.Name = "cmbEncryption";
            this.cmbEncryption.Size = new System.Drawing.Size(79, 29);
            this.cmbEncryption.TabIndex = 2;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(113, 121);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(40, 21);
            this.lblPort.TabIndex = 9;
            this.lblPort.Text = "Port";
            // 
            // lblLogin
            // 
            this.lblLogin.AutoSize = true;
            this.lblLogin.Location = new System.Drawing.Point(113, 151);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(40, 21);
            this.lblLogin.TabIndex = 7;
            this.lblLogin.Text = "Mail";
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(191, 148);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(210, 28);
            this.txtLogin.TabIndex = 4;
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(97, 89);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(57, 21);
            this.lblServer.TabIndex = 3;
            this.lblServer.Text = "Server";
            // 
            // pnlLogin
            // 
            this.pnlLogin.BackColor = System.Drawing.Color.Transparent;
            this.pnlLogin.Controls.Add(this.label2);
            this.pnlLogin.Controls.Add(this.TxtAdiSoyadi);
            this.pnlLogin.Controls.Add(this.TxtPort);
            this.pnlLogin.Controls.Add(this.TxtSunucu);
            this.pnlLogin.Controls.Add(this.button1);
            this.pnlLogin.Controls.Add(this.wbrMain);
            this.pnlLogin.Controls.Add(this.lblWait);
            this.pnlLogin.Controls.Add(this.chkValidateCertificate);
            this.pnlLogin.Controls.Add(this.btnSignIn);
            this.pnlLogin.Controls.Add(this.lblPassword);
            this.pnlLogin.Controls.Add(this.txtPassword);
            this.pnlLogin.Controls.Add(this.cmbEncryption);
            this.pnlLogin.Controls.Add(this.lblPort);
            this.pnlLogin.Controls.Add(this.lblLogin);
            this.pnlLogin.Controls.Add(this.txtLogin);
            this.pnlLogin.Controls.Add(this.lblServer);
            this.pnlLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogin.Location = new System.Drawing.Point(0, 161);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(537, 292);
            this.pnlLogin.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 21);
            this.label2.TabIndex = 23;
            this.label2.Text = "Adınız Soyadınız";
            // 
            // TxtAdiSoyadi
            // 
            this.TxtAdiSoyadi.Location = new System.Drawing.Point(189, 48);
            this.TxtAdiSoyadi.Name = "TxtAdiSoyadi";
            this.TxtAdiSoyadi.Size = new System.Drawing.Size(212, 28);
            this.TxtAdiSoyadi.TabIndex = 22;
            // 
            // TxtPort
            // 
            this.TxtPort.Location = new System.Drawing.Point(191, 113);
            this.TxtPort.Name = "TxtPort";
            this.TxtPort.Size = new System.Drawing.Size(102, 28);
            this.TxtPort.TabIndex = 21;
            // 
            // TxtSunucu
            // 
            this.TxtSunucu.Location = new System.Drawing.Point(190, 82);
            this.TxtSunucu.Name = "TxtSunucu";
            this.TxtSunucu.Size = new System.Drawing.Size(212, 28);
            this.TxtSunucu.TabIndex = 20;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(55)))), ((int)(((byte)(95)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(340, 210);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 49);
            this.button1.TabIndex = 19;
            this.button1.Text = "x";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Controls.Add(this.picLogo);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(24, 12, 12, 12);
            this.pnlTop.Size = new System.Drawing.Size(537, 161);
            this.pnlTop.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(55)))), ((int)(((byte)(95)))));
            this.label1.Location = new System.Drawing.Point(288, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 34);
            this.label1.TabIndex = 1;
            this.label1.Text = "PİTBULL";
            // 
            // picLogo
            // 
            this.picLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picLogo.Image = global::MailboxClient.Properties.Resources.pitbull_logo;
            this.picLogo.Location = new System.Drawing.Point(191, 12);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(84, 119);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // AddUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(537, 453);
            this.Controls.Add(this.pnlLogin);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AddUsers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "";
            this.Text = "KULLANICI EKLE";
            this.Load += new System.EventHandler(this.AddUsers_Load);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip tltMain;
        private System.Windows.Forms.CheckBox chkValidateCertificate;
        private System.Windows.Forms.WebBrowser wbrMain;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.Button btnSignIn;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ComboBox cmbEncryption;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TxtSunucu;
        private System.Windows.Forms.TextBox TxtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtAdiSoyadi;
    }
}