using Domain.Entities.Common;

namespace Domain.Entities.Onboarding
{
    public class Onboarding : BaseEntity
    {
        public int cpbOnboarding { get; set; }
        public int supplierId { get; set; }
        public string supplier_csp_id { get; set; }
        public string token { get; set; }
        public string deviceId { get; set; }
        public int onboadingStatus { get; set; }
        public string orgId { get; set; }
        public int serviceId { get; set; }
    }
}