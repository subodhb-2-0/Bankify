using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class GetPaymentHistory
    {
        public int p_orgid { get; set; }
        public string p_fromdate { get; set; }
        public string p_todate { get; set; }
    }
}
