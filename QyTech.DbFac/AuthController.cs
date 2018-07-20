using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using log4net;
using QyTech.Auth.Dao;
using System.Data.Objects;

using QyTech.Core;
using QyTech.Core.ExController;
using QyTech.Core.Helpers;
namespace QyTech.Auth
{
    public class AuthController:QyTechController
    {
        public AuthController()
            : base()
        {
            DbContext = new QyTech.Auth.Dao.QyTech_AuthEntities();
            objNameSpace = "QyTech.Auth.Dao";
            ObjectClassFullName = StrForReplaceObject + ", QyTech.Auth.Dao, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        }


    }
}
