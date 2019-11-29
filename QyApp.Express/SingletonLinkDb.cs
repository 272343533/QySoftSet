using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
using System.Data.Entity;


//zhwsun
//218-10-06 note:暂时没有使用
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace SunMvcExpress.Core.BLL
{
    
    public class SingletonLinkDb
    {
        private static ObjectContext Db_ = new QyExpress.Dao.QyExpressEntities();

   
        private SingletonLinkDb(string EntityName) { }  
        public static ObjectContext GetCommon{

            get
            {
                return Db_;
            }
        }

    }
}
