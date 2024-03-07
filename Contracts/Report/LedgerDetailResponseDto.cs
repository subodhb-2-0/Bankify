namespace Contracts.Report
{
    public class LedgerDetailResponseDto
    {
        public int orgId { get; set; }
        public string orgName { get; set; }
        public decimal openingBalance { get; set; }
        public decimal closingBalance { get; set; }
        public int totalDebits { get; set; }
        public int totalCredits { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public LedgerDetail data { get; set; }
    }
}