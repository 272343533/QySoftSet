using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QyExpress.Dao;

namespace QyTech.UICreate
{
    public abstract class IaddComponent
    {
        public virtual Control Create(GroupBox gbContainer, string labText, string FName, object FValue, int x, int y, object querytag = null, int labwidth = 90, int textwidth = 150, bool Enabled = true)
        {
            return null;
        }
        protected virtual Control Create(TFieldControlConf tfcc)
        {
            return null;
        }
    }

    public class TFieldControlConf
    {
        /// <summary>
        /// 所属groupbox容器
        /// </summary>
        protected Control cParent { get; set; }
        /// <summary>
        /// 提示
        /// </summary>
        protected string labText { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        protected string FName { get; set; }
        /// <summary>
        /// 字段值
        /// </summary>
        protected object FValue { get; set; }
        /// <summary>
        /// 控件开始位置x
        /// </summary>
        protected int x { get; set; }
        /// <summary>
        /// 控件开始位置y
        /// </summary>
        protected int y { get; set; }
        /// <summary>
        /// 查询条件？
        /// </summary>
        protected object querytag = null;
        /// <summary>
        /// 提示label宽度
        /// </summary>
        protected int labwidth = 90;
        /// <summary>
        /// 控件宽度
        /// </summary>
        protected int textwidth = 150;
        /// <summary>
        /// 是否可编辑
        /// </summary>
        protected bool Enabled { get; set; }
    }

}
