using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using log4net;

using System.Data.Objects;

using QyTech.Core;
using QyTech.Core.ExController;
using QyTech.Core.Helpers;

using Dao.WjGisDb;

namespace QyTech.DbFac
{
    public class WjkfqController : QyTechController
    {

        public WjkfqController()
            : base()
        {
            StrDbContext = "wj_GisDbEntities";
            //DbContext = new wj_GisDbEntities();
            objNameSpace = "Dao.WjGisDb";
            ObjectClassFullName = StrForReplaceObject + ", Dao.WjGisDb, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            //DbContext = new QyTech.Auth.aut();
        }


    }
}
