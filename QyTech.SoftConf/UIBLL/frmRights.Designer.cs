namespace QyTech.SoftConf.UIBLL
{
    partial class frmRights
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRights));
            this.qytvRights = new QyTech.SkinForm.Controls.qyTreeView();
            this.qyBtn_Ok = new QyTech.SkinForm.Component.qyBtn_ok();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // qytvRights
            // 
            this.qytvRights.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.qytvRights.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.qytvRights.Location = new System.Drawing.Point(12, 12);
            this.qytvRights.Name = "qytvRights";
            this.qytvRights.NodeCheckedWithParent = false;
            this.qytvRights.Size = new System.Drawing.Size(279, 395);
            this.qytvRights.TabIndex = 0;
            // 
            // qyBtn_Ok
            // 
            this.qyBtn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.qyBtn_Ok.Image = ((System.Drawing.Image)(resources.GetObject("qyBtn_Ok.Image")));
            this.qyBtn_Ok.Location = new System.Drawing.Point(180, 414);
            this.qyBtn_Ok.Name = "qyBtn_Ok";
            this.qyBtn_Ok.Size = new System.Drawing.Size(75, 23);
            this.qyBtn_Ok.TabIndex = 1;
            this.qyBtn_Ok.Text = "确定";
            this.qyBtn_Ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.qyBtn_Ok.UseVisualStyleBackColor = true;
            this.qyBtn_Ok.Click += new System.EventHandler(this.qyBtn_Ok_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 418);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "联动";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // frmRights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 450);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.qyBtn_Ok);
            this.Controls.Add(this.qytvRights);
            this.Name = "frmRights";
            this.Text = "角色功能权限";
            this.Load += new System.EventHandler(this.frmRoleFuns_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SkinForm.Controls.qyTreeView qytvRights;
        private SkinForm.Component.qyBtn_ok qyBtn_Ok;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}