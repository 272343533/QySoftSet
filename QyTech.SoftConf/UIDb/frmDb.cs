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
    public partial class frmDb : qyfLayoutList
    {
        public frmDb()
            :base(GlobalVaribles.ObjContext_Base,GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base,
                 Guid.Parse("AA207EA0-D06F-449A-A3B3-D78EBFD044DE"),BLL.commService.DbWhere)
        {
            InitializeComponent();
        }
        private void frm_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;

        }

    }
}
