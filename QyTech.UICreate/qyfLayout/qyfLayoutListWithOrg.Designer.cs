namespace QyTech.UICreate
{
    partial class qyfLayoutListWithOrg
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(qyfLayoutListWithOrg));
            this.scForm = new System.Windows.Forms.SplitContainer();
            this.treeOrg = new QyTech.SkinForm.Component.qyTvOrg();
            this.scDgv = new System.Windows.Forms.SplitContainer();
            this.scQuery = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsPbr = new System.Windows.Forms.ToolStripProgressBar();
            this.qyDgvList = new QyTech.SkinForm.Controls.qyDgv();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.qyBtn_Refresh = new QyTech.SkinForm.Component.qyBtn_Search();
            this.gbCondition = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.scForm)).BeginInit();
            this.scForm.Panel1.SuspendLayout();
            this.scForm.Panel2.SuspendLayout();
            this.scForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scDgv)).BeginInit();
            this.scDgv.Panel1.SuspendLayout();
            this.scDgv.Panel2.SuspendLayout();
            this.scDgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scQuery)).BeginInit();
            this.scQuery.Panel1.SuspendLayout();
            this.scQuery.Panel2.SuspendLayout();
            this.scQuery.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qyDgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // scForm
            // 
            this.scForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scForm.Location = new System.Drawing.Point(0, 28);
            this.scForm.Name = "scForm";
            // 
            // scForm.Panel1
            // 
            this.scForm.Panel1.Controls.Add(this.treeOrg);
            // 
            // scForm.Panel2
            // 
            this.scForm.Panel2.Controls.Add(this.scDgv);
            this.scForm.Size = new System.Drawing.Size(800, 422);
            this.scForm.SplitterDistance = 214;
            this.scForm.TabIndex = 5;
            // 
            // treeOrg
            // 
            this.treeOrg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeOrg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeOrg.Location = new System.Drawing.Point(0, 0);
            this.treeOrg.Name = "treeOrg";
            this.treeOrg.Size = new System.Drawing.Size(214, 422);
            this.treeOrg.TabIndex = 19;
            this.treeOrg.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeOrg_AfterCheck);
            this.treeOrg.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeOrg_AfterSelect);
            // 
            // scDgv
            // 
            this.scDgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scDgv.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scDgv.Location = new System.Drawing.Point(0, 0);
            this.scDgv.Name = "scDgv";
            this.scDgv.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scDgv.Panel1
            // 
            this.scDgv.Panel1.Controls.Add(this.scQuery);
            // 
            // scDgv.Panel2
            // 
            this.scDgv.Panel2.Controls.Add(this.qyDgvList);
            this.scDgv.Size = new System.Drawing.Size(582, 422);
            this.scDgv.SplitterDistance = 102;
            this.scDgv.TabIndex = 0;
            // 
            // scQuery
            // 
            this.scQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scQuery.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scQuery.Location = new System.Drawing.Point(0, 0);
            this.scQuery.Name = "scQuery";
            this.scQuery.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scQuery.Panel1
            // 
            this.scQuery.Panel1.Controls.Add(this.qyBtn_Refresh);
            this.scQuery.Panel1.Controls.Add(this.gbCondition);
            this.scQuery.Panel1MinSize = 0;
            // 
            // scQuery.Panel2
            // 
            this.scQuery.Panel2.Controls.Add(this.toolStrip1);
            this.scQuery.Panel2MinSize = 0;
            this.scQuery.Size = new System.Drawing.Size(582, 102);
            this.scQuery.SplitterDistance = 34;
            this.scQuery.TabIndex = 12;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbEdit,
            this.tsbDelete,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tsPbr});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(582, 64);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // tsbAdd
            // 
            this.tsbAdd.AutoSize = false;
            this.tsbAdd.Enabled = false;
            this.tsbAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdd.Image")));
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(55, 25);
            this.tsbAdd.Text = "新增";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbEdit
            // 
            this.tsbEdit.AutoSize = false;
            this.tsbEdit.Enabled = false;
            this.tsbEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsbEdit.Image")));
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(55, 25);
            this.tsbEdit.Text = "修改";
            this.tsbEdit.ToolTipText = "修改";
            this.tsbEdit.Click += new System.EventHandler(this.tsbEdit_Click);
            // 
            // tsbDelete
            // 
            this.tsbDelete.AutoSize = false;
            this.tsbDelete.Enabled = false;
            this.tsbDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbDelete.Image")));
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(55, 25);
            this.tsbDelete.Text = "删除";
            this.tsbDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 64);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(48, 61);
            this.toolStripLabel1.Text = "          ";
            // 
            // tsPbr
            // 
            this.tsPbr.Name = "tsPbr";
            this.tsPbr.Size = new System.Drawing.Size(200, 26);
            this.tsPbr.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.tsPbr.ToolTipText = "0/100";
            this.tsPbr.Visible = false;
            this.tsPbr.MouseHover += new System.EventHandler(this.tsPbr_MouseHover);
            // 
            // qyDgvList
            // 
            this.qyDgvList.AllowUserToAddRows = false;
            this.qyDgvList.AllowUserToDeleteRows = false;
            this.qyDgvList.AllowUserToResizeColumns = false;
            this.qyDgvList.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.qyDgvList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.qyDgvList.BackgroundColor = System.Drawing.Color.White;
            this.qyDgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.qyDgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.qyDgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.qyDgvList.Location = new System.Drawing.Point(0, 0);
            this.qyDgvList.Name = "qyDgvList";
            this.qyDgvList.RowTemplate.Height = 23;
            this.qyDgvList.Size = new System.Drawing.Size(582, 316);
            this.qyDgvList.TabIndex = 10;
            this.qyDgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.qyDgvList_CellClick);
            this.qyDgvList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.qyDgvList_CellValueChanged);
            this.qyDgvList.CurrentCellDirtyStateChanged += new System.EventHandler(this.qyDgvList_CurrentCellDirtyStateChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "选择";
            this.Column1.Name = "Column1";
            this.Column1.Width = 40;
            // 
            // qyBtn_Refresh
            // 
            this.qyBtn_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.qyBtn_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("qyBtn_Refresh.Image")));
            this.qyBtn_Refresh.Location = new System.Drawing.Point(501, 8);
            this.qyBtn_Refresh.Name = "qyBtn_Refresh";
            this.qyBtn_Refresh.Size = new System.Drawing.Size(69, 23);
            this.qyBtn_Refresh.TabIndex = 5;
            this.qyBtn_Refresh.Text = "刷新";
            this.qyBtn_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.qyBtn_Refresh.UseVisualStyleBackColor = true;
            // 
            // gbCondition
            // 
            this.gbCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCondition.Location = new System.Drawing.Point(3, 0);
            this.gbCondition.Name = "gbCondition";
            this.gbCondition.Size = new System.Drawing.Size(492, 34);
            this.gbCondition.TabIndex = 4;
            this.gbCondition.TabStop = false;
            // 
            // qyfLayoutListWithOrg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.scForm);
            this.Name = "qyfLayoutListWithOrg";
            this.Text = "frmList";
            this.Load += new System.EventHandler(this.frmListWithLeft_Load);
            this.Controls.SetChildIndex(this.scForm, 0);
            this.scForm.Panel1.ResumeLayout(false);
            this.scForm.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scForm)).EndInit();
            this.scForm.ResumeLayout(false);
            this.scDgv.Panel1.ResumeLayout(false);
            this.scDgv.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scDgv)).EndInit();
            this.scDgv.ResumeLayout(false);
            this.scQuery.Panel1.ResumeLayout(false);
            this.scQuery.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scQuery)).EndInit();
            this.scQuery.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qyDgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.SplitContainer scForm;
        public System.Windows.Forms.SplitContainer scDgv;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        protected System.Windows.Forms.ToolStripButton tsbEdit;
        protected System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        protected System.Windows.Forms.ToolStripProgressBar tsPbr;
        protected QyTech.SkinForm.Controls.qyDgv qyDgvList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private QyTech.SkinForm.Component.qyTvOrg treeOrg;
        public System.Windows.Forms.SplitContainer scQuery;
        public SkinForm.Component.qyBtn_Search qyBtn_Refresh;
        public System.Windows.Forms.GroupBox gbCondition;
    }
}