using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using QyTech.Core.BLL;
using Dao.QyBllApp;
using QyTech.ExcelOper;


namespace QyTech.qyWcfServiceLib
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class ExcelService : IExcelService
    {
        public static EntityManager EM = new EntityManager(new Dao.QyBllApp.QyBllAppEntities());
        #region IExcel Members

        public void Export(string filename, string where, string orderby)
        {
            QyExcelHelper exExcelHelper = new QyExcelHelper("local");
            List<t市局表格> objs = EM.GetListNoPaging<t市局表格>(where, orderby);

            exExcelHelper.ExportListToExcl<t市局表格>(objs, filename, "XH,ND,YF,DWDM,DW,SQ,QY,ZS,JYFW,ZCSJ,ZHY,HYXF,GM,QYS,CBRS,GS,DS,XS,ZD,QZCZ,NH,YD,PF,YFJFZC,PJZGRS,GDZCZJ,SCSJE,YYYE,ZGGZZE,SS,ZZZ,MJSS", "yyyy-MM-dd");

        }

        #endregion
    }
}
