using Contracts.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class UsersDto : BaseEntityDto
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



    }
    public class UpdateUsersDto : BaseEntityDto
    {
        public UpdateUsersDto()
        {
            MiddleName = String.Empty;
        }

        [Required]
        public string FirstName { get; set; }


        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }
        [Required]
        public string LoginId { get; set; }
        [Required]
        public long? OrgId { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public int? DepartmentId { get; set; }
        [Required]
        public int? DesignationId { get; set; }
        [Required]
        public int? ReportsTo { get; set; }
        [Required]
        public int? RoleId { get; set; }
 
    }
}
