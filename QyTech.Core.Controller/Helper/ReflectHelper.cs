using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using log4net;
using QyTech.Core.Helpers;
using QyTech.Auth.Dao;
using QyTech.Core;
using System.Data.Objects;
using QyTech.Core.BLL;



namespace QyTech.Core.ExController
{
    public class ReflectHelper
    {
        public object aaa()
        {
        //    object dbobj, rowdataobj;
        //    Type dbtype;
        //    MethodInfo miObj;
        //    Type typeEm = typeof(EntityManager);
        //    dbtype = Type.GetType("QyTech.Auth.Dao." + objClassName + ", QyTech.Auth.Dao, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

        //    dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);
        //    miObj = typeEm.GetMethod("GetListNoPaging").MakeGenericMethod(dbtype);
        //    rowdataobj = miObj.Invoke(EManager, new object[] { where, orderby });
            return null;
        }
    }
}
