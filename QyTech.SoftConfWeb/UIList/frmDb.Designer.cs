﻿namespace QyTech.SoftConf.UIList
{
    partial class frmDb
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
            ((System.ComponentModel.ISupportInitialize)(this.scForm)).BeginInit();
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
            this.scForm.Size = new System.Drawing.Size(874, 422);
            this.scForm.SplitterDistance = 250;
            // 
            // scDgv
            // 
            // 
            // scDgv.Panel1
            // 
            this.scDgv.Panel1.Controls.Add(this.gbCondition);
            this.scDgv.Size = new System.Drawing.Size(874, 422);
            // 
            // scQuery
            // 
            this.scQuery.Size = new System.Drawing.Size(874, 68);
            // 
            // qyBtn_Refresh
            // 
            this.qyBtn_Refresh.Location = new System.Drawing.Point(786, 8);
            // 
            // gbCondition
            // 
            this.gbCondition.Size = new System.Drawing.Size(780, 34);
            // 
            // frmApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 450);
            this.Name = "frmApp";
            this.Text = "应用程序";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
    }
}