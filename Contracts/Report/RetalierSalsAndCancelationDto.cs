using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Report
{
    public class RetalierSalsAndCancelationDto
    {
        public int p_offsetrows { get; set; }
        public int p_fetchrows { get; set; }
        public long p_serviceid { get; set; }
        public int p_distributorid { get; set; }
        public string p_fromdt { get; set; }
        public string p_todt { get; set; }
        public int statusone { get; set; }
        public int statustwo { get; set; }
    }
}
