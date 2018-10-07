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
    public partial class frmDtField : qyfLayoutListWithLeft
    {

        public Guid PFk;

        public frmDtField()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base,
                 new Guid("B7BF7641-CCDF-4726-B948-2F9F4B212A7F"), BLL.commService.bsTWhere)//, "FNo")
        {
            InitializeComponent();
        }

        private void frmDtField_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;

            List < qytvNode > nodes = BLL.commService.GetbsTables(DB_Base);

            qytvDbTable.LoadData(nodes);
        }


        private void qytvDbTable_AfterSelect(object sender, TreeViewEventArgs e)
        {

            TreeNode tn = e.Node;
            qytvNode tntag = tn.Tag as qytvNode;
            if (tntag.Tag != "Db")
            {
                PFk = Guid.Parse(tntag.Id);

                TreeNode ptn = tn.Parent;
                qytvNode ptntag = ptn.Tag as qytvNode;

                strBaseWhere = "bsT_Id='" + tntag.Id + "'";

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

        }
    }
}
