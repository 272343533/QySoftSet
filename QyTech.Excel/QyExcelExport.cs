using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;



namespace QyTech.ExcelOper
{
    public partial class QyExcelHelper
    {

        IQyExclSettings exportSetting;

        public event DelegateForExportNo eventNumChanged;

        private QyExcel QyExcel_ = new QyExcel();

        public QyExcelHelper(string LocalOrWeb)
        {
            if (LocalOrWeb.Substring(0, 3).ToLower() == "loc")
                exportSetting = new QyExclSettingsLocal();
            else
                exportSetting = new QyExclSettingsWeb();

            QyExcel_.ExportNoChanged += new DelegateForExportNo(QyExcel__ExportNoChanged);
        }

        private void QyExcel__ExportNoChanged(int num, int percent)
        {
            if (eventNumChanged != null)
                eventNumChanged(num, percent);
        }

        /// <summary>
        /// 通过datatable导出，需要模板
        /// </summary>
        /// <param name="exDataTable"></param>
        /// <param name="exFileName"></param>
        /// <param name="propertiesFilds"></param>
        /// <param name="HaveNoColumn"></param>
        /// <param name="RowStart"></param>
        /// <param name="ColStart"></param>
        /// <param name="localOrWeb"></param>
        /// <returns></returns>
        public string ExportDataTableToExclWithModel(System.Data.DataTable exDataTable, string exFileName, string propertiesFilds,
        bool HaveNoColumn = false, int RowStart = 2, int ColStart = 1, string localOrWeb = "local")
        {

            int i = 1;
            //传进来的列表都放入  错误的列不进行处理  内部实现
            List<string> listStr = new List<string>();
            string[] strs = propertiesFilds.Split(',');
            foreach (string s in strs)
            {
                if (s != "")
                {
                    listStr.Add(s);
                }
            }
            IQyExclSettings exportSetting;
            if (localOrWeb.ToLower() == "local")
                exportSetting = new QyExclSettingsLocal();
            else
                exportSetting = new QyExclSettingsWeb();

            exportSetting.ExFileName = exFileName;
            exportSetting.PropertiesFilds = listStr;
            exportSetting.NullValueHandling = NullValueHandling.Include;
            exportSetting.FormatDT = "yyyy - MM - dd HH: mm: ss";
            exportSetting.ColStartValue = ColStart;
            exportSetting.RowStartValue = RowStart;
            exportSetting.HaveNumberColumn = HaveNoColumn;
            exportSetting.ExServerPath += exDataTable.TableName + ".xls";// typeof(T).Name.ToString() + ".xls";


            string filePath = QyExcel_.ExportWithTemplate(exDataTable, exportSetting);
            return filePath;
        }


        /// <summary>
        /// datatable导出，不需要模板
        /// </summary>
        /// <param name="exDataTable"></param>
        /// <param name="exFileName"></param>
        /// <param name="propertiesFields"></param>
        /// <param name=""></param>
        /// <param name="HaveNoColumn"></param>
        /// <param name="RowStart"></param>
        /// <param name="ColStart"></param>
        /// <param name="localOrWeb"></param>
        /// <returns></returns>
        public string ExportDataTableToExcl(System.Data.DataTable exDataTable, string exFileName, Dictionary<string, string> propertiesFields,
       bool HaveNoColumn = false, int RowStart = 2, int ColStart = 1, string localOrWeb = "local")
        {
            //传进来的列表都放入  错误的列不进行处理  内部实现
            IQyExclSettings exportSetting;
            if (localOrWeb.ToLower() == "local")
                exportSetting = new QyExclSettingsLocal();
            else
                exportSetting = new QyExclSettingsWeb();

            exportSetting.ExFileName = exFileName;
            exportSetting.DicFields = propertiesFields;
            exportSetting.NullValueHandling = NullValueHandling.Include;
            exportSetting.FormatDT = "yyyy - MM - dd HH: mm: ss";
            exportSetting.ColStartValue = ColStart;
            exportSetting.RowStartValue = RowStart;
            exportSetting.HaveNumberColumn = HaveNoColumn;
            exportSetting.ExServerPath += exDataTable.TableName + ".xls";// typeof(T).Name.ToString() + ".xls";

            string filePath = QyExcel_.ExportWithoutTemplate(exDataTable, exportSetting);

            return filePath;
        }



