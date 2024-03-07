using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class txndetailsresponse
    {
        public long txncount { get; set; }
        public long TransactionId { get; set; }
        public DateTime TxnDate { get; set; }
        public float SupplierTxnValue { get; set; }
        public string TxnType { get; set; }
        public string FirstName { get; set; }
        public string OrgName { get; set; }
        public float RetCommPayout { get; set; }
        public float DistCommPayout { get; set; }

        public float SuperDistCommPayout { get; set; }
        public float CommRecb { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public float TxnValue { get; set; }
        public string SuppTxnNumber { get; set; }
        public string OrgCode { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ServiceProviderName { get; set; }
        public string Remarks { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }

        public string TransType { get; set; }
    }
}
