using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QyTech.Core.BLL;

using QyTech.Auth.Dao;
using QyTech.SkinForm;


namespace QyTech.UICreate
{
    public partial class qyfLayouListWithDgv : qyFormWithTitle
    {
        protected bsFunConf bsFc;
        protected List<bsFunField> bffs;
        protected Dictionary<string, bsFunField> dicBFFs;
        protected bsTable bstable;


        protected object currRowObjId;//不是guid就是int
        protected int currRowIndex;
        protected object CurrRowObj;
        protected int tpkColumnIndex = 1;//标识列的索引号

        private EntityManager EM_Base;
        private EntityManager EM_App;
        private SqlConnection sqlConn;

        protected DataTable dtList;
        protected string tName;
        protected string strBaseWhere = "", strWhere = "", strOrderby = "Id";

        public Guid currOrgId;
        public string currOrgName;


        private int PbrMax = 100;

       
        /// <summary>
        /// 子类界面不显示，需要加这个构造函数
        /// </summary>
        public qyfLayouListWithDgv()
        {
            InitializeComponent();
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
        public qyfLayouListWithDgv(EntityManager em_Base, EntityManager em_App,SqlConnection conn,string tname,string where="",string orderby="")
        {
            InitializeComponent();

            EM_Base = em_Base;
            EM_App = em_App;
            sqlConn = conn;
            tName = tname;
            strBaseWhere = where;
            strOrderby = orderby;
        }

        /// <summary>
        /// 供外部调用
        /// </summary>
        /// <param name="bsfcid"></param>
        /// <param name="currUser"></param>
        public void Init(string bsfcid, bsUser currUser)
        {
            try
            {
                CurrLogUser = currUser;
                if (currUser.bsR_Name == "浏览人员")
                {
                    scDgv.SplitterDistance = scQuery.Panel1.Height;
                }
                bstable = EM_Base.GetByPk<bsTable>("TName", tName);
                bsFc = EM_Base.GetByPk<bsFunConf>("bsFC_Id", bsfcid);

                //ea4b62aa-472e-4b3f-b152-fccf61908cf8
               
                bffs = EM_Base.GetListNoPaging<bsFunField>("TName='" + tName + "'", "NoInList");
                
                dicBFFs = new Dictionary<string, bsFunField>();
                for (int i = 0; i < bffs.Count; i++)
                {
                    dicBFFs.Add(bffs[i].FName, bffs[i]);
                }

                RefreshDgv(strWhere);
            }
            catch (Exception ex) { log.Error("Init:" + ex.Message); }
        }



        private void qyDgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex == -1)
            //{
            //    tsbEdit.Enabled = false;
            //    return;
            //}
            //currRowIndex = e.RowIndex;
            //try
            //{
            //    currRowObjId = Convert.ToInt32(qyDgvList.Rows[currRowIndex].Cells[tpkColumnIndex].Value);
            //}
            //catch
            //{
            //    currRowObjId = Guid.Parse(qyDgvList.Rows[currRowIndex].Cells[tpkColumnIndex].Value.ToString());
            //}
            //tsbEdit.Enabled = true;
        }

        public void RefreshDgv(string where, string orderby = "")
        {
            try
            {
                PbrMax = bffs.Count + 100;
                ProgressChangeddEvent(5, PbrMax);

                if (orderby == "")
                    orderby = strOrderby;

                string strWhere = strBaseWhere;
                if (where != "")
                {
                    strWhere += " and " + where;
                }


                string strSql = "select * from [" + tName + "] " + (strWhere == "" ? "" : " where " + strWhere) + (orderby == "" ? "" : " order by " + orderby);//表名的写法也应注意不同，对应的excel表为sheet1，在这里要在其后加美元符号$，并用中括号
                SqlCommand Cmd = new SqlCommand(strSql, sqlConn);//建立要执行的命令
                SqlDataAdapter da = new SqlDataAdapter(Cmd);//建立数据适配器
                DataSet ds = new DataSet();
                da.Fill(ds, tName);//把数据适配器中的数据读到数据集中的一个表中（此处表名为shyman，可以任取表名）
                                   //指定datagridview1的数据源为数据集ds的第一张表（也就是shyman表），也可以写ds.Table["shyman"]
                ProgressChangeddEvent(15, PbrMax);

                dtList = ds.Tables[tName];
                qyDgvList.DataSource = dtList;
                ProgressChangeddEvent(25, PbrMax);

                ResetDgvHeader();
                ProgressChangeddEvent(PbrMax, PbrMax);

                //tsbEdit.Enabled = false;

            }
            catch (Exception ex) { log.Error("RefreshDgv:" + ex.Message); }
        }


