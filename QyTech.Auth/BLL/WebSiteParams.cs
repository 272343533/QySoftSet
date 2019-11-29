using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QyExpress.BLL
{
    public class WebSiteParams
    {
        public static string currAppRunV = System.Web.Configuration.WebConfigurationManager.AppSettings["currAppRunV"];
        public static string currAppName = System.Web.Configuration.WebConfigurationManager.AppSettings["currAppName"];
        public static string currSoftCustId = System.Web.Configuration.WebConfigurationManager.AppSettings["currSoftCustId"];
        public static string currSoftCustUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["currSoftCustUrl"];
    }
}
