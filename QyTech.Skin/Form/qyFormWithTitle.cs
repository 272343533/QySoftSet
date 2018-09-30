using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using QyTech.Auth.Dao;

namespace QyTech.SkinForm
{
    public partial class qyFormWithTitle : Form
    {
        protected log4net.ILog log = log4net.LogManager.GetLogger("QyTech.SkinForm.qyFormContainer");
        public bsUser CurrLogUser;
        public qyFormWithTitle()
        {
            InitializeComponent();

            this.BackColor = SystemColors.GradientInactiveCaption;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = true;
            this.Text = "";
        }

        public string Title { set { lblFormTitle.Text = value;this.Text = value; } }

 

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Left)
                qyFormUtil.MouseMoveForm(this.Handle);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void qyFormWithTitle_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void qyFormWithTitle_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
