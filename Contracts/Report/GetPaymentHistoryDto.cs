using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Report
{
    public class GetPaymentHistoryDto
    {
        public int p_orgid { get; set; }
        public string p_fromdate { get; set; }
        public string p_todate { get; set; }
    }
}
