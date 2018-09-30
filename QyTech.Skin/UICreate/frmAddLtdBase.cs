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
using QyTech.SkinForm;
using QyTech.SkinForm.UICreate;

namespace TDObject.UI
{
    public partial class frmAddLtdBase : qyFormWithTitle
    {
        string addormodify = "add";
        List<QyTech.Auth.Dao.bsFunField> funfields;
        bsFunConf bsFc;
        t企业基础数据 obj_;
        string objtype_;


        public static EntityManager QyTech_EM = new EntityManager(new QyTech.Auth.Dao.QyTech_AuthEntities());
        public static EntityManager EM_Wj;// = new EntityManager(new wj_GisDbEntities());

        int initWidth = 5 + 265 + 265 + 5 + 10;
        int InitHeight = 110;

        public frmAddLtdBase(string objtype, object obj, EntityManager EM_app)
        {
            InitializeComponent();
            EM_Wj = EM_app;
            objtype_ = objtype;
            //bsFc=MainForm.QyTech_EM.getbyp
            if (obj != null)
            {
                addormodify = "modify";
                obj_ = obj as t企业基础数据;
            }
            else
            {
                obj_ = new t企业基础数据();
            }

            this.Title = "信息编辑";
            
       }
        private void frmAdd_Load(object sender, EventArgs e)
        {
           this.gbBase.Location = new Point(0, this.gbBase.Location.Y);
            this.gbTotal.Location = new Point(initWidth, this.gbBase.Location.Y);

            InitFrom();


            // this.btnClose.Image = qyResources.exit_16;
            this.btnSave.Image = qyResources.save_16;


          
            this.StartPosition = FormStartPosition.CenterScreen;


        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void InitFrom()
        {
            int leftheight, rightH;

            bsTable dbtable = QyTech_EM.GetBySql<bsTable>("TName='"+ objtype_+ "' and bsD_Name='wj_GisDb'");
            if (dbtable != null)
                lblTitle.Text = dbtable.Desp;
            else
                lblTitle.Text = objtype_;

            this.Width = initWidth;

            int lwidth = 0, lheight = 0, rwidth = 0, rheight = 0;
            funfields = QyTech_EM.GetListNoPaging<bsFunField>("TName='"+ objtype_ + "' and FNo<210", "NoInForm");
            UICreate.CreateFormEditPart(obj_, funfields, gbBase, ref lwidth, ref lheight);
            //CreateFormEditPart(gbBase, funfields,ref lwidth,ref lheight);

            funfields = QyTech_EM.GetListNoPaging<bsFunField>("TName='" + objtype_ + "' and FNo>=210", "NoInForm");
            UICreate.CreateFormEditPart(obj_, funfields, gbTotal, ref rwidth, ref rheight);

            //CreateFormEditPart(gbTotal, funfields, ref rwidth, ref rheight);


            Height = InitHeight+ (lheight > rheight ? lheight : rheight) +30;

            
            if (("System,委领导,统计站,吴江经济技术开发区".Contains(CurrLogUser.bsO_Name)))
            {
                this.btnExtent.Visible = true;
            }
            else
                this.btnExtent.Visible = false;
            this.lblTitle.Location = new Point(this.Width / 2 - System.Text.Encoding.Default.GetBytes(this.lblTitle.Text).Length * 10 / 2, lblTitle.Location.Y);

            //this.btnSave.Location = new Point(this.Width / 2 - this.btnSave.Width / 2, this.Height-btnSave.Height-10);


            #region AAA
            //if (funfields.Count > 0)
            //{
            //    int num = -1;
            //    int LocationX, LocationY;

            //    int colCount;

            //    if ("System,委领导".Contains(MainForm.LoginUser.bsO_Name))
            //        colCount= (int)Math.Ceiling(funfields.Count / 13.0);//每列最多十三行，进行列扩充
            //    else
            //        colCount = (int)Math.Ceiling(19 / 13.0);//每列最多十三行，进行列扩充
            //    this.Width += 250 * (colCount - 1);




            //    LocationX = InitLocationX;
            //    LocationY = InitLocationY;

            //    foreach (bsFunField ff in funfields)
            //    {
            //        if (!("System,委领导".Contains(MainForm.LoginUser.bsO_Name)))
            //        {
            //            if (ff.FNo >= 210)
            //                break;
            //        }
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
            //                UICreate.CreateCombobox(gbBase, ff.FDesp, ff.FName, ff.FEditValueEx, Fvalue, LocationX, LocationY);
            //            }
            //            else if (ff.FEditType == "text")
            //            {
            //                //text
            //                UICreate.CreateTextDisplay(gbBase, ff.FDesp, ff.FName, Fvalue, LocationX, LocationY);
            //            }
            //            else if (ff.FEditType == "multext")
            //            {
            //                if (LocationX != InitLocationX)
            //                {
            //                    LocationX = InitLocationX;
            //                    LocationY += stepY;
            //                }
            //                //text
            //                UICreate.CreateMultiTextDisplay(gbBase, ff.FDesp, ff.FName, Fvalue, LocationX, LocationY, 90, gbBase.Width - 140);

            //            }

            //        }

            //        //}


            //        //this.btnSave.Location = new Point(this.Width / 2 - this.btnSave.Width - 100, btnSave.Location.Y);
            //        //this.btnClose.Location = new Point(this.Width / 2 + 100, btnClose.Location.Y);
            //    }
            #endregion
        }

     
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Type t = obj_.GetType();
                string ret = "";

                GroupBox gbContainer=gbBase;
                obj_ = EM_Wj.GetByPk<t企业基础数据>("Id",(obj_ as t企业基础数据).Id);

                funfields = QyTech_EM.GetListNoPaging<bsFunField>("TName='" + objtype_ + "'", "NoInForm");

                foreach (bsFunField ff in funfields)
                {
                    try
                    {
                        if (ff.FNo < 210) { gbContainer = gbBase; }
                        else { gbContainer = gbTotal; }
                        //if (ff.FEditType != null && ff.VisibleInForm != false && "select,text".Contains(ff.FEditType))
                          if (ff.FEditType != null && "select,text".Contains(ff.FEditType))
                            {

                            if (ff.FName == "增加值")
                                Console.WriteLine(ff.FName);
                            if (gbContainer.Controls[ff.FName].Text.Trim() != "")
                            {
                                var property = obj_.GetType().GetProperty(ff.FName);
                                if (ff.OType == "string")
                                    property.SetValue(obj_, gbContainer.Controls[ff.FName].Text);
                                else if (ff.OType == "decimal")
                                    property.SetValue(obj_, Convert.ToDecimal(gbContainer.Controls[ff.FName].Text));
                                else if (ff.OType == "float")
                                    property.SetValue(obj_, Convert.ToSingle(gbContainer.Controls[ff.FName].Text));
                                else if (ff.OType == "double")
                                    property.SetValue(obj_, Convert.ToDouble(gbContainer.Controls[ff.FName].Text));
                                else if (ff.OType == "datetime")
                                    property.SetValue(obj_, Convert.ToDecimal(gbContainer.Controls[ff.FName].Text));
                                else if (ff.OType == "int")
                                    property.SetValue(obj_, Convert.ToInt32(gbContainer.Controls[ff.FName].Text));
                                //property.SetValue(obj_, (decimal)Convert.ChangeType(gbContainer.Controls[ff.FName].Text, property.PropertyType), null);

                            }
                        }
                    }
                    catch (Exception ex) { log.Error(ff.FName + ":" + ex.Message); }
                }
               ret= EM_Wj.Modify<t企业基础数据>(obj_ as t企业基础数据);
               

                //string detailUrl = "";// MainForm.App_URI + "lyRemoteServ/UpdGisObj?TType=" + TType + "&json=" + json;

                //string ret = AsyncHttp.CommFun.GetRemoteJson(detailUrl);

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

      

        private void btnExtent_Click(object sender, EventArgs e)
        {
           if (this.Width== initWidth) {
                this.Width += 265 * 2 + 10;
            }
            else
            {
                this.Width -= 265 * 2 + 10;

            }
        }

       
    }
}
