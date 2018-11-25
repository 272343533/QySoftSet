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

using QyExpress.Dao;
using QyTech.SoftConf;
using QyTech.SkinForm.Controls;

using QyTech.DbUtils;
using QyTech.Core.Common;


namespace QyTech.SoftConf.UIList
{
    public partial class frmFunOper : qyfLayoutListWithLeft
    {
        
        public frmFunOper()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base,
                 Guid.Parse("4C1E9407-618A-4A3F-86AB-07279F64B095"), "")
        {
            InitializeComponent();

 
       }

        private void frmFunOper_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;

            List<qytvNode> nodes = BLL.commService.GetFunConfs(DB_Base, strBaseWhere);
            qytvDbTable.LoadData(nodes);
        }



        private void qytvDbTable_AfterSelect(object sender, TreeViewEventArgs e)
        {

            TreeNode tn = e.Node;
            qytvNode tntag = tn.Tag as qytvNode;

            strBaseWhere = "bsFC_Id='" + tntag.id + "'";

            RefreshDgv();
        }
        


        private void tsbAdd_Click(object sender, EventArgs e)
        {
            bsFunField bff = new bsFunField();
            Add(bff);
        }

    }
}
