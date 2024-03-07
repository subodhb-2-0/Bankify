using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UserManager
{
    public class UserDetailDto
    {

        public long UserId { get; set; }
        public string LoginId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string Status { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public int ReportsTo { get; set; }
        public int RoleId { get; set; }

          public string RoleName { get; set; }
        public string OrgCode { get; set; }
        public string OrgId { get; set; }
    }
}
