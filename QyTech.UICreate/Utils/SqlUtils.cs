using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

using QyExpress.Dao;
using System.Windows.Forms;

namespace QyTech.DbUtils
{
    public class SqlUtils
    {

        public static DataTable GetDataTable(SqlConnection sqlConn, string tName, string selectfields,string where, string orderby = "")
        {
            DataTable dtList = null;
            try
            {
                //sqlConn = new SqlConnection(sqlConn.ConnectionString);
                string strSql = "select "+ selectfields + " from [" + tName + "] " + (where == "" ? "" : " where " + where) + (orderby == "" ? "" : " order by " + orderby);//表名的写法也应注意不同，对应的excel表为sheet1，在这里要在其后加美元符号$，并用中括号
                SqlCommand Cmd = new SqlCommand(strSql, sqlConn);//建立要执行的命令
                SqlDataAdapter da = new SqlDataAdapter(Cmd);//建立数据适配器
                DataSet ds = new DataSet();
                da.Fill(ds, tName);//把数据适配器中的数据读到数据集中的一个表中（此处表名为shyman，可以任取表名）
                                   //指定datagridview1的数据源为数据集ds的第一张表（也就是shyman表），也可以写ds.Table["shyman"]
                dtList = ds.Tables[tName];
               
                return dtList;
            }
            catch (Exception ex)
            {
                throw new Exception("GetDataTable:" + ex.Message);
            }
        }

