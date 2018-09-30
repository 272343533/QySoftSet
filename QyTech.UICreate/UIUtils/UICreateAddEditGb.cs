using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QyTech.Auth.Dao;



namespace QyTech.SkinForm.UICreate
{
    public class UICreateAddEditGb
    {
                                                 //int initWidth = 5 + 245 + 245 + 5 + 10;
                                                 //int InitHeight = 110;
        

        private void CreateFormEditPart(object obj,List<bsFunField> ffs, GroupBox gbParent, ref int gbwidth, ref int gbheight)
        {
            int InitLocationX = 0, InitLocationY = 5;//初始从相对于gbParnet的位置开始铺设

            int stepX = 250;//没列输入站的宽度
            int stepY = 40;//每行之间的像素距离
            int num = -1;//移动的控件数
            int colCount = 1;


            int lblWidth = 90;
            int betweenWidth = 5;
            int EditWidhth = 150;
            int ColumnWidth = lblWidth + betweenWidth + EditWidhth;

            int LocationX, LocationY;
            if (ffs.Count > 0)
            {


                colCount = (int)Math.Ceiling(ffs.Count / 13.0);//每列最多十三行，进行列扩充

                LocationX = InitLocationX;
                LocationY = InitLocationY;

                foreach (bsFunField ff in ffs)
                {
                    if (ff.FName == "ID")
                        continue;
                    if (ff.FEditType != null && "select,text,multext".Contains(ff.FEditType))
                    {
                        num++;
                        LocationX = InitLocationX + num % colCount * stepX;
                        LocationY = InitLocationY + num / colCount * stepY;
                        //LocationX +=  num % colCount * stepX;
                        //LocationY +=  num / colCount * stepY;

                        string cname = ff.FDesp;


                        string Fvalue = QyTechReflection.GetObjectPropertyValue(obj_, ff.FName);

                        if (ff.FEditType == "select")//combox
                        {
                            UICreate.CreateCombobox(gbParent, ff.FDesp, ff.FName, ff.FEditValueEx, Fvalue, LocationX, LocationY);
                        }
                        else if (ff.FEditType == "text")
                        {
                            //text
                            UICreate.CreateTextDisplay(gbParent, ff.FDesp, ff.FName, Fvalue, LocationX, LocationY);
                        }
                        else if (ff.FEditType == "multext")
                        {
                            if (LocationX != InitLocationX)
                            {
                                LocationX = InitLocationX;
                                LocationY += stepY;
                            }
                            //text
                            UICreate.CreateMultiTextDisplay(gbParent, ff.FDesp, ff.FName, Fvalue, LocationX, LocationY, lblWidth, (int)ff.FWidthInForm);

                        }

                    }

                }
            }
            gbheight = num / colCount * stepY;
            gbwidth = ColumnWidth * colCount;

        }
    }
}
