using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QyTech.Core.ExController;
using QyTech.Auth.Dao;
using QyTech.Core.BLL;
using QyTech.Core;
using QyTech.Core.Common;
using QyTech.Json;

namespace QyTech.Auth.Controllers.api
{
    public class bsNavigationController : AuthController
    {

        public class Navi
        {
            public string title { get; set; }
            public string route { get; set; }
            public string icon { get; set; }
            public List<Navi> items { get; set; }
        }


        public override string GetAll(string fields,string where, string orderby)
        {
            //object[] abc=new object[] { 1, null };
            //List<bsNavigation> aa = EManager.GetAllByStorProcedure<bsNavigation>("splyTestRetValue", ref abc);


            //应该与用户账号相关
            List<Navi> navis = new List<Navi>();
            List<bsNavigation> funs = EManager.GetListNoPaging<bsNavigation>("NaviStatus='正常'", "NaviNo");
          
            List<bsNavigation> fun_start = funs.Where(p => p.pId == null).OrderBy(p => p.NaviNo).ToList<bsNavigation>();
            foreach (bsNavigation pN in fun_start)
            {
                Navi gN = new Navi();
                gN.title = pN.NaviName;
                gN.icon = pN.Icon;
                gN.route = pN.Route;
                List<bsNavigation> items = funs.Where(p => p.pId == pN.bsN_Id).OrderBy(p => p.NaviNo).ToList<bsNavigation>();
                if (items.Count > 0)
                {
                    gN.items=new List<Navi>();
                    gN.items.AddRange(GetSubNavis(funs, items, pN));
                }
                navis.Add(gN);
            }

            return jsonMsgHelper.Create(0, navis, "",navis[0].GetType(),null);
        }

        private List<Navi> GetSubNavis(List<bsNavigation> allitems, List<bsNavigation> items, bsNavigation pN)
        {
            List<Navi> navis = new List<Navi>();
       
            foreach (bsNavigation item in items)
            {
                Navi gN = new Navi();
                gN.title = item.NaviName;
                gN.icon = item.Icon;
                gN.route = item.Route;

                List<bsNavigation> subitems = allitems.Where(p => p.pId == item.bsN_Id).OrderBy(p => p.NaviNo).ToList<bsNavigation>();
                if (subitems.Count > 0)
                {
                    gN.items = new List<Navi>();
                    gN.items.AddRange(GetSubNavis(allitems, subitems, item));
                }
                
                navis.Add(gN);
            }

            return navis;

        }


        public override string TreeDis(string where, string orderby)
        {
            if (orderby != null)
            {
                return base.GetTree(where, orderby, "bsN_Id", "NaviName", "pId", "NaviType");
            }
            else
            {
                return base.GetTree(where, "NaviNo", "bsN_Id", "NaviName", "pId", "NaviType");
            
            }
        }
    }
}
