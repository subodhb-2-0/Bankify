using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class InventoryReportRequestModel
    {
        public string p_fromdate { get; set; }
        public string p_todate { get; set; }
        public int p_offsetrows { get; set; }
        public int p_fetchrows { get; set; }
    }
}
