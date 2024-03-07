using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class DistributerSalsAndCancelationResponse
    {
        public string distributor { get; set; }
        public string servicename { get; set; }
        public int distributorid { get; set; }
        public int serviceid { get; set; }
        public long totalcount { get; set; }
        public long txn_count { get; set; }
        public double Value { get; set; }
        public long cancellation_count { get; set; }
        public double cancellation_value { get; set; }
    }
}
