using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QyTech.Core.ExController;
using QyExpress.Dao;
using QyTech.Core.BLL;
using QyTech.Core;
using QyTech.Core.Common;
using QyTech.Json;
using QyTech.BLL;


namespace QyExpress.Controllers.api
{
    public class bsNavigationController : apiController
    {

        //public class Navi
        //{
        //    public string title { get; set; }
        //    public string route { get; set; }
        //    public string icon { get; set; }
        //    public List<Navi> items { get; set; }
        //}

        /// <summary>
        ///  配置导航使用  重写了getall,需要吗？//2018-10-18
        ///  2019-03-19
        ///  配置导航应该与应用相关，可以获取所有的，目前我们每个项目用一套数据库，可以包含多个客户。
        ///  配置导航后，通过给客户导航权限，因此目前导航本身与用户无关，可以考虑为按模块给用户即可。
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public override string GetAll(string sessionid, string fields,string where="", string orderby="")
        {
            //1.应该与用户账号相关，默认的getall没有考虑账号，

            //2.应该对有些表，根据权限获取数据 zhwsun 2018-10-08 根据用户权限在结合where结果进行过滤
            //应该没用，直接调用即可，2019-03-16
            //ObjectClassFullName = ObjectClassFullName.Replace(StrForReplaceObject, objNameSpace + "." + "bsNavigation");

            return base.GetAll(sessionid,fields, where, orderby);
        }

        /// <summary>
        /// 重写的gettree（获取用户导航功能实际使用的是bsuser/GetNavis）
        /// 此处给出的树结构应该是给c/s端用的？2019-03-19
        /// </summary>
        /// <param name="where">不需要</param>
        /// <param name="orderby">不需要</param>
        /// <returns></returns>
        public string getUserTree(string sessionid, string where="", string orderby="")
        {
            bllbsNavigation bobj = new bllbsNavigation();
            List<qytvNode> nodes= bobj.GetNaviNodes(EManager,LoginUser);
            string json = jsonMsgHelper.Create(0, nodes, "");
            return json;
        }

       
    }
}
