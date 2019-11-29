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
    public partial class qyForm : Form
    {
        protected log4net.ILog log = log4net.LogManager.GetLogger("QyTech.qyForm");

        public qyForm()
        {
            InitializeComponent();
            this.BackColor = SystemColors.GradientInactiveCaption;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = true;
        }

        


        private void FlatForm_MouseMove(object sender, MouseEventArgs e)
        {
           // qyFormUtil.MouseMoveForm(this.Handle);
        }

    }
}
