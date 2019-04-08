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

using QyExpress.Dao;
using QyTech.ExcelOper;

namespace QyTech.Core.ExController
{


    /// 给前端使用的接口

    public partial class QyTechController
    {

        protected virtual string CheckLogical(string idValue)
        {
            return "";
        }

        /// <summary>
        /// 新增json对应的对象
        /// </summary>
        /// <param name="strjson">json数据</param>
        /// <returns>新增结果json</returns>
        public virtual string Add(string sessionid, string strjson)
        {

            if (strjson == null || strjson.Equals(""))
            {
                return jsonMsgHelper.Create(1, null, "参数为空，无法增加");
            }

            object dbobj, rowdataobj;
            Type dbtype;
            MethodInfo miObj;
            try
            {
                //生成主键
                AddGuidTPk(ref strjson);

                dbtype = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));
                dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);

                Type typeEm = typeof(JsonHelper);
                miObj = typeEm.GetMethod("DeserializeJsonToObject").MakeGenericMethod(dbtype);
                rowdataobj = miObj.Invoke(null, new object[] { strjson });
                ////增加日志
                //AddLogTable("增加", bsT.TName, bsT.Desp, "");

                //typeEm = typeof(EntityManager);
                //miObj = typeEm.GetMethod("Add").MakeGenericMethod(dbtype);
                //rowdataobj = miObj.Invoke(EManagerApp_, new object[] { rowdataobj });
                string ret = DaoAdd(dbtype, rowdataobj);

