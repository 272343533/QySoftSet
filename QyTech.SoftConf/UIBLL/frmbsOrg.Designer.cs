namespace QyTech.SoftConf.UIBLL
{
    partial class frmbsOrg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmbsOrg));
            this.cboType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.qyBtn_RefreshTree = new QyTech.SkinForm.Component.qyBtn_Search();
            ((System.ComponentModel.ISupportInitialize)(this.scForm)).BeginInit();
            this.scForm.Panel1.SuspendLayout();
            this.scForm.Panel2.SuspendLayout();
            this.scForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // scForm
            // 
            // 
            // scForm.Panel1
            // 
            this.scForm.Panel1.Controls.Add(this.cboType);
            this.scForm.Panel1.Controls.Add(this.label1);
            this.scForm.Panel1.Controls.Add(this.qyBtn_RefreshTree);
            this.scForm.Size = new System.Drawing.Size(631, 422);
            this.scForm.SplitterDistance = 241;
            // 
            // qytvLeft
            // 
            this.qytvLeft.LineColor = System.Drawing.Color.Black;
            this.qytvLeft.Location = new System.Drawing.Point(3, 36);
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Items.AddRange(new object[] {
            "",
            "开发区",
            "行政",
            "科室"});
            this.cboType.Location = new System.Drawing.Point(80, 9);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(97, 20);
            this.cboType.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "分类";
            // 
            // qyBtn_RefreshTree
            // 
            this.qyBtn_RefreshTree.Image = ((System.Drawing.Image)(resources.GetObject("qyBtn_RefreshTree.Image")));
            this.qyBtn_RefreshTree.Location = new System.Drawing.Point(195, 6);
            this.qyBtn_RefreshTree.Name = "qyBtn_RefreshTree";
            this.qyBtn_RefreshTree.Size = new System.Drawing.Size(56, 23);
            this.qyBtn_RefreshTree.TabIndex = 4;
            this.qyBtn_RefreshTree.Text = "刷新";
            this.qyBtn_RefreshTree.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.qyBtn_RefreshTree.UseVisualStyleBackColor = true;
            this.qyBtn_RefreshTree.Click += new System.EventHandler(this.qyBtn_RefreshTree_Click);
            // 
            // frmbsOrg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 450);
            this.Name = "frmbsOrg";
            this.Text = "frmbsOrg";
            this.Load += new System.EventHandler(this.frmbsOrg_Load);
            this.scForm.Panel1.ResumeLayout(false);
            this.scForm.Panel1.PerformLayout();
            this.scForm.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scForm)).EndInit();
            this.scForm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label label1;
        private SkinForm.Component.qyBtn_Search qyBtn_RefreshTree;
    }
}