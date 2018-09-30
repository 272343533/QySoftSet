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
    public partial class frmApp : qyfLayoutList
    {

        public frmApp()//://base(GlobalVaribles.EM_Base,GlobalVaribles.EM_Base, GlobalVaribles.SqConn_Base,"bsAppName","","")
        {
            InitializeComponent();
        }

    }
}
