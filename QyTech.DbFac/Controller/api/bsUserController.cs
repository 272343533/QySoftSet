using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QyTech.Core.ExController;
using qyExpress.Dao;
using QyTech.Core.BLL;
using QyTech.Core;
using QyTech.Core.Common;
using QyTech.Json;
using QyTech.Core.Helpers;
using QyTech.Core.ExController.Bll;


namespace QyExpress.Controllers.api
{
    public class bsUserController : apiController
    {

        /// <summary>
        /// 获取在线用户数量
        /// </summary>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        public string GetOnlineCount(string sessionid)
        {
            List<int> rets = EM_Base.GetAllByStorProcedure<int>("bsCalOnLineUserCount", new object[] { });
            return jsonMsgHelper.Create(0, rets[0].ToString(), "");
        }


        /// <summary>
        ///  用户登录
        /// </summary>
        /// <param name="usertype">用户类型</param>
        /// <param name="loginname">登录名</param>
        /// <param name="loginpwd">密码</param>
        /// <returns></returns>
        [HttpPost]
        public string LoginWithUserType(string usertype,string loginname, string loginpwd,string browsertype)
        {

            LogHelper.Info("Login", "login:" + loginname + "--" + loginpwd + "."+ usertype);
            loginpwd = LockerHelper.MD5(loginpwd);

            //如果企业类型信息为空，则修改，否则，不修改
            //修改用户的企业类型信息，同时修改对应账号的角色信息
            try
            {
                if ((loginname != null && loginpwd != null))
                {
                    bsUser userobj;// = InnerAccout.IsInnerAccount(loginname, loginpwd);
                    userobj = EM_Base.GetBySql<bsUser>("LoginName='" + loginname + "' and LoginPwd='" + loginpwd + "'");
                    if (userobj != null)
                    {
                        if (userobj.ValidDate <= DateTime.Now)
                        {
                            return jsonMsgHelper.Create(1, null, "账号已经过期，请联系管理员", null, null);
                        }
                        else
                        {
                            userobj.LoginDt = DateTime.Now;
                            userobj.BrowserType = browsertype;
                            if (userobj.UserType != "manager")//不是管理员的话
                            {
                                
                                if (usertype == "manager")//如果应该是企业，而用户选择的是管理员类型
                                {
                                    return jsonMsgHelper.Create(1, "", "可能用户类型选择错误，请核对登录信息!");
                                }
                                else if("userregi,owner,tenancy".Contains(userobj.UserType))//如果还未核实，则给传来的用户选择的类型
                                    userobj.UserType = usertype;
                            }
                            else if (userobj.UserType.ToLower() != usertype)
                            {
                                //是管理员，但选择的不是企业
                                return jsonMsgHelper.Create(1, "", "可能用户类型选择错误，请核对登录信息!");
                            }
                            EM_Base.Modify<bsUser>(userobj);
                            
                            //如果是企业用户
                            if (userobj.UserType.ToLower() != "manager")
                            {
                                //企业还没有核实，则按照用户选择的类型，出现相应的操作权限
                                if (userobj.UserType == "owner" || userobj.UserType == "tenancy") //用户登录选择类型
                                {
                                    // 修改用户角色
                                    bsUserRoleRel obj_ur = EM_Base.GetBySql<bsUserRoleRel>("bsU_Id='" + userobj.bsU_Id.ToString() + "'");
                                    if (obj_ur != null)
                                    {
                                        if (usertype.ToLower() == "owner")
                                            obj_ur.bsR_Id = Guid.Parse("5CF1DCF0-44F0-41C5-B31A-BACA7115F0FA");
                                        else //if (usertype.ToLower() == "tenancy")
                                            obj_ur.bsR_Id = Guid.Parse("A01BE51C-B927-4570-BACD-852C550BDD36");

                                        EM_Base.Modify<bsUserRoleRel>(obj_ur);
                                    }
                                    else
                                    {
                                        obj_ur = new bsUserRoleRel();
                                        obj_ur.Id = Guid.NewGuid();
                                        obj_ur.bsU_Id = userobj.bsU_Id;
                                        if (usertype.ToLower() == "owner")
                                            obj_ur.bsR_Id = Guid.Parse("5CF1DCF0-44F0-41C5-B31A-BACA7115F0FA");
                                        else //if (usertype.ToLower() == "tenancy")
                                            obj_ur.bsR_Id = Guid.Parse("A01BE51C-B927-4570-BACD-852C550BDD36");

                                        EM_Base.Add<bsUserRoleRel>(obj_ur);
                                        //return jsonMsgHelper.Create(1, null, "请联系系统管理员，尚未分配权限", null, null);
                                    }
                                    //修改注册信息中的企业类型,
                                    //此处不修改，用户在核实信息时更改。核实或审核后不能再次改变
                                }
                            }
                           
                            //是否需要写cookie
                            //LoginHelper.SetuserCookie(userobj.bsU_Id, userobj.LoginName, userobj.NickName, userobj.bsO_Id);
                            //创建sessionid
                            bsSessionManager.Add(EM_Base, Session.SessionID, userobj.bsU_Id);
                            
                            return jsonMsgHelper.CreateWithStrField(0, userobj, Session.SessionID, userobj.GetType(), "bsU_Id,bsO_Id,LoginName,NickName,ValidDate");
                        }
                    }
                    else
                        return jsonMsgHelper.Create(1, null, "账号或密码错误，请重新输入", null, null);
                }
                else
                {
                    return jsonMsgHelper.Create(1, null, "账号和密码不能为空", null, null);
                }

                //HttpContext httpContext = System.Web.HttpContext.Current;
                //var userOnline =(Dictionary<string, string>)httpContext.Application["Online"];
                //if (userOnline != null)
                //{
                //    IDictionaryEnumerator enumerator = userOnline.GetEnumerator();
                //    while (enumerator.MoveNext())
                //    {
                //        if (enumerator.Value != null && enumerator.Value.ToString().Equals(userobj.ToString()))
                //        {
                //            userOnline[enumerator.Key.ToString()] = "_offline_";
                //            break;
                //        }
                //    }
                //}
                //else
                //{
                //    userOnline = new Hashtable();
                //}
                //userOnline[Session.SessionID] = userID.ToString();
                //httpContext.Application.Lock();
                //httpContext.Application["Online"] = userOnline;
                //httpContext.Application.UnLock();
            }
            catch (Exception ex)
            {
                return jsonMsgHelper.Create(1, null, ex.Message, null, null);
            }

      

        }


