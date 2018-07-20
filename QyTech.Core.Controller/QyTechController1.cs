using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using log4net;
using QyTech.Core.Helpers;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using QyTech.Core;
using System.Data.Objects;
using QyTech.Core.BLL;
using QyTech.Json;
using QyTech.Core.Common;
using System.Collections;
using QyTech.Core.ExController.Bll;

using QyTech.Auth.Dao;

namespace QyTech.Core.ExController
{
    public partial class QyTechController
    {

        /// <summary>
        /// 校正where条件
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        protected string AjustWhereSql(string where)
        {
            string wheresql = "";
          
            //List<keyVal> wherelist = JsonHelper.DeserializeJsonToList<keyVal>(where);
            List<QyTech.Json.keyVal> wherelist = JsonHelper.DeserializeJsonToKeyValList(where);


            Dictionary<string, bsFunField> bsFFs = getFunFields(bsFC_Id);
            foreach (QyTech.Json.keyVal kv in wherelist)
            {
                //获取该字段的对象，根据类型 得到处理的方式
                if (bsFFs.ContainsKey(kv.key))
                {
                    if (bsFFs[kv.key].FType == "varchar")
                    {
                        wheresql += " and " + kv.key + " like '%" + kv.val + "%'";
                    }
                    else if (bsFFs[kv.key].FType == "uniqueidentifier")
                    {
                        wheresql += " and " + kv.key + "='" + kv.val + "'";
                    }
                    else if (bsFFs[kv.key].FType == "int"
                        || bsFFs[kv.key].FType == "decimal"
                        )
                    {
                        if (kv.val.IndexOf("|") == -1)
                        {
                            wheresql += " and " + kv.key + "=" + kv.val;
                        }
                        else
                        {
                            string[] str = kv.val.Split(new char[] { '|' });

                            wheresql += " and " + kv.key + ">=" + str[0] + " and " + kv.key + "<=" + str[1];

                        }
                    }
                    else if (bsFFs[kv.key].FType == "datetime")
                    {
                        if (kv.val.IndexOf("|") == -1)
                        {
                            wheresql += " and " + kv.key + "='" + kv.val+"'";
                        }
                        else
                        {
                            string[] str = kv.val.Split(new char[] { '|' });

                            wheresql += " and " + kv.key + ">='" + str[0] + "' and " + kv.key + "<='" + str[1]+"'";

                        }
                    }
                    else
                    {
                        wheresql += " and " + kv.key + " like '%" + kv.val + "%'";
                    }

                }
            }
            if (wheresql.Length > 0)
            {
                wheresql = wheresql.Substring(4);
            }
            return wheresql;
        }
         
        /// <summary>
        /// 获取数据列表，字段区分大小写
        /// </summary>
        /// <param name="fields">要显示的字段，为空则表示显示全部</param>
        /// <param name="where">keyvalue方式的json串</param>
        /// <param name="orderby">不含order by的完整排序sql</param>
        /// <returns>json数据</returns>
        //[HttpPost]
        public virtual string GetAll(string fields="", string where="", string orderby="")
        {
            //ObjectClassFullName
            if (where.Length>0)
                where= AjustWhereSql(where);
          
            try
            {
                object objs = dataManager.GetObjects(where, orderby);
                Type type = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));

                if (type != null && objs != null)
                {
                    if (fields == null || fields.Equals(""))
                    {
                        List<DataItemSet> dispitems = null;
                        try
                        {
                            dispitems = bllbsUserFields.GetUserNeedDispListItemDesps(EManagerApp_, LoginUser.bsU_Id, bsFC);
                        }
                        catch { }
                        dispitems = null;
                        return jsonMsgHelper.Create(0, objs, "", type, dispitems);
                    }
                    else
                    {
                        return jsonMsgHelper.CreateWithStrField(0, objs, "", type, fields);
                    }
                }
                else
                {
                    return jsonMsgHelper.CreateWithStrField(1, objs, "", type, fields);
                }

            }
            catch (Exception ex)
            {
                log.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }


