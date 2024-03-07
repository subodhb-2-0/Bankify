using Contracts.Common;

namespace Contracts.Report
{
    public class LedgerDetail : BaseEntityDto
    {
        public string ledgerId { get; set; }
        public DateTime date { get; set; }
        public string accountId { get; set; }
        public string accountName { get; set; }
        public string transactionId { get; set; }
        public int paymentId { get; set; }
        public string narration1 { get; set; }
        public string narration2 { get; set; }
        public decimal debit { get; set; }
        public decimal credit { get; set; }
        public int status { get; set; }
        public string jvNo { get; set; }
        public string srNo { get; set; }
    }
}