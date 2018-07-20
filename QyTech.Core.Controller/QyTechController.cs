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
using QyTech.Json;
using QyTech.Core.ExController.Bll;

using QyTech.Auth.Dao;


[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
  
namespace QyTech.Core.ExController
{
    public partial class QyTechController:Controller
    {
        protected static ILog log = log4net.LogManager.GetLogger("QyTechController");
        
        
        protected bsUser LoginUser = new bsUser();
        protected bsOrganize Login_UserOrg = null;
        protected LogHelper logHelper = new LogHelper();


        protected string errMsg;

        protected string RefreshRel = ""; //nouse

        protected Guid bsFC_Id;//当前使用的功能id
        protected bsFunConf bsFC;

        private EntityManager EManagerApp_;//用于外部数据库的查询，引用
        private ObjectContext dbContextApp_;//用于外部数据库的数据库上下文


        private EntityManager EManager_;//用于配置数据库的查询，引用
       

        protected string objNameSpace = "";//用于对象的命名空间
        private string objClassName = "";//用于dao对象的类名称
        private string objClassFullName = "";//用于dao对象的完整命名空间名称 
        private string strForReplaceObject = "ten1111111111";

        private DataManager dataManager;//用于应用数据库的访问

        public QyTechController()
        {
            EManager_ = new EntityManager(new QyTech_AuthEntities());
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
                    if (LoginHelper.IsLogin())
                    {
                        LoginUser = LoginHelper.GetLoginUser();

                        Login_UserOrg = LoginHelper.GetLoginOrg();

                        logHelper = new LogHelper();
                    }

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
                string objStr=bsdbobj.objNameSpace+"."+bsdbobj.DbContextName+bsdbobj.ObjectClassFullName;//.Substring(1)
                Type t = Type.GetType(objStr);

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





        protected Dictionary<string, bsFunField> getFunFields(Guid bsFc_Id)
        {
            Dictionary<string, bsFunField> bsFFs = new Dictionary<string, bsFunField>();
            List<bsFunField> ffs = EManager_.GetListNoPaging<bsFunField>("bsFC_Id='" + this.bsFC_Id.ToString() + "'", "");

            foreach (bsFunField item in ffs)
            {
                bsFFs.Add(item.FName, item);
            }
            return bsFFs;
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

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
         

            LogHelper.Info(Request.Url.ToString(), "验证登录信息");



            if (LoginUser.bsU_Id == null || LoginUser.bsU_Id == Guid.Empty)
            {
                LoginUser.bsU_Id = Guid.Parse("F4DC74A7-480C-43D7-A2D2-1E6E8B7AF5CC");
            }
            //if (LoginUser.bsU_Id == null || LoginUser.bsU_Id == Guid.Empty)
            //{
            //    LogHelper.Info(Request.Url.ToString(), "登录信息已过期，请重新登录，然后操作");
            //    filterContext.Result = RedirectToAction("login", "Home"); // new RedirectToRouteResult("Login", new RouteValueDictionary { { "from", Request.Url.ToString() } });
            //}
            //else
            //{
            //    LogHelper.Info(Request.Url.ToString(), "登录信息有效，继续操作");
            //    base.OnActionExecuting(filterContext);
            //}

            bsFC_Id = bllbsUserFields.GetFunConfId(EManager, RouteData);
            if (bsFC_Id != Guid.Empty)
            {
                bsFC = EManager_.GetByPk<bsFunConf>("bsFc_Id", bsFC_Id);

                objClassName = bsFC.TName;
                ObjectClassFullName = ObjectClassFullName.Replace(strForReplaceObject, objNameSpace + "." + ObjectClassName);// +", QyTech.Auth.Dao, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            }
        }
     
        public string  ReturnErrMessage(Exception ex)
        {
            string errmsg = ex.Message;
            if (ex.InnerException != null)
                errmsg += " detail:" + ex.InnerException.Message;
            return errmsg;
        }
        public ILog GetCurrentLogWithAction()
        {
            return log4net.LogManager.GetLogger(RouteData.Route.GetRouteData(this.HttpContext).Values["action"].ToString());
        }
        public ILog GetCurrentLogWithController()
        {
            //            RouteData.Route.GetRouteData(this.HttpContext).Values["controller"] 
            //RouteData.Route.GetRouteData(this.HttpContext).Values["action"] 
            //或 
            //RouteData.Values["controller"] 
            //RouteData.Values["action"] 
            return log4net.LogManager.GetLogger(RouteData.Values["controller"].ToString());
        }

        public void logInfo(string msg)
        {
            log = GetCurrentLogWithAction();
            log.Info(msg);
        }
        public void logFatal(Exception ex)
        {
            log = GetCurrentLogWithAction();
            string msg = ex.Message;
            if (ex.InnerException != null)
            log.Fatal(msg);
        }



        public void logError(string msg)
        {
            log = GetCurrentLogWithAction();
            log.Error(msg);
        }
        public void logError(Exception ex)
        {
            log = GetCurrentLogWithAction();
            string msg = ex.Message;
            if (ex.InnerException != null)
                msg += "(detail:" + ex.InnerException.Message + ")";
            log.Error(msg);
        }
        //public JsonResult treeDrag<T>(Guid id, Guid pid)
        //{
        //    T objdb = EntityManager<T>.GetByPk<T>("Id",id);
        //    Type type = typeof(T);
        //    object obj = Activator.CreateInstance(type);
        //    PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //    foreach (PropertyInfo item in props)
        //    {
        //        if (item.Name == "PId")
        //        {
        //            item.SetValue(obj, Convert.ChangeType(item.Name, item.PropertyType), null);
        //        }
        //    }
        //    EntityManager<T>.Modify<T>(objdb);

        //    return null;
        //}
        protected ActionResult ReturnFile(string saveToPath, string downFileName)
        {
            LogHelper.Info("ReturnFile", saveToPath + " down(ws) to:" + downFileName + "浏览器：" + HttpContext.Request.Browser.Browser);
        
            if (HttpContext.Request.Browser.Browser == "InternetExplorer" || HttpContext.Request.Browser.Browser == "IE")
            {
                return File(saveToPath, "application/octet-stream; charset=utf-8", Url.Encode(downFileName));
            }
            else if (HttpContext.Request.Browser.Browser == "Chrome")
            {
                //return File(saveToPath, "application/octet-stream; charset=utf-8", downFileName);
                return File(saveToPath, "application/msexcel; charset=utf-8", downFileName);
            }
            else
            {
                return File(saveToPath, "application/octet-stream; charset=utf-8", HttpContext.Request.Browser.Browser + downFileName);
            }
        }
    }
}
