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
using System.Data.Objects;


namespace QyTech.UICreate
{
    public partial class qyfLayList : qyfLayListParent
    {

        /// <summary>
        /// 子类界面不显示，需要加这个构造函数
        /// </summary>
        public qyfLayList()
        {
            InitializeComponent();
            this.scForm.SplitterDistance = 0;
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
        public qyfLayList(ObjectContext em_Base, ObjectContext em_App, SqlConnection conn, Guid bsFC_Id, string where = "")
            : base(em_Base, em_App, conn, bsFC_Id)
        {
            InitializeComponent();
            this.scForm.SplitterDistance = 0;
        }

    }
}
