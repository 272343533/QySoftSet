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
    public class BllAppController : QyTechController
    {
        public BllAppController()
            : base()
        {
            DbContext = new szEIPAppEntities();
            objNameSpace = "Dao.QyBllApp";
            //		AssemblyQualifiedName	"Dao.QyBllApp.QyBllAppEntities, Dao.QyBllApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"	string

            ObjectClassFullName = StrForReplaceObject + ", Dao.szEIPApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        }


    }
}
