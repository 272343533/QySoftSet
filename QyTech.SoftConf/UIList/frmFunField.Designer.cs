namespace QyTech.SoftConf.UIList
{
    partial class frmFunField
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFunField));
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.qytvDbTable = new QyTech.SkinForm.Controls.qyTreeView();
            this.tsToolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSetQueryField = new System.Windows.Forms.ToolStripButton();
            this.tsbAddNewField = new System.Windows.Forms.ToolStripButton();
            this.tsbDels = new System.Windows.Forms.ToolStripButton();
            this.tsDDbOrder = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiListLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiListRight = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditUp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditDown = new System.Windows.Forms.ToolStripMenuItem();
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
            this.gbCondition.SuspendLayout();
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
            this.scForm.Size = new System.Drawing.Size(1015, 422);
            this.scForm.SplitterDistance = 225;
            // 
            // scDgv
            // 
            this.scDgv.Size = new System.Drawing.Size(786, 422);
            // 
            // scQuery
            // 
            // 
            // scQuery.Panel2
            // 
            this.scQuery.Panel2.Controls.Add(this.tsToolBar);
            this.scQuery.Size = new System.Drawing.Size(786, 68);
            // 
            // qyBtn_Refresh
            // 
            this.qyBtn_Refresh.Location = new System.Drawing.Point(598, 8);
            this.qyBtn_Refresh.Click += new System.EventHandler(this.qyBtn_Refresh_Click);
            // 
            // gbCondition
            // 
            this.gbCondition.Controls.Add(this.txtName);
            this.gbCondition.Controls.Add(this.label1);
            this.gbCondition.Location = new System.Drawing.Point(3, 0);
            this.gbCondition.Size = new System.Drawing.Size(577, 31);
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
            this.label1.Text = "字段名称";
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
            this.toolStripSeparator1,
            this.tsbSetQueryField,
            this.tsbAddNewField,
            this.tsbDels,
            this.tsDDbOrder});
            this.tsToolBar.Location = new System.Drawing.Point(0, 0);
            this.tsToolBar.Name = "tsToolBar";
            this.tsToolBar.Size = new System.Drawing.Size(786, 25);
            this.tsToolBar.TabIndex = 15;
            this.tsToolBar.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSetQueryField
            // 
            this.tsbSetQueryField.Image = ((System.Drawing.Image)(resources.GetObject("tsbSetQueryField.Image")));
            this.tsbSetQueryField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSetQueryField.Name = "tsbSetQueryField";
            this.tsbSetQueryField.Size = new System.Drawing.Size(76, 22);
            this.tsbSetQueryField.Text = "设为查询";
            this.tsbSetQueryField.Click += new System.EventHandler(this.tsbSetQueryField_Click);
            // 
            // tsbAddNewField
            // 
            this.tsbAddNewField.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddNewField.Image")));
            this.tsbAddNewField.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddNewField.Name = "tsbAddNewField";
            this.tsbAddNewField.Size = new System.Drawing.Size(76, 22);
            this.tsbAddNewField.Text = "补充字段";
            this.tsbAddNewField.Click += new System.EventHandler(this.tsbAddNewField_Click);
            // 
            // tsbDels
            // 
            this.tsbDels.Image = ((System.Drawing.Image)(resources.GetObject("tsbDels.Image")));
            this.tsbDels.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDels.Name = "tsbDels";
            this.tsbDels.Size = new System.Drawing.Size(76, 22);
            this.tsbDels.Text = "选择删除";
            this.tsbDels.ToolTipText = "删除选择的行";
            this.tsbDels.Visible = false;
            this.tsbDels.Click += new System.EventHandler(this.tsbDels_Click);
            // 
            // tsDDbOrder
            // 
            this.tsDDbOrder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiListLeft,
            this.tsmiListRight,
            this.tsmiEditUp,
            this.tsmiEditDown});
            this.tsDDbOrder.Image = ((System.Drawing.Image)(resources.GetObject("tsDDbOrder.Image")));
            this.tsDDbOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDDbOrder.Name = "tsDDbOrder";
            this.tsDDbOrder.Size = new System.Drawing.Size(85, 22);
            this.tsDDbOrder.Text = "顺序调整";
            // 
            // tsmiListLeft
            // 
            this.tsmiListLeft.Name = "tsmiListLeft";
            this.tsmiListLeft.Size = new System.Drawing.Size(124, 22);
            this.tsmiListLeft.Text = "浏览前移";
            this.tsmiListLeft.Click += new System.EventHandler(this.tsmiListLeft_Click);
            // 
            // tsmiListRight
            // 
            this.tsmiListRight.Name = "tsmiListRight";
            this.tsmiListRight.Size = new System.Drawing.Size(124, 22);
            this.tsmiListRight.Text = "浏览后移";
            this.tsmiListRight.Click += new System.EventHandler(this.tsmiListRight_Click);
            // 
            // tsmiEditUp
            // 
            this.tsmiEditUp.Name = "tsmiEditUp";
            this.tsmiEditUp.Size = new System.Drawing.Size(124, 22);
            this.tsmiEditUp.Text = "编辑前移";
            this.tsmiEditUp.Click += new System.EventHandler(this.tsmiEditUp_Click);
            // 
            // tsmiEditDown
            // 
            this.tsmiEditDown.Name = "tsmiEditDown";
            this.tsmiEditDown.Size = new System.Drawing.Size(124, 22);
            this.tsmiEditDown.Text = "编辑后移";
            this.tsmiEditDown.Click += new System.EventHandler(this.tsmiEditDown_Click);
            // 
            // frmFunField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 450);
            this.Name = "frmFunField";
            this.Text = "frmDtTable";
            this.Load += new System.EventHandler(this.frmFunField_Load);
            this.Controls.SetChildIndex(this.scForm, 0);
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
            this.gbCondition.ResumeLayout(false);
            this.gbCondition.PerformLayout();
            this.tsToolBar.ResumeLayout(false);
            this.tsToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SkinForm.Controls.qyTreeView qytvDbTable;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip tsToolBar;
        private System.Windows.Forms.ToolStripButton tsbDels;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbSetQueryField;
        private System.Windows.Forms.ToolStripButton tsbAddNewField;
        private System.Windows.Forms.ToolStripDropDownButton tsDDbOrder;
        private System.Windows.Forms.ToolStripMenuItem tsmiListLeft;
        private System.Windows.Forms.ToolStripMenuItem tsmiListRight;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditUp;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditDown;
    }
}