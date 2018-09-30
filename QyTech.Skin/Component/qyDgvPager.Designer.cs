namespace QyTech.SkinForm.Component
{
    partial class qyDgvPager
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.comboPageSize = new System.Windows.Forms.ComboBox();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.lblRecordRegion = new System.Windows.Forms.Label();
            this.lblPageCount = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.txtBoxCurPage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboPageSize
            // 
            this.comboPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPageSize.FormattingEnabled = true;
            this.comboPageSize.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50",
            "60",
            "100"});
            this.comboPageSize.Location = new System.Drawing.Point(73, 8);
            this.comboPageSize.Name = "comboPageSize";
            this.comboPageSize.Size = new System.Drawing.Size(62, 20);
            this.comboPageSize.TabIndex = 27;
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(153, 8);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(29, 23);
            this.btnFirst.TabIndex = 29;
            this.btnFirst.Text = "|<";
            this.btnFirst.UseVisualStyleBackColor = true;
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(176, 9);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(28, 23);
            this.btnPrev.TabIndex = 30;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(309, 5);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(26, 23);
            this.btnNext.TabIndex = 31;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(341, 7);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(29, 23);
            this.btnLast.TabIndex = 32;
            this.btnLast.Text = ">|";
            this.btnLast.UseVisualStyleBackColor = true;
            // 
            // lblRecordRegion
            // 
            this.lblRecordRegion.AutoSize = true;
            this.lblRecordRegion.Location = new System.Drawing.Point(389, 14);
            this.lblRecordRegion.Name = "lblRecordRegion";
            this.lblRecordRegion.Size = new System.Drawing.Size(65, 12);
            this.lblRecordRegion.TabIndex = 33;
            this.lblRecordRegion.Text = "显示1-50条";
            // 
            // lblPageCount
            // 
            this.lblPageCount.AutoSize = true;
            this.lblPageCount.Location = new System.Drawing.Point(460, 14);
            this.lblPageCount.Name = "lblPageCount";
            this.lblPageCount.Size = new System.Drawing.Size(35, 12);
            this.lblPageCount.TabIndex = 34;
            this.lblPageCount.Text = "共1页";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Location = new System.Drawing.Point(527, 16);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(35, 12);
            this.lblTotalCount.TabIndex = 35;
            this.lblTotalCount.Text = "共0条";
            // 
            // txtBoxCurPage
            // 
            this.txtBoxCurPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtBoxCurPage.Location = new System.Drawing.Point(231, 6);
            this.txtBoxCurPage.Name = "txtBoxCurPage";
            this.txtBoxCurPage.Size = new System.Drawing.Size(52, 21);
            this.txtBoxCurPage.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 37;
            this.label1.Text = "每页条数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 38;
            this.label2.Text = "第           页";
            // 
            // qyDgvPager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoxCurPage);
            this.Controls.Add(this.lblTotalCount);
            this.Controls.Add(this.lblPageCount);
            this.Controls.Add(this.lblRecordRegion);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.comboPageSize);
            this.Controls.Add(this.label2);
            this.Name = "qyDgvPager";
            this.Size = new System.Drawing.Size(637, 39);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboPageSize;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Label lblRecordRegion;
        private System.Windows.Forms.Label lblPageCount;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.TextBox txtBoxCurPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
