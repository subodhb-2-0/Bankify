using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Report
{
    public class GetcancelTxnDto
    {
        public long service_id { get; set; }
        public string p_fromdt { get; set; }
        public string p_todt { get; set; }
        public int txn_status { get; set; }
    }
}
