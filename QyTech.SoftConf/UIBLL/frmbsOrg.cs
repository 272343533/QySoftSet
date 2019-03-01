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
using QyTech.Json;
using QyTech.SkinForm.Controls;
using QyTech.Core.Common;
using QyTech.Utils;
using QyTech.Core.BLL;
using System.Data.Objects;

namespace QyTech.SoftConf.UIBLL
{
    public partial class frmbsOrg : qyfLayTreeEdit
    {
      
        public frmbsOrg() 
            : base(GlobalVaribles.ObjContext_Base, null, GlobalVaribles.SqConn_Base, Guid.Parse("6c6e2474-0b65-49fa-8dd9-3c30140eb07b"), "bsS_Id='"+ GlobalVaribles.currSoftCutomer.bsS_Id.ToString()+ "'")
        {
            InitializeComponent();


        }
        private void frmbsOrg_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;
            cboType.SelectedIndex = 1;
            qyBtn_RefreshTree_Click(null, null);
        }

        private void qyBtn_RefreshTree_Click(object sender, EventArgs e)
        {
            RefreshTree();
        }

        #region 重写父类方法
        protected override void Add()
        {
            base.Add();
            bsOrganize obj = new bsOrganize();
            obj.DelStatus = false;
            obj.OrganizeStatus = "正常";
            obj.PId = Guid.Parse(currTPkId.ToString());
            obj.bsO_Id = Guid.NewGuid();
            obj.bsoAttr = cboType.Text;
            obj.bsS_Id = GlobalVaribles.currSoftCutomer.bsS_Id;
            obj.CreateTime = DateTime.Now;
            obj.Operater = "admin";
            
            InitFrom(obj); 
        }
        /// <summary>
        /// 刷新树，子类实现
        /// </summary>
        protected override void RefreshTree()
        {
            //加载左侧数据
            string where = "";
            if (cboType.Text == "")
                where = strBaseWhere;
            else
                where =strBaseWhere + " and bsoAttr='" + cboType.Text + "' or (bsoAttr='公司' or bsoAttr is null)";

            List<qytvNode> nodes = new List<qytvNode>();

            //string url = "";
            //url = GlobalVaribles.ServerUrl + "/api/bsOrganize/getTree";
            //Dictionary<string, string> dicparas = new Dictionary<string, string>();
            //dicparas.Add("where", where);
            //QyJsonData jsonData = HttpRequestUtils.PostRemoteJsonQy(url, dicparas);
            //nodes = JsonHelper.DeserializeJsonToList<qytvNode>(jsonData.data.ToString());
            List<bsOrganize> orgs = EntityManager_Static.GetListNoPaging<bsOrganize>(DB_Base, where, "Code");
            foreach(bsOrganize org in orgs)
            {
                qytvNode node = new qytvNode();
                node.id = org.bsO_Id.ToString();
                node.name = org.Name;
                node.pId = org.PId.ToString();
                node.type = org.OrgType;
                nodes.Add(node);
            }


            if (nodes.Count > 0)
            {
                qytvLeft.LoadData(nodes);
                if (qytvLeft.Nodes.Count>0)
                    qytvLeft.SetSelectNode(qytvLeft.Nodes[0].Text);
            }
        }

        #endregion
    }
}