        /// <summary>
        /// 无模板导出，列头在参数propertiesFields中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exData"></param>
        /// <param name="exFileName"></param>
        /// <param name="templateFileName"></param>
        /// <param name="propertiesFields"></param>
        /// <param name="FormatDT"></param>
        /// <param name="HaveNoColumn"></param>
        /// <param name="RowStart"></param>
        /// <param name="ColStart"></param>
        /// <param name="localOrWeb"></param>
        /// <returns></returns>
        public string ExportListToExcl<T>(List<T> exData, string exFileName, string templateFileName, Dictionary<string, string> propertiesFields,
          string FormatDT = "yyyy-MM-dd HH:mm:ss", bool HaveNoColumn = true, int RowStart = 2, int ColStart = 2, string localOrWeb = "local")
        {
            //传进来的列表都放入  错误的列不进行处理  内部实现
            IQyExclSettings exportSetting;
            if (localOrWeb.ToLower() == "local")
                exportSetting = new QyExclSettingsLocal();
            else
                exportSetting = new QyExclSettingsWeb();

            exportSetting.ExFileName = exFileName;
            exportSetting.DicFields = propertiesFields;
            exportSetting.NullValueHandling = NullValueHandling.Include;
            exportSetting.FormatDT = "yyyy - MM - dd HH: mm: ss";
            exportSetting.ColStartValue = ColStart;
            exportSetting.RowStartValue = RowStart;
            exportSetting.HaveNumberColumn = HaveNoColumn;
            exportSetting.ExServerPath += templateFileName + ".xls";// typeof(T).Name.ToString() + ".xls";

            string filePath = QyExcel_.ExportListWithoutTemplate(exData, exportSetting);

            return filePath;

        }

