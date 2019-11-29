using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QyTech.Core.Helpers
{
    public class PkCreateHelper
    {
        public static Guid GetGuidPk()
        {
            return Guid.NewGuid();
        }
        public static Guid GetQySortGuidPk()
        {
            byte[] objData = Guid.NewGuid().ToByteArray();
            byte[] binaryData = objData as byte[];
            string strHex = BitConverter.ToString(binaryData);
            Guid id = new Guid(strHex.Replace("-", ""));
            return id;
        }
    }
}
