using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QyTech.SkinForm
{
    public partial class qyFormContainer : Form
    {
        protected log4net.ILog log = log4net.LogManager.GetLogger("QyTech.SkinForm.qyFormContainer");

        public qyFormContainer( Form son)
        {
            InitializeComponent();

            this.Height = son.Height + this.panTop.Height;
            this.Width = son.Width;
            this.BackColor = SystemColors.GradientInactiveCaption;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = true;

            son.TopLevel = false;
            this.panForm.Controls.Add(son);
            son.Location = new Point(0, 0);
            //this.IsMdiContainer = true;//设置父窗体是容器
            //son.MdiParent = this;//设置窗体的父子关系
            //son.Parent = this.panForm;//设置子窗体的容器为父窗体中的Panel


            son.Show();//显示子窗体，此句很重要，否则子窗体不会显示


        }

        


        private void FlatForm_MouseMove(object sender, MouseEventArgs e)
        {
            qyFormUtil.MouseMoveForm(this.Handle);
        }

        private void FlatForm_Load(object sender, EventArgs e)
        {

        }


      

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            qyFormUtil.MouseMoveForm(this.Handle);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
