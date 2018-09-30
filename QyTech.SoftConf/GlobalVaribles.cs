using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QyTech.Auth.Dao;
using QyTech.Core.BLL;
using System.Data;
using System.Data.SqlClient;
namespace QyTech.SoftConf
{
    public class GlobalVaribles
    {
        public static frmMain mdiform;


        private static bsAppName _currAppObj;
        public static bsAppName currAppObj {
            get { return _currAppObj; }
            set { _currAppObj = value; mdiform.tsslAppName.Text = "当前应用："+_currAppObj.AppName; }
        }

        public static bsDb currDbObj;

        public static EntityManager EM_Base = new EntityManager(new QyTech.Auth.Dao.QyTech_AuthEntities());
        public static EntityManager EM_App;//= new EntityManager(new QyTech.Auth.Dao.QyTech_AuthEntities());
        public static SqlConnection SqConn_Base= new SqlConnection("server =122.114.190.250,2433; uid = sa; pwd = Qy_ltd414; database = QyTech_Auth");//考虑从配置文件中，不过密码不安全
        public static SqlConnection SqConn_App= new SqlConnection("server =122.114.190.250,2433; uid = sa; pwd = Qy_ltd414; database = wj_GisDb");//考虑从配置文件中，不过密码不安全


    }
}
