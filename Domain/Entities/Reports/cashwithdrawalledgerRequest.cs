using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class cashwithdrawalledgerRequest
    {
        public string p_fromdate { get; set; }
        public string p_todate { get; set; }
        public int p_orgid { get; set; }
        public int p_offsetrows { get; set; }
        public int p_fetchrows { get; set; }
        public int p_count { get; set; }
        public double p_debit { get; set; }
        public double p_credit { get; set; }
    }
}
