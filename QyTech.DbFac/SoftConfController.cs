using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using log4net;
using QyExpress.Dao;
using System.Data.Objects;

using QyTech.Core;
using QyTech.Core.ExController;
using QyTech.Core.Helpers;
namespace QyExpress
{
    public class SoftConfController:QyTechController
    {
        public SoftConfController()
            : base()
        {
            DbContext = new QyExpress.Dao.QyExpressEntities();
            objNameSpace = "QyExpress.Dao";
            ObjectClassFullName = StrForReplaceObject + ", QyExpress.Dao, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        }


    }
}
