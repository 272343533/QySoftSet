using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace QyTech.LtdUp2.Wcf
{
    [DataContract]
    public class MessageEntity
    {
        [DataMember]
        public string Content { get; set; }
    }

}
