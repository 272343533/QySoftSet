using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using log4net;
using qyExpress.Dao;
using System.Data.Objects;

using QyTech.Core;
using QyTech.Core.ExController;
using QyTech.Core.Helpers;
namespace QyExpress
{
    public class apiController : QyTechController
    {
        public apiController()
            : base()
        {
            DbContext = new qyExpress.Dao.qyExpressEntities();
            objNameSpace = "qyExpress.Dao";
            ObjectClassFullName = StrForReplaceObject + ", qyExpress.Dao, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        }


    }
}
