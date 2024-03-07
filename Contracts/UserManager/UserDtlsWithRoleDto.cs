using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UserManager
{
    public class UserDtlsWithRoleDto
    {
        public long UserId { get; set; }
        public int OrgId { get; set; }
        public string LoginId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string RoleName { get; set; }
        public string Dept { get; set; }
        public string Desig { get; set; }
        public string Status { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public int ReportsTo { get; set; }
        public int RoleId { get; set; }
    }
}
