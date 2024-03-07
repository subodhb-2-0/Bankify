using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Acquisition
{
    public class ListofFeildStaffDto 
    {
        public long userId { get; set; }
        public string firstName { get; set; }
        public string lastname { get; set; }
        public string rolename { get; set; }
    }
}
