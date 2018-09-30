using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QyTech.Core.BLL;

using QyTech.Auth.Dao;
using QyTech.SkinForm;
using QyTech.SkinForm.Component;
using QyTech.SkinForm.Controls;
using QyTech.UICreate.Util;

namespace QyTech.UICreate
{
    public partial class qyfLayoutList : qyfLayoutListParent
    {

       
        /// <summary>
        /// 子类界面不显示，需要加这个构造函数
        /// </summary>
        public qyfLayoutList()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="em_Base">基本EM</param>
        /// <param name="em_App">应用EM</param>
        /// <param name="conn">应用EM的SqlConn</param>
        /// <param name="tname">表或视图对象</param>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        public qyfLayoutList(EntityManager em_Base, EntityManager em_App,SqlConnection conn,Guid bsFC_Id,string where="",string orderby="")
            :base(em_Base, em_App, conn, bsFC_Id, where, orderby)
        {
            InitializeComponent();

            this.scForm.SplitterDistance = 0;
        }

    }
}
