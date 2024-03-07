using Domain.Entities.Common;

namespace Domain.Entities.Onboarding
{
    public class OrgType : BaseEntity
    {
        public int org_type_id { get; set; }
        public string org_type { get; set; }
        public string org_type_desc { get; set; }
    }
}