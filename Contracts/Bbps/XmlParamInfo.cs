using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Contracts.Bbps
{
    [Serializable]
    [XmlRoot("ParamInfo")]
    public class XmlParamInfo
    {
        [XmlElement("paramName")]
        public string paramName { get; set; }
        [XmlElement("dataType")]
        public string dataType { get; set; }
        
        [XmlElement("isOptional")]
        public string isOptional { get; set; }

        [XmlElement("minLength")]
        public string minLength { get; set; }

        [XmlElement("maxLength")]
        public string maxLength { get; set; }
    }
}
