using Contracts.Common;
using Contracts.WorkingCapital;

namespace Contracts.Report
{
    public class TransactionRequestDto 
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