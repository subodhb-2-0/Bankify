using Contracts.Common;
using Contracts.UserManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Account
{
    public class DynamicSearchJVResponse
    {
        public IEnumerable<DynamicSearchJVDto> jVDetailsListDtos { get; set; }
    }
}
