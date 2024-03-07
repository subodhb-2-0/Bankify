using Domain.Entities.Common;

namespace Domain.Entities.Onboarding
{
    public class OrgConsent : BaseEntity
    {
        public int consentId { get; set; }
        public string consentType { get; set; }
        public int orgId { get; set; }
        public string consentDesc { get; set; }
        public int status { get; set; }
        public string ref_param1 { get; set; }
        public string ref_param2 { get; set; }
    }
}