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
using QyTech.Json;
using QyTech.Utils;

using QyExpress.Dao;
using QyTech.Core.Common;




namespace QyTech.SoftConf.UIList
{
    public partial class frmDtTable : qyfLayoutListWithLeft
    {
        public frmDtTable()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base,
                 Guid.Parse("717519BD-7AAB-4445-8686-1033FAC8EBE7"), GlobalVaribles.currloginUserFilter.DTable)
        {
            InitializeComponent();
        }

        private void frmDtTable_Load(object sender, EventArgs e)
        {

            this.Text = bsFc.FunDesp;

            刷新ToolStripMenuItem_Click(null, null);
        }

        private void 导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (qytvDbTable.SelectedNode!=null)
            {
                TreeNode tn = qytvDbTable.SelectedNode;
                qytvNode tntag = tn.Tag as qytvNode;
                if (tntag.type == "Table")
                {
                    TreeNode ptn = tn.Parent;//.Parent;
                    qytvNode ptntag = ptn.Tag as qytvNode;
                    //库id，表名，主键，描述，类型，是否重建
                    string sqls = "exec bsly1AppendCreatedTableInfoToBsTable '" + ptntag.id + "','" + tn.Text + "','" + tn.Text + "',0";
                    QyTech.DbUtils.SqlUtils.ExceuteSql(sqlConn, sqls);
                }
                else if (tntag.type == "View")
                {
                    TreeNode ptn = tn.Parent;//.Parent;
                    qytvNode ptntag = ptn.Tag as qytvNode;
                    //库id，表名，主键，描述，类型，是否重建
                    string sqls = "exec [bsly1AppendCreatedViewsInfoToBsTable] '" + ptntag.id + "','" + tn.Text + "','" + tn.Text + "'";
                    QyTech.DbUtils.SqlUtils.ExceuteSql(sqlConn, sqls);
                }
            }
        }


        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //string url = GlobalVaribles.ServerUrl + "/api/bsTable/GetDtNotinBsTables";
            //QyJsonData jsonData = HttpRequestUtils.PostRemoteJsonQy(url, null);
            //List<qytvNode> nodes = JsonHelper.DeserializeJsonToList<qytvNode>(jsonData.data.ToString());
            List<qytvNode> nodes = new List<qytvNode>();
            string appName = System.Configuration.ConfigurationManager.AppSettings["curAppName"];
            if (GlobalVaribles.currloginUser.bsU_Id==InnerAccout.expressAdminUser.bsU_Id)
            {
                appName = "系统配置";
            }
            List<tmpTreeNode> dbts = QyTech.Core.BLL.EntityManager_Static.GetAllByStorProcedure<tmpTreeNode>(DB_Base, "splyGetDbAndTable", new object[] { appName,0 });
            foreach (tmpTreeNode s in dbts)
            {
                qytvNode n = new qytvNode();
                n.id = s.id.ToString();
                n.pId = s.pId.ToString();
                n.name = s.name;
                n.type = s.type;
                nodes.Add(n);
            }
            qytvDbTable.LoadData(nodes);
        }

        private void tsbNeedImport_Click(object sender, EventArgs e)
        {

            if (CurrRowObj != null)
            {
                string sqls = "exec bslyAddField2ExcelMap '" +(CurrRowObj as bsTable).bsT_Id.ToString()+"'";
                QyTech.DbUtils.SqlUtils.ExceuteSql(sqlConn, sqls);
            }
           
        }

        /// <summary>
        /// 按照区域选择表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void qytvAreas_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
