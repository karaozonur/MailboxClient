namespace MailboxClient
{
    partial class SendMail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendMail));
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnMinimized = new System.Windows.Forms.Button();
            this.BtnMaxsimized = new System.Windows.Forms.Button();
            this.BrnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtkonu = new System.Windows.Forms.TextBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btntfolder = new System.Windows.Forms.Button();
            this.BtnSend = new System.Windows.Forms.Button();
            this.BTNRENK = new System.Windows.Forms.Button();
            this.btnBoyutKucult = new System.Windows.Forms.Button();
            this.BtnBoyutArtir = new System.Windows.Forms.Button();
            this.cbAltiCizili = new System.Windows.Forms.CheckBox();
            this.cbKalin = new System.Windows.Forms.CheckBox();
            this.cbItalik = new System.Windows.Forms.CheckBox();
            this.cbBoyut = new System.Windows.Forms.ComboBox();
            this.cbFont = new System.Windows.Forms.ComboBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnMinimized);
            this.panel1.Controls.Add(this.BtnMaxsimized);
            this.panel1.Controls.Add(this.BrnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(56, 600);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // BtnMinimized
            // 
            this.BtnMinimized.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.BtnMinimized.FlatAppearance.BorderSize = 0;
            this.BtnMinimized.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMinimized.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnMinimized.ForeColor = System.Drawing.Color.White;
            this.BtnMinimized.Image = global::MailboxClient.Properties.Resources.window_5_16;
            this.BtnMinimized.Location = new System.Drawing.Point(8, 44);
            this.BtnMinimized.Name = "BtnMinimized";
            this.BtnMinimized.Size = new System.Drawing.Size(39, 36);
            this.BtnMinimized.TabIndex = 12;
            this.BtnMinimized.UseVisualStyleBackColor = false;
            this.BtnMinimized.Click += new System.EventHandler(this.BtnMinimized_Click);
            // 
            // BtnMaxsimized
            // 
            this.BtnMaxsimized.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.BtnMaxsimized.FlatAppearance.BorderSize = 0;
            this.BtnMaxsimized.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMaxsimized.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnMaxsimized.ForeColor = System.Drawing.Color.White;
            this.BtnMaxsimized.Image = global::MailboxClient.Properties.Resources.top_navigation_toolbar_16;
            this.BtnMaxsimized.Location = new System.Drawing.Point(8, 44);
            this.BtnMaxsimized.Name = "BtnMaxsimized";
            this.BtnMaxsimized.Size = new System.Drawing.Size(39, 36);
            this.BtnMaxsimized.TabIndex = 11;
            this.BtnMaxsimized.UseVisualStyleBackColor = false;
            this.BtnMaxsimized.Click += new System.EventHandler(this.BtnMaxsimized_Click);
            // 
            // BrnClose
            // 
            this.BrnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.BrnClose.FlatAppearance.BorderSize = 0;
            this.BrnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BrnClose.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BrnClose.ForeColor = System.Drawing.Color.White;
            this.BrnClose.Image = global::MailboxClient.Properties.Resources.power_2_24;
            this.BrnClose.Location = new System.Drawing.Point(8, 5);
            this.BrnClose.Name = "BrnClose";
            this.BrnClose.Size = new System.Drawing.Size(39, 36);
            this.BrnClose.TabIndex = 9;
            this.BrnClose.UseVisualStyleBackColor = false;
            this.BrnClose.Click += new System.EventHandler(this.BrnClose_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel9);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(56, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1029, 600);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.richTextBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 80);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1029, 408);
            this.panel4.TabIndex = 1;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1029, 408);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.SelectionChanged += new System.EventHandler(this.richTextBox1_SelectionChanged);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.listView1);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel9.Location = new System.Drawing.Point(0, 488);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(1029, 63);
            this.panel9.TabIndex = 4;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1029, 63);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Window;
            this.panel5.Controls.Add(this.txtEmail);
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 41);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1029, 39);
            this.panel5.TabIndex = 2;
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmail.Location = new System.Drawing.Point(78, 0);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(951, 22);
            this.txtEmail.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Controls.Add(this.label1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(78, 39);
            this.panel7.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(138)))), ((int)(((byte)(136)))));
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "ALICI";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.txtkonu);
            this.panel6.Controls.Add(this.panel8);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1029, 41);
            this.panel6.TabIndex = 3;
            // 
            // txtkonu
            // 
            this.txtkonu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtkonu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtkonu.Location = new System.Drawing.Point(78, 0);
            this.txtkonu.Multiline = true;
            this.txtkonu.Name = "txtkonu";
            this.txtkonu.Size = new System.Drawing.Size(951, 41);
            this.txtkonu.TabIndex = 3;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.Controls.Add(this.label2);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(78, 41);
            this.panel8.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(138)))), ((int)(((byte)(136)))));
            this.label2.Location = new System.Drawing.Point(5, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "KONU";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.btntfolder);
            this.panel3.Controls.Add(this.BtnSend);
            this.panel3.Controls.Add(this.BTNRENK);
            this.panel3.Controls.Add(this.btnBoyutKucult);
            this.panel3.Controls.Add(this.BtnBoyutArtir);
            this.panel3.Controls.Add(this.cbAltiCizili);
            this.panel3.Controls.Add(this.cbKalin);
            this.panel3.Controls.Add(this.cbItalik);
            this.panel3.Controls.Add(this.cbBoyut);
            this.panel3.Controls.Add(this.cbFont);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 551);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1029, 49);
            this.panel3.TabIndex = 0;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(848, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "label3";
            // 
            // btntfolder
            // 
            this.btntfolder.Image = global::MailboxClient.Properties.Resources.search_11_24;
            this.btntfolder.Location = new System.Drawing.Point(576, 8);
            this.btntfolder.Name = "btntfolder";
            this.btntfolder.Size = new System.Drawing.Size(48, 35);
            this.btntfolder.TabIndex = 11;
            this.btntfolder.UseVisualStyleBackColor = true;
            this.btntfolder.Click += new System.EventHandler(this.btntfolder_Click);
            // 
            // BtnSend
            // 
            this.BtnSend.Location = new System.Drawing.Point(942, 19);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(75, 23);
            this.BtnSend.TabIndex = 10;
            this.BtnSend.Text = "button1";
            this.BtnSend.UseVisualStyleBackColor = true;
            this.BtnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // BTNRENK
            // 
            this.BTNRENK.Image = global::MailboxClient.Properties.Resources.text_bg_color_24;
            this.BTNRENK.Location = new System.Drawing.Point(512, 8);
            this.BTNRENK.Name = "BTNRENK";
            this.BTNRENK.Size = new System.Drawing.Size(58, 35);
            this.BTNRENK.TabIndex = 9;
            this.BTNRENK.UseVisualStyleBackColor = true;
            this.BTNRENK.Click += new System.EventHandler(this.BTNRENK_Click);
            // 
            // btnBoyutKucult
            // 
            this.btnBoyutKucult.Image = global::MailboxClient.Properties.Resources.decrease_font_24;
            this.btnBoyutKucult.Location = new System.Drawing.Point(454, 8);
            this.btnBoyutKucult.Name = "btnBoyutKucult";
            this.btnBoyutKucult.Size = new System.Drawing.Size(52, 33);
            this.btnBoyutKucult.TabIndex = 8;
            this.btnBoyutKucult.UseVisualStyleBackColor = true;
            this.btnBoyutKucult.Click += new System.EventHandler(this.btnBoyutKucult_Click);
            // 
            // BtnBoyutArtir
            // 
            this.BtnBoyutArtir.Image = global::MailboxClient.Properties.Resources.increase_font_24;
            this.BtnBoyutArtir.Location = new System.Drawing.Point(396, 7);
            this.BtnBoyutArtir.Name = "BtnBoyutArtir";
            this.BtnBoyutArtir.Size = new System.Drawing.Size(52, 33);
            this.BtnBoyutArtir.TabIndex = 7;
            this.BtnBoyutArtir.UseVisualStyleBackColor = true;
            this.BtnBoyutArtir.Click += new System.EventHandler(this.BtnBoyutArtir_Click);
            // 
            // cbAltiCizili
            // 
            this.cbAltiCizili.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbAltiCizili.Image = global::MailboxClient.Properties.Resources.line_2_16;
            this.cbAltiCizili.Location = new System.Drawing.Point(342, 7);
            this.cbAltiCizili.Name = "cbAltiCizili";
            this.cbAltiCizili.Size = new System.Drawing.Size(48, 35);
            this.cbAltiCizili.TabIndex = 6;
            this.cbAltiCizili.UseVisualStyleBackColor = true;
            this.cbAltiCizili.CheckedChanged += new System.EventHandler(this.cbAltiCizili_CheckedChanged);
            // 
            // cbKalin
            // 
            this.cbKalin.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbKalin.Image = global::MailboxClient.Properties.Resources.text_bold_16;
            this.cbKalin.Location = new System.Drawing.Point(234, 7);
            this.cbKalin.Name = "cbKalin";
            this.cbKalin.Size = new System.Drawing.Size(48, 35);
            this.cbKalin.TabIndex = 5;
            this.cbKalin.UseVisualStyleBackColor = true;
            this.cbKalin.CheckedChanged += new System.EventHandler(this.cbKalin_CheckedChanged);
            // 
            // cbItalik
            // 
            this.cbItalik.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbItalik.Image = global::MailboxClient.Properties.Resources.text_italic_16;
            this.cbItalik.Location = new System.Drawing.Point(288, 7);
            this.cbItalik.Name = "cbItalik";
            this.cbItalik.Size = new System.Drawing.Size(48, 35);
            this.cbItalik.TabIndex = 4;
            this.cbItalik.UseVisualStyleBackColor = true;
            this.cbItalik.CheckedChanged += new System.EventHandler(this.cbItalik_CheckedChanged);
            // 
            // cbBoyut
            // 
            this.cbBoyut.FormattingEnabled = true;
            this.cbBoyut.Items.AddRange(new object[] {
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50"});
            this.cbBoyut.Location = new System.Drawing.Point(168, 13);
            this.cbBoyut.Name = "cbBoyut";
            this.cbBoyut.Size = new System.Drawing.Size(60, 24);
            this.cbBoyut.TabIndex = 1;
            this.cbBoyut.Text = "12";
            this.cbBoyut.SelectedIndexChanged += new System.EventHandler(this.cbBoyut_SelectedIndexChanged);
            // 
            // cbFont
            // 
            this.cbFont.FormattingEnabled = true;
            this.cbFont.Items.AddRange(new object[] {
            "Arial",
            "Microsoft Sans Serif",
            "Comic Sans",
            "Tahoma",
            "Trebuchet",
            "Verdana"});
            this.cbFont.Location = new System.Drawing.Point(6, 13);
            this.cbFont.Name = "cbFont";
            this.cbFont.Size = new System.Drawing.Size(156, 24);
            this.cbFont.TabIndex = 0;
            this.cbFont.Text = "Microsoft Sans Serif";
            this.cbFont.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbFont_DrawItem);
            this.cbFont.SelectedIndexChanged += new System.EventHandler(this.cbFont_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // SendMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 600);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SendMail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SendMail";
            this.Load += new System.EventHandler(this.SendMail_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BrnClose;
        public System.Windows.Forms.Button BtnMinimized;
        public System.Windows.Forms.Button BtnMaxsimized;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cbFont;
        private System.Windows.Forms.ComboBox cbBoyut;
        private System.Windows.Forms.CheckBox cbItalik;
        private System.Windows.Forms.CheckBox cbKalin;
        private System.Windows.Forms.CheckBox cbAltiCizili;
        private System.Windows.Forms.Button BtnBoyutArtir;
        private System.Windows.Forms.Button btnBoyutKucult;
        private System.Windows.Forms.Button BTNRENK;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtkonu;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnSend;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btntfolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.ListView listView1;
    }
}