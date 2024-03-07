namespace Contracts.Onboarding
{
    public class GetCPDetailsByMobileNumberDto
    {
        public string RTName { get; set; }
        public string LoginCode { get; set; }
        public int OrgId { get; set; }
        public string MappedEmployeeName { get; set; }
        public int MappedEmployeeId { get; set; }
        public int Status { get; set; }
    }
}
