using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using System.Data;
using QyTech.Auth.Dao;
using System.Data.SqlClient;
using QyTech.SkinForm.Controls;

namespace QyTech.UICreate.Util
{
    public class qyUICreate
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">实体对象</param>
        /// <param name="ffs">实体的字段定义</param>
        /// <param name="gbParent">容器</param>
        /// <param name="gbwidth">容器宽度</param>
        /// <param name="gbheight">容器高度</param>
        public static void CreateFormEditPart(SqlConnection sqlconn, object obj, List<bsFunField> ffs, GroupBox gbParent, ref int gbwidth, ref int gbheight)
        {

            int InitLocationX = 0, InitLocationY = 10;//初始从相对于gbParnet的位置开始铺设

            int lblWidth = 90;
            int betweenWidth = 5;
            int EditWidhth = 150;
            int ColumnWidth = lblWidth + EditWidhth + betweenWidth;//每栏实际宽度，包括每栏中间的分割宽度

            int stepX = 260;//没列输入站的宽度
            stepX = ColumnWidth;
            int stepY = 40;//每行之间的像素距离

            int colCount = (int)Math.Ceiling(ffs.Count / 13.0);//每列最多十三行，进行列扩充
            int colIndex = 1;//用于当前控件所在列，默认从第1列开始，从1-ColCount


            int LocationX, LocationY;
            LocationX = InitLocationX;
            LocationY = InitLocationY;
            gbParent.Controls.Clear();

            if (ffs.Count > 0)
            {
                foreach (bsFunField ff in ffs)//bsFunField ff in ffs)
                {
                    try
                    {
                        if (ff.FEditType != null && ff.VisibleInForm != null && (bool)ff.VisibleInForm)// && "select,text,multext,checkbox".Contains(ff.FEditType))
                        {
                            string Fvalue;
                            if (obj is DataRow)
                                Fvalue = (obj as DataRow)[ff.FName].ToString();
                            else
                                Fvalue = QyTechReflection.GetObjectPropertyValue(obj, ff.FName);

                            if (ff.FEditType == "select")//combox
                            {
                                string[] items = new string[2];
                                if (ff.FEditValueEx != null && ff.FEditValueEx.Trim() != "")
                                    items = QyTech.DbUtils.SqlUtils.ParseSelectItemsFromFunField(sqlconn, ff.FEditValueEx, "系统配置");
                                else
                                {
                                    items[0] = ""; items[1] = "";
                                }

                                qyUICreate.CreateCombobox(gbParent, ff.FDesp, ff.FName, items[0], Fvalue, LocationX, LocationY, items[1]);
                            }
                            else if (ff.FEditType == "multext")
                            {
                                colIndex = 1;
                                if (LocationX != InitLocationX)
                                {
                                    LocationX = InitLocationX;
                                    LocationY += stepY;
                                }
                                //text
                                qyUICreate.CreateMultiTextDisplay(gbParent, ff.FDesp, ff.FName, Fvalue, LocationX, LocationY, lblWidth, ColumnWidth * (colCount - 1) + EditWidhth);//(int)ff.FWidthInForm);

                            }
                            else if (ff.FEditType == "checkbox")
                            {
                                qyUICreate.CreateCheckboxDisplay(gbParent, ff.FDesp, ff.FName, Fvalue, LocationX, LocationY, null, lblWidth, (int)ff.FWidthInForm);

                            }
                            else //if (ff.FEditType == "text")
                            {
                                //text
                                qyUICreate.CreateTextDisplay(gbParent, ff.FDesp, ff.FName, Fvalue, LocationX, LocationY);
                            }

                            #region 计算下一个控件参数的位置
                            colIndex = colIndex + 1 > colCount ? 1 : colIndex + 1;
                            if (ff.FEditType != "multext")//正常的向后进行
                            {
                                if (colIndex == 1)
                                {
                                    LocationX = InitLocationX;
                                    LocationY += stepY;
                                }
                                else
                                {
                                    LocationX += stepX;
                                }
                            }
                            else //遇到了多文本如何进行
                            {
                                colIndex = 1;
                                LocationX = InitLocationX;
                                LocationY += stepY;
                            }
                            #endregion
                        }

                    }
                    catch (Exception ex)
                    { }
                }
            }

            gbheight = LocationY;
            gbwidth = stepX * colCount + 5;//留点边界，看起来好看些

        }


        /// <summary>
        /// 创建界面的查询部分
        /// </summary>
        /// <param name="fqs"></param>
        /// <param name="gbParent"></param>
        /// <param name="gbwidth"></param>
        /// <param name="gbheight"></param>
        public static void CreateFunQueryPart(SqlConnection sqlconn, List<bsFunQuery> fqs, GroupBox gbParent, ref int gbwidth, ref int gbheight)
        {
            int InitLocationX = 0, InitLocationY =10;//初始从相对于gbParnet的位置开始铺设

            int stepX = 260;//每列输入站的宽度
            int stepY = 30;//每行之间的像素距离
            int num = -1;//移动的控件数
            int colCount = fqs.Count>=3?3:fqs.Count; //目前最大是3列，界面宽度是动态的，应该根据屏幕宽度进行计算
            int rowCount = 1;

            int lblWidth = 90;
            int EditWidhth = 150;
            int ColumnWidth = lblWidth + EditWidhth;

            int LocationX, LocationY;
            if (fqs.Count > 0)
            {
                rowCount = (int)Math.Ceiling(1.0*fqs.Count / colCount);//应该根据屏幕宽度进行计算，每行暂时3个条件

                LocationX = InitLocationX;
                LocationY = InitLocationY;

                foreach (bsFunQuery fq in fqs)//bsFunField ff in ffs)
                {

                    num++;
                    LocationX = InitLocationX + num % colCount * stepX;
                    LocationY = InitLocationY + num / colCount * stepY;
 
                    string Fvalue;
                    Fvalue = fq.DataEx;

                    if (fq.QueryType== "select")//combox
                    {
                        string[] items = QyTech.DbUtils.SqlUtils.ParseSelectItemsFromFunField(sqlconn,fq.DataEx, "");
                        qyUICreate.CreateCombobox(gbParent, fq.QueryName, fq.FName, items[0],"",LocationX, LocationY, fq.WhereSql);
                    }
                    else if (fq.QueryType == "checkbox")
                    {
                        qyUICreate.CreateCheckboxDisplay(gbParent, fq.QueryName, fq.FName, "", LocationX, LocationY, fq.WhereSql);

                    }
                    else //if (ff.FEditType == "text")
                    {
                        //text
                        qyUICreate.CreateTextDisplay(gbParent, fq.QueryName, fq.FName, "", LocationX, LocationY, fq.WhereSql);
                    }

                }
            }
            gbheight = gbheight+num/ colCount * stepY;
            if (colCount > 1)
                gbwidth = stepX * colCount;
            else
                gbwidth = stepX * colCount;

        }


