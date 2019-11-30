using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using qyExpress.Dao;


namespace QyTech.Core.Common
{
    //系统加载时具有的的权限
    public class InnerAccountFilter
    {
        private bsUser bsuserobj_;
        private bsAppName appnameobj_;

        private string App_ = "";
        private string Area_ = "";
        private string Navi_ = "";
        private string Cust_ = "";
        
        private string Db_ = "";
        private string DTable_ = "";
        //private string DField_ = "";
        //private string DTInterface_ = "";

        private string FunConf_ = "";
        //private string FunField_ = "";
        //private string FunQuery_ = "";
        //private string FunOperation_ = "";

        public string App { get { return App_; } }
        public string Area
        {
            get { return Area_; }
        }
        public string Navi
        {
            get { return Navi_; }
        }
        public string Cust
        {
            get { return Cust_; }
        }

        public string Db
        {
            get { return Db_; }
        }
        public string DTable
        {
            get { return DTable_; }
        }
        //public string DField
        //{
        //    get { return DField_; }
        //}
        //public string DTInterface
        //{
        //    get { return DTInterface_; }
        //}
        public string FunConf
        {
            get { return FunConf_; }
        }
        //public string FunField
        //{
        //    get { return FunField_; }
        //}
        //public string FunQuery
        //{
        //    get { return FunQuery_; }
        //}
        //public string FunOperation
        //{
        //    get { return FunOperation_; }
        //}
        //public string bsOFilter
        //{
        //    get;set;
        //}
        public InnerAccountFilter(bsUser user)
        {
            bsuserobj_ = user;
            if (bsuserobj_.LoginName == InnerAccout.expressAdminUser.LoginName || bsuserobj_.LoginName == InnerAccout.devAdminUser.LoginName)
            {
                if (bsuserobj_.LoginName == InnerAccout.expressAdminUser.LoginName)
                {
                    App_ = "AppName='系统配置'";
                }
                else
                {
                    App_ = "(AppName!='系统配置')";
                }
                Cust_ = App_;

            }
        }
        public InnerAccountFilter(bsUser user,bsAppName app)
        {
            bsuserobj_ = user;
            appnameobj_ = app;
            if (bsuserobj_.LoginName == InnerAccout.expressAdminUser.LoginName || bsuserobj_.LoginName== InnerAccout.devAdminUser.LoginName)
            {
                #region 应用的条件
                App_ = "(AppName='"+app.AppName+"')";
                Area_ = App_;
                Cust_ = App_;
                Db_ = App_;
                Navi_ = App_;
                DTable_ = "((bsD_Id in (select bsD_Id from bsDb where " + Db_ + " ))";//应用的表
                #endregion


                #region 系统配置表中的
                if (bsuserobj_.LoginName == InnerAccout.expressAdminUser.LoginName)
                {
                    Db_ += " or (bsD_Id='0617C6BE-62E5-4CB0-90E1-662387C6F4D3')";
                    DTable_ += " or  (bsD_Id='0617C6BE-62E5-4CB0-90E1-662387C6F4D3' and TRank<=100))";  //
                }
                else
                {
                    Db_ += " or (bsD_Id='0617C6BE-62E5-4CB0-90E1-662387C6F4D3')";
                    DTable_ += " or (bsD_Id='0617C6BE-62E5-4CB0-90E1-662387C6F4D3' and TRank<100))";
                }
                FunConf_ = "bsN_Id in (select bsN_Id from bsNavigation where " + Navi_ + " )";

                #endregion
            }
            else
            {
                App_ = "-1";
                Area_ = "-1";
                Navi_ = "-1";
                Cust_ = "-1";

                Db_ = "-1";
                DTable_ = "-1";
                FunConf_ = "-1";
            }

        }
        public InnerAccountFilter(bsUser user, string appname)
        {
            bsuserobj_ = user;

            if (bsuserobj_.LoginName == InnerAccout.expressAdminUser.LoginName || bsuserobj_.LoginName == InnerAccout.devAdminUser.LoginName)
            {
                #region 应用的条件
                App_ = "(AppName='" + appname + "')";
                Area_ = App_;
                Cust_ = App_;
                Db_ = App_;
                Navi_ = App_;
                DTable_ = "((bsD_Id in (select bsD_Id from bsDb where " + Db_ + " ))";//应用的表
                #endregion


                #region 系统配置表中的
                if (bsuserobj_.LoginName == InnerAccout.expressAdminUser.LoginName)
                {
                    Db_ += " or (bsD_Id='0617C6BE-62E5-4CB0-90E1-662387C6F4D3')";
                    DTable_ += " or  (bsD_Id='0617C6BE-62E5-4CB0-90E1-662387C6F4D3' and TRank<=100))";  //
                }
                else
                {
                    Db_ += " or (bsD_Id='0617C6BE-62E5-4CB0-90E1-662387C6F4D3')";
                    DTable_ += " or (bsD_Id='0617C6BE-62E5-4CB0-90E1-662387C6F4D3' and TRank<100))";
                }
                FunConf_ = "bsN_Id in (select bsN_Id from bsNavigation where " + Navi_ + " )";

                #endregion
            }
            else
            {
                App_ = "-1";
                Area_ = "-1";
                Cust_ = "-1";
                Db_ = "-1";
                FunConf_ = "-1";
                //用户具有的角色具有的功能
                Navi_ = "-1";
                
                DTable_ = "-1";



            }
        }
        }
}
