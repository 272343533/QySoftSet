using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using log4net;


namespace QyTech.ExcelOper
{

    public partial class QyExcelHelper
    {
        static ILog log = LogManager.GetLogger("QyExcelHelper");

        public delegate void delegateCopyHandler(long copycount, long Total);
        public event delegateCopyHandler CopiedEvent;

        DataTable dt = new DataTable();

        string connString = "server = (local); uid = sa; pwd = sa; database = db_test";
        SqlConnection conn;

        /// <summary>
        /// 快速导入，要求顺序必须一致
        /// </summary>
        /// <param name="excelFile"></param>
        /// <param name="sheetName"></param>
        /// <param name="connectionString">IP，账号，密码，数据库，同名sheet表</param>
        public void TransferDataWithBulkCopy(string excelFile, string sheetName, string connectionString)
        {
             try
            {
                FillDataTable(excelFile, sheetName);
                ////如果目标表不存在则创建,excel文件的第一行为列标题,从第二行开始全部都是数据记录     
                //string strSql = string.Format("if not exists(select * from sysobjects where name = '{0}') create table {0}(", sheetName);   //以sheetName为表名     

                //foreach (System.Data.DataColumn c in dt.Columns)
                //{
                //    strSql += string.Format("[{0}] varchar(255),", c.ColumnName);
                //}
                //strSql = strSql.Trim(',') + ")";

                //using (System.Data.SqlClient.SqlConnection sqlconn = new System.Data.SqlClient.SqlConnection(connectionString))
                //{
                //    sqlconn.Open();
                //    System.Data.SqlClient.SqlCommand command = sqlconn.CreateCommand();
                //    command.CommandText = strSql;
                //    command.ExecuteNonQuery();
                //    sqlconn.Close();
                //}
                //用bcp导入数据        
                //excel文件中列的顺序必须和数据表的列顺序一致，因为数据导入时，是从excel文件的第二行数据开始，不管数据表的结构是什么样的，反正就是第一列的数据会插入到数据表的第一列字段中，第二列的数据插入到数据表的第二列字段中，以此类推，它本身不会去判断要插入的数据是对应数据表中哪一个字段的     
                using (System.Data.SqlClient.SqlBulkCopy bcp = new System.Data.SqlClient.SqlBulkCopy(connectionString))
                {
                    bcp.SqlRowsCopied += new System.Data.SqlClient.SqlRowsCopiedEventHandler(bcp_SqlRowsCopied);
                    bcp.BatchSize = 100;//每次传输的行数        
                    bcp.NotifyAfter = 100;//进度提示的行数        
                    bcp.DestinationTableName = sheetName;//目标表        
                    bcp.WriteToServer(dt);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        //进度显示        
        public void bcp_SqlRowsCopied(object sender, System.Data.SqlClient.SqlRowsCopiedEventArgs e)
        {
            if (CopiedEvent != null)
                CopiedEvent(e.RowsCopied, dt.Rows.Count);

        }

        /// <summary>
        /// 用sql导入数据
        /// </summary>
        /// <param name="excelFile">excel文件</param>
        /// <param name="sheetName">sheet名称</param>
        /// <param name="connectionString">sql连接串</param>
        /// <param name="ColRel">数据库列与excel列对应关系</param>
        /// <returns></returns>
        public string TransferDataWithSql(string excelFile, string sheetName, string connectionString,string TName,Dictionary<string,string> FName2ExcelColName,bool delTableData=false,string ExcelNotNullCols="",string where="")
        {
            FillDataTable(excelFile, sheetName);

            conn = new SqlConnection(connectionString);
            conn.Open();
            if (delTableData)
            {
                SqlCommand cmd = new SqlCommand("delete  from "+TName+where==""?"":"where "+where, conn);
                cmd.ExecuteNonQuery();
            }
            if (dt.Rows.Count > 0)
            {
                DataRow dr = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    insertToSql(dr, TName, FName2ExcelColName, ExcelNotNullCols);
                    if (CopiedEvent != null)
                        CopiedEvent(i+1, dt.Rows.Count);
                }
            }
            return "";
        }

        /// <summary>
        /// 将dt中的数据导入数据库
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="connectionString"></param>
        /// <param name="TName"></param>
        /// <param name="FName2ExcelColName"></param>
        /// <param name="delTableData"></param>
        /// <returns></returns>
        public DataTable TransferDataWithSql(DataTable dtData, string connectionString, string TName, Dictionary<string, string> FName2ExcelColName, bool delTableData = false, string ExcelNotNullCols = "")
        {
            DataRow drWrong;
            DataTable dtWrong = dtData.Clone();
            this.dt = dtData;
            conn = new SqlConnection(connectionString);
            conn.Open();
            if (delTableData)
            {
                SqlCommand cmd = new SqlCommand("delete  from " + TName, conn);
                cmd.ExecuteNonQuery();
            }
            if (dt.Rows.Count > 0)
            {
                DataRow dr = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    drWrong=insertToSql(dr, TName, FName2ExcelColName, ExcelNotNullCols);
                    if (drWrong != null)
                    {
                        DataRow drcopy = dtWrong.NewRow();

                        foreach (DataColumn dc in dtWrong.Columns)
                        {
                            drcopy[dc.ColumnName] = dr[dc.ColumnName];
                        }
                        dtWrong.Rows.Add(drcopy);
                    }
                    if (CopiedEvent != null)
                        CopiedEvent(i + 1, dt.Rows.Count);
                }
            }
            return dtWrong;
        }
        private DataRow CopyDataRow(DataRow dr)
        {
            DataRow drcopy = dr.Table.Clone().NewRow();

            foreach (DataColumn dc in dr.Table.Columns)
            {
                drcopy[dc] = dr[dc];
            }
            return drcopy;
        }
        /// <summary>
        /// 数据库脚本导入excel数据, 对已存在数据的修改怎么处理呢？让用户自己决定是否直接删除？
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <param name="ColRel">列关系</param>
        private DataRow insertToSql(DataRow dr,string TName,Dictionary<string, string> ColRel,string ExcelNotNullCols="")
        {
            string sql = "insert into " + TName;
            //excel表中的列名和数据库中的列名一定要对应  
            string fields = "",values="";

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
                    fields += ","+key;
                    values += ",'" + dr[ColRel[key]].ToString()+"'";
                
                }
            
                fields = "("+fields.Substring(1)+")";
                values = "(" + values.Substring(1) + ")";

                sql = sql + fields +" values"+ values;
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                return null;
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
                return dr;
            }
        }

       


        private void FillDataTable(string excelFile, string sheetName="")
        {
            //获取全部数据   
            string strCon = "provider=Microsoft.Ace.OleDb.12.0;data source=" + excelFile + ";extended properties='excel 12.0;HDR=YES;IMEX=1'";//关键是红色区域
            OleDbConnection Con = new OleDbConnection(strCon);//建立连接
            if (sheetName == "") { 
                Con.Open();
                System.Data.DataTable dt = Con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                sheetName = dt.Rows[0][2].ToString().Trim();
                Con.Close();
            }
            string strSql = "select * from [" + sheetName + "]";//表名的写法也应注意不同，对应的excel表为sheet1，在这里要在其后加美元符号$，并用中括号
            OleDbCommand Cmd = new OleDbCommand(strSql, Con);//建立要执行的命令
            OleDbDataAdapter da = new OleDbDataAdapter(Cmd);//建立数据适配器
            DataSet ds = new DataSet();
            ds.Tables.Clear();
            da.Fill(ds, sheetName);//把数据适配器中的数据读到数据集中的一个表中（此处表名为shyman，可以任取表名）

            dt = ds.Tables[0];

        }
    }
}

