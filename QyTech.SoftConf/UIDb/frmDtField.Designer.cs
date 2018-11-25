namespace QyTech.SoftConf.UIList
{
    partial class frmDtField
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDtField));
            this.qytvDbTable = new QyTech.SkinForm.Controls.qyTreeView();
            this.tsToolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDelNoValidField = new System.Windows.Forms.ToolStripButton();
            this.tsbAddNewField = new System.Windows.Forms.ToolStripButton();
            this.tsbCreate = new System.Windows.Forms.ToolStripButton();
            this.tsbInitColunn = new System.Windows.Forms.ToolStripButton();
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
            this.qyBtn_Refresh.Location = new System.Drawing.Point(609, 6);
            // 
            // gbCondition
            // 
            this.gbCondition.Location = new System.Drawing.Point(3, 0);
            this.gbCondition.Size = new System.Drawing.Size(200, 31);
            // 
            // qytvDbTable
            // 
            this.qytvDbTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.qytvDbTable.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.qytvDbTable.Location = new System.Drawing.Point(0, 0);
            this.qytvDbTable.Name = "qytvDbTable";
            this.qytvDbTable.NodeCheckedWithParent = false;
            this.qytvDbTable.Size = new System.Drawing.Size(225, 422);
            this.qytvDbTable.TabIndex = 0;
            this.qytvDbTable.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.qytvDbTable_AfterSelect);
            // 
            // tsToolBar
            // 
            this.tsToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.tsbDelNoValidField,
            this.tsbAddNewField,
            this.tsbCreate,
            this.tsbInitColunn});
            this.tsToolBar.Location = new System.Drawing.Point(0, 0);
            this.tsToolBar.Name = "tsToolBar";
            this.tsToolBar.Size = new System.Drawing.Size(701, 25);
            this.tsToolBar.TabIndex = 15;
            this.tsToolBar.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbDelNoValidField
            // 
            this.tsbDelNoValidField.Image = ((System.Drawing.Image)(resources.GetObject("tsbDelNoValidField.Image")));
            this.tsbDelNoValidField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelNoValidField.Name = "tsbDelNoValidField";
            this.tsbDelNoValidField.Size = new System.Drawing.Size(100, 22);
            this.tsbDelNoValidField.Text = "删除失效字段";
            this.tsbDelNoValidField.Click += new System.EventHandler(this.tsbDelNoValidField_Click);
            // 
            // tsbAddNewField
            // 
            this.tsbAddNewField.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddNewField.Image")));
            this.tsbAddNewField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddNewField.Name = "tsbAddNewField";
            this.tsbAddNewField.Size = new System.Drawing.Size(100, 22);
            this.tsbAddNewField.Text = "补充新增字段";
            this.tsbAddNewField.Click += new System.EventHandler(this.tsbAddNewField_Click);
            // 
            // tsbCreate
            // 
            this.tsbCreate.Image = ((System.Drawing.Image)(resources.GetObject("tsbCreate.Image")));
            this.tsbCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCreate.Name = "tsbCreate";
            this.tsbCreate.Size = new System.Drawing.Size(100, 22);
            this.tsbCreate.Text = "修改创建字段";
            this.tsbCreate.Click += new System.EventHandler(this.tsbCreate_Click);
            // 
            // tsbInitColunn
            // 
            this.tsbInitColunn.Image = ((System.Drawing.Image)(resources.GetObject("tsbInitColunn.Image")));
            this.tsbInitColunn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInitColunn.Name = "tsbInitColunn";
            this.tsbInitColunn.Size = new System.Drawing.Size(100, 22);
            this.tsbInitColunn.Text = "初始化列信息";
            this.tsbInitColunn.Click += new System.EventHandler(this.tsbInitColunn_Click);
            // 
            // frmDtField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 450);
            this.Name = "frmDtField";
            this.Text = "frmDtTable";
            this.Load += new System.EventHandler(this.frmDtField_Load);
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbDelNoValidField;
        private System.Windows.Forms.ToolStripButton tsbAddNewField;
        private System.Windows.Forms.ToolStripButton tsbCreate;
        private System.Windows.Forms.ToolStripButton tsbInitColunn;
    }
}