        public static void CreateTextDisplay( GroupBox gbContainer,string labText, string FName,object FValue,int x, int y, object querytag = null, int labwidth = 90, int textwidth = 150, bool Enabled=true)
        {
            Label l = new Label();
            l.Name = "lbl_" + FName;
            l.Text = labText;
            l.Location = new System.Drawing.Point(x, y);//(300,...)
            l.Width = labwidth;
            l.TextAlign = ContentAlignment.MiddleRight;
            gbContainer.Controls.Add(l);

            TextBox tb = new TextBox();
            tb.Location = new System.Drawing.Point(x + 100, y);
            tb.Width = textwidth;
            tb.Name = FName;
            tb.Text = FValue.ToString();
            tb.Tag = querytag;
            gbContainer.Controls.Add(tb);
        }
        public static void CreateCheckboxDisplay(GroupBox gbContainer, string labText, string FName, object FValue, int x, int y, object querytag = null, int labwidth = 90, int textwidth = 150, bool Enabled = true)
        {
            Label l = new Label();
            l.Name = "lbl_" + FName;
            l.Text = labText;
            l.Location = new System.Drawing.Point(x, y);//(300,...)
            l.Width = labwidth;
            l.TextAlign = ContentAlignment.MiddleRight;
            gbContainer.Controls.Add(l);

            CheckBox tb = new CheckBox();
            tb.Location = new System.Drawing.Point(x + 100, y);
            tb.Width = textwidth;

            //tb.Text = labText;
            //tb.RightToLeft = RightToLeft.Yes;// = ContentAlignment.MiddleLeft;
            tb.Name = FName;
            tb.Checked = FValue.ToString() == "True" ? true : false;;
            tb.Tag = querytag;
            gbContainer.Controls.Add(tb);
        }
        

        public static void CreateMultiTextDisplay(GroupBox gbContainer, string labText, string FName, object FValue, int x, int y, int labwidth = 90, int textwidth = 150, bool Enabled = true)
        {
            Label l = new Label();
            l.Name = "lbl_" + FName;
            l.Text = labText;
            l.Location = new System.Drawing.Point(x, y);//(300,...)
            l.Width = labwidth;
            l.TextAlign = ContentAlignment.MiddleRight;
            gbContainer.Controls.Add(l);

            TextBox tb = new TextBox();
            tb.Location = new System.Drawing.Point(x + 100, y);
            tb.Width = textwidth;
            tb.Name = FName;
            tb.Text = FValue.ToString();
            tb.Multiline = true;
            //tb.Height = 40;
            gbContainer.Controls.Add(tb);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gbContainer"></param>
        /// <param name="labText"></param>
        /// <param name="FName"></param>
        /// <param name="items">逗号分隔的字符串，每项用来显示</param>
        /// <param name="FValue"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="itemstag">逗号分隔的字符串，每个表示一项id，对应Items的显示</param>
        /// <param name="labwidth"></param>
        /// <param name="comboxwidth"></param>
        /// <returns></returns>
        public static ComboBox CreateCombobox(GroupBox gbContainer, string labText, string FName, string items, object FValue, int x, int y, object itemstag=null, int labwidth = 90, int comboxwidth = 150, bool Enabled = true)
        {
            Label l = new Label();
            l.Text = labText;
            l.Location = new System.Drawing.Point(x, y);
            l.Width = labwidth;
            l.TextAlign = ContentAlignment.MiddleRight;
            gbContainer.Controls.Add(l);

            qyComboBox cb = new qyComboBox();
            cb.Location = new System.Drawing.Point(x + 100, y);
            cb.Width = comboxwidth;
            cb.Name = FName;
            if (itemstag!=null)
                cb.Tag = itemstag.ToString();
            if (items.Trim() != "")
            {
                string[] sitems = items.Split(new char[] { ',' ,';'});
                cb.Items.Clear();
                //cb.Items.Add("");
                foreach (string s in sitems)
                {
                    cb.Items.Add(s);
                }
                if (cb.Tag != null)
                {
                    int index = 0;
                    string[] tagitems = itemstag.ToString().Split(new char[] { ',', ';' });
                    for (int i = 0; i < tagitems.Length; i++)
                        if (tagitems[i] == FValue.ToString())
                        { index = i; break; }
                    cb.SelectedIndex = index;
                }
            }
            cb.Text = FValue.ToString();

            gbContainer.Controls.Add(cb);
            return cb;
        }
    }
}
