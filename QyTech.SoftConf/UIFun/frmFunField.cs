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
using QyTech.Core.Common;



namespace QyTech.SoftConf.UIList
{
    public partial class frmFunField : qyfLayoutListWithLeft
    {
        public frmFunField() 
            : base(GlobalVaribles.ObjContext_Base, GlobalVaribles.ObjContext_App, GlobalVaribles.SqConn_Base,
                  Guid.Parse("F98AD688-66F8-439D-A6E1-66B81640721D"), "")
        {
            InitializeComponent();

        }

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmFunField_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;


            dgvList.addDgvSaveButton();

            List<qytvNode> nodes = BLL.commService.GetFunConfs(DB_Base, strBaseWhere);
            qytvDbTable.LoadData(nodes);
        }
        protected override void CreateAddForm()
        {
            subfrmAdd = new UIAdd.qyfFunFieldfAdd();
        }
        /// <summary>
        /// 左侧条件选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void qytvDbTable_AfterSelect(object sender, TreeViewEventArgs e)
        {

            TreeNode tn = e.Node;
            qytvNode tntag = tn.Tag as qytvNode;

            strBaseWhere = "bsFC_Id='" + tntag.id + "'";
            CurrLeftPFk = tntag.id;

            RefreshDgv();
        }



        #region 工具栏操作

        /// <summary>
        /// 选择删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbDels_Click(object sender, EventArgs e)
        {
            base.DeleteRows();
        }
        private void tsbSetQueryField_Click(object sender, EventArgs e)
        {
            string sqls = "exec bslyAddFunFieldToFunQuery '" + currRowTPkId.ToString() + "'";
            QyTech.DbUtils.SqlUtils.ExceuteSql(GlobalVaribles.SqConn_Base, sqls);
        }

        private void tsbAddNewField_Click(object sender, EventArgs e)
        {
            string sqls = "exec bslyAppendFunAllFields2FCField '" + CurrLeftPFk.ToString() + "'";
            int ret=QyTech.DbUtils.SqlUtils.ExceuteSql(GlobalVaribles.SqConn_Base, sqls);
            if (ret == -2)
            {
                MessageBox.Show("操作失败");
            }
            //else if (ret==-1)
                //MessageBox.Show("没有需要导入的数据");
            else
                MessageBox.Show("操作成功");
        }

        #region 字段位置移动
        private void tsmiListLeft_Click(object sender, EventArgs e)
        {
            ChangeOrder("list", currRowIndex, "up");

        }

        private void tsmiListRight_Click(object sender, EventArgs e)
        {
            ChangeOrder("list", currRowIndex, "down");

        }

        private void tsmiEditUp_Click(object sender, EventArgs e)
        {
            ChangeOrder("edit", currRowIndex, "up");

        }

        private void tsmiEditDown_Click(object sender, EventArgs e)
        {
            ChangeOrder("edit", currRowIndex, "down");

        }



        private void ChangeOrder(string ListOrEdit, int rowindex, string upordown)
        {
            int ToRow = rowindex;
            bsFunField RowObj1;
            bsFunField RowObj2;
            Guid toRowTpkId;// urrRowTPkId;
            if (upordown == "up")
            {
                if (rowindex == 0)
                    return;
                ToRow = rowindex - 1;
            }
            else
            {
                if (rowindex ==dgvList.RowCount - 1)
                    return;
                ToRow = rowindex + 1;
            }
            toRowTpkId = Guid.Parse(dgvList.Rows[ToRow].Cells["bsFF_Id"].Value.ToString());//["NoInList"]
            RowObj1 = EntityManager_Static.GetByPk<bsFunField>(DB_Base,"bsFF_Id", currRowTPkId);
            RowObj2 = EntityManager_Static.GetByPk<bsFunField>(DB_Base, "bsFF_Id", toRowTpkId);

            if (ListOrEdit == "list")
            {
                int tmp = (int)RowObj1.NoInList;
                RowObj1.NoInList = RowObj2.NoInList;
                RowObj2.NoInList = tmp;
            }
            else
            {
                int tmp = (int)RowObj1.NoInForm;
                RowObj1.NoInForm = RowObj2.NoInForm;
                RowObj2.NoInForm = tmp;
            }
            EntityManager_Static.Modify<bsFunField>(DB_Base, RowObj1);
            EntityManager_Static.Modify<bsFunField>(DB_Base, RowObj2);
            RefreshDgv(dgvList, "");
        }

        #endregion
        #endregion



    }
}
