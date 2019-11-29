using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QyExpress.Dao;
using System.Web.Configuration;

namespace QyExpress.GlobalStatic
{
    public class Varibles
    {
        public static string CurrbsAppName =WebConfigurationManager.AppSettings["currappName"];
    }
}