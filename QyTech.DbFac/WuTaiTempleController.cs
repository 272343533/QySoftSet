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

using Dao.WuTaiTemple;

namespace QyTech.Auth
{
    public class WuTaiTempleController : QyTechController
    {

        public WuTaiTempleController()
            : base()
        {
            //StrDbContext = "WuTaiTempleEntities";
            DbContext = new WuTaiTempleEntities();
            objNameSpace = "Dao.WuTaiTemple";
            ObjectClassFullName = StrForReplaceObject + ", Dao.WuTaiTemple, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            //DbContext = new QyTech.Auth.aut();
        }
       

    }
}