        protected void tsbAddForm(object currobj)
        {
            qyfAdd frmaddobj = new qyfAdd(AddOrEdit.Add, sqlConn, currobj, this.bstable, bffs);
            frmaddobj.ShowDialog();
        }
        protected void tsbEditForm(object currobj)
        {
            try
            {
                qyfAdd frmaddobj = new qyfAdd(AddOrEdit.Edit, sqlConn, currobj, this.bstable, bffs);
                frmaddobj.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("请先选中某行数据");
            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("确定要删除所有选择的对象吗？", "提示", MessageBoxButtons.YesNo))
            {
                try
                {
                    for (int i = 0; i < qyDgvList.Rows.Count; i++)
                    {
                        bool cellv = Convert.ToBoolean(qyDgvList.Rows[i].Cells[0].Value);
                        if (cellv)
                        {
                            string strobjid = qyDgvList.Rows[i].Cells[tpkColumnIndex].Value.ToString();
                            if (currRowObjId is Int32)
                                ExceuteSql(sqlConn, "delete from " + tName + " where " + bstable.TPk + "=" + strobjid);
                            else if (currRowObjId is Guid)
                                ExceuteSql(sqlConn, "delete from " + tName + " where " + bstable.TPk + "='" + strobjid + "'");
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.Error("tsbDelete_Click:" + ex.Message);
                }

                RefreshDgv(strWhere, strOrderby);
            }
        }
        private void tsPbr_MouseHover(object sender, EventArgs e)
        {
            //tsPbr.ToolTipText = tsPbr.Value.ToString() + "/" + tsPbr.Maximum.ToString();
        }

        public void ResetDgvHeader()
        {

            for (int i = 0; i < bffs.Count; i++)
            {
                try
                {
                    qyDgvList.Columns[bffs[i].FName].Visible = bffs[i].VisibleInList == null ? true : (bool)bffs[i].VisibleInList;
                    qyDgvList.Columns[bffs[i].FName].HeaderText = bffs[i].FDesp;
                    qyDgvList.Columns[bffs[i].FName].Width = (int)bffs[i].FWidthInList;

                    ProgressChangeddEvent(25 + (i + 1), PbrMax);

                }
                catch (Exception ex) { log.Error("RefreshDgv:" + ex.Message); }
            }
        }


        private void qyDgvList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.qyDgvList.IsCurrentCellDirty) //有未提交的更//改
            {
                this.qyDgvList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void qyDgvList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void qyfLayouListWithDgv_Load(object sender, EventArgs e)
        {

        }

        public static int ExceuteSql(SqlConnection sqlconn, string strSql)
        {
            try
            {
                sqlconn.Open();
                SqlCommand Cmd = new SqlCommand(strSql, sqlconn);//建立要执行的命令
                int ret = Cmd.ExecuteNonQuery();
                sqlconn.Close();
                return ret;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        private void ProgressChangeddEvent(int value, int Total)
        {

            //tsPbr.Value = value;
            //if (value == Total)
            //{
            //    tsPbr.Visible = false;
            //}
            //else
            //{
            //    tsPbr.Visible = true;
            //    tsPbr.Maximum = (int)Total;
            //    tsPbr.Minimum = 0;
            //}
        }
    }
}
