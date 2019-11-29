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
    public partial class frmSoftCustInfo : qyfLayoutList
    {
        public frmSoftCustInfo()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base,Guid.Parse("d76708a0-e594-4720-a166-be37b0143e6a"),"","")
        {
            InitializeComponent();
        }

    }
}
