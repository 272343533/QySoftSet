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
using QyTech.SkinForm.Controls;
using QyTech.Core.BLL;
using System.Data.Objects;
using System.Data.SqlClient;
using QyTech.Core.Common;

namespace QyTech.UICreate
{
    public partial class qyfLayListWithLeftTree : qyfLayListParent
    {
        List<bsFunQuery> fqs_Left;
        /// <summary>
        /// 子类界面不显示，需要加这个构造函数
        /// </summary>
        public qyfLayListWithLeftTree()
        {
            InitializeComponent();
            this.scForm.SplitterDistance = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="em_Base">基本EM</param>
        /// <param name="em_App">应用EM</param>
        /// <param name="conn">应用EM的SqlConn</param>
        /// <param name="tname">表或视图对象</param>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        public qyfLayListWithLeftTree(ObjectContext em_Base, ObjectContext em_App, SqlConnection conn, Guid bsFC_Id, string where = "")
            : base(em_Base, em_App, conn, bsFC_Id)
        {
            InitializeComponent();

            fqs_Left = EntityManager_Static.GetListNoPaging<bsFunQuery>(DB_Base, "bsFC_Id='" + bsFc.bsFC_Id.ToString() + "' and Itempos='左侧' and  QueryType='tree'", "");
        }



        private void qyfLayListWithLeftTree_Load(object sender, EventArgs e)
        {
        }

        protected virtual void LoadTreeData(string where = "")
        {

        }



        private void qytvLeft_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                currLeftFPk = (e.Node.Tag as qytvNode).id;
                currLeftText = e.Node.Text;
                //找到配置中的tree的查询条件，用这个条件对应的Sql即可，
                if (fqs_Left.Count > 0)
                {
                    if (fqs_Left[0].WhereSql.Contains("@@@@"))
                    {
                        RefreshDgv(dgvList, fqs_Left[0].WhereSql.Replace("@@@@", currLeftFPk.ToString()));
                    }
                    else
                    {
                        RefreshDgv(dgvList, fqs_Left[0].WhereSql.Replace("####", currLeftText));
                    }
                }
            }
            catch(Exception ex)
            { }
        }

        protected override string CreateWhere()
        {
            try
            {
                string Conditions = "";
                if (currLeftFPk != null)
                {
                    //获取左侧的wheresql，然后串起来
                    Conditions = fqs_Left[0].WhereSql.Replace("@@@@", currLeftFPk.ToString());

                    string upConditions = base.CreateWhere();
                    if (upConditions != "")
                        Conditions += " and " + upConditions;
                }
                else
                {
                    Conditions = "bsT_Id='"+Guid.Empty.ToString()+"'";
                }
                return Conditions;
            }
            catch { return ""; }
        }
    }
}
