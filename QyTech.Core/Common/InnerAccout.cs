using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QyExpress.Dao;

namespace QyTech.Core.Common
{

    /// <summary>
    ///  对应bsTable中的TRank，用于某类账户可用的表的级别,
    /// </summary>
    public enum AccountType {
        /// <summary>
        /// 普通用户
        /// </summary>
        UserAccount =0,
        /// <summary>
        /// 客户管理员
        /// </summary>
        sysAdmin = 1,
        /// <summary>
        /// 开发管理员
        /// </summary>
        devAccount =10,
        /// <summary>
        /// 平台管理员
        /// </summary>
        expressAccount =100}

    public class InnerAccout
    {
        private static bsUser devAdminUser_;
        private static bsUser expressAdminUser_;

        static InnerAccout()
        {
            devAdminUser_ = new bsUser();
            devAdminUser_.bsU_Id = new Guid("DD406509-FFF8-4515-9EA2-ECAAB435ADA6");
            devAdminUser_.bsO_Id = new Guid("F2C52587-19C6-4DD0-9671-6D05045C1D41");
            devAdminUser_.LoginName = "devadmin";
            devAdminUser_.NickName = "开发人员";
            devAdminUser_.LoginPwd = "F379EAF3C831B04DE153469D1BEC345E";//实际为666666


            expressAdminUser_ = new bsUser();
            expressAdminUser_.bsU_Id = new Guid("1D406509-FFF8-4515-9EA2-ECAAB435ADA6");
            expressAdminUser_.bsO_Id = new Guid("E1C99D09-04A6-4F98-A270-9655DD71FEE9");
            expressAdminUser_.LoginName = "expressAdmin";
            expressAdminUser_.NickName = "平台管理员";
            expressAdminUser_.LoginPwd = "F379EAF3C831B04DE153469D1BEC345E";//实际为666666

        }


        /// <summary>
        /// 应用开发人员，选择的应用的所有功能，
        /// </summary>
        public static bsUser devAdminUser
        {
            get {
                
                return devAdminUser_;
            }
        }

        /// <summary>
        /// 包括所有应用的功能
        /// </summary>
        public static bsUser expressAdminUser
        {
            get
            {
                return expressAdminUser_;
            }
        }


        public static bsUser IsInnerAccount(string loginname,string loginpwd)
        {
            bsUser userobj = null;
            if (loginname == devAdminUser_.LoginName && loginpwd == devAdminUser_.LoginPwd)
                return devAdminUser_;
            else if (loginname == expressAdminUser_.LoginName && loginpwd == expressAdminUser_.LoginPwd)
                return expressAdminUser_;
            else
                return userobj;
        }

        public static bool IsInnerAccount(bsUser userobj)
        {
            if (userobj.LoginName == devAdminUser_.LoginName || userobj.LoginName == expressAdminUser_.LoginName)
                return true;
            else
                return false;
        }

        public static bool IssysAdmin(bsUser userobj)
        {
            if (userobj.LoginName.Length >= 8 && userobj.LoginName.Substring(userobj.LoginName.Length - 8) == AccountType.sysAdmin.ToString())
                return true;
            else
                return false;
        }
    }
}