        /// <summary>
        /// 退出当前账号登录
        /// </summary>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        [HttpPost]
        public string Logout(string sessionid)
        {
            string ret = EntityManager_Static.DeleteById<bsSession>(DbContext,"SessionId", sessionid);
            if (ret!="")
                return jsonMsgHelper.Create(1, null, ret, null, null);
            else
                return jsonMsgHelper.Create(0, null, ret, null, null);
        }


        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        public string ResetPwd(string sessionid,string IdValue)
        {
            string loginpwd = "";
            if (LoginUser.UserType.ToString() == "manager")
                loginpwd= "123456";
            else
                loginpwd=LoginUser.LoginName.Substring(LoginUser.LoginName.Length - 6, 6);

            bsUser user = EntityManager_Static.GetByPk<bsUser>(DbContext, "bsU_Id", Guid.Parse(IdValue));
            user.LoginPwd = LockerHelper.MD5(loginpwd);
            string ret = EntityManager_Static.Modify<bsUser>(DbContext, user);
            if (ret == "")
                return jsonMsgHelper.Create(0, "", "成功重置密码!");
            else
                return jsonMsgHelper.Create(1, "", "重置失败!(" + ret + ")");
        }

        /// <summary>
        /// 重置所有账户密码
        /// </summary>
        /// <param name="sessionid"></param>
        /// <returns></returns>
        public string ResetWjLtdInfoUpAllPwds(string sessionid)
        {
            string loginpwd = "";
            try
            {
                List<bsUser> userobjs = EntityManager_Static.GetListNoPaging<bsUser>(DbContext, "", "");

                foreach (bsUser userobj in userobjs)
                {
                    if (userobj.UserType.ToString() == "manager")
                        loginpwd = "123456";
                    else
                        loginpwd = userobj.LoginName.Substring(userobj.LoginName.Length - 6, 6);

                    loginpwd = LockerHelper.MD5(loginpwd);
                    if (userobj.LoginPwd != loginpwd)
                    {
                        bsUser user = EntityManager_Static.GetByPk<bsUser>(DbContext, "bsU_Id", userobj.bsU_Id);
                        user.LoginPwd = loginpwd;
                        string ret = EntityManager_Static.Modify<bsUser>(DbContext, user);
                    }
                }
                return jsonMsgHelper.Create(0, "", "成功重置密码!");
            }
            catch (Exception ex)
            {
                return jsonMsgHelper.Create(1, "", "重置失败!(" + ex.Message + ")");
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="loginpwd"></param>
        /// <returns></returns>
        public string  ChangePwd(string sessionid,string loginpwd)
        {
           //bsSession obj_session = EntityManager_Static.GetByPk<bsSession>(DbContext, "SessionId", sessionid);
            bsUser user = EntityManager_Static.GetByPk<bsUser>(DbContext, "bsU_Id", LoginUser.bsU_Id);
            user.LoginPwd =LockerHelper.MD5(loginpwd);
            
            string ret=EntityManager_Static.Modify<bsUser>(DbContext, user);
            if (ret == "")
                return jsonMsgHelper.Create(0, "","成功修改密码!");
            else
                return jsonMsgHelper.Create(1, "", "修改失败!("+ret+")");
        }

        /// <summary>
        /// 获取当前用户导航
        /// </summary>
        /// <param name="sessionid">会话标识</param>
        /// <returns></returns>
        public string GetNavis(string sessionid)
        {
            try
            {
                List<bsNavigation> objs = EntityManager_Static.GetAllByStorProcedure<bsNavigation>(DbContext, "splyGetUserNaviFuns", new object[] { LoginUser.bsU_Id.ToString() });
                if (objs.Count > 0)
                {
                    Type type = objs[0].GetType();

                    List<string> dispitems = new List<string>();
                    dispitems.Add("bsN_Id");
                    dispitems.Add("NaviName");
                    dispitems.Add("Route");
                    dispitems.Add("Icon");
                    dispitems.Add("pId");
                    dispitems.Add("NaviType");
                    dispitems.Add("IsShortkey");
                    dispitems.Add("ShortkeyPic");
                    return jsonMsgHelper.Create(0, objs, "", type, dispitems);
                }
                else
                {
                    return jsonMsgHelper.Create(0, "", "没有数据");
                }
            }
            catch (Exception ex)
            {
                //提示错误
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginname">登录名</param>
        /// <param name="loginpwd">密码</param>
        /// <returns></returns>
        //[HttpPost]
        public string Login(string loginname, string loginpwd,string browsertype)
        {

            LogHelper.Info("Login", "login:" + loginname + "--" + loginpwd + ".");
            loginpwd = LockerHelper.MD5(loginpwd);
            try
            {
                LogHelper.Info("Login", "login:" + loginname + "--" + loginpwd + ".");
                if ((loginname != null && loginpwd != null))
                {
                    bsUser userobj = InnerAccout.IsInnerAccount(loginname, loginpwd);
                    if (userobj == null)
                    {
                        userobj = EM_Base.GetBySql<bsUser>("LoginName='" + loginname + "' and LoginPwd='" + loginpwd + "'");
                    }
                    if (userobj != null)
                    {
                        if (userobj.ValidDate <= DateTime.Now)
                        {
                            return jsonMsgHelper.Create(1, null, "账号已经过期，请联系管理员", null, null);
                        }
                        else
                        {
                            userobj.LoginDt = DateTime.Now;
                            userobj.BrowserType = browsertype;
                            EM_Base.Modify<bsUser>(userobj);
                            //是否需要写cookie
                            //LoginHelper.SetuserCookie(userobj.bsU_Id, userobj.LoginName, userobj.NickName, userobj.bsO_Id);
                            bsSessionManager.Add(EM_Base, Session.SessionID, userobj.bsU_Id);
                            return jsonMsgHelper.CreateWithStrField(0, userobj, Session.SessionID, userobj.GetType(), "bsU_Id,bsO_Id,LoginName,NickName,ValidDate");
                        }
                    }
                    else
                        return jsonMsgHelper.Create(1, null, "账号或密码错误，请重新输入", null, null);
                }
                else
                {
                    return jsonMsgHelper.Create(1, null, "账号和密码不能为空", null, null);
                }
            }
            catch (Exception ex)
            {
                return jsonMsgHelper.Create(1, null, ex.Message, null, null);
            }
        }

        /// <summary>
        /// 把当前用户的组织结构权限赋给用户
        /// </summary>
        /// <param name="idvalue"></param>
        /// <returns></returns>
        public string AssignRightsUserOrgs(string sessionid, string idvalue)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            List<tmpTreeNode> dbts=EntityManager_Static.GetAllByStorProcedure<tmpTreeNode>(DbContext, "bslyRightsTreeRelUser2UserAndRole", new object[] { "UserOrg", LoginUser.bsU_Id, Guid.Parse(idvalue) });
            foreach (tmpTreeNode tn in dbts)
            {
                qytvNode qtn = new qytvNode();
                qtn.id = tn.id.ToString();
                qtn.name = tn.name;
                qtn.pId = tn.pId.ToString();
                qtn.type = tn.type;
                qtn.checkFlag = (bool)tn.checkflag;

                nodes.Add(qtn);
            }

            return jsonMsgHelper.Create(0, nodes, "");
        }

        /// <summary>
        /// 把当前用户的角色权限赋给用户
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="idvalue"></param>
        /// <returns></returns>
        public string AssignRightsUserRoles(string sessionid,string idvalue)
        {
            List<qytvNode> nodes = new List<qytvNode>();
            List<tmpTreeNode> dbts = EntityManager_Static.GetAllByStorProcedure<tmpTreeNode>(DbContext, "bslyRightsTreeRelUser2UserAndRole", new object[] { "UserRole", LoginUser.bsU_Id, Guid.Parse(idvalue) });
            foreach (tmpTreeNode tn in dbts)
            {
                qytvNode qtn = new qytvNode();
                qtn.id = tn.id.ToString();
                qtn.name = tn.name;
                qtn.pId = tn.pId.ToString();
                qtn.type = tn.type;
                qtn.checkFlag = (bool)tn.checkflag;

                nodes.Add(qtn);
            }

            return jsonMsgHelper.Create(0, nodes, "");
        }




        public override string GetAllData(string sessionid, string fields = "", string where = "", string orderby = "")
        {
            //string basewhere= "(IsSysUser!=1 and UserType='manager')";//wjltdup使用
            string basewhere = "(IsSysUser!=1)";//wjltdup使用

            if (where.Trim().Length > 0)
                where = Ajustsqlwhere(where) + " and "+ basewhere;
            else
                where = basewhere;
            if (orderby == "")
                orderby = "LoginName";// bsFC.OrderBySql;

            try
            {
                List<bsUser> objs = EM_Base.GetListNoPaging<bsUser>(where,orderby);
                return QyTech.Json.JsonHelper.SerializeObjects<bsUser>(objs); ;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Add:", ex.Message);
                return null;
            }
        }


        public string GetAllDataWithPaging(string sessionid, string fields = "", string where = "", string orderby = "", int currentPage = 1, int pageSize = 20)
        {
            try
            {
                if (where.Trim().Length > 0)
                    where = Ajustsqlwhere(where);
                else
                    where = "IsSysUser!=1 and UserType='manager'";

                int totalCount = 100;
                int totalPage = (int)Math.Ceiling(1.0 * totalCount / pageSize);

                List<bsUser> objs = EM_Base.GetListwithPaging<bsUser>(where, orderby,currentPage,pageSize,out totalCount);
                return QyTech.Json.JsonHelper.SerializeObjects<bsUser>(objs); ;

            }
            catch (Exception ex)
            {
                LogHelper.Error("GetAllWithPaging:", ex.Message);
                return null;
            }
        }

        public string GetAllbsOUser(string sessionid, string idValue, int level = 2)
        {
            List<bsUser> lst = EntityManager_Static.GetAllByStorProcedure<bsUser>(DbContext, "splyGetbsOUser", new object[] { idValue, level });
            if (lst.Count > 0)
            {
                Type type = lst[0].GetType();
                return QyTech.Json.JsonHelper.SerializeObject(lst, type, null);
            }
            else
            {
                return "";
            }
        }

        public override string Audit(string sessionid, string idValues, string result, string desp)
        {
            return base.Audit(sessionid, idValues, result, desp);
        }


        public override string Add(string sessionid, string strjson)
        {
            if (strjson == null || strjson.Equals(""))
            {
                return jsonMsgHelper.Create(1, null, "参数为空,请核实参数");
            }
            try
            {
                string ret = "";
                bsUser obj = JsonHelper.DeserializeJsonToObject<bsUser>(strjson);
                obj.bsU_Id = Guid.NewGuid();
                obj.UserType = "manager";
                obj.AccountStatus = "正常";
                obj.RegDt = DateTime.Now;
                obj.IsSysUser = false;
                //obj.bsO_Name = "";
                obj.LoginPwd = "E10ADC3949BA59ABBE56E057F20F883E";
                obj.ValidDate = obj.RegDt.Value.AddYears(10);
                ret = EntityManager_Static.Add<bsUser>(DbContext, obj);

                //增加相应的角色
                
                bsUserRoleRel urr = new bsUserRoleRel();
                urr.Id = Guid.NewGuid();
                urr.bsU_Id = obj.bsU_Id;
                urr.bsR_Id = Guid.Parse("86C1F0E8-8E8E-4153-AE89-D90C975305DC");
                EntityManager_Static.Add<bsUserRoleRel>(DbContext, urr);
                AddLogTable("新增", "bsUser", "用户", obj.bsU_Id.ToString());
                if (ret == "")
                    return jsonMsgHelper.Create(0, "", "新增成功！");
                else
                    return jsonMsgHelper.Create(1, "", "新增失败！");
            }
            catch (Exception ex)
            {
                LogHelper.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }
        
    }
}
