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
using QyTech.Core.Common;

using QyTech.UICreate.UIUtils;


namespace QyTech.UICreate
{
    public partial class qyfLayTreeEdit : qyFormWithTitle
    {
        protected bsFunConf bsFc;
        protected List<bsFunField> bffs;//按照formno排序的
        protected Dictionary<string, bsFunField> dicBFFs;
        protected bsTable bstable;


        protected object currTPkId;//行主键  一般不是guid就是int 行主键
        protected object CurrSelectObj;
        protected string TPkType = "Guid";


        protected ObjectContext DB_Base;
        protected ObjectContext DB_App;
        protected SqlConnection sqlConn;


        protected string tName;
        protected string strBaseWhere = "", strWhere = "", strOrderby = "";

        Dictionary<string, object> HiddenFieldsValue = new Dictionary<string, object>();

        protected AddOrEdit addoredit;

        public qyfLayTreeEdit()
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
        public qyfLayTreeEdit(ObjectContext db_Base, ObjectContext db_App, SqlConnection conn, 
            Guid bsFC_Id, string where = "")
        {
            InitializeComponent();

            try
            {
                DB_Base = db_Base;
                DB_App = db_App;
                sqlConn = conn;
                bsFc = EntityManager_Static.GetByPk<bsFunConf>(DB_Base, "bsFC_ID", bsFC_Id);

                bstable = EntityManager_Static.GetByPk<bsTable>(DB_Base, "bsT_Id", bsFc.bsT_Id);
                tName = bstable.TName;
                if (bsFc.baseWhereSql == null)
                {
                    if (where != "")
                        strBaseWhere = "(" + where + ")";
                    else
                        strBaseWhere = "";
                }
                else
                    strBaseWhere = "(" + bsFc.baseWhereSql + ")";
                if (bsFc.OrderBySql == null)
                    strOrderby = "";
                else
                    strOrderby = bsFc.OrderBySql;

                this.Title = bsFc.FunDesp;


                bffs = EntityManager_Static.GetListNoPaging<bsFunField>(DB_Base, "bsFC_Id='" + bsFc.bsFC_Id.ToString() + "'", "NoInForm");
            }
            catch(Exception ex)
            { }
        }

        private void frmListWithLeft_Load(object sender, EventArgs e)
        {
            qyLayoutUtil.FormLoad(this);

        }


        protected void InitFrom(object currobj)
        {
            HiddenFieldsValue.Clear();
            //应该首先对非显示数据 存放到HiddenFieldsValue中
            foreach (bsFunField ff in bffs)
            {
                if (ff.VisibleInForm == false)
                {
                    System.Reflection.PropertyInfo pi = currobj.GetType().GetProperty(ff.FName);
                    object v = pi.GetValue(currobj);
                    if (v != null)
                        HiddenFieldsValue.Add(ff.FName, v);
                }
            }
            int gbWidth = 0, gbHeight = 0;


            Util.qyUICreate.CreateFormEditPart(sqlConn, currobj, bffs, gbContainer, ref gbWidth, ref gbHeight);
            //gbContainer.Height = gbHeight;
            //gbContainer.Width = gbWidth;
            //this.Height = gbHeight + this.Height;// -gbContainer.Height;//自身也要占高度
            //this.Width = gbWidth + this.Width - gbContainer.Width;

            //for (int i=0;i<bsFc.)
       }




        #region 增删改功能+保存

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshTree();
        }

        private void 新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();

        }

        protected  void qyBtn_Save_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void qytvLeft_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                currTPkId = (e.Node.Tag as qytvNode).id;
                CurrSelectObj = EntityManager_Static.GetByPk<bsOrganize>(DB_Base, "bsO_Id", currTPkId);
                InitFrom(CurrSelectObj);
            }
            catch(Exception ex)
            {
                QyTech.Core.LogHelper.Error(ex);
            }
        }

        #endregion

        #region 子类实现或根据业务可覆盖

        /// <summary>
        /// 刷新树，子类实现
        /// </summary>
        protected virtual void RefreshTree()
        {

        }


        /// <summary>
        /// 增加功能，子类实现
        /// </summary>
        /// <param name="currRowObj"></param>
        protected virtual void Add()
        {
           
        }

        /// <summary>
        /// 删除功能，子类可覆盖,一般应该可以了
        /// </summary>
        protected virtual void Delete()
        {
            int ret = -1;
            if (DialogResult.Yes == MessageBox.Show("确定要删除所有选择的对象吗？", "提示", MessageBoxButtons.YesNo))
            {
                try
                {
                    if (bstable.TPkType=="int")
                        ret = QyTech.DbUtils.SqlUtils.ExceuteSql(sqlConn, "delete from " + tName + " where " + bstable.TPk + "=" + currTPkId.ToString());
                    else if (bstable.TPkType=="uniqueidentifier")
                        ret = QyTech.DbUtils.SqlUtils.ExceuteSql(sqlConn, "delete from " + tName + " where " + bstable.TPk + "='" + currTPkId.ToString() + "'");

                    if (ret == -1)
                    {
                        MessageBox.Show("删除失败！");
                    }
                    else
                    {
                        qytvLeft.Nodes.Remove(qytvLeft.SelectedNode);
                        //RefreshTree(_qyDgvList, strWhere, strOrderby);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Delete:" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 保存编辑数据，子类可覆盖
        /// </summary>
        protected virtual void Save()
        {
            try
            {
                string ret = "";

                List<Control> listC = new List<Control>();
                foreach (Control c in gbContainer.Controls)
                {
                    if (c.GetType().Name.ToLower() != "label")
                    {
                        listC.Add(c);
                    }
                }
                Dictionary<string, string> dicFName2FType = new Dictionary<string, string>();

                foreach (bsFunField ff in bffs)
                {
                    if (!dicFName2FType.ContainsKey(ff.FName))
                        dicFName2FType.Add(ff.FName, ff.OType);
                }

                int rcount = 0;
                if (addoredit == AddOrEdit.Edit)
                {
                    rcount = QyTech.DbUtils.SqlUtils.updateForAddForm(sqlConn, bstable, listC, dicFName2FType, currTPkId.ToString(), HiddenFieldsValue);
                }
                else
                {
                    rcount = QyTech.DbUtils.SqlUtils.insertForAddForm(sqlConn, bstable, listC, dicFName2FType, HiddenFieldsValue);
                }

                if (rcount == 1)
                {
                    MessageBox.Show("保存成功！");
                }
                else
                {
                    MessageBox.Show("保存失败!(" + ret + ")");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion


   
        private void qyfLayoutListParent_FormClosed(object sender, FormClosedEventArgs e)
        {
            DB_Base.Dispose();
            DB_Base = null;

            if (DB_App != null)
            {
                DB_App.Dispose();
                DB_App = null;
            }
        }

      
     


    }

}
