using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Bbps
{
    public class ParamInfo
    {
        public string paramName { get; set; }
        public string dataType { get; set; }
        public string isOptional { get; set; }
        public string minLength { get; set; }
        public string maxLength { get; set; }
        //public string regEx { get; set; }
    }
}
