using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QyTech.UICreate;
using QyTech.SkinForm;

using QyTech.Auth.Dao;
using QyTech.SoftConf;

namespace QyTech.SoftConf.UIList
{
    public partial class frmArea : qyfLayoutList
    {
        public frmArea()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base,
                 Guid.Parse("da7682c6-1cad-499d-9cbc-42dc79f62bb4"))
        {
            InitializeComponent();
        }
        private void frm_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;
        }

    }
}
