using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Acquisition
{
    public class VerifyChannelPartner
    {
        public long orgId { get; set; }
        public string orgcode { get; set; }
        public string orgName { get; set; }
        public string org_type { get; set; }
        public string mobilenumber { get; set; }

    }
}
