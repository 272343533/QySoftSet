using System;
using System.Collections.Generic;

using System.Data;

using System.Windows.Forms;

using System.Data.SqlClient;
using QyTech.Core.BLL;
using QyTech.Auth.Dao;
using QyTech.SkinForm;
using QyTech.SkinForm.Component;
using QyTech.SkinForm.Controls;
using QyTech.UICreate.Util;
using QyTech.DbUtils;

using QyTech.UICreate.UIUtils;

namespace QyTech.UICreate
{
    public partial class qyfLayoutListParent : qyFormWithTitle
    {
        protected bsFunConf bsFc;
        protected List<bsFunField> bffs;
        protected Dictionary<string, bsFunField> dicBFFs;
        protected bsTable bstable;


        protected object currRowTPkId;//行主键  一般不是guid就是int 行主键
        protected int currRowIndex;
        protected object CurrRowObj;
        protected string TPkType = "Guid";


        protected EntityManager EM_Base;
        protected EntityManager EM_App;
        protected SqlConnection sqlConn;

        protected DataTable dtList;
        protected string tName;
        protected string strBaseWhere = "", strWhere = "", strOrderby = "";

        public Guid currOrgId;
        public string currOrgName;


        protected int PbrMax = 100;

        protected qyDgv _qyDgvList;
        
 
        public qyfLayoutListParent()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 根据配置bsFunConf/或者表名称进行设置相应内容
        /// </summary>
        /// <param name="em_Base">基本EM</param>
        /// <param name="em_App">应用EM</param>
        /// <param name="conn">应用EM的SqlConn</param>
        /// <param name="tname">表或视图对象</param>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <summary>
        public qyfLayoutListParent(EntityManager em_Base, EntityManager em_App, SqlConnection conn, Guid bsFC_Id, string where = "", string orderby = "")
        {
            InitializeComponent();

            EM_Base = em_Base;
            EM_App = em_App;
            sqlConn = conn;
            bsFc = EM_Base.GetByPk<bsFunConf>("bsFC_ID", bsFC_Id);

            bstable = EM_Base.GetBySql<bsTable>("bsD_Name='" + bsFc.bsD_Name + "' and TName='" + bsFc.TName+"'");
            tName = bsFc.TName;
            if (bsFc.baseWhereSql== null)
                strBaseWhere = "(" + where+")";
            else
                strBaseWhere = "(" + bsFc.baseWhereSql+")";
            if (bsFc.OrderBySql == null)
                strOrderby = orderby;
            else
                strOrderby = bsFc.OrderBySql;

            this.Title = bsFc.FunDesp;

        }

        private void frmListWithLeft_Load(object sender, EventArgs e)
        {
            qyLayoutUtil.FormLoad(this);

        }

