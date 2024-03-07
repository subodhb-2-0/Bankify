using Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UserManager
{
    public class UserEditDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public long ReportsTo { get; set; }
        public int RoleId { get; set; }
        public long UserId { get; set; }
    }
}
