using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QyTech.SkinForm;
using QyExpress.Dao;
using System.Reflection;
using QyTech.Core.BLL;
using System.Text.RegularExpressions;

using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace QyTech.UICreate
{
    public enum AddOrEdit { None=0,Add=1,Edit=2 }

    public partial class qyfAdd : qyFormWithTitle
    {

        protected SqlConnection sqlConn;
        protected bsTable bstable;
        List<QyExpress.Dao.bsFunField> _bffs;


        protected AddOrEdit addoredit = AddOrEdit.Add;

        private string DataRowOrEntityObject;
        protected object efobj_;
        protected DataRow drobj_;
        protected string TpkValue = "";

        protected Dictionary<string, object> HiddenFieldsValue = new Dictionary<string, object>();


        protected Dictionary<string, Control> dicControls = new Dictionary<string, Control>();

        public qyfAdd()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 明确增加或修改
        /// </summary>
        /// <param name="addormodify"></param>
        /// <param name="obj">如果是新增，则为实体对象，如果是编辑，则为DataRow或实体对象</param>
        /// <param name="EM_app"></param>
        public qyfAdd(AddOrEdit addormodify, SqlConnection sqlconn, object obj, bsTable bst, List<bsFunField> bffs)
        {
            InitializeComponent();

            InitqyfAdd(addormodify, sqlconn, obj, bst, bffs);
        }

        public void InitqyfAdd(AddOrEdit addormodify, SqlConnection sqlconn, object obj, bsTable bst, List<bsFunField> bffs)
        {
            try
            {
                addoredit = addormodify;
                sqlConn = sqlconn;
                bstable = bst;
                _bffs = bffs;

                if (obj is DataRow)
                {
                    drobj_ = obj as DataRow;
                    DataRowOrEntityObject = "DataRow";
                    if (bstable.TPkType == "uniqueidentifier")
                        TpkValue = "'" + drobj_[bstable.TPk].ToString() + "'";
                    else
                        TpkValue = drobj_[bstable.TPk].ToString();
                }
                else
                {
                    efobj_ = obj;
                    DataRowOrEntityObject = "EntityObject";

                    PropertyInfo pi = efobj_.GetType().GetProperty(bstable.TPk);
                    if (bstable.TPkType == "uniqueidentifier")
                        TpkValue = "'" + pi.GetValue(efobj_).ToString() + "'";
                    else
                        TpkValue = pi.GetValue(efobj_).ToString();
                }


                if (addormodify == AddOrEdit.Edit)
                    this.Title = "编辑";

                else
                {
                    this.Title = "新增";
                    foreach (bsFunField ff in _bffs)
                    {
                        if (ff.VisibleInForm == false)
                        {
                            PropertyInfo pi = efobj_.GetType().GetProperty(ff.FName);
                            object v = pi.GetValue(efobj_);
                            if (v != null)
                                HiddenFieldsValue.Add(ff.FName, v);
                        }
                    }
                }
                gbContainer.Controls.Clear();
                InitFrom();

                //获取从简信息写入数组
                dicControls = new Dictionary<string, Control>();
                foreach (Control c in gbContainer.Controls)
                {
                    if (c is Label)
                        continue;
                    dicControls.Add(c.Name, c);
                }
            }
            catch(Exception ex)
            { }
        }

        private void frmAdd_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;

           // this.btnClose.Image = qyResources.exit_16;
            this.btnSave.Image = qyResources.save_16;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void InitFrom()
        {
            if (bstable != null)
                lblAddTitle.Text = bstable.Desp;
            else
                lblAddTitle.Text = "信息编辑";


            int gbWidth = 0, gbHeight = 0;

            object tmpobj = drobj_;
            if (DataRowOrEntityObject=="EntityObject")
                tmpobj = efobj_;

            Util.qyUICreate.CreateFormEditPart(sqlConn, tmpobj, _bffs, gbContainer, ref gbWidth, ref gbHeight);

            this.Height = gbHeight +this.Height;// -gbContainer.Height;//自身也要占高度
            this.Width = gbWidth + this.Width - gbContainer.Width;
            

            this.lblAddTitle.Location = new Point(this.Width / 2 - System.Text.Encoding.Default.GetBytes(this.lblAddTitle.Text).Length * 10 / 2, lblAddTitle.Location.Y);
            this.btnSave.Location = new Point(this.Width / 2 - this.btnSave.Width / 2, btnSave.Location.Y);


 
        }
        private void btnSave_Click(object sender, EventArgs e)
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

                foreach (bsFunField ff in _bffs)
                {
                    if (!dicFName2FType.ContainsKey(ff.FName))
                        dicFName2FType.Add(ff.FName, ff.OType);
                }

                int rcount = 0;
                if (addoredit == AddOrEdit.Edit)
                {
                    rcount=QyTech.DbUtils.SqlUtils.updateForAddForm(sqlConn, bstable, listC, dicFName2FType, TpkValue, HiddenFieldsValue);
                }
                else
                {
                    rcount=QyTech.DbUtils.SqlUtils.insertForAddForm(sqlConn, bstable, listC, dicFName2FType, HiddenFieldsValue);
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

        private void qyfAdd_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }


        //      1.验证用户名和密码：（"^[a-zA-Z]\w{5,15}$"）正确格式："[A-Z][a-z]_[0-9]"组成,并且第一个字必须为字母6 ~16位；

        //2.验证电话号码：（"^(\d{3.4}-)\d{7,8}$"）正确格式：xxx/xxxx-xxxxxxx/xxxxxxxx；

        //3.验证身份证号（15位或18位数字）：（"^\d{15}|\d{18}$"）；

        //4.验证Email地址：("^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")；

        //5.只能输入由数字和26个英文字母组成的字符串：("^[A-Za-z0-9]+$") ;

        //6.整数或者小数：^[0-9]+\.{0,1}[0-9]{0,2}$

        //7.只能输入数字："^[0-9]*$"。

        //8.只能输入n位的数字："^\d{n}$"。

        //9.只能输入至少n位的数字："^\d{n,}$"。

        //10.只能输入m ~n位的数字：。"^\d{m,n}$"

        //11.只能输入零和非零开头的数字："^(0|[1-9][0-9]*)$"。

        //12.只能输入有两位小数的正实数："^[0-9]+(.[0-9]{2})?$"。

        //13.只能输入有1 ~3位小数的正实数："^[0-9]+(.[0-9]{1,3})?$"。

        //14.只能输入非零的正整数："^\+?[1-9][0-9]*$"。

        //15.只能输入非零的负整数："^\-[1-9][]0-9"*$。

        //16.只能输入长度为3的字符："^.{3}$"。

        //17.只能输入由26个英文字母组成的字符串："^[A-Za-z]+$"。

        //18.只能输入由26个大写英文字母组成的字符串："^[A-Z]+$"。

        //19.只能输入由26个小写英文字母组成的字符串："^[a-z]+$"。

        //20.验证是否含有^%&',;=?$\"等字符："[^%&',;=?$\x22]+"。

        //21.只能输入汉字："^[\u4e00-\u9fa5]{0,}$"

        //22.验证URL："^http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$"。

        //23.验证一年的12个月："^(0?[1-9]|1[0-2])$"正确格式为："01"～"09"和"1"～"12"。

        //24.验证一个月的31天："^((0?[1-9])|((1|2)[0-9])|30|31)$"正确格式为；"01"～"09"和"1"～"31"。

    }
}
