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
using QyTech.Core.Common;
using QyTech.Core.CommUtils;

namespace QyTech.SoftConf.UIDb
{
    public partial class frmDtInterface : qyfLayListWithLeftTree
    {
        public frmDtInterface()
            :base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base
                 ,Guid.Parse("7582bbab-0733-4a73-b551-ae705f1f9f36"), "")
        {
            InitializeComponent();
        }


        private void frm_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;

            ToolStripButton tsbAdd = AddtsbButton("新增");
            tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            ToolStripButton tsbAddDefault = AddtsbButton("预定义接口");
            tsbAddDefault.Click += new System.EventHandler(this.tsbAddDefault_Click);

            List<qytvNode> nodes = BLL.commService.GetbsTables(DB_Base);
            qytvLeft.LoadData("数据",nodes);

            List<qytvNode> areas = BLL.commService.GetAppAreas(DB_Base);
            qytvArea.LoadData("区域",areas);


            if (qytvLeft.Nodes.Count > 0)
                qytvLeft.SelectedNode = qytvLeft.Nodes[0];

        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            bsTInterface objforadd = new bsTInterface();
            objforadd.bsTI_Id = Guid.NewGuid();
            objforadd.bsT_Id =Guid.Parse(currLeftFPk.ToString());
  
            qyfAdd frm = new qyfAdd(AddOrEdit.Add, sqlConn, objforadd, bstable, bffs_byFormNo);
            frm.ShowDialog();
        }

        private void tsbAddDefault_Click(object sender, EventArgs e)
        {
            //存储过程加入，
            if (areaname=="")
            {
                if (DialogResult.Cancel == MessageBox.Show("确定不选择区域吗?"))
                     return;
            }
            if (currLeftFPk == null || currLeftFPk.ToString() == "")
            {
                MessageBox.Show("数据表必须选择！");
                return;
            }
            QyTech.Core.BLL.EntityManager_Static.ExecuteSql(GlobalVaribles.ObjContext_Base, "exec [bslyAppendTInterface] '" + currLeftFPk.ToString() + "','" + areaname + "'");
            return;
            //获取数据
            List<qytvNode> Nodes_ = qyDefalutInterface.GetSysPreDefineTInterface();

            List<qytvNode> havenodes = new List<qytvNode>();
            foreach (DataRow item in dtList.Rows)
            {
                qytvNode node = new qytvNode();
                node.id = item["bsTI_Id"].ToString();
                node.name = item["InterfaceName"].ToString();

                havenodes.Add(node);
            }
            Nodes_ = qyTreeViewUtil.RefreshCheckedByName(Nodes_, havenodes);
            frmAddDefaultTInterface obj = new frmAddDefaultTInterface(Nodes_, bstable, areaname);
            obj.ShowDialog();

            //string str = QyTech.DbUtils.SqlUtils.ExceuteSp(sqlConn, "bslyAppendTInterface", "'" + currLeftFPk.ToString() + "','" + areaname + "'");
            //MessageBox.Show(str);
            //RefreshDgv(dgvList, "bsT_Id='" + currLeftFPk.ToString() + "'");
        }
        private string areaname = "";
        private void qytvArea_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text != "区域")
                areaname = e.Node.Text;
            else
                areaname = "";
        }
    }
}
