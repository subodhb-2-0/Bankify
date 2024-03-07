using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class DynamicSearchComissionReceiveableModelDto 
    {
        public long totalcount { get; set; }
        public string crname { get; set; }
        public int crid { get; set; }
        public int crstatus { get; set; }
        public DateTime creationdate { get; set; }
    }
}
