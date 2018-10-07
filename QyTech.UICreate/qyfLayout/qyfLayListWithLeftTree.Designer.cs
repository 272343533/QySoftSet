namespace QyTech.UICreate
{
    partial class qyfLayListWithLeftTree
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
            this.qytvLeft = new QyTech.SkinForm.Controls.qyTreeView();
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
            this.scForm.Panel1.Controls.Add(this.qytvLeft);
            this.scForm.Panel1MinSize = 1;
            this.scForm.Size = new System.Drawing.Size(800, 450);
            this.scForm.SplitterDistance = 264;
            // 
            // scDgv
            // 
            this.scDgv.Size = new System.Drawing.Size(532, 450);
            // 
            // scQuery
            // 
            this.scQuery.Size = new System.Drawing.Size(532, 68);
            // 
            // gbCondition
            // 
            this.gbCondition.Size = new System.Drawing.Size(0, 34);
            // 
            // qytvLeft
            // 
            this.qytvLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.qytvLeft.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.qytvLeft.Location = new System.Drawing.Point(0, 0);
            this.qytvLeft.Name = "qytvLeft";
            this.qytvLeft.NodeCheckedWithParent = false;
            this.qytvLeft.Size = new System.Drawing.Size(264, 450);
            this.qytvLeft.TabIndex = 0;
            this.qytvLeft.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.qytvLeft_AfterSelect);
            // 
            // qyfLayListWithLeftTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "qyfLayListWithLeftTree";
            this.Load += new System.EventHandler(this.qyfLayListWithLeftTree_Load);
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

        public SkinForm.Controls.qyTreeView qytvLeft;
    }
}