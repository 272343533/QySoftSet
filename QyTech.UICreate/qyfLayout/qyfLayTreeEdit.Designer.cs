namespace QyTech.UICreate
{
    partial class qyfLayTreeEdit
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(qyfLayTreeEdit));
            this.scForm = new System.Windows.Forms.SplitContainer();
            this.qytvLeft = new QyTech.SkinForm.Controls.qyTreeView();
            this.qyBtn_Save = new QyTech.SkinForm.Component.qyBtn_ok();
            this.gbContainer = new System.Windows.Forms.GroupBox();
            this.cmsTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qyBtn_SearchOrg = new QyTech.SkinForm.Component.qyBtn_Search();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.scForm)).BeginInit();
            this.scForm.Panel1.SuspendLayout();
            this.scForm.Panel2.SuspendLayout();
            this.scForm.SuspendLayout();
            this.cmsTree.SuspendLayout();
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
            this.scForm.Panel1.Controls.Add(this.comboBox1);
            this.scForm.Panel1.Controls.Add(this.label1);
            this.scForm.Panel1.Controls.Add(this.qyBtn_SearchOrg);
            this.scForm.Panel1.Controls.Add(this.qytvLeft);
            // 
            // scForm.Panel2
            // 
            this.scForm.Panel2.Controls.Add(this.qyBtn_Save);
            this.scForm.Panel2.Controls.Add(this.gbContainer);
            this.scForm.Size = new System.Drawing.Size(600, 422);
            this.scForm.SplitterDistance = 230;
            this.scForm.TabIndex = 5;
            // 
            // qytvLeft
            // 
            this.qytvLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.qytvLeft.ContextMenuStrip = this.cmsTree;
            this.qytvLeft.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.qytvLeft.Location = new System.Drawing.Point(0, 39);
            this.qytvLeft.Name = "qytvLeft";
            this.qytvLeft.Size = new System.Drawing.Size(230, 383);
            this.qytvLeft.TabIndex = 0;
            this.qytvLeft.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.qytvLeft_AfterSelect);
            // 
            // qyBtn_Save
            // 
            this.qyBtn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.qyBtn_Save.Image = ((System.Drawing.Image)(resources.GetObject("qyBtn_Save.Image")));
            this.qyBtn_Save.Location = new System.Drawing.Point(261, 389);
            this.qyBtn_Save.Name = "qyBtn_Save";
            this.qyBtn_Save.Size = new System.Drawing.Size(75, 23);
            this.qyBtn_Save.TabIndex = 1;
            this.qyBtn_Save.Text = "保存";
            this.qyBtn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.qyBtn_Save.UseVisualStyleBackColor = true;
            this.qyBtn_Save.Click += new System.EventHandler(this.qyBtn_Save_Click);
            // 
            // gbContainer
            // 
            this.gbContainer.Location = new System.Drawing.Point(14, 6);
            this.gbContainer.Name = "gbContainer";
            this.gbContainer.Size = new System.Drawing.Size(340, 377);
            this.gbContainer.TabIndex = 0;
            this.gbContainer.TabStop = false;
            this.gbContainer.Text = "编辑";
            // 
            // cmsTree
            // 
            this.cmsTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新ToolStripMenuItem,
            this.新增ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.cmsTree.Name = "cmsTree";
            this.cmsTree.Size = new System.Drawing.Size(101, 70);
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.刷新ToolStripMenuItem.Text = "刷新";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.刷新ToolStripMenuItem_Click);
            // 
            // 新增ToolStripMenuItem
            // 
            this.新增ToolStripMenuItem.Name = "新增ToolStripMenuItem";
            this.新增ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.新增ToolStripMenuItem.Text = "新增";
            this.新增ToolStripMenuItem.Click += new System.EventHandler(this.新增ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // qyBtn_SearchOrg
            // 
            this.qyBtn_SearchOrg.Image = ((System.Drawing.Image)(resources.GetObject("qyBtn_SearchOrg.Image")));
            this.qyBtn_SearchOrg.Location = new System.Drawing.Point(164, 8);
            this.qyBtn_SearchOrg.Name = "qyBtn_SearchOrg";
            this.qyBtn_SearchOrg.Size = new System.Drawing.Size(56, 23);
            this.qyBtn_SearchOrg.TabIndex = 1;
            this.qyBtn_SearchOrg.Text = "刷新";
            this.qyBtn_SearchOrg.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.qyBtn_SearchOrg.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "分类";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(61, 10);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(97, 20);
            this.comboBox1.TabIndex = 3;
            // 
            // qyfLayoutTreeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.scForm);
            this.Name = "qyfLayoutTreeEdit";
            this.Text = "frmList";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.qyfLayoutListParent_FormClosed);
            this.Load += new System.EventHandler(this.frmListWithLeft_Load);
            this.Controls.SetChildIndex(this.scForm, 0);
            this.scForm.Panel1.ResumeLayout(false);
            this.scForm.Panel1.PerformLayout();
            this.scForm.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scForm)).EndInit();
            this.scForm.ResumeLayout(false);
            this.cmsTree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.SplitContainer scForm;
        protected SkinForm.Controls.qyTreeView qytvLeft;
        private System.Windows.Forms.ContextMenuStrip cmsTree;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新增ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        protected System.Windows.Forms.GroupBox gbContainer;
        private SkinForm.Component.qyBtn_ok qyBtn_Save;
        private SkinForm.Component.qyBtn_Search qyBtn_SearchOrg;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}