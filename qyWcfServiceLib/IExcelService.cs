using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace QyTech.qyWcfServiceLib
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract(Namespace = "qyTech", CallbackContract = typeof(ICallback))]
    public interface IExcelService
    {
        // TODO: 在此添加您的服务操作
        [OperationContract]
        void Export(string filename, string where, string orderby);
    }
    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void ExportedExcelFinished(string filename);

    }
}
