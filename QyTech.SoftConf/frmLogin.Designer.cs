namespace QyTech.SoftConf
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.UserName = new System.Windows.Forms.ComboBox();
            this.RemeberPassword = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLoad = new System.Windows.Forms.Label();
            this.pbrLoad = new System.Windows.Forms.ProgressBar();
            this.exButton2 = new System.Windows.Forms.Button();
            this.exButton1 = new System.Windows.Forms.Button();
            this.PassWord = new System.Windows.Forms.TextBox();
            this.exLabel2 = new System.Windows.Forms.Label();
            this.exLabel1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 293);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.UserName);
            this.panel1.Controls.Add(this.RemeberPassword);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblLoad);
            this.panel1.Controls.Add(this.pbrLoad);
            this.panel1.Controls.Add(this.exButton2);
            this.panel1.Controls.Add(this.exButton1);
            this.panel1.Controls.Add(this.PassWord);
            this.panel1.Controls.Add(this.exLabel2);
            this.panel1.Controls.Add(this.exLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 293);
            this.panel1.TabIndex = 7;
            // 
            // UserName
            // 
            this.UserName.FormattingEnabled = true;
            this.UserName.Location = new System.Drawing.Point(123, 99);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(121, 20);
            this.UserName.TabIndex = 24;
            this.UserName.SelectedValueChanged += new System.EventHandler(this.UserName_SelectedValueChanged);
            // 
            // RemeberPassword
            // 
            this.RemeberPassword.AutoSize = true;
            this.RemeberPassword.BackColor = System.Drawing.Color.Transparent;
            this.RemeberPassword.Checked = true;
            this.RemeberPassword.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RemeberPassword.Location = new System.Drawing.Point(173, 162);
            this.RemeberPassword.Name = "RemeberPassword";
            this.RemeberPassword.Size = new System.Drawing.Size(72, 16);
            this.RemeberPassword.TabIndex = 23;
            this.RemeberPassword.Text = "记住密码";
            this.RemeberPassword.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(223, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 22;
            this.label3.Text = "V1.0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Font = new System.Drawing.Font("楷体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(24, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 29);
            this.label1.TabIndex = 21;
            this.label1.Text = "快速开发平台";
            // 
            // lblLoad
            // 
            this.lblLoad.AutoSize = true;
            this.lblLoad.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLoad.Location = new System.Drawing.Point(36, 253);
            this.lblLoad.Name = "lblLoad";
            this.lblLoad.Size = new System.Drawing.Size(64, 16);
            this.lblLoad.TabIndex = 20;
            this.lblLoad.Text = "Loading";
            this.lblLoad.Visible = false;
            // 
            // pbrLoad
            // 
            this.pbrLoad.Location = new System.Drawing.Point(105, 254);
            this.pbrLoad.Maximum = 50;
            this.pbrLoad.Name = "pbrLoad";
            this.pbrLoad.Size = new System.Drawing.Size(172, 15);
            this.pbrLoad.TabIndex = 19;
            this.pbrLoad.Visible = false;
            // 
            // exButton2
            // 
            this.exButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exButton2.ForeColor = System.Drawing.Color.Black;
            this.exButton2.Location = new System.Drawing.Point(173, 210);
            this.exButton2.Name = "exButton2";
            this.exButton2.Size = new System.Drawing.Size(90, 25);
            this.exButton2.TabIndex = 18;
            this.exButton2.Text = "退出";
            this.exButton2.Click += new System.EventHandler(this.exButton2_Click);
            // 
            // exButton1
            // 
            this.exButton1.ForeColor = System.Drawing.Color.Black;
            this.exButton1.Location = new System.Drawing.Point(50, 211);
            this.exButton1.Name = "exButton1";
            this.exButton1.Size = new System.Drawing.Size(90, 25);
            this.exButton1.TabIndex = 17;
            this.exButton1.Text = "登录";
            this.exButton1.Click += new System.EventHandler(this.exButton1_Click);
            // 
            // PassWord
            // 
            this.PassWord.Location = new System.Drawing.Point(123, 135);
            this.PassWord.Name = "PassWord";
            this.PassWord.PasswordChar = '*';
            this.PassWord.Size = new System.Drawing.Size(121, 21);
            this.PassWord.TabIndex = 16;
            this.PassWord.TextChanged += new System.EventHandler(this.tbPassword_TextChanged);
            // 
            // exLabel2
            // 
            this.exLabel2.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exLabel2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.exLabel2.Location = new System.Drawing.Point(55, 135);
            this.exLabel2.Name = "exLabel2";
            this.exLabel2.Size = new System.Drawing.Size(72, 20);
            this.exLabel2.TabIndex = 14;
            this.exLabel2.Text = "密  码：";
            // 
            // exLabel1
            // 
            this.exLabel1.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exLabel1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.exLabel1.Location = new System.Drawing.Point(55, 100);
            this.exLabel1.Name = "exLabel1";
            this.exLabel1.Size = new System.Drawing.Size(72, 20);
            this.exLabel1.TabIndex = 13;
            this.exLabel1.Text = "用户名：";
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(300, 293);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox RemeberPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLoad;
        private System.Windows.Forms.ProgressBar pbrLoad;
        private System.Windows.Forms.Button exButton2;
        private System.Windows.Forms.Button exButton1;
        private System.Windows.Forms.TextBox PassWord;
        private System.Windows.Forms.Label exLabel2;
        private System.Windows.Forms.Label exLabel1;
        private System.Windows.Forms.ComboBox UserName;
    }
}