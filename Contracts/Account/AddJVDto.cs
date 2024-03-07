using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Account
{
    public class AddJVDto
    {
        public string jvname { get; set; }
        public string description { get; set; }
        public int status { get; set; }
        public int jvstatus { get; set; }
        public int creator { get; set; }
    }
}
