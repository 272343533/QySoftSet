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
            :base(GlobalVaribles.ObjContext_Base,GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base,Guid.Parse("c05251e8-57fa-4e9a-a707-c08b4a982a55"),"","")
        {
            InitializeComponent();
        }

    }
}
