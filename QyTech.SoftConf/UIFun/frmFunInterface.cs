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
    public partial class frmFunInterface : qyfLayoutListWithLeft
    {
        
        public frmFunInterface()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base, 
                 Guid.Parse("CCFA9FC6-F4A3-448B-BAF0-1F4507BE5627"), BLL.commService.FunConfWhere)//, "InterfaceName")
        {
            InitializeComponent();


        }

        private void frm_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;

            List<qytvNode> nodes = BLL.commService.GetFunConfs(DB_Base,strBaseWhere);
            qytvDbTable.LoadData(nodes);
        }

 

        private void qytvDbTable_AfterSelect(object sender, TreeViewEventArgs e)
        {

            TreeNode tn = e.Node;
            qytvNode tntag = tn.Tag as qytvNode;

            strBaseWhere = "bsFC_Id='" + tntag.Id + "'";


            RefreshDgv();
        }


        private void tsbAddTDefault_Click(object sender, EventArgs e)
        {
            string sqls = "exec bslyAppendFunInterfaceByFunConf '" + CurrLeftPFk.ToString() + "'";


            int ret = QyTech.DbUtils.SqlUtils.ExceuteSql(GlobalVaribles.SqConn_Base, sqls);
            if (ret == -1)
            {
                MessageBox.Show("增加失败");
            }
        }
    }
}
