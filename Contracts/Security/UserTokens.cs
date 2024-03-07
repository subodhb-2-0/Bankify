using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Security
{
    public class UserTokens
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Token
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            get;
            set;
        }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string UserName
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            get;
            set;
        }
        public TimeSpan Validaty
        {
            get;
            set;
        }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string RefreshToken
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            get;
            set;
        }
        public long Id
        {
            get;
            set;
        }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string EmailId
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            get;
            set;
        }
        public Guid GuidId
        {
            get;
            set;
        }
        public DateTime ExpiredTime
        {
            get;
            set;
        }
        //code added by biswa
        public string OrgName { get; set; }
        public int OrgType { get; set; }
        public int ParentorgId { get; set; }
        public int ParentorgType { get; set; }
        public string ParentOrgName { get; set; }
        public int CreditLimit { get; set; }
        public int ProductId { get; set; }
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public long OrgId { get; set; }
        public string MobileNumber { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public int ReportsTo { get; set; }
        public int InfiniteCreditLimit { get; set; }
        public int NeedsPassChnage { get; set; }
        public int RoleId { get; set; }
        public int Status { get; set; }
        public int Creator { get; set; }
        public DateTime? CreationDate { get; set; }
        public int Modifier { get; set; }
        public DateTime? ModificationDate { get; set; }
        public DateTime? LastPwdChangeDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
    }
}
