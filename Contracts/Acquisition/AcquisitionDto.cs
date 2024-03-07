using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Acquisition
{
    public class AcquisitionDto : BaseEntityDto
    {
        public long orgid { get; set; }
        public string orgcode { get; set; }
        public string orgname { get; set; }
        public string org_type { get; set; }
        public string mobilenumber { get; set; }
        public string Status { get; set; }

    }
}
