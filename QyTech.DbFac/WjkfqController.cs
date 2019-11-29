using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using log4net;
using Dao.QyBllApp;
using System.Data.Objects;

using QyTech.Core;
using QyTech.Core.ExController;
using QyTech.Core.Helpers;


namespace QyExpress
{
    public class WjkfqController : QyTechController
    {
        public WjkfqController()
            : base()
        {
            DbContext = new wj_GisDbEntities();
            objNameSpace = "Dao.QyBllApp";
            //		AssemblyQualifiedName	"Dao.QyBllApp.QyBllAppEntities, Dao.QyBllApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"	string

            ObjectClassFullName = StrForReplaceObject + ", Dao.QyBllApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        }


    }
}
