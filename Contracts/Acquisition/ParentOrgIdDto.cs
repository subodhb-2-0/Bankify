using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Acquisition
{
    public class ParentOrgIdDto 
    {
        public int orgId { get; set; }
        public string orgName { get; set; }
    }
}