        protected int qyDgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1) return -1;
            currRowIndex = e.RowIndex;
            _qyDgvList = sender as qyDgv;
            try
            {
                try
                {
                    currRowTPkId = Convert.ToInt32(_qyDgvList.Rows[currRowIndex].Cells[_qyDgvList.tpkColumnIndex].Value);
                }
                catch
                {
                    currRowTPkId = Guid.Parse(_qyDgvList.Rows[currRowIndex].Cells[_qyDgvList.tpkColumnIndex].Value.ToString());
                }
            }
            catch (Exception ex)
            { }
            return 0;
        }

        public void RefreshDgv(qyDgv dgv,string where, string orderby = "")
        {
            try
            {
                //_qyDgvList = dgv;
                if (orderby == ""&& strOrderby!=null)
                    orderby = strOrderby;

                string strWhere = strBaseWhere;
                if (where != "")
                {
                    strWhere += " and " + where;
                }

                qyDgvListUtil.RefreshDgv(dgv, sqlConn, tName, strWhere, orderby);

                ResetDgvHeader(dgv);
            }
            catch (Exception ex) { log.Error("RefreshDgv:" + ex.Message); }
        }


        public void ResetDgvHeader(qyDgv dgv)
        {
            bffs = EM_Base.GetListNoPaging<bsFunField>("bsFC_Id='"+bsFc.bsFC_Id.ToString()+"'", "NoInList");
            if (bffs != null&& bffs.Count>0)
            {
                qyDgvListUtil.ResetDgvHeader(dgv, bffs);
            }
        }

        protected void Add(object currRowObj)
        {
            CurrRowObj = currRowObj;
            qyfAdd frmaddobj = new qyfAdd(AddOrEdit.Add, sqlConn, CurrRowObj,bstable, bffs);
            frmaddobj.ShowDialog();
        }
        protected void EditRow(object currRowObj)
        {
            try
            {
                CurrRowObj = currRowObj;

                qyfAdd frmaddobj = new qyfAdd(AddOrEdit.Edit, sqlConn, CurrRowObj, bstable, bffs);

                frmaddobj.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show("请先选中某行数据");
            }
        }

        //private void tsbDelete_Click(object sender, EventArgs e)
        protected void DeleteRows()
        {
            if (DialogResult.Yes == MessageBox.Show("确定要删除所有选择的对象吗？", "提示", MessageBoxButtons.YesNo))
            {
                try
                {
                    for (int i = 0; i < _qyDgvList.Rows.Count; i++)
                    {
                        bool cellv = Convert.ToBoolean(_qyDgvList.Rows[i].Cells[0].Value);
                        if (cellv)
                        {
                            string strobjid = _qyDgvList.Rows[i].Cells[_qyDgvList.tpkColumnIndex].Value.ToString();
                            if (currRowTPkId is Int32)
                                QyTech.DbUtils.SqlUtils.ExceuteSql(sqlConn, "delete from " + tName + " where " + bstable.TPk + "=" + strobjid);
                            else if (currRowTPkId is Guid)
                                QyTech.DbUtils.SqlUtils.ExceuteSql(sqlConn, "delete from " + tName + " where " + bstable.TPk + "='" + strobjid + "'");
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("tsbDelete_Click:" + ex.Message);
                }

                RefreshDgv(_qyDgvList, strWhere, strOrderby);
            }
        }
        protected void DeleteRow(int rowindex)
        {
            int ret = -1;
            if (DialogResult.Yes == MessageBox.Show("确定要删除所有选择第（"+(rowindex+1).ToString()+"）的数据吗？", "提示", MessageBoxButtons.YesNo))
            {
                try
                {
                    if (currRowTPkId is Int32)
                        ret=QyTech.DbUtils.SqlUtils.ExceuteSql(sqlConn, "delete from " + tName + " where " + bstable.TPk + "=" + currRowTPkId.ToString());
                    else if (currRowTPkId is Guid)
                        ret=QyTech.DbUtils.SqlUtils.ExceuteSql(sqlConn, "delete from " + tName + " where " + bstable.TPk + "='" + currRowTPkId.ToString() + "'");

                    if (ret == -1)
                    {
                        MessageBox.Show("删除失败！");
                    }
                    else
                    {
                        RefreshDgv(_qyDgvList, strWhere, strOrderby);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("tsbDelete_Click:" + ex.Message);
                }

               
            }
        }

        protected void SaveRow(int rowindex)
        {
            DataRow dr = (_qyDgvList.DataSource as DataTable).Rows[rowindex];
            int ret = SqlUtils.UpdateDr(sqlConn, dr, bstable);
            if (ret == -1)
            {
                MessageBox.Show("保存失败！");
            }
        }

        private void qyBtn_Refresh_Click(object sender, EventArgs e)
        {

        }

        private void qyDgvList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this._qyDgvList.IsCurrentCellDirty) //有未提交的更//改
            {
                this._qyDgvList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }


        private void dgvList_eventColumnOrderByChanged(string orderby)
        {
            strOrderby = orderby;
        }

    }

}
