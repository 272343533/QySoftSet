using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using log4net;
using QyTech.Core.Helpers;

using QyTech.Core;
using System.Data.Objects;
using QyTech.Core.BLL;
using QyTech.Core.Common;
using QyTech.Core.ExController.Bll;

using qyExpress.Dao;

namespace QyTech.Core.ExController
{

    public partial class QyTechController:Controller
    {
        /// <summary>
        /// 当前登录用户
        /// </summary>
        protected bsUser LoginUser = new bsUser();
        /// <summary>
        /// 当前登录用户所属单位
        /// </summary>
        protected bsOrganize Login_UserOrg = null;




        //如果是UI访问则为当前使用的funconf
        protected bsFunConf bsFC;
        //如果是数据表访问则为当前使用的表
        protected bsTable bsT;
        protected UrlType urlType;
        protected string whereDefaultSql;
        protected string orderDefaultSql;

        private EntityManager EManagerApp_;//用于外部数据库的查询，引用
        private ObjectContext dbContextApp_;//用于外部数据库的数据库上下文

        private EntityManager EManager_;// 用于配置数据库的查询，引用
        protected DataManager dataManager;//用于应用数据库的访问 //2018-11-05 modified proteced for bsnavigation使用


        protected string objNameSpace = "";//用于对象的命名空间，处理具体的类时需要加上命名控件使用
        private string objClassName = "";//用于dao对象的类名称
        private string objClassFullName = "";//用于dao对象的程序集名称 
        private string strForReplaceObject = "ten1111111111";


        public QyTechController()
        {
            EManager_ = new EntityManager(new qyExpressEntities());
        }

        public EntityManager EM_Base
        {
            get { return EManager_; }
        }

        public EntityManager EM_App
        {
            get { return EManagerApp_; }
        }

        /// <summary>
        /// 用于外部对数据库上下文的赋值
        /// </summary>
        public ObjectContext DbContext
        {
            set
            {
                this.dbContextApp_ = value;
                try
                {
                    EManagerApp_ = new EntityManager(dbContextApp_);
                }
                catch (Exception ex)
                {
                    LogHelper.Error(ex);
                    RedirectToAction("newindex", "Home");
                }
            }
            get { return dbContextApp_; }
        }

        public String StrDbContext
        {
            set
            {
                //从数据库中获取该dbContrext，然后反射一个dao实体对象
                bsDb bsdbobj = EManager_.GetByPk<bsDb>("DbContextName", value);
                System.Reflection.Assembly ass = Assembly.LoadFrom(System.AppDomain.CurrentDomain.BaseDirectory + @"bin\Dao.WjGisDb.dll");
                string objStr =bsdbobj.objNameSpace+"."+bsdbobj.DbContextName+bsdbobj.ObjectClassFullName;//.Substring(1)
                Type t = Type.GetType(objStr);
                object obj =  ass.CreateInstance(objStr);
                DbContext = (ObjectContext)Activator.CreateInstance(t);
            }
        }

        /// <summduixary>
        /// 类名
        /// </summary>
        protected string ObjectClassName
        {
            set
            {
                objClassName = value;
                ObjectClassFullName = ObjectClassFullName.Replace(strForReplaceObject, objNameSpace + "." + objClassName);
            }
            get
            {
                return objClassName;
            }
        }

        /// <summary>
        /// 类全部信息
        /// </summary>
        protected string ObjectClassFullName
        {
            set { objClassFullName = value;
                dataManager = new DataManager(EManagerApp_, objClassFullName);
            }
            get { return objClassFullName; }
        }






        /// <summary>
        /// 用于配置类访问数据库
        /// </summary>
        protected EntityManager EManager
        {
            get { return EManager_; }
        }
        /// <summary>
        /// 用于引用类访问数据库
        /// </summary>
        protected EntityManager EManager_App
        {
            get { return EManagerApp_; }
        }

