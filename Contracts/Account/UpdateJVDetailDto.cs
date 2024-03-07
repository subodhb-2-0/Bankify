using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Account
{
    public class UpdateJVDetailDto
    {
        public int jvdetailsid { get; set; }
        public int status { get; set; }
        public int ModifierBy { get; set; }
    }
}
