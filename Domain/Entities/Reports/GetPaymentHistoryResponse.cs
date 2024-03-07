using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class GetPaymentHistoryResponse
    {
        public long totalCount { get; set; }
        public long paymentId { get; set; }
        public DateTime creationDate { get; set; }
        public string orgCode { get; set; }
        public string orgName { get; set; }
        public double amount { get; set; }
        public int paymentMode { get; set; }
        public string instrumentNumber { get; set; }
        public string bankAccount { get; set; }
        public string issuingIFSCCode { get; set; }
        public int status { get; set; }
        public DateTime modificationDate { get; set; }
        public string bankName { get; set; }
        public string bankRefNo { get; set; }
        public int orgType { get; set; }
        public int pgId { get; set; }
        public string remark { get; set; }
        public string bankPayInSlip { get; set; }
    }
}
