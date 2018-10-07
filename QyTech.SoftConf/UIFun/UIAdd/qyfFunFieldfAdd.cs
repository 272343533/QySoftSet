using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using QyTech.UICreate;
namespace QyTech.SoftConf.UIAdd
{
    public partial class qyfFunFieldfAdd : qyfAdd
    {
        ComboBox cbo_FEditType;
        TextBox txt_FEditValueEx;

        public qyfFunFieldfAdd()
        {
            InitializeComponent();
        }

        private void qyfForm_Load(object sender, EventArgs e)
        {
            //添加需要的事件
            //1。 当表名修改后，对应的字段信息也要修改
            //可是还没创建，事件怎么写呢
            try
            {
                cbo_FEditType = dicControls["FEditType"] as ComboBox;
                cbo_FEditType.SelectedIndexChanged += new EventHandler(FEditType_SelectChange);
                txt_FEditValueEx = dicControls["FEditValueEx"] as TextBox;

                if (cbo_FEditType.Text == "select")
                    txt_FEditValueEx.Enabled = true;
                else
                    txt_FEditValueEx.Enabled = false;
            }
            catch { }
        }




        private void FEditType_SelectChange(object sender,EventArgs e)
        {
            try
            {
                if (cbo_FEditType.Text == "select")
                    txt_FEditValueEx.Enabled = true;
                else
                    txt_FEditValueEx.Enabled = false;
            }
            catch
            { }
        }
    }
}
