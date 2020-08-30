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

using qyExpress.Dao;
using QyTech.ExcelOper;

namespace QyTech.Core.ExController
{
    public partial class QyTechController
    {

        private string DaoAdd(Type dbtype, object rowdataobj)
        {
            //增加日志
            AddLogTable("增加", bsT.TName, bsT.Desp, "");

            Type typeEm = typeof(EntityManager);
            MethodInfo miObj = typeEm.GetMethod("Add").MakeGenericMethod(dbtype);
            object ret = miObj.Invoke(EManagerApp_, new object[] { rowdataobj });
            return ret.ToString();
        }

        private string DaoAddReturnEntity(Type dbtype, object rowdataobj)
        {
            //增加日志
            AddLogTable("增加", bsT.TName, bsT.Desp, "");

            Type typeEm = typeof(EntityManager);
            MethodInfo miObj = typeEm.GetMethod("AddReturnEntity").MakeGenericMethod(dbtype);
            object rowdbobj = miObj.Invoke(EManagerApp_, new object[] { rowdataobj });

            if (rowdbobj != null)
                return jsonMsgHelper.Create(0, rowdbobj, "保存成功", rowdbobj.GetType(),new List<string>());
            else
                return jsonMsgHelper.Create(1, null, "获取失败！");
        }
        


        private string DaoAdds(Type dbtype,List<object> rowobjs)
        {
            //增加日志
            AddLogTable("批量增加", bsT.TName, bsT.Desp, "");
            foreach(object row in rowobjs)
            {
                //应该反射增加主键，not finished 2018-11-27
            }
            Type typeEm = typeof(EntityManager);
            MethodInfo miObj = typeEm.GetMethod("Adds").MakeGenericMethod(dbtype);
            object ret = miObj.Invoke(EManagerApp_, new object[] { rowobjs });
            return ret.ToString();
        }

        private string DaoModify(Type dbtype,object rowdataobj)
        {

            //1.获取rowdataobj主键值
            System.Reflection.PropertyInfo propertyInfo = dbtype.GetProperty(bsT.TPk); //获取指定名称的属性
            object idV = propertyInfo.GetValue(rowdataobj, null);
            Guid ltid = AddLogTable("编辑", bsT.TName, bsT.Desp, idV.ToString());

            //2. 获取对应的数据库对象rowdbobj
            Type typeEm = typeof(EntityManager);
            MethodInfo miObj = typeEm.GetMethod("GetByPk").MakeGenericMethod(dbtype);
            object rowdbobj = miObj.Invoke(EManagerApp_, new object[] { bsT.TPk, idV });

            //3。获取在界面中编辑的字段列表
            if (bsFC != null)
            {
                List<listWriteDataItemSet> items = bllbsUserFields.GetEditListDispItemDesps(EManager_, LoginUser.bsU_Id, bsFC.bsFC_Id);
                //4 按照上面的字段列表将rowdataobj中的值复制给rowdbobj，
                foreach (listWriteDataItemSet item in items)
                {
                    if (item.FEditable)
                    {
                        propertyInfo = dbtype.GetProperty(item.FName);
                        object svalue = propertyInfo.GetValue(rowdataobj, null);
                        propertyInfo.SetValue(rowdbobj, svalue, null); //给对应属性赋值
                        try
                        {
                            object prevalue = propertyInfo.GetValue(rowdbobj, null);
                            AddLogField(ltid, item.FName, item.FDesp, svalue.ToString(), prevalue.ToString());
                        }
                        catch { }
                    }
                }
            }
            else
            {
                List<bsField> items = EManager_.GetListNoPaging<bsField>("bsT_id='" + bsT.bsT_Id.ToString() + "' and FEditable=1", "");
                //4 按照上面的字段列表将rowdataobj中的值复制给rowdbobj，
                foreach (bsField item in items)
                {
                    if (item.FEditable != null && item.FEditable.Value)
                    {
                        try
                        {
                            propertyInfo = dbtype.GetProperty(item.FName);
                            object svalue = propertyInfo.GetValue(rowdataobj, null);
                            
                            if (propertyInfo.PropertyType.ToString().Contains("string") &&  svalue == null)
                            {
                                continue;//应为可能数据太多，所以（水电表）有些数据可能分批保存，某些之为null，则字符型为null时不处理，可以为"" modified on 2019-04-18
                            }
                            propertyInfo.SetValue(rowdbobj, svalue, null); //给对应属性赋值

                            //加入日志
                            object prevalue = propertyInfo.GetValue(rowdbobj, null);
                            if (prevalue != svalue)
                                AddLogField(ltid, item.FName, item.FDesp, svalue == null ? "null" : svalue.ToString(), prevalue == null ? "null" : prevalue.ToString());
                           
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error(ex);
                        }
                    }
                }
            }
            //5。保存rowdbobj //,new Type[] { typeof(String), typeof(String)}// new Type[] { typeof(System.Data.Objects.DataClasses.EntityObject), typeof(String), typeof(object) }
            miObj = typeEm.GetMethod("Modify").MakeGenericMethod(dbtype);
            object retmsg = miObj.Invoke(EManagerApp_, new object[] { rowdbobj });
            return retmsg.ToString();
        }

