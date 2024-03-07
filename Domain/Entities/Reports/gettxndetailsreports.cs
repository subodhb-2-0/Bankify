using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class gettxndetailsreports
    {
        public int p_offsetrows { get; set; }
        public int p_fetchrows { get; set; }
        public int p_orgid { get; set; }
        public string p_fromdate { get; set; }
        public string p_todate { get; set; }
        public string p_transactionids { get; set; }
        public int p_supplierid { get; set; }
        public int p_serviceid { get; set; }
        public int p_txnstatus { get; set; }
        public string p_rtorgcode { get; set; }
        public int p_serviceproviderid { get; set; }
    }
}
