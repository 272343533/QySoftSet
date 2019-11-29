using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace QyTech.Utils
{
    public class SqlUtils
    {
        public static int ExceuteSql(SqlConnection sqlconn, string strSql)
        {
            try
            {
                sqlconn.Open();
                SqlCommand Cmd = new SqlCommand(strSql, sqlconn);//建立要执行的命令
                int ret = Cmd.ExecuteNonQuery();
                sqlconn.Close();
                return ret;
            }
            catch (Exception ex)
            {
                return -1;
            }


        }
    }
}
