using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Objects;

using QyExpress.Dao;
using QyTech.Core.BLL;
using QyTech.Core.Common;
using System.Data.SqlClient;
using System.Configuration;
namespace QyTech.SoftConf
{
    public class GlobalVaribles
    {

        public static frmMain mdiform;
        public static bool MainFormLoadFinished = false;
        public static LoginStatus LoginStatus = LoginStatus.None;


        private static bsUser currloginUser_;
        public static InnerAccountFilter currloginUserFilter;


        public static string ServerUrl = ConfigurationManager.AppSettings["ServerUrl"];
        public static string SessionId = "";

        private static bsAppName _currAppObj;
        

        /// <summary>
        /// 赋值需要早于currSoftCustomer,涉及到创建LoginUserFilter的创建
        /// </summary>
        public static bsAppName currAppObj
        {
            get { return _currAppObj; }
            set
            {
                _currAppObj = value;
                if (currSoftCutomer_ == null)
                    mdiform.tsslAppName.Text = "当前应用：" + _currAppObj.AppName;
                else
                    mdiform.tsslAppName.Text = "当前应用：" + _currAppObj.AppName + ";当前客户：" + currSoftCutomer_.Name;

                
                currloginUserFilter = new InnerAccountFilter(currloginUser_, currAppObj);

            }
        }
        public static bsDb currDbObj;

        private static bsSoftCustInfo currSoftCutomer_;
        public static bsSoftCustInfo currSoftCutomer
        {
            get { return currSoftCutomer_; }
            set
            {
                currSoftCutomer_ = value;
                mdiform.tsslAppName.Text = "当前应用：" + _currAppObj.AppName + ";当前客户：" + currSoftCutomer_.Name;

                currloginUserFilter.bsOFilter= "bsO_Id in ( select bsO_Id from bsOrganize where bsS_Id='" + GlobalVaribles.currSoftCutomer_.bsS_Id + "')";
            }
        }


        public static SqlConnection SqConn_Base = new SqlConnection("server =122.114.190.250,2433; uid = sa; pwd = Qy_ltd414; database = LtdUp_QyExpress");//考虑从配置文件中，不过密码不安全
        public static SqlConnection SqConn_App = new SqlConnection("server =122.114.190.250,2433; uid = sa; pwd = Qy_ltd414; database = LtdUp_Wj");//考虑从配置文件中，不过密码不安全
        //public static SqlConnection SqConn_Base = new SqlConnection("server =(local); uid = sa; pwd = Qy_ltd414; database = QyExpress");//考虑从配置文件中，不过密码不安全
        //public static SqlConnection SqConn_App = new SqlConnection("server =(local); uid = sa; pwd = Qy_ltd414; database = wj_GisDb");//考虑从配置文件中，不过密码不安全


        public static ObjectContext ObjContext_Base_ = new QyExpress.Dao.QyExpressEntities();
        public static ObjectContext ObjContext_Base
        {
            get {
                //_ObjContext_Base.Dispose();
                //_ObjContext_Base = null;
                ObjectContext _ObjContext_Base1 = new QyExpress.Dao.QyExpressEntities();
                return _ObjContext_Base1;
            }
        }
        private static ObjectContext _ObjContext_App;
        public static ObjectContext ObjContext_App
        {
            get { return _ObjContext_App; }
            set { _ObjContext_App = value; }//记住这个app，然后再get中反射重新生成一个新的对象
        }

        //下面方式适合网站的数据库库访问方式，只需要一个数据库链接的应用
        //public static EntityManager EM_Base = new EntityManager(new QyExpress.Dao.QyTech_AuthEntities());
        //public static EntityManager EM_App;//= new EntityManager(new QyExpress.Dao.QyTech_AuthEntities());

       public static bsUser currloginUser
        {
            get { return currloginUser_; }
            set
            {
                currloginUser_ = value;
            }
        }

    }
}
