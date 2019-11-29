using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using QyTech.Core;
using System.IO;
using System.Data;
using System.Text;


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

        //add on 2019-05-25

        /// <summary>
        /// OLEDB方式导出DataTable
        /// </summary>
        /// <param name="reportDt"></param>
        /// <param name="Settings"></param>
        /// <returns></returns>
        public string ExportWithTemplateUsingAce(System.Data.DataTable reportDt, IQyExclSettings Settings)
        {
            CopyTemplate(Settings.ExServerPath, Settings.ExFileName);

            string strCon = string.Empty;
            FileInfo file = new FileInfo(Settings.ExFileName);
            string extension = file.Extension;
            switch (extension)
            {
                case ".xls":
                    strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Settings.ExFileName + ";Extended Properties=Excel 8.0;";
                    //strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=0;'";
                    //strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Path + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=2;'";
                    break;
                case ".xlsx":
                    //strCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path + ";Extended Properties=Excel 12.0;";
                    //strCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=2;'";    //出现错误了
                    strCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Settings.ExFileName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=0;'";
                    break;
                default:
                    strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Settings.ExFileName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=0;'";
                    break;
            }
            try
            {
                using (System.Data.OleDb.OleDbConnection con = new System.Data.OleDb.OleDbConnection(strCon))
                {
                    con.Open();
                    StringBuilder strSQL = new StringBuilder();
                    System.Data.OleDb.OleDbCommand cmd;

                 
                    //添加数据
                    for (int i = 0; i < reportDt.Rows.Count; i++)
                    {
                        strSQL.Clear();
                        StringBuilder strvalue = new StringBuilder();
                        for (int j = 0; j < reportDt.Columns.Count; j++)
                        {
                            strvalue.Append("'" + reportDt.Rows[i][j].ToString() + "'");
                            if (j != reportDt.Columns.Count - 1)
                            {
                                strvalue.Append(",");
                            }
                            else
                            {
                            }
                        }
                        strSQL.Append(" update [" + reportDt.TableName + "] values (").Append(strvalue).Append(")").ToString();
                        cmd = new System.Data.OleDb.OleDbCommand(strSQL.ToString(), con);    //覆盖文件时可能会出现Table 'Sheet1' already exists.所以这里先删除了一下
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch { }
            return Settings.ExTempPath + Settings.ExFileName;

        }
        //add end

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
                                //if (!Settings.EliminateFilds.Contains(fname))
                                if (!(Settings.EliminateFilds + ",").Contains(fname + ","))
                                {
                                    object o = dr[fname];
                                    worksheet.Cells[num, Settings.ColStartValue + i - 1] = o == null && Settings.NullValueHandling == NullValueHandling.Include ? "" : o.ToString();
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
                                //if (!Settings.EliminateFilds.Contains(fname))
                                if (!(Settings.EliminateFilds + ",").Contains(fname + ","))
                                {
                                    object o = dr[fname];
                                    if (Settings.HaveNumberColumn)
                                    {
                                        //worksheet.Cells[num, i + 2] = Settings.DicFields[keys[i]];
                                        worksheet.Cells[num, i + 2] = o == null && Settings.NullValueHandling == NullValueHandling.Include ? "" : o.ToString();
                                    }
                                    else
                                    {
                                        //worksheet.Cells[num, i + 1] = Settings.DicFields[keys[i]];
                                        worksheet.Cells[num, i + 1] = o == null && Settings.NullValueHandling == NullValueHandling.Include ? "" : o.ToString();
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

        public string ExportListWithTemplate<T>(List<T> reportDate, IQyExclSettings Settings)
        {
            Type type = typeof(T);
            Microsoft.Office.Interop.Excel.Application app = CopyTemplateAndOpenWorkSheet(Settings.ExTemplateFileName, Settings.ExFileName);
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks[1];
            _Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)app.Worksheets[1];
            try
            {
                int RowNo = 0;
                int Rowth = Settings.RowStartValue;
                foreach (T t in reportDate)
                {
                    RowNo++;
                    if (Settings.HaveNumberColumn)
                    {
                        worksheet.Cells[Rowth, Settings.ColStartValue - 1] = RowNo;
                    }
                    for (int i = 0; i < Settings.PropertiesFilds.Count; i++)
                    {
                        try
                        {
                            PropertyInfo p = t.GetType().GetProperty(Settings.PropertiesFilds[i].ToString());
                            if (!(Settings.EliminateFilds + ",").Contains(p.Name + ","))
                            {
                                object o = p.GetValue(t);
                                Type pt = p.PropertyType;
                                worksheet.Cells[Rowth, Settings.ColStartValue + i] = o == null && Settings.NullValueHandling == NullValueHandling.Include ? "" : pt.Name == "DateTime" ? DateTime.Parse(o.ToString()).ToString(Settings.FormatDT) : o.ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error(ex);
                        }

                    }
                    Rowth++;
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
            return Settings.ExFileName;
        }


        /// <summary>
        /// 导出Excel文件核心方法-简单填充文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reportDate">数据源</param>
        /// <param name="Settings">相关设置</param>
        /// <returns>文件路径</returns>
        public string ExportListWithoutTemplate<T>(List<T> reportDate, IQyExclSettings Settings)
        {
            Type type = typeof(T);
            Microsoft.Office.Interop.Excel.Application app = CopyTemplateAndOpenWorkSheet(Settings.ExTemplateFileName, Settings.ExFileName);
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks[1];
            _Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)app.Worksheets[1];
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
                            // if (!Settings.EliminateFilds.Contains(p.Name))
                            if (!(Settings.EliminateFilds + ",").Contains(p.Name + ","))
                            {
                                object o = p.GetValue(t);
                                Type pt = p.PropertyType;
                                worksheet.Cells[num, Settings.ColStartValue + i - 1] = o == null && Settings.NullValueHandling == NullValueHandling.Include ? "" : pt.Name == "DateTime" ? DateTime.Parse(o.ToString()).ToString(Settings.FormatDT) : o.ToString();
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

            return Settings.ExFileName;
        }


        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="Settings">把需要复制的数据放到DicPosValues中</param>
        /// <returns></returns>
        public string ExportDiy(IQyExclSettings Settings)
        {
            Microsoft.Office.Interop.Excel.Application app = CopyTemplateAndOpenWorkSheet(Settings.ExTemplateFileName, Settings.ExFileName);
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks[1];
            _Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)app.Worksheets[1];
            try
            {
                int rowindex, colindex;
                foreach (string key in Settings.DicPosValues.Keys)
                {
                    try
                    {
                        string[] rowcol = key.Split(new char[] { ',' });
                        colindex = Convert.ToInt16(rowcol[0]);
                        rowindex = Convert.ToInt16(rowcol[1]);

                        worksheet.Cells[rowindex, colindex] = Settings.DicPosValues[key];
                        Range range = worksheet.Cells[rowindex, colindex];
                        //range.Characters[2, 2].Font.Underline = true;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error(ex);
                    }
                }
                //但对象数据不多，不需要进度控制
                //if (ExportNoChanged != null)
                //    ExportNoChanged(RowNo, reportDate.Count);
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
            return Settings.ExFileName;
        }


        /// <summary>
        /// 新增行，并填充数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="exData"></param>
        /// <param name="fieldNames"></param>
        /// <param name="valueFillcols"></param>
        /// <param name="RowWirteStart"></param>
        /// <param name="rangeForCopy"></param>
        /// <param name="FormatDT"></param>
        /// <param name="localOrWeb"></param>
        public void Export2ExcelWithInsertRow<T>(string fileName, List<T> exData, string fieldNames, string valueFillcols,
          int RowWirteStart, string rangeForCopy, string FormatDT = "yyyy-MM-dd HH:mm:ss", string localOrWeb = "local")
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = false;
            app.DisplayAlerts = false;
            app.UserControl = true;
            Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
            Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(fileName); //加载模板

            _Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)app.Worksheets[1];
            try
            {
                //首先插入行
                for (int i = 1; i < exData.Count; i++)
                {
                    //1.插入行没问题，但是占用了下面表的格式，下面
                    // worksheet.Range["A24", "L24"].Insert(Shift: (XlDirection.xlDown));
                    //2.这个直接插入，不会保留原来各列的合并设置关系
                    //Microsoft.Office.Interop.Excel.Range range = (Range)worksheet.Rows[RowWirteStart+1];
                    //range.Insert(XlInsertShiftDirection.xlShiftDown);
                    //3.试试整行复制,OK
                    Range range = (Range)worksheet.Rows["24:24", System.Type.Missing];
                    range.Select();
                    range.Copy();
                    range.Insert(XlInsertShiftDirection.xlShiftDown);
                }
                int rowIndex = -1, colIndex = -1;
                string[] fillcols = valueFillcols.Split(new char[] { ',' });
                if (exData.Count == 0)
                {
                    foreach (string field in fieldNames.Split(new char[] { ',' }))
                    {
                        colIndex++;
                        try
                        {
                            worksheet.Cells[RowWirteStart + rowIndex, Convert.ToInt32(fillcols[colIndex])] = "无";
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error(ex);
                        }
                    }
                }
                else
                {
                    foreach (T t in exData)
                    {
                        rowIndex++;
                        colIndex = -1;
                        foreach (string field in fieldNames.Split(new char[] { ',' }))
                        {
                            colIndex++;
                            try
                            {
                                PropertyInfo p = t.GetType().GetProperty(field);
                                object o = p.GetValue(t);
                                Type pt = p.PropertyType;
                                worksheet.Cells[RowWirteStart + rowIndex, Convert.ToInt32(fillcols[colIndex])] = o == null ? "" : pt.Name == "DateTime" ? DateTime.Parse(o.ToString()).ToString(FormatDT) : o.ToString();
                            }
                            catch (Exception ex)
                            {
                                LogHelper.Error(ex);
                            }
                        }
                    }
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
        private Microsoft.Office.Interop.Excel.Application CopyTemplateAndOpenWorkSheet(string strTemplatFile, string strTempFile)
        {
            try
            {
                CopyTemplate(strTemplatFile, strTempFile);

                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

                app.Visible = false;

                app.DisplayAlerts = false;

                app.UserControl = true;

                Microsoft.Office.Interop.Excel.Workbooks workbooks = app.Workbooks;
                Microsoft.Office.Interop.Excel._Workbook workbook = workbooks.Open(strTempFile); //加载模板
                Microsoft.Office.Interop.Excel.Sheets sheets = workbook.Sheets;
                _Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)sheets.get_Item(1);
                return app;

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }
        private void CopyTemplate(string strTemplatFile, string strTempFile)
        {
            try
            {
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
        }

    }

}