        /// <summary>
        /// 通过List<object>导出，需要模板，字段按顺序对应相应列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exDate"></param>
        /// <param name="exFileName"></param>
        /// <param name="propertiesFilds"></param>
        /// <param name="FormatDT"></param>
        /// <param name="HaveNoColumn"></param>
        /// <param name="RowStart"></param>
        /// <param name="ColStart"></param>
        /// <param name="localOrWeb"></param>
        /// <returns></returns>
        public string ExportListToExcl<T>(List<T> exDate, string exFileName, string templateFileName, string propertiesFilds,
          string FormatDT = "yyyy-MM-dd HH:mm:ss", bool HaveNoColumn = true, int RowStart = 2, int ColStart = 2, string localOrWeb = "local")
        {

            int i = 1;
            //传进来的列表都放入  错误的列不进行处理  内部实现
            List<string> listStr = new List<string>();
            string[] strs = propertiesFilds.Split(',');
            foreach (string s in strs)
            {
                if (s != "")
                {
                    listStr.Add(s);
                }
            }
            IQyExclSettings exportSetting;
            if (localOrWeb.ToLower() == "local")
                exportSetting = new QyExclSettingsLocal();
            else
                exportSetting = new QyExclSettingsWeb();

            exportSetting.ExTemplateFileName += templateFileName;
            exportSetting.ExFileName = exFileName;
            exportSetting.PropertiesFilds = listStr;
            exportSetting.NullValueHandling = NullValueHandling.Include;
            exportSetting.FormatDT = FormatDT;
            exportSetting.ColStartValue = ColStart;
            exportSetting.RowStartValue = RowStart;
            exportSetting.HaveNumberColumn = HaveNoColumn;

            string filePath = QyExcel_.ExportListWithTemplate<T>(exDate, exportSetting);
            return filePath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exFileName"></param>
        /// <param name="keyValues">位置与对应位置的值</param>
        /// <param name="localOrWeb"></param>
        /// <returns></returns>
        public string ExportDiyToExcl(string exFileName, string templateFileName, Dictionary<string, string> keyValues, string localOrWeb = "local")
        {
            IQyExclSettings exportSetting;
            if (localOrWeb.ToLower() == "local")
                exportSetting = new QyExclSettingsLocal();
            else
                exportSetting = new QyExclSettingsWeb();

            exportSetting.ExTemplateFileName = templateFileName;
            exportSetting.ExFileName = exFileName;
            exportSetting.DicPosValues = keyValues;
            exportSetting.NullValueHandling = NullValueHandling.Include;

            string filePath = QyExcel_.ExportDiy(exportSetting);
            return filePath;
        }


        public void Export2ExcelWithInsertRow<T>(string fileName, List<T> exData, string fieldNames, string valueFillcols,
          int RowWirteStart, string rangeForCopy, string FormatDT = "yyyy-MM-dd HH:mm:ss", string localOrWeb = "local")
        {
            QyExcel_.Export2ExcelWithInsertRow(fileName, exData, fieldNames, valueFillcols,RowWirteStart, rangeForCopy,FormatDT, localOrWeb);
        }


        ///// <summary>
        ///// 导出List数据源，不带模板文件名（默认使用空白Excel表导出）
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="exDate">导出的数据</param>
        ///// <param name="propertiesFilds">导出的字段</param>
        ///// <returns>文件路径</returns>
        //public string ExportListToExcl<T>(List<T> exDate, string propertiesFilds, string FormatDT = "yyyy-MM-dd HH:mm:ss")
        //{
        //    Type type = typeof(T);

        //    return ExportListToExcl<T>(exDate, type.Name, propertiesFilds, FormatDT);
        //}
        ///// <summary>
        ///// 导出List数据，带模板文件名，当模板文件不存在的时候，使用空白Excel表导出
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="exDate">导出的数据</param>
        ///// <param name="exFileName">导出模板名</param>
        /////<param name="propertiesFilds">导出的字段</param>
        ///// <returns>文件路径</returns>
        //public string ExportListToExcl<T>(List<T> exDate, string exFileName, string propertiesFilds, 
        //    string FormatDT = "yyyy-MM-dd HH:mm:ss")
        //{

        //    Type type = typeof(T);
        //    //通过上述类型反射得到操作对象的公共属性（p）
        //    PropertyInfo[] _PropertyInfo = type.GetProperties();
        //    int i = 1;
        //    //传进来的列表都放入  错误的列不进行处理  内部实现
        //    List<string> listStr = new List<string>();
        //    string[] strs = propertiesFilds.Split(',');
        //    foreach (string s in strs)
        //    {
        //        if (s != "")
        //        {
        //            listStr.Add(s);
        //        }
        //    }
        //    IQyExclSettings exportSetting = new IQyExclSettings();
        //    exportSetting.ExFileName = exFileName;
        //    exportSetting.PropertiesFilds = listStr;
        //    exportSetting.NullValueHandling = NullValueHandling.Include;
        //    exportSetting.FormatDT = FormatDT;

        //    string filePath = QyExcel_.Export<T>(exDate, exportSetting);
        //    return filePath;
        //}


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="exDate"></param>
        ///// <param name="exportSetting">设定类型：本地或web</param>
        ///// <param name="exFileName"></param>
        ///// <param name="propertiesFilds"></param>
        ///// <param name="isreportNum">开始列前加序号</param>
        ///// <param name="FormatDT"></param>
        ///// <returns></returns>
        //public string ExportListToExcl<T>(List<T> exDate, string exFileName, string propertiesFilds, 
        //    bool isreportNum, string FormatDT = "yyyy-MM-dd HH:mm:ss")
        //{
        //    try
        //    {
        //        Type type = typeof(T);
        //        //通过上述类型反射得到操作对象的公共属性（p）
        //        PropertyInfo[] _PropertyInfo = type.GetProperties();
        //        int i = 1;
        //        //传进来的列表都放入  错误的列不进行处理  内部实现
        //        List<string> listStr = new List<string>();
        //        string[] strs = propertiesFilds.Split(',');
        //        foreach (string s in strs)
        //        {
        //            if (s != "")
        //            {
        //                listStr.Add(s);
        //            }
        //        }

        //        //exportSetting = new QyExclSettingsWeb();
        //        exportSetting.ExFileName = exFileName;// @"\" + exFileName;
        //        exportSetting.PropertiesFilds = listStr;
        //        exportSetting.NullValueHandling = NullValueHandling.Include;
        //        exportSetting.FormatDT = FormatDT;

        //        string filePath = QyExcel_.Export<T>(exDate, exportSetting, isreportNum);
        //        return filePath;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.Error(ex);
        //        return exFileName;
        //    }
        //}


        ///// <summary>
        /////字段带名称接口 - 不用模板
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="exDate">数据项</param>
        ///// <param name="exFileName">导出模板名--Report或随便不为空的不存在模板名称（使用默认空模板）</param>
        ///// <param name="propertiesFilds">导出的字段</param>
        ///// <param name="propertiesFildsName">导出字段对应名称，顺序填充</param>
        ///// <param name="isreportNum">是否导出序号 </param>
        ///// <returns></returns>
        //public string ExportListToExcl<T>(List<T> exDate, string exFileName, string propertiesFilds, 
        //    string propertiesFildsName, bool isreportNum, string FormatDT = "yyyy-MM-dd HH:mm:ss")
        //{

        //    Type type = typeof(T);
        //    //通过上述类型反射得到操作对象的公共属性（p）
        //    PropertyInfo[] _PropertyInfo = type.GetProperties();
        //    int i = 1;
        //    //传进来的列表都放入  错误的列不进行处理  内部实现
        //    List<string> listStr = new List<string>();
        //    List<string> listStrName = new List<string>();
        //    string[] strs = propertiesFilds.Split(',');

        //    foreach (string s in strs)
        //    {
        //        if (s != "")
        //        {
        //            listStr.Add(s);
        //        }
        //    }
        //    string[] strsName = propertiesFildsName.Split(',');
        //    foreach (string s in strsName)
        //    {
        //        if (s != "")
        //        {
        //            listStrName.Add(s);
        //        }
        //    }
        //    IQyExclSettings exportSetting = new IQyExclSettings();
        //    exportSetting.ExFileName = exFileName + ".xls";
        //    exportSetting.PropertiesFilds = listStr;
        //    exportSetting.PropertiesFildsName = listStrName;
        //    exportSetting.NullValueHandling = NullValueHandling.Include;
        //    exportSetting.FormatDT = FormatDT;

        //    string filePath = QyExcel_.Export<T>(exDate, exportSetting, isreportNum);
        //    return filePath;
        //}

    }
}