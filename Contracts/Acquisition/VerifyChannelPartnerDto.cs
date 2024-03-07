using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Acquisition
{
    public class VerifyChannelPartnerDto
    {
        public long orgId { get; set; }
        public string orgcode { get; set; }
        public string orgName { get; set; }
        public string org_type { get; set; }
        public string mobilenumber { get; set; }
    }
}
