using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using log4net;
using System.Collections;


using QyTech.Core.Helpers;
using QyTech.Core.Helpers;
using QyTech.Core;

using QyTech.Core.BLL;
using QyTech.Json;
using QyTech.Core.Common;
using QyTech.Core.ExController.Bll;
using QyTech.Core.ExController;

using Dao.WuTaiTemple;


namespace WuTaiTempleFire.Controllers
{
    public class WuTaiTempleController : QyTechController
    {
       public WuTaiTempleController()
            : base()
        {
            DbContext = new Dao.WuTaiTemple.WuTaiTempleEntities();
            objNameSpace = "Dao.WuTaiTemple";
            ObjectClassFullName = StrForReplaceObject + ", Dao.WuTaiTemple, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        }
       

    }
}
