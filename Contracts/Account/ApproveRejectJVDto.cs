using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Account
{
    public class ApproveRejectJVDto
    {
        public int jvno { get; set; }
        public int status { get; set; }
        public int modifier { get; set; }
        public int currentstatus { get; set; }

    }
}
