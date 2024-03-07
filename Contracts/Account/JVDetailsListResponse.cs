using Contracts.Acquisition;
using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Account
{
    public class JVDetailsListResponse
    {
        public int PageSize { get; set; }
        public int TotalRecord { get; set; }
        public int PageNumber { get; set; }
        public string OrderBy { get; set; }
        public string orderByColumn { get; set; }
        public IEnumerable<JVDetailsListDto> jVDetailsListDtos { get; set; }
    }
}
