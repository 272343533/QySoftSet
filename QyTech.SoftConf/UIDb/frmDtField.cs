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
    public partial class frmDtField : qyfLayoutListWithLeft
    {

        public Guid PFk;

        public frmDtField()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base,
                 new Guid("B7BF7641-CCDF-4726-B948-2F9F4B212A7F"), "")
        {
            InitializeComponent();
        }

        private void frmDtField_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;

            List < qytvNode > nodes = BLL.commService.GetbsTables(DB_Base);

            qytvDbTable.LoadData(nodes);

            if (qytvDbTable.Nodes.Count > 0)
                qytvDbTable.SelectedNode = qytvDbTable.Nodes[0];
        }


        private void qytvDbTable_AfterSelect(object sender, TreeViewEventArgs e)
        {

            TreeNode tn = e.Node;
            qytvNode tntag = tn.Tag as qytvNode;
            if (tntag.type != "Db")
            {
                PFk = Guid.Parse(tntag.id);

                TreeNode ptn = tn.Parent;
           
                strBaseWhere = "bsT_Id='" + tntag.id + "'";
                RefreshDgv();
             }
            else
            {
                strBaseWhere = "bsT_Id='" + Guid.Empty.ToString()+ "'";

                RefreshDgv();
            }

        }


        private void tsbDelNoValidField_Click(object sender, EventArgs e)
        {
            string sqls = "exec bslybsFieldForDeleteNotValid '" + PFk.ToString() + "'";
            

            int ret = QyTech.DbUtils.SqlUtils.ExceuteSql(GlobalVaribles.SqConn_Base, sqls);
            

        }

        private void tsbAddNewField_Click(object sender, EventArgs e)
        {
            string sqls = "exec bslybsFieldForAddNew '" + PFk.ToString() + "'";
            QyTech.DbUtils.SqlUtils.ExceuteSql(GlobalVaribles.SqConn_Base, sqls);

        }


        private void tsbCreate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("数据库中创建字段");
        }

        private void tsbInitColunn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("？");
        }
    }
}
