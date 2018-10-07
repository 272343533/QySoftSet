namespace QyTech.UICreate
{
    partial class qyfLayListParent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(qyfLayListParent));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.scForm = new System.Windows.Forms.SplitContainer();
            this.scDgv = new System.Windows.Forms.SplitContainer();
            this.scQuery = new System.Windows.Forms.SplitContainer();
            this.qyBtn_Refresh = new QyTech.SkinForm.Component.qyBtn_Search();
            this.gbCondition = new System.Windows.Forms.GroupBox();
            this.qyPbr_Refresh = new QyTech.SkinForm.Controls.qyPbr();
            this.tsbToolBar = new System.Windows.Forms.ToolStrip();
            this.dgvList = new QyTech.SkinForm.Controls.qyDgv();
            ((System.ComponentModel.ISupportInitialize)(this.scForm)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // scForm
            // 
            this.scForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scForm.Location = new System.Drawing.Point(0, 0);
            this.scForm.Name = "scForm";
            // 
            // scForm.Panel2
            // 
            this.scForm.Panel2.Controls.Add(this.scDgv);
            this.scForm.Size = new System.Drawing.Size(970, 450);
            this.scForm.SplitterDistance = 278;
            this.scForm.TabIndex = 5;
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
            this.scDgv.Panel2.Controls.Add(this.dgvList);
            this.scDgv.Size = new System.Drawing.Size(688, 450);
            this.scDgv.SplitterDistance = 68;
            this.scDgv.TabIndex = 0;
            // 
            // scQuery
            // 
            this.scQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scQuery.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
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
            this.scQuery.Panel2.Controls.Add(this.qyPbr_Refresh);
            this.scQuery.Panel2.Controls.Add(this.tsbToolBar);
            this.scQuery.Panel2MinSize = 0;
            this.scQuery.Size = new System.Drawing.Size(688, 68);
            this.scQuery.SplitterDistance = 34;
            this.scQuery.TabIndex = 12;
            // 
            // qyBtn_Refresh
            // 
            this.qyBtn_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.qyBtn_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("qyBtn_Refresh.Image")));
            this.qyBtn_Refresh.Location = new System.Drawing.Point(600, 8);
            this.qyBtn_Refresh.Name = "qyBtn_Refresh";
            this.qyBtn_Refresh.Size = new System.Drawing.Size(80, 23);
            this.qyBtn_Refresh.TabIndex = 3;
            this.qyBtn_Refresh.Text = "刷新";
            this.qyBtn_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.qyBtn_Refresh.UseVisualStyleBackColor = true;
            this.qyBtn_Refresh.Click += new System.EventHandler(this.qyBtn_Refresh_Click);
            // 
            // gbCondition
            // 
            this.gbCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCondition.Location = new System.Drawing.Point(0, 0);
            this.gbCondition.Name = "gbCondition";
            this.gbCondition.Size = new System.Drawing.Size(594, 34);
            this.gbCondition.TabIndex = 2;
            this.gbCondition.TabStop = false;
            // 
            // qyPbr_Refresh
            // 
            this.qyPbr_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.qyPbr_Refresh.Location = new System.Drawing.Point(413, 3);
            this.qyPbr_Refresh.Name = "qyPbr_Refresh";
            this.qyPbr_Refresh.Size = new System.Drawing.Size(272, 23);
            this.qyPbr_Refresh.TabIndex = 1;
            this.qyPbr_Refresh.Visible = false;
            // 
            // tsbToolBar
            // 
            this.tsbToolBar.Location = new System.Drawing.Point(0, 0);
            this.tsbToolBar.Name = "tsbToolBar";
            this.tsbToolBar.Size = new System.Drawing.Size(688, 25);
            this.tsbToolBar.TabIndex = 2;
            this.tsbToolBar.Text = "toolStrip1";
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dgvList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Lavender;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Lavender;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(688, 378);
            this.dgvList.TabIndex = 0;
            this.dgvList.eventColumnOrderByChanged += new QyTech.SkinForm.Controls.deldgvColumnOrderByChangedhandeler(this.dgvList_eventColumnOrderByChanged);
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            // 
            // qyfLayListParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 450);
            this.Controls.Add(this.scForm);
            this.Name = "qyfLayListParent";
            this.Text = "frmList";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.qyfLayoutListParent_FormClosed);
            this.Load += new System.EventHandler(this.frmListWithLeft_Load);
            this.scForm.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scForm)).EndInit();
            this.scForm.ResumeLayout(false);
            this.scDgv.Panel1.ResumeLayout(false);
            this.scDgv.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scDgv)).EndInit();
            this.scDgv.ResumeLayout(false);
            this.scQuery.Panel1.ResumeLayout(false);
            this.scQuery.Panel2.ResumeLayout(false);
            this.scQuery.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scQuery)).EndInit();
            this.scQuery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.SplitContainer scForm;
        public System.Windows.Forms.SplitContainer scDgv;
        public System.Windows.Forms.SplitContainer scQuery;
        private SkinForm.Controls.qyPbr qyPbr_Refresh;
        public SkinForm.Component.qyBtn_Search qyBtn_Refresh;
        public System.Windows.Forms.GroupBox gbCondition;
        public SkinForm.Controls.qyDgv dgvList;
        protected System.Windows.Forms.ToolStrip tsbToolBar;
    }
}