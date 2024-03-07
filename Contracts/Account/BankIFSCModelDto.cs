using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Account
{
    public class BankIFSCModelDto
    {
        public string ifsc_code { get; set; }
        public int bankid { get; set; }
        public string micr_code { get; set; }
        public string branch_name { get; set; }
        public string address { get; set; }
        public string contact { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public bool ifsc_enabled { get; set; }
        public string bankname { get; set; }
        public int status { get; set; }
    }
}
