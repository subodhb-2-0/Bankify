namespace Contracts.Onboarding
{
    public class GetEmployeeDetailsByMobilenoAndCodeDto
    {
        public string ReportTo { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
    }
}
