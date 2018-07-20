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

using Dao.WuTaiTemple;


namespace QyTech.Auth
{
    public class QyTechCommonController : QyTechController
    {
        public QyTechCommonController()
            : base()
        {
            //从xml文件中获取子类对应的信息，从而文件不需要进行配置

            StrDbContext = "wj_GisDbEntities";
            objNameSpace = "Dao.WuTaiTemple";
            ObjectClassFullName = StrForReplaceObject + ", Dao.WuTaiTemple, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        }


    }
}