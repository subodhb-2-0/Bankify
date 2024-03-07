using Domain.Entities.Common;
namespace Domain.Entities.Onboarding
{
    public class RetailerBalance : BaseEntity
    {
        public int orgid { get; set; }
        public string orgname { get; set; }
    }
}