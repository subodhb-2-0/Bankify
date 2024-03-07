using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Account
{
    public class AccountDetailsByOrgIDDto
    {
        public int p_count { get; set; }
        public string organisation { get; set; }
        public string p_acountdescription { get; set; }
        public string p_createdby { get; set; }
        public DateTime p_creationdate { get; set; }
        public string p_modifiedby { get; set; }
        public DateTime p_modificationdate { get; set; }
    }
}
