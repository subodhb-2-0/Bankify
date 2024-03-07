namespace Contracts.Report
{
    public class LedgerRequestDto
    {
        public int OrgId { get; set; }
        public int OrgType { get; set; }
        public int OrgCode { get; set; }
        public int fromDate { get; set; }
        public int toDate { get; set; }
    }
}