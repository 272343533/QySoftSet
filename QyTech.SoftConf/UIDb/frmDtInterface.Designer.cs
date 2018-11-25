namespace QyTech.SoftConf.UIDb
{
    partial class frmDtInterface
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
            this.qytvArea = new QyTech.SkinForm.Controls.qyTreeView();
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
            // qytvLeft
            // 
            this.qytvLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.qytvLeft.Dock = System.Windows.Forms.DockStyle.None;
            this.qytvLeft.LineColor = System.Drawing.Color.Black;
            this.qytvLeft.Size = new System.Drawing.Size(240, 300);
            // 
            // scForm
            // 
            // 
            // scForm.Panel1
            // 
            this.scForm.Panel1.Controls.Add(this.qytvArea);
            this.scForm.Size = new System.Drawing.Size(874, 450);
            this.scForm.SplitterDistance = 242;
            // 
            // scDgv
            // 
            this.scDgv.Size = new System.Drawing.Size(628, 450);
            // 
            // scQuery
            // 
            this.scQuery.Size = new System.Drawing.Size(628, 68);
            // 
            // gbCondition
            // 
            this.gbCondition.Size = new System.Drawing.Size(74, 34);
            // 
            // qytvArea
            // 
            this.qytvArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.qytvArea.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.qytvArea.Location = new System.Drawing.Point(0, 303);
            this.qytvArea.Name = "qytvArea";
            this.qytvArea.NodeCheckedWithParent = false;
            this.qytvArea.Size = new System.Drawing.Size(240, 145);
            this.qytvArea.TabIndex = 1;
            this.qytvArea.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.qytvArea_AfterSelect);
            // 
            // frmDtInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 450);
            this.Name = "frmDtInterface";
            this.Text = "应用程序";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_Load);
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

        private SkinForm.Controls.qyTreeView qytvArea;
    }
}