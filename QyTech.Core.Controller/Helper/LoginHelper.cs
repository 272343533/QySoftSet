using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//-------------新增引用
using System.Web.Security;
using QyTech.Core.BLL ;
using QyExpress.Dao;
using System.Data.Objects;
using QyTech.Core.Common;

using log4net;


namespace QyTech.Core.Helpers
{
    /// <summary>
    /// 用于登录用到的各种操作接口,需进一步测试 noted by zhwsun on 2018-10-12
    /// </summary>
    public class LoginHelper
    {
        private static ILog log = log4net.LogManager.GetLogger("LoginHelper");
        private static QyExpressEntities db_ = new QyExpressEntities();
        private static QyTech.Core.BLL.EntityManager EManager= new QyTech.Core.BLL.EntityManager(db_);

        #region Login登录相关
        private static string WebSiteName = "WinterSunExpress";
        private static string COOKIENAME = WebSiteName;
        private static string cookieID = WebSiteName+"ID";
        private static string cookieName = WebSiteName+"Name";
        private static string cookieOrg = WebSiteName+"Org";
        private static string cookieCompany = WebSiteName+"Company";

        //-------------------------登录  -1 代表登录失败，否则返回UserID
        public static bool Login(string name, string pwd)
        {
            //if (!IsLogin()) 不需要判断，覆盖原来登录即可
            try
            {
                 string custcode;
                 string loginname;
                log.Info("login:" + name + pwd + "end");// Environment.UserDomainName
                
                //pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5");
                pwd = LockerHelper.MD5(pwd);
                DateTime dtnow = DateTime.Now;
                bsUser userobj = InnerAccout.IsInnerAccount(name, pwd);
                if (userobj!=null)
                {
                    SetuserCookie(userobj.bsU_Id, userobj.LoginName, userobj.NickName, userobj.bsO_Id);
                }
                else
                {
                    if (name != InnerAccout.expressAdminUser.LoginName.ToString())
                    {
                        custcode = name.Substring(0, 4);
                        loginname = name.Substring(4);

                        var custobj = EManager.GetByPk<bsSoftCustInfo>( "Code", custcode);
                        Guid custid = custobj.bsS_Id;
                        userobj = EManager.GetBySql<bsUser>("LoginName ='" + loginname + "' and LoginPwd='" + pwd + "' and CustId ='" + custid + "' and AccountStatus = 1 and ValidDate >'" + dtnow.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                    }
                    else
                    {
                        userobj = EManager.GetBySql<bsUser>("LoginName ='" + name + "' and LoginPwd='" + pwd + "' and AccountStatus = 1 and ValidDate >'" + dtnow.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                   
                    }
                    if (userobj != null)
                    {
                        SetuserCookie(userobj.bsU_Id, name, userobj.NickName, userobj.bsO_Id);
                    }
                    else
                    {
                        log.Info("login:false1");
                        return false;
                    }
                }
               
                log.Info("login:true1");

                return true;
            }
            catch (Exception ex)
            {
                log.Error("login:Message" + ex.Message);
                return false;
            }
        }

        //-------------------------登出
        public static void Logout()
        {
            SetCookieClear();
        }

        //登录用户
        public static void SetuserCookie(Guid UserID, string loginName, string nickName,Guid orgId)
        {
            HttpContext.Current.Response.AddHeader("P3P", "CP=CAO PSA OUR");
            System.Web.Security.FormsAuthentication.SetAuthCookie(UserID.ToString(), false);

            SetUserNameInCookie(UserID, loginName,orgId);
        }

        //-------------------------写cookie
        private static void SetUserNameInCookie(Guid UserID, string loginName, Guid orgId)
        {
            HttpContext.Current.Response.AddHeader("P3P", "CP=CAO PSA OUR");

            HttpContext.Current.Response.Cookies.Remove(COOKIENAME);
            HttpCookie httpcookie = new HttpCookie(COOKIENAME);
            
            httpcookie.Values.Add(cookieID, UserID.ToString());
            httpcookie.Values.Add(cookieName, HttpUtility.UrlEncode(loginName));
            httpcookie.Values.Add(cookieOrg, orgId.ToString());
            
            httpcookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.Cookies.Add(httpcookie);

        }

        private static void SetCookieClear()
        {
            HttpContext.Current.Response.AddHeader("P3P", "CP=CAO PSA OUR");
            FormsAuthentication.SignOut();
        }



        #endregion


        #region 用户接口

        public static bool IsLogin()
        {
            return System.Web.HttpContext.Current.Request.IsAuthenticated;
            
        }

        //------------------------获取用户ID---对应UserID，返回-1表示未登录
        public static Guid GetLoginUserId()
        {
            if (IsLogin())
            {
                return new Guid(HttpContext.Current.User.Identity.Name);
            }
            else
            {
                return Guid.Empty;
            }
        }
        public static Guid GetLoginOrgId()
        {
            if (IsLogin())
            {
                return new Guid(HttpContext.Current.Request.Cookies[COOKIENAME][cookieOrg]);
            }
            else
            {
                return Guid.Empty;
            }
        }
        public static bsOrganize GetLoginOrg()
        {
            Guid orgid = GetLoginOrgId();
            if (orgid != Guid.Empty)
            {
                return EManager.GetByPk<bsOrganize>("Id", orgid);
            }
            return null;
        }

        //------------------------获取用户名
        public static string GetLoginUserName()
        {
            //return "me:";
            if (HttpContext.Current.Request.Cookies[COOKIENAME] == null)
                return null;
            else
                return HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.Cookies[COOKIENAME][cookieName]);
        }
        public static bsUser GetLoginUser()
        {
            bsUser currUser =null;

            if (HttpContext.Current.User.Identity.Name != "")
            {
                Guid userid = new Guid(HttpContext.Current.User.Identity.Name);
                if (userid != Guid.Empty)
                {
                    currUser = EManager.GetByPk<bsUser>("UserID", userid);
                }
            }
            return currUser;
        }
        //------------------------获取companyID
        //public static Guid GetLoginCompanyId()
        //{
        //    return new Guid(HttpContext.Current.Request.Cookies[COOKIENAME][cookieCompany]);
        //}

        public static string GetLoginNickName()
        {

            Guid userid = new Guid(HttpContext.Current.User.Identity.Name);
            var obj = db_.bsUser.SingleOrDefault(o => o.bsU_Id == userid);
            if (obj == null)
                return "";
            else
                return obj.NickName;
        }

        //public static bsOrganize GetLoginCompany()
        //{

        //    Guid orgid = new Guid(HttpContext.Current.Request.Cookies[COOKIENAME][cookieCompany]);
        //    var obj = EntityManager.GetByPk<bsOrganize>(db_, "Id", orgid);
        //    return obj;
        //}


        #endregion

        #region ---------------权限接口-----公司问题暂时不太好处理！

        //public static bool isusefunc(int UserID, int funcID)
        //{
        //    using (var db = new WSExpressEntities())
        //    {
        //        int n = db.ExecuteStoreCommand("select count(*) from Employee, Role, SystemFunc,EmpRoleRel, RolFuncRel " + 
        //            "where Employee.UserID = " + UserID.ToString() +" and SystemFunc.FuncID = " + funcID +
        //            " and Employee.UserID =EmpRoleRe.UserID and Role.RoleID = EmpRoleRe.RoleID and SystemFunc.FuncID =RolFuncRel.FuncID and RolFuncRel.RoleID = Role.RoleID", null);
        //        if (n > 0) return true;
        //    }
        //    return false;
        //}

            /// <summary>
            /// 是否是用户有无此路由的权限？//2018-10-17
            /// </summary>
            /// <param name="UserID"></param>
            /// <param name="controllername"></param>
            /// <param name="actionname"></param>
            /// <returns></returns>
        public static bool isusefunc(Guid UserID, string controllername, string actionname)
        {
            //需要修改 2015-01-07


            string str_con = "  SELECT     COUNT(*) AS Expr1 FROM         RolFuncRel INNER JOIN SystemFunc ON RolFuncRel.FuncID = SystemFunc.FuncID " +
            "INNER JOIN Employee INNER JOIN "
            + "EmpRoleRel ON Employee.UserID = EmpRoleRel.UserID INNER JOIN "
            + "Role ON EmpRoleRel.RoleID = Role.RoleID ON RolFuncRel.RoleID = Role.RoleID "
            + "WHERE (Employee.UserID = " + UserID.ToString() + ") AND (SystemFunc.FunContr = '" + controllername + "')";

            System.Data.Objects.ObjectResult<int> a = db_.ExecuteStoreQuery<int>(str_con, null);
            List<int> aa = a.ToList();
            if (aa[0] > 0)
                return true;

            string str_act = "  SELECT     COUNT(*) AS Expr1 FROM         RolFuncRel INNER JOIN SystemFunc ON RolFuncRel.FuncID = SystemFunc.FuncID " +
                "INNER JOIN Employee INNER JOIN "
                  + "EmpRoleRel ON Employee.UserID = EmpRoleRel.UserID INNER JOIN "
                   + "Role ON EmpRoleRel.RoleID = Role.RoleID ON RolFuncRel.RoleID = Role.RoleID "
                   + "WHERE (Employee.UserID = " + UserID.ToString() + ") AND (SystemFunc.FunContr = '"
                   + controllername + "_" + actionname + "')";
            a = db_.ExecuteStoreQuery<int>(str_act, null);
            aa = a.ToList();
            if (aa[0] > 0)
                return true;
            else
                return false;
        }

        #endregion

        /// <summary>
        /// 获取当前用户的权限范围
        /// </summary>
        /// <returns></returns>
        //public static List<String> AuthenticatedActionList()
        //{
        //    List<String> target = new List<string>();
        //    int usrId = GetLoginUserID();
        //    using (var db = new WSExpressEntities())
        //    {
        //        if (usrId != 1)
        //        {
        //            //需要建立视图 2015-01-07
        //            //var list = db.vw_EmpFuncList.Where(e => e.UserID == usrId);
        //            //if (list != null && list.Count() > 0)
        //            //{
        //            //    foreach (var m in list)
        //            //    {
        //            //        target.Add(m.FuncName);
        //            //    }
        //            //}
        //        }
        //        else
        //        {
        //            var list = db.bsSysFunc;
        //            if (list != null && list.Count() > 0)
        //            {
        //                foreach (var m in list)
        //                {
        //                    target.Add(m.FuncName);
        //                }
        //            }
        //        }
        //    }
        //    return target;
        //}

        public static SortedList<string, string[]> AuthenticatedActionList()
        {
            SortedList<string, string[]> target = new SortedList<string, string[]>();
            Guid usrId = GetLoginUserId();
            //using (var db = new WSExpressEntities())
            //{
                ////if (usrId != 1)
                ////{
                ////    //需要建立视图 2015-01-07
                ////    //var list = db.vw_EmpFuncList.Where(e => e.UserID == usrId);
                ////    //if (list != null && list.Count() > 0)
                ////    //{
                ////    //    foreach (var m in list)
                ////    //    {
                ////    //        target.Add(m.FuncName);
                ////    //    }
                ////    //}
                ////}
                ////else
                ////{
                ////    var list = db.bsSysFunc;
                ////    if (list != null && list.Count() > 0)
                ////    {
                ////        foreach (var m in list)
                ////        {
                ////            target.Add(m.FuncName,);
                ////        }
                ////    }
                ////}
            //}
            return target;
        }

      
    }
}