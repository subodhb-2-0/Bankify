using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Product
{
    public class ServiceCSMListDto
    {
        public long CSMId { get; set; }
        public string CSMName { get; set; }
        public int status { get; set; }
    }
}
