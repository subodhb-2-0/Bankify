using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Role
{
    public class AssignRoleDto
    {
        public string LoginId { get; set; }
        public int RoleId { get; set; }
    }
}
