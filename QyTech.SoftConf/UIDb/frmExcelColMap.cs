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
using QyTech.Core.Common;

using QyExpress.Dao;
using QyTech.SoftConf;
using QyTech.SkinForm.Controls;
using QyTech.Json;
using QyTech.Core.BLL;

namespace QyTech.SoftConf.UIList
{
    public partial class frmExcelColMap : qyfLayListWithLeftTree
    {
        
        public frmExcelColMap()
             : base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base,
                 Guid.Parse("c573aacc-34ed-4487-bba5-af6f5462ec82"), GlobalVaribles.currloginUserFilter.DTable)
        {
            InitializeComponent();
        }

        private void frm_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;
            List<qytvNode> nodes = new List<qytvNode>();
            //string url = GlobalVaribles.ServerUrl + "/api/bsTable/getTreeForExcelMap";
            //QyJsonData jsonData = HttpRequestUtils.PostRemoteJsonQy(url, null);
            //nodes = JsonHelper.DeserializeJsonToList<qytvNode>(jsonData.data.ToString());

            string dtwhere = "NeedExcelImpo=1";
            bsUser LoginUser = GlobalVaribles.currloginUser;
           
            List<bsTable> dbts = EntityManager_Static.GetListNoPaging<bsTable>(DB_Base, dtwhere, "TName");
            foreach (bsTable t in dbts)
            {
                qytvNode n1 = new qytvNode();
                n1.id = t.bsT_Id.ToString();

                n1.name = t.TName;
                n1.pId = t.bsD_Id.ToString();
                n1.type = "Table";
                nodes.Add(n1);
            }

            qytvLeft.LoadData("数据可以导入表",nodes);
        }
    }
}
