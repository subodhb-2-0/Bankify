using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Report
{
    public class InventoryReportRequestDto
    {
        public string p_fromdate {  get; set; }
        public string p_todate { get; set; }
        public int p_offsetrows { get; set; }
        public int p_fetchrows { get; set; }
    }
}
