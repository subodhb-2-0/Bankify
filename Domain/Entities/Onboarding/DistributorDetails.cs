using Domain.Entities.Common;
namespace Domain.Entities.Onboarding
{
    public class DistributorDetails : BaseEntity
    {
        public int orgid { get; set; }
        public string orgcode { get; set; }
        public string orgname { get; set; }
        public double runningbalance { get; set; }
    }
}