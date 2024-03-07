using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UserManager
{
    public class UserDetailResponse
    {
        public int PageSize { get; set; }
        public int TotalRecord { get; set; }
        public int PageNumber { get; set; }

        public string OrderBy { get; set; }

        public string orderByColumn{ get; set; }
        public IEnumerable<UserDetailDto> UserDetails { get; set; }
    }
}
