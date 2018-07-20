using System.Web;
using System.Web.Mvc;
using QyTech.Core.ExController;
using QyTech.Auth.Dao;
using QyTech.Core.BLL;
using QyTech.Core;
using QyTech.Core.Common;
using QyTech.Json;
using System.Collections;
using System.Collections.Generic;
using log4net;

namespace QyTech.Auth.Controllers
{
    public class HomCeontroller : AuthController
    {
        //protected static ILog log = log4net.LogManager.GetLogger("HomCeontroller");

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return Redirect(Url.Content("~/index.html"));
            //return Redirect(LoginUser.WebUrl);
        }



        //public ActionResult Login()
        //{
        //    return Redirect(Url.Content("~/Login.html"));
        //}


    }
}
