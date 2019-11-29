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
    public class QyExclSettingsWeb : IQyExclSettings
    {

        public QyExclSettingsWeb()
        {
            exServerPath = System.Web.HttpContext.Current.Server.MapPath("~/DownLoads/Template/");
            exTempPath = System.Web.HttpContext.Current.Server.MapPath("~/DownLoads/Temp/");
        }
        /// <summary>
        /// 导出excl模板路径-默认路径为DownLoads/Template 必须存在
        /// </summary>
        public override string ExServerPath
        {
            get { return exServerPath; }
            set { exServerPath = System.Web.HttpContext.Current.Server.MapPath(value); }
        }

        /// <summary>
        /// 导出excl填充后路径，即下载路径
        /// </summary>
        public override string ExTempPath
        {
            get { return exTempPath; }
            set { exTempPath = System.Web.HttpContext.Current.Server.MapPath(value); }
        }
    }

}