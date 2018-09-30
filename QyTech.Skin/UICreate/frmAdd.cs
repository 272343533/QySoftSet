using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SunMvcExpress.Dao;
using QyTech.Auth.Dao;

using System.Reflection;

using QyTech.Core.BLL;
using System.Text.RegularExpressions;

namespace QyTech.SkinForm.UICreate
{
    public enum AddOrEdit { None=0,Add=1,Edit=2 }

    public partial class frmAdd : qyFormWithTitle
    {
        protected AddOrEdit addoredit = AddOrEdit.Add;
        List<QyTech.Auth.Dao.bsFunField> funfields;
        bsFunConf bsFc;
        protected object obj_;
        string objtype_;


        public static EntityManager QyTech_EM = new EntityManager(new QyTech.Auth.Dao.QyTech_AuthEntities());
        public static EntityManager EM_Wj;// = new EntityManager(new wj_GisDbEntities());

        int initWidth = 5 + 245 + 245 + 5 + 10;
        int InitHeight = 110;

        public frmAdd()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 根据obj是否为null判断是增加还是修改
        /// </summary>
        /// <param name="objtype"></param>
        /// <param name="obj"></param>
        /// <param name="EM_app"></param>
        public frmAdd(string objtype, object obj, EntityManager EM_app)
        {
            InitializeComponent();

           
            EM_Wj = EM_app;
            objtype_ = objtype;
            //bsFc=MainForm.QyTech_EM.getbyp
            if (obj != null)
            {
                addoredit = AddOrEdit.Edit;
                obj_ = obj;
                this.Title = "编辑";

            }
            else
            {
                if (objtype == "bsUser")
                {
                    Assembly ass = Assembly.LoadFrom(@"QyTech.Auth.Dao.dll");
                    Type type = ass.GetType("QyTech.Auth.Dao." + objtype_);
                    obj_ = Activator.CreateInstance(type);
                }
                else
                {
                    Assembly ass = Assembly.LoadFrom(@"SunMvcExpress.Dao.dll");
                    Type type = ass.GetType("SunMvcExpress.Dao." + objtype_);
                    obj_ = Activator.CreateInstance(type);
                }
                this.Title = "新增";

            }
            InitFrom();
        }


