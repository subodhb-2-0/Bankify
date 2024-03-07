using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class RetailerTxnReportResponse
    {
        public long totalcount { get; set; }
        public long paymentid { get; set; }
        public int orgid { get; set; }
        public string ratailercode { get; set; }
        public string retailername { get; set; }
        public double amount { get; set; }
        public int status { get; set; }
        public DateTime creationdate { get; set; }
    }
}
