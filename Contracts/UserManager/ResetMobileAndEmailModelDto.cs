using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UserManager
{
    public class ResetMobileAndEmailModelDto
    {
        public string mobilenumber { get; set; }
        public string emailid { get; set; }
        public string loginid { get; set; }
    }
}
