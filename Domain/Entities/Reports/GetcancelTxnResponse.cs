using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class GetcancelTxnResponse
    {
        public string Retailer { get; set; }
        public string Distributor { get; set; }
        public string ServiceName { get; set; }
        public string SupplierName { get; set; }
        public string ServProvider { get; set; }
        public long TxnCount { get; set; }
        public decimal TotalTxnValue { get; set; }
        public decimal TotalCommRecb { get; set; }
        public decimal TotalCommRevToRet { get; set; }
        public decimal SupplierTxnValue { get; set; }
        public decimal TotalDistCommPayout { get; set; }
    }
}
