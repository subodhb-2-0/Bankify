using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Acquisition
{
    public class getCPDetailsByNameDto
    {
        public int orgid { get; set; }
        public int status { get; set; }
        public string orgcode { get; set; }
        public string orgname { get; set; }
    }
}