        protected string StrForReplaceObject
        {
            get { return strForReplaceObject; }
        }

        
        /// <summary>
        /// web请求会被这里捕获，然后可以得到Area/Controller/Action 然后。。。
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LogHelper.Info("OnAction", "请求地址："+ Request.Url.ToString());

             string InCName = filterContext.RouteData.Values["controller"].ToString();
            string InAName = filterContext.RouteData.Values["action"].ToString();
            string currUrl_CA= (InCName + "/" + InAName).ToLower();

          
           if (!bsSessionManager.GetUrlWithoutSession().Contains(currUrl_CA))
            {
                #region cookie使用 目前用session方式，cookie已注释
                //if (LoginHelper.IsLogin())
                //{
                //    LoginUser = LoginHelper.GetLoginUser();
                //    Login_UserOrg = LoginHelper.GetLoginOrg();
                //}
                //////if (!LoginHelper.IsLogin() || HttpContext.Request.Cookies["bso_id"] == null)
                //////{
                //////    HttpContext.Response.Redirect("/Home/Login");
                //////    return;
                //////}
                //////else
                //////{
                //////    if (LoginHelper.GetLoginUserId() == Guid.Empty)
                //////    {
                //////        HttpContext.Response.Redirect("/Home/Login");
                //////        return;
                //////    }
                //////    string sysfunc = filterContext.Controller.ToString();
                //////    int m = sysfunc.LastIndexOf('.');
                //////    int msize = sysfunc.Length - m - 11;
                //////    sysfunc = sysfunc.Substring(m + 1, msize);
                //////    string actionname = filterContext.ActionDescriptor.ActionName;
                //判断用户是否有此路由权限
                //////    if (!LoginHelper.isusefunc(LoginHelper.GetLoginUserId(), sysfunc, actionname))
                //////    {
                //////        HttpContext.Response.Redirect("/Home/Error");
                //////        return;
                //////    }
                //////}

                //if (LoginUser.bsU_Id == null || LoginUser.bsU_Id == Guid.Empty)
                //{
                //    LogHelper.Info(Request.Url.ToString(), "登录信息已过期，请重新登录，然后操作");
                //    filterContext.Result = RedirectToAction("login", "Home"); // new RedirectToRouteResult("Login", new RouteValueDictionary { { "from", Request.Url.ToString() } });
                //}
                //else
                //{
                //    LogHelper.Info(Request.Url.ToString(), "登录信息有效，继续操作");
                //    //base.OnActionExecuting(filterContext);
                //}
                #endregion

                #region 用户登录验证 可通过webconfig配置取消，设为调试模式
                if (filterContext.ActionParameters.ContainsKey("sessionid"))//需要sessionid参数
                {
                    if (WebSiteParams.currAppRunV.Substring(0,5) != "debug")
                    {
                        if (filterContext.ActionParameters["sessionid"] == null)//传入了sessionid参数
                        {
                            //需要sessioniid，但是没有给
                            LogHelper.Info(Request.Url.ToString(), "没有登录信息，需转登录界面");
                            HttpContext.Response.Redirect("http://119.3.21.35:9001/", true);
                            return;
                            //base.OnActionExecuting(filterContext);
                        }
                        else if (filterContext.ActionParameters["sessionid"].ToString().Length > 24)//form传过来的
                        {
                            LoginUser = EManager_.GetByPk<bsUser>("bsU_Id", Guid.Parse(filterContext.ActionParameters["sessionid"].ToString()));
                        }
                        else
                        {
                            LoginUser = QyTech.Core.ExController.Bll.bsSessionManager.GetLoginUser(EManager_, filterContext.ActionParameters["sessionid"].ToString());
                        }

                        if (LoginUser == null || LoginUser.bsU_Id == null || LoginUser.bsU_Id == Guid.Empty)
                        {
                            LogHelper.Info(Request.Url.ToString(), filterContext.ActionParameters["sessionid"].ToString() + "登录信息已过期，请重新登录！");
                            //filterContext.Result = RedirectToAction("login", "Home"); // new RedirectToRouteResult("Login", new RouteValueDictionary { { "from", Request.Url.ToString() } });
                            HttpContext.Response.Redirect(System.Web.Configuration.WebConfigurationManager.AppSettings["currSoftCustUrl"], true);
                            return;
                        }

                        //判断用户是否有此路由权限,用户id，控制器，action
                        //if (!LoginHelper.isusefunc(LoginHelper.GetLoginUserId(), sysfunc, actionname))
                        //{
                        //    HttpContext.Response.Redirect("/Home/Error");
                        //    return;
                        //}
                    }
                }
                #endregion

                //日志记录
                AddLogFun(currUrl_CA, "");

                //如果是动态路由，则需要判断在哪，如果不是，则应该直接调用对应的方法
                //可是bst对象需要
                if (RouteData.Values["dynamicRoute"] != null) //没有控制器，则为动态路由
                {
                    //需要判断是UI还是DAO，无论是谁，操作的最终都是一个表的信息
                    object accessobj = null;
                    urlType = bllbsUserFields.GetAccessObject(EManager, RouteData, ref accessobj);
                    if (urlType == UrlType.UI)
                    {
                        bsFC = accessobj as bsFunConf;
                        whereDefaultSql = bsFC.baseWhereSql;
                        orderDefaultSql = bsFC.OrderBySql;
                        bsT = EManager.GetByPk<bsTable>("bsT_Id", bsFC.bsT_Id);
                        SetObjectClassNamebyTName(bsT.TName);
                    }
                    else if (urlType==UrlType.DAO)
                    {
                        bsT = accessobj as bsTable;
                        whereDefaultSql = bsT.InitWhereSql;
                        orderDefaultSql = bsT.InitOrderBy;
                        SetObjectClassNamebyTName(bsT.TName);
                    }
                    else //NoNe
                    {
                        SetObjectClassNamebyTName(RouteData.Values["dynamicroute"].ToString().Split(new char[] { '/' })[0]);
                    }
                }
                else //不是动态路由，也就是存在控制器
                {
                    //此时也不一定存在具体的action，所以也要尝试寻找，如果找不到，就应该明确已经写了对应的Action方法
                    try
                    {
                        object accessobj = null;
                        urlType = bllbsUserFields.GetAccessObject(EManager, RouteData, ref accessobj);
                        if (urlType != UrlType.None)
                        {
                            if (urlType == UrlType.UI)
                            {
                                bsFC = accessobj as bsFunConf;
                                whereDefaultSql = bsFC.baseWhereSql;
                                orderDefaultSql = bsFC.OrderBySql;
                                bsT = EManager.GetByPk<bsTable>("bsT_Id", bsFC.bsT_Id);
                            }
                            else if (urlType == UrlType.DAO)
                            {
                                bsT = accessobj as bsTable;
                                whereDefaultSql = bsT.InitWhereSql;
                                orderDefaultSql = bsT.InitOrderBy;
                            }
                            objClassName = bsT.TName;
                            ObjectClassFullName = ObjectClassFullName.Replace(strForReplaceObject, objNameSpace + "." + ObjectClassName);// +", qyExpress.Dao, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
                        }
                        else
                        {
                            SetObjectClassNamebyTName(filterContext.RouteData.Values["controller"].ToString());
                        }
                    }
                    catch(Exception ex)
                    {
                        LogHelper.Error(ex.Message);
                    }
                }
            }
        }




        /// <summary>
        /// 需要吗？
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public JsonResult treeDrag<T>(Guid id, Guid pid)
        {
            //T objdb = EntityManager<T>.GetByPk<T>("Id", id);
            //Type type = typeof(T);
            //object obj = Activator.CreateInstance(type);
            //PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //foreach (PropertyInfo item in props)
            //{
            //    if (item.Name == "PId")
            //    {
            //        item.SetValue(obj, Convert.ChangeType(item.Name, item.PropertyType), null);
            //    }
            //}
            //EntityManager<T>.Modify<T>(objdb);

            return null;
        }



    }
}