        /// <summary>
        /// 明确增加或修改
        /// </summary>
        /// <param name="addormodify"></param>
        /// <param name="obj"></param>
        /// <param name="EM_app"></param>
        public frmAdd(AddOrEdit addormodify, object obj, EntityManager EM_app)
        {
            InitializeComponent();


            EM_Wj = EM_app;
            objtype_ = obj.GetType().Name ;
            obj_ = obj;
            addoredit = addormodify;

            if (addormodify== AddOrEdit.Edit)
            {
                this.Title = "编辑";

            }
            else
            {
                this.Title = "新增";
            }
            InitFrom();
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

            bsTable dbtable = QyTech_EM.GetBySql<bsTable>("TName='" + objtype_ + "' and bsD_Name='wj_GisDb'");
            if (dbtable != null)
                lblAddTitle.Text = dbtable.Desp;
            else if (objtype_ == "bsUser")
                lblAddTitle.Text = "用户信息";
            else
                lblAddTitle.Text = "信息编辑";


            funfields = QyTech_EM.GetListNoPaging<bsFunField>("TName='" + objtype_ + "' and VisibleInForm=1", "NoInForm");
            int gbWidth = 0, gbHeight = 0;
            UICreate.CreateFormEditPart(obj_, funfields, gbContainer, ref gbWidth, ref gbHeight);

            this.Height = InitHeight+ gbHeight + 30;// InitHeight+ (lheight > rheight ? lheight : rheight) +30;
            this.Width = gbWidth + 10;
            

            this.lblAddTitle.Location = new Point(this.Width / 2 - System.Text.Encoding.Default.GetBytes(this.lblAddTitle.Text).Length * 10 / 2, lblAddTitle.Location.Y);
            this.btnSave.Location = new Point(this.Width / 2 - this.btnSave.Width / 2, btnSave.Location.Y);

            //int stepX = 250;
            //int stepY = 40;
            //int InitLocationX = 0, InitLocationY = 10;//初始从0，10开始铺设控件

            //if (funfields.Count > 0)
            //{
            //    int num = -1;
            //    int LocationX, LocationY;
            //    int colCount = (int)Math.Ceiling(funfields.Count / 13.0);//每列最多十三行，进行列扩充

            //    this.Width += 250 * (colCount - 1);


            //    LocationX = InitLocationX;
            //    LocationY = InitLocationY;
            //    int mulTextCount = 0;
            //    foreach (bsFunField ff in funfields)
            //    {
            //        if (ff.VisibleInForm != false && ff.FEditType != null && "select,text,multext".Contains(ff.FEditType))
            //        {
            //            num++;
            //            LocationX = InitLocationX + num % colCount * stepX;
            //            LocationY = InitLocationY + num / colCount * stepY;
            //            //LocationX +=  num % colCount * stepX;
            //            //LocationY +=  num / colCount * stepY;

            //            string cname = ff.FDesp;


            //            string Fvalue = QyTechReflection.GetObjectPropertyValue(obj_, ff.FName);

            //            if (ff.FEditType == "select")//combox
            //            {
            //                UICreate.CreateCombobox(gbContainer, ff.FDesp, ff.FName, ff.FEditValueEx, Fvalue, LocationX, LocationY);
            //            }
            //            else if (ff.FEditType == "text")
            //            {
            //                //text
            //                UICreate.CreateTextDisplay(gbContainer, ff.FDesp, ff.FName, Fvalue, LocationX, LocationY);
            //            }
            //            else if (ff.FEditType == "multext")
            //            {
            //                mulTextCount++;
            //                if (LocationX != InitLocationX) {
            //                    LocationX = InitLocationX;
            //                    LocationY += stepY;
            //                 }
            //                //text
            //                UICreate.CreateMultiTextDisplay(gbContainer, ff.FDesp, ff.FName, Fvalue, LocationX, LocationY,90,gbContainer.Width-150);

            //            }

            //        }

            //    }
            //int RowCount = (int)Math.Ceiling(1.0 * (num + 1) / colCount);
            //if ((int)Math.Ceiling(1.0 * (num + 1) / colCount)==  (num + 1) / colCount)//整数
            //{
            //    this.Height = 120 + (RowCount + mulTextCount) * stepY;

            //}
            //else//有多的
            //{
            //    this.Height = 120 + RowCount * stepY;

            //}

            //this.lblAddTitle.Location = new Point(this.Width / 2 - System.Text.Encoding.Default.GetBytes(this.lblAddTitle.Text).Length * 10 / 2, lblAddTitle.Location.Y);
            //this.btnSave.Location = new Point(this.Width / 2 - this.btnSave.Width / 2, btnSave.Location.Y);

            //this.btnSave.Location = new Point(this.Width / 2 - this.btnSave.Width - 100, btnSave.Location.Y);
            //this.btnClose.Location = new Point(this.Width / 2 + 100, btnClose.Location.Y);

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Type t = obj_.GetType();
                string ret = "";
                if (addoredit == AddOrEdit.Edit)
                {
                    if (t.Name.Contains("企业范围"))
                    {
                        obj_ = EM_Wj.GetByPk<企业范围>("OBJECTID", (obj_ as 企业范围).OBJECTID);
                    }
                    else if (t.Name.Contains("房屋建筑"))
                    {
                        obj_ = EM_Wj.GetByPk<房屋建筑>("OBJECTID", (obj_ as 房屋建筑).OBJECTID);

                    }
                    else if (t.Name.Contains("城市规划"))
                    {
                        obj_ = EM_Wj.GetByPk<城市规划>("OBJECTID", (obj_ as 城市规划).OBJECTID);
                    }

                    else if (t.Name.Contains("t安全事故情况"))
                    {
                        obj_ = EM_Wj.GetByPk<t安全事故情况>("Id", (obj_ as t安全事故情况).Id);
                    }
                    else if (t.Name.Contains("t标准化级别"))
                    {
                        obj_ = EM_Wj.GetByPk<t标准化级别>("Id", (obj_ as t标准化级别).Id);
                    }
                    else if (t.Name.Contains("t高企"))
                    {
                        obj_ = EM_Wj.GetByPk<t高企>("Id", (obj_ as t高企).Id);
                    }
                    else if (t.Name.Contains("t工程技术研究中心"))
                    {
                        obj_ = EM_Wj.GetByPk<t工程技术研究中心>("Id", (obj_ as t工程技术研究中心).Id);
                    }
                    else if (t.Name.Contains("t工业固定资产"))
                    {
                        obj_ = EM_Wj.GetByPk<t工业固定资产>("Id", (obj_ as t工业固定资产).Id);
                    }
                    else if (t.Name.Contains("t规上企业名单"))
                    {
                        obj_ = EM_Wj.GetByPk<t规上企业名单>("Id", (obj_ as t规上企业名单).Id);
                    }
                    else if (t.Name.Contains("t经发局表格"))
                    {
                        obj_ = EM_Wj.GetByPk<t经发局表格>("Id", (obj_ as t经发局表格).Id);
                    }
                    else if (t.Name.Contains("t立案处罚情况"))
                    {
                        obj_ = EM_Wj.GetByPk<t立案处罚情况>("Id", (obj_ as t立案处罚情况).Id);
                    }
                    else if (t.Name.Contains("t企业基础数据"))
                    {
                        obj_ = EM_Wj.GetByPk<t企业基础数据>("Id", (obj_ as t企业基础数据).Id);
                    }
                    else if (t.Name.Contains("t企业技术中心台账"))
                    {
                        obj_ = EM_Wj.GetByPk<t企业技术中心台账>("Id", (obj_ as t企业技术中心台账).Id);
                    }
                    else if (t.Name.Contains("t清洁生产历年"))
                    {
                        obj_ = EM_Wj.GetByPk<t清洁生产历年>("Id", (obj_ as t清洁生产历年).Id);
                    }
                    else if (t.Name.Contains("t市局表格"))
                    {
                        obj_ = EM_Wj.GetByPk<t市局表格>("Id", (obj_ as t市局表格).Id);
                    }
                    else if (t.Name.Contains("t同里镇开发区上市企业台帐三板"))
                    {
                        obj_ = EM_Wj.GetByPk<t同里镇开发区上市企业台帐三板>("Id", (obj_ as t同里镇开发区上市企业台帐三板).Id);
                    }
                    else if (t.Name.Contains("t同里镇开发区上市企业台帐台资"))
                    {
                        obj_ = EM_Wj.GetByPk<t同里镇开发区上市企业台帐台资>("Id", (obj_ as t同里镇开发区上市企业台帐台资).Id);
                    }
                    else if (t.Name.Contains("t同里镇开发区上市企业台帐主版后备"))
                    {
                        obj_ = EM_Wj.GetByPk<t同里镇开发区上市企业台帐主版后备>("Id", (obj_ as t同里镇开发区上市企业台帐主版后备).Id);
                    }
                    else if (t.Name.Contains("t吴江区智能制造示范试点企业名单"))
                    {
                        obj_ = EM_Wj.GetByPk<t吴江区智能制造示范试点企业名单>("Id", (obj_ as t吴江区智能制造示范试点企业名单).Id);
                    }
                    else if (t.Name.Contains("t新地标计划企业名单"))
                    {
                        obj_ = EM_Wj.GetByPk<t新地标计划企业名单>("Id", (obj_ as t新地标计划企业名单).Id);
                    }
                    else if (t.Name.Contains("t智能车间"))
                    {
                        obj_ = EM_Wj.GetByPk<t智能车间>("Id", (obj_ as t智能车间).Id);
                    }
                    else if (t.Name.Contains("bsUser"))
                    {
                        obj_ = QyTech_EM.GetByPk<bsUser>("bsU_Id", (obj_ as bsUser).bsU_Id);
                    }
                }
                foreach (bsFunField ff in funfields)
                {
                    try
                    {
                        if (ff.FEditType!=null && ff.VisibleInForm != false && "select,text".Contains(ff.FEditType))
                        {

                            //if (gbContainer.Controls[ff.FName].Text == "")
                            //    continue;

                            var property = obj_.GetType().GetProperty(ff.FName);
                            if (ff.OType == "string")
                                property.SetValue(obj_, gbContainer.Controls[ff.FName].Text);
                            else
                            {

                                if (gbContainer.Controls[ff.FName].Text != "")
                                {
                                    if (ff.OType == "decimal")
                                        property.SetValue(obj_, Convert.ToDecimal(gbContainer.Controls[ff.FName].Text));
                                    else if (ff.OType == "datetime")
                                        property.SetValue(obj_, Convert.ToDecimal(gbContainer.Controls[ff.FName].Text));
                                    else if (ff.OType == "int")
                                        property.SetValue(obj_, Convert.ToInt32(gbContainer.Controls[ff.FName].Text));
                                    //property.SetValue(obj_, (decimal)Convert.ChangeType(gbContainer.Controls[ff.FName].Text, property.PropertyType), null);
                                }
                                else
                                {
                                    property.SetValue(obj_, null);
                                }
                            }

                        }
                    }
                    catch (Exception ex) { //log.Error(ff.FName + ":" + ex.Message);
                    }
                }
                if (addoredit == AddOrEdit.Edit)
                {
                    if (t.Name.Contains("企业范围"))
                    {
                        ret = EM_Wj.Modify<企业范围>(obj_ as 企业范围);
                    }
                    else if (t.Name.Contains("房屋建筑"))
                    {
                        ret = EM_Wj.Modify<房屋建筑>(obj_ as 房屋建筑);

                    }
                    else if (t.Name.Contains("城市规划"))
                    {
                        ret = EM_Wj.Modify<城市规划>(obj_ as 城市规划);
                    }

                    else if (t.Name.Contains("t安全事故情况"))
                    {
                        ret = EM_Wj.Modify<t安全事故情况>(obj_ as t安全事故情况);
                    }
                    else if (t.Name.Contains("t标准化级别"))
                    {
                        ret = EM_Wj.Modify<t标准化级别>(obj_ as t标准化级别);
                    }
                    else if (t.Name.Contains("t高企"))
                    {
                        ret = EM_Wj.Modify<t高企>(obj_ as t高企);
                    }
                    else if (t.Name.Contains("t工程技术研究中心"))
                    {
                        ret = EM_Wj.Modify<t工程技术研究中心>(obj_ as t工程技术研究中心);
                    }
                    else if (t.Name.Contains("t工业固定资产"))
                    {
                        ret = EM_Wj.Modify<t工业固定资产>(obj_ as t工业固定资产);
                    }
                    else if (t.Name.Contains("t规上企业名单"))
                    {
                        ret = EM_Wj.Modify<t规上企业名单>(obj_ as t规上企业名单);
                    }
                    else if (t.Name.Contains("t经发局表格"))
                    {
                        ret = EM_Wj.Modify<t经发局表格>(obj_ as t经发局表格);
                    }
                    else if (t.Name.Contains("t立案处罚情况"))
                    {
                        ret = EM_Wj.Modify<t立案处罚情况>(obj_ as t立案处罚情况);
                    }
                    else if (t.Name.Contains("t企业基础数据"))
                    {
                        ret = EM_Wj.Modify<t企业基础数据>(obj_ as t企业基础数据);
                    }
                    else if (t.Name.Contains("t企业技术中心台账"))
                    {
                        ret = EM_Wj.Modify<t企业技术中心台账>(obj_ as t企业技术中心台账);
                    }
                    else if (t.Name.Contains("t清洁生产历年"))
                    {
                        ret = EM_Wj.Modify<t清洁生产历年>(obj_ as t清洁生产历年);
                    }
                    else if (t.Name.Contains("t市局表格"))
                    {
                        ret = EM_Wj.Modify<t市局表格>(obj_ as t市局表格);
                    }
                    else if (t.Name.Contains("t同里镇开发区上市企业台帐三板"))
                    {
                        ret = EM_Wj.Modify<t同里镇开发区上市企业台帐三板>(obj_ as t同里镇开发区上市企业台帐三板);
                    }
                    else if (t.Name.Contains("t同里镇开发区上市企业台帐台资"))
                    {
                        ret = EM_Wj.Modify<t同里镇开发区上市企业台帐台资>(obj_ as t同里镇开发区上市企业台帐台资);
                    }
                    else if (t.Name.Contains("t同里镇开发区上市企业台帐主版后备"))
                    {
                        ret = EM_Wj.Modify<t同里镇开发区上市企业台帐主版后备>(obj_ as t同里镇开发区上市企业台帐主版后备);
                    }
                    else if (t.Name.Contains("t吴江区智能制造示范试点企业名单"))
                    {
                        ret = EM_Wj.Modify<t吴江区智能制造示范试点企业名单>(obj_ as t吴江区智能制造示范试点企业名单);
                    }
                    else if (t.Name.Contains("t新地标计划企业名单"))
                    {
                        ret = EM_Wj.Modify<t新地标计划企业名单>(obj_ as t新地标计划企业名单);
                    }
                    else if (t.Name.Contains("t智能车间"))
                    {
                        ret = EM_Wj.Modify<t智能车间>(obj_ as t智能车间);
                    }
                    else if (t.Name.Contains("bsUser"))
                    {
                        ret = QyTech_EM.Modify<bsUser>(obj_ as bsUser);
                    }
                }
                else
                {
                    if (t.Name.Contains("企业范围"))
                    {
                        ret = EM_Wj.Add<企业范围>(obj_ as 企业范围);
                    }
                    else if (t.Name.Contains("房屋建筑"))
                    {
                        ret = EM_Wj.Add<房屋建筑>(obj_ as 房屋建筑);

                    }
                    else if (t.Name.Contains("城市规划"))
                    {
                        ret = EM_Wj.Add<城市规划>(obj_ as 城市规划);
                    }

                    else if (t.Name.Contains("t安全事故情况"))
                    {
                        ret = EM_Wj.Add<t安全事故情况>(obj_ as t安全事故情况);
                    }
                    else if (t.Name.Contains("t标准化级别"))
                    {
                        ret = EM_Wj.Add<t标准化级别>(obj_ as t标准化级别);
                    }
                    else if (t.Name.Contains("t高企"))
                    {
                        ret = EM_Wj.Add<t高企>(obj_ as t高企);
                    }
                    else if (t.Name.Contains("t工程技术研究中心"))
                    {
                        ret = EM_Wj.Add<t工程技术研究中心>(obj_ as t工程技术研究中心);
                    }
                    else if (t.Name.Contains("t工业固定资产"))
                    {
                        ret = EM_Wj.Add<t工业固定资产>(obj_ as t工业固定资产);
                    }
                    else if (t.Name.Contains("t规上企业名单"))
                    {
                        ret = EM_Wj.Add<t规上企业名单>(obj_ as t规上企业名单);
                    }
                    else if (t.Name.Contains("t经发局表格"))
                    {
                        ret = EM_Wj.Add<t经发局表格>(obj_ as t经发局表格);
                    }
                    else if (t.Name.Contains("t立案处罚情况"))
                    {
                        ret = EM_Wj.Add<t立案处罚情况>(obj_ as t立案处罚情况);
                    }
                    else if (t.Name.Contains("t企业基础数据"))
                    {
                        ret = EM_Wj.Add<t企业基础数据>(obj_ as t企业基础数据);
                    }
                    else if (t.Name.Contains("t企业技术中心台账"))
                    {
                        ret = EM_Wj.Add<t企业技术中心台账>(obj_ as t企业技术中心台账);
                    }
                    else if (t.Name.Contains("t清洁生产历年"))
                    {
                        ret = EM_Wj.Add<t清洁生产历年>(obj_ as t清洁生产历年);
                    }
                    else if (t.Name.Contains("t市局表格"))
                    {
                        ret = EM_Wj.Add<t市局表格>(obj_ as t市局表格);
                    }
                    else if (t.Name.Contains("t同里镇开发区上市企业台帐三板"))
                    {
                        ret = EM_Wj.Add<t同里镇开发区上市企业台帐三板>(obj_ as t同里镇开发区上市企业台帐三板);
                    }
                    else if (t.Name.Contains("t同里镇开发区上市企业台帐台资"))
                    {
                        ret = EM_Wj.Add<t同里镇开发区上市企业台帐台资>(obj_ as t同里镇开发区上市企业台帐台资);
                    }
                    else if (t.Name.Contains("t同里镇开发区上市企业台帐主版后备"))
                    {
                        ret = EM_Wj.Add<t同里镇开发区上市企业台帐主版后备>(obj_ as t同里镇开发区上市企业台帐主版后备);
                    }
                    else if (t.Name.Contains("t吴江区智能制造示范试点企业名单"))
                    {
                        ret = EM_Wj.Add<t吴江区智能制造示范试点企业名单>(obj_ as t吴江区智能制造示范试点企业名单);
                    }
                    else if (t.Name.Contains("t新地标计划企业名单"))
                    {
                        ret = EM_Wj.Add<t新地标计划企业名单>(obj_ as t新地标计划企业名单);
                    }
                    else if (t.Name.Contains("t智能车间"))
                    {
                        ret = EM_Wj.Add<t智能车间>(obj_ as t智能车间);
                    }
                    else if (t.Name.Contains("bsUser"))
                    {
                        ret = QyTech_EM.Add<bsUser>(obj_ as bsUser);
                    }
                }

                if (ret == "")
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
