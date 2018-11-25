using System.Web;
using System.Web.Mvc;
using QyTech.Core.ExController;
using QyExpress.Dao;
using QyTech.Core.BLL;
using QyTech.Core;
using QyTech.Core.Common;
using QyTech.Json;
using System.Collections;
using System.Collections.Generic;
using log4net;

namespace QyExpress.Controllers
{
    public class HomeController : Controller
    {
        //protected static ILog log = log4net.LogManager.GetLogger("HomCeontroller");

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return Redirect(Url.Content("~/index.html"));
            //return Redirect(LoginUser.WebUrl);
        }

        public ActionResult Login()
        {
            return View();
        }


    }
}
