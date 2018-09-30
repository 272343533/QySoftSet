using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

using QyTech.Auth.Dao;
using System.Windows.Forms;

namespace QyTech.DbUtils
{
    public class SqlUtils
    {

        public static DataTable GetDataTable(SqlConnection sqlConn, string tName, string where, string orderby = "")
        {
            DataTable dtList = null;
            try
            {

                string strSql = "select * from [" + tName + "] " + (where == "" ? "" : " where " + where) + (orderby == "" ? "" : " order by " + orderby);//表名的写法也应注意不同，对应的excel表为sheet1，在这里要在其后加美元符号$，并用中括号
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
                throw new Exception("RefreshDgv:" + ex.Message);
            }
        }

        public static int ExceuteSql(SqlConnection sqlconn, string strSql)
        {
            int ret = -1;
            sqlconn.Open();
            try
            {
                SqlCommand Cmd = new SqlCommand(strSql, sqlconn);//建立要执行的命令
                ret = Cmd.ExecuteNonQuery();
                sqlconn.Close();
                return ret;
            }
            catch (Exception ex)
            {
                ret= - 1;
            }
            finally
            {
                sqlconn.Close();
            }
            return ret;
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
        public static int updateForAddForm(SqlConnection sqlconn, bsTable bst, List<Control> controls, Dictionary<string, string> dicFName2Type,string tpkvalue)
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

                                updateSql += "," + c.Name + "='" + c.Text + "'";
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
        public static T DataRow2EntityObject<T>(DataRow r)
        {
            T t = default(T);
            t = Activator.CreateInstance<T>();
            PropertyInfo[] ps = t.GetType().GetProperties();
            foreach (var item in ps)
            {
                if (r.Table.Columns.Contains(item.Name))
                {
                    object v = r[item.Name];
                    if (v.GetType() == typeof(System.DBNull))
                        v = null;
                    item.SetValue(t, v, null);
                }
            }
            return t;
        }

    }
}
