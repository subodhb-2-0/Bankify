using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Account
{
    public class ApproveRejectWCDto
    {
        public int paymentid { get; set; }
        //public int depositstatus { get; set; }
        public int status { get; set; }
        public int userid { get; set; }
        public string remark { get; set; }
        //public string instrumentid { get; set; }
    }
}
