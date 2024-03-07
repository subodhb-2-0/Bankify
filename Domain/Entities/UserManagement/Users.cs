using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.UserManagement
{
    public class Users : BaseEntity
    {
        public long UserId { get; set; }
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public string LastName { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }

        public long OrgId { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string AlternateNumber { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public int ReportsTo { get; set; }
        public int InfiniteCreditLimit { get; set; }
        public int NeedsPassChnage { get; set; }
        public int RoleId { get; set; }
        public int FailedCount { get; set; }
        public int Status { get; set; }

        public int Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public int Modifier { get; set; }
        public DateTime ModificationDate { get; set; }
        public DateTime LastPwdChangeDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int IsPassCodeExists { get; set; }

        public string OrgCode { get; set; }
        public string RoleName { get; set; }
        //public decimal CreditLimit { get; set; }
        public int CreditLimit { get; set; }

        public int NeedsPassChange { get; set; }
        public string OrgName { get; set; }
        public int OrgType { get; set; }
        public int ParentorgId { get; set; }
        public int ParentorgType { get; set; }
        public string ParentOrgName { get; set; }
        public int ProductId { get; set; }
        
    }
}
