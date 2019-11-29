using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using QyTech.Core;

namespace QyTech.ExcelOper
{
    /// <summary>
    /// excl导出设置
    /// </summary>
    public class IQyExclSettings
    {
        /// <summary>
        /// null值的处理，Ignore为忽略，Include为包括
        /// </summary>
        public NullValueHandling NullValueHandling = NullValueHandling.Ignore;
        protected string exServerPath = "";// System.Web.HttpContext.Current.Server.MapPath("~/DownLoads/Template/");
        protected string exTempPath = "";// System.Web.HttpContext.Current.Server.MapPath("~/DownLoads/Temp/");
        protected string exFileName;
        protected List<string> propertiesFilds = new List<string>();//要导出的字段列表  
        protected List<string> propertiesFildsDesp = new List<string>();//字段表头中文
        protected Dictionary<string, string> dicFields = new Dictionary<string, string>();//字段及中文对应关系
        protected string eliminateFilds = "EntityState,EntityKey"; //排除字段

        protected int RowStartValue_ = 2;
        protected int ColStartValue_ = 1;
        protected bool haveNumberColumn_ = false;

        protected string formatDT = "yyyy-MM-dd HH:mm:ss";


        /// <summary>
        /// 导出时DateTime类型的格式化
        /// </summary>
        public string FormatDT
        {
            get { return formatDT; }
            set { formatDT = value; }
        }
         /// <summary>
        /// 导出excl模板路径-默认路径为DownLoads/Template 必须存在
        /// </summary>
        public virtual string ExServerPath
        {
            get { return exServerPath; }
            set { exServerPath = System.Web.HttpContext.Current.Server.MapPath(value); }
        }

         /// <summary>
        /// 导出excl填充后路径，即下载路径
        /// </summary>
        public virtual string ExTempPath
        {
            get { return exTempPath; }
            set { exTempPath =value; }
        }
 
        public string ExFileName
        {
            get { return exFileName; }
            set { exFileName = value; }
        }
        /// <summary>
        /// 要导出的字段列表，null为全部导出
        /// </summary>
        public List<string> PropertiesFilds
        {
            get { return propertiesFilds; }
            set { propertiesFilds = value; }
        }
        public Dictionary<string,string> DicFields
        {
            get { return dicFields; }
            set { dicFields = value; }
        }
         /// <summary>
        /// 导出时排除的字段 - 优先级比PropertiesFilds 高
        /// </summary>
        public string EliminateFilds
        {
            get { return eliminateFilds; }
            set { eliminateFilds = value; }
        }

        public int RowStartValue
        {
            get { return RowStartValue_; }
            set { RowStartValue_ = value; }
        }
        public int ColStartValue
        {
            get { return ColStartValue_; }
            set { ColStartValue_ = value; }
        }

        public bool HaveNumberColumn
        {
            get { return haveNumberColumn_; }
            set { haveNumberColumn_ = value; }
        }

    }

}