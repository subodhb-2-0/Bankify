using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class RetailerTxnReport
    {
        public int p_offsetrows { get; set; }
        public int p_fetchrows { get; set; }
        public string p_retailercode { get; set; }
        public string p_fromdt { get; set; }
        public string p_todt { get; set; }
        public int p_status { get; set; }

    }
}
