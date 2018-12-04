using System;
using System.Collections.Generic;

using System.Data;

using System.Windows.Forms;

using System.Data.Objects;
using System.Data.SqlClient;
using QyTech.Core.BLL;
using QyExpress.Dao;
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
        protected List<bsFunField> bffs;//按照noinlist排序的
        protected List<bsFunField> bffs_byFormNo;//按照formno排序的
        protected Dictionary<string, bsFunField> dicBFFs;
        protected bsTable bstable;
        protected string selectfields = "*";


        protected object currRowTPkId;//行主键  一般不是guid就是int 行主键
        protected int currRowIndex;
        protected object CurrRowObj;
        protected string TPkType = "Guid";


        protected ObjectContext DB_Base;
        protected ObjectContext DB_App;
        protected SqlConnection sqlConn;

        protected DataTable dtList;
        protected string tName;
        protected string strBaseWhere = "", strWhere = "", strOrderby = "";

        public Guid currOrgId;
        public string currOrgName;


        protected int PbrMax = 100;

        protected qyDgv _qyDgvList;
        protected Dictionary<string, Control> dicQueryControls=new Dictionary<string, Control>();
        
        private qyfAdd frmaddobj=new qyfAdd();//用用新增和修改的界面调用
        protected qyfAdd subfrmAdd
        {
            get { return frmaddobj; }
            set { frmaddobj = value; }
        }

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
        public qyfLayoutListParent(ObjectContext db_Base, ObjectContext db_App, SqlConnection conn, 
            Guid bsFC_Id, string where = "")
        {
            InitializeComponent();

            try
            {
                DB_Base = db_Base;
                DB_App = db_App;
                sqlConn = conn;
                bsFc = EntityManager_Static.GetByPk<bsFunConf>(DB_Base, "bsFC_ID", bsFC_Id);

                bstable = EntityManager_Static.GetByPk<bsTable>(DB_Base, "bsT_Id",bsFc.bsT_Id);
                tName = bstable.TName;
                if (bsFc.basesqlwhere == null)
                {
                    if (where != "")
                        strBaseWhere = "(" + where + ")";
                    else
                        strBaseWhere = "";
                }
                else
                    strBaseWhere = "(" + bsFc.basesqlwhere + ")";
                if (bsFc.OrderBySql == null)
                    strOrderby = "";
                else
                    strOrderby = bsFc.OrderBySql;

                this.Title = bsFc.FunDesp;


                bffs = EntityManager_Static.GetListNoPaging<bsFunField>(DB_Base, "bsFC_Id='" + bsFc.bsFC_Id.ToString() + "'", "NoInList");
                if (bffs.Count > 0)
                {
                    selectfields = "";
                    foreach (bsFunField ff in bffs)
                    {
                        selectfields += "," + ff.FName;
                    }
                    if (selectfields.Length > 0)
                        selectfields = selectfields.Substring(1);
                }
                bffs_byFormNo = EntityManager_Static.GetListNoPaging<bsFunField>(DB_Base, "bsFC_Id='" + bsFc.bsFC_Id.ToString() + "'", "NoInForm");
            }
            catch(Exception ex)
            { }
        }

        private void frmListWithLeft_Load(object sender, EventArgs e)
        {
            qyLayoutUtil.FormLoad(this);

            CreateQueryGroupBox();

            RefreshDgv(dgvList, "", "");


        }


        #region 列表浏览dgv事件

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1) return;


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

                #region  获取当前行对象
                Type typeEm = typeof(QyTech.Core.SqlUtils.Sql2Entity);
                Type dbtype = Type.GetType("QyExpress.Dao." + bstable.TName + ",QyExpress.Dao,Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                object dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);
                System.Reflection.MethodInfo miObj = typeEm.GetMethod("DataRow2Entity").MakeGenericMethod(dbtype);//获取泛型类方法,不能有重名的，否则找不到，2018-10-06有一个错误，就是一个实例，一个静态，报错了
                                                                                                                        //静态方法，所以Invode的第一个参数为null
                CurrRowObj = miObj.Invoke(null, new object[] { (dgvList.DataSource as DataTable).Rows[e.RowIndex] });
                #endregion

                //行操作
                if (dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "删除")
                {
                    DeleteRow(e.RowIndex);
                    CurrRowObj = null;
                }
                else if (dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "保存")
                {
                    SaveRow(e.RowIndex);
                }
                else if (dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "查看/编辑")
                {
                    //CurrRowObj = QyTech.DbUtils.SqlUtils.DataRow2EntityObject<bsFunField>((dgvList.DataSource as DataTable).Rows[e.RowIndex]);
                    //EditRow(CurrRowObj);
                    EditRow((dgvList.DataSource as DataTable).Rows[e.RowIndex]);
                }
            }
            catch (Exception ex)
            { }
        }

        private void dgvList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
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
                    if (strWhere != "")
                        strWhere += " and " + where;
                    else
                        strWhere = where;
                }

                qyDgvListUtil.RefreshDgv(dgv, sqlConn, tName, selectfields,strWhere, orderby);

                ResetDgvHeader(dgv);
            }
            catch (Exception ex) {
                //log.Error("RefreshDgv:" + ex.Message);
            }
        }

        /// <summary>
        /// 刷新表格头
        /// </summary>
        /// <param name="dgv"></param>
        private void ResetDgvHeader(qyDgv dgv)
        {
            if (bffs != null&& bffs.Count>0)
            {
                qyDgvListUtil.ResetDgvHeader(dgv, bffs);
            }
        }


        /// <summary>
        /// 用于修改的checkbox判断是否选中使用，暂时未使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        #endregion

        #region 增删改功能+保存
        /// <summary>
        /// 增加功能
        /// </summary>
        /// <param name="currRowObj"></param>
        protected void Add(object currRowObj)
        {
            CurrRowObj = currRowObj;
            qyfAdd frmaddobj = new qyfAdd(AddOrEdit.Add, sqlConn, currRowObj,bstable, bffs_byFormNo);
            frmaddobj.ShowDialog();
        }

        protected virtual void CreateAddForm()
        {
            frmaddobj = new qyfAdd();
        }

        /// <summary>
        /// 调用界面编辑行数据
        /// </summary>
        /// <param name="currRowObj"></param>
        protected void EditRow(object currRowObj)
        {
            try
            {
                CreateAddForm();
                frmaddobj.InitqyfAdd(AddOrEdit.Edit, sqlConn, currRowObj, bstable, bffs_byFormNo);
                frmaddobj.ShowDialog();
                //RefreshDgv();
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

        #endregion


        #region 查询框
        private void qyBtn_Refresh_Click(object sender, EventArgs e)
        {
            RefreshDgv();
        }

        protected void RefreshDgv()
        {
            strWhere = CreateWhere();
            RefreshDgv(dgvList, strWhere);

        }

        private void qyfLayoutListParent_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                DB_Base.Dispose();
                DB_Base = null;

                if (DB_App != null)
                {
                    DB_App.Dispose();
                    DB_App = null;
                }
            }
            catch { }
        }

        private string CreateWhere()
        {
            string Conditions = "";
            foreach (Control c in gbCondition.Controls)
            {
                if (c is Label)
                    continue;
                try
                {
                    if (c is CheckBox)
                    {
                        CheckBox cb = c as CheckBox;
                        Conditions += " and " + c.Tag.ToString().Replace("@@@@", cb.Checked?"1":"0");
                    }
                    else
                    {
                        if (c.Text != "")
                        {
                            if (c is TextBox)
                            {
                                Conditions += " and " + c.Tag.ToString().Replace("@@@@", c.Text);
                            }
                            else if (c is ComboBox)//根据分号分割条件
                            {
                                ComboBox cb = c as ComboBox;
                                //如果显示与实际参数不同，有对应关系，则可考虑使用tag中字符串再split使用value
                                if (cb.Tag.ToString() != "")
                                {
                                    int cb_selectindex = cb.SelectedIndex;
                                    string[] conds = c.Tag.ToString().Split(new char[] { ';' });
                                    int condindex = cb_selectindex < conds.Length ? cb_selectindex : conds.Length - 1;//如果条件不够，就用最后一个条件
                                    Conditions += " and " + conds[condindex].Replace("@@@@", c.Text);
                                }
                            }

                        }
                    }
                }
                catch { }
            }
            if (Conditions.Length > 5)
                Conditions = Conditions.Substring(4);

            return Conditions;
        }
        private void CreateQueryGroupBox()
        {
            try
            {
                //参数未实体对象
                List<bsFunQuery> fqs = EntityManager_Static.GetListNoPaging<bsFunQuery>(DB_Base, "bsFC_Id='" + bsFc.bsFC_Id.ToString() + "'", "");
                int gbWidth =0;
                int gbHeight = gbCondition.Height;
                if (fqs.Count > 0)
                {
                    Util.qyUICreate.CreateFunQueryPart(sqlConn, fqs, gbCondition, ref gbWidth, ref gbHeight);

                    gbCondition.Width = gbWidth;
                    //gbCondition.Height = gbHeight;
                    this.scDgv.SplitterDistance = this.scDgv.SplitterDistance + (gbHeight - 34);//scdgv的上面pandel高度-
                }
                else
                {
                    gbCondition.Width = 0;
                    //this.scDgv.SplitterDistance = 34;
                }
                qyBtn_Refresh.Left = gbCondition.Left + gbWidth + 10;

                foreach(Control c in gbCondition.Controls)
                {
                    if (c is Label)
                    {
                        dicQueryControls.Add(c.Name, c);
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion


    }

}
