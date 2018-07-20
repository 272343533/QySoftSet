using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QyTech.Auth.Controllers.Wjkfq
{
    public class WjkfqDefaultController : QyTech.DbFac.WjkfqController
    {
        //
        // GET: /Default1/

        public ActionResult Index()
        {
            return View();
        }

    }
}
