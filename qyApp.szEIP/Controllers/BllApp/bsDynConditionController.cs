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
using QyTech.Core.ExController.Bll;
using Dao.QyBllApp;

namespace QyExpress.Controllers.BllApp
{
    public class bsDynConditionController : BllAppController
    {
        public override string getTreeDefault(string sessionid, string where, string orderby)
        {
            if (LoginUser.bsO_Id.ToString().ToUpper() == "C6D2FF6A-10AC-4A42-B228-2EB8584EFB98")
                where = "RoleName is not null";
            else
                where = "RoleName = '所有' or RoleName = '" + LoginUser.bsO_Name + "'";

            List<bsDynCondition> objs;
            
            objs = EManager_App.GetListNoPaging<bsDynCondition>(where, "Id");

            var treelist = new List<qytvNode>();
            qytvNode treenode = new qytvNode();

            foreach (var cc in objs)
            {
                treenode = new qytvNode();
                treenode.id = cc.Id.ToString();
                treenode.name = cc.condName;
                treenode.pId = cc.PId.ToString();
                treenode.type =cc.compType;
                treenode.exInfo = cc.compitems ;
                treenode.exInfo2 = cc.Sql;
                treenode.addBtnFlag = false;
                treenode.removeBtnFlag = false;
                treenode.editBtnFlag = false;


                //if (objs.Count < 50)
                    //treenode.openFlag = true;
                treelist.Add(treenode);
            }
            return jsonMsgHelper.Create(0, treelist, "", treenode.GetType(), null);
        }


        /// <summary>
        /// 根据动态条件表id获取对应的地块编号逗号分隔列表
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetDkbhsByDynSelect(string sessionid,string where)//string id,string selitem)
        {
            //从where中获取id和selitem

            Dictionary<string, string> kvs = kvWhere2Dic(where);

            bsDynCondition dc = EManager_App.GetByPk<bsDynCondition>("Id", int.Parse(kvs["id"]));

            string cond = Get企业范围WhereCondition(dc, kvs["selitem"]);


            List<企业范围> objs = EManager_App.GetListNoPaging<企业范围>(cond, "");
            string dkbhs = "";

            foreach (企业范围 obj in objs)
            {
                if (obj.DKBH != null && obj.DKBH != "")
                {
                    dkbhs += "," + obj.DKBH;
                }
            }
            if (dkbhs.Length > 0)
                dkbhs = dkbhs.Substring(1);
            return jsonMsgHelper.Create(0, dkbhs, "");

        }

        #region 逻辑数据获取

        private string Get企业范围WhereCondition(bsDynCondition dc, string item)
        {
            string Conditions = GetItemCondition(dc, item);

            if (Conditions.Length > 5)
            {
                Conditions = Conditions.Substring(4);
                Conditions = "YDQYMC in (select 纳税人名称 from bsLtdInfo where" + Conditions + ")";
            }

            return Conditions;
        }



     
        /// <summary>
        /// 针对ltdbase获取符合条件的记录
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="itemtext"></param>
        /// <returns></returns>
        private string GetItemCondition(bsDynCondition cond, string itemtext)
        {
            string Conditions = "";
            string itemname = cond.condName;

            string[] sqls = cond.Sql.Split(new char[] { '|' });
            int sqlindex = 0;
            if (cond.compType == "textbox")
            {
                Conditions += " and " + cond.Sql.Replace("@@@@", itemtext);
            }
            else if (cond.compType == "combox")
            {
                foreach (string c in cond.compitems.Split(new char[] { ',' }))
                {
                    if (c == itemtext)
                        break;
                    sqlindex++;
                }


                if (sqlindex + 1 > sqls.Length)
                    sqlindex = sqls.Length - 1;//没有对应的就用最后一个


                if ("技术中心获批级别" == itemname || itemtext == "全部")
                    Conditions += " and " + sqls[sqlindex];
                else if ("智能车间,清洁生产".Contains(itemname))
                    Conditions += " and " + sqls[sqlindex].Replace("@@@@", itemtext.Substring(0, 4));

                else if ("地标性企业".Contains(itemname))
                {
                    if (sqlindex == 1)
                        Conditions += " and " + sqls[sqlindex].Replace("@@@@", "1");
                    else
                        Conditions += " and " + sqls[sqlindex].Replace("@@@@", itemtext.Substring(1, itemtext.Length - 3));
                }
                else if ("营业额，税收，土地盘活计划".Contains(itemname))
                    Conditions += " and " + sqls[sqlindex].Replace("@@@@", itemtext.Substring(0, itemtext.Length - 3));

                else
                    Conditions += " and " + sqls[sqlindex].Replace("@@@@", itemtext);


            }


            if (itemtext.Trim() == "")
                return "";

            return Conditions;
        }

        #endregion
    }
}
