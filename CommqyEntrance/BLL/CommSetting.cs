using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Objects;
using QyTech.Core.BLL;

namespace QyTech.BLL
{
    public class CommSetting
    {
        public static ObjectContext dblink = new QyExpress.Dao.QyExpressEntities();//.Auth.Dao.wj_GisDbEntities();

        public static EntityManager EM = new EntityManager(dblink);
    }
}