using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Account
{
    public class ListOfOrgTypeDto
    {
        public int org_type_id { get; set; }
        public string org_type { get; set; }
        public string org_type_desc { get; set; }
    }
}
