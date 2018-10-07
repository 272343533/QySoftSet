namespace QyTech.SoftConf.UIList
{
    partial class frmFunConf
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
            this.scQuery.SuspendLayout();
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
            this.scQuery.Size = new System.Drawing.Size(701, 68);
            // 
            // qyBtn_Refresh
            // 
            this.qyBtn_Refresh.Location = new System.Drawing.Point(609, 6);
            // 
            // gbCondition
            // 
            this.gbCondition.Location = new System.Drawing.Point(3, 0);
            this.gbCondition.Size = new System.Drawing.Size(560, 31);
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
            // frmFunConf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 450);
            this.Name = "frmFunConf";
            this.Text = "frmDtTable";
            this.Load += new System.EventHandler(this.frmFunConf_Load);
            this.scForm.Panel1.ResumeLayout(false);
            this.scForm.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scForm)).EndInit();
            this.scForm.ResumeLayout(false);
            this.scDgv.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scDgv)).EndInit();
            this.scDgv.ResumeLayout(false);
            this.scQuery.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scQuery)).EndInit();
            this.scQuery.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SkinForm.Controls.qyTreeView qytvDbTable;
    }
}