using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QyTech.Core.Common
{
    #region 确认使用中
    public enum FlowStatus
    {
        None = 0, 未提交, 已提交, 已审核, 已退回
    }

    #endregion  

    public enum RightType { CustFuns = 1, RoleNaviFuns = 10, RoleFunOpers, RoleTFs, UserOrgs = 100, UserRoles }


    public enum LoginStatus { None=0,Logining = 1,Success=2,Exit=10}

    /// <summary>
    /// Url类型，Dao访问还是UI返回
    /// </summary>
    public enum UrlType { None=0,UI=1, DAO}

    /// <summary>
    /// 默认的数据接口
    /// </summary>
    public enum DaoDefaultInterface { Nadd = 1, edit = 2, delete = 4, getall = 8, getallwithpaging = 16, getone = 32, gettree = 64, getallbytfk = 128, import = 256, export = 512,Audit=1024 }
    public enum DaoDefaultInterfaceName { 新增 = 1, 修改 = 2, 删除 = 4, 获取所有 = 8, 分页获取 = 16, 获取单个 = 32, 获取树列表 = 64, 根据默认外键获取 = 128, Excel导入 = 256, Excel导出 = 512, 审核 = 1024 }

    /// <summary>
    /// 编辑类型
    /// </summary>
    public enum EditType
    {
        //文本   单选      复选    下拉     隐藏     文件
        text = 1, radio, checkbox, select, hidden, fileup
    }

    /// <summary>
    /// 操作类型
    /// </summary>
    public enum ActionType
    {
        新增, 删除, 编辑, 审核, Excel导入, Excel导出
    }

    public enum AddorModify
    {
        Add = 1, Modify
    }



    /// <summary>
    /// 字段类型
    /// </summary>
    public enum FieldType { Uniqueidentifier, Varchar, Datetime, Date, Int, Bigint, Bit, Decimal }
















    /// <summary>
    /// 树节点定义
    /// </summary>
    public class qytvNode
    {
        public string id { get; set; }
        public string name { get; set; }
        public string pId { get; set; }
        public string type { get; set; }//类型
        public string exInfo { get; set; }//其它信息
        public string exInfo2 { get; set; }//其它信息
        public string exInfo3 { get; set; }//其它信息
        public string exInfo4 { get; set; }//其它信息
        public string exInfo5 { get; set; }//其它信息

        public bool checkFlag { get; set; }//是否选中
        public bool open { get; set; }//是否expand


        public bool addBtnFlag { get; set; }
        public bool removeBtnFlag { get; set; }
        public bool editBtnFlag { get; set; }
    }


    /// <summary>
    /// 用于键值对的json串转换使用(集合有KeyValuePair对象 是否可以直接使用，)
    /// </summary>
    //public class keyVal
    //{
    //    public string key { get; set; }
    //    public string val { get; set; }
    //}


}