        /// <summary>
        /// 通过存储过程来获取所有数据
        /// </summary>
        /// <param name="spname">存储过程名</param>
        /// <param name="fields">要显示的字段，为空则表示显示全部</param>
        /// <param name="where">不含where的完整sql条件</param>
        /// <param name="orderby">不含order by的完整排序sql</param>
        /// <returns>json数据</returns>
        public virtual string GetAllByProcedure(string spname, string fields, string where, string orderby)
        {
            where = AjustWhereSql(where);
            
            if (spname == null || spname.Equals(""))
            {
                log.Error("GetAllByProcedure:必须输入参数：存储过程名" );
                return jsonMsgHelper.Create(1, "", "必须输入参数：存储过程名");
            }
            try
            {
                dataManager = new DataManager(EManagerApp_, objClassFullName);

                Type type = Type.GetType(objClassFullName);


                string[] strs = where.Split(new char[] { ',' });
                object[] paras = new object[strs.Length];
                int index = 0;
                foreach (string str in strs)
                {
                    paras[index++] = str;
                }

                object objs = dataManager.GetObjects(spname, paras, orderby);

                if (fields == null || fields.Equals(""))
                {
                    List<DataItemSet> dispitems = bllbsUserFields.GetUserNeedDispListItemDesps(EManagerApp_, LoginUser.bsU_Id, bsFC);
                    return jsonMsgHelper.Create(0, objs, "", type, dispitems);
                }
                else
                {
                    return jsonMsgHelper.CreateWithStrField(0, objs, "", type, fields);
                }
            }
            catch (Exception ex)
            {
                log.Error("GetAllByProcedure:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }


        /// <summary>
        /// 非存储过程获取分页数据
        /// </summary>
        /// <param name="fields">要显示的字段，为空则表示显示全部</param>
        /// <param name="where">不含where的完整sql条件</param>
        /// <param name="orderby">不含order by的完整排序sql</param>
        /// <returns>json数据</returns>
        public virtual string GetAllWithPaging(string fields="", string where="", string orderby="", int currentPage = 1, int pageSize = 20)
        {
            try
            {
               
                if (where!="")
                    where = AjustWhereSql(where);
                
                int totalCount = 100;
                int totalPage = (int)Math.Ceiling(1.0 * totalCount / pageSize);

                object objs = dataManager.GetObjectsWithPaging(where, orderby, out totalCount, out totalPage, currentPage, pageSize);

                Type type = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));

                if (fields == null || fields.Equals(""))
                {
                    List<DataItemSet> dispitems = bllbsUserFields.GetUserNeedDispListItemDesps(EManagerApp_, LoginUser.bsU_Id, bsFC);
                    return jsonMsgHelper.CreatePage(0, objs, currentPage, pageSize, totalCount, totalPage, "", type, dispitems);
                }
                else
                {
                    return jsonMsgHelper.CreatePageWithStrField(0, objs, currentPage, pageSize, totalCount, totalPage, "", type, fields);
                }
            }
            catch (Exception ex)
            {
                log.Error("GetAllWithPaging:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }

        /// <summary>
        /// 通用存储过程获取分页数据
        /// </summary>
        /// <param name="fields">输出字段列表</param>
        /// <param name="where">不含where的自定义条件</param>
        /// <param name="orderbyfield">单排序字段</param>
        /// <param name="ascordesc">升序(asc)或降序(desc)</param>
        /// <param name="currentPage">返回页</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public virtual string GetAllWithPagingByCommonProcedure(string fields, string where, string orderbyfield, string ascordesc, int currentPage = 1, int pageSize = 20)
        {
            try
            {
                where = AjustWhereSql(where);
                
                int totalCount = 100;
                int totalPage = (int)Math.Ceiling(1.0 * totalCount / pageSize);
                object objs = dataManager.GetObjectsWithCommonStorePaging(objClassName, where, orderbyfield, ascordesc.ToLower() == "asc" ? true : false, out totalCount, out totalPage, currentPage, pageSize);


                Type type = Type.GetType(objClassFullName);
                if (fields == null || fields.Equals(""))
                {
                    List<DataItemSet> dispitems = bllbsUserFields.GetUserNeedDispListItemDesps(EManagerApp_, LoginUser.bsU_Id, bsFC);
                    return jsonMsgHelper.CreatePage(0, objs, currentPage, pageSize, totalCount, totalPage, "", type, dispitems);
                }
                else
                {
                    return jsonMsgHelper.CreatePageWithStrField(0, objs, currentPage, pageSize, totalCount, totalPage, "", type, fields);
                }
            }
            catch (Exception ex)
            {
                log.Error("GetAllWithPagingByCommonProcedure:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }


        /// <summary>
        /// 自定义存储过程获取分页数据
        /// </summary>
        /// <param name="spname">存储过程名</param>
        /// <param name="fields">输出字段列表</param>
        /// <param name="where">不含where的自定义条件</param>
        /// <param name="orderby">不含orderby的排序sql</param>
        /// <param name="currentPage">返回页</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public virtual string GetAllWithPagingBySelfProcedure(string spname, string fields, string where, string orderby, int currentPage = 1, int pageSize = 20)
        {
            try
            {
                where = AjustWhereSql(where);
                
                int totalCount = 0;
                int totalPage = (int)Math.Ceiling(1.0 * totalCount / pageSize);

                string[] strs = where.Split(new char[] { ',' });
                object[] paras = new object[strs.Length];
                int index = 0;
                foreach (string str in strs)
                {
                    paras[index++] = str;
                }
                object objs = dataManager.GetObjectsWithStorePaging(spname, paras, orderby, out totalCount, out totalPage, currentPage, pageSize);


                Type type = Type.GetType(objClassFullName);
                if (fields == null || fields.Equals(""))
                {
                    List<DataItemSet> dispitems = bllbsUserFields.GetUserNeedDispListItemDesps(EManagerApp_, LoginUser.bsU_Id, bsFC);
                    return jsonMsgHelper.CreatePage(0, objs, currentPage, pageSize, totalCount, totalPage, "", type, dispitems);
                }
                else
                {
                    return jsonMsgHelper.CreatePageWithStrField(0, objs, currentPage, pageSize, totalCount, totalPage, "", type, fields);
                }
            }
            catch (Exception ex)
            {
                log.Error("GetAllWithPagingBySelfProcedure:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }


        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <param name="idValue">对应唯一标识字段值</param>
        /// <returns>json格式对象</returns>
        public virtual string GetOne(string idValue)
        {
            if (idValue == null || idValue.Equals(""))
            {
                return jsonMsgHelper.Create(1, "", "参数必须输入");
            }
            try
            {
                List<DataItemSet> dispitems = bllbsUserFields.GetUserNeedDispListItemDesps(EManagerApp_, LoginUser.bsU_Id, bsFC);

                object idV = idValue;
                Guid guidV = Guid.Empty;
                if (idValue != null)
                {
                    if (!Guid.TryParse(idValue, out guidV))
                    {
                        try
                        {
                            idV = Convert.ToInt64(idValue);
                        }
                        catch { idV = idValue; }
                    }
                    else
                    {
                        idV = guidV;
                    }
                }
                object dbobj, rowdataobj;
                Type dbtype;
                MethodInfo miObj;

                dbtype = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));
                dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);

                Type typeEm = typeof(EntityManager);
                miObj = typeEm.GetMethod("GetByPk").MakeGenericMethod(dbtype);
                rowdataobj = miObj.Invoke(EManagerApp_, new object[] { bsFC.TPk, idV });

                if (rowdataobj != null)
                    return jsonMsgHelper.Create(0, rowdataobj, "", rowdataobj.GetType(), dispitems);
                else
                    return jsonMsgHelper.Create(1, null, "");
            }
            catch (Exception ex)
            {
                log.Error("GetOne:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }

        }


        /// <summary>
        /// 获取树结构
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public virtual string TreeDis(string where, string orderby)
        {
            where = AjustWhereSql(where);
            return "";
        }


        /// <summary>
        /// ？
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="orderby">排序方式</param>
        /// <param name="idfieldname">主键字段</param>
        /// <param name="namefieldname">名称字段</param>
        /// <param name="pidfieldname">Pid字段</param>
        /// <param name="typefieldname">节点类型字段</param>
        /// <returns></returns>
        protected string GetTree(string where, string orderby,
          string idfieldname, string namefieldname, string pidfieldname,string typefieldname)
        {

            try
            {
                where = AjustWhereSql(where);
                
                
                System.Reflection.PropertyInfo propertyInfo;
                object val;
                Type type = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));

                object objs = dataManager.GetObjects(where, orderby);

                var treelist = new List<TreeNode>();
                TreeNode treenode = new TreeNode();
                foreach (var cc in (objs as IEnumerable))
                {
                    treenode = new TreeNode();

                    propertyInfo = type.GetProperty(idfieldname);
                    val = propertyInfo.GetValue(cc, null);
                    treenode.id = val.ToString();

                    propertyInfo = type.GetProperty(namefieldname);
                    val = propertyInfo.GetValue(cc, null);
                    treenode.name = val.ToString();


                    propertyInfo = type.GetProperty(pidfieldname);
                    val = propertyInfo.GetValue(cc, null);
                    if (val != null)
                        treenode.pId = val.ToString();

                    if (typefieldname == null || typefieldname.Equals(""))
                        typefieldname = namefieldname;
                    propertyInfo = type.GetProperty(typefieldname);
                    val = propertyInfo.GetValue(cc, null);
                    treenode.type = val.ToString();

                    treenode.addBtnFlag = false;
                    treenode.removeBtnFlag = false;
                    treenode.editBtnFlag = false;

                    treelist.Add(treenode);
                }
                return jsonMsgHelper.Create(0, treelist, "", treenode.GetType(), null);
            }
            catch (Exception ex)
            {
                log.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }

        }
        
        public virtual string Save(string strjson)
        {
            if (strjson == null || strjson.Equals(""))
            {
                return jsonMsgHelper.Create(1, "", "参数为空，无法增加");
            }

            try
            {
                //判断strjson是否有主键及默认值，没有则增加,目前标识只考虑了Guid
                string Key_TPk = "\"" + bsFC.TPk + "\":";
                string TpkDefaultIn = Key_TPk + "\"\"";

                if (!strjson.Contains(TpkDefaultIn))
                {
                    return Edit(strjson);
                }
                return Add(strjson);

              
            }
            catch (Exception ex)
            {
                log.Error("Save:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }

        /// <summary>
        /// 新增json对应的对象
        /// </summary>
        /// <param name="strjson">json数据</param>
        /// <returns>新增结果json</returns>
        public virtual string Add(string strjson)
        {

            if (strjson == null || strjson.Equals(""))
            {
                return jsonMsgHelper.Create(1, "", "参数为空，无法增加");
            }

            object dbobj, rowdataobj;
            Type dbtype;
            MethodInfo miObj;
            try
            {

                //判断strjson是否有主键及默认值，没有则增加,目前标识只考虑了Guid
                string Key_TPk = "\"" + bsFC.TPk + "\":";
                string TpkDefaultIn = Key_TPk + "\"\"";
                string TpkDefault =Key_TPk + "\"00000000-0000-0000-0000-000000000000\"";

                if (strjson.Contains(Key_TPk))
                {
                    if (strjson.Contains(TpkDefaultIn))
                    {
                        strjson = strjson.Replace(TpkDefaultIn, TpkDefault);
                    }
                }
                else
                {
                    strjson = "{" + TpkDefault + "," + strjson.Substring(1);
                }

                dbtype = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));
                dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);

                Type typeEm = typeof(JsonHelper);
                miObj = typeEm.GetMethod("DeserializeJsonToObject").MakeGenericMethod(dbtype);
                rowdataobj = miObj.Invoke(null, new object[] { strjson });

                //生成外键 not finished
                Type type = rowdataobj.GetType(); //获取类型
                System.Reflection.PropertyInfo propertyInfo = type.GetProperty(bsFC.TPk);
                propertyInfo.SetValue(rowdataobj, PkCreateHelper.GetQySortGuidPk(), null); //给对应属性赋值,应使用设置的默认值函数

                typeEm = typeof(EntityManager);
                miObj = typeEm.GetMethod("Add").MakeGenericMethod(dbtype);
                rowdataobj = miObj.Invoke(EManagerApp_, new object[] { rowdataobj });

                if (rowdataobj.ToString() == "")
                    return jsonMsgHelper.Create(0, "", "");
                else
                    return jsonMsgHelper.Create(1, "", rowdataobj.ToString());
            }
            catch (Exception ex)
            {
                log.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }

        /// <summary>
        /// 修改json对应的对象
        /// </summary>
        /// <param name="strjson">json数据</param>
        /// <returns>修改结果json</returns>
        public virtual string Edit(string strjson)
        {
            if (strjson == null || strjson.Equals(""))
            {
                return jsonMsgHelper.Create(1, "", "参数为空，无法修改");
            }
            object dbobj, rowdataobj;
            Type dbtype;
            MethodInfo miObj;

            try
            {
                dbtype = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));
                dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);

                Type typeEm = typeof(JsonHelper);
                miObj = typeEm.GetMethod("DeserializeJsonToObject").MakeGenericMethod(dbtype);
                rowdataobj = miObj.Invoke(null, new object[] { strjson });


                //还需要对应赋值，否则编辑中的外键可能没有值？？？？？not finished
                ////   object dbobj, rowdataobj;
                ////Type dbtype;
                ////MethodInfo miObj;
                //1.获取rowdataobj主键值
                System.Reflection.PropertyInfo propertyInfo = dbtype.GetProperty(bsFC.TPk); //获取指定名称的属性
                object idV = propertyInfo.GetValue(rowdataobj, null);

                //2. 获取对应的数据库对象rowdbobj
                typeEm = typeof(EntityManager);
                miObj = typeEm.GetMethod("GetByPk").MakeGenericMethod(dbtype);
                object rowdbobj = miObj.Invoke(EManagerApp_, new object[] { bsFC.TPk, idV });

                //3。获取在界面中编辑的字段列表
                List<listWriteDataItemSet> items = bllbsUserFields.GetEditListDispItemDesps(EManager_, LoginUser.bsU_Id, bsFC_Id);

                //4 按照上面的字段列表将rowdataobj中的值复制给rowdbobj，
                foreach (listWriteDataItemSet item in items)
                {
                    if (item.FEditable)
                    {
                        propertyInfo = dbtype.GetProperty(item.FName);
                        object svalue = propertyInfo.GetValue(rowdataobj, null);

                        propertyInfo.SetValue(rowdbobj, svalue, null); //给对应属性赋值
                    }
                }
                //5。保存rowdbobj
                miObj = typeEm.GetMethod("Modify").MakeGenericMethod(dbtype);
                rowdataobj = miObj.Invoke(EManagerApp_, new object[] { rowdbobj });

                if (rowdataobj.ToString() == "")
                    return jsonMsgHelper.Create(0, "", "");
                else
                    return jsonMsgHelper.Create(1, "", rowdataobj.ToString());
            }
             catch (Exception ex)
            {
                log.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }


 
        /// <summary>
        /// 删除idValue标识对应的对象
        /// </summary>
        /// <param name="idValue"></param>
        /// <returns></returns>
        public virtual string Delete(string idValue)
        {
            if (idValue == null || idValue.Equals(""))
            {
                return jsonMsgHelper.Create(1, "", "参数为空，无法删除");
            }

            object dbobj, rowdataobj;
            Type dbtype;
            MethodInfo miObj;
            try
            {
                dbtype = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));
                dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);

                Type typeEm = typeof(EntityManager);
                miObj = typeEm.GetMethod("DeleteById").MakeGenericMethod(dbtype);
                rowdataobj = miObj.Invoke(EManagerApp_, new object[] { bsFC.TPk, idValue });

                if (rowdataobj.ToString() == "")
                    return jsonMsgHelper.Create(0, "", "");
                else
                    return jsonMsgHelper.Create(1, "", rowdataobj.ToString());
            }
            catch (Exception ex)
            {
                log.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }

        ///// <summary>
        ///// 删除json对应的对象
        ///// </summary>
        ///// <param name="strjson">json数据</param>
        ///// <returns>删除结果json</returns>
        //public virtual string DeleteByObj(string strjson)
        //{
        //    object dbobj, rowdataobj;
        //    Type dbtype;
        //    MethodInfo miObj;

        //    dbtype = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));
        //    dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);

        //    Type typeEm = typeof(JsonHelper);
        //    miObj = typeEm.GetMethod("DeserializeJsonToObject").MakeGenericMethod(dbtype);
        //    rowdataobj = miObj.Invoke(null, new object[] { strjson });

        //    typeEm = typeof(EntityManager);
        //    miObj = typeEm.GetMethod("Delete").MakeGenericMethod(dbtype);
        //    rowdataobj = miObj.Invoke(EManager_, new object[] { rowdataobj });

        //    if (rowdataobj.ToString() == "")
        //        return jsonMsgHelper.Create(0, "", "");
        //    else
        //        return jsonMsgHelper.Create(1, "", rowdataobj.ToString());
        //}

    }
}
