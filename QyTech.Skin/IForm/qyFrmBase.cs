using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using QyTech.Core;

namespace QyTech.SkinForm
{
    public partial class qyFrmBase : Form
    {

        public qyFrmBase()
        {
            InitializeComponent();

            this.BackColor = SystemColors.GradientInactiveCaption;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = true;
            this.Text = "";
        }
    }
}