                if (ret.ToString() == "")
                    return jsonMsgHelper.Create(0, "", "新增成功！");
                else
                    return jsonMsgHelper.Create(1, "",new Exception(ret));
            }
            catch (Exception ex)
            {
                LogHelper.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex);
            }
        }



        public virtual string Adds(string sessionid, string strjson)
        {
            if (strjson == null || strjson.Equals(""))
            {
                return jsonMsgHelper.Create(1, null, "参数为空，无法增加");
            }
            object dbobj, rowdataobj;
            Type dbtype;
            try
            {
                //增加主建 2018-11-27  不合适，应为多个记录的话每个主键应该不同
                //AddGuidTPk(ref strjson);

                dbtype = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));
                dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);

                Type typeEm = typeof(JsonHelper);
                MethodInfo miObj = typeEm.GetMethod("DeserializeJsonToList").MakeGenericMethod(dbtype);
                //此时是一个List
                rowdataobj = miObj.Invoke(null, new object[] { strjson });

                //typeEm = typeof(EntityManager);
                //miObj = typeEm.GetMethod("Adds").MakeGenericMethod(dbtype);
                //rowdataobj = miObj.Invoke(EManagerApp_, new object[] { rowdataobj });
                string ret= DaoAdds(dbtype, rowdataobj as List<object>);
                if (ret.ToString() == "")
                    return jsonMsgHelper.Create(0, "", "新增成功！");
                else
                    return jsonMsgHelper.Create(1, "",new Exception(ret));
            }
            catch (Exception ex)
            {
                LogHelper.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex);
            }
        }

        /// <summary>
        /// 修改json对应的对象
        /// </summary>
        /// <param name="strjson">json数据</param>
        /// <returns>修改结果json</returns>
        public virtual string EditThenCheckLogical(string sessionid, string strjson,string idValue)
        {
            if (strjson == null || strjson.Equals(""))
            {
                return jsonMsgHelper.Create(1, "", "参数为空，无法修改");
            }

           
            strjson = strjson.Replace(" ", "");
            try
            {
                Type dbtype = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));

                Type typeJh = typeof(JsonHelper);
                MethodInfo miObj = typeJh.GetMethod("DeserializeJsonToObject").MakeGenericMethod(dbtype);
                object rowdataobj = miObj.Invoke(null, new object[] { strjson });

                string ret= DaoModify(dbtype, rowdataobj);

                string retLogical = CheckLogical(idValue);

                if (ret == "")
                {
                    if (retLogical == null|| retLogical.Trim()=="")
                        return jsonMsgHelper.Create(0, "", "保存成功！");
                    else
                        return jsonMsgHelper.Create(0, "",  retLogical );
                }
                else
                    return jsonMsgHelper.Create(1, "", new Exception(ret));
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return jsonMsgHelper.Create(1, "", ex);
            }
        }
        /// <summary>
        /// 修改json对应的对象
        /// </summary>
        /// <param name="strjson">json数据</param>
        /// <returns>修改结果json</returns>
        public virtual string Edit(string sessionid, string strjson)
        {
            if (strjson == null || strjson.Equals(""))
            {
                return jsonMsgHelper.Create(1, "", "参数为空，无法修改");
            }
            strjson = strjson.Replace(" ", "");
            try
            {
                Type dbtype = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));

                Type typeJh = typeof(JsonHelper);
                MethodInfo miObj = typeJh.GetMethod("DeserializeJsonToObject").MakeGenericMethod(dbtype);
                object rowdataobj = miObj.Invoke(null, new object[] { strjson });

                string ret = DaoModify(dbtype, rowdataobj);

                if (ret == "")
                {
                   return jsonMsgHelper.Create(0, "", "保存成功！");
                }
                else
                    return jsonMsgHelper.Create(1, "", new Exception(ret));
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return jsonMsgHelper.Create(1, "", ex);
            }
        }

        /// <summary>
        ///  根据默认主键编辑数据，按照编辑的数据项进行修改
        /// </summary>
        /// <param name="keyvalues">主键及编辑的数据，json数组格式，编辑项的keyvalue列表，非数值型不需加引号，如：[{Name: 张三},{Age: 18}]</param>
        /// <returns>修改结果json</returns>
        public virtual string EditbyKeyValuesDefaultTpk(string sessionid, string keyvalues)
        {
            //增加日志
            AddLogTable("EditbyKeyValuesDefaultTpk", bsT.TName, bsT.Desp, keyvalues);

            if (keyvalues == null || keyvalues.Equals(""))
                return jsonMsgHelper.Create(1, "", "参数为空，无法修改");
            //进一步修改为keyvalues
            Dictionary<string, string> dicKV = json2dicKV(keyvalues);
            try
            {
                //2.获取rowdataobj主键值
                Dictionary<string, bsField> items = getbsFields(bsT.bsT_Id);
                object idV = GetRightFValue(items[bsT.TPk], dicKV[bsT.TPk]);//类型

                return EditbyKeyValues(bsT.TPk, dicKV[bsT.TPk], dicKV);
            }
            catch (Exception ex)
            {
                LogHelper.Error("EditbyKeyValues:" + ex);
                return jsonMsgHelper.Create(1, "", ex);
            }
        }

      
        /// <summary>
        /// 根据指定字段值编辑数据
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="FName"></param>
        /// <param name="FValue"></param>
        /// <param name="keyvalues">需要编辑的数据，json数组格式，编辑项的keyvalue列表，非数值型不需加引号，如：[{Name: 张三},{Age: 18}]</param>
        /// <returns></returns>
        public virtual string EditbyKeyValues(string sessionid, string FName, string FValue, string keyvalues)
        {
            //增加日志
            AddLogTable("EditbyKeyValues", bsT.TName, bsT.Desp, FName+" "+ FValue + " "+keyvalues);

            Dictionary<string, string> dicKV = new Dictionary<string, string>();
            keyvalues = keyvalues.Replace(" ", "");
            try
            {
                keyvalues = keyvalues.Replace("{", "{\"").Replace("}", "\"}").Replace(":", "\":\"");
        
                List<QyTech.Json.keyVal> kvs = JsonHelper.DeserializeJsonToKeyValList(keyvalues);
                //进一步修改为keyvalues
                foreach (keyVal kv in kvs)
                {
                    dicKV.Add(kv.key, kv.val);
                }
                return EditbyKeyValues(FName, FValue, dicKV);
            }
            catch (Exception ex)
            {
                LogHelper.Error("EditbyKeyValues:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex);
            }
        }


      

        /// <summary>
        /// 删除idValue标识对应的对象
        /// </summary>
        /// <param name="idValue"></param>
        /// <returns></returns>
        public virtual string Delete(string sessionid, string idValue)
        {
            AddLogTable("编辑", bsT.TName, bsT.Desp, idValue);
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

                rowdataobj = miObj.Invoke(EManagerApp_, new object[] { bsT.TPk, idValue });

                if (rowdataobj.ToString() == "")
                    return jsonMsgHelper.Create(0, "", "删除成功！");
                else
                    return jsonMsgHelper.Create(1, "", new Exception(rowdataobj.ToString()));
            }
            catch (Exception ex)
            {
                LogHelper.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex);
            }
        }

        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <param name="sqlwhere">sql条件</param>
        /// <returns></returns>
        public virtual string DeleteBysqlwhere(string sessionid, string sqlwhere)
        {
            LogHelper.Error(sqlwhere);
            AddLogTable("编辑", bsT.TName, bsT.Desp, sqlwhere);
            if (sqlwhere == null || sqlwhere.Equals(""))
            {
                return jsonMsgHelper.Create(1, "", "没有删除条件！");
            }

            object dbobj, rowdataobj;
            Type dbtype;
            MethodInfo miObj;
            try
            {
                dbtype = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));
                dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);

                Type typeEm = typeof(EntityManager);
                miObj = typeEm.GetMethod("DeleteBysqlwhere").MakeGenericMethod(dbtype);
                if (bsT.bsD_Name.Contains("QyExpress"))
                {
                    rowdataobj = miObj.Invoke(EManager_, new object[] { sqlwhere });
                }
                else
                {
                    rowdataobj = miObj.Invoke(EManagerApp_, new object[] { sqlwhere });
                }
                if (Convert.ToInt32(rowdataobj)!=-1)
                    return jsonMsgHelper.Create(0, "", "删除成功！");
                else
                    return jsonMsgHelper.Create(1, "", new Exception(rowdataobj.ToString()));
            }
            catch (Exception ex)
            {
                LogHelper.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex);
            }
        }

        public string ExcuteSql(string sessionid, string sql)
        {
            AddLogTable("执行", bsT.TName, bsT.Desp, sql.Substring(0,50));
            if (sql.ToLower().Contains("delete"))
            {
                return jsonMsgHelper.Create(1, "", "参数不合法");
            }
            try
            {
                string  ret = EManagerApp_.ExecuteSql(sql);
                if (ret=="")
                    return jsonMsgHelper.Create(0, "", "执行成功");
                else
                    return jsonMsgHelper.Create(0, "", ret);
            }
            catch (Exception ex)
            {
                return jsonMsgHelper.Create(1, "", ex);
            }
        }


        public string ExcuteStoreProcedure(string sessionid, string spname,string paras)
        {
            AddLogTable("执行", bsT.TName, bsT.Desp, spname+" "+paras);
            if ((spname+paras).ToLower().Contains("delete"))
            {
                return jsonMsgHelper.Create(1, "", "参数不合法");
            }

            try
            {
                int ret = EManagerApp_.ExcuteStoreProcedure(spname, paras);
                return jsonMsgHelper.Create(0, "", "执行成功");
            }
            catch(Exception ex)
            {
                return jsonMsgHelper.Create(0, "", ex);
            }
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="strjson">要保存的数据</param>
        /// <returns>json</returns>
        public virtual string Save(string sessionid, string strjson)
        {
            if (strjson == null || strjson.Equals(""))
            {
                return jsonMsgHelper.Create(1, "", "参数为空，无法增加");
            }

            try
            {
                //判断strjson是否有主键及默认值，没有则增加,目前标识只考虑了Guid
                string Key_TPk = "\"" + bsT.TPk + "\":";
                string TpkDefaultIn = Key_TPk + "\"\"";

                if (strjson.Contains(Key_TPk))
                {
                    return Edit(sessionid, strjson);
                }
                return Add(sessionid, strjson);
            }
            catch (Exception ex)
            {
                LogHelper.Error("Save:" + ex.Message);
                return jsonMsgHelper.Create(1, "",ex);
            }
        }

        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="strjson">要保存的数据，可以时新增（无主键值）或修改</param>
        /// <returns></returns>
        public virtual string Saves(string sessionid, string strjson)
        {
            if (strjson == null || strjson.Equals(""))
            {
                return jsonMsgHelper.Create(1, null, "参数为空，无法增加");
            }
            object dbobj, rowdataobj;
            Type dbtype;
            try
            {
                //增加主建 //不合适，同adds的原因
                //AddGuidTPk(ref strjson);

                dbtype = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));
                dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);

                Type typeEm = typeof(JsonHelper);
                MethodInfo miObj = typeEm.GetMethod("DeserializeJsonToList").MakeGenericMethod(dbtype);
                //此时是一个List
                rowdataobj = miObj.Invoke(null, new object[] { strjson });

                string ret = DaoSaves(dbtype, rowdataobj as List<object>);
                if (ret.ToString() == "")
                    return jsonMsgHelper.Create(0, "", "保存成功！");
                else
                    return jsonMsgHelper.Create(1, "", new Exception(ret));
            }
            catch (Exception ex)
            {
                LogHelper.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex);
            }
        }
        




        ///////// <summary>
        ///////// 获取数据列表，字段区分大小写
        ///////// </summary>
        ///////// <param name="fields">要显示的字段，为空则表示显示全部</param>
        ///////// <param name="where">keyvalue方式的json串</param>
        ///////// <param name="orderby">不含order by的完整排序sql</param>
        ///////// <returns>json数据</returns>
        ////////[HttpPost]
        //////public virtual string GetAll(string fields="", string where="", string orderby="")
        //////{
        //////    //ObjectClassFullName
        //////    if (where.Length>0)
        //////        where= Ajustsqlwhere(where);
        //////    //if (orderby == "")
        //////    //    orderby = bsFC.OrderBySql;
        //////    try
        //////    {
        //////        object objs = dataManager.GetObjects(where, orderby);
        //////        Type type = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));

        //////        if (type != null && objs != null)
        //////        {
        //////            if (fields == null || fields.Equals(""))
        //////            {
        //////                List<string> dispitems = null;
        //////                try
        //////                {
        //////                    dispitems = bllbsUserFields.GetUserNeedDispListItemDesps(EManagerApp_, LoginUser.bsU_Id, bsFC);
        //////                }
        //////                catch { }
        //////                return jsonMsgHelper.Create(0, objs, "", type, dispitems);
        //////            }
        //////            else
        //////            {
        //////                return jsonMsgHelper.CreateWithStrField(0, objs, "", type, fields);
        //////            }
        //////        }
        //////        else
        //////        {
        //////            //提示错误
        //////           //return jsonMsgHelper.CreateWithStrField(1, "","类型错误！", type, fields);
        //////           return jsonMsgHelper.Create(1, "", "类型错误！");

        //////        }

        //////    }
        //////    catch (Exception ex)
        //////    {
        //////        LogHelper.Error("Add:" ,ex.Message);
        //////        return jsonMsgHelper.Create(1, "", ex.Message);
        //////    }
        //////}


        /// <summary>
        /// 根据外键获取数据列表
        /// </summary>
        /// <param name="tfkValue"></param>
        /// <param name="fields"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public virtual string GetAllByTFk(string sessionid, string tfkValue,string fields = "", string orderby = "")
        {
            AddLogTable("获取", bsT.TName, bsT.Desp, "TFK");
            //要明确外键的数据类型
            Dictionary<string, bsField> bsFs = getbsFields(bsT.bsT_Id);

            string where = "";
            if (tfkValue != "")
            {
                if ("uniqueidentifer,datatime,varchar,nvarchar".Contains(bsFs[bsT.TFk].FType))
                    where = bsT.TFk + "='" + tfkValue + "'";
                else
                    where = bsT.TFk + "=" + tfkValue;
            }
            if (orderby == "")
                orderby = orderDefaultSql;
            try
            {
                //获取数据
                object objs = dataManager.GetObjects(where, orderby);
                //按字段进行转换
                Type type = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));
                if (type != null && objs != null)
                {
                    if (fields == null || fields.Equals(""))
                    {
                        List<string> dispitems = null;
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
                LogHelper.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "", "未能成功保存");
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
        public virtual string GetAllByProcedure(string sessionid, string spname, string fields, string where, string orderby)
        {
            AddLogTable("获取", bsT.TName, bsT.Desp, spname+" "+where);
            where = Ajustsqlwhere(where);
            if (orderby == "")
            {
                if (bsFC != null)
                    orderby = bsFC.OrderBySql;
                else
                    orderby = bsT.InitOrderBy;
            }
            if (spname == null || spname.Equals(""))
            {
                LogHelper.Error("GetAllByProcedure:必须输入参数：存储过程名" );
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
                    List<string> dispitems = bllbsUserFields.GetUserNeedDispListItemDesps(EManagerApp_, LoginUser.bsU_Id, bsFC);
                    return jsonMsgHelper.Create(0, objs, "", type, dispitems);
                }
                else
                {
                    return jsonMsgHelper.CreateWithStrField(0, objs, "", type, fields);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetAllByProcedure:" , ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }


        ///////// <summary>
        ///////// 非存储过程获取分页数据
        ///////// </summary>
        ///////// <param name="fields">要显示的字段，为空则表示显示全部</param>
        ///////// <param name="where">不含where的完整sql条件</param>
        ///////// <param name="orderby">不含order by的完整排序sql</param>
        ///////// <returns>json数据</returns>
        //////public virtual string GetAllWithPaging(string fields="", string where="", string orderby="", int currentPage = 1, int pageSize = 20)
        //////{
        //////    try
        //////    {
               
        //////        if (where!="")
        //////            where = Ajustsqlwhere(where);
                
        //////        int totalCount = 100;
        //////        int totalPage = (int)Math.Ceiling(1.0 * totalCount / pageSize);

        //////        object objs = dataManager.GetObjectsWithPaging(where, orderby, out totalCount, out totalPage, currentPage, pageSize);

        //////        Type type = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));

        //////        if (fields == null || fields.Equals(""))
        //////        {
        //////            List<string> dispitems = bllbsUserFields.GetUserNeedDispListItemDesps(EManagerApp_, LoginUser.bsU_Id, bsFC);
        //////            return jsonMsgHelper.CreatePage(0, objs, currentPage, pageSize, totalCount, totalPage, "", type, dispitems);
        //////        }
        //////        else
        //////        {
        //////            return jsonMsgHelper.CreatePageWithStrField(0, objs, currentPage, pageSize, totalCount, totalPage, "", type, fields);
        //////        }
        //////    }
        //////    catch (Exception ex)
        //////    {
        //////        LogHelper.Error("GetAllWithPaging:" , ex.Message);
        //////        return jsonMsgHelper.Create(1, "", ex.Message);
        //////    }
        //////}

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
        public virtual string GetAllWithPagingByCommonProcedure(string sessionid, string fields, string where, string orderbyfield, string ascordesc, int currentPage = 1, int pageSize = 20)
        {
            AddLogTable("获取", bsT.TName, bsT.Desp, where);
            try
            {
                where = Ajustsqlwhere(where);
                
                int totalCount = 100;
                int totalPage = (int)Math.Ceiling(1.0 * totalCount / pageSize);
                object objs = dataManager.GetObjectsWithCommonStorePaging(objClassName, where, orderbyfield, ascordesc.ToLower() == "asc" ? true : false, out totalCount, out totalPage, currentPage, pageSize);


                Type type = Type.GetType(objClassFullName);
                if (fields == null || fields.Equals(""))
                {
                    List<string> dispitems = bllbsUserFields.GetUserNeedDispListItemDesps(EManagerApp_, LoginUser.bsU_Id, bsFC);
                    return jsonMsgHelper.CreatePage(0, objs, currentPage, pageSize, totalCount, totalPage, "", type, dispitems);
                }
                else
                {
                    return jsonMsgHelper.CreatePageWithStrField(0, objs, currentPage, pageSize, totalCount, totalPage, "", type, fields);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetAllWithPagingByCommonProcedure:" , ex.Message);
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
        public virtual string GetAllWithPagingBySelfProcedure(string sessionid, string spname, string fields, string where, string orderby, int currentPage = 1, int pageSize = 20)
        {
            AddLogTable("获取", bsT.TName, bsT.Desp, spname + " " + where);

            try
            {
                where = Ajustsqlwhere(where);
                
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
                    List<string> dispitems = bllbsUserFields.GetUserNeedDispListItemDesps(EManagerApp_, LoginUser.bsU_Id, bsFC);
                    return jsonMsgHelper.CreatePage(0, objs, currentPage, pageSize, totalCount, totalPage, "", type, dispitems);
                }
                else
                {
                    return jsonMsgHelper.CreatePageWithStrField(0, objs, currentPage, pageSize, totalCount, totalPage, "", type, fields);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetAllWithPagingBySelfProcedure:" , ex.Message);
                return jsonMsgHelper.Create(1, "", ex.Message);
            }
        }


        /// <summary>
        /// 获取单个对象
        /// </summary>
        /// <param name="idValue">对应主键字段值</param>
        /// <returns>json格式对象</returns>
        public virtual string GetOne(string sessionid, string idValue)
        {
            AddLogTable("获取", bsT.TName, bsT.Desp, idValue);
            if (idValue == null || idValue.Equals(""))
            {
                return jsonMsgHelper.Create(1, "", "参数必须输入");
            }
            try
            {
                List<string> dispitems = null;
                if (bsFC!=null)
                    dispitems=bllbsUserFields.GetUserNeedDispListItemDesps(EManagerApp_, LoginUser.bsU_Id, bsFC);

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
                //if (bsFC!=null)
                //    rowdataobj = miObj.Invoke(EManagerApp_, new object[] { bsFC.TPk, idV });
                //else
                    rowdataobj = miObj.Invoke(EManagerApp_, new object[] { bsT.TPk, idV });

                if (rowdataobj != null)
                    return jsonMsgHelper.Create(0, rowdataobj, "", rowdataobj.GetType(), dispitems);
                else
                    return jsonMsgHelper.Create(1, null, "获取失败！");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return jsonMsgHelper.Create(1, "", "获取失败");
            }

        }

        /// <summary>
        /// 根据某字段值（唯一）获取一条数据
        /// </summary>
        /// <param name="FName">字段名</param>
        /// <param name="FValue">字段值</param>
        /// <returns></returns>
        public virtual string GetOneByFName(string sessionid, string FName,string FValue)
        {
            AddLogTable("获取一个", bsT.TName, bsT.Desp, FValue);

            if (FName == null || FName.Equals(""))
            {
                return jsonMsgHelper.Create(1, "", "参数不足");
            }
            try
            {
                object rowdataobj = DaoGetOneFromDb(FName, FValue);

                if (rowdataobj != null)
                    return jsonMsgHelper.Create(0, rowdataobj, "");
                else
                    return jsonMsgHelper.Create(1, null, "获取失败！");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message);
                return jsonMsgHelper.Create(1, "", "获取失败！");
            }

        }

        protected virtual string GetOnebySql(string sessionid,string sql)
        {
          
            if (sql == null || sql.Equals(""))
            {
                return jsonMsgHelper.Create(1, "", "参数不足");
            }
            else if (sql.ToLower().Contains("delete"))
            {
                return jsonMsgHelper.Create(1, "", "参数不合理");
            }
            try
            {
                object rowdataobj = DaoGetOneBySql(sql);

                if (rowdataobj != null)
                    return jsonMsgHelper.Create(0, rowdataobj, "");
                else
                    return jsonMsgHelper.Create(0, "", "没有记录！");
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message);
                return jsonMsgHelper.Create(1, "", "获取失败！");
            }

        }

        /// <summary>
        /// 按照配置获取树结构
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public virtual string getTreeDefault(string sessionid, string where, string orderby)
        {
            bsTInterface bsti = EntityManager_Static.GetBySql<bsTInterface>(DbContext, "bsT_Id='" + bsT.bsT_Id + "' and LinkAction='getree'");
            return getTree(where, orderby, bsti.TNId, bsti.TNText, bsti.TNpId, bsti.TNType);
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
        public string getTree(string where, string orderby,
          string idfieldname, string namefieldname, string pidfieldname,string typefieldname="")
        {
            try
            {
                List<qytvNode> treelist = getTreeNodes(where, orderby, idfieldname, namefieldname, pidfieldname, typefieldname);
                //return jsonMsgHelper.Create(0, treelist, "", treelist[0].GetType(), null);
                return jsonMsgHelper.Create(0, treelist, "", typeof(qytvNode), null);
            }
            catch (Exception ex)
            {
                LogHelper.Error("Add:" + ex.Message);
                return jsonMsgHelper.Create(1, "","获取失败！");
            }
        }

       
        /// <summary>
        /// 导入Excel文件 ,导入一定时excel文件并转入数据库，上传可以时各种文件
        /// </summary>
        /// <returns></returns>
        protected string Import(string sessionid, string filename)
        {
            return jsonMsgHelper.Create(1,null, "还没做呢！确定需要导入接口吗？需要请联系后台人员实现。");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string Export(string sessionid, string filename,string where,string orderby,string fields="")
        {
            QyExcelHelper excelhlper = new QyExcelHelper("Web");
            //excelhlper.ExportDataTableToExcl()
            return jsonMsgHelper.Create(1, null, "确定需要导出接口吗？需要请联系后台人员实现。");
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="idValue">记录主键</param>
        /// <param name="YesOrNo">1: 通过，0：不通过</param>
        /// <param name="AuditDesp">审核意见</param>
        /// <returns></returns>
        public virtual string Audit(string sessionid,string idValue,int YesOrNo,string AuditDesp)
        {
            return jsonMsgHelper.Create(1, null, "确定需要审核接口吗？需要请联系后台人员实现。");
        }



        /// <summary>
        /// key value格式的无引号json串转换为dictionary方式，用于编辑时引用
        /// </summary>
        /// <param name="jsonkevalues">无引号json串</param>
        /// <returns></returns>
        protected Dictionary<string, string> json2dicKV(string jsonkevalues)
        {
            Dictionary<string, string> dicKV = new Dictionary<string, string>();

            if (jsonkevalues == null || jsonkevalues.Equals(""))
                return dicKV;
            jsonkevalues = jsonkevalues.Replace(" ", "");
            try
            {
                jsonkevalues = jsonkevalues.Replace("{", "{\"").Replace("}", "\"}").Replace(":", "\":\"");
                List<QyTech.Json.keyVal> kvs = JsonHelper.DeserializeJsonToKeyValList(jsonkevalues);
                //进一步修改为keyvalues
                foreach (keyVal kv in kvs)
                {
                    dicKV.Add(kv.key, kv.val);
                }
                return dicKV;
            }

            catch (Exception ex)
            {
                LogHelper.Error("EditbyKeyValues:" + ex);
                return dicKV;
            }
        }


        /// <summary>
        /// 根据指定字段值编辑数据
        /// </summary>
        /// <param name="sessionid"></param>
        /// <param name="FName"></param>
        /// <param name="FValue"></param>
        /// <param name="dicKV">字段名，和字段值（string）</param>
        /// <returns></returns>
        protected string EditbyKeyValues(string FName, string FValue, Dictionary<string, string> dicKV)
        {
            //增加日志
            AddLogTable("EditbydicKV", bsT.TName, bsT.Desp, FName + " " + FValue);

            if (dicKV == null || dicKV.Count == 0)
            {
                return jsonMsgHelper.Create(1, "", "参数为空，无法修改");
            }

            try
            {
                Type dbtype = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));

                #region 单独方法 不使用
                ////1。获取字段列表
                //Dictionary<string, bsField> items = getbsFields(bsT.bsT_Id);
                ////2.获取rowdataobj主键值
                //object idV = GetRightFValue(items[FName], FValue);//类型
                //Guid ltid = AddLogTable("编辑", bsT.TName, bsT.Desp, idV.ToString());


                ////3. 获取对应的数据库对象rowdbobj
                //Type typeEm = typeof(EntityManager);
                //MethodInfo miObj = typeEm.GetMethod("GetByPk").MakeGenericMethod(dbtype);
                //object rowdbobj = miObj.Invoke(EManagerApp_, new object[] { FName, idV });

                ////4 按照上面的字段列表将rowdataobj中的值复制给rowdbobj，
                //foreach (string fname in dicKV.Keys)
                //{
                //    if (fname == bsT.TPk)
                //        continue;
                //    PropertyInfo propertyInfo = dbtype.GetProperty(fname);
                //    object svalue = dicKV[fname];
                //    propertyInfo.SetValue(rowdbobj, svalue, null); //给对应属性赋值

                //    try
                //    {
                //        object prevalue = propertyInfo.GetValue(rowdbobj, null);
                //        AddLogField(ltid, fname, items[fname].FDesp, svalue.ToString(), prevalue.ToString());
                //    }
                //    catch { }
                //}
                ////5。保存rowdbobj //,new Type[] { typeof(String), typeof(String)}// new Type[] { typeof(System.Data.Objects.DataClasses.EntityObject), typeof(String), typeof(object) }
                //miObj = typeEm.GetMethod("Modify").MakeGenericMethod(dbtype);
                //string editret = miObj.Invoke(EManagerApp_, new object[] { rowdbobj }).ToString();
                #endregion
                string editret = DaoModify(dbtype, FName, FValue, dicKV);
                if (editret.ToString() == "")
                    return jsonMsgHelper.Create(0, "", "保存成功！");
                else
                    return jsonMsgHelper.Create(1, "", new Exception(editret));
            }
            catch (Exception ex)
            {
                LogHelper.Error("EditbyKeyValues_dicKV:" + ex.Message);
                return jsonMsgHelper.Create(1, "", ex);
            }
        }
        protected List<qytvNode> getTreeNodes(string where, string orderby)
        {
            bsTInterface bsti = EntityManager_Static.GetBySql<bsTInterface>(DbContext, "bsT_Id='" + bsT.bsT_Id + "' and LinkAction='getree'");
            return getTreeNodes(where, orderby, bsti.TNId, bsti.TNText, bsti.TNpId, bsti.TNType);
        }
        protected List<qytvNode> getTreeNodes(string where, string orderby,
         string idfieldname, string namefieldname, string pidfieldname, string typefieldname = "")
        {

            var treelist = new List<qytvNode>();

            try
            {
                System.Reflection.PropertyInfo propertyInfo;
                object val;
                Type type = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));

                object objs = dataManager.GetObjects(where, orderby);

                qytvNode treenode = new qytvNode();
                foreach (var cc in (objs as IEnumerable))
                {
                    treenode = new qytvNode();

                    propertyInfo = type.GetProperty(idfieldname);
                    val = propertyInfo.GetValue(cc, null);
                    treenode.id = val.ToString();

                    propertyInfo = type.GetProperty(namefieldname);
                    val = propertyInfo.GetValue(cc, null);
                    treenode.name = val.ToString();

                    if (pidfieldname != null || typefieldname.Equals(""))
                    {
                        propertyInfo = type.GetProperty(pidfieldname);
                        val = propertyInfo.GetValue(cc, null);
                        if (val != null)
                            treenode.pId = val.ToString();
                    }
                    else
                    {
                        treenode.pId = null;
                    }


                    if (typefieldname == null || typefieldname.Equals(""))
                    {
                        treenode.type = "";
                    }
                    else
                    {
                        propertyInfo = type.GetProperty(typefieldname);
                        val = propertyInfo.GetValue(cc, null);
                        treenode.type = val.ToString();
                    }
                    treenode.addBtnFlag = false;
                    treenode.removeBtnFlag = false;
                    treenode.editBtnFlag = false;

                    treelist.Add(treenode);
                }
                return treelist;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Add:" + ex.Message);
                return treelist;
            }
        }


    }
}
