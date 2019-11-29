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

using QyTech.DbUtils;

namespace QyTech.SoftConf.UIList
{
    public partial class frmFunOper : qyfLayoutListWithLeft
    {
        
        public frmFunOper()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base, Guid.Parse("4C1E9407-618A-4A3F-86AB-07279F64B095"), "bsFC_Id in (select bsFC_Id from bsFunConf where bsN_Id in (select bsN_Id from bsNavigation where bsA_Id='" + GlobalVaribles.currAppObj.AppId.ToString()+"'))", "OperNo")
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

            strBaseWhere = "bsFC_Id='" + tntag.Id + "'";

            RefreshDgv();
        }
        


        private void tsbAdd_Click(object sender, EventArgs e)
        {
            bsFunField bff = new bsFunField();
            Add(bff);
        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1) return;
            CurrRowObj = QyTech.DbUtils.SqlUtils.DataRow2EntityObject<bsFunOper>((dgvList.DataSource as DataTable).Rows[e.RowIndex]);
            
        }
    }
}
