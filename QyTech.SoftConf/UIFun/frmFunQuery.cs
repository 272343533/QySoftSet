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
using QyTech.SkinForm.Controls;



namespace QyTech.SoftConf.UIList
{
    public partial class frmFunQuery : qyfLayoutListWithLeft
    {
        
        public frmFunQuery()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App,  GlobalVaribles.SqConn_Base,
                 Guid.Parse("1CAC11A9-6D71-4140-B73C-0D9CE9B169BE"), BLL.commService.FunConfWhere)//, "FQNo")
        {
            InitializeComponent();


        }

        private void frm_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;


            List<qytvNode> nodes = BLL.commService.GetFunConfs(DB_Base, strBaseWhere);
            qytvDbTable.LoadData(nodes);
        }



        private void qytvDbTable_AfterSelect(object sender, TreeViewEventArgs e)
        {
            qytvNode tntag = e.Node.Tag as qytvNode;

            strBaseWhere = "bsFC_Id='" + tntag.Id + "'";

            RefreshDgv();
        }

    }
}
