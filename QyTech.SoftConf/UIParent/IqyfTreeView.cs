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
using QyTech.Core.BLL;
using QyTech.Core;
using QyTech.Core.CommUtils;

using QyTech.Core.Common;
using QyTech.Json;

namespace QyTech.SoftConf.UIParent
{

    public partial class IqyfTreeView : qyFrmBase
    {

        protected List<qytvNode> Nodes_;
        protected List<qytvNode> initcheckedNodes_;

        protected bsTable bsT_;

        //Dictionary<string, string> dicTInterface = new Dictionary<string, string>();

        public IqyfTreeView()
        {
            InitializeComponent();
        }

        public IqyfTreeView(List<qytvNode> Nodes, bsTable bsT)
        {
            InitializeComponent();
            Nodes_ = Nodes;
            bsT_ = bsT;
            initcheckedNodes_= qyTreeViewUtil.GetChecked(Nodes);
        }

        //base(, null, GlobalVaribles.SqConn_Base, Guid.Parse("0bd9e80f-1e96-4672-b34c-1266adb55f86"), "")
        private void IqyfTreeView_Load(object sender, EventArgs e)
        {
            qytvForm.LoadData(Nodes_, true);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            qytvForm.NodeCheckedWithParent = checkBox1.Checked;
        }
        protected virtual void qyBtn_Ok_Click(object sender, EventArgs e)
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
                PostAddNodes(addNodeIds);

            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        /// <summary>
        ///  删除数据，每个单独的保存都需要再子类中覆盖此方法完成删除
        /// </summary>
        /// <param name="delNodeIds">逗号分割的id串</param>
        protected virtual void PostDeLNodesWhere(string delNodeIds)
        {
            //调用服务器方法直接删除
            string url = GlobalVaribles.ServerUrl + "/softconf/bsTInterface/DeleteBysqlwhere";
            Dictionary<string, string> dicwhere = new Dictionary<string, string>();
            QyJsonData qyjson = QyTech.Utils.HttpRequestUtils.PostRemoteJsonQy(url, dicwhere);

        }
        /// <summary>
        /// 增加数据，每个单独的保存都需要再子类中覆盖此方法完成新增
        /// </summary>
        /// <param name="addNodeIds">逗号分割的id串</param>
        protected virtual void PostAddNodes(string addNodeIds)
        {
        }


    }
}
