using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QyTech.qyWcfServiceLib;

namespace QyExpress.WcfClient
{
    public class IExcelSercieCallback : ICallback
    {
        public void ExportedExcelFinished(string filename)
        {
            //应该提供下载，在congtroller中

        }
     
    }
}
