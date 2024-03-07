using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class GetbaseParamByIdDto
    {
        public int Id { get; set; }
        public string KeyParam { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
    }
}
