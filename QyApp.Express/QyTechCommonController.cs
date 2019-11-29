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
    public class QyTechCommonController : QyTechController
    {
        public QyTechCommonController()
            : base()
        {
            //根据路由，无论是ui还是数据，，判断是哪个数据库，然后找到获得对应的相关对象，并实例化
            //Assembly ass = Assembly.LoadFrom(bsP.Code + @".dll");
            //Type type = ass.GetType("QyTech." + bsP.Code + ".ProtocalFac");
            //Object PacketFlag = Activator.CreateInstance(type, dtup.bsP_Id);


            DbContext = new QyExpress.Dao.QyExpressEntities();
            objNameSpace = "QyExpress.Dao";
            ObjectClassFullName = StrForReplaceObject + ", QyExpress.Dao, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

            //从xml文件中获取子类对应的信息，从而文件不需要进行配置//还没有配置 zhwsun on 2018-10-06

            StrDbContext = "QyTech_BLLEntities";
            objNameSpace = "Dao.QyTech_BLL";
            ObjectClassFullName = StrForReplaceObject + ", Dao.QyTech_BLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        }


    }
}