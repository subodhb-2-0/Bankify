using Domain.Entities.Common;

namespace Domain.Entities.Onboarding
{
    public class OrgCpDetails : BaseEntity
    {
        public int orgCpDetailsId { get; set; }
        public int orgId { get; set; }
        public int dob { get; set; }
        public int perm_house_no { get; set; }
        public int perm_road { get; set; }
        public int perm_dist { get; set; }
        public int perm_sub_dist { get; set; }
        public string perm_pincode { get; set; }
        public int perm_landmark { get; set; }
        public string perm_addr_proof { get; set; }
        public string busi_house_no { get; set; }
        public string busi_road { get; set; }
        public string busi_district { get; set; }
        public string busi_sub_district { get; set; }
        public string busi_pincode { get; set; }
        public string busi_landmark { get; set; }
        public string busi_addr_proof { get; set; }
        public int productId { get; set; }
        public int status { get; set; }
        public int gender { get; set; }
        public int isHandiCapped { get; set; }
        public int occupationType { get; set; }
        public int device { get; set; }
        public int bcType { get; set; }
    }
}