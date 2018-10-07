namespace QyTech.UICreate.qyfLayout
{
    partial class qyfLayListWithLeftOrg
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
            this.qytvLeftOrg = new QyTech.SkinForm.Controls.qyTreeView();
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
            // 
            // scForm.Panel1
            // 
            this.scForm.Panel1.Controls.Add(this.qytvLeftOrg);
            this.scForm.Size = new System.Drawing.Size(800, 422);
            this.scForm.SplitterDistance = 229;
            // 
            // scDgv
            // 
            this.scDgv.Size = new System.Drawing.Size(567, 422);
            // 
            // scQuery
            // 
            this.scQuery.Size = new System.Drawing.Size(567, 68);
            // 
            // gbCondition
            // 
            this.gbCondition.Size = new System.Drawing.Size(351, 34);
            // 
            // qytvLeftOrg
            // 
            this.qytvLeftOrg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.qytvLeftOrg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.qytvLeftOrg.Location = new System.Drawing.Point(0, 0);
            this.qytvLeftOrg.Name = "qytvLeftOrg";
            this.qytvLeftOrg.Size = new System.Drawing.Size(229, 422);
            this.qytvLeftOrg.TabIndex = 0;
            this.qytvLeftOrg.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.qytvLeftOrg_AfterSelect);
            // 
            // qyfLayListWithLeftOrg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "qyfLayListWithLeftOrg";
            this.Text = "qyfLayListWithLeftOrg";
            this.Load += new System.EventHandler(this.qyfLayListWithLeftOrg_Load);
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

        private SkinForm.Controls.qyTreeView qytvLeftOrg;
    }
}