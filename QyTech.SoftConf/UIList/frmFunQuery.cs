﻿using System;
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
    public partial class frmFunQuery : qyfLayoutListWithLeft
    {
        
        public frmFunQuery():base(GlobalVaribles.EM_Base, GlobalVaribles.EM_Base, GlobalVaribles.SqConn_Base, Guid.Parse("1CAC11A9-6D71-4140-B73C-0D9CE9B169BE"), "bsFC_Id in (select bsFC_Id from bsFunConf where bsN_Id in (select bsN_Id from bsNavigation where bsA_Id='" + GlobalVaribles.currAppObj.AppId.ToString()+"'))", "FQNo")
        {
            InitializeComponent();


        }

        private void frm_Load(object sender, EventArgs e)
        {
            this.Text = bsFc.FunDesp;

            RefreshDgv(dgvList, "bsFC_Id is null", "");

            List<qytvNode> nodes = BLL.commService.GetFunConfs(strBaseWhere);
            qytvDbTable.LoadData(nodes);
        }

        private void qyBtn_Refresh_Click(object sender, EventArgs e)
        {
            string sqlwhere = CreateWhere();
            RefreshDgv(dgvList,sqlwhere);
        }

        private string CreateWhere()
        {
            if (txtName.Text.Trim() != "")
                return "OperName like '%" + txtName.Text.Trim() + "%'";
            else
                return "";
        }

        private void qytvDbTable_AfterSelect(object sender, TreeViewEventArgs e)
        {

            TreeNode tn = e.Node;
            qytvNode tntag = tn.Tag as qytvNode;

            strBaseWhere = "bsFC_Id='" + tntag.Id + "'";


            string sqlwhere = CreateWhere();
            RefreshDgv(dgvList, sqlwhere);
        }


        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (base.qyDgvList_CellClick(sender, e) == -1)
                return;
            if (dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "删除")
            {
                DeleteRow(e.RowIndex);
            }
            else if (dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "查看/编辑")
            {
                CurrRowObj = QyTech.DbUtils.SqlUtils.DataRow2EntityObject<bsFunQuery>((dgvList.DataSource as DataTable).Rows[e.RowIndex]);
                EditRow(CurrRowObj);
            }
            else if (dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "保存")
            {
                SaveRow(e.RowIndex);
            }
        }
    }
}