        private string DaoModify(Type dbtype, string FName, string FValue, Dictionary<string, string> dicKV)
        {

          
            //1。获取字段列表
            Dictionary<string, bsField> items = getbsFields(bsT.bsT_Id);
            //2.获取rowdataobj主键值
            object idV = GetRightFValue(items[FName], FValue);//类型
            Guid ltid = AddLogTable("编辑", bsT.TName, bsT.Desp, idV.ToString());


            //3. 获取对应的数据库对象rowdbobj
            Type typeEm = typeof(EntityManager);
            MethodInfo miObj = typeEm.GetMethod("GetByPk").MakeGenericMethod(dbtype);
            object rowdbobj = miObj.Invoke(EManagerApp_, new object[] { FName, idV });

            //4 按照上面的字段列表将rowdataobj中的值复制给rowdbobj，
            foreach (string fname in dicKV.Keys)
            {
                if (fname == bsT.TPk)
                    continue;
                PropertyInfo propertyInfo = dbtype.GetProperty(fname);
                object svalue = dicKV[fname];
                if (propertyInfo.PropertyType.ToString().Contains("Int32"))
                    svalue = Convert.ToInt32(svalue);
                else if (propertyInfo.PropertyType.ToString().Contains("GUID"))
                    svalue = Guid.Parse(svalue.ToString());
                else if (propertyInfo.PropertyType.ToString().ToLower().Contains("datetime"))
                    svalue = Convert.ToDateTime(svalue.ToString());
                propertyInfo.SetValue(rowdbobj, svalue, null); //给对应属性赋值

                try
                {
                    object prevalue = propertyInfo.GetValue(rowdbobj, null);
                    AddLogField(ltid, fname, items[fname].FDesp, svalue.ToString(), prevalue.ToString());
                }
                catch { }
            }
            //5。保存rowdbobj //,new Type[] { typeof(String), typeof(String)}// new Type[] { typeof(System.Data.Objects.DataClasses.EntityObject), typeof(String), typeof(object) }
            miObj = typeEm.GetMethod("Modify").MakeGenericMethod(dbtype);
            string retmsg = miObj.Invoke(EManagerApp_, new object[] { rowdbobj }).ToString();
            return retmsg.ToString();
        }

        private string DaoSaves(Type dbtype,List<object> objs)
        {
            string ret = "";
            int count = 0;
            string tpkfname = bsT.TPk;
            //判断没有object的主键值，有则编辑，否则新增
            foreach (object obj in objs)
            {
                try
                {
                    PropertyInfo propertyInfo = dbtype.GetProperty(tpkfname);
                    object tpkvalue = propertyInfo.GetValue(obj);
                    if (tpkvalue == null)
                    {
                        DaoAdd(dbtype, obj);
                    }
                    else
                    {
                        DaoModify(dbtype, obj);
                    }
                }
                catch(Exception  ex) {
                    LogHelper.Error(ex);
                    count++;
                    ret = count.ToString()+"行数据没有成功保存，请核实数据！";
                }
            }
            return ret;
        }

        private object DaoGetOneFromDb(string FName, string FValue)
        {
            if (FName == null || FName.Equals(""))
            {
                return null;
            }
            try
            {
                Dictionary<string, string> fieldTypes = bllbsUserFields.GetbsTFieldsOType(EM_Base, LoginUser.bsU_Id, bsT);
                object idV = FValue;
                Guid guidV = Guid.Empty;
                if (FValue != null)
                {
                    if ("GUID".Contains(fieldTypes[FName].ToUpper()))
                    {
                        idV = Guid.Parse(FValue);
                    }
                    else if (fieldTypes[FName].ToUpper() == "INT")
                    {
                        idV = Convert.ToInt64(FValue);
                    }
                    else if (fieldTypes[FName].ToUpper() == "DECIMAL")
                    {
                        idV = Convert.ToDecimal(FValue);
                    }
                    else
                    {
                        idV = FValue;
                    }
                }


                object dbobj, rowdataobj;
                Type dbtype;
                MethodInfo miObj;

                dbtype = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));
                dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);

