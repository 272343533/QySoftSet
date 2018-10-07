namespace QyTech.SoftConf.UIList
{
    partial class frmNaviagtion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNaviagtion));
            this.tsToolBar = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRefreshTree = new System.Windows.Forms.ToolStripButton();
            this.tsbInitFunConf = new System.Windows.Forms.ToolStripButton();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.qytvDbTable = new QyTech.SkinForm.Controls.qyTreeView();
            ((System.ComponentModel.ISupportInitialize)(this.scForm)).BeginInit();
            this.scForm.Panel1.SuspendLayout();
            this.scForm.Panel2.SuspendLayout();
            this.scForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scDgv)).BeginInit();
            this.scDgv.Panel1.SuspendLayout();
            this.scDgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scQuery)).BeginInit();
            this.scQuery.Panel1.SuspendLayout();
            this.scQuery.Panel2.SuspendLayout();
            this.scQuery.SuspendLayout();
            this.tsToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // scForm
            // 
            this.scForm.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            // 
            // scForm.Panel1
            // 
            this.scForm.Panel1.Controls.Add(this.qytvDbTable);
            this.scForm.Size = new System.Drawing.Size(930, 422);
            this.scForm.SplitterDistance = 225;
            // 
            // scDgv
            // 
            this.scDgv.Size = new System.Drawing.Size(701, 422);
            // 
            // scQuery
            // 
            // 
            // scQuery.Panel2
            // 
            this.scQuery.Panel2.Controls.Add(this.tsToolBar);
            this.scQuery.Size = new System.Drawing.Size(701, 68);
            // 
            // qyBtn_Refresh
            // 
            this.qyBtn_Refresh.Location = new System.Drawing.Point(609, 8);
            // 
            // gbCondition
            // 
            this.gbCondition.Size = new System.Drawing.Size(483, 34);
            // 
            // tsToolBar
            // 
            this.tsToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.toolStripSeparator1,
            this.tsbRefreshTree,
            this.tsbInitFunConf});
            this.tsToolBar.Location = new System.Drawing.Point(0, 0);
            this.tsToolBar.Name = "tsToolBar";
            this.tsToolBar.Size = new System.Drawing.Size(701, 25);
            this.tsToolBar.TabIndex = 14;
            this.tsToolBar.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdd.Image")));
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(52, 22);
            this.tsbAdd.Text = "新增";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbRefreshTree
            // 
            this.tsbRefreshTree.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefreshTree.Image")));
            this.tsbRefreshTree.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefreshTree.Name = "tsbRefreshTree";
            this.tsbRefreshTree.Size = new System.Drawing.Size(64, 22);
            this.tsbRefreshTree.Text = "刷新树";
            this.tsbRefreshTree.Click += new System.EventHandler(this.tsbRefreshTree_Click);
            // 
            // tsbInitFunConf
            // 
            this.tsbInitFunConf.Image = ((System.Drawing.Image)(resources.GetObject("tsbInitFunConf.Image")));
            this.tsbInitFunConf.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInitFunConf.Name = "tsbInitFunConf";
            this.tsbInitFunConf.Size = new System.Drawing.Size(88, 22);
            this.tsbInitFunConf.Text = "初始化配置";
            this.tsbInitFunConf.Click += new System.EventHandler(this.tsbInitFunConf_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(74, 10);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(186, 21);
            this.txtName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "导航名称";
            // 
            // qytvDbTable
            // 
            this.qytvDbTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.qytvDbTable.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.qytvDbTable.Location = new System.Drawing.Point(0, 0);
            this.qytvDbTable.Name = "qytvDbTable";
            this.qytvDbTable.Size = new System.Drawing.Size(225, 422);
            this.qytvDbTable.TabIndex = 0;
            this.qytvDbTable.eventDragDroped += new QyTech.SkinForm.Controls.qyTreeView.delDragHandler(this.qytvDbTable_eventDragDroped);
            this.qytvDbTable.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.qytvDbTable_AfterSelect);
            // 
            // frmNaviagtion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 450);
            this.Name = "frmNaviagtion";
            this.Text = "frmDtTable";
            this.Load += new System.EventHandler(this.frmNaviagtion_Load);
            this.scForm.Panel1.ResumeLayout(false);
            this.scForm.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scForm)).EndInit();
            this.scForm.ResumeLayout(false);
            this.scDgv.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scDgv)).EndInit();
            this.scDgv.ResumeLayout(false);
            this.scQuery.Panel1.ResumeLayout(false);
            this.scQuery.Panel2.ResumeLayout(false);
            this.scQuery.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scQuery)).EndInit();
            this.scQuery.ResumeLayout(false);
            this.tsToolBar.ResumeLayout(false);
            this.tsToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SkinForm.Controls.qyTreeView qytvDbTable;
        private System.Windows.Forms.ToolStrip tsToolBar;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tsbRefreshTree;
        private System.Windows.Forms.ToolStripButton tsbInitFunConf;
    }
}