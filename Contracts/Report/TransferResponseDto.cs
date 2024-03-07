namespace Contracts.Report
{
    public class TransferResponseDto
    {
        public int paymentId { get; set; }
        public DateTime paymentDate { get; set; }
        public int paymentOrgId { get; set; }
        public int orgId { get; set; }
        public string orgCode { get; set; }
        public string orgName { get; set; }
        public decimal transferAmount { get; set; }
        public int paymentMode { get; set; }
        public int status { get; set; }
    }
}