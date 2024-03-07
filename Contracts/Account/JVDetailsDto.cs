using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Account
{
    public class JVDetailsDto
    {
        public int jvno { get; set; }
        public int orgtype { get; set; }
        public int accid { get; set; }
        public int orgid { get; set; }
        public decimal amount { get; set; }
        public string debitcredit { get; set; }
        public int creator { get; set; }
        public int status { get; set; }
        public string narration { get; set; }
    }
}
