using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserManagement
{
    public class UserEdit
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
