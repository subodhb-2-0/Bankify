using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class channelserviceSalesandCancelResponseModel
    {
        public string P_Category { get; set; }
        public long totalcount { get; set; }
        public string P_Service { get; set; }
        public int p_serviceid { get; set; }
        public string P_Vendor { get; set; }
        public long P_Txn_Count { get; set; }
        public double P_Value { get; set; }
        public long P_Cancellation_Count { get; set; }
        public double P_Cancellation_Value { get; set; }
        public long P_RetailerCount { get; set; }
    }
}
