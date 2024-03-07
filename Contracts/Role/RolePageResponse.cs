﻿using Contracts.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Role
{
    public class RolePageResponse
    {
        public int? PageSize { get; set; }
        public int? TotalRecord { get; set; }
        public int? PageNumber { get; set; }
        public string? OrderBy { get; set; }
        public string? orderByColumn { get; set; }
        public IEnumerable<RoleDto> roleDtos   { get; set; }
    }
}
