using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using QyTech.Core;
using System.Windows.Forms;

using System.Data;


[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace QyTech.ExcelOper
{
    public enum NullValueHandling
    {
        Include = 0,
        Ignore = 1,
    }
    public delegate void DelegateForExportNo(int num,int maxvalue);  //声明了一个Delegate Type
 
     
    public class QyExcel
    {
        private static string DefaultTemplateExcelFile = "Report.xls";//System.Web.HttpContext.Current.Server.MapPath("~/DownLoads/Template/Report.xls")

        public event DelegateForExportNo ExportNoChanged;


        /// <summary>
        /// 通过datatable导出数据
        /// </summary>
        /// <param name="reportDate"></param>
        /// <param name="Settings"></param>
        /// <returns></returns>
        public string ExportWithTemplate(System.Data.DataTable reportDt, IQyExclSettings Settings)
        {
            Microsoft.Office.Interop.Excel.Application app = CopyTemplateAndOpenWorkSheet(Settings.ExServerPath, Settings.ExFileName);
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks[1];
            _Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)app.Worksheets[1];
            try
            {
                //再此处进行excl的填充
                int num = 1;
                #region 表头的绑定  设置了模板可不用这个 统一改模板
                //for (int i = 1; i <= Settings.PropertiesFilds.Count; i++)
                //{
                //    try
                //    {
                //        PropertyInfo p = typeof(T).GetProperty(Settings.PropertiesFilds[i - 1].ToString());
                //        if (!Settings.EliminateFilds.Contains(p.Name))
                //            worksheet.Cells[num, i] = typeof(T).GetProperty(Settings.PropertiesFilds[i - 1].ToString()).Name;
                //    }
                //    catch (Exception ex) { }
                //}
                #endregion
                num = Settings.RowStartValue;
                int RowNo = 0;
                foreach (DataRow dr in reportDt.Rows)
                {
                    RowNo++;
                    if (Settings.HaveNumberColumn)
                    {
                        worksheet.Cells[num, Settings.ColStartValue - 1] = RowNo;
                    }
                    for (int i = 1; i <= Settings.PropertiesFilds.Count; i++)
                    {
                        try
                        {
                            string fname = Settings.PropertiesFilds[i - 1].ToString();
                            if (reportDt.Columns.Contains(fname))
                            {
                                if (!Settings.EliminateFilds.Contains(fname))
                                {
                                    object o = dr[fname];
                                    worksheet.Cells[num, Settings.ColStartValue + i - 1] = o == null && Settings.NullValueHandling == NullValueHandling.Include ? "" :  o.ToString();
                                }
                            }
                          
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error(ex);
                        }

                    }
                    num++;
                    if (ExportNoChanged != null)
                        ExportNoChanged(RowNo, reportDt.Rows.Count);
                }
                workbook.Save();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            finally
            {
                CloseExcel(app, workbook);
            }

            return Settings.ExTempPath + Settings.ExFileName;
        }


        /// <summary>
        /// 通过datatable导出数据
        /// </summary>
        /// <param name="reportDate"></param>
        /// <param name="Settings"></param>
        /// <returns></returns>
        public string ExportWithoutTemplate(System.Data.DataTable reportDt, IQyExclSettings Settings)
        {
            Microsoft.Office.Interop.Excel.Application app = CopyTemplateAndOpenWorkSheet(Settings.ExServerPath, Settings.ExFileName);
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks[1];
            _Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)app.Worksheets[1];
            worksheet.Columns.AutoFit();
            try
            {
                //再此处进行excl的填充
                #region 表头的绑定  设置了模板可不用这个 统一改模板
                int num = 1;
                if (Settings.HaveNumberColumn)
                {
                    worksheet.Cells[num, 1] = "序号";
                }
                List<string> keys = Settings.DicFields.Keys.ToList<string>();
                for (int i = 0; i <= keys.Count; i++)
                {
                    try
                    {
                        //PropertyInfo p = typeof(T).GetProperty(Settings.PropertiesFilds[i - 1].ToString());
                        //if (!Settings.EliminateFilds.Contains(p.Name))
                        //    worksheet.Cells[num, i] = typeof(T).GetProperty(Settings.PropertiesFilds[i - 1].ToString()).Name;
                        if (Settings.HaveNumberColumn)
                        {
                            worksheet.Cells[num, i + 2] = Settings.DicFields[keys[i]];
                        }
                        else
                            worksheet.Cells[num, i + 1] = Settings.DicFields[keys[i]];
                    }
                    catch (Exception ex) { }
                }
                #endregion

                //num = Settings.RowStartValue;
                num = 2;
                int RowNo = 0;
                foreach (DataRow dr in reportDt.Rows)
                {
                    RowNo++;
                    if (Settings.HaveNumberColumn)
                    {
                        worksheet.Cells[num, Settings.ColStartValue - 1] = RowNo;
                    }
                    for (int i = 0; i < keys.Count; i++)
                    {
                        try
                        {
                            string fname = keys[i];
                            if (reportDt.Columns.Contains(fname))
                            {
                                if (!Settings.EliminateFilds.Contains(fname))
                                {
                                    object o = dr[fname];
                                    if (Settings.HaveNumberColumn)
                                    {
                                        //worksheet.Cells[num, i + 2] = Settings.DicFields[keys[i]];
                                        worksheet.Cells[num, i +2] = o == null && Settings.NullValueHandling == NullValueHandling.Include ? "" : o.ToString();
                                    }
                                    else
                                    {
                                        //worksheet.Cells[num, i + 1] = Settings.DicFields[keys[i]];
                                        worksheet.Cells[num, i +1] = o == null && Settings.NullValueHandling == NullValueHandling.Include ? "" : o.ToString();
                                    }
                                    }
                                }

                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error(ex);
                        }

                    }
                    num++;
                    if (ExportNoChanged != null)
                        ExportNoChanged(RowNo, reportDt.Rows.Count);
                }
                worksheet.Columns.AutoFit();
                workbook.Save();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            finally
            {
                CloseExcel(app, workbook);
            }

            return Settings.ExTempPath + Settings.ExFileName;
        }

        /// <summary>
        /// 导出Excel文件核心方法-简单填充文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reportDate">数据源</param>
        /// <param name="Settings">相关设置</param>
        /// <returns>文件路径</returns>
        public string Export<T>(List<T> reportDate, IQyExclSettings Settings)
        {
            Type type = typeof(T);
            Microsoft.Office.Interop.Excel.Application app = CopyTemplateAndOpenWorkSheet(Settings.ExServerPath , Settings.ExFileName);
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks[1];
            _Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)app.Worksheets[1];
            try
            {
                //再此处进行excl的填充
                int num = 1;
                #region 表头的绑定  设置了模板可不用这个 统一改模板
                //for (int i = 1; i <= Settings.PropertiesFilds.Count; i++)
                //{
                //    try
                //    {
                //        PropertyInfo p = typeof(T).GetProperty(Settings.PropertiesFilds[i - 1].ToString());
                //        if (!Settings.EliminateFilds.Contains(p.Name))
                //            worksheet.Cells[num, i] = typeof(T).GetProperty(Settings.PropertiesFilds[i - 1].ToString()).Name;
                //    }catch(Exception ex){}
                //}
                #endregion
                num = Settings.RowStartValue;
                int RowNo = 0;
                foreach (T t in reportDate)
                {
                    RowNo++;
                    if (Settings.HaveNumberColumn)
                    {
                        worksheet.Cells[num, Settings.ColStartValue - 1] = RowNo;
                    }
                    for (int i = 1; i <= Settings.PropertiesFilds.Count; i++)
                    {
                        try
                        {
                            PropertyInfo p = t.GetType().GetProperty(Settings.PropertiesFilds[i - 1].ToString());
                            if (!Settings.EliminateFilds.Contains(p.Name))
                            {
                                object o = p.GetValue(t);
                                Type pt = p.PropertyType;
                                worksheet.Cells[num, Settings.ColStartValue+i-1] = o == null && Settings.NullValueHandling == NullValueHandling.Include ? "" : pt.Name == "DateTime" ? DateTime.Parse(o.ToString()).ToString(Settings.FormatDT) : o.ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error(ex);
                        }

                    }
                    num++;
                    if (ExportNoChanged != null)
                        ExportNoChanged(RowNo, reportDate.Count);
                }
                workbook.Save();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            finally
            {
                CloseExcel(app, workbook);
            }

            return Settings.ExTempPath + Settings.ExFileName;
        }


        #region 注释代码
        ///// <summary>
        ///// 导出文件
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="reportDate"></param>
        ///// <param name="Settings"></param>
        ///// <param name="isreportNum"></param>
        ///// <param name="rowStartData"></param>
        ///// <param name="colStartData"></param>
        ///// <returns></returns>
        //public string Export<T>(List<T> reportDate, IQyExclSettings Settings,
        //    Boolean isreportNum = true, int rowStartData = 2, int colStartData = 2)
        //{
        //    string tempName= Settings.ExFileName + DateTime.Now.ToString("yyyyMMddHHmmssFFF") + ".xls";
        //    SaveFileDialog frm = new SaveFileDialog();
        //    frm.Filter = "Excel文件(*.xls,xlsx)|*.xls;*.xlsx";
        //    frm.FileName = Settings.ExFileName + DateTime.Now.ToString("yyyyMMddHHmmssFFF") + ".xls";
        //     if (frm.ShowDialog() == DialogResult.OK)
        //    {
        //        tempName = frm.FileName;
        //        try
        //        {
        //            LogHelper.Info("Export", "1");

        //            Microsoft.Office.Interop.Excel.Application app = CopyTemplateAndOpenWorkSheet(Settings.ExServerPath + Settings.ExFileName + ".xls", tempName);//Settings.ExTempPath + tempName);//Settings.ExFileName);
        //            LogHelper.Info("Export", "2");

        //            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks[1];
        //            LogHelper.Info("Export", "3");

        //            _Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)app.Worksheets[1];
        //            LogHelper.Info("Export", "4");


        //            try
        //            {

        //                //再此处进行excl的填充
        //                int Rowth = Settings.RowStartValue - 1;
        //                int RowNo = 1;
        //                #region 表头的绑定  设置了模板可不用这个 统一改模板
        //                if (Settings.PropertiesFildsName.Count > 0)
        //                {
        //                    for (int i = colStartData; i <= Settings.PropertiesFildsName.Count; i++)
        //                    {
        //                        try
        //                        {
        //                            worksheet.Cells[RowNo, i] = Settings.PropertiesFildsName[i - 1].ToString();
        //                        }
        //                        catch (Exception ex) { LogHelper.Error(ex); }
        //                    }


        //                }

        //                #endregion
        //                Rowth = rowStartData;
        //                foreach (T t in reportDate)
        //                {
        //                    if (Settings.HaveNumberColumn)
        //                    {
        //                        worksheet.Cells[Rowth, colStartData - 1] = RowNo++;
        //                        for (int i = 1; i <= Settings.PropertiesFilds.Count; i++)
        //                        {
        //                            try
        //                            {
        //                                PropertyInfo p = t.GetType().GetProperty(Settings.PropertiesFilds[i - 1].ToString());
        //                                if (!Settings.EliminateFilds.Contains(p.Name))
        //                                {
        //                                    object o = p.GetValue(t);
        //                                    Type pt = p.PropertyType;
        //                                    if (pt.ToString().Contains("Boolean"))
        //                                    {
        //                                        worksheet.Cells[Rowth, colStartData + i - 1] = o == null && Settings.NullValueHandling == NullValueHandling.Include ? "" : o.ToString() == "False" ? "否" : "是";

        //                                    }
        //                                    else
        //                                    {
        //                                        worksheet.Cells[Rowth, colStartData + i - 1] = o == null && Settings.NullValueHandling == NullValueHandling.Include ? "" : pt.Name == "DateTime" ? DateTime.Parse(o.ToString()).ToString(Settings.FormatDT) : o.ToString();
        //                                    }
        //                                }
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                LogHelper.Error(ex);
        //                            }
        //                        }
        //                        Rowth++;
        //                        if (ExportNoChanged!=null)
        //                            ExportNoChanged(Rowth);
        //                    }
        //                    else
        //                    {
        //                        for (int i = 1; i <= Settings.PropertiesFilds.Count; i++)
        //                        {
        //                            try
        //                            {
        //                                PropertyInfo p = t.GetType().GetProperty(Settings.PropertiesFilds[i - 1].ToString());
        //                                if (!Settings.EliminateFilds.Contains(p.Name))
        //                                {
        //                                    object o = p.GetValue(t);
        //                                    Type pt = p.PropertyType;
        //                                    if (pt.ToString().Contains("Boolean"))
        //                                    {
        //                                        worksheet.Cells[Rowth, colStartData + i - 1] = o == null && Settings.NullValueHandling == NullValueHandling.Include ? "" : o.ToString() == "False" ? "否" : "是";
        //                                    }
        //                                    else
        //                                    {
        //                                        worksheet.Cells[Rowth, colStartData + i - 1] = o == null && Settings.NullValueHandling == NullValueHandling.Include ? "" : pt.Name == "DateTime" ? DateTime.Parse(o.ToString()).ToString(Settings.FormatDT) : o.ToString();
        //                                    }
        //                                }
        //                            }
        //                            catch (Exception ex) { LogHelper.Error(ex); }
        //                        }
        //                        Rowth++;
        //                        if (ExportNoChanged != null)
        //                            ExportNoChanged(Rowth);
        //                    }
        //                }
        //                workbook.Save();
        //            }
        //            catch (Exception ex)
        //            {
        //                LogHelper.Error(ex);
        //            }
        //            finally
        //            {
        //                CloseExcel(app, workbook);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            LogHelper.Error(ex);
        //        }
        //    }
        //     return tempName;// Settings.ExTempPath + tempName;// Settings.ExFileName;
        //}
        #endregion

        /// <summary>
        /// 关闭Excel
        /// </summary>
        /// <param name="app"></param>
        /// <param name="workbook"></param>
        private void CloseExcel(Microsoft.Office.Interop.Excel.Application app, Microsoft.Office.Interop.Excel._Workbook workbook)
        {
            workbook.Close();
            if (workbook != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                workbook = null;
            }
            //关闭wordApp
            app.Quit();
            if (app != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                app = null;
            }
        }

        /// <summary>
        /// 复制模板文件
        /// </summary>
        /// <param name="strTemplatFile"></param>
        /// <param name="strTempFile"></param>
        /// <returns></returns>
        private  Microsoft.Office.Interop.Excel.Application CopyTemplateAndOpenWorkSheet(string strTemplatFile, string strTempFile)
        {
            try
            {
                LogHelper.Info("CopyTemplateAndOpenWorkSheet", "11");
                if (System.IO.File.Exists(strTempFile))
                {
                    System.IO.File.Delete(strTempFile);
                }
                if (System.IO.File.Exists(strTemplatFile))
                {

                    System.IO.File.Copy(strTemplatFile, strTempFile);
                }
                else
                {
                    //没找到模板使用默认模板
                    System.IO.File.Copy(DefaultTemplateExcelFile, strTempFile);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
            LogHelper.Info("CopyTemplateAndOpenWorkSheet", "21.5");
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            LogHelper.Info("CopyTemplateAndOpenWorkSheet", "22");
            app.Visible = false;
            LogHelper.Info("CopyTemplateAndOpenWorkSheet", "23");
            app.DisplayAlerts = false;
            LogHelper.Info("CopyTemplateAndOpenWorkSheet", "24");
            app.UserControl = true;
            LogHelper.Info("CopyTemplateAndOpenWorkSheet", "31");
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(strTempFile); //加载模板
            Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
            _Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)sheets.get_Item(1);
            LogHelper.Info("CopyTemplateAndOpenWorkSheet", "41");
            return app;


        }
    }
   

}