namespace Contracts.Onboarding
{
    public class GetCPDetailsByOrgCodeDto
    {
        public int OrgId { get; set; }
        public string RTName { get; set; }
        public string RTId { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
    }
}
