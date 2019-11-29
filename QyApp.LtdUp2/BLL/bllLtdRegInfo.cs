using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dao.QyBllApp;
using QyTech.Core.Common;
using QyTech.Core.BLL;
using QyExpress.Dao;
using System.Data.Objects;
namespace QyExpress.BLL
{
    public class bllLtdRegInfo
    {
        /// <summary>
        /// 核实后应该项用户表里增加数据,或者使用bllSpCreateUserAfterAudit存储过程,去掉了管理员审核，所以命名后面多了AfterAudit
        /// 在注册成功后也会调用
        /// </summary>
        public static string AddUserAfterAudited(ObjectContext objectcontext, LtdRegInfo lri)
        {

            // if (lri.IsCheck!=null && lri.IsCheck==1)

            string pwd = lri.NSRSBH.Length >= 6 ? lri.NSRSBH.Substring(lri.NSRSBH.Length - 6, 6) : lri.NSRSBH;
            pwd = QyTech.Core.Helpers.LockerHelper.MD5(pwd);
            string ret = EntityManager_Static.ExecuteSql(objectcontext, "exec bllSpCreateUserAfterAudit '" + lri.bsO_Id.ToString() + "','" + pwd + "'");
            return ret;
        }
    }
}