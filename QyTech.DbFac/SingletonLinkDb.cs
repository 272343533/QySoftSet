using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.DataClasses;
using System.Data.Objects;
using System.Data.Entity;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
namespace SunMvcExpress.Core.BLL
{
    
    public class SingletonLinkDb
    {
        private static ObjectContext Db_ = new QyTech.Auth.Dao.QyTech_AuthEntities();

   
        private SingletonLinkDb(string EntityName) { }  
        public static ObjectContext GetCommon{

            get
            {
                return Db_;
            }
        }

    }
}
