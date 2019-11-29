using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QyTech.SoftConf.UIParent;
using QyExpress.Dao;
using QyTech.Core.Common;
using QyTech.Core.CommUtils;
using QyTech.Core;
using QyTech.Json;

namespace QyTech.SoftConf.UIDb
{
    public partial class frmAddDefaultTInterface : IqyfTreeView
    {
        string areaname_ = "";
        public frmAddDefaultTInterface(List<qytvNode> nodes,bsTable bst,string areaname) 
            : base(nodes, bst)
        {
            InitializeComponent();

            areaname_ = areaname;
        }

        protected override void qyBtn_Ok_Click(object sender, EventArgs e)
        {
           try
            {
                List<qytvNode> SelectNodes = qytvForm.GetCheckedNode();

                //保存
                #region 找到差异，删除没有的
                string delNodeIds = "";
                foreach (qytvNode node in initcheckedNodes_)
                {
                    if (!SelectNodes.Contains(node))
                        delNodeIds += "," + node.name;
                }

                if (delNodeIds.Length > 0)
                {
                    delNodeIds = delNodeIds.Substring(1);
                    PostDeLNodesWhere(delNodeIds);
                }
                #endregion


                //增加新的
                string addNodeIds = "";
                foreach (qytvNode node in SelectNodes)
                {
                    if (!initcheckedNodes_.Contains(node))
                        addNodeIds += "," + node.name;
                }
                if (delNodeIds.Length > 0)
                {
                    addNodeIds = addNodeIds.Substring(1);
                    PostAddNodes(delNodeIds);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }


        protected override void PostDeLNodesWhere(string delNodeNames)
        {
            Dictionary<string, string> dicwhere = new Dictionary<string, string>();
            dicwhere.Add("where", "bsT_Id='" +bsT_.bsT_Id.ToString()+ "' and interfacename in ('"+ delNodeNames.Replace(",","', '") + "')");

            //执行接口 deletesqlwhere
            string url = GlobalVaribles.ServerUrl + "/softconf/bstinterface/deletesqlwhere";
            QyJsonData qyjson = QyTech.Utils.HttpRequestUtils.PostRemoteJsonQy(url,dicwhere);
        }
        /// <summary>
        /// 每个单独的保存都需要再子类中覆盖此方法即可
        /// </summary>
        /// <param name="addNodes"></param>
        protected override void PostAddNodes(string addNodes)
        {
            string[] names = addNodes.Split(new char[] { ',' });
            int intvalue = 0;
            foreach(string item in names)
            {
                if (item!="")
                    intvalue += qyEnumUtil.GetValueByName<DaoDefaultInterfaceName>(item);
            }

            //这个应该放到bsinterface服务中，直接调用，不应该在这里出现存储名，important
            Dictionary<string, string> dicwhere = new Dictionary<string, string>();
            dicwhere.Add("spname", "bslyAppendTInterface");
            dicwhere.Add("paras", intvalue.ToString());


            //执行存储过程接口：ExcuteStoreProcedure
            string url = GlobalVaribles.ServerUrl + "/softconf/bstinterface/ExcuteStoreProcedure";
            QyJsonData qyjson = QyTech.Utils.HttpRequestUtils.PostRemoteJsonQy(url, dicwhere);


        }
    }
}
