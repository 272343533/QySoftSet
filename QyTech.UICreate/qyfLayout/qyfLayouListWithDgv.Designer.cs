namespace QyTech.UICreate
{
    partial class qyfLayouListWithDgv
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(qyfLayouListWithDgv));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.scDgv = new System.Windows.Forms.SplitContainer();
            this.scQuery = new System.Windows.Forms.SplitContainer();
            this.qyBtn_Refresh = new QyTech.SkinForm.Component.qyBtn_Search();
            this.gbCondition = new System.Windows.Forms.GroupBox();
            this.tsPreTools = new System.Windows.Forms.ToolStrip();
            this.tsPbr = new System.Windows.Forms.ToolStripProgressBar();
            this.qyDgvList = new QyTech.SkinForm.Controls.qyDgv();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.scDgv)).BeginInit();
            this.scDgv.Panel1.SuspendLayout();
            this.scDgv.Panel2.SuspendLayout();
            this.scDgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scQuery)).BeginInit();
            this.scQuery.Panel1.SuspendLayout();
            this.scQuery.Panel2.SuspendLayout();
            this.scQuery.SuspendLayout();
            this.tsPreTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qyDgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // scDgv
            // 
            this.scDgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scDgv.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scDgv.Location = new System.Drawing.Point(0, 28);
            this.scDgv.Name = "scDgv";
            this.scDgv.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scDgv.Panel1
            // 
            this.scDgv.Panel1.Controls.Add(this.scQuery);
            // 
            // scDgv.Panel2
            // 
            this.scDgv.Panel2.Controls.Add(this.qyDgvList);
            this.scDgv.Size = new System.Drawing.Size(800, 422);
            this.scDgv.SplitterDistance = 67;
            this.scDgv.TabIndex = 5;
            // 
            // scQuery
            // 
            this.scQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scQuery.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
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
            this.scQuery.Panel2.Controls.Add(this.tsPreTools);
            this.scQuery.Panel2MinSize = 0;
            this.scQuery.Size = new System.Drawing.Size(800, 67);
            this.scQuery.SplitterDistance = 39;
            this.scQuery.TabIndex = 12;
            // 
            // qyBtn_Refresh
            // 
            this.qyBtn_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.qyBtn_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("qyBtn_Refresh.Image")));
            this.qyBtn_Refresh.Location = new System.Drawing.Point(713, 13);
            this.qyBtn_Refresh.Name = "qyBtn_Refresh";
            this.qyBtn_Refresh.Size = new System.Drawing.Size(75, 23);
            this.qyBtn_Refresh.TabIndex = 1;
            this.qyBtn_Refresh.Text = "刷新";
            this.qyBtn_Refresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.qyBtn_Refresh.UseVisualStyleBackColor = true;
            // 
            // gbCondition
            // 
            this.gbCondition.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbCondition.Location = new System.Drawing.Point(0, 0);
            this.gbCondition.Name = "gbCondition";
            this.gbCondition.Size = new System.Drawing.Size(698, 39);
            this.gbCondition.TabIndex = 0;
            this.gbCondition.TabStop = false;
            // 
            // tsPreTools
            // 
            this.tsPreTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tsPreTools.Dock = System.Windows.Forms.DockStyle.None;
            this.tsPreTools.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.tsPreTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsPbr});
            this.tsPreTools.Location = new System.Drawing.Point(501, 0);
            this.tsPreTools.Name = "tsPreTools";
            this.tsPreTools.Size = new System.Drawing.Size(294, 29);
            this.tsPreTools.TabIndex = 12;
            // 
            // tsPbr
            // 
            this.tsPbr.Name = "tsPbr";
            this.tsPbr.Size = new System.Drawing.Size(280, 26);
            this.tsPbr.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.tsPbr.ToolTipText = "0/100";
            // 
            // qyDgvList
            // 
            this.qyDgvList.AllowUserToAddRows = false;
            this.qyDgvList.AllowUserToDeleteRows = false;
            this.qyDgvList.AllowUserToResizeColumns = false;
            this.qyDgvList.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.qyDgvList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.qyDgvList.BackgroundColor = System.Drawing.Color.White;
            this.qyDgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.qyDgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.qyDgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.qyDgvList.Location = new System.Drawing.Point(0, 0);
            this.qyDgvList.Name = "qyDgvList";
            this.qyDgvList.RowTemplate.Height = 23;
            this.qyDgvList.Size = new System.Drawing.Size(800, 351);
            this.qyDgvList.TabIndex = 10;
            this.qyDgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.qyDgvList_CellClick);
            this.qyDgvList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.qyDgvList_CellValueChanged);
            this.qyDgvList.CurrentCellDirtyStateChanged += new System.EventHandler(this.qyDgvList_CurrentCellDirtyStateChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "选择";
            this.Column1.Name = "Column1";
            this.Column1.Width = 40;
            // 
            // qyfLayouListWithDgv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.scDgv);
            this.Name = "qyfLayouListWithDgv";
            this.Text = "frmList";
            this.Load += new System.EventHandler(this.qyfLayouListWithDgv_Load);
            this.Controls.SetChildIndex(this.scDgv, 0);
            this.scDgv.Panel1.ResumeLayout(false);
            this.scDgv.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scDgv)).EndInit();
            this.scDgv.ResumeLayout(false);
            this.scQuery.Panel1.ResumeLayout(false);
            this.scQuery.Panel2.ResumeLayout(false);
            this.scQuery.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scQuery)).EndInit();
            this.scQuery.ResumeLayout(false);
            this.tsPreTools.ResumeLayout(false);
            this.tsPreTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qyDgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.SplitContainer scDgv;
        public System.Windows.Forms.SplitContainer scQuery;
        public QyTech.SkinForm.Controls.qyDgv qyDgvList;
        public System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        public System.Windows.Forms.GroupBox gbCondition;
        public SkinForm.Component.qyBtn_Search qyBtn_Refresh;
        public System.Windows.Forms.ToolStrip tsPreTools;
        public System.Windows.Forms.ToolStripProgressBar tsPbr;
    }
}