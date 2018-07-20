using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QyTech.Core.ExController.Bll
{

    /// <summary>
    /// 页面查询条件布局,右侧为0，左侧为16，16位无符号整数
    /// 0位：0:query 1:edit 页面是查询还是编辑
    /// 1位：0：list 1:form 数据部分是表格还是form形式
    /// 2位：主布局：0：上下，1：左右
    /// 3位：
    /// 4位：0：无  1：有   顶部是否有查询条件
    /// 5位：0：无  1：有   左部是否有查询条件   
    /// 6位：0：无  1：有   右部是否有查询条件
    /// 7位：0：无  1：有   下部是否有查询条件

    /// 
    /// 布局   list/form    条件        使用场景举例
    /// 0     列表查看 （条件：无）： 
    /// 1     列表编辑 （条件：无）：  无条件的网格编辑
    /// 2     Form查看 （条件：无）：  普通的详细信息查看
    /// 3     Form编辑 （条件：无）：  普通的add页面
    /// 
    /// 16    列表查看 （条件：上）：   上部有条件的主页面浏览
    /// 35    Form编辑 （条件：左）：   树形结构编辑
    /// 48   列表查看 （条件：左上）：  左侧树，上部有条件的一般主页面浏揽
    ///       



    public enum PageLayoutOutLine{rightleft=1,topbottom=2}

    public enum ItemPos { none=0,top = 1, bottom, left, right, rowend }

    public enum PageType { model = 1, newpage }

    public enum ColumnType { col_field = 1, col_operation };
    
    public enum FDataType { DT_guid, DT_string = 1, DT_decimal, DT_int, DT_date, DT_datetime, DT_time, DT_bool};

    public enum FDataEditType { ET_hidden = 1, ET_text, ET_mtext, ET_date, ET_checkbox, ET_radiobutton, ET_select, ET_password, DT_ajax, DT_function }

    public enum FDataQueryType { QT_tree,QT_mtree, QT_grid,QT_mgrid, QT_text, QT_date, QT_datetime, QT_time, QT_checkBox, QT_radiobutton, QT_select, QT_daterange, QT_datetime_range, QT_time_range }

    public class FDataInputVerity 
    {
        public FDataType fdatatype;
        public int minlength;
        public int maxlength;
    }

    public class FunQueryCondition
    {
        public FunQueryCondition()
        {
            Top = new List<FunQueryItem>();
            Bottom = new List<FunQueryItem>();
            Left = new List<FunQueryItem>();
            Right = new List<FunQueryItem>();
        }
        public PageLayoutOutLine Layout;
        public ushort PageLayout;
        public List<FunQueryItem> Top;
        public int TopHeight;
        public List<FunQueryItem> Bottom;
        public int BottomHeight;
        public List<FunQueryItem> Left;
        public int LeftWidth;
        public List<FunQueryItem> Right;
        public int RightWidth;

    }

    /// <summary>
    /// 功能的查询条件
    /// </summary>
    public class FunQueryItem
    {
        public string NameTip { get; set; }
        public string Name { get; set; }
        public FDataQueryType InputType { get; set; }
        //public string Postion { get; set; }
        public string DataEx { get; set; }
        public string DefaultValue { get; set; }
        public int Size { get; set; }
    }

   
    /// <summary>
    /// 功能操作
    /// </summary>
    public class FunOperation
    {
        public string btnNameTip { get; set; }
        public string btnType { get; set; }
        public string btnSize { get; set; }

        public string btnClass { get; set; }

        public string btnClickType { get; set; } //类型，get：请求页面 post：提交操作
        public string btnClickUrl { get; set; }  //按钮对应的操作地址
        public string getPagePostUrl { get; set; }
        public PageType pageType { get; set; }
        public ItemPos btnPos { get; set; }      //按钮位置

    }

    public class listReadDataItemSet
    {
        /// <summary>
        /// 项描述
        /// </summary>
        public string FDesp { get; set; }
        /// <summary>
        /// 字段列火操作列
        /// </summary>
        public ColumnType columnType { get; set; }
        /// <summary>
        /// 项字段名
        /// </summary>
        public string FName { get; set; }
        /// <summary>
        /// 项宽度
        /// </summary>
        public int FWidth { get; set; }

        //主要用于提示主键，外键类的guid不显示，但是要传递到前端
        public bool FVisible { get; set; }

        public List<FunOperation> RowOpers { get; set; }
    }

    public class formReadDataItemSet
    {
        /// <summary>
        /// 项描述
        /// </summary>
        public string FDesp { get; set; }
        /// <summary>
        /// 项字段名
        /// </summary>
        public string FName { get; set; }
        /// <summary>
        /// 项宽度
        /// </summary>
        public int FWidth { get; set; }

        //主要用于提示主键，外键类的guid不显示，但是要传递到前端
        public bool FVisible { get; set; }

    }


    public class listWriteDataItemSet
    {
        /// <summary>
        /// 项描述
        /// </summary>
        public string FDesp { get; set; }

        public ColumnType columnType { get; set; }
        /// <summary>
        /// 项字段名
        /// </summary>
        public string FName { get; set; }
        /// <summary>
        /// 项宽度
        /// </summary>
        public int FWidth { get; set; }

        /// <summary>
        /// 数据验证方式
        /// </summary>
        public FDataInputVerity FDataVerity { get; set; }

        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool FEditable { get; set; }

        /// <summary>
        /// 默认值,尤其用于不可编辑时的值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 编辑方式类型组：Hidden,Text,MText,DatePicker,CheckBox,RadioButton,
        /// </summary>
        public FDataEditType FEditType { get; set; } //包括

        /// <summary>
        /// 非输入时的数据源（值-显示）： select(男:男;女:女),checkBox（足球:足球;篮球:篮球）,radiobutton,ajax_url(/bsOrganize/TreeDis)
        /// </summary>
        public string FEditValueEx { get; set; }


        //主要用于提示主键，外键类的guid不显示，但是要传递到前端
        public bool FVisible { get; set; }

        public bool FRequired { get; set; }

        public List<FunOperation> RowOpers { get; set; }
    }

   
    public class formWriteDataItemSet
    {
        /// <summary>
        /// 项描述
        /// </summary>
        public string FDesp { get; set; }

         /// <summary>
        /// 项字段名
        /// </summary>
        public string FName { get; set; }
        /// <summary>
        /// 项宽度
        /// </summary>
        public int FWidth { get; set; }

        /// <summary>
        /// 数据验证方式
        /// </summary>
        public FDataInputVerity FDataVerity { get; set; }

        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool FEditable { get; set; }

        /// <summary>
        /// 默认值,尤其用于不可编辑时的值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 编辑方式类型组：Hidden,Text,MText,DatePicker,CheckBox,RadioButton,
        /// </summary>
        public FDataEditType FEditType { get; set; } //包括

        /// <summary>
        /// 非输入时的数据源（值-显示）： select(男:男;女:女),checkBox（足球:足球;篮球:篮球）,radiobutton,ajax_url(/bsOrganize/TreeDis)
        /// </summary>
        public string FEditValueEx { get; set; }


        //主要用于提示主键，外键类的guid不显示，但是要传递到前端
        public bool FVisible { get; set; }

        public bool FRequired { get; set; }

    }
    /// <summary>
    /// 数据项的设定，包括表格头各列的设定
    /// </summary>
    public class DataItemSet
   {
    ////    /// <summary>
    ////    /// 项描述
    ////    /// </summary>
    ////    public string FDesp { get; set; }

    ////    public ColumnType columnType { get; set; }
        /// <summary>
        /// 项字段名
        /// </summary>
        public string FName { get; set; }
        /////// <summary>
        /////// 项宽度
        /////// </summary>
        ////public int FWidth { get; set; }

        /////// <summary>
        /////// 数据类型
        /////// </summary>
        ////public FDataType FDatatype { get; set; }

        /////// <summary>
        /////// 是否可编辑
        /////// </summary>
        ////public bool FEditable { get; set; }

        /////// <summary>
        /////// 默认值,尤其用于不可编辑时的值
        /////// </summary>
        ////public string DefaultValue { get; set; }

        /////// <summary>
        /////// 编辑方式类型组：Hidden,Text,MText,DatePicker,CheckBox,RadioButton,
        /////// </summary>
        ////public FDataEditType FEditType { get; set; } //包括

        /////// <summary>
        /////// 非输入时的数据源（值-显示）： select(男:男;女:女),checkBox（足球:足球;篮球:篮球）,radiobutton,ajax_url(/bsOrganize/TreeDis)
        /////// </summary>
        ////public string FEditValueEx { get; set; }


        //////主要用于提示主键，外键类的guid不显示，但是要传递到前端
        ////public bool FVisible { get; set; }

        ////public bool FRequired { get; set; }

        ////public List<FunOperation> RowOpers { get; set; }
    }



    public class keyVal
    {
        public string key { get; set; }
        public string val { get; set; }
    }
    class bllCommType
    {
    }
}
