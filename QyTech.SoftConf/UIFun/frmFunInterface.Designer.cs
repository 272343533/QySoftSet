namespace QyTech.SoftConf.UIList
{
    partial class frmFunInterface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFunInterface));
            this.qytvDbTable = new QyTech.SkinForm.Controls.qyTreeView();
            this.tsToolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tsbAddTDefault = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
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
            this.qyBtn_Refresh.Location = new System.Drawing.Point(490, 8);
            this.qyBtn_Refresh.Size = new System.Drawing.Size(123, 23);
            // 
            // gbCondition
            // 
            this.gbCondition.Location = new System.Drawing.Point(3, 0);
            this.gbCondition.Size = new System.Drawing.Size(382, 31);
            // 
            // qytvDbTable
            // 
            this.qytvDbTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.qytvDbTable.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.qytvDbTable.Location = new System.Drawing.Point(0, 0);
            this.qytvDbTable.Name = "qytvDbTable";
            this.qytvDbTable.Size = new System.Drawing.Size(225, 422);
            this.qytvDbTable.TabIndex = 0;
            this.qytvDbTable.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.qytvDbTable_AfterSelect);
            // 
            // tsToolBar
            // 
            this.tsToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.tsbAddTDefault,
            this.toolStripSeparator1});
            this.tsToolBar.Location = new System.Drawing.Point(0, 0);
            this.tsToolBar.Name = "tsToolBar";
            this.tsToolBar.Size = new System.Drawing.Size(701, 25);
            this.tsToolBar.TabIndex = 16;
            this.tsToolBar.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton1.Text = "新增";
            // 
            // tsbAddTDefault
            // 
            this.tsbAddTDefault.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddTDefault.Image")));
            this.tsbAddTDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddTDefault.Name = "tsbAddTDefault";
            this.tsbAddTDefault.Size = new System.Drawing.Size(100, 22);
            this.tsbAddTDefault.Text = "增加默认接口";
            this.tsbAddTDefault.Click += new System.EventHandler(this.tsbAddTDefault_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // frmFunInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 450);
            this.Name = "frmFunInterface";
            this.Text = "frmDtTable";
            this.Load += new System.EventHandler(this.frm_Load);
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
        private System.Windows.Forms.ToolStripButton tsbAddTDefault;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}