        public static List<string> GetOneFieldValues(SqlConnection sqlConn, string fullsql)
        {
            List<string> lstStr = new List<string>();
            try
            {
                //sqlConn = new SqlConnection(sqlConn.ConnectionString);
                string strSql = fullsql;
                SqlCommand Cmd = new SqlCommand(strSql, sqlConn);//建立要执行的命令
                SqlDataAdapter da = new SqlDataAdapter(Cmd);//建立数据适配器
                DataSet ds = new DataSet();
                da.Fill(ds, "tName");//把数据适配器中的数据读到数据集中的一个表中（此处表名为shyman，可以任取表名）
                                     //指定datagridview1的数据源为数据集ds的第一张表（也就是shyman表），也可以写ds.Table["shyman"]
                DataTable dtList = ds.Tables["tName"];

                foreach (DataRow dr in dtList.Rows)
                {
                    lstStr.Add(dr[0].ToString());
                }
                return lstStr;
            }
            catch (Exception ex)
            {
                throw new Exception("GetOneFieldValues:" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlConn"></param>
        /// <param name="ff"></param>
        /// <param name="dynara">用用程序名找表，表id找字段</param>
        /// <returns></returns>
        public static string[] ParseSelectItemsFromFunField(SqlConnection sqlConn, string editItems,string dynara)
        {
            string[] ret = new string[2];
            string[] items;

            Dictionary<string, string> dics = new Dictionary<string, string>();
            //是定义变量还是普通列表
            List<string> predefVars = GetOneFieldValues(sqlConn, "SELECT [InterfName] FROM [QyExpress].[dbo].[blPredefineInterface] ");
            int KhPos = editItems.IndexOf('(');
            if (KhPos >= 0)
            {
                string var = editItems.Substring(0, KhPos);
                if (predefVars.Contains(var))//包含(左侧的完整串匹配则是与动态获取
                {
                    if (var == "GetDbAllTables")
                    {
                        dics = GetDbAllTables(sqlConn, dynara);
                    }
                    else if (var== "GetDbAllAreas")
                    { 
                        dics = GetDbAllAreas(sqlConn, dynara);
                    }

                }
            }
            else//普通列表,;分割项，分割显示与值
            {
                items = editItems.Split(new char[] { ';' });
                if (items.Length > 1)//使用了；
                {
                    foreach (string item in items)
                    {
                        int dh = item.IndexOf(",");
                        if (dh >= 0)
                        {
                            dics.Add(item.Substring(0, dh), item.Substring(dh + 1));
                        }
                        else
                        {
                            dics.Add(item, item);
                        }
                    }
                }
                else//只是逗号的分割，也可以使用，但都是
                {
                    items = editItems.Split(new char[] { ',' });
                    foreach (string item in items)
                    {
                       dics.Add(item, item);
                    }
                }

            }
            foreach(string key in dics.Keys)
            {
                ret[0] += "," + key;
                ret[1] += "," + dics[key];
            }
            ret[0] = ret[0].Substring(1);
            ret[1] = ret[1].Substring(1);

            return ret;
        }
        public static List<string> GetOneFieldValues_Where(SqlConnection sqlConn,string tName,string fName,string where="")
        {
            try
            {
                string strSql = "select fName from [" + tName + "] " + (where == "" ? "" : " where " + where) + " order by " + fName;//表名的写法也应注意不同，对应的excel表为sheet1，在这里要在其后加美元符号$，并用中括号
                return GetOneFieldValues(sqlConn,strSql);
            }
            catch (Exception ex)
            {
                throw new Exception("GetOneFieldValues:" + ex.Message);
            }
        }
        
        public static Dictionary<string, string> GetDbAllTables(SqlConnection sqlConn, string APPName)
        {
            Dictionary <string, string> dicTables = new Dictionary<string, string>();
            try
            {
                string tName = "bsTable";
                //sqlConn = new SqlConnection(sqlConn.ConnectionString);
                //string strSql = "select bsT_Id,bsD_Name+'.'+TName as TName from bsTable where bsD_Id in (select bsD_Id from bsDb where AppName='" +APPName + "') order by tName";
                string strSql = "select bsT_Id,TName as TName from bsTable where bsD_Id in (select bsD_Id from bsDb where AppName='" + APPName + "') order by tName";
                SqlCommand Cmd = new SqlCommand(strSql, sqlConn);//建立要执行的命令
                SqlDataAdapter da = new SqlDataAdapter(Cmd);//建立数据适配器
                DataSet ds = new DataSet();
                da.Fill(ds, tName);//把数据适配器中的数据读到数据集中的一个表中（此处表名为shyman，可以任取表名）
                                   //指定datagridview1的数据源为数据集ds的第一张表（也就是shyman表），也可以写ds.Table["shyman"]
                DataTable dtList = ds.Tables[tName];

                foreach (DataRow dr in dtList.Rows)
                {
                    dicTables.Add(dr["TName"].ToString(),dr["bsT_Id"].ToString());
                }
                return dicTables;
            }
            catch (Exception ex)
            {
                throw new Exception("GetDbAllTables:" + ex.Message);
            }
        }

        public static Dictionary<string, string> GetDbAllAreas(SqlConnection sqlConn, string APPName)
        {
            Dictionary<string, string> dicTables = new Dictionary<string, string>();
            try
            {
                string tName = "bsAreaRegistration";
                //sqlConn = new SqlConnection(sqlConn.ConnectionString);
                string strSql = "select AreaName from bsAreaRegistration where AppName='" + APPName + "' order by AreaName";
                if (APPName=="")
                {
                    strSql = "select AreaName from bsAreaRegistration order by AreaName";
                }
                
                SqlCommand Cmd = new SqlCommand(strSql, sqlConn);//建立要执行的命令
                SqlDataAdapter da = new SqlDataAdapter(Cmd);//建立数据适配器
                DataSet ds = new DataSet();
                da.Fill(ds, tName);//把数据适配器中的数据读到数据集中的一个表中（此处表名为shyman，可以任取表名）
                                   //指定datagridview1的数据源为数据集ds的第一张表（也就是shyman表），也可以写ds.Table["shyman"]
                DataTable dtList = ds.Tables[tName];

                foreach (DataRow dr in dtList.Rows)
                {
                    dicTables.Add(dr["AreaName"].ToString(), dr["AreaName"].ToString());
                }
                return dicTables;
            }
            catch (Exception ex)
            {
                throw new Exception("GetDbAllAreas:" + ex.Message);
            }
        }
        public static List<string> GetTableAllFields(SqlConnection sqlConn, string bsT_Id)
        {
            List<string> lstStr = new List<string>();
            try
            {
                string tName = "bsfield";
                //sqlConn = new SqlConnection(sqlConn.ConnectionString);
                string strSql = "select fName from bsfield where bsT_Id='"+bsT_Id+"' order by FNo";
                SqlCommand Cmd = new SqlCommand(strSql, sqlConn);//建立要执行的命令
                SqlDataAdapter da = new SqlDataAdapter(Cmd);//建立数据适配器
                DataSet ds = new DataSet();
                da.Fill(ds, tName);//把数据适配器中的数据读到数据集中的一个表中（此处表名为shyman，可以任取表名）
                                   //指定datagridview1的数据源为数据集ds的第一张表（也就是shyman表），也可以写ds.Table["shyman"]
                DataTable dtList = ds.Tables[tName];

                foreach (DataRow dr in dtList.Rows)
                {
                    lstStr.Add(dr["fName"].ToString());
                }
                return lstStr;
            }
            catch (Exception ex)
            {
                throw new Exception("GetOneFieldValues:" + ex.Message);
            }
        }
        public static int ExceuteSql(SqlConnection sqlconn, string strSql)
        {
            int ret = -2;
            try
            {
                sqlconn.Open();
                SqlCommand Cmd = new SqlCommand(strSql, sqlconn);//建立要执行的命令
                ret = Cmd.ExecuteNonQuery();
                sqlconn.Close();
                return ret;
            }
            catch (Exception ex)
            {
                ret= -2;
            }
            finally
            {
                sqlconn.Close();
            }
            return ret;
        }

        public static string ExceuteSp(SqlConnection sqlconn, string spName,string param)
        {
            string retstr = "操作成功";
            try
            {
                sqlconn.Open();
                string strSql = "exec "+ spName+"  "+ param;

                SqlCommand Cmd = new SqlCommand(strSql, sqlconn);//建立要执行的命令
                int ret = Cmd.ExecuteNonQuery();
                sqlconn.Close();
                //if (ret == -1)
                //    retstr = "没有记录被影响";
                return retstr;
            }
            catch (Exception ex)
            {
                retstr = ex.Message;
            }
            finally
            {
                sqlconn.Close();
            }
            return retstr;
        }


        /// <summary>
        /// 数据库脚本导入excel数据, 对已存在数据的修改怎么处理呢？让用户自己决定是否直接删除？
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <param name="ColRel">列关系</param>
        private DataRow insertToSql(SqlConnection sqlconn, DataRow dr, string TName, Dictionary<string, string> ColRel, string ExcelNotNullCols = "")
        {
            string sql = "insert into " + TName;
            //excel表中的列名和数据库中的列名一定要对应  
            string fields = "", values = "";

            try
            {
                foreach (string key in ColRel.Keys)
                {
                    //没有对应Excel列
                    if (ColRel[key].Trim() == "")
                        continue;
                    //非空列
                    if (ExcelNotNullCols.Contains("," + fields) && dr[ColRel[key]].ToString() == "")
                        return dr;
                    fields += "," + key;
                    values += ",'" + dr[ColRel[key]].ToString() + "'";

                }

                fields = "(" + fields.Substring(1) + ")";
                values = "(" + values.Substring(1) + ")";

                sql = sql + fields + " values" + values;
                SqlCommand cmd = new SqlCommand(sql, sqlconn);
                cmd.ExecuteNonQuery();
                return null;
            }
            catch (Exception ex)
            {
                //log.Error(ex.Message);
                return dr;
            }
        }

        public static  int insertForAddForm(SqlConnection sqlconn, bsTable bst,List<Control> controls,Dictionary<string,string> dicFName2Type, Dictionary<string,object> HiddenFieldsValue)
        {

            string sql = "insert into " + bst.TName;
            //excel表中的列名和数据库中的列名一定要对应  
            string fields = "", values = "";

            try
            {
                foreach (Control c in controls)
                {
                    fields += "," + c.Name;

                    if (c.Text.Trim() == "")
                    {
                        values += ",null";
                    }
                    else
                    {
                        if ("string,guid".Contains(dicFName2Type[c.Name].ToLower()))
                        {
                            values += ",'" + c.Text + "'";
                        }
                        else if ("int,decimal,float,int32".Contains(dicFName2Type[c.Name].ToLower()))
                        {
                            values += "," + c.Text;
                        }
                        else if ("datetime".Contains(dicFName2Type[c.Name].ToLower()))
                        {
                            values += ",'" + c.Text + "'";
                        }
                        else if ("boolean" == dicFName2Type[c.Name].ToLower())
                        {
                            if (c.Text == "False")
                                values += ",0";
                            else
                                values += ",1";
                        }
                    }

                }
                //隐藏域已经赋值的字段
                foreach(string fname in HiddenFieldsValue.Keys)
                {
                    fields += "," + fname;
                    if ("string,guid".Contains(dicFName2Type[fname].ToLower()))
                    {
                        if ("guid".Contains(dicFName2Type[fname].ToLower()) && HiddenFieldsValue[fname] == null)
                            values += ",null";
                        else
                            values += ",'" + HiddenFieldsValue[fname].ToString()+ "'";
                    }
                    else if ("int,decimal,float,int32".Contains(dicFName2Type[fname].ToLower()))
                    {
                        values += "," + HiddenFieldsValue[fname].ToString();
                    }
                    else if ("datetime".Contains(dicFName2Type[fname].ToLower()))
                    {
                        values += ",'" + HiddenFieldsValue[fname].ToString() + "'";
                    }
                    else if ("boolean" == dicFName2Type[fname].ToLower())
                    {
                        if (Convert.ToBoolean(HiddenFieldsValue[fname]))
                            values += ",1";
                        else
                            values += ",0";
                    }
                    
                }

                fields = "(" + fields.Substring(1) + ")";
                values = "(" + values.Substring(1) + ")";

                sql = sql + fields + " values" + values;

                int row = ExceuteSql(sqlconn, sql);
                return row;
            }
            catch (Exception ex)
            {
                //log.Error(ex.Message);
                return 0;
            }
        }
        public static int updateForAddForm(SqlConnection sqlconn, bsTable bst, List<Control> controls, Dictionary<string, string> dicFName2Type,string tpkvalue, Dictionary<string, object> HiddenFieldsValue)
        {

            string sql = "update " + bst.TName+ " set ";

            Dictionary<string, string> fName2ValuesSql = new Dictionary<string, string>();

            string updateSql = "";

            try
            {
                foreach (Control c in controls)
                {
                    if (c is CheckBox)
                    {
                        CheckBox cb = c as CheckBox;
                        if ("boolean" == dicFName2Type[c.Name].ToLower())
                        {
                            if (cb.Checked)
                                updateSql += "," + c.Name + "=1";
                            else
                                updateSql += "," + c.Name + "=0";

                        }
                    }
                    else if (c is ComboBox)
                    {
                        #region Combox处理
                        ComboBox cb = c as ComboBox;
                        string selectV = cb.Text;
                        if (cb.Tag.ToString() != "")
                        {
                            string[] items = cb.Tag.ToString().Split(new char[] { ',' });
                            int selectedindex = cb.SelectedIndex;
                            int condindex = selectedindex >= items.Length - 1 ? items.Length - 1 : selectedindex;
                            selectV = items[condindex];
                        }
                        if (c.Text.Trim() == "")
                        {
                            updateSql += "," + c.Name + "=null";
                        }
                        else
                        {

                            if ("string,guid".Contains(dicFName2Type[c.Name].ToLower()))
                            {

                                updateSql += "," + c.Name + "='" + selectV + "'";
                            }
                            else if ("int,decimal,float,int32".Contains(dicFName2Type[c.Name].ToLower()))
                            {
                                updateSql += "," + c.Name + "=" + selectV;
                            }
                            else if ("datetime".Contains(dicFName2Type[c.Name].ToLower()))
                            {
                                updateSql += "," + c.Name + "='" + selectV + "'";
                            }
                            else if ("boolean" == dicFName2Type[c.Name].ToLower())
                            {
                                if (c.Text == "False")
                                    updateSql += "," + c.Name + "=0";
                                else
                                    updateSql += "," + c.Name + "=1";

                            }
                        }
                        #endregion
                    }
                    else
                    {

                        if (c.Text.Trim() == "")
                        {
                            updateSql += "," + c.Name + "=null";
                        }
                        else
                        {
                            if ("string,guid".Contains(dicFName2Type[c.Name].ToLower()))
                            {

                                updateSql += "," + c.Name + "='" + c.Text.Replace("'", "''") + "'";
                            }
                            else if ("int,decimal,float,int32".Contains(dicFName2Type[c.Name].ToLower()))
                            {
                                updateSql += "," + c.Name + "=" + c.Text;
                            }
                            else if ("datetime".Contains(dicFName2Type[c.Name].ToLower()))
                            {
                                updateSql += "," + c.Name + "='" + c.Text + "'";
                            }
                            else if ("boolean" == dicFName2Type[c.Name].ToLower())
                            {
                                if (c.Text == "False")
                                    updateSql += "," + c.Name + "=0";
                                else
                                    updateSql += "," + c.Name + "=1";

                            }
                        }
                    }
                }

                //隐藏域已经赋值的字段
                foreach (string fname in HiddenFieldsValue.Keys)
                {
                    
                    if ("string,guid,datetime".Contains(dicFName2Type[fname].ToLower()))
                    {
                        if ("guid".Contains(dicFName2Type[fname].ToLower()) && HiddenFieldsValue[fname] == null)
                            updateSql += "," + fname + "=null";
                        else
                            updateSql += "," + fname + "='" + HiddenFieldsValue[fname]+"'";
                    }
                    else if ("int,decimal,float,int32,boolean".Contains(dicFName2Type[fname].ToLower()))
                    {
                        updateSql += "," + fname + "=" + HiddenFieldsValue[fname];
                    }
                }

                sql = sql + updateSql.Substring(1) + "  where "+bst.TPk+"="+ tpkvalue;

                int row = ExceuteSql(sqlconn, sql);
                return row;
            }
            catch (Exception ex)
            {
                //log.Error(ex.Message);
                return 0;
            }
        }
        public static int UpdateDr(SqlConnection sqlConn, DataRow dr, bsTable bst)
        {
            try
            {
                int row = 0;
                DataTable dt = dr.Table;
                string sqls = "update " + bst.TName + " set ";
                string fieldvalues = "";

                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName == bst.TPk)
                        continue;
                    if (dr[dc].ToString().Trim() == "")
                    {
                        fieldvalues += "," + dc.ColumnName + "=null";
                    }
                    else
                    {
                        if ("string,guid".Contains(dc.DataType.Name.ToLower()))
                        {
                            fieldvalues += "," + dc.ColumnName + "='" + dr[dc].ToString() + "'";
                        }
                        else if ("int,decimal,float,int32".Contains(dc.DataType.Name.ToLower()))
                        {
                            fieldvalues += "," + dc.ColumnName + "=" + dr[dc].ToString() + "";

                        }
                        else if ("datetime".Contains(dc.DataType.Name.ToLower()))
                            fieldvalues += "," + dc.ColumnName + "='" + dr[dc].ToString() + "'";
                        else if ("boolean" == dc.DataType.Name.ToLower())
                        {
                            if (dr[dc].ToString() == "False")
                                fieldvalues += "," + dc.ColumnName + "=0";
                            else
                                fieldvalues += "," + dc.ColumnName + "=1";
                        }
                        else
                        {
                            fieldvalues += "," + dc.ColumnName + "='" + dr[dc].ToString() + "'";
                        }
                    }

                }
                //获取再读取一次数据库库，判断是guid还是int，目前只处理int

                if (bst.TPkType.ToLower() == "uniqueidentifier")
                    sqls = sqls + fieldvalues.Substring(1) + "  where  " + bst.TPk + "='" + dr[bst.TPk].ToString() + "'";
                else if (bst.TPkType.ToLower() == "int")
                    sqls = sqls + fieldvalues.Substring(1) + "  where  " + bst.TPk + "=" + dr[bst.TPk].ToString();
                else
                    sqls = sqls + fieldvalues.Substring(1) + "  where  " + bst.TPk + "='" + dr[bst.TPk].ToString() + "'";

                
                row = ExceuteSql(sqlConn, sqls);
                return row;
            }
            catch (Exception ex) {
                return 0;
            }
        }


 
    }
}