                Type typeEm = typeof(EntityManager);
                miObj = typeEm.GetMethod("GetByPk").MakeGenericMethod(dbtype);
                rowdataobj = miObj.Invoke(EManagerApp_, new object[] { FName, idV });
                return rowdataobj;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }

        protected object DaoGetOneBySql(string sql)
        {
            if (sql == null || sql.Equals(""))
            {
                return null;
            }
            try
            {
                object dbobj, rowdataobj;
                Type dbtype;
                MethodInfo miObj;

                dbtype = Type.GetType(objClassFullName);//.Replace(strForReplaceObject, objNameSpace + "." + objClassName));
                dbobj = dbtype.Assembly.CreateInstance(dbtype.FullName);

                Type typeEm = typeof(EntityManager);
                miObj = typeEm.GetMethod("GetBySql").MakeGenericMethod(dbtype);
                rowdataobj = miObj.Invoke(EManagerApp_, new object[] { sql });
                return rowdataobj;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }
        protected Dictionary<string, string> kvWhere2Dic(string kvwhere)
        {
            LogHelper.Info(kvwhere);
            Dictionary<string, string> dics = new Dictionary<string, string>();

            try
            {
                if (kvwhere.Substring(0, 1) != "[")//则认为是原始的sql语句
                    return dics;

                //where = "[{Name: 张三},{Age: 18}]";
                kvwhere = kvwhere.Replace("{", "{\"").Replace("}", "\"}").Replace(":", "\":\"");

                List<QyTech.Json.keyVal> wherelist = JsonHelper.DeserializeJsonToKeyValList(kvwhere);
                foreach (QyTech.Json.keyVal kv in wherelist)
                {
                    dics.Add(kv.key.ToLower(), kv.val);
                }
                return dics;
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
            return dics;
        }

        /// <summary>
        /// 校正where条件
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        protected string Ajustsqlwhere(string where)
        {
            LogHelper.Info(where);

            string sqlwhere = "";
            if (where.Substring(0, 1) != "[")//则认为是原始的sql语句
                return where;
            
            //where = "[{Name: 张三},{Age: 18}]";
            where = where.Replace("{", "{\"").Replace("}", "\"}").Replace(":", "\":\"");
            
            List<QyTech.Json.keyVal> wherelist = JsonHelper.DeserializeJsonToKeyValList(where);

            Dictionary<string, bsField> bsFs = getbsFields(bsT.bsT_Id);
            foreach (QyTech.Json.keyVal kv in wherelist)
            {
                //获取该字段的对象，根据类型 得到处理的方式
                if (bsFs.ContainsKey(kv.key))
                {
                    if (bsFs[kv.key].FType == "varchar")
                    {
                        sqlwhere += " and " + kv.key + " like '%" + kv.val + "%'";
                    }
                    else if (bsFs[kv.key].FType == "uniqueidentifier")
                    {
                        sqlwhere += " and " + kv.key + "='" + kv.val + "'";
                    }
                    else if (bsFs[kv.key].FType == "int"
                        || bsFs[kv.key].FType == "decimal"
                        )
                    {
                        if (kv.val.IndexOf("|") == -1)
                        {
                            sqlwhere += " and " + kv.key + "=" + kv.val;
                        }
                        else
                        {
                            string[] str = kv.val.Split(new char[] { '|' });

                            sqlwhere += " and " + kv.key + ">=" + str[0] + " and " + kv.key + "<=" + str[1];

                        }
                    }
                    else if (bsFs[kv.key].FType == "datetime")
                    {
                        if (kv.val.IndexOf("|") == -1)
                        {
                            sqlwhere += " and " + kv.key + "='" + kv.val+"'";
                        }
                        else
                        {
                            string[] str = kv.val.Split(new char[] { '|' });

                            sqlwhere += " and " + kv.key + ">='" + str[0] + "' and " + kv.key + "<='" + str[1]+"'";

                        }
                    }
                    else
                    {
                        sqlwhere += " and " + kv.key + " like '%" + kv.val + "%'";
                    }
                }
                else
                {
                    sqlwhere += " and " + kv.key + " like '%" + kv.val + "%'";
                }
            }
            if (sqlwhere.Length > 0)
            {
                sqlwhere = sqlwhere.Substring(4);
            }
            return sqlwhere;
        }

        protected bsTable GetbsTablebyName(string tName)
        {
            return EManager_.GetByPk<bsTable>("TName", tName);
        }

        protected void SetObjectClassNamebyTName(string tName)
        {
            bsT = GetbsTablebyName(tName);
            if (bsT!=null)
                ObjectClassName = bsT.TName;
        }
        protected Dictionary<string, bsField> getbsFields(Guid bsT_Id)
        {
            Dictionary<string, bsField> bsFs = new Dictionary<string, bsField>();
            List<bsField> ffs = EManager_.GetListNoPaging<bsField>("bsT_Id='" + bsT_Id.ToString() + "'", "");

            foreach (bsField item in ffs)
            {
                bsFs.Add(item.FName, item);
            }
            return bsFs;
        }


        /// <summary>
        /// 根据字段类型得到正确的字段值对象
        /// </summary>
        /// <param name="bsf">字段信息</param>
        /// <param name="val">值</param>
        /// <returns></returns>
        protected object GetRightFValue(bsField bsf, string val)
        {
            object objvalue;
            if (val == null || val == "")
                objvalue = null;
            else if (bsf.OType.ToUpper() == "STRING")
                objvalue = val;
            else if (bsf.OType.ToUpper() == "GUID")
                objvalue = Guid.Parse(val);
            else if (bsf.OType.ToUpper() == "DATETIME")
                objvalue = Convert.ToDateTime(val);
            else if (bsf.OType.ToUpper() == "INT16")
                objvalue = Convert.ToInt16(val);
            else if (bsf.OType.ToUpper() == "INT32")
                objvalue = Convert.ToInt32(val);
            else if (bsf.OType.ToUpper() == "INT64")
                objvalue = Convert.ToInt64(val);
            else if (bsf.OType.ToUpper() == "DECIMAL")
                objvalue = Convert.ToDecimal(val);
            else if (bsf.OType.ToUpper() == "FLOAT")
                objvalue = Convert.ToSingle(val);
            else if (bsf.OType.ToUpper() == "DOUBLE")
                objvalue = Convert.ToDouble(val);
            else if (bsf.OType.ToUpper() == "BOOLEAN")
                objvalue = Convert.ToBoolean(val);
            else
                objvalue = null;

            return objvalue;
        }

        /// <summary>
        /// 处理json中是否包含Guid主键
        /// </summary>
        /// <param name="strjson"></param>
        private void AddGuidTPk(ref string strjson)
        {
            //如果主键类型是guid，则直接给值
            //如果是整型，应该默认为自增
            //否则，应该
            string Key_TPk = "\"" + bsT.TPk + "\"";
            string TpkDefault = Key_TPk + ":" + "\"" + PkCreateHelper.GetQySortGuidPk().ToString() + "\"";
            if (bsT.TPkType == null)
            {
                throw new Exception("需明确主键类型！");
            }
            else if (bsT.TPkType == "uniqueidentifier")
            {
                //json串是否包含主键
                if (strjson.Contains(Key_TPk))
                {
                    //判断strjson是否有主键及默认值，没有则增加,此处用了一个有序的guid
                    string TpkDefaultIn = Key_TPk + ":" + "\"\"";
                    if (strjson.Contains(TpkDefaultIn)) //如果传的串包含主键值为""，否则就是已经赋值
                    {
                        strjson = strjson.Replace(TpkDefaultIn, TpkDefault);//则用赋值替代
                    }
                }
                else //否则，直接用一个GUID代替
                {
                    strjson = "{" + TpkDefault + "," + strjson.Substring(1);
                }
            }
            else if (bsT.TPkType.ToUpper() == "INT")
            {
                //自增数据，如果用户界面中包含主键对应值，应该删除掉这部分str；不包含的话就不用管
                //json串是否包含主键
                if (strjson.Contains(Key_TPk))
                {
                    //判断strjson是否有主键及默认值，没有则增加,此处用了一个有序的guid
                    string TpkDefaultIn = Key_TPk + ":" + "\"null\",";
                    if (strjson.Contains(TpkDefaultIn)) //如果传的串包含主键值为""，否则就是已经赋值
                    {
                        strjson = strjson.Replace(TpkDefaultIn, "");//则取消这部分
                    }
                }
            }
            //其它类型暂不处理
        }



    }
}
