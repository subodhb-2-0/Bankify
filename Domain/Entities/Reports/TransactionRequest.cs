using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class TransactionRequest
    {
        public string fromdate { get; set; }
        public string todate { get; set; }
        public int distributorid { get; set; }
        public int txnstatus { get; set; }
        public int txnid { get; set; }
        public int retailerorgid { get; set; }
        public int offsetrows { get; set; }
        public int fetchrows { get; set; }
    }
}
