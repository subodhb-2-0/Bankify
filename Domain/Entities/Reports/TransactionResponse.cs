using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Reports
{
    public class TransactionResponse
    {
        public int totalcount { get; set; }
        public DateTime datetime { get; set; }
        public int txnid { get; set; }
        public string orgname { get; set; }
        public int orgid { get; set; }
        public double amt { get; set; }
        public string txntype { get; set; }
        public string status { get; set; }
        public string servicename { get; set; }
        public string supplier { get; set; }
        public string serviceprovider { get; set; }
    }
}
