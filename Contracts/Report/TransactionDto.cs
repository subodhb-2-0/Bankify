using Contracts.Common;
using Contracts.WorkingCapital;

namespace Contracts.Report
{
    public class TransactionDto : BaseEntityDto
    {
        public string transactionId { get; set; }
        public int sericeId { get; set; }
        public int spId { get; set; }
        public int supplierId { get; set; }
        public int orgId { get; set; }
        public string orgCode { get; set; }
        public string orgName { get; set; }
        public string serviceName { get; set; }
        public string supplierName { get; set; }
        public string spname { get; set; }
        public string city { get; set; }
        public DateTime transactionDate { get; set; }
        public string serviceProviderName { get; set; }
        public string serviceProviderCode { get; set; }
        public decimal txnAmount { get; set; }
        public PaymentStatus status { get; set; }
        public TransactionType transactionType { get; set; }
    }
}