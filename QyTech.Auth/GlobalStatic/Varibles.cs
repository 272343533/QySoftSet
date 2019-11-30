using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using qyExpress.Dao;
using System.Web.Configuration;

namespace qyExpress.GlobalStatic
{
    public class Varibles
    {
        public static string CurrbsAppName =WebConfigurationManager.AppSettings["currappName"];
    